using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Saving;

public class UsersManager : Singleton<UsersManager>, ISaveable
{
    public List<User> usersDatabase;
    private User admin;
    public bool adminLogged = false;
    private SavingWrapper savingWrapper = null;
    public User currentUser = new User("pessoa","");

    protected  override void Awake()
    {
        base.Awake();
        usersDatabase = new List<User>();
        admin = new User("1", "1");
        if (!usersDatabase.Contains(admin))
        {
            usersDatabase.Add(admin);
        }
    }

    private void Start()
    {
        DontDestroyOnLoad(this.gameObject);
        savingWrapper = FindObjectOfType<SavingWrapper>();        
    }

    /// <summary>
    /// Add a new user to the UsersDatabase and save it
    /// </summary>
    public void  AddNewUser(User userToAdd)
    {
        usersDatabase.Add(userToAdd);
        if(savingWrapper == null)
        {
            savingWrapper = FindObjectOfType<SavingWrapper>();
            savingWrapper.Save(ConstStrings.UserDatabaseSaveFile);
        }
        else
        {
            savingWrapper.Save(ConstStrings.UserDatabaseSaveFile);
        }
    }

    public object CaptureState()
    {
        return usersDatabase;
    }

    public void RestoreState(object state)
    {
        usersDatabase = (List<User>)state;
    }
}
