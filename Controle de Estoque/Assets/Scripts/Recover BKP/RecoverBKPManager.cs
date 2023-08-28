using Assets.Scripts.Inventory.Database;
using Assets.Scripts.Inventory.Movement;
using Assets.Scripts.Misc;
using Assets.Scripts.Mouse;
using Assets.Scripts.Web;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.UIElements;

namespace Assets.Scripts.RecoverBKP
{
    public class RecoverBKPManager : MonoBehaviour
    {
        private DropdownField _tableToRecoverOnDatabaseDP;
        private TablesNamesOnDatabase _tableToSend;

        private void OnEnable()
        {
            GetUIElementsReferences();
            RegisterToEvents();
        }

        private void OnDisable()
        {
            UnregisterEvents();
        }

        private void GetUIElementsReferences()
        {
            VisualElement root = GetComponent<UIDocument>().rootVisualElement;
            _tableToRecoverOnDatabaseDP = root.Q<DropdownField>("TableToRecoverOnDatabaseDP");
        }

        private void RegisterToEvents()
        {
            _tableToRecoverOnDatabaseDP.RegisterCallback<ChangeEvent<string>>(ChooseWhatToRecover);
        }

        private void UnregisterEvents()
        {
            _tableToRecoverOnDatabaseDP.UnregisterCallback<ChangeEvent<string>>(ChooseWhatToRecover);
        }

        public void RecoverBKPasedOnSelection()
        {
            switch (_tableToSend)
            {
                case TablesNamesOnDatabase.Adaptador_AC:
                    break;
                case TablesNamesOnDatabase.Carregador:
                    break;
                case TablesNamesOnDatabase.Categorias:
                    break;
                case TablesNamesOnDatabase.Desktop:
                    break;
                case TablesNamesOnDatabase.Fonte:
                    break;
                case TablesNamesOnDatabase.GBIC:
                    break;
                case TablesNamesOnDatabase.HD:
                    break;
                case TablesNamesOnDatabase.iDrac:
                    break;
                case TablesNamesOnDatabase.Inventario:
                    StartCoroutine(RecoverInventarioRoutine());
                    break;
                case TablesNamesOnDatabase.Locais:
                    break;
                case TablesNamesOnDatabase.Memoria:
                    break;
                case TablesNamesOnDatabase.Monitor:
                    break;
                case TablesNamesOnDatabase.movements:
                    StartCoroutine(RecoverRegularMovements());
                    break;
                case TablesNamesOnDatabase.NoPaNoSe:
                    break;
                case TablesNamesOnDatabase.NoPaNoSeMovements:
                    break;
                case TablesNamesOnDatabase.Notebook:
                    break;
                case TablesNamesOnDatabase.Placa_constroladora:
                    break;
                case TablesNamesOnDatabase.Placa_de_captura_de_video:
                    break;
                case TablesNamesOnDatabase.Placa_de_rede:
                    break;
                case TablesNamesOnDatabase.Placa_de_som:
                    break;
                case TablesNamesOnDatabase.Placa_de_video:
                    break;
                case TablesNamesOnDatabase.Placa_SAS:
                    break;
                case TablesNamesOnDatabase.Processador:
                    break;
                case TablesNamesOnDatabase.Roteador:
                    break;
                case TablesNamesOnDatabase.Servidor:
                    break;
                case TablesNamesOnDatabase.Storage_NAS:
                    break;
                case TablesNamesOnDatabase.Switch:
                    break;
                case TablesNamesOnDatabase.users:
                    break;
                default:
                    break;
            }
        }


        private void ChooseWhatToRecover(ChangeEvent<string> evt)
        {
            _tableToSend = (TablesNamesOnDatabase)Enum.Parse(typeof(TablesNamesOnDatabase), evt.newValue);
        }

        private IEnumerator RecoverRegularMovements()
        {
            List<MovementRecords> _regularRecords = RegularMovementSaver.Instance.GetAllRegularRecords();
            if (_regularRecords.Count <= 0)
            {
                //TODO: Show message saying that no regular movement records were found.
                yield break;
            }
            MouseManager.Instance.SetWaitingCursor();
            foreach (var movement in _regularRecords)
            {
                WWWForm movementForm = CreateForm.GetMoveItemForm(ConstStrings.RecoverBKPKey,
                    movement.item.Patrimonio.ToString(), movement.item.Serial, movement.username, movement.date,
                    movement.fromWhere, movement.toWhere);

                UnityWebRequest createRecoverMovementBkpRequest = CreatePostRequest.GetPostRequest(
                    movementForm, ConstStrings.RecoverBKPPHP, 6);
                yield return createRecoverMovementBkpRequest.SendWebRequest();
                if (createRecoverMovementBkpRequest.result == UnityWebRequest.Result.ConnectionError)
                {
                    Debug.LogWarning("RecoveryBKP: conectionerror");
                    EventHandler.CallOpenMessageEvent("Server error: 1");
                }
                else if (createRecoverMovementBkpRequest.result == UnityWebRequest.Result.DataProcessingError)
                {
                    Debug.LogWarning("RecoveryBKP: data processing error");
                    EventHandler.CallOpenMessageEvent("Server error: 2");
                }
                else if (createRecoverMovementBkpRequest.result == UnityWebRequest.Result.ProtocolError)
                {
                    Debug.LogWarning("RecoveryBKP: protocol error");
                    EventHandler.CallOpenMessageEvent("Server error: 3");
                }
                if (createRecoverMovementBkpRequest.error == null)
                {
                    string response = createRecoverMovementBkpRequest.downloadHandler.text;
                    if (response == "Conection error")
                    {
                        Debug.LogWarning("AddUpdate: Server error");
                        EventHandler.CallOpenMessageEvent("Server error: 4");
                    }
                    else if (response == "Update failed")
                    {
                        Debug.LogWarning("Update: UpdateQuery failed");
                        EventHandler.CallOpenMessageEvent("Server error: 5");
                    }
                    else if (response == "insert item failed")
                    {
                        Debug.LogWarning("Insert: InsertQuery failed");
                        EventHandler.CallOpenMessageEvent("Server error: 6");
                    }
                }
            }
            MouseManager.Instance.SetDefaultCursor();
        }

        private IEnumerator RecoverInventarioRoutine()
        {
            print("Starting Routine");
            int index = 0;
            foreach (var item in InternalDatabase.Instance.fullDatabase.itens)
            {
                print($"item index: {index} patrimônio: {item.Patrimonio}");
                index++;
                List<string> parameters = new List<string>() { item.Aquisicao, item.Entrada, item.Patrimonio.ToString(),
            item.Status, item.Serial, item.Categoria, item.Fabricante, item.Modelo, item.Local, item.Saida, item.Observacao};
                WWWForm itemForm = CreateForm.GetInventarioForm(ConstStrings.RecoverBKPKey, parameters);

                UnityWebRequest createRecoverBKPRequest = CreatePostRequest.GetPostRequest(itemForm, ConstStrings.RecoverBKPPHP, 6);
                MouseManager.Instance.SetWaitingCursor();

                yield return createRecoverBKPRequest.SendWebRequest();
                if (createRecoverBKPRequest.result == UnityWebRequest.Result.ConnectionError)
                {
                    Debug.LogWarning("RecoveryBKP: conectionerror");
                    EventHandler.CallOpenMessageEvent("Server error: 1");
                }
                else if (createRecoverBKPRequest.result == UnityWebRequest.Result.DataProcessingError)
                {
                    Debug.LogWarning("RecoveryBKP: data processing error");
                    EventHandler.CallOpenMessageEvent("Server error: 2");
                }
                else if (createRecoverBKPRequest.result == UnityWebRequest.Result.ProtocolError)
                {
                    Debug.LogWarning("RecoveryBKP: protocol error");
                    EventHandler.CallOpenMessageEvent("Server error: 3");
                }
                if (createRecoverBKPRequest.error == null)
                {
                    string response = createRecoverBKPRequest.downloadHandler.text;
                    if (response == "Conection error")
                    {
                        Debug.LogWarning("AddUpdate: Server error");
                        EventHandler.CallOpenMessageEvent("Server error: 4");
                    }
                    else if (response == "Update failed")
                    {
                        Debug.LogWarning("Update: UpdateQuery failed");
                        EventHandler.CallOpenMessageEvent("Server error: 5");
                    }
                    else if (response == "insert item failed")
                    {
                        Debug.LogWarning("Insert: InsertQuery failed");
                        EventHandler.CallOpenMessageEvent("Server error: 6");
                    }
                }
            }
            MouseManager.Instance.SetDefaultCursor();
        }
    }
}