using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Saving;

public class UsersManager : Singleton<UsersManager>
{
    public List<User> usersDatabase;
    private User admin;
    private User admin1;
    private User admin2;
    public bool adminLogged = false;
    public User currentUser = new User("pessoa","");

    protected  override void Awake()
    {
        base.Awake();
        usersDatabase = new List<User>();
        admin = new User("pedro.neto", "Sysnetpr0");
        if (!usersDatabase.Contains(admin))
        {
            usersDatabase.Add(admin);
        }
        admin1 = new User("marcelo.fonseca", "Umsegredo1");
        if (!usersDatabase.Contains(admin1))
        {
            usersDatabase.Add(admin1);
        }
        admin1 = new User("admin", "admin");
        if (!usersDatabase.Contains(admin1))
        {
            usersDatabase.Add(admin1);
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
