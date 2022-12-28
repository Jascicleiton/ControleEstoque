using Saving;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenuOffline : MonoBehaviour
{
    [SerializeField] GameObject usersManagerPrefab = null;

    public void CheckIfUserDatabaseExists()
    {
        if (UsersManager.Instance == null)
        {
            Instantiate(usersManagerPrefab);
        }
        else
        {
            if (UsersManager.Instance.usersDatabase == null)
            {
                UsersManager.Instance.usersDatabase = new List<User>();
                SavingSystem saving = FindObjectOfType<SavingSystem>();
                saving.Load(ConstStrings.UserDatabaseSaveFile);
            }
        }
    }

    public void Login(string user)
    {
        UsersManager.Instance.currentUser.username = user;
        if (user == "marcelo.fonseca" || user == "pedro.neto")
        {
            UsersManager.Instance.adminLogged = true;
        }
        else
        {
            UsersManager.Instance.adminLogged = false;
        }
        SceneManager.LoadScene(ConstStrings.SceneInitial);
    }

    public bool CheckLogin(string username, string password)
    {

        User userToCheck = new User(username, password);
        bool userFound = false;
        foreach (User user in UsersManager.Instance.usersDatabase)
        {
            if (user.username == userToCheck.username && user.password == userToCheck.password)
            {
                userFound = true;
                Login(userToCheck.username);
                break;
            }
        }
        return userFound;


    }
}