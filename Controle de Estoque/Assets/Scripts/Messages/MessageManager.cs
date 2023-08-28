using Assets.Scripts.Mouse;
using System.Collections;
using UnityEngine;
using UnityEngine.UIElements;

namespace Assets.Scripts.Messages
{
    public class MessageManager : MonoBehaviour
    {
        private Label _messageText;
        private VisualElement _messagePanel;
        private Button _closeMessageButton;

        private string _message1 = "";
        private string _message2 = "";
        private bool _inputEnabled = false;
        private bool _isOnlyOneMessage = false;

        private void Update()
        {
            if (_inputEnabled)
            {
                if ((Input.GetKeyDown(KeyCode.KeypadEnter) || Input.GetKeyDown(KeyCode.Return)) && _messagePanel.style.display == DisplayStyle.Flex)
                {
                    CloseMessage();
                }
            }
        }

        private void OnEnable()
        {
            VisualElement root = GetComponent<UIDocument>().rootVisualElement;
            _messageText = root.Q<Label>("MessageLabel");
            _messagePanel = root.Q<VisualElement>("MessagePanel");
            _closeMessageButton = root.Q<Button>("MessageButton");
            EventHandler.OpenMessageEvent += MessageReceived;
            //  EventHandler.EnableInput += SetInputEnabled;
            EventHandler.IsOneMessageOnlyEvent += SetIsOneMessageOnly;
            _closeMessageButton.clicked += () => { CloseMessage(); };
            _inputEnabled = false;
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
            if (_isOnlyOneMessage)
            {
                _message1 = messageReceived;
                OpenMessage();
            }
            else
            {
                if (_message1 == "")
                {
                    _message1 = messageReceived;
                }
                else
                {
                    _message2 = messageReceived;
                    OpenMessage();
                }
            }
        }

        /// <summary>
        /// Determines if it should show a message after receiving one or two messages. Called by IsOneMessageOnlyEvent
        /// </summary>
        private void SetIsOneMessageOnly(bool value)
        {
            _isOnlyOneMessage = value;
        }

        /// <summary>
        /// Enable or disable input. Called by EnableInput event. It is the opossite of the input received so it is enabled when the "screen" inputs
        /// are disabled
        /// </summary>
        private void SetInputEnabled(bool inputEnabled)
        {
            this._inputEnabled = !inputEnabled;
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
            _inputEnabled = false;
            MouseManager.Instance.SetDefaultCursor();
            _messagePanel.style.display = DisplayStyle.Flex;
            _messagePanel.style.visibility = Visibility.Visible;

            if (!_isOnlyOneMessage)
            {
                if (_message1 == "Worked" && _message2 == "Worked" || _message1 == "Updated" && _message2 == "Updated")
                {
                    if (_message1 == "Worked")
                    {
                        _messageText.text = "Item adicionado com sucesso.";
                    }
                    else if (_message1 == "Updated")
                    {
                        _messageText.text = "Item atualizado com sucesso.";
                    }

                    StartCoroutine(CloseMessageRoutine());
                    _inputEnabled = true;
                    return;
                }
                else if (_message1 == "Worked" && _message2 != "Worked" || _message1 == "Updated" && _message2 != "Updated")
                {
                    if (_message1 == "Worked")
                    {
                        _messageText.text = "Item adicionado no invent�rio com sucesso.\n" + _message2;
                    }
                    else if (_message1 == "Updated" && _message2 == "Worked")
                    {
                        _messageText.text = "Item atualizado no invent�rio com sucesso.";
                    }
                    else
                    {
                        _messageText.text = "Item atualizado no invent�rio com sucesso.\n" + _message2;
                    }
                    StartCoroutine(CloseMessageRoutine());
                    _inputEnabled = true;
                    return;
                }
            }
            if (_message1 == "Patrim�nio j� existe" || _message1 == "Serial j� existe")
            {
                _messageText.text = _message1;
            }
            else if (_message1 == "Duplicate locations")
            {
                _messageText.text = "N�o � poss�vel mover um item para o mesmo local atual";
            }
            else if (_message1 == "Empty location")
            {
                _messageText.text = "Um dos locais est� em branco. Coloque um local v�lido para a movimenta��o";
            }
            else if (_message1 == "Item not found")
            {
                _messageText.text = "Item n�o encontrado, verifique se o identificador do item foi digitado corretamente";
            }
            else if (_message1 == "Item moved")
            {
                _messageText.text = "Item movido com sucesso";
            }
            else if (_message1 == "Unable to move")
            {
                _messageText.text = "Item n�o p�de ser movido. Tente novamente e, se o problema persistir contate o suporte do programa";
            }
            else if (_message1 == "Invalid patrimonio format")
            {
                _messageText.text = "Patrim�nio consiste somente de n�meros. Tente novamente";
            }
            else if (_message1 == "Negative quantity")
            {
                _messageText.text = "O estoque � menor que o valor que voc� quer mover. Altere o valor e tente novamente.";
            }
            else if (_message1 == "Invalid number")
            {
                _messageText.text = "Use apenas algarismos para determinar a quantidade a ser movida";
            }
            else if (_message1 == "No movement found")
            {
                _messageText.text = "Nenhuma movimenta��o encontrada para este item.";
            }
            else if (_message1 == "Invalid number format")
            {
                _messageText.text = "Use apenas algarismos para determinar a quantidade do item a ser adicionado.";
            }
            else if (_message1 == "Username null")
            {
                _messageText.text = "Usu�rio n�o encontrado. Tente novamente.";
            }
            else if (_message1 == "Duplicate username")
            {
                _messageText.text = "Usu�rio duplicado no banco de dados.\nContate seu administrador sobre este erro.";
            }
            else if (_message1 == "Wrong password")
            {
                _messageText.text = "Senha incorreta. Tente novamente.";
            }
            else if (_message1 == "Username already exist")
            {
                _messageText.text = "Usu�rio j� cadastrado.";
            }
            else if (_message1 == "User added")
            {
                _messageText.text = "Novo usu�rio adicionado com sucesso.";
            }
            else if (_message1 == "Wrong authorization access level")
            {
                _messageText.text = "Usu�rio n�o possui autoriza��o para permitir a cria��o de um novo usu�rio";
            }
            else if (_message1 == "Empty values")
            {
                _messageText.text = "Verifique se o usu�rio e/ou senha foi digitado corretamente";
            }
            else if (_message1 == "Negative number")
            {
                _messageText.text = "Utilize somente n�meros positivos para o campo quantidade";
            }
            else if (_message1 == "Zero quantity")
            {
                _messageText.text = "Escolha uma quantidade maior que zero";
            }
            else if (_message1 == "Atualizado")
            {
                _messageText.text = "Item atualizado no invent�rio com sucesso.";
            }
            else if (_message1 == "Added")
            {
                _messageText.text = "Item adicionado com sucesso.";
            }
            else if (_message1 == "Null movement record")
            {
                _messageText.text = "Registro do novo movimento n�o foi adicionado";
            }
            else if (_message1 == "Empty input")
            {
                _messageText.text = "Caixa de texto vazia. Digite um valor para ser pesquisado.";
            }
            else
            {
                _messageText.text = _message1 + "\nContate o administrador sobre este erro";
            }

            StartCoroutine(CloseMessageRoutine());
            StartCoroutine(WaitASecond());

        }

        /// <summary>
        /// Close the message panel and send an event call to all subscribed classes
        /// </summary>
        public void CloseMessage()
        {
            _messageText.text = "";
            _message1 = "";
            _message2 = "";
            _messagePanel.style.display = DisplayStyle.None;
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
            _inputEnabled = true;
            yield return new WaitForSecondsRealtime(8f);
            CloseMessage();
        }

    }
}