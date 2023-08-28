using Assets.Scripts.Inventory.Database;
using Assets.Scripts.Misc;
using Assets.Scripts.Mouse;
using Assets.Scripts.ScreenManager;
using Assets.Scripts.Users;
using Assets.Scripts.Web;
using System.Collections;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.UIElements;

namespace Assets.Scripts.MainMenu
{
    public class MainMenuManager : MonoBehaviour
    {
        #region Login
        private VisualElement _loginPanel;
        private TextField _userInput;
        private TextField _passwordInput;
        private Button _loginShowHidePasswordButton;
        #endregion
        #region NewUser
        private VisualElement _newUserPanel;
        private TextField _addNewUserInput;
        private TextField _addNewPasswordInput;
        private Button _closeNewUserPanelButton;
        private Button _openAdminAuthorizationPanelButton;
        private Button _newUserShowHidePasswordButton;
        #endregion
        #region Admin
        private VisualElement _adminAuthorizationPanel;
        private TextField _adminUserInput;
        private TextField _adminPasswordInput;
        private Button _closeAdminAuthorizationPanelButton;
        private Button _adminShowHidePasswordButton;
        #endregion
        private Button _openAddNewUserPanelButton;
        
        private bool _loginEnabled = true; // enable or disable the Enter key press to login
        private bool _adminAuthorizing = false;
        private bool _inputEnabled = true;
        private int _authorizationAccessLevel; // used to check if the person authorizing the creation of a new user have high enough access level
                                              //  private bool isWindows = false;

        private Label _versionLabel;

        [SerializeField] private Texture2D _eyeClosedTexture;
        [SerializeField] private Texture2D _eyeOpenTexture;

        /// <summary>
        /// Initializes the varibles
        /// </summary>
        private void Start()
        {
            _loginEnabled = true;
            _adminAuthorizing = false;
            _inputEnabled = true;
            _authorizationAccessLevel = 0;

            //CheckIfCurrentPlatformIsWindows();
        }

        private void OnEnable()
        {
            EventHandler.PostRequestResponse += GetUserAccessLevel;
            GetUiElementsReferences();
            _versionLabel.text = InternalDatabase.Instance.currentVersion;
        }

        private void OnDisable()
        {
            EventHandler.PostRequestResponse -= GetUserAccessLevel;
            UnsubscribeUIElementsToEvents();
        }

        /// <summary>
        /// Handles what happens if enter is pressed
        /// </summary>
        private void Update()
        {
            if (_inputEnabled)
            {
                if (Input.GetKeyDown(KeyCode.KeypadEnter) || Input.GetKeyDown(KeyCode.Return))
                {
                    if (_loginEnabled)
                    {
                        _inputEnabled = false;
                        if (InternalDatabase.Instance.isOfflineProgram)
                        {
                            LoginOffline();
                        }
                        else
                        {
                            StartCoroutine(Login());
                        }
                    }
                    if (_adminAuthorizing)
                    {
                        _inputEnabled = false;
                        StartCoroutine(CheckIfAdmin());
                    }
                }
            }
        }

        /// <summary>
        /// Get the references of all UI elements that will have some functionality
        /// </summary>
        private void GetUiElementsReferences()
        {
            VisualElement root = GetComponent<UIDocument>().rootVisualElement;
            _loginPanel = root.Q<VisualElement>("LoginContainer");
            _userInput = root.Q<TextField>("UserTextField");
            _passwordInput = root.Q<TextField>("PasswordTextField");
            _newUserPanel = root.Q<VisualElement>("NewUserContainer");
            _addNewUserInput = root.Q<TextField>("NewUserTextField");
            _addNewPasswordInput = root.Q<TextField>("NewPasswordTextField");
            _openAddNewUserPanelButton = root.Q<Button>("AddNewUserButton");
            _adminAuthorizationPanel = root.Q<VisualElement>("AdminAuthorizationContainer");
            _adminUserInput = root.Q<TextField>("AdminUserTextField");
            _adminPasswordInput = root.Q<TextField>("AdminPasswordTextField");
            _closeAdminAuthorizationPanelButton = root.Q<Button>("AdminCancelButton");
            _closeNewUserPanelButton = root.Q<Button>("CancelNewuserButton");
            _openAdminAuthorizationPanelButton = root.Q<Button>("OpenAdminAuthorizeButton");
            _loginShowHidePasswordButton = root.Q<Button>("LoginShowHidePassword");
            _newUserShowHidePasswordButton = root.Q<Button>("NewUserShowHidePassword");
            _adminShowHidePasswordButton = root.Q<Button>("AdminShowHidePassword");
            _versionLabel = root.Q<Label>("Version");
            SubscribeUIElementsToEvents();
        }

        private void SubscribeUIElementsToEvents()
        {
            _openAddNewUserPanelButton.clicked += () => { ShowAddNewUserPanel(); };
            _closeAdminAuthorizationPanelButton.clicked += () => { CloseAdminPanel(); };
            _closeNewUserPanelButton.clicked += () => { CancelAddNewUser(); };
            _openAdminAuthorizationPanelButton.clicked += () => { AdminAuthorizeAddNewUserClicked(); };
            _loginShowHidePasswordButton.clicked += () => { ShowHidePassword(); };
            _newUserShowHidePasswordButton.clicked += () => { ShowHidePassword(); };
            _adminShowHidePasswordButton.clicked += () => { ShowHidePassword(); };
        }

        private void UnsubscribeUIElementsToEvents()
        {
            _openAddNewUserPanelButton.clicked -= () => { ShowAddNewUserPanel(); };
            _closeAdminAuthorizationPanelButton.clicked -= () => { CloseAdminPanel(); };
            _closeNewUserPanelButton.clicked -= () => { CancelAddNewUser(); };
            _openAdminAuthorizationPanelButton.clicked -= () => { AdminAuthorizeAddNewUserClicked(); };
            _loginShowHidePasswordButton.clicked -= () => { ShowHidePassword(); };
            _newUserShowHidePasswordButton.clicked -= () => { ShowHidePassword(); };
            _adminShowHidePasswordButton.clicked -= () => { ShowHidePassword(); };
        }

        /// <summary>
        /// Get the user Accces level when it try to login or atuthorize the creation of a new user
        /// </summary>
        private void GetUserAccessLevel(string accessLevel)
        {
            _authorizationAccessLevel = int.Parse(accessLevel);
        }

        ///// <summary>
        ///// Check if the current platform that the program is running is Windows or not.
        ///// </summary>
        //private void CheckIfCurrentPlatformIsWindows()
        //{
        //    if (Application.platform == RuntimePlatform.WindowsEditor || Application.platform == RuntimePlatform.WindowsPlayer)
        //    {
        //        isWindows = true;
        //    }
        //    else
        //    {
        //        isWindows = false;
        //    }
        //}

        private void LoginOffline()
        {
            if (_userInput.text == UsersManager.Instance.Admin.GetUsername() && _passwordInput.text == UsersManager.Instance.Admin.GetPassword())
            {
                UsersManager.Instance.CurrentUser = new User(_userInput.text, 10);
                LoadScreen();
            }
        }

        /// <summary>
        /// Check if the person authorazing the creation of a new user have the correct authorization acces level
        /// </summary>
        private IEnumerator CheckIfAdmin()
        {
            if ((_adminUserInput.text == "" || _adminUserInput.text == "Digite seu usuário") || (_adminPasswordInput.text == "" || _adminPasswordInput.text == "Digite sua senha"))
            {
                EventHandler.CallIsOneMessageOnlyEvent(true);
                EventHandler.CallOpenMessageEvent("Empty values");
                yield break;
            }

            WWWForm checkAccesssLevelForm = CreateForm.GetCheckAccessLevelForm(ConstStrings.LoginKey, _adminUserInput.text, _adminPasswordInput.text);

            UnityWebRequest createPostRequest = CreatePostRequest.GetPostRequest(checkAccesssLevelForm, ConstStrings.CheckAccessLevel, 0);

            yield return createPostRequest.SendWebRequest();
            if (HandlePostRequestResponse.HandleWebRequest(createPostRequest))
            {
                if (_authorizationAccessLevel == 2 || _authorizationAccessLevel > 4)
                {
                    User userToAdd = new User(_addNewUserInput.text, _addNewPasswordInput.text);
                    StartCoroutine(AddNewUser(userToAdd));
                }
                else
                {
                    _inputEnabled = true;
                    EventHandler.CallIsOneMessageOnlyEvent(true);
                    EventHandler.CallOpenMessageEvent("Wrong authorization access level");
                    yield break;
                }
            }
        }

        /// <summary>
        /// Login into the system. Different users have different access
        /// </summary>
        private IEnumerator Login()
        {
            if ((_userInput.text == "" || _userInput.text == "Digite seu usuário") || (_passwordInput.text == "" || _passwordInput.text == "Digite sua senha"))
            {
                EventHandler.CallIsOneMessageOnlyEvent(true);
                EventHandler.CallOpenMessageEvent("Empty values");
                yield break;
            }
            WWWForm loginUserInfo = new WWWForm();
            loginUserInfo.AddField("apppassword", "LoginUser");
            loginUserInfo.AddField("username", _userInput.text);
            loginUserInfo.AddField("password", _passwordInput.text);

            UnityWebRequest createPostRequest = CreatePostRequest.GetPostRequest(loginUserInfo, ConstStrings.CheckLoginUser, 0);

            MouseManager.Instance.SetWaitingCursor();
            _inputEnabled = false;
            yield return createPostRequest.SendWebRequest();

            if (HandlePostRequestResponse.HandleWebRequest(createPostRequest))
            {
                UsersManager.Instance.CurrentUser = new User(_userInput.text, _authorizationAccessLevel);
                LoadScreen();
            }

            MouseManager.Instance.SetDefaultCursor();
            _inputEnabled = true;
        }

        /// <summary>
        /// Checks on the online database if the user already exists
        /// </summary>
        private IEnumerator CheckIfUserAlreadyExists()
        {
            WWWForm newUserInfo = new WWWForm();
            newUserInfo.AddField("apppassword", "CheckIfUserExist");
            newUserInfo.AddField("username", _addNewUserInput.text);

            UnityWebRequest createPostRequest = CreatePostRequest.GetPostRequest(newUserInfo, ConstStrings.CheckUserExist, 0);

            MouseManager.Instance.SetWaitingCursor();
            _inputEnabled = false;
            yield return createPostRequest.SendWebRequest();

            if (HandlePostRequestResponse.HandleWebRequest(createPostRequest))
            {
                _newUserPanel.style.display = DisplayStyle.None;
                _adminAuthorizationPanel.style.display = DisplayStyle.Flex;
                _adminAuthorizing = true;
            }

            MouseManager.Instance.SetDefaultCursor();
            _inputEnabled = true;
        }

        /// <summary>
        /// Adds a new user to the online user database
        /// </summary>
        private IEnumerator AddNewUser(User userToAdd)
        {
            WWWForm newUserInfo = new WWWForm();
            newUserInfo.AddField("apppassword", "InsertNewUser");
            newUserInfo.AddField("username", userToAdd.GetUsername());
            newUserInfo.AddField("password", userToAdd.GetPassword());

            UnityWebRequest createPostRequest = CreatePostRequest.GetPostRequest(newUserInfo, ConstStrings.CheckNewUser, 0);

            MouseManager.Instance.SetWaitingCursor();
            _inputEnabled = false;
            yield return createPostRequest.SendWebRequest();
            if (HandlePostRequestResponse.HandleWebRequest(createPostRequest))
            {
                _adminAuthorizationPanel.style.display = DisplayStyle.None;
                _adminAuthorizing = false;
                _loginPanel.style.display = DisplayStyle.Flex;
                _openAddNewUserPanelButton.style.display = DisplayStyle.Flex;
                _loginEnabled = true;
            }

            MouseManager.Instance.SetDefaultCursor();
            _inputEnabled = true;
        }

        /// <summary>
        /// Loads the next screen. What will be available on this next screen is dependent if it is an admin loging in or not
        /// </summary>
        private void LoadScreen()
        {
            EventHandler.CallFillInternalDatabase();
            ChangeScreenManager.Instance.OpenScene(Scenes.MainMenu, Scenes.InitialScene);
        }

        /// <summary>
        /// Show the panel to add a new user
        /// </summary>
        private void ShowAddNewUserPanel()
        {
            _openAddNewUserPanelButton.style.display = DisplayStyle.None;
            _loginPanel.style.display = DisplayStyle.None;
            _newUserPanel.style.display = DisplayStyle.Flex;

            _adminAuthorizing = false;
        }

        /// <summary>
        /// Opens screen to enter admin username and password to authorize the adition of the 
        /// new user if the user does not exist
        /// </summary>
        private void AdminAuthorizeAddNewUserClicked()
        {
            if ((_addNewUserInput.text == "" || _addNewUserInput.text == "Digite o usuário a ser adicionado") || (_addNewPasswordInput.text == "" || _addNewPasswordInput.text == "Digite a senha do novo usuário"))
            {
                EventHandler.CallIsOneMessageOnlyEvent(true);
                EventHandler.CallOpenMessageEvent("Empty values");
                return;
            }
            else
            {
                _loginEnabled = false;


                StartCoroutine(CheckIfUserAlreadyExists());
            }
        }

        /// <summary>
        /// show or hide the password
        /// </summary>
        private void ShowHidePassword()
        {
            if (_newUserPanel.style.display == DisplayStyle.Flex)
            {
                if (_addNewPasswordInput.isPasswordField)
                {
                    _addNewPasswordInput.isPasswordField = false;
                    _newUserShowHidePasswordButton.style.backgroundImage = _eyeOpenTexture;
                }
                else
                {
                    _addNewPasswordInput.isPasswordField = true;
                    _newUserShowHidePasswordButton.style.backgroundImage = _eyeClosedTexture;
                }
            }
            if (_adminAuthorizationPanel.style.display == DisplayStyle.Flex)
            {
                if (_adminPasswordInput.isPasswordField)
                {
                    _adminPasswordInput.isPasswordField = false;
                    _adminShowHidePasswordButton.style.backgroundImage = _eyeOpenTexture;
                }
                else
                {
                    _adminPasswordInput.isPasswordField = true;
                    _adminShowHidePasswordButton.style.backgroundImage = _eyeClosedTexture;
                }
            }
            else
            {
                if (_passwordInput.isPasswordField)
                {
                    _passwordInput.isPasswordField = false;
                    _loginShowHidePasswordButton.style.backgroundImage = _eyeOpenTexture;
                }
                else
                {
                    _passwordInput.isPasswordField = true;
                    _loginShowHidePasswordButton.style.backgroundImage = _eyeClosedTexture;
                }
            }
        }

        /// <summary>
        /// Cancel the adition of a new user and returns to login panel
        /// </summary>
        private void CancelAddNewUser()
        {
            _addNewPasswordInput.SetValueWithoutNotify("Digite a senha do novo usuário");
            _addNewUserInput.SetValueWithoutNotify("Digite o usuário a ser adicionado");
            _newUserPanel.style.display = DisplayStyle.None;
            _loginPanel.style.display = DisplayStyle.Flex;
            _openAddNewUserPanelButton.style.display = DisplayStyle.Flex;
            _loginEnabled = true;
            _adminAuthorizing = false;
            _inputEnabled = true;
        }

        /// <summary>
        /// Close the admin authorization panel and returns to the add new user panel
        /// </summary>
        private void CloseAdminPanel()
        {
            _adminUserInput.SetValueWithoutNotify("");
            _adminPasswordInput.SetValueWithoutNotify("");
            _adminAuthorizationPanel.style.display = DisplayStyle.None;
            _adminAuthorizing = false;
            _newUserPanel.style.display = DisplayStyle.Flex;
        }
    }
}