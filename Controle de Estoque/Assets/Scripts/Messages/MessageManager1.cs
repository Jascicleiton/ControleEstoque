using System.Collections;
using UnityEngine;
using TMPro;
using UnityEngine.UIElements;

public class MessageManager1 : MonoBehaviour
{
    private VisualElement root;
    private Label messageText;
    private VisualElement messagePanel;
    private Button closeMessageButton;

    private string message1 = "";
    private string message2 = "";
    private bool inputEnabled = false;
    private bool isOnlyOneMessage = false;

    private void Update()
    {
        if (inputEnabled)
        {           
            if ((Input.GetKeyDown(KeyCode.KeypadEnter) || Input.GetKeyDown(KeyCode.Return)) && messagePanel.style.display == DisplayStyle.Flex)
            {
                CloseMessage();
            }
        }
    }

    private void OnEnable()
    {
        root = GetComponent<UIDocument>().rootVisualElement;
        messageText = root.Q<Label>("MessageLabel");
        messagePanel = root.Q<VisualElement>("MessagePanel");
        closeMessageButton = root.Q<Button>("MessageButton");
        EventHandler.OpenMessageEvent += MessageReceived;
      //  EventHandler.EnableInput += SetInputEnabled;
        EventHandler.IsOneMessageOnlyEvent += SetIsOneMessageOnly;
        closeMessageButton.clicked += () => { CloseMessage(); };
        inputEnabled = false;
          }

    private void OnDisable()
    {
        EventHandler.OpenMessageEvent -= MessageReceived;
        //EventHandler.EnableInput -= SetInputEnabled;
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
    /// Waits one second after message is opened to enable it's input
    /// </summary>
        private IEnumerator WaitASecond()
    {
        yield return new WaitForSeconds(1f);
    }

    /// <summary>
    /// Open a message based on the message(s) received
    /// </summary>
    private void OpenMessage()
    {
        inputEnabled = false;
        MouseManager.Instance.SetDefaultCursor();              
        messagePanel.style.display = DisplayStyle.Flex;
        messagePanel.style.visibility = Visibility.Visible;        

        if (!isOnlyOneMessage)
        {
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
            else if ((message1 == "Worked" && message2 != "Worked") || (message1 == "Updated" && message2 != "Updated"))
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
        }
        if (message1 == "Patrimônio já existe" || message1 == "Serial já existe")
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
            messageText.text = "Item não encontrado, verifique se o identificador do item foi digitado corretamente";
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
        else if (message1 == "Negative quantity")
        {
            messageText.text = "O estoque é menor que o valor que você quer mover. Altere o valor e tente novamente.";
              }
        else if (message1 == "Invalid number")
        {
            messageText.text = "Use apenas algarismos para determinar a quantidade a ser movida";
         }
        else if (message1 == "No movement found")
        {
            messageText.text = "Nenhuma movimentação encontrada para este item.";
            }
        else if (message1 == "Invalid number format")
        {
            messageText.text = "Use apenas algarismos para determinar a quantidade do item a ser adicionado.";
             }
        else if (message1 == "Username null")
        {
            messageText.text = "Usuário não encontrado. Tente novamente.";
         }
        else if (message1 == "Duplicate username")
        {
            messageText.text = "Usuário duplicado no banco de dados.\nContate seu administrador sobre este erro.";
            }
        else if (message1 == "Wrong password")
        {
            messageText.text = "Senha incorreta. Tente novamente.";
          }
        else if (message1 == "Username already exist")
        {
            messageText.text = "Usuário já cadastrado.";
           }
        else if (message1 == "User added")
        {
            messageText.text = "Novo usuário adicionado com sucesso.";
         }
        else if (message1 == "Wrong authorization access level")
        {
            messageText.text = "Usuário não possui autorização para permitir a criação de um novo usuário";
          }
        else if (message1 == "Empty values")
        {
            messageText.text = "Verifique se o usuário e/ou senha foi digitado corretamente";
                }
        else if (message1 == "Negative number")
        {
            messageText.text = "Utilize somente números positivos para o campo quantidade";
                }
        else if (message1 == "Zero quantity")
        {
            messageText.text = "Escolha uma quantidade maior que zero";
            }
        else if (message1 == "Atualizado")
        {
                        messageText.text = "Item atualizado no inventário com sucesso.";
        }
        else if(message1 == "Added")
        {
            messageText.text = "Item adicionado com sucesso.";
        }
        else if(message1 == "Null movement record")
        {
            messageText.text = "Registro do novo movimento não foi adicionado";
        }
        else if(message1 == "Empty input")
        {
            messageText.text = "Caixa de texto vazia. Digite um valor para ser pesquisado.";
        }
        else
        {
            messageText.text = message1 + "\nContate o administrador sobre este erro";
        }
        
        StartCoroutine(CloseMessageRoutine());
        StartCoroutine(WaitASecond());
        
    }

    /// <summary>
    /// Close the message panel and send an event call to all subscribed classes
    /// </summary>
    public void CloseMessage()
    {
        messageText.text = "";
        message1 = "";
        message2 = "";
        messagePanel.style.display = DisplayStyle.None;
        StopAllCoroutines();
        
        EventHandler.CallMessageClosed();
        //EventHandler.CallEnableInput(true);
    }

    /// <summary>
    /// Close the message after 10 seconds
    /// </summary>
    private IEnumerator CloseMessageRoutine()
    {
        yield return new WaitForSeconds(2f);
        inputEnabled = true;
        yield return new WaitForSecondsRealtime(8f);
        CloseMessage();
    }

}
