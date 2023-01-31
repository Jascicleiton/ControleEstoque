using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class MessageManager : MonoBehaviour
{
    [SerializeField] TMP_Text messageText;
    [SerializeField] GameObject messagePanel;

    private string message1 = "";
    private string message2 = "";
    private bool inputEnabled = false;
    private bool isOnlyOneMessage = false;

    private void Update()
    {
        if (inputEnabled)
        {
            
            if ((Input.GetKeyDown(KeyCode.KeypadEnter) || Input.GetKeyDown(KeyCode.Return)) && messagePanel.activeInHierarchy)
            {
                CloseMessage();
            }
        }
    }

    private void OnEnable()
    {
        EventHandler.OpenMessageEvent += MessageReceived;
        EventHandler.EnableInput += SetInputEnabled;
        EventHandler.IsOneMessageOnlyEvent += SetIsOneMessageOnly;
    }

    private void OnDisable()
    {
        EventHandler.OpenMessageEvent -= MessageReceived;
        EventHandler.EnableInput -= SetInputEnabled;
        EventHandler.IsOneMessageOnlyEvent -= SetIsOneMessageOnly;
    }

    private void MessageReceived(string message)
    {
        if (isOnlyOneMessage)
        {
            message1 = message;
            OpenMessage();
        }
        else
        {
            if (message1 == "")
            {
                message1 = message;
            }
            else
            {
                message2 = message;
                OpenMessage();
            }
        }   
    }

    private void SetIsOneMessageOnly(bool value)
    {
        isOnlyOneMessage = value;
    }

    private void SetInputEnabled(bool inputEnabled)
    {
        
        this.inputEnabled = !inputEnabled;
    }

    private void OpenMessage()
    {
        MouseManager.Instance.SetDefaultCursor();
        EventHandler.CallEnableInput(false);
        inputEnabled = true;
        messagePanel.SetActive(true);
        if((message1 == "Worked" && message2 == "Worked") || (message1 == "Updated" && message2 == "Updated"))
        {
            if(message1 == "Worked")
            {
                messageText.text = "Item adicionado com sucesso.";
            }
            else if(message1 == "Updated")
            {
                messageText.text = "Item atualizado com sucesso.";
            }
            
            //full success
        }
        else if ((message1 == "Worked" && message2 != "Worked") || (message1 == "Updated" && (message2 != "Updated" ||message2 != "Worked")))
        {
            if (message1 == "Worked")
            {
                messageText.text = "Item adicionado no inventário com sucesso.\n" + message2;
            }
            else if(message1 == "Updated")
            {
                messageText.text = "Item atualizado no inventário com sucesso.\n" + message2;
            }
        }
        else 
        {
            messageText.text = message1 + "\nContate o administrador sobre este erro";
        }
        StartCoroutine(CloseMessageRoutine());
        message1 = "";
        message2 = "";
        inputEnabled = true;
    }

    public void CloseMessage()
    {
        messageText.text = "";
        message1 = "";
        message2 = "";
        messagePanel.SetActive(false);
        StopAllCoroutines();
        
        EventHandler.CallMessageClosed();
        //EventHandler.CallEnableInput(true);
    }

    private IEnumerator CloseMessageRoutine()
    {
        yield return new WaitForSecondsRealtime(10f);
        CloseMessage();
    }

}
