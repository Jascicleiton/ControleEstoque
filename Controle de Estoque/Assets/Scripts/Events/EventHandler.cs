using System;
using System.Collections.Generic;


public static class EventHandler
{
    /// <summary>
    /// Event used to save the database
    /// </summary>
    public static event Action<string> DatabaseUpdatedEvent;

    public static void CallDatabaseUpdatedEvent(string saveName)
    {
        if (DatabaseUpdatedEvent != null)
        {
            DatabaseUpdatedEvent(saveName);
        }
    }

    public static event Action<string> OpenMessageEvent;

    public static void CallOpenMessageEvent(string message)
    {
        if(OpenMessageEvent != null)
        {
            OpenMessageEvent(message);
        }
    }
}

