using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
using System.Linq;
using UnityEngine.UIElements;

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
    private TextField whereFromTextField;
    private TextField whereToTextField;
    private Button moveItemButton;
    #endregion

    private int itemIndex;
    private int tempInt = 0; // Used for all int.TryParse

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
        defaultColor = new Color(255, 255, 255, 255);
        inactiveColor = new Color(150, 149, 449, 255);
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
        whereFromTextField = root.Q<TextField>("WhereFromTextField");
        whereToTextField = root.Q<TextField>("WhereToTextField");
        moveItemButton = root.Q<Button>("MoveItemButton");
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
        moveItemButton.clicked += () => { MoveItemClicked(); };
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
        EventHandler.NoPaNoSeItemQuantityChanged -= UpdateItemQuantity;
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
        consultList.makeItem = () => itemTemplate.Instantiate();
                
        NoPaNoSeItem newItem = new NoPaNoSeItem();
        newItem.ItemName = itemName;
        newItem.Quantity = itemQuantity;
        allitems.noPaNoSeItems.Add(newItem);
        NoPaNoSeImporter.Instance.AddNewItem(newItem);
        consultList.itemsSource.Add(newItem);
        if (!wasActive)
        {
            consultContainer.style.display = DisplayStyle.None;
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
            ve.style.marginTop = 15;
            Label itemNameLabel = ve.Q<Label>("Name");
            Label itemQuantityLabel = ve.Q<Label>("Quantity");

            itemNameLabel.text = sortedItemsToShow[i].ItemName;
            itemQuantityLabel.text = sortedItemsToShow[i].Quantity.ToString();
        };

        consultList.itemsSource = sortedItemsToShow;
        consultList.fixedItemHeight = 60;
        
        allitems.noPaNoSeItems = sortedItemsToShow;              
    }

    /// <summary>
    /// Try to add a new item to the online database and shows it on the screen
    /// </summary>
    private IEnumerator AddNewItemRoutine()
    {
        if (!int.TryParse(newItemQuantityInput.text, out tempInt))
        {
            EventHandler.CallIsOneMessageOnlyEvent(true);
            EventHandler.CallOpenMessageEvent("Invalid number format");
            yield break;
        }

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
        StartCoroutine(AddNewItemRoutine());     
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
    }

    private void MoveItemClicked()
    {
        NoPaNoSeItem itemToMove = FindItemToMove();
        if (itemToMove != null)
        {
            if (int.TryParse(quantityTextField.value, out tempInt))
            {
                if (NoPaNoSeItemManager1.CanChangeQuantity(itemToMove, tempInt))
                {
                    StartCoroutine(MoveItem(itemToMove, tempInt, whereFromTextField.value, whereToTextField.value));
                }
            }
            else
            {
                print("invalid number");
            }
        }
        else
        {
            print("Null");
        }
    }

    private IEnumerator MoveItem(NoPaNoSeItem itemToChange, int quantityToMove, string whereFrom, string whereTo)
    {
        yield return NoPaNoSeItemManager1.ChangeItemQuantityRoutine(itemToChange, quantityToMove);
        if(NoPaNoSeItemManager1.quantityChanged)
        {
            yield return NoPaNoSeItemManager1.MoveItem(itemToChange, quantityToMove, whereFrom, whereTo);
        }        
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

    private void FillItemsToMoveDP()
    {
        List<string> choices = new List<string>();
        foreach (var item in allitems.noPaNoSeItems)
        {
            choices.Add(item.ItemName);
        }
        itemsNamesDP.choices = choices;
        itemsNamesDP.value = choices[0];
    }

    private void ItemToMoveChosen(ChangeEvent<string> evt)
    {
        itemToMoveContainer.style.display = DisplayStyle.Flex;
        itemNameToMoveLabel.text = evt.newValue;
    }

    /// <summary>
    /// Goes to InitialScene
    /// </summary>
    private void ReturnToPreviousScreen()
    {
        ChangeScreenManager.Instance.OpenScene(Scenes.NoPaNoSeScene, Scenes.InitialScene);
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

    private void UpdateItemQuantity(int itemNewQuantity)
    {
        consultContainer.style.display = DisplayStyle.Flex;
        NoPaNoSeItem itemToChange = (NoPaNoSeItem)consultList.itemsSource[itemIndex];
        itemToChange.Quantity = itemNewQuantity;
        consultList.Rebuild();
        consultContainer.style.display = DisplayStyle.None;
        quantityTextField.value = "";
        whereFromTextField.value = "";
        whereToTextField.value = "";
        itemToMoveContainer.style.display = DisplayStyle.None;
    }
}