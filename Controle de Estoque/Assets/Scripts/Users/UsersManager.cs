using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Saving;

public class UsersManager : Singleton<UsersManager>, ISaveable
{
    public List<User> usersDatabase;
    private User admin;
    public User Admin { get { return admin; } }
    public bool adminLogged = false;
    private SavingWrapper savingWrapper = null;

    protected void Awake()
    {
        base.Awake();
        usersDatabase = new List<User>();
        admin = new User("marcelo.fonseca", "Umsegredo1");
        usersDatabase.Add(admin);
    }

    private void Start()
    {
        savingWrapper = FindObjectOfType<SavingWrapper>();
        DontDestroyOnLoad(this.gameObject);
    }

    public void  AddNewUser(User userToAdd)
    {
        usersDatabase.Add(userToAdd);
        if(savingWrapper == null)
        {
            savingWrapper = FindObjectOfType<SavingWrapper>();
            AddNewUser(userToAdd);
        }
        else
        {
            savingWrapper.Save();
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
