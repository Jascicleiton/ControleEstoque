using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MessagesManager : Singleton<MessagesManager>
{
    // Start is called before the first frame update
    void Start()
    {
        DontDestroyOnLoad(this);
    }

    public static void ShowMessage(string message)
    {

    }
}
