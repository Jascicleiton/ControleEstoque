using SimpleJSON;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UIElements;
using UnityEngine;
using UnityEngine.Networking;
using Assets.Scripts.UI;
using Assets.Scripts.Inventory.Database;
using Assets.Scripts.Misc;
using Assets.Scripts.Mouse;
using Assets.Scripts.ScreenManager;
using Assets.Scripts.Web;

namespace Assets.Scripts.Inventory.UpdateItem
{
    public class UpdateItem : MonoBehaviour
    {
        private ItemInformationPanelControler _itemInformationPanelControler;
        private ConsultDatabase _consultDatabaseReference;

        private TextField _itemToUpdatePatrimonio;
        private VisualElement _inputsPanel;
        private Button _resetButton;
        private Button _returnButton;

        private bool _searchingItem = true;
        private bool _inputEnabled = true;

        private List<string> _parameters = new List<string>();
        private ItemColumns _itemToUpdate;
        private int _itemToUpdateIndex;

        private bool _updateInventarioSuccess = false;

        void Start()
        {
            ResetInputs();
            _itemToUpdate = new ItemColumns();
            _inputEnabled = true;
            _itemInformationPanelControler = GetComponent<ItemInformationPanelControler>();
            _consultDatabaseReference = new ConsultDatabase();
        }

        /// <summary>
        /// Subscribes to MessageClosed and SetInputEnabled events
        /// </summary>
        private void OnEnable()
        {
            GetUIReferences();
        }

        /// <summary>
        /// Unsubscribes to MessageClosed and SetInputEnabled events
        /// </summary>
        private void OnDisable()
        {
            UnsubscribeToEvents();
        }

        /// <summary>
        /// Handles what happens if Enter is pressed
        /// </summary>
        private void Update()
        {
            if (_inputEnabled)
            {
                if (Input.GetKeyDown(KeyCode.Return) || Input.GetKeyDown(KeyCode.KeypadEnter))
                {
                    _inputEnabled = false;
                    if (_searchingItem)
                    {
                        if (_itemToUpdatePatrimonio.value == "" || _itemToUpdatePatrimonio.value == null)
                        {
                            EventHandler.CallIsOneMessageOnlyEvent(true);
                            EventHandler.CallOpenMessageEvent("Empty input");
                            _inputEnabled = true;
                            return;
                        }
                        if (!InternalDatabase.Instance.isOfflineProgram)
                        {
                            StartCoroutine(CheckIfItemExists());
                        }
                        _itemToUpdate = _consultDatabaseReference.ConsultPatrimonio(int.Parse(_itemToUpdatePatrimonio.value), InternalDatabase.Instance.fullDatabase);
                        _itemToUpdateIndex = _consultDatabaseReference.GetItemIndex();
                        ShowUpdateItem();
                    }
                    else
                    {
                        if (!InternalDatabase.Instance.isOfflineProgram)
                        {
                            StartCoroutine(UpdateDatabaseRoutine());
                        }
                        else
                        {
                            UpdateFullDatabase();
                        }
                    }
                }
            }
        }

        private void GetUIReferences()
        {
            VisualElement root = GetComponent<UIDocument>().rootVisualElement;
            _itemToUpdatePatrimonio = root.Q<TextField>("PatrimonioTextField");
            _inputsPanel = root.Q<VisualElement>("ParametersContainer");
            _resetButton = root.Q<Button>("ResetButton");
            _returnButton = root.Q<Button>("ReturnButton");
            SubscribeToEvents();
        }

        private void SubscribeToEvents()
        {
            _returnButton.clicked += () => { ReturnToPreviousScreen(); };
            _resetButton.clicked += () => { ResetInputs(); };
            EventHandler.MessageClosed += MessageClosed;
            EventHandler.EnableInput += SetInputEnabled;
        }

        private void UnsubscribeToEvents()
        {
            _returnButton.clicked -= () => { ReturnToPreviousScreen(); };
            _resetButton.clicked -= () => { ResetInputs(); };
            EventHandler.MessageClosed -= MessageClosed;
            EventHandler.EnableInput -= SetInputEnabled;
        }

        /// <summary>
        /// Enables or disables the input. Called by the Event EnableInput
        /// </summary>
        private void SetInputEnabled(bool inputEnabled)
        {
            _resetButton.SetEnabled(inputEnabled);
            _returnButton.SetEnabled(inputEnabled);
            _itemToUpdatePatrimonio.SetEnabled(inputEnabled);
            _inputEnabled = inputEnabled;
        }

        /// <summary>
        /// Check if the item that is going to be updated exists on the online database
        /// </summary>
        private IEnumerator CheckIfItemExists()
        {
            WWWForm itemForm = new WWWForm();
            _itemToUpdate = new ItemColumns();
            UnityWebRequest createItemUpdatePostRequest = new UnityWebRequest();

            itemForm = CreateForm.GetConsultPatrimonioForm(ConstStrings.ConsultKey, _itemToUpdatePatrimonio.text);
            createItemUpdatePostRequest = CreatePostRequest.GetPostRequest(itemForm, ConstStrings.GetItemPatrimonioToUpdate, 4);

            MouseManager.Instance.SetWaitingCursor();
            EventHandler.CallEnableInput(false);
            yield return createItemUpdatePostRequest.SendWebRequest();
            EventHandler.CallIsOneMessageOnlyEvent(true);
            if (createItemUpdatePostRequest.result == UnityWebRequest.Result.ConnectionError)
            {
                Debug.LogWarning("CheckIfItemExists: conectionerror");
                EventHandler.CallOpenMessageEvent("Server error: 1");
            }
            else if (createItemUpdatePostRequest.result == UnityWebRequest.Result.DataProcessingError)
            {
                Debug.LogWarning("CheckIfItemExists: data processing error");
                EventHandler.CallOpenMessageEvent("Server error: 2");
            }
            else if (createItemUpdatePostRequest.result == UnityWebRequest.Result.ProtocolError)
            {
                Debug.LogWarning("CheckIfItemExists: protocol error");
                EventHandler.CallOpenMessageEvent("Server error: 3");
            }

            if (createItemUpdatePostRequest.error == null)
            {
                string response = createItemUpdatePostRequest.downloadHandler.text;
                if (response == "Database connection error")
                {
                    Debug.LogWarning("CheckIfItemExists: conectionerror error");
                    EventHandler.CallOpenMessageEvent("Server error: 4");
                }
                else if (response == "Wrong appkey")
                {
                    Debug.LogWarning("CheckIfItemExists: app key");
                    EventHandler.CallOpenMessageEvent("Server error: 8");
                }
                else if (response == "Check query failed")
                {
                    Debug.LogWarning("CheckIfItemExists: query error");
                    EventHandler.CallOpenMessageEvent("Server error: 7.5");
                }
                else if (response == "Item not found")
                {
                    Debug.LogWarning("CheckIfItemExists: Item does not exist");
                    EventHandler.CallOpenMessageEvent("Server error: 11");
                }
                else
                {
                    JSONNode inventario = JSON.Parse(createItemUpdatePostRequest.downloadHandler.text);
                    Sheet tempSheet = new Sheet();
                    ImportingInventoryFunctions.ImportInventory(inventario, out tempSheet);
                    _itemToUpdate = tempSheet.itens[0];
                    if (inventario == null)
                    {
                        Debug.LogWarning("CheckIfItemExists: " + response);
                        EventHandler.CallOpenMessageEvent("Server error: 12");
                    }
                }

            }
            else
            {
                Debug.LogWarning(createItemUpdatePostRequest.error);
                EventHandler.CallOpenMessageEvent("Server error: 10");
            }
            createItemUpdatePostRequest.Dispose();
            MouseManager.Instance.SetDefaultCursor();
            EventHandler.CallEnableInput(true);

            if (_itemToUpdate != null)
            {
                _searchingItem = false;
                //inputsPanel.SetActive(true);
                EventHandler.CallEnableInput(true);
                ShowUpdateItem();

            }
            else
            {
                _searchingItem = true;
                EventHandler.CallOpenMessageEvent("Item a ser atualizado não foi encontrado");
            }
        }

        /// <summary>
        /// Update the item on the online database
        /// </summary>
        private IEnumerator UpdateDatabaseRoutine()
        {
            EventHandler.CallIsOneMessageOnlyEvent(false);
            #region Update inventario
            _parameters.Clear();
            _parameters = _itemInformationPanelControler.GetInventoryValues();
            yield return HelperMethods.AddUpdateItem(HelperMethods.GetCategoryInt(_itemToUpdate.Categoria), 4, _parameters, true);
            if (HelperMethods.GetAddUpdateResponse())
            {
                _updateInventarioSuccess = true;
            }
            else
            {
                _updateInventarioSuccess = false;
            }

            #endregion
            #region Update the details tables
            _parameters.Clear();
            _parameters = _itemInformationPanelControler.GetCategoryValues(_itemToUpdate.Categoria);
            #endregion
            yield return HelperMethods.AddUpdateItem(HelperMethods.GetCategoryInt(_itemToUpdate.Categoria), 4, _parameters, false);
            if (HelperMethods.GetAddUpdateResponse())
            {
                _updateInventarioSuccess = true;
            }
            else
            {
                _updateInventarioSuccess = false;
            }
            if (_updateInventarioSuccess)
            {
                UpdateFullDatabase();
            }
        }

        /// <summary>
        /// Updates the internal full database
        /// </summary>
        private void UpdateFullDatabase()
        {
            List<string> parameters = new List<string>();
            parameters.AddRange(_itemInformationPanelControler.GetInventoryValues());
            parameters.AddRange(_itemInformationPanelControler.GetCategoryValues(_itemToUpdate.Categoria));

            UpdateDatabaseItem.UpdateItem(parameters, _itemToUpdateIndex);
            EventHandler.CallDatabaseUpdatedEvent();
            if (InternalDatabase.Instance.isOfflineProgram)
            {
                EventHandler.CallIsOneMessageOnlyEvent(true);
                EventHandler.CallOpenMessageEvent("Atualizado");
            }
        }

        /// <summary>
        /// Shows the itemToUpdate values on the text of all the inputs
        /// </summary>
        private void ShowUpdateItem()
        {
            if (_itemToUpdate == null)
            {
                EventHandler.CallIsOneMessageOnlyEvent(true);
                EventHandler.CallOpenMessageEvent("Item not found");
                return;
            }

            ItemColumns tempItem = _consultDatabaseReference.ConsultPatrimonio(_itemToUpdate.Patrimonio, HelperMethods.GetCategoryDatabaseToConsult(_itemToUpdate.Categoria));
            if (tempItem != null)
            {
                _itemToUpdate = tempItem;
                tempItem = _consultDatabaseReference.ConsultPatrimonio(_itemToUpdate.Patrimonio, InternalDatabase.Instance.fullDatabase);
            }
            else
            {

            }
            _searchingItem = false;
            _inputsPanel.style.display = DisplayStyle.Flex;
            _itemInformationPanelControler.ShowItem(_itemToUpdate);
        }


        /// <summary>
        /// Resets the texts on all inputs
        /// </summary>
        private void ResetInputs()
        {
            _searchingItem = true;
            _inputEnabled = true;
            _inputsPanel.style.display = DisplayStyle.Flex;
            if (_itemInformationPanelControler == null)
            {
                _itemInformationPanelControler = GetComponent<ItemInformationPanelControler>();
            }
            _itemInformationPanelControler.ResetItems();
            _itemInformationPanelControler.ResetValues();
            _inputsPanel.style.display = DisplayStyle.None;
        }

        /// <summary>
        /// Called by the Event MessageClosed to reset all inputs and enable input after tha message is closed
        /// </summary>
        private void MessageClosed()
        {
            _searchingItem = true;
            ResetInputs();

            _itemToUpdatePatrimonio.value = "";
            MouseManager.Instance.SetDefaultCursor();
            StartCoroutine(WaitASecond());
        }

        /// <summary>
        /// Waits one second to enable input to prevent the Update from wrongly detecting enter input 
        /// </summary>
        private IEnumerator WaitASecond()
        {
            yield return new WaitForSecondsRealtime(1f);
            EventHandler.CallEnableInput(true);
        }

        /// <summary>
        /// Returns to InitialScene
        /// </summary>
        public void ReturnToPreviousScreen()
        {
            ChangeScreenManager.Instance.OpenScene(Scenes.UpdateItemScene, Scenes.InitialScene);
        }
    }
}