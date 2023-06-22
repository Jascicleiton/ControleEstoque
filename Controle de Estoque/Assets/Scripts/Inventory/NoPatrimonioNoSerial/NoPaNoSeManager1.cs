using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
using System.Linq;
using UnityEngine.UIElements;
using System.Xml.Linq;
using System;

public class NoPaNoSeManager1 : Singleton<NoPaNoSeManager>
{
    private NoPaNoSeAll allitems = new NoPaNoSeAll();
    private VisualElement root;
    private VisualTreeAsset itemTemplate;
    [HideInInspector] public bool inputEnabled = true;
    private Button openAddNewItemButton;
    private Button consultButton;
    private Button movementButton;
    private Button returnButton;
    private Button increaseButton;
    private Button decreaseButton;
    private Color defaultColor;
    private Color inactiveColor;

    #region New item
    private VisualElement newItemPanel;
    private TextField newItemNameInput;
    private TextField newItemQuantityInput;
    private Button addNewItemButton;
    #endregion

    #region Consult
    private VisualElement consultContainer;
    private ListView consultList;
    #endregion

    #region Movement
    private VisualElement movementContainer;
    private DropdownField itemsNamesDP;
    private VisualElement itemToMoveContainer;
    private Label itemNameToMoveLabel;
    private TextField quantityTextField;
    private VisualElement whereFromPanel;
    private DropdownField fromDP;
    private TextField whereFromTextField;
    private VisualElement whereToPanel;
    private DropdownField toDP;
    private TextField whereToTextField;
    private Button moveItemButton;
    private VisualElement quantityPanel;
    #endregion

    private int itemIndex;
    private int tempInt = 0; // Used for all int.TryParse
    private bool isAdding = false; // if false, the item is being removed from "Estoque", if true the item is beign added to "Estoque"

    private void OnEnable()
    {
        GetUIReferences();
    }

    private void OnDisable()
    {
        UnsubscribeToEvents();
    }

    /// <summary>
    /// Called when NoPaNoSeScene is loaded to initialize some values and update the UI
    /// </summary>
    private void Initialize()
    {
        defaultColor = new Color(1f, 1f, 1f, 1f);
        inactiveColor = new Color(0.6f, 0.6f, 0.6f, 1f);
        allitems = new NoPaNoSeAll();
        switch (UsersManager.Instance.currentUser.GetAccessLevel())
        {
            case 1:
            case 2:
            case 4:
                addNewItemButton.style.display = DisplayStyle.None;
                break;
            default:
                break;
        }
        ShowItems(NoPaNoSeImporter.Instance.itemsList.noPaNoSeItems);
        FillDropdowns();
    }

    private void GetUIReferences()
    {
        root = GetComponent<UIDocument>().rootVisualElement;
        openAddNewItemButton = root.Q<Button>("OpenAddNewItemButton");
        returnButton = root.Q<Button>("ReturnButton");
        itemTemplate = Resources.Load<VisualTreeAsset>("Templates/NoPaNoSeConsultItem");

        newItemPanel = root.Q<VisualElement>("NewItemContainer");
        newItemNameInput = root.Q<TextField>("NewItemName");
        newItemQuantityInput = root.Q<TextField>("NewItemQuantity");
        addNewItemButton = root.Q<Button>("AddNewItemButton");

        consultButton = root.Q<Button>("ConsultButton");
        movementButton = root.Q<Button>("MoveButton");

        consultContainer = root.Q<VisualElement>("ConsultContainer");
        consultList = root.Q<ListView>("ConsultLV");

        movementContainer = root.Q<VisualElement>("MovementContainer");
        itemsNamesDP = root.Q<DropdownField>("ItemsNamesDP");
        itemToMoveContainer = root.Q<VisualElement>("ItemToMoveContainer");
        itemNameToMoveLabel = root.Q<Label>("ItemNameToMoveLabel");
        quantityTextField = root.Q<TextField>("QuantityTextField");
        whereFromPanel = root.Q<VisualElement>("WhereFromPanel");
        fromDP = root.Q<DropdownField>("FromDP");
        whereFromTextField = root.Q<TextField>("WhereFromTextField");
        whereToPanel = root.Q<VisualElement>("WhereToPanel");
        toDP = root.Q<DropdownField>("ToDP");
        whereToTextField = root.Q<TextField>("WhereToTextField");
        moveItemButton = root.Q<Button>("MoveItemButton");
        increaseButton = root.Q<Button>("IncreaseButton");
        decreaseButton = root.Q<Button>("DecreaseButton");
        quantityPanel = root.Q<VisualElement>("QuantityPanel");
        SubscribeToEvents();
        Initialize();
    }

    private void SubscribeToEvents()
    {
        returnButton.clicked += () => { ReturnToPreviousScreen(); };
        openAddNewItemButton.clicked += () => { OpenAddNewItemPanel(); };
        addNewItemButton.clicked += () => { AddNewItemClicked(); };
        consultButton.clicked += () => { ConsultButtonClicked(); };
        movementButton.clicked += () => { MovementButtonClicked(); };
        itemsNamesDP.RegisterCallback<ChangeEvent<string>>(ItemToMoveChosen);
        fromDP.RegisterCallback<ChangeEvent<string>>(HandleFromDP);
        toDP.RegisterCallback<ChangeEvent<string>>(HandleToDP);
        moveItemButton.clicked += () => { MoveItemClicked(); };
        increaseButton.clicked += () => { IncreaseClicked(); };
        decreaseButton.clicked += () => { DecreaseClicked(); };
        EventHandler.NoPaNoSeItemQuantityChanged += UpdateItemQuantity;
    }

    private void UnsubscribeToEvents()
    {
        returnButton.clicked -= () => { ReturnToPreviousScreen(); };
        openAddNewItemButton.clicked -= () => { OpenAddNewItemPanel(); };
        addNewItemButton.clicked -= () => { AddNewItemClicked(); };
        consultButton.clicked -= () => { ConsultButtonClicked(); };
        movementButton.clicked -= () => { MovementButtonClicked(); };
        itemsNamesDP.UnregisterCallback<ChangeEvent<string>>(ItemToMoveChosen);
        moveItemButton.clicked -= () => { MoveItemClicked(); };
        fromDP.UnregisterCallback<ChangeEvent<string>>(HandleFromDP);
        toDP.UnregisterCallback<ChangeEvent<string>>(HandleToDP);
        increaseButton.clicked -= () => { IncreaseClicked(); };
        decreaseButton.clicked -= () => { DecreaseClicked(); };
        EventHandler.NoPaNoSeItemQuantityChanged -= UpdateItemQuantity;
    }

    /// <summary>
    /// Used by AddNewItem_btn outside newItemPanel
    /// </summary>
    private void OpenAddNewItemPanel()
    {
        newItemPanel.style.display = DisplayStyle.Flex;
        newItemNameInput.value = "";
        newItemQuantityInput.value = "";
        //  EventHandler.CallUpdateTabInputs();
    }

    /// <summary>
    /// Add a new item to the screen and scrolls to the bottom of the list
    /// </summary>
    private void AddNewItem(string itemName, int itemQuantity)
    {
        bool wasActive = true;
        if (consultContainer.style.display != DisplayStyle.Flex)
        {
            wasActive = false;
            consultContainer.style.display = DisplayStyle.Flex;
        }

        NoPaNoSeItem newItem = new NoPaNoSeItem();
        newItem.ItemName = itemName;
        newItem.Quantity = itemQuantity;
        allitems.noPaNoSeItems.Add(newItem);
        NoPaNoSeImporter.Instance.AddNewItem(newItem);

        if (!wasActive)
        {
            consultContainer.style.display = DisplayStyle.None;
        }
        else
        {
            consultList.Rebuild();
        }
    }

    /// <summary>
    /// Sort Alphabetically the imported list of items and show it
    /// </summary>
    private void ShowItems(List<NoPaNoSeItem> itemsToShow)
    {
        List<NoPaNoSeItem> sortedItemsToShow = itemsToShow.OrderBy(x => x.ItemName).ToList();

        consultList.makeItem = () => itemTemplate.Instantiate();
        consultList.bindItem = (ve, i) =>
        {
           // ve.style.marginTop = 15;
            Label itemNameLabel = ve.Q<Label>("Name");
            Label itemQuantityLabel = ve.Q<Label>("Quantity");

            itemNameLabel.text = sortedItemsToShow[i].ItemName;
            itemQuantityLabel.text = sortedItemsToShow[i].Quantity.ToString();
            ve.style.height = StyleKeyword.Auto;
        };
        consultList.fixedItemHeight = 80;
        consultList.itemsSource = sortedItemsToShow;      

        allitems.noPaNoSeItems = sortedItemsToShow;
    }

    /// <summary>
    /// Try to add a new item to the online database and shows it on the screen
    /// </summary>
    private IEnumerator AddNewItemRoutine()
    {
        

        WWWForm itemForm = CreateForm.GetNoPaNoSeForm(ConstStrings.AddNewItemKey, newItemNameInput.text, tempInt);

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
                AddNewItem(newItemNameInput.value, int.Parse(newItemQuantityInput.value));
                EventHandler.CallDatabaseUpdatedEvent();
                newItemPanel.style.display = DisplayStyle.None;
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
        if (!int.TryParse(newItemQuantityInput.text, out tempInt))
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
            AddNewItem(newItemNameInput.value, tempInt);
            EventHandler.CallDatabaseUpdatedEvent();
            newItemPanel.style.display = DisplayStyle.None;
        }
    }

    private void ConsultButtonClicked()
    {
        movementContainer.style.display = DisplayStyle.None;
        consultContainer.style.display = DisplayStyle.Flex;
        movementButton.style.unityBackgroundImageTintColor = inactiveColor;
        consultButton.style.unityBackgroundImageTintColor = defaultColor;
    }

    private void MovementButtonClicked()
    {
        consultContainer.style.display = DisplayStyle.None;
        movementContainer.style.display = DisplayStyle.Flex;
        movementButton.style.unityBackgroundImageTintColor = defaultColor;
        consultButton.style.unityBackgroundImageTintColor = inactiveColor;
        FillItemsToMoveDP();
        HideMoveElements();
    }

    private void ItemToMoveChosen(ChangeEvent<string> evt)
    {
        itemToMoveContainer.style.display = DisplayStyle.Flex;
        //itemNameToMoveLabel.text = evt.newValue;
    }

    private void IncreaseClicked()
    {
        isAdding = true;
        quantityPanel.style.display = DisplayStyle.Flex;
        whereFromPanel.style.display = DisplayStyle.Flex;
        whereToPanel.style.display = DisplayStyle.None;
        moveItemButton.style.display = DisplayStyle.Flex;
    }

    private void DecreaseClicked()
    {
        isAdding = false;
        quantityPanel.style.display = DisplayStyle.Flex;
        whereFromPanel.style.display = DisplayStyle.None;
        whereToPanel.style.display = DisplayStyle.Flex;
        moveItemButton.style.display = DisplayStyle.Flex;
    }

    private void MoveItemClicked()
    {
        string whereFrom = "";
        string whereTo = "";
        NoPaNoSeItem itemToMove = FindItemToMove();
        if(!IsItemQuantityInputValid())
        {
            EventHandler.CallIsOneMessageOnlyEvent(true);
            EventHandler.CallOpenMessageEvent("Invalid number");
            return;
        }
        else
        {
            if (tempInt < 0)
            {
                EventHandler.CallIsOneMessageOnlyEvent(true);
                EventHandler.CallOpenMessageEvent("Negative number");
                return;
            }
            else if (tempInt == 0)
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
                if (isAdding)
                {
                    whereFrom = GetFromLocation();
                    whereTo = "Estoque";
                    StartCoroutine(MoveItem(itemToMove, tempInt, whereFrom, whereTo));

                }
                else
                {
                    if (NoPaNoSeItemManager1.CanChangeQuantity(itemToMove, tempInt))
                    {
                        whereFrom = "Estoque";
                        whereTo = GetToLocation();
                        StartCoroutine(MoveItem(itemToMove, tempInt, whereFrom, whereTo));
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
        yield return NoPaNoSeItemManager1.ChangeItemQuantityRoutine(itemToChange, quantityToMove, isAdding, whereFrom, whereTo);
        if (NoPaNoSeItemManager1.quantityChanged)
        {
            yield return NoPaNoSeItemManager1.MoveItem(itemToChange, quantityToMove, whereFrom, whereTo);
            yield break;
        }
    }

    private void UpdateItemQuantity(int itemNewQuantity)
    {
        consultContainer.style.display = DisplayStyle.Flex;
        allitems.noPaNoSeItems[itemIndex].Quantity = itemNewQuantity;
        consultList.Rebuild();
        consultContainer.style.display = DisplayStyle.None;
        HideMoveElements();
    }

    private NoPaNoSeItem FindItemToMove()
    {
        for (int i = 0; i < allitems.noPaNoSeItems.Count; i++)
        {
            if (allitems.noPaNoSeItems[i].ItemName == itemsNamesDP.value)
            {
                itemIndex = i;
                return allitems.noPaNoSeItems[i];
            }
        }
        return null;
    }

    private void FillItemsToMoveDP()
    {
        List<string> choices = new List<string>();
        foreach (var item in allitems.noPaNoSeItems)
        {
            choices.Add(item.ItemName);
        }
        itemsNamesDP.choices = choices;
        itemsNamesDP.value = allitems.noPaNoSeItems[0].ItemName;
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
        fromDP.choices = InternalDatabase.locations;
        fromDP.value = fromDP.choices[HelperMethods.GetLocationDPValue("Estoque")];
        toDP.choices = InternalDatabase.locations;
        toDP.value = toDP.choices[0];
    }

    private void HandleToDP(ChangeEvent<string> evt)
    {
        if (evt.newValue == "Outros")
        {
            whereToTextField.style.display = DisplayStyle.Flex;
        }
        else
        {
            whereToTextField.style.display = DisplayStyle.None;
        }
    }

    private void HandleFromDP(ChangeEvent<string> evt)
    {
        if (evt.newValue == "Outros")
        {
            whereFromTextField.style.display = DisplayStyle.Flex;
        }
        else
        {
            whereFromTextField.style.display = DisplayStyle.None;
        }
    }

    private string GetToLocation()
    {
        if (toDP.value == "Outros")
        {
            return whereToTextField.value;
        }
        else
        {
            return toDP.value;
        }
    }

    private string GetFromLocation()
    {
        if (fromDP.value == "Outros")
        {
            return whereFromTextField.value;
        }
        else
        {
            return fromDP.value;
        }
    }

    private void HideMoveElements()
    {
        quantityTextField.value = "";
        whereFromTextField.value = "";
        whereToTextField.value = "";
        moveItemButton.style.display = DisplayStyle.None;
        quantityPanel.style.display = DisplayStyle.None;
        whereFromPanel.style.display = DisplayStyle.None;
        whereToPanel.style.display = DisplayStyle.None;
        itemToMoveContainer.style.display = DisplayStyle.None;
    }

    private bool IsItemQuantityInputValid()
    {
        return int.TryParse(quantityTextField.text, out tempInt);
    }

    private void MoveItemOffline(NoPaNoSeItem itemToMove)
    {
        string whereFrom = "";
        string whereTo = "";
        int newItemQuantity = itemToMove.Quantity;
        if (!isAdding)
        {
            if((itemToMove.Quantity - tempInt) < 0)
            {
                EventHandler.CallIsOneMessageOnlyEvent(true);
                EventHandler.CallOpenMessageEvent("Negative quantity");
                return;
            }
            else
            {
                whereFrom = "Estoque";
                whereTo = GetToLocation();
                newItemQuantity -= tempInt;
            }
        }
        else
        {
            newItemQuantity += tempInt; 
        }

        UpdateItemQuantity(newItemQuantity);
        NoPaNoSeMovementRecords newMovementRecord = new NoPaNoSeMovementRecords();
        newMovementRecord.itemName = itemToMove.ItemName;
        newMovementRecord.quantity = tempInt.ToString();
        newMovementRecord.username = UsersManager.Instance.currentUser.GetUsername();
        newMovementRecord.date = DateTime.Now.ToString("dd/mm/yy");
        newMovementRecord.fromWhere = whereFrom;
        newMovementRecord.toWhere = whereTo;
        NoPaNoSeMovementSaver.Instance.RegisterNewNoPaNoSeMovement(newMovementRecord);
        EventHandler.CallIsOneMessageOnlyEvent(true);
        EventHandler.CallOpenMessageEvent("Item moved");
    }
}