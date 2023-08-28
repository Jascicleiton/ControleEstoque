using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
using System.Linq;
using UnityEngine.UIElements;
using System.Xml.Linq;
using System;
using Assets.Scripts.Inventory.Database;
using Assets.Scripts.Inventory.Movement;
using Assets.Scripts.Misc;
using Assets.Scripts.Mouse;
using Assets.Scripts.ScreenManager;
using Assets.Scripts.Users;
using Assets.Scripts.Web;

namespace Assets.Scripts.Inventory.NoPatrimonioNoSerial
{
    public class NoPaNoSeManager : Singleton<NoPaNoSeManager>
    {
        private NoPaNoSeAll _allitems = new NoPaNoSeAll();
        private VisualTreeAsset _itemTemplate;
        private Button _openAddNewItemButton;
        private Button _consultButton;
        private Button _movementButton;
        private Button _returnButton;
        private Button _increaseButton;
        private Button _decreaseButton;
        private Color _defaultColor;
        private Color _inactiveColor;

        #region New item
        private VisualElement _newItemPanel;
        private TextField _newItemNameInput;
        private TextField _newItemQuantityInput;
        private Button _addNewItemButton;
        private bool _isNewItemPanelOpen = false;
        #endregion

        #region Consult
        private VisualElement _consultContainer;
        private ListView _consultList;
        #endregion

        #region Movement
        private VisualElement _movementContainer;
        private DropdownField _itemsNamesDP;
        private VisualElement _itemToMoveContainer;
        private TextField _quantityTextField;
        private VisualElement _whereFromPanel;
        private DropdownField _fromDP;
        private TextField _whereFromTextField;
        private VisualElement _whereToPanel;
        private DropdownField _toDP;
        private TextField _whereToTextField;
        private Button _moveItemButton;
        private VisualElement _quantityPanel;
        private Label _itemCurrentQuantityLabel;
        #endregion

        private int _itemIndex;
        private int _parsedInt = 0; // Used for all int.TryParse
        private bool _isAdding = false; // if false, the item is being removed from "Estoque", if true the item is beign added to "Estoque"

        NoPaNoSeItemManager _noPaNoSeItemManager;

        private void OnEnable()
        {
            _noPaNoSeItemManager = new NoPaNoSeItemManager();
            GetUIReferences();
        }

        private void OnDisable()
        {
            UnsubscribeToEvents();
        }

        private void Update()
        {
            if (_isNewItemPanelOpen && Input.GetKeyDown(KeyCode.Escape))
            {
                CloseNewItemPanel();
            }
        }

        /// <summary>
        /// Called when NoPaNoSeScene is loaded to initialize some values and update the UI
        /// </summary>
        private void Initialize()
        {
            _defaultColor = new Color(1f, 1f, 1f, 1f);
            _inactiveColor = new Color(0.6f, 0.6f, 0.6f, 1f);
            _allitems = new NoPaNoSeAll();
            switch (UsersManager.Instance.CurrentUser.GetAccessLevel())
            {
                case 1:
                case 2:
                case 4:
                    _addNewItemButton.style.display = DisplayStyle.None;
                    break;
                default:
                    break;
            }
            ShowItems(NoPaNoSeImporter.Instance.ItemsList.noPaNoSeItems);
            FillDropdowns();
        }

        private void GetUIReferences()
        {
            VisualElement root = GetComponent<UIDocument>().rootVisualElement;
            _openAddNewItemButton = root.Q<Button>("OpenAddNewItemButton");
            _returnButton = root.Q<Button>("ReturnButton");
            _itemTemplate = Resources.Load<VisualTreeAsset>("Templates/NoPaNoSeConsultItem");

            _newItemPanel = root.Q<VisualElement>("NewItemContainer");
            _newItemNameInput = root.Q<TextField>("NewItemName");
            _newItemQuantityInput = root.Q<TextField>("NewItemQuantity");
            _addNewItemButton = root.Q<Button>("AddNewItemButton");

            _consultButton = root.Q<Button>("ConsultButton");
            _movementButton = root.Q<Button>("MoveButton");

            _consultContainer = root.Q<VisualElement>("ConsultContainer");
            _consultList = root.Q<ListView>("ConsultLV");

            _movementContainer = root.Q<VisualElement>("MovementContainer");
            _itemsNamesDP = root.Q<DropdownField>("ItemsNamesDP");
            _itemToMoveContainer = root.Q<VisualElement>("ItemToMoveContainer");
            _quantityTextField = root.Q<TextField>("QuantityTextField");
            _whereFromPanel = root.Q<VisualElement>("WhereFromPanel");
            _fromDP = root.Q<DropdownField>("FromDP");
            _whereFromTextField = root.Q<TextField>("WhereFromTextField");
            _whereToPanel = root.Q<VisualElement>("WhereToPanel");
            _toDP = root.Q<DropdownField>("ToDP");
            _whereToTextField = root.Q<TextField>("WhereToTextField");
            _moveItemButton = root.Q<Button>("MoveItemButton");
            _increaseButton = root.Q<Button>("IncreaseButton");
            _decreaseButton = root.Q<Button>("DecreaseButton");
            _quantityPanel = root.Q<VisualElement>("QuantityPanel");
            _itemCurrentQuantityLabel = root.Q<Label>("ItemCurrentQuantity");
            SubscribeToEvents();
            Initialize();
        }

        private void SubscribeToEvents()
        {
            _returnButton.clicked += () => { ReturnToPreviousScreen(); };
            _openAddNewItemButton.clicked += () => { OpenAddNewItemPanel(); };
            _addNewItemButton.clicked += () => { AddNewItemClicked(); };
            _consultButton.clicked += () => { ConsultButtonClicked(); };
            _movementButton.clicked += () => { MovementButtonClicked(); };
            _itemsNamesDP.RegisterCallback<ChangeEvent<string>>(ItemToMoveChosen);
            _fromDP.RegisterCallback<ChangeEvent<string>>(HandleFromDP);
            _toDP.RegisterCallback<ChangeEvent<string>>(HandleToDP);
            _moveItemButton.clicked += () => { MoveItemClicked(); };
            _increaseButton.clicked += () => { IncreaseClicked(); };
            _decreaseButton.clicked += () => { DecreaseClicked(); };
            EventHandler.NoPaNoSeItemQuantityChanged += UpdateItemQuantity;
        }

        private void UnsubscribeToEvents()
        {
            _returnButton.clicked -= () => { ReturnToPreviousScreen(); };
            _openAddNewItemButton.clicked -= () => { OpenAddNewItemPanel(); };
            _addNewItemButton.clicked -= () => { AddNewItemClicked(); };
            _consultButton.clicked -= () => { ConsultButtonClicked(); };
            _movementButton.clicked -= () => { MovementButtonClicked(); };
            _itemsNamesDP.UnregisterCallback<ChangeEvent<string>>(ItemToMoveChosen);
            _moveItemButton.clicked -= () => { MoveItemClicked(); };
            _fromDP.UnregisterCallback<ChangeEvent<string>>(HandleFromDP);
            _toDP.UnregisterCallback<ChangeEvent<string>>(HandleToDP);
            _increaseButton.clicked -= () => { IncreaseClicked(); };
            _decreaseButton.clicked -= () => { DecreaseClicked(); };
            EventHandler.NoPaNoSeItemQuantityChanged -= UpdateItemQuantity;
        }

        /// <summary>
        /// Used by AddNewItem_btn outside newItemPanel
        /// </summary>
        private void OpenAddNewItemPanel()
        {
            _newItemPanel.style.display = DisplayStyle.Flex;
            _newItemNameInput.value = "";
            _newItemQuantityInput.value = "";
            _isNewItemPanelOpen = true;
            //  EventHandler.CallUpdateTabInputs();
        }

        /// <summary>
        /// Add a new item to the screen and scrolls to the bottom of the list
        /// </summary>
        private void AddNewItem(string itemName, int itemQuantity)
        {
            bool wasActive = true;
            if (_consultContainer.style.display != DisplayStyle.Flex)
            {
                wasActive = false;
                _consultContainer.style.display = DisplayStyle.Flex;
            }

            NoPaNoSeItem newItem = new NoPaNoSeItem();
            newItem.ItemName = itemName;
            newItem.Quantity = itemQuantity;
            _allitems.noPaNoSeItems.Add(newItem);
            NoPaNoSeImporter.Instance.AddNewItem(newItem);

            if (!wasActive)
            {
                _consultContainer.style.display = DisplayStyle.None;
            }
            else
            {
                _consultList.Rebuild();
            }
        }

        /// <summary>
        /// Sort Alphabetically the imported list of items and show it
        /// </summary>
        private void ShowItems(List<NoPaNoSeItem> itemsToShow)
        {
            List<NoPaNoSeItem> sortedItemsToShow = itemsToShow.OrderBy(x => x.ItemName).ToList();

            _consultList.makeItem = () => _itemTemplate.Instantiate();
            _consultList.bindItem = (ve, i) =>
            {
                // ve.style.marginTop = 15;
                Label itemNameLabel = ve.Q<Label>("Name");
                Label itemQuantityLabel = ve.Q<Label>("Quantity");

                itemNameLabel.text = sortedItemsToShow[i].ItemName;
                itemQuantityLabel.text = sortedItemsToShow[i].Quantity.ToString();
                ve.style.height = StyleKeyword.Auto;
            };
            _consultList.fixedItemHeight = 80;
            _consultList.itemsSource = sortedItemsToShow;

            _allitems.noPaNoSeItems = sortedItemsToShow;
        }

        /// <summary>
        /// Try to add a new item to the online database and shows it on the screen
        /// </summary>
        private IEnumerator AddNewItemRoutine()
        {
            WWWForm itemForm = CreateForm.GetNoPaNoSeForm(ConstStrings.AddNewItemKey, _newItemNameInput.text, _parsedInt);

            UnityWebRequest createUpdateInventarioRequest = CreatePostRequest.GetPostRequest(itemForm, ConstStrings.AddNoPaNoSe, 5);
            MouseManager.Instance.SetWaitingCursor();

            yield return createUpdateInventarioRequest.SendWebRequest();

            if (createUpdateInventarioRequest.result == UnityWebRequest.Result.ConnectionError)
            {
                Debug.LogWarning("AddNewItemRoutine: conectionerror");
            }
            else if (createUpdateInventarioRequest.result == UnityWebRequest.Result.DataProcessingError)
            {
                Debug.LogWarning("AddNewItemRoutine: data processing error");
            }
            else if (createUpdateInventarioRequest.result == UnityWebRequest.Result.ProtocolError)
            {
                Debug.LogWarning("AddNewItemRoutine: protocol error");
            }

            if (createUpdateInventarioRequest.error == null)
            {
                string response = createUpdateInventarioRequest.downloadHandler.text;
                if (response == "Conection error" || response == "Inventario update failed")
                {
                    Debug.LogWarning("AddNewItemRoutine: Server error");
                }
                else if (response == "Wrong app key")
                {
                    Debug.LogWarning("AddNewItemRoutine: app key");
                }
                else if (response == "Item added")
                {
                    AddNewItem(_newItemNameInput.value, int.Parse(_newItemQuantityInput.value));
                    EventHandler.CallDatabaseUpdatedEvent();
                    _newItemPanel.style.display = DisplayStyle.None;
                }
                else
                {
                    Debug.LogWarning("AddNewItemRoutine: " + response);
                    // TODO: send message to user with error and recomendation
                }
            }
            else
            {
                Debug.LogWarning("AddNewItemRoutine: " + createUpdateInventarioRequest.error);
                // TODO: send message to user with error and recomendation
            }
            createUpdateInventarioRequest.Dispose();
            MouseManager.Instance.SetDefaultCursor();
        }

        /// <summary>
        /// Used by AddNewItem_btn inside newItemPanel
        /// </summary>
        public void AddNewItemClicked()
        {
            if (!int.TryParse(_newItemQuantityInput.text, out _parsedInt))
            {
                EventHandler.CallIsOneMessageOnlyEvent(true);
                EventHandler.CallOpenMessageEvent("Invalid number format");
                return;
            }

            if (!InternalDatabase.Instance.isOfflineProgram)
            {
                StartCoroutine(AddNewItemRoutine());
            }
            else
            {
                AddNewItem(_newItemNameInput.value, _parsedInt);
                EventHandler.CallDatabaseUpdatedEvent();
                CloseNewItemPanel();
            }
        }

        private void CloseNewItemPanel()
        {
            _newItemPanel.style.display = DisplayStyle.None;
            _isNewItemPanelOpen = false;
        }

        private void ConsultButtonClicked()
        {
            _movementContainer.style.display = DisplayStyle.None;
            _consultContainer.style.display = DisplayStyle.Flex;
            _movementButton.style.unityBackgroundImageTintColor = _inactiveColor;
            _consultButton.style.unityBackgroundImageTintColor = _defaultColor;
        }

        private void MovementButtonClicked()
        {
            _consultContainer.style.display = DisplayStyle.None;
            _movementContainer.style.display = DisplayStyle.Flex;
            _movementButton.style.unityBackgroundImageTintColor = _defaultColor;
            _consultButton.style.unityBackgroundImageTintColor = _inactiveColor;
            FillItemsToMoveDP();
            HideMoveElements();
        }

        private void ItemToMoveChosen(ChangeEvent<string> evt)
        {
            _itemToMoveContainer.style.display = DisplayStyle.Flex;
            _itemCurrentQuantityLabel.text = GetItemToMove().Quantity.ToString();
            //itemNameToMoveLabel.text = evt.newValue;
        }

        private void IncreaseClicked()
        {
            _isAdding = true;
            _quantityPanel.style.display = DisplayStyle.Flex;
            _whereFromPanel.style.display = DisplayStyle.Flex;
            _whereToPanel.style.display = DisplayStyle.None;
            _moveItemButton.style.display = DisplayStyle.Flex;
        }

        private void DecreaseClicked()
        {
            _isAdding = false;
            _quantityPanel.style.display = DisplayStyle.Flex;
            _whereFromPanel.style.display = DisplayStyle.None;
            _whereToPanel.style.display = DisplayStyle.Flex;
            _moveItemButton.style.display = DisplayStyle.Flex;
        }

        private void MoveItemClicked()
        {
            string whereFrom = "";
            string whereTo = "";
            NoPaNoSeItem itemToMove = GetItemToMove();
            if (!IsItemQuantityInputValid())
            {
                EventHandler.CallIsOneMessageOnlyEvent(true);
                EventHandler.CallOpenMessageEvent("Invalid number");
                return;
            }
            else
            {
                if (_parsedInt < 0)
                {
                    EventHandler.CallIsOneMessageOnlyEvent(true);
                    EventHandler.CallOpenMessageEvent("Negative number");
                    return;
                }
                else if (_parsedInt == 0)
                {
                    EventHandler.CallIsOneMessageOnlyEvent(true);
                    EventHandler.CallOpenMessageEvent("Zero quantity");
                    return;
                }
            }
            if (itemToMove != null)
            {
                if (!InternalDatabase.Instance.isOfflineProgram)
                {
                    if (_isAdding)
                    {
                        whereFrom = GetFromLocation();
                        whereTo = "Estoque";
                        StartCoroutine(MoveItem(itemToMove, _parsedInt, whereFrom, whereTo));

                    }
                    else
                    {
                        if (NoPaNoSeItemManager.CanChangeQuantity(itemToMove, _parsedInt))
                        {
                            whereFrom = "Estoque";
                            whereTo = GetToLocation();
                            StartCoroutine(MoveItem(itemToMove, _parsedInt, whereFrom, whereTo));
                        }
                        else
                        {
                            EventHandler.CallIsOneMessageOnlyEvent(true);
                            EventHandler.CallOpenMessageEvent("Negative quantity");
                        }
                    }
                }
                else
                {
                    MoveItemOffline(itemToMove);
                }
            }
            else
            {
                print("Null");
            }
        }

        private IEnumerator MoveItem(NoPaNoSeItem itemToChange, int quantityToMove, string whereFrom, string whereTo)
        {
            yield return _noPaNoSeItemManager.ChangeItemQuantityRoutine(itemToChange, quantityToMove, _isAdding, whereFrom, whereTo);
            if (_noPaNoSeItemManager.QuantityChanged)
            {
                yield return _noPaNoSeItemManager.MoveItem(itemToChange, quantityToMove, whereFrom, whereTo);
                yield break;
            }
        }

        private void UpdateItemQuantity(int itemNewQuantity)
        {
            _consultContainer.style.display = DisplayStyle.Flex;
            _allitems.noPaNoSeItems[_itemIndex].Quantity = itemNewQuantity;
            _consultList.Rebuild();
            _consultContainer.style.display = DisplayStyle.None;
            HideMoveElements();
        }

        private NoPaNoSeItem GetItemToMove()
        {
            for (int i = 0; i < _allitems.noPaNoSeItems.Count; i++)
            {
                if (_allitems.noPaNoSeItems[i].ItemName == _itemsNamesDP.value)
                {
                    _itemIndex = i;
                    return _allitems.noPaNoSeItems[i];
                }
            }
            return null;
        }

        private void FillItemsToMoveDP()
        {
            List<string> choices = new List<string>();
            foreach (var item in _allitems.noPaNoSeItems)
            {
                choices.Add(item.ItemName);
            }
            _itemsNamesDP.choices = choices;
            if (_allitems.noPaNoSeItems.Count > 0)
            {
                _itemsNamesDP.value = _allitems.noPaNoSeItems[0].ItemName;
            }
        }


        /// <summary>
        /// Goes to InitialScene
        /// </summary>
        private void ReturnToPreviousScreen()
        {
            ChangeScreenManager.Instance.OpenScene(Scenes.NoPaNoSeScene, Scenes.InitialScene);
        }

        private void FillDropdowns()
        {
            _fromDP.choices = InternalDatabase.locations;
            _fromDP.value = _fromDP.choices[HelperMethods.GetLocationDPValue("Estoque")];
            _toDP.choices = InternalDatabase.locations;
            _toDP.value = _toDP.choices[0];
        }

        private void HandleToDP(ChangeEvent<string> evt)
        {
            if (evt.newValue == "Outros")
            {
                _whereToTextField.style.display = DisplayStyle.Flex;
            }
            else
            {
                _whereToTextField.style.display = DisplayStyle.None;
            }
        }

        private void HandleFromDP(ChangeEvent<string> evt)
        {
            if (evt.newValue == "Outros")
            {
                _whereFromTextField.style.display = DisplayStyle.Flex;
            }
            else
            {
                _whereFromTextField.style.display = DisplayStyle.None;
            }
        }

        private string GetToLocation()
        {
            if (_toDP.value == "Outros")
            {
                return _whereToTextField.value;
            }
            else
            {
                return _toDP.value;
            }
        }

        private string GetFromLocation()
        {
            if (_fromDP.value == "Outros")
            {
                return _whereFromTextField.value;
            }
            else
            {
                return _fromDP.value;
            }
        }

        private void HideMoveElements()
        {
            _quantityTextField.value = "";
            _whereFromTextField.value = "";
            _whereToTextField.value = "";
            _moveItemButton.style.display = DisplayStyle.None;
            _quantityPanel.style.display = DisplayStyle.None;
            _whereFromPanel.style.display = DisplayStyle.None;
            _whereToPanel.style.display = DisplayStyle.None;
            _itemToMoveContainer.style.display = DisplayStyle.None;
        }

        private bool IsItemQuantityInputValid()
        {
            return int.TryParse(_quantityTextField.text, out _parsedInt);
        }

        private void MoveItemOffline(NoPaNoSeItem itemToMove)
        {
            string whereFrom = "";
            string whereTo = "";
            int newItemQuantity = itemToMove.Quantity;
            if (!_isAdding)
            {
                if (itemToMove.Quantity - _parsedInt < 0)
                {
                    EventHandler.CallIsOneMessageOnlyEvent(true);
                    EventHandler.CallOpenMessageEvent("Negative quantity");
                    return;
                }
                else
                {
                    whereFrom = "Estoque";
                    whereTo = GetToLocation();
                    newItemQuantity -= _parsedInt;
                }
            }
            else
            {
                newItemQuantity += _parsedInt;
            }

            UpdateItemQuantity(newItemQuantity);
            NoPaNoSeMovementRecords newMovementRecord = new NoPaNoSeMovementRecords();
            newMovementRecord.itemName = itemToMove.ItemName;
            newMovementRecord.quantity = _parsedInt.ToString();
            newMovementRecord.username = UsersManager.Instance.CurrentUser.GetUsername();
            newMovementRecord.date = DateTime.Now.ToString("dd/mm/yy");
            newMovementRecord.fromWhere = whereFrom;
            newMovementRecord.toWhere = whereTo;
            NoPaNoSeMovementSaver.Instance.RegisterNewNoPaNoSeMovement(newMovementRecord);
            EventHandler.CallIsOneMessageOnlyEvent(true);
            EventHandler.CallOpenMessageEvent("Item moved");
        }
    }
}