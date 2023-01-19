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
    }

    private void OnDisable()
    {
        EventHandler.OpenMessageEvent -= MessageReceived;
        EventHandler.EnableInput -= SetInputEnabled;
    }

    private void MessageReceived(string message)
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
        if(message2 == "")
        {
            OpenMessage();
        }
        
    }

    private void SetInputEnabled(bool inputEnabled)
    {
        this.inputEnabled = !inputEnabled;
    }

    private void OpenMessage()
    {
        EventHandler.CallEnableInput(false);
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
        else if ((message1 == "Worked" && message2 != "Worked") || (message1 == "Updated" && message2 != "Updated"))
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
    }

    public void CloseMessage()
    {
        messageText.text = "";
        message1 = "";
        message2 = "";
        messagePanel.SetActive(false);
        StopAllCoroutines();
        EventHandler.CallEnableInput(true);
        EventHandler.CallMessageClosed();
    }

    private IEnumerator CloseMessageRoutine()
    {
        yield return new WaitForSecondsRealtime(10f);
        CloseMessage();
    }

}
