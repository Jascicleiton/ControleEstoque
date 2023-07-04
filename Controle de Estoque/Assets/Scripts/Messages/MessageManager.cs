using System.Collections;
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

            StartCoroutine(CloseMessageRoutine());
            inputEnabled = true;
            return;
        }
        else if ((message1 == "Worked" && message2 != "Worked") || (message1 == "Updated" && message2 != "Updated") )
        {
            if (message1 == "Worked")
            {
                messageText.text = "Item adicionado no inventário com sucesso.\n" + message2;
            }
            else if (message1 == "Updated" && message2 == "Worked")
            {
                messageText.text = "Item atualizado no inventário com sucesso.";
            }
            else
            {
                messageText.text = "Item atualizado no inventário com sucesso.\n" + message2;
            }
            StartCoroutine(CloseMessageRoutine());
            inputEnabled = true;
            return;
        }
        else if (message1 == "Patrimônio já existe" || message1 == "Serial já existe")
        {
            messageText.text = message1;
            StartCoroutine(CloseMessageRoutine());
            inputEnabled = true;
            return;
        }
        else if (message1 == "Duplicate locations")
        {
            messageText.text = "Não é possível mover um item para o mesmo local atual";
            StartCoroutine(CloseMessageRoutine());
            inputEnabled = true;
            return;
        }
        else if (message1 == "Empty location")
        {
            messageText.text = "Um dos locais está em branco. Coloque um local válido para a movimentação";
            StartCoroutine(CloseMessageRoutine());
            inputEnabled = true;
            return;
        }
        else if (message1 == "Item not found")
        {
            messageText.text = "Item não encontrado, verifique o identificador do item foi digitado corretamente";
            StartCoroutine(CloseMessageRoutine());
            inputEnabled = true;
            return;
        }
        else if (message1 == "Item moved") 
        {
            messageText.text = "Item movido com sucesso";
            StartCoroutine(CloseMessageRoutine());
            inputEnabled = true;
            return;
        }
        else if (message1 == "Unable to move")
        {
            messageText.text = "Item não pôde ser movido. Tente novamente e, se o problema persistir contate o suporte do programa";
            StartCoroutine(CloseMessageRoutine());
            inputEnabled = true;
            return;
        }
        else if (message1 == "Invalid patrimonio format")
        {
            messageText.text = "Patrimônio consiste somente de números. Tente novamente";
            StartCoroutine(CloseMessageRoutine());
            inputEnabled = true;
            return;
        }
        else if (message1 == "Negative number")
        {
            messageText.text = "O estoque é menor que o valor que você quer mover. Altere o valor e tente novamente.";
            StartCoroutine(CloseMessageRoutine());
            inputEnabled = true;
            return;
        }
        else if( message1 == "Invalid number")
        {
            messageText.text = "Use apenas algarismos para determinar a quantidade a ser movida";
            StartCoroutine(CloseMessageRoutine());
            inputEnabled = true;
            return;
        }
        else if(message1 == "No movement found")
        {
            messageText.text = "Nenhuma movimentação encontrada para este item.";
            StartCoroutine(CloseMessageRoutine());
            inputEnabled = true;
            return;
        }
        else if(message1 == "Invalid number format")
        {
            messageText.text = "Use apenas algarismos para determinar a quantidade do item a ser adicionado.";
            StartCoroutine(CloseMessageRoutine());
            inputEnabled = true;
            return;
        }
        else if(message1 == "Username null")
        {
            messageText.text = "Usuário não encontrado. Tente novamente.";
            StartCoroutine(CloseMessageRoutine());
            inputEnabled = true;
            return;
        }
        else if(message1 == "Duplicate username")
        {
            messageText.text = "Usuário duplicado no banco de dados.\nContate seu administrador sobre este erro.";
            StartCoroutine(CloseMessageRoutine());
            inputEnabled = true;
            return;
        }
        else if(message1 == "Wrong password")
        {
            messageText.text = "Senha incorreta. Tente novamente.";
            StartCoroutine(CloseMessageRoutine());
            inputEnabled = true;
            return;
        }
        else if(message1 == "Username already exist")
        {
            messageText.text = "Usuário já cadastrado.";
            StartCoroutine(CloseMessageRoutine());
            inputEnabled = true;
            return;
        }
        else if (message1 == "User added")
        {
            messageText.text = "Novo usuário adicionado com sucesso.";
            StartCoroutine(CloseMessageRoutine());
            inputEnabled = true;
            return;
        }
        else if(message1 == "Wrong authorization access level")
        {
            messageText.text = "Usuário não possui autorização para permitir a criação de um novo usuário";
            StartCoroutine(CloseMessageRoutine());
            inputEnabled = true;
        }
        else if(message1 == "Empty values")
        {
            messageText.text = "Verifique se o usuário e/ou senha foi digitado corretamente";
            StartCoroutine(CloseMessageRoutine());
            inputEnabled = true;
        }
        else
        {
            messageText.text = message1 + "\nContate o administrador sobre este erro";
        }
        StartCoroutine(CloseMessageRoutine());
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
        EventHandler.CallEnableInput(true);
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
