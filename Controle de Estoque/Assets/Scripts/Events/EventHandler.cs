using System;
using System.Collections.Generic;


public static class EventHandler
{
    public static event Action<string> DatabaseUpdatedEvent;

    public static void CallDatabaseUpdatedEvent(string saveName)
    {
        if (DatabaseUpdatedEvent != null)
        {
            DatabaseUpdatedEvent(saveName);
        }
    }
}

