using System.Collections;
using UnityEngine;
using UnityEngine.UIElements;
using UnityEngine.Networking;

public class MainMenuManager1 : MonoBehaviour
{
    
    #region Login
    private VisualElement loginPanel;
    private TextField userInput;
    private TextField passwordInput;
    private Button loginShowHidePasswordButton;
    #endregion
    #region NewUser
    private VisualElement newUserPanel;
    private TextField addNewUserInput;
    private TextField addNewPasswordInput;
    private Button closeNewUserPanelButton;
    private Button openAdminAuthorizationPanelButton;
    private Button newUserShowHidePasswordButton;
    #endregion
    #region Admin
    private VisualElement adminAuthorizationPanel;
    private TextField adminUserInput;
    private TextField adminPasswordInput;
    private Button closeAdminAuthorizationPanelButton;
    private Button adminShowHidePasswordButton;
    #endregion
    private Button openAddNewUserPanelButton;
    private VisualElement root;

    private bool loginEnabled = true; // enable or disable the Enter key press to login
    private bool adminAuthorizing = false;
    private bool adminAuthorized = false;
    private bool inputEnabled = true;
    private int authorizationAccessLevel; // used to check if the person authorizing the creation of a new user have high enough access level
    private bool isWindows = false;

    private Label versionLabel;
   

    /// <summary>
    /// Initializes the varibles
    /// </summary>
    private void Start()
    {
        loginEnabled = true;
        adminAuthorizing = false;
        adminAuthorized = false;
         inputEnabled = true;
        authorizationAccessLevel = 0;
        
        CheckIfCurrentPlatformIsWindows();
        // testText.text = string.Compare("estoque", "Estoque", true).ToString();
    //    InternalDatabase.Instance.testingSheet = InternalDatabase.Instance.splitDatabase[ConstStrings.Inventario];
    }

    private void OnEnable()
    {
        EventHandler.PostRequestResponse += GetUserAccessLevel;
        GetUiElementsReferences();
               versionLabel.text = InternalDatabase.Instance.currentVersion;

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
        if (inputEnabled)
        {
            if (Input.GetKeyDown(KeyCode.KeypadEnter) || Input.GetKeyDown(KeyCode.Return))
            {
                if (loginEnabled)
                {
                    inputEnabled = false;
                    if (InternalDatabase.Instance.isOfflineProgram)
                    {
                        LoginOffline();
                    }
                    else
                    {
                        StartCoroutine(Login());
                    }
                }
                if (adminAuthorizing)
                {
                    inputEnabled = false;
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
        root = GetComponent<UIDocument>().rootVisualElement;
        loginPanel = root.Q<VisualElement>("LoginContainer");
        userInput = root.Q<TextField>("UserTextField");
        passwordInput = root.Q<TextField>("PasswordTextField");
        newUserPanel = root.Q<VisualElement>("NewUserContainer");
        addNewUserInput = root.Q<TextField>("NewUserTextField");
        addNewPasswordInput = root.Q<TextField>("NewPasswordTextField");
        openAddNewUserPanelButton = root.Q<Button>("AddNewUserButton");
        adminAuthorizationPanel = root.Q<VisualElement>("AdminAuthorizationContainer");
        adminUserInput = root.Q<TextField>("AdminUserTextField");
        adminPasswordInput = root.Q<TextField>("AdminPasswordTextField");
        closeAdminAuthorizationPanelButton = root.Q<Button>("AdminCancelButton");
        closeNewUserPanelButton = root.Q<Button>("CancelNewuserButton");
        openAdminAuthorizationPanelButton = root.Q<Button>("OpenAdminAuthorizeButton");
        loginShowHidePasswordButton = root.Q<Button>("LoginShowHidePassword");
        newUserShowHidePasswordButton = root.Q<Button>("NewUserShowHidePassword");
        adminShowHidePasswordButton = root.Q<Button>("AdminShowHidePassword");
        versionLabel = root.Q<Label>("Version");
        SubscribeUIElementsToEvents();
    }

    private void SubscribeUIElementsToEvents()
    {
        openAddNewUserPanelButton.clicked += () => { ShowAddNewUserPanel(); };
        closeAdminAuthorizationPanelButton.clicked += () => { CloseAdminPanel(); };
        closeNewUserPanelButton.clicked += () => { CancelAddNewUser(); };
        openAdminAuthorizationPanelButton.clicked += () => { AdminAuthorizeAddNewUserClicked(); };
        loginShowHidePasswordButton.clicked += () => { ShowHidePassword(); };
        newUserShowHidePasswordButton.clicked += () => { ShowHidePassword(); };
        adminShowHidePasswordButton.clicked += () => { ShowHidePassword(); };
    }

    private void UnsubscribeUIElementsToEvents()
    {
        openAddNewUserPanelButton.clicked -= () => { ShowAddNewUserPanel(); };
        closeAdminAuthorizationPanelButton.clicked -= () => { CloseAdminPanel(); };
        closeNewUserPanelButton.clicked -= () => { CancelAddNewUser(); };
        openAdminAuthorizationPanelButton.clicked -= () => { AdminAuthorizeAddNewUserClicked(); };
        loginShowHidePasswordButton.clicked -= () => { ShowHidePassword(); };
        newUserShowHidePasswordButton.clicked -= () => { ShowHidePassword(); };
        adminShowHidePasswordButton.clicked -= () => { ShowHidePassword(); };
    }

    /// <summary>
    /// Get the user Accces level when it try to login or atuthorize the creation of a new user
    /// </summary>
    private void GetUserAccessLevel(string accessLevel)
    {
        authorizationAccessLevel = int.Parse(accessLevel);
    }

    /// <summary>
    /// Check if the current platform that the program is running is Windows or not.
    /// </summary>
    private void CheckIfCurrentPlatformIsWindows()
    {     
        if (Application.platform == RuntimePlatform.WindowsEditor || Application.platform == RuntimePlatform.WindowsPlayer)
        {
            isWindows = true;
        }
        else
        {
            isWindows = false;
        }
    }

    private void LoginOffline()
    {
        if(userInput.text == UsersManager.Instance.admin.GetUsername() && passwordInput.text == UsersManager.Instance.admin.GetPassword())
        {
            UsersManager.Instance.currentUser = new User(userInput.text, 10);
            LoadScreen();
        }
    }

    /// <summary>
    /// Check if the person authorazing the creation of a new user have the correct authorization acces level
    /// </summary>
    private IEnumerator CheckIfAdmin()
    {
        if ((adminUserInput.text == "" || adminUserInput.text == "Digite seu usuário") || (adminPasswordInput.text == "" || adminPasswordInput.text == "Digite sua senha"))
        {
            EventHandler.CallIsOneMessageOnlyEvent(true);
            EventHandler.CallOpenMessageEvent("Empty values");
            yield break;
        }

        WWWForm checkAccesssLevelForm = CreateForm.GetCheckAccessLevelForm(ConstStrings.LoginKey, adminUserInput.text, adminPasswordInput.text);

        UnityWebRequest createPostRequest = CreatePostRequest.GetPostRequest(checkAccesssLevelForm, ConstStrings.CheckAccessLevel, 0);

        yield return createPostRequest.SendWebRequest();
        if (HandlePostRequestResponse.HandleWebRequest(createPostRequest))
        {
            if (authorizationAccessLevel == 2 || authorizationAccessLevel > 4)
            {
                adminAuthorized = true;
                User userToAdd = new User(addNewUserInput.text, addNewPasswordInput.text);
                StartCoroutine(AddNewUser(userToAdd));
            }
            else
            {
                inputEnabled = true;
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
        if((userInput.text == "" || userInput.text == "Digite seu usuário") ||(passwordInput.text == "" || passwordInput.text == "Digite sua senha"))
        {
            EventHandler.CallIsOneMessageOnlyEvent(true);
            EventHandler.CallOpenMessageEvent("Empty values");
            yield break;
        }
        WWWForm loginUserInfo = new WWWForm();
        loginUserInfo.AddField("apppassword", "LoginUser");
        loginUserInfo.AddField("username", userInput.text);
        loginUserInfo.AddField("password", passwordInput.text);

        UnityWebRequest createPostRequest = CreatePostRequest.GetPostRequest(loginUserInfo, ConstStrings.CheckLoginUser, 0);
       
        MouseManager.Instance.SetWaitingCursor();
        inputEnabled = false;
        yield return createPostRequest.SendWebRequest();

        if(HandlePostRequestResponse.HandleWebRequest(createPostRequest))
        {
            UsersManager.Instance.currentUser = new User(userInput.text, authorizationAccessLevel);
            EventHandler.CallFillInternalDatabase();
            LoadScreen();
        }
       
        MouseManager.Instance.SetDefaultCursor();
        inputEnabled = true;
    }

    /// <summary>
    /// Checks on the online database if the user already exists
    /// </summary>
    private IEnumerator CheckIfUserAlreadyExists()
    {
        WWWForm newUserInfo = new WWWForm();
        newUserInfo.AddField("apppassword", "CheckIfUserExist");
        newUserInfo.AddField("username", addNewUserInput.text);

        UnityWebRequest createPostRequest = CreatePostRequest.GetPostRequest(newUserInfo, ConstStrings.CheckUserExist, 0);
        
        MouseManager.Instance.SetWaitingCursor();
        inputEnabled = false;
        yield return createPostRequest.SendWebRequest();
        
        if(HandlePostRequestResponse.HandleWebRequest(createPostRequest))
        {
            newUserPanel.style.display = DisplayStyle.None;
            adminAuthorizationPanel.style.display = DisplayStyle.Flex;
            adminAuthorizing = true;
        }
  
        MouseManager.Instance.SetDefaultCursor();
        inputEnabled = true;
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
        inputEnabled = false;
        yield return createPostRequest.SendWebRequest();
        if(HandlePostRequestResponse.HandleWebRequest(createPostRequest))
        {
            adminAuthorizationPanel.style.display = DisplayStyle.None;
            adminAuthorizing = false;
            loginPanel.style.display = DisplayStyle.Flex;
            openAddNewUserPanelButton.style.display = DisplayStyle.Flex;
            loginEnabled = true;
        }
       
        MouseManager.Instance.SetDefaultCursor();
        inputEnabled = true;
    }

    /// <summary>
    /// Loads the next screen. What will be available on this next screen is dependent if it is an admin loging in or not
    /// </summary>
    private void LoadScreen()
    {
        EventHandler.CallFillInternalDatabase();
        ChangeScreenManager.Instance.OpenScene(Scenes.MainMenu ,Scenes.InitialScene);
    }
   
    /// <summary>
    /// Show the panel to add a new user
    /// </summary>
    private void ShowAddNewUserPanel()
    {
        print("Hello");
        openAddNewUserPanelButton.style.display = DisplayStyle.None;
        loginPanel.style.display = DisplayStyle.None;
        newUserPanel.style.display = DisplayStyle.Flex;
        
        adminAuthorizing = false;
    }

    /// <summary>
    /// Opens screen to enter admin username and password to authorize the adition of the 
    /// new user if the user does not exist
    /// </summary>
    private void AdminAuthorizeAddNewUserClicked()
    {
        if((addNewUserInput.text == "" || addNewUserInput.text == "Digite o usuário a ser adicionado") ||(addNewPasswordInput.text == "" ||addNewPasswordInput.text == "Digite a senha do novo usuário"))
        {
            EventHandler.CallIsOneMessageOnlyEvent(true);
            EventHandler.CallOpenMessageEvent("Empty values");
            return;
        }
        else
        {
            loginEnabled = false;


            StartCoroutine(CheckIfUserAlreadyExists());
        }      
    } 

    /// <summary>
    /// show or hide the password
    /// </summary>
    private void ShowHidePassword()
    {      
        if (newUserPanel.style.display == DisplayStyle.Flex)
        {
            if (addNewPasswordInput.isPasswordField)
            {
                addNewPasswordInput.isPasswordField = false;
            }
            else
            {
                addNewPasswordInput.isPasswordField = true;
            }            
        }
        if (adminAuthorizationPanel.style.display == DisplayStyle.Flex)
        {
            if (adminPasswordInput.isPasswordField)
            {
                adminPasswordInput.isPasswordField = false;
            }
            else
            {
                adminPasswordInput.isPasswordField = true;
            }            
        }
        else
        {
            if (passwordInput.isPasswordField)
            {
                passwordInput.isPasswordField = false;
            }
            else
            {
                passwordInput.isPasswordField = true;
            }            
        }
    }

    /// <summary>
    /// Cancel the adition of a new user and returns to login panel
    /// </summary>
    private void CancelAddNewUser()
    {
        addNewPasswordInput.SetValueWithoutNotify("Digite a senha do novo usuário");
        addNewUserInput.SetValueWithoutNotify("Digite o usuário a ser adicionado");
        newUserPanel.style.display = DisplayStyle.None;
        loginPanel.style.display = DisplayStyle.Flex;
        openAddNewUserPanelButton.style.display = DisplayStyle.Flex;
        loginEnabled = true;
        adminAuthorizing = false;
        inputEnabled = true;
    }

    /// <summary>
    /// Close the admin authorization panel and returns to the add new user panel
    /// </summary>
    private void CloseAdminPanel()
    {
        adminUserInput.SetValueWithoutNotify("");
        adminPasswordInput.SetValueWithoutNotify("");
        adminAuthorizationPanel.style.display = DisplayStyle.None;
        adminAuthorizing = false;
        newUserPanel.style.display = DisplayStyle.Flex;
    }
}
