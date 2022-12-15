using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using Saving;
using System;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using UnityEngine.Networking;
using System.Net;

public class MainMenuManager : MonoBehaviour
{
    #region Login
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

    [SerializeField] private TMP_Text testingText;

    private void Start()
    {
        loginEnabled = true;
        adminAuthorizing = false;
        adminAuthorized = false;
    }

    private void Update()
    {
        if(Input.GetKeyDown(KeyCode.KeypadEnter) || Input.GetKeyDown(KeyCode.Return))
        {
           if(loginEnabled)
            {
                StartCoroutine(Login());
            }
           if(adminAuthorizing)
            {
                CheckAdminAuthorization();
            }         
        }
    }

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
              if(!adminAuthorized)
        {
            SetErrorMessage(6);
        }
    }

    /// <summary>
    /// Login into the system. Different users have different access
    /// </summary>
    private IEnumerator Login()
    {
        InternalDatabase.Instance.FillFullDatabase();
        WWWForm loginUserInfo = new WWWForm();
        loginUserInfo.AddField("apppassword", "LoginUser");
        loginUserInfo.AddField("username", userInput.text);
        loginUserInfo.AddField("password", passwordInput.text);

        UnityWebRequest createPostRequest = UnityWebRequest.Post("http://localhost/controledeestoque/loginuser.php", loginUserInfo);
         yield return createPostRequest.SendWebRequest();

        if (createPostRequest.result == UnityWebRequest.Result.ConnectionError)
        {
            Debug.LogWarning("conectionerror");
        }
        else if (createPostRequest.result == UnityWebRequest.Result.DataProcessingError)
        {
            Debug.LogWarning("data processing error");
        }
        else if (createPostRequest.result == UnityWebRequest.Result.ProtocolError)
        {
            Debug.LogWarning("protocol error");
        }

        if (createPostRequest.error == null)
        {
           
                        string response = createPostRequest.downloadHandler.text;
            if (response == "1" || response == "2" || response == "5")
            {
                errorPanel.SetActive(true);
                StartCoroutine(ErrorPanelRoutine());
                SetErrorMessage(3);
            }
            else if (response == "3")
            {
                errorPanel.SetActive(true);
                StartCoroutine(ErrorPanelRoutine());
                SetErrorMessage(0);
            }
            else if (response == "4")
            {
                errorPanel.SetActive(true);
                StartCoroutine(ErrorPanelRoutine());
                SetErrorMessage(5);
            }
            else
            {
                CheckIfAdminLogging();
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
        }

    private void CheckIfAdminLogging()
    {
        UsersManager.Instance.adminLogged = false;
        foreach (User user in UsersManager.Instance.usersDatabase)
        {
            if(userInput.text == user.username)
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
    /// After 5 seconds close the error panel and enables Enter input
    /// </summary>
    private IEnumerator ErrorPanelRoutine()
    {
        yield return new WaitForSeconds(10);
        loginEnabled = true;
        CloseErrorPanel();
        
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
                    errorText.text = "Erro na appkey";
                    break;
                case 5:
                    errorText.text = "Senha incorreta. Tente novamente.";
                    break;
                case 6:
                    
                    errorText.text = "Login e/ou senha do admin não reconhecido.";
                    break;
                default:
                    break;
            }
        }
        StartCoroutine(ErrorPanelRoutine());
    }

    /// <summary>
    /// Closes the ErrorPanel
    /// </summary>
    public void CloseErrorPanel()
    {
        StopCoroutine(ErrorPanelRoutine()); 
        errorPanel.SetActive(false);
        if(adminAuthorizing)
        {            
            adminAuthorizationPanel.SetActive(false);
            adminAuthorizing = false;
        }
        if (adminAuthorized)
        {
            newUserPanel.SetActive(false);
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

    private IEnumerator CheckIfUserAlreadyExists()
    {
        WWWForm newUserInfo = new WWWForm();
        newUserInfo.AddField("apppassword", "CheckIfUserExist");
        newUserInfo.AddField("username", addNewUserInput.text);
       
        UnityWebRequest createPostRequest = UnityWebRequest.Post("http://localhost/controledeestoque/checkuserexist.php", newUserInfo);
        yield return createPostRequest.SendWebRequest();

        if (createPostRequest.result == UnityWebRequest.Result.ConnectionError)
        {
            Debug.LogWarning("conectionerror");
        }
        else if (createPostRequest.result == UnityWebRequest.Result.DataProcessingError)
        {
            Debug.LogWarning("data processing error");
        }
        else if (createPostRequest.result == UnityWebRequest.Result.ProtocolError)
        {
            Debug.LogWarning("protocol error");
        }

        if (createPostRequest.error == null)
        {
                       string response = createPostRequest.downloadHandler.text;
            if (response == "1" || response == "2")
            {
                SetErrorMessage(3);
            }
            else if (response == "3")
            {
                adminAuthorizationPanel.SetActive(true);
                adminAuthorizing = true;
            }
            else if (response == "4")
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
    }

    /// <summary>
    /// Adds a new user to the user database
    /// </summary>
    private IEnumerator AddNewUser(User userToAdd)
    {
        WWWForm newUserInfo = new WWWForm();
        newUserInfo.AddField("apppassword", "InsertNewUser");
        newUserInfo.AddField("username", userToAdd.username);
        newUserInfo.AddField("password", userToAdd.password);
    
       UnityWebRequest createPostRequest = UnityWebRequest.Post("http://localhost/controledeestoque/newuser.php", newUserInfo);
               yield return createPostRequest.SendWebRequest();

        if (createPostRequest.result == UnityWebRequest.Result.ConnectionError)
        {
            Debug.LogWarning("conectionerror");
        }
        else if (createPostRequest.result == UnityWebRequest.Result.DataProcessingError)
        {
            Debug.LogWarning("data processing error");
        }
        else if (createPostRequest.result == UnityWebRequest.Result.ProtocolError)
        {
            Debug.LogWarning("protocol error");
        }
       
        if (createPostRequest.error == null)
        {
           
                       string response = createPostRequest.downloadHandler.text;
            if (response == "1" || response == "2" || response == "4")
            {
                SetErrorMessage(3);
                
            }
            else if (response == "3")
            {
                SetErrorMessage(1);
                
            }
            else if (response == "5")
            {
                SetErrorMessage(4);
            }
            else
            {
                Debug.Log(response);
                SetErrorMessage(2);
                newUserPanel.SetActive(false);
                adminAuthorizing = false;
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
    }

    /// <summary>
    /// Called by the OK button inside the error panel to close the panel and enable enter input
    /// </summary>
    public void SetInputEnabled(bool isEnabled)
    {
        errorPanel.SetActive(false);
        loginEnabled = isEnabled;
    }

    /// <summary>
    /// show or hide the password
    /// </summary>
    public void ShowHidePassword(bool showPassword)
    {
        if(newUserPanel.activeInHierarchy)
        {
            if(addNewPasswordInput.contentType == TMP_InputField.ContentType.Password)
            {
                addNewPasswordInput.contentType = TMP_InputField.ContentType.Standard;
            }
            else
            {
                addNewPasswordInput.contentType = TMP_InputField.ContentType.Password;
            }
            addNewPasswordInput.ForceLabelUpdate();
        }
        if(adminAuthorizationPanel.activeInHierarchy)
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
        if (!adminAuthorizing && !adminAuthorized &&!newUserPanel.activeInHierarchy)
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
}
