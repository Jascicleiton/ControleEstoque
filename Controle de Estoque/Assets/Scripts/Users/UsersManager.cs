using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Saving;

public class UsersManager : Singleton<UsersManager>
{
    public List<User> usersDatabase;
    public User admin;

    public bool adminLogged = false;
    public User currentUser = new User("pessoa","");

    protected  override void Awake()
    {
        base.Awake();
        usersDatabase = new List<User>();
        admin = new User("admin", "admin");
        if (!usersDatabase.Contains(admin))
        {
            usersDatabase.Add(admin);
        }
    }

    private void Start()
    {
        DontDestroyOnLoad(this.gameObject);
    }

    /// <summary>
    /// Add a new user to the UsersDatabase
    /// </summary>
    public void  AddNewUser(User userToAdd)
    {
        usersDatabase.Add(userToAdd);
    }
}
