using System.Collections;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using UnityEngine.Networking;

public class MainMenuManager : MonoBehaviour
{
    #region Login
    [SerializeField] private GameObject loginPanel = null;
    [SerializeField] private TMP_InputField userInput;
    [SerializeField] private TMP_InputField passwordInput;
    #endregion
    #region Error
    [SerializeField] private GameObject errorPanel;
    [SerializeField] private TMP_Text errorText;
    #endregion
    #region NewUser
    [SerializeField] private GameObject newUserPanel;
    [SerializeField] private TMP_Text newUserMessage;
    [SerializeField] private TMP_InputField addNewUserInput;
    [SerializeField] private TMP_InputField addNewPasswordInput;
    [SerializeField] private Button openAddNewUserPanelButton;
    [SerializeField] private TMP_Text addNewUserButtonText;
    #endregion
    #region Admin
    [SerializeField] private GameObject adminAuthorizationPanel;
    [SerializeField] private TMP_InputField adminUserInput;
    [SerializeField] private TMP_InputField adminPasswordInput;
    #endregion

    [SerializeField] private GameObject usersManagerPrefab;
    private bool loginEnabled = true; // enable or disable the Enter key press to login
    private bool adminAuthorizing = false;
    private bool adminAuthorized = false;
    private bool inputEnabled = true;

    [SerializeField] private TMP_Text testingText;

    private void Start()
    {
        loginEnabled = true;
        adminAuthorizing = false;
        adminAuthorized = false;
    }

    private void Update()
    {
        if (inputEnabled)
        {
            if (Input.GetKeyDown(KeyCode.KeypadEnter) || Input.GetKeyDown(KeyCode.Return))
            {
                if (loginEnabled)
                {
                    StartCoroutine(Login());
                }
                if (adminAuthorizing)
                {
                    CheckAdminAuthorization();
                }
            }
        }
    }

    /// <summary>
    /// Check if admin login and password are correct to allow the creation of a new user
    /// </summary>
    private void CheckAdminAuthorization()
    {
        adminAuthorized = false;
        foreach (User user in UsersManager.Instance.usersDatabase)
        {
            if (adminUserInput.text == user.username && adminPasswordInput.text == user.password)
            {
                User userToAdd = new User(addNewUserInput.text, addNewPasswordInput.text);
                adminAuthorized = true;

                StartCoroutine(AddNewUser(userToAdd));

                break;
            }
        }
        if (!adminAuthorized)
        {
            SetErrorMessage(6);
        }
    }

    /// <summary>
    /// Login into the system. Different users have different access
    /// </summary>
    private IEnumerator Login()
    {

        WWWForm loginUserInfo = new WWWForm();
        loginUserInfo.AddField("apppassword", "LoginUser");
        loginUserInfo.AddField("username", userInput.text);
        loginUserInfo.AddField("password", passwordInput.text);

        UnityWebRequest createPostRequest = new UnityWebRequest();
        switch (InternalDatabase.Instance.currentEstoque)
        {
            case CurrentEstoque.SnPro:
                createPostRequest = UnityWebRequest.Post(ConstStrings.PhpRootFolder + "loginuser.php", loginUserInfo);
                break;
            case CurrentEstoque.Funsoft:
                createPostRequest = UnityWebRequest.Post(ConstStrings.PhpRootFolderFunsoft + "loginuser.php", loginUserInfo);
                break;
            case CurrentEstoque.ESF:
                createPostRequest = UnityWebRequest.Post(ConstStrings.PhpRootFolderESF + "loginuser.php", loginUserInfo);
                break;
            default:
                createPostRequest = UnityWebRequest.Post(ConstStrings.PhpRootFolder + "loginuser.php", loginUserInfo);
                break;
        }
        MouseManager.Instance.SetWaitingCursor();
        inputEnabled = false;
        yield return createPostRequest.SendWebRequest();

        if (createPostRequest.result == UnityWebRequest.Result.ConnectionError)
        {
            Debug.LogWarning("Login: conectionerror");
        }
        else if (createPostRequest.result == UnityWebRequest.Result.DataProcessingError)
        {
            Debug.LogWarning("Login: data processing error");
        }
        else if (createPostRequest.result == UnityWebRequest.Result.ProtocolError)
        {
            Debug.LogWarning("Login: protocol error");
        }

        if (createPostRequest.error == null)
        {
            string response = createPostRequest.downloadHandler.text;
            if (response == "Database connection error" || response == "username query ran into an error" || response == "playerinfo query failed" || response == "wrong appkey")
            {
                errorPanel.SetActive(true);
                StartCoroutine(ErrorPanelRoutine());
                SetErrorMessage(3);
            }
            else if (response == "Username does not exist or there is more than one in the table")
            {
                errorPanel.SetActive(true);
                StartCoroutine(ErrorPanelRoutine());
                SetErrorMessage(0);
            }
            else if (response == "password was not able to be verified")
            {
                errorPanel.SetActive(true);
                StartCoroutine(ErrorPanelRoutine());
                SetErrorMessage(5);
            }
            else
            {
                CheckIfAdminLogging();
                createPostRequest.Dispose();
                LoadScreen();
            }

        }
        else
        {
            errorPanel.SetActive(true);
            Debug.LogWarning(createPostRequest.error);
            errorText.text = createPostRequest.error;
            StartCoroutine(ErrorPanelRoutine());
        }
        createPostRequest.Dispose();
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

        UnityWebRequest createPostRequest = new UnityWebRequest();
        switch (InternalDatabase.Instance.currentEstoque)
        {
            case CurrentEstoque.SnPro:
                createPostRequest = UnityWebRequest.Post(ConstStrings.PhpRootFolder + "checkuserexist.php", newUserInfo);
                break;
            case CurrentEstoque.Funsoft:
                createPostRequest = UnityWebRequest.Post(ConstStrings.PhpRootFolderFunsoft + "checkuserexist.php", newUserInfo);
                break;
            case CurrentEstoque.ESF:
                createPostRequest = UnityWebRequest.Post(ConstStrings.PhpRootFolderESF + "checkuserexist.php", newUserInfo);
                break;
            default:
                createPostRequest = UnityWebRequest.Post(ConstStrings.PhpRootFolder + "checkuserexist.php", newUserInfo);
                break;
        }
        MouseManager.Instance.SetWaitingCursor();
        inputEnabled = false;
        yield return createPostRequest.SendWebRequest();

        if (createPostRequest.result == UnityWebRequest.Result.ConnectionError)
        {
            Debug.LogWarning("CheckIfUserAlreadyExists: conectionerror");
        }
        else if (createPostRequest.result == UnityWebRequest.Result.DataProcessingError)
        {
            Debug.LogWarning("CheckIfUserAlreadyExists: data processing error");
        }
        else if (createPostRequest.result == UnityWebRequest.Result.ProtocolError)
        {
            Debug.LogWarning("CheckIfUserAlreadyExists: protocol error");
        }

        if (createPostRequest.error == null)
        {
            string response = createPostRequest.downloadHandler.text;
            if (response == "Database connection error" || response == "username query ran into an error")
            {
                SetErrorMessage(3);
            }
            else if (response == "Username does not exist or there is more than one in the table")
            {
                newUserPanel.SetActive(false);
                adminAuthorizationPanel.SetActive(true);
                adminAuthorizing = true;
            }
            else if (response == "Username already exist")
            {
                SetErrorMessage(1);
            }
            else
            {
                Debug.Log(response);
            }
        }
        else
        {
            errorPanel.SetActive(true);
            Debug.LogWarning(createPostRequest.error);
            errorText.text = createPostRequest.error;
            StartCoroutine(ErrorPanelRoutine());
        }
        createPostRequest.Dispose();
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
        newUserInfo.AddField("username", userToAdd.username);
        newUserInfo.AddField("password", userToAdd.password);

        UnityWebRequest createPostRequest = new UnityWebRequest();
        switch (InternalDatabase.Instance.currentEstoque)
        {
            case CurrentEstoque.SnPro:
                createPostRequest = UnityWebRequest.Post(ConstStrings.PhpRootFolder + "newuser.php", newUserInfo);
                break;
            case CurrentEstoque.Funsoft:
                createPostRequest = UnityWebRequest.Post(ConstStrings.PhpRootFolderFunsoft + "newuser.php", newUserInfo);
                break;
            case CurrentEstoque.ESF:
                createPostRequest = UnityWebRequest.Post(ConstStrings.PhpRootFolderESF + "newuser.php", newUserInfo);
                break;
            default:
                createPostRequest = UnityWebRequest.Post(ConstStrings.PhpRootFolder + "newuser.php", newUserInfo);
                break;
        }
        MouseManager.Instance.SetWaitingCursor();
        inputEnabled = false;
        yield return createPostRequest.SendWebRequest();

        if (createPostRequest.result == UnityWebRequest.Result.ConnectionError)
        {
            Debug.LogWarning("AddNewUser: conectionerror");
        }
        else if (createPostRequest.result == UnityWebRequest.Result.DataProcessingError)
        {
            Debug.LogWarning("AddNewUser: data processing error");
        }
        else if (createPostRequest.result == UnityWebRequest.Result.ProtocolError)
        {
            Debug.LogWarning("AddNewUser: protocol error");
        }

        if (createPostRequest.error == null)
        {
            string response = createPostRequest.downloadHandler.text;
            if (response == "Database connection error" || response == "username query ran into an error" || response == "insert user failed")
            {
                SetErrorMessage(3);

            }
            else if (response == "Username already exists")
            {
                SetErrorMessage(1);

            }
            else if (response == "wrong appkey")
            {
                SetErrorMessage(4);
            }
            else if (response == "User added")
            {
                Debug.Log(response);
                SetErrorMessage(2);
                newUserPanel.SetActive(false);
                adminAuthorizing = false;
                loginPanel.SetActive(true);
                loginEnabled = true;
            }
        }
        else
        {
            errorPanel.SetActive(true);
            Debug.LogWarning(createPostRequest.error);
            errorText.text = createPostRequest.error;
            StartCoroutine(ErrorPanelRoutine());
        }
        createPostRequest.Dispose();
        MouseManager.Instance.SetDefaultCursor();
        inputEnabled = true;
    }

    /// <summary>
    /// Check to see if it is an admin logging to enable more features
    /// </summary>
    private void CheckIfAdminLogging()
    {
        UsersManager.Instance.adminLogged = false;
        foreach (User user in UsersManager.Instance.usersDatabase)
        {
            if (userInput.text == user.username)
            {
                UsersManager.Instance.adminLogged = true;
                break;
            }
        }
        UsersManager.Instance.currentUser.username = userInput.text;
    }

    /// <summary>
    /// Loads the next screen. What will be available on this next screen is dependent if it is an admin loging in or not
    /// </summary>
    private void LoadScreen()
    {
        SceneManager.LoadScene(ConstStrings.SceneInitial);
    }

    /// <summary>
    /// 0 = Username/Password error, 1 = Username already exists, 2 = New user added
    /// </summary>
    private void SetErrorMessage(int errorID)
    {
        errorPanel.SetActive(true);
        if (errorText.isActiveAndEnabled)
        {
            switch (errorID)
            {
                case 0:
                    errorText.text = "Usuário incorreto. Tente novamente.";
                    break;
                case 1:
                    errorText.text = "Usuário já existe. Tente adicionar outro usuário";
                    break;
                case 2:
                    errorText.text = "Usuário cadastrado com sucesso.";
                    break;
                case 3:
                    errorText.text = "Erro no acesso ao banco de dados";
                    break;
                case 4:
                    errorText.text = "Erro na autorização do aplicativo";
                    break;
                case 5:
                    errorText.text = "Senha incorreta. Tente novamente.";
                    break;
                case 6:
                    errorText.text = "Login e/ou senha do admin não reconhecido.";
                    break;
                case 7:
                    errorText.text = "Login e/ou senha incorretos. Tente novamente";
                    break;
                default:
                    break;
            }
        }
        StartCoroutine(ErrorPanelRoutine());
    }

    /// <summary>
    /// After 5 seconds close the error panel and enables Enter input
    /// </summary>
    private IEnumerator ErrorPanelRoutine()
    {
        yield return new WaitForSeconds(10);
        loginEnabled = true;
        CloseErrorPanel();
    }

    /// <summary>
    /// Closes the ErrorPanel
    /// </summary>
    public void CloseErrorPanel()
    {
        StopAllCoroutines();
        errorPanel.SetActive(false);
        if (adminAuthorizing)
        {
          //  adminAuthorizationPanel.SetActive(false);
            adminAuthorizing = false;
        }
        if (adminAuthorized)
        {
           // newUserPanel.SetActive(false);
            adminAuthorizationPanel.SetActive(false);
        }
        openAddNewUserPanelButton.enabled = true;
        openAddNewUserPanelButton.interactable = true;
    }

    /// <summary>
    /// Show the panel to add a new user
    /// </summary>
    public void ShowAddNewUserPanel()
    {
        openAddNewUserPanelButton.interactable = false;
        openAddNewUserPanelButton.enabled = false;
        newUserPanel.SetActive(true);
        loginPanel.SetActive(false);
        newUserMessage.text = "Digite o novo usuário e senha, e aperte no botão abaixo para adicionar novo usuário.";
        addNewUserButtonText.text = "Adicionar novo usuário";
        adminAuthorizing = false;
    }

    /// <summary>
    /// Opens screen to enter admin username and password to authorize the adition of the 
    /// new user if the user does not exist
    /// </summary>
    public void AddNewUserClicked()
    {
        loginEnabled = false;

        StartCoroutine(CheckIfUserAlreadyExists());
    } 

    /// <summary>
    /// Called by the OK button inside the error panel to close the panel and enable enter input
    /// </summary>
    public void SetInputEnabled(bool isEnabled)
    {
        errorPanel.SetActive(false);
        loginEnabled = isEnabled;
        inputEnabled = isEnabled;
    }

    /// <summary>
    /// show or hide the password
    /// </summary>
    public void ShowHidePassword()
    {
        if (newUserPanel.activeInHierarchy)
        {
            if (addNewPasswordInput.contentType == TMP_InputField.ContentType.Password)
            {
                addNewPasswordInput.contentType = TMP_InputField.ContentType.Standard;
            }
            else
            {
                addNewPasswordInput.contentType = TMP_InputField.ContentType.Password;
            }
            addNewPasswordInput.ForceLabelUpdate();
        }
        if (adminAuthorizationPanel.activeInHierarchy)
        {
            if (adminPasswordInput.contentType == TMP_InputField.ContentType.Password)
            {
                adminPasswordInput.contentType = TMP_InputField.ContentType.Standard;
            }
            else
            {
                adminPasswordInput.contentType = TMP_InputField.ContentType.Password;
            }
            adminPasswordInput.ForceLabelUpdate();
        }
        if (!adminAuthorizing && !adminAuthorized && !newUserPanel.activeInHierarchy)
        {
            if (passwordInput.contentType == TMP_InputField.ContentType.Password)
            {
                passwordInput.contentType = TMP_InputField.ContentType.Standard;
            }
            else
            {
                passwordInput.contentType = TMP_InputField.ContentType.Password;
            }
            passwordInput.ForceLabelUpdate();
        }
    }

    /// <summary>
    /// Cancel the adition of a new user and returns to login panel
    /// </summary>
    public void CancelAddNewUser()
    {
        addNewPasswordInput.text = "";
        addNewUserInput.text = "";
        newUserPanel.SetActive(false);
        loginPanel.SetActive(true);
        loginEnabled = true;
        adminAuthorizing = false;
        inputEnabled = true;
    }

    /// <summary>
    /// CLose the admin authorization panel and returns to the add new user panel
    /// </summary>
    public void CloseAdminPanel()
    {
        adminUserInput.text = "";
        adminPasswordInput.text = "";
        adminAuthorizationPanel.SetActive(false);
        adminAuthorizing = false;
        newUserPanel.SetActive(true);
    }
}
