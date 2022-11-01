using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using Saving;
using System;
using UnityEngine.UI;

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
    private bool inputEnabled = true; // enable or disable the Enter key press to login
    private bool adminAuthorizing = false;
    private bool adminAuthorized = false;

    private void Start()
    {
        CheckIfUserDatabaseExists();
    }

    private void Update()
    {
        if(Input.GetKeyDown(KeyCode.KeypadEnter) || Input.GetKeyDown(KeyCode.Return))
        {
            CheckLogin();
        }
       
    }

    /// <summary>
    /// Safety check to see if there is a UsersManager instance and if the usersDatabase is null
    /// </summary>
    private void CheckIfUserDatabaseExists()
    {
        if(UsersManager.Instance == null)
        {
            Instantiate(usersManagerPrefab);
        }
        else
        {
            if(UsersManager.Instance.usersDatabase == null)
            {
                UsersManager.Instance.usersDatabase = new List<User>();
                SavingSystem saving = FindObjectOfType<SavingSystem>();
                saving.Load(ConstStrings.UserDatabaseSaveFile);
            }
        }
    }

    /// <summary>
    /// Login into the system. Different users have different access
    /// </summary>
    private void Login(string user)
    {
        if (user == "marcelo.fonseca" || user == "pedro.neto")
        {
            UsersManager.Instance.adminLogged = true;
        }
        else
        {
            UsersManager.Instance.adminLogged = false;
        }
        LoadScreen();
    }

    /// <summary>
    /// Loads the next screen. What will be available on this next screen is dependent if it is an admin loging in or not
    /// </summary>
    private void LoadScreen()
    {
        print("Load screen");
    }

    /// <summary>
    /// After 5 seconds close the error panel and enables Enter input
    /// </summary>
    private IEnumerator ErrorPanelRoutine()
    {
        yield return new WaitForSeconds(10);
        inputEnabled = true;
        CloseErrorPanel();
        
    }
    /// <summary>
    /// 0 = Username/Password error, 1 = Username already exists, 2 = New user added
    /// </summary>
    private void SetErrorMessage(int errorID)
    {
        if(errorText.isActiveAndEnabled)
        {
            switch (errorID)
            {
                case 0:
                    errorText.text = "Usuário e/ou senha incorretos. Tente novamente.";
                    break;
                case 1:
                    errorText.text = "Usuário já existe. Tente adicionar outro usuário";
                    break;
                case 2:
                    errorText.text = "Usuário cadastrado com sucesso.";
                    break;
                default:
                    break;
            }
        }
    }

    public void CloseErrorPanel()
    {
        StopAllCoroutines();
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
        bool userFound = false;
        adminAuthorizing = true;
        inputEnabled = false;
        foreach (User user in UsersManager.Instance.usersDatabase)
        {
            if(user.username == addNewUserInput.text)
            {
                inputEnabled = false;
                errorPanel.SetActive(true);
                SetErrorMessage(1);
                StartCoroutine(ErrorPanelRoutine());
                userFound = true;
                break;
            }
        }
        if (!userFound)
        {
            adminAuthorizationPanel.SetActive(true);
        }
        
    }

    /// <summary>
    /// Adds a new user to the user database
    /// </summary>
    public void AddNewUser(User userToAdd)
    {
        bool userFound = false;
        foreach (User user in UsersManager.Instance.usersDatabase)
        {
            if (user.username == userToAdd.username && user.password == userToAdd.password)
            {
                inputEnabled = false;
                errorPanel.SetActive(true);
                SetErrorMessage(1);
                StartCoroutine(ErrorPanelRoutine());
                userFound = true;
                break;
            }
        }
         if(!userFound)
        {
            UsersManager.Instance.AddNewUser(userToAdd);
            inputEnabled = false;
            errorPanel.SetActive(true);
            SetErrorMessage(2);
            StartCoroutine(ErrorPanelRoutine());
        }
    }

    /// <summary>
    /// Checks if the username and password exist on the database and are typed correctly
    /// </summary>
    public void CheckLogin()
    {
        if (!adminAuthorizing)
        {
            User userToCheck = new User(userInput.text, passwordInput.text);
            bool userFound = false;
            foreach (User user in UsersManager.Instance.usersDatabase)
            {
                if(user.username == userToCheck.username && user.password == userToCheck.password)
                {
                    Login(userToCheck.username);
                    userFound = true;
                    break;
                }
            }               
            if(!userFound)
            {
                inputEnabled = false;
                errorPanel.SetActive(true);
                SetErrorMessage(0);
                StartCoroutine(ErrorPanelRoutine());
            }
        }
        else
        {
            User userToCheck = new User(adminUserInput.text, adminPasswordInput.text);
            User userToAdd = new User(addNewUserInput.text, addNewPasswordInput.text);
            bool userFound = false;
            foreach (User user in UsersManager.Instance.usersDatabase)
            {
                if (user.username == userToCheck.username && user.password == userToCheck.password)
                {
                    AddNewUser(userToAdd);
                    adminAuthorizing = false;
                    adminAuthorized = true;
                    errorPanel.SetActive(true);
                    SetErrorMessage(2);
                    StartCoroutine(ErrorPanelRoutine());
                    userFound = true;
                    break;
                }
            }
            if(!userFound)
            {
                inputEnabled = false;
                errorPanel.SetActive(true);
                SetErrorMessage(0);
                StartCoroutine(ErrorPanelRoutine());
            }
        }
    }

    /// <summary>
    /// Called by the OK button inside the error panel to close the panel and enable enter input
    /// </summary>
    public void SetInputEnabled(bool isEnabled)
    {
        errorPanel.SetActive(false);
        inputEnabled = isEnabled;
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
