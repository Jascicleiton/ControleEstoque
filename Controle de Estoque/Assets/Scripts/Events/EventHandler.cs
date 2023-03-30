using System;
using System.Collections.Generic;
using Unity.VisualScripting;

public static class EventHandler
{
    /// <summary>
    /// Event used to save the database
    /// </summary>
    public static event Action DatabaseUpdatedEvent;
        public static void CallDatabaseUpdatedEvent()
    {
        if (DatabaseUpdatedEvent != null)
        {
            DatabaseUpdatedEvent();
        }
    }

    /// <summary>
    /// Event used to open the message panel
    /// </summary>
    public static event Action<string> OpenMessageEvent;
        public static void CallOpenMessageEvent(string message)
    {
        if(OpenMessageEvent != null)
        {
            OpenMessageEvent(message);
        }
    }

    /// <summary>
    /// Event used to tell the message panel if it is going to receive one or two strings before showing the message
    /// </summary>
    public static event Action<bool> IsOneMessageOnlyEvent;
        public static void CallIsOneMessageOnlyEvent(bool isOneMessageOnly)
    {
        if (IsOneMessageOnlyEvent != null)
        {
            IsOneMessageOnlyEvent(isOneMessageOnly);
        }
    }


    /// <summary>
    /// Called to enable or disable input
    /// </summary>
    public static event Action<bool> EnableInput;
        public static void CallEnableInput(bool inputEnabled)
    {
        if(EnableInput != null)
        {
            EnableInput(inputEnabled);
        }
    }

    /// <summary>
    /// Used to tell all inscribed classses that the message panel was closed
    /// </summary>
    public static event Action MessageClosed;
        public static void CallMessageClosed()
    {
        if(MessageClosed != null)
        {
            MessageClosed();
        }
    }

    /// <summary>
    /// Used to tell the ImportingWidgetController each time a sheet is imported, so it can update it's status
    /// </summary>
    public static event Action<bool> ImportFinished;
        public static void CallImportFinished(bool isInventory)
    {
        if(ImportFinished != null)
        {
            ImportFinished(isInventory);
        }
    }

    /// <summary>
    /// Used by TabInputHandler to know when to get the active inputs
    /// </summary>
    public static event Action UpdateTabInputs;
        public static void CallUpdateTabInputs()
    {
        if(UpdateTabInputs != null)
        {
            UpdateTabInputs();
        }
    }

    /// <summary>
    /// Used to let all classes know when an window is closed (not message window)
    /// </summary>
    public static event Action WindowClosed;
        public static void CallWindowClosed()
    {
        if(WindowClosed != null)
        {
            WindowClosed();
        }
    }

    public static event Action<string> PostRequestResponse;
        public static void CallPostRequestResponse(string response)
    {
        if(PostRequestResponse != null)
        {
            PostRequestResponse(response);
        }
    }

    /// <summary>
    /// Used to load the database from a local save when there is a connection problem with the SQL database
    /// </summary>
    public static event Action DisconectedFromInternet;
    public static void CallDisconectedFromInternet()
    {
        if(DisconectedFromInternet != null)
        {
            DisconectedFromInternet();
        }
    }

}

