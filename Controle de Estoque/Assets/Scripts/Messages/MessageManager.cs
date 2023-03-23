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

    /// <summary>
    /// Receive a message and show an apropriate response
    /// </summary>
    private void MessageReceived(string messageReceived)
    {
        if (isOnlyOneMessage)
        {
            message1 = messageReceived;
            OpenMessage();
        }
        else
        {
            if (message1 == "")
            {
                message1 = messageReceived;
            }
            else
            {
                message2 = messageReceived;
                OpenMessage();
            }
        }   
    }

    /// <summary>
    /// Determines if it should show a message after receiving one or two messages. Called by IsOneMessageOnlyEvent
    /// </summary>
    private void SetIsOneMessageOnly(bool value)
    {
        isOnlyOneMessage = value;
    }

    /// <summary>
    /// Enable or disable input. Called by EnableInput event. It is the opossite of the input received so it is enabled when the "screen" inputs
    /// are disabled
    /// </summary>
    private void SetInputEnabled(bool inputEnabled)
    {        
        this.inputEnabled = !inputEnabled;
    }

    /// <summary>
    /// Open a message based on the message(s) received
    /// </summary>
    private void OpenMessage()
    {
        MouseManager.Instance.SetDefaultCursor();
        EventHandler.CallEnableInput(false);
        inputEnabled = true;
        messagePanel.SetActive(true);
        if ((message1 == "Worked" && message2 == "Worked") || (message1 == "Updated" && message2 == "Updated"))
        {
            if (message1 == "Worked")
            {
                messageText.text = "Item adicionado com sucesso.";
            }
            else if (message1 == "Updated")
            {
                messageText.text = "Item atualizado com sucesso.";
            }

            //full success
        }
        else if ((message1 == "Worked" && message2 != "Worked") || (message1 == "Updated" && (message2 != "Updated" || message2 != "Worked")))
        {
            if (message1 == "Worked")
            {
                messageText.text = "Item adicionado no inventário com sucesso.\n" + message2;
            }
            else if (message1 == "Updated")
            {
                messageText.text = "Item atualizado no inventário com sucesso.\n" + message2;
            }
        }
        else if (message1 == "Patrimônio já existe" || message1 == "Serial já existe")
        {
            messageText.text = message1;
        }
        else if (message1 == "Duplicate locations")
        {
            messageText.text = "Não é possível mover um item para o mesmo local atual";
        }
        else if (message1 == "Empty location")
        {
            messageText.text = "Um dos locais está em branco. Coloque um local válido para a movimentação";
        }
        else if (message1 == "Item not found")
        {
            messageText.text = "Item não encontrado, verifique o identificador do item foi digitado corretamente";
        }
        else if (message1 == "Item moved") 
        {
            messageText.text = "Item movido com sucesso";
        }
        else if (message1 == "Unable to move")
        {
            messageText.text = "Item não pôde ser movido. Tente novamente e, se o problema persistir contate o suporte do programa";
        }
        else if (message1 == "Invalid patrimonio format")
        {
            messageText.text = "Patrimônio consiste somente de números. Tente novamente";
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

    /// <summary>
    /// Close the message panel and send an event call to all subscribed classes
    /// </summary>
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

    /// <summary>
    /// Close the message after 10 seconds
    /// </summary>
    private IEnumerator CloseMessageRoutine()
    {
        yield return new WaitForSecondsRealtime(10f);
        CloseMessage();
    }

}
