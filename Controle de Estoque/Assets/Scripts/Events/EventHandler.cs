using System;
using System.Collections.Generic;
using Unity.VisualScripting;

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

    public static event Action<bool> IsOneMessageOnlyEvent;

    public static void CallIsOneMessageOnlyEvent(bool isOneMessageOnly)
    {
        if(IsOneMessageOnlyEvent != null)
        {
            IsOneMessageOnlyEvent(isOneMessageOnly);
        }
    }

    public static event Action<bool> EnableInput;

    public static void CallEnableInput(bool inputEnabled)
    {
        if(EnableInput != null)
        {
            EnableInput(inputEnabled);
        }
    }

    public static event Action MessageClosed;

    public static void CallMessageClosed()
    {
        if(MessageClosed != null)
        {
            MessageClosed();
        }
    }

    public static event Action<bool> ImportFinished;

    public static void CallImportFinished(bool isInventory)
    {
        if(ImportFinished != null)
        {
            ImportFinished(isInventory);
        }
    }

    public static event Action UpdateTabInputs;

    public static void CallUpdateTabInputs()
    {
        if(UpdateTabInputs != null)
        {
            UpdateTabInputs();
        }
    }
}

