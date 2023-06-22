using System.Collections;
using UnityEngine;
using UnityEngine.UIElements;
using UnityEngine.Networking;
using System;
using System.IO;
using Saving;

public class MovementManager1 : MonoBehaviour
{
    private VisualElement root;
    private TextField itemInformationInput;
    private VisualElement fromPanel;
    private DropdownField fromDP;
    private TextField fromInput;
    private VisualElement toPanel;
    private DropdownField toDP;
    private TextField toInput;
    private VisualElement whoPanel;
    private Label whoLabel;
    private Button resetButton;
    private Button moveButton;
    private Button returnButton;

    private int itemToChangeIndex;
    private ItemColumns itemToChange;
    private bool itemFound = false;
    private bool inputEnabled = true;
    private int tempInt = 0; // used for all int.TryParse
    private bool fromInputEnabled = false;
    private bool toInputEnabled = false;

    MovementRecords movementToRecord;

    private void OnEnable()
    {
        GetUIReferences();
        ShouldHidePanels(true);
    }

    private void OnDisable()
    {
        UnsubscribeToEvents();
    }

    /// <summary>
    /// Handles what happens if Enter is pressed
    /// </summary>
    private void Update()
    {
        if (inputEnabled)
        {
            if (Input.GetKeyDown(KeyCode.Return) || Input.GetKeyDown(KeyCode.KeypadEnter))
            {
                if (!itemFound)
                {                   
                        StartCoroutine(CheckIfItemExists());                   
                }
            }
        }
    }

    private void GetUIReferences()
    {
        root = GetComponent<UIDocument>().rootVisualElement;
        itemInformationInput = root.Q<TextField>("ParameterTextField");
        fromPanel = root.Q<VisualElement>("FromContainer");
        fromDP = root.Q<DropdownField>("FromDP");
        fromInput = root.Q<TextField>("FromTextField");
        toPanel = root.Q<VisualElement>("ToContainer");
        toDP = root.Q<DropdownField>("ToDP");
        toInput = root.Q<TextField>("ToTextField");
        whoPanel = root.Q<VisualElement>("WhoContainer");
        whoLabel = root.Q<Label>("WhoLabel");
        resetButton = root.Q<Button>("ResetButton");
        moveButton = root.Q<Button>("MoveButton");
        returnButton = root.Q<Button>("ReturnButton");
        SubscribeToEvents();
        FillDropDowns();
    }

    private void SubscribeToEvents()
    {
        resetButton.clicked += () => { ResetMovement(); };
        moveButton.clicked += () => { MoveItemClicked(); };
        returnButton.clicked += () => { ReturnToPreviousScreen(); };
        fromDP.RegisterCallback<ChangeEvent<string>>(ShowHideFromLocationInput);
        toDP.RegisterCallback<ChangeEvent<string>>(ShowHideToLocationInput);
    }

    private void UnsubscribeToEvents()
    {
        resetButton.clicked -= () => { ResetMovement(); };
        moveButton.clicked -= () => { MoveItemClicked(); };
        returnButton.clicked -= () => { ReturnToPreviousScreen(); };
        fromDP.UnregisterCallback<ChangeEvent<string>>(ShowHideFromLocationInput);
        toDP.UnregisterCallback<ChangeEvent<string>>(ShowHideToLocationInput);
    }

    /// <summary>
    /// Checks if the item that is trying to move exists on the online database and get it's information from the 
    /// local database
    /// </summary>
    private IEnumerator CheckIfItemExists()
    {
        itemFound = false;

        if (int.TryParse(itemInformationInput.text, out tempInt))
        {
            itemToChange = ConsultDatabase.Instance.ConsultPatrimonio(tempInt, InternalDatabase.Instance.fullDatabase);
        }
        else
        {
            EventHandler.CallIsOneMessageOnlyEvent(true);
            EventHandler.CallOpenMessageEvent("Invalid patrimonio format");
            yield break;
        }

        if (InternalDatabase.Instance.isOfflineProgram)
        {
            if(itemToChange != null)
            {
                itemFound = true;
                ShouldHidePanels(false);
                if (CheckIfLocationIsRegistered(itemToChange.Local))
                {
                    fromDP.value = itemToChange.Local;
                }
                else
                {
                    fromDP.value = "Outros";
                    fromInput.style.visibility = Visibility.Visible;
                    fromInput.value = itemToChange.Local;
                }
                whoLabel.text = UsersManager.Instance.currentUser.GetUsername();
                EnableDisableMoveButton();
                EventHandler.CallChangeAnimation("2");
            }
            else
            {
                EventHandler.CallIsOneMessageOnlyEvent(true);
                EventHandler.CallOpenMessageEvent("Invalid patrimonio format");               
            }
            yield break;
        }
        WWWForm consultPatrimonioForm = CreateForm.GetConsultPatrimonioForm(ConstStrings.ConsultKey, itemInformationInput.text);

        UnityWebRequest createPostRequest = CreatePostRequest.GetPostRequest(consultPatrimonioForm, "consultpatrimonio.php", 3);

        MouseManager.Instance.SetWaitingCursor();
        inputEnabled = false;
        yield return createPostRequest.SendWebRequest();

        if (createPostRequest.result == UnityWebRequest.Result.ConnectionError)
        {
            Debug.LogWarning("CheckIfItemExists: conectionerror");
        }
        else if (createPostRequest.result == UnityWebRequest.Result.DataProcessingError)
        {
            Debug.LogWarning("CheckIfItemExists: data processing error");
        }
        else if (createPostRequest.result == UnityWebRequest.Result.ProtocolError)
        {
            Debug.LogWarning("CheckIfItemExists: protocol error");
        }

        if (createPostRequest.error == null)
        {
            string response = createPostRequest.downloadHandler.text;
            if (response == "Database connection error" || response == "Query failed" || response == "Wrong appkey")
            {
                Debug.LogWarning("Server errror");
                // TODO: show message to user
            }
            else if (response == "Item found")
            {
                itemFound = true;
                EnableDisableMoveButton();
                EventHandler.CallChangeAnimation("2");
            }
            else if (response == "Not found or found duplicate")
            {
                Debug.LogWarning("Item not found or found duplicate");
            }
            else
            {
                Debug.LogWarning("CheckIfItemExists - Patrimonio: " + response);
            }
        }
        else
        {
            Debug.LogWarning("CheckIfItemExists: " + createPostRequest.error);
        }
        createPostRequest.Dispose();

        yield return new WaitForSeconds(0.5f);
        MouseManager.Instance.SetDefaultCursor();
        inputEnabled = true;
        if (itemFound)
        {
            ShouldHidePanels(false);
            if (CheckIfLocationIsRegistered(itemToChange.Local))
            {
                fromDP.value = itemToChange.Local;
            }
            else
            {
                fromDP.value = "Outros";
                fromInput.style.visibility = Visibility.Visible;
                fromInput.value = itemToChange.Local;
            }
            whoLabel.text = UsersManager.Instance.currentUser.GetUsername();
        }
        else
        {
            itemFound = false;
            EventHandler.CallIsOneMessageOnlyEvent(true);
            EventHandler.CallOpenMessageEvent("Item not found");
            //ShowMessage(itemFound);
        }
    }

    /// <summary>
    /// Try to change the item location
    /// </summary>
    private IEnumerator MoveItem()
    {
        EventHandler.CallIsOneMessageOnlyEvent(true);
        WWWForm moveItemForm = new WWWForm();

        moveItemForm = CreateForm.GetMoveItemForm(ConstStrings.MoveItemKey, itemInformationInput.text,
        itemToChange.Serial, UsersManager.Instance.currentUser.GetUsername(), DateTime.Now.ToString("dd/MM/yyyy"),
        GetFromLocation(), GetToLocation());


        UnityWebRequest createPostRequest = CreatePostRequest.GetPostRequest(moveItemForm, ConstStrings.MoveItem, 3);
        MouseManager.Instance.SetWaitingCursor();
        inputEnabled = false;
        yield return createPostRequest.SendWebRequest();
        if (HandlePostRequestResponse.HandleWebRequest(createPostRequest))
        {
           // Debug.LogWarning("MoveItem: itemMoved");
            itemToChangeIndex = ConsultDatabase.Instance.GetItemIndex();
            UpdateItemToChange();
            UpdateDatabase();
            createPostRequest.Dispose();
            MouseManager.Instance.SetDefaultCursor();
            itemFound = false;
            ResetInputs();
            EventHandler.CallChangeAnimation("HelpMovement");
        }
        else
        {
            EventHandler.CallOpenMessageEvent("Unable to move");
            createPostRequest.Dispose();
        }
        MouseManager.Instance.SetDefaultCursor();
        inputEnabled = true;
    }

    private void MoveItemOffLine()
    {
        itemToChangeIndex = ConsultDatabase.Instance.GetItemIndex();
        UpdateItemToChange();
        UpdateDatabase();
        ResetInputs();
        itemFound = false;
        MovementRecords newMovement = new MovementRecords();
        newMovement.item = itemToChange;
        newMovement.date = DateTime.Now.ToString("dd/MM/yyyy");
        newMovement.username = UsersManager.Instance.currentUser.GetUsername();
        newMovement.fromWhere = GetFromLocation();
        newMovement.toWhere = GetToLocation();
        RegularMovementSaver.Instance.RegisterNewRegularMovement(newMovement);
        EventHandler.CallIsOneMessageOnlyEvent(true);
        EventHandler.CallOpenMessageEvent("Item moved");
        SavingWrapper.Instance.Save();
    }

    /// <summary>
    /// Hide or shows the panels that hold the inputs
    /// </summary>
    private void ShouldHidePanels(bool shouldHide)
    {
        if (shouldHide)
        {
            fromPanel.style.visibility = Visibility.Hidden;
            toPanel.style.visibility = Visibility.Hidden;
            whoPanel.style.visibility = Visibility.Hidden;
            fromInput.style.visibility = Visibility.Hidden;
            fromInput.style.visibility = Visibility.Hidden;
        }
        else
        {
            fromPanel.style.visibility = Visibility.Visible;
            toPanel.style.visibility = Visibility.Visible;
            whoPanel.style.visibility = Visibility.Visible;
        }
    }

    /// <summary>
    /// Update the item "Local", "Entrada", "Saída" and create a new MovementRecords
    /// </summary>
    private void UpdateItemToChange()
    {
        movementToRecord = new MovementRecords();
        movementToRecord.fromWhere = itemToChange.Local;
        movementToRecord.toWhere = toDP.value;
        if (toDP.value != "Outros")
        {
            itemToChange.Local = toDP.value;
        }
        else
        {
            itemToChange.Local = toInput.value;
        }
        if (itemToChange.Local == "Estoque")
        {
            itemToChange.Entrada = DateTime.Now.ToString("dd/MM/yyyy");
            itemToChange.Saida = "";
        }
        if (fromDP.value == "Estoque")
        {
            itemToChange.Entrada = "";
            itemToChange.Saida = DateTime.Now.ToString("dd/MM/yyyy");
        }


        movementToRecord.username = UsersManager.Instance.currentUser.GetUsername();
        movementToRecord.date = DateTime.Now.ToString("dd/MM/yyyy");
        movementToRecord.item = itemToChange;
    }

    /// <summary>
    /// Update the item on the fullDatabase, save a new MovementRecords and call DatabaseUpdatedEvent
    /// </summary>
    private void UpdateDatabase()
    {
        InternalDatabase.Instance.fullDatabase.itens[itemToChangeIndex] = itemToChange;
        EventHandler.CallDatabaseUpdatedEvent();
    }

    /// <summary>
    /// Resets all the input texts
    /// </summary>
    private void ResetInputs()
    {
        itemInformationInput.value = "";
        toDP.value = "Estoque";
        whoLabel.text = "";
        fromInput.value = "";
        toInput.value = "";
        ShouldHidePanels(true);
        EnableDisableMoveButton();
        EventHandler.CallChangeAnimation("1");
    }

    /// <summary>
    /// Get the location for the "From" field
    /// </summary>
    private string GetFromLocation()
    {
        string location = "";
        if (fromDP.value == "Outros")
        {
            location = itemToChange.Local;
        }
        else
        {
            location = fromDP.value;
        }

        return location;
    }

    /// <summary>
    /// Get the location for the "To" field
    /// </summary>
    private string GetToLocation()
    {
        string location = "";
        if (toDP.value == "Outros")
        {
            location = toInput.text;
        }
        else
        {
            location = toDP.value;
        }

        return location;
    }

    /// <summary>
    /// Enable or disable the MoveButton if a item was found or not
    /// </summary>
    private void EnableDisableMoveButton()
    {
        if (moveButton.style.visibility == Visibility.Visible)
        {
            moveButton.style.visibility = Visibility.Hidden;
            moveButton.pickingMode = PickingMode.Ignore;
        }
        else
        {
            moveButton.style.visibility = Visibility.Visible;
            moveButton.pickingMode = PickingMode.Position;
        }

    }

    /// <summary>
    /// Goes to InitialScene
    /// </summary>
    private void ReturnToPreviousScreen()
    {
        ChangeScreenManager.Instance.OpenScene(Scenes.MovementScene, Scenes.InitialScene);
    }
    /// <summary>
    /// Resets all inputs to default values
    /// </summary>
    private void ResetMovement()
    {
        inputEnabled = true;
        itemFound = false;
        ResetInputs();
    }

    /// <summary>
    /// Show a input for "To" field if the location is "Outros"
    /// </summary>    
    private void ShowHideToLocationInput(ChangeEvent<string> evt)
    {
        if (evt.newValue == "Outros")
        {
            toInput.style.visibility = Visibility.Visible;
        }
        else
        {
            toInput.style.visibility = Visibility.Hidden;
        }
    }

    /// <summary>
    /// Show a input for "From" field if the location is "Outros"
    /// </summary>    
    private void ShowHideFromLocationInput(ChangeEvent<string> evt)
    {
        if (evt.newValue == "Outros")
        {
            fromInput.style.visibility = Visibility.Visible;
        }
        else
        {
            fromInput.style.visibility = Visibility.Hidden;
        }
    }

    /// <summary>
    /// Start the move Coroutine when the MoveItem button is clicked
    /// </summary>
    private void MoveItemClicked()
    {
        if (itemFound)
        {
            EventHandler.CallIsOneMessageOnlyEvent(true);
            if (fromInputEnabled && toInputEnabled && (GetFromLocation() == GetToLocation()))
            {
                EventHandler.CallOpenMessageEvent("Duplicate locations");
            }
            else if ((toInputEnabled && GetToLocation() == "") || (fromInputEnabled && GetFromLocation() == ""))
            {
                EventHandler.CallOpenMessageEvent("Empty location");
            }
            else
            {
                if (!InternalDatabase.Instance.isOfflineProgram)
                {
                    StartCoroutine(MoveItem());
                }
                else
                {
                    MoveItemOffLine();

                }
            }
        }
    }    

    private void FillDropDowns()
    {
        fromDP.choices = InternalDatabase.locations;
        toDP.choices = InternalDatabase.locations;
        //toDP.value = InternalDatabase.locations[HelperMethods.GetLocationDPValue("Estoque")];
    }

    private bool CheckIfLocationIsRegistered(string location)
    {
        bool isRegistered = false;
        foreach (var item in InternalDatabase.locations)
        {
            if (item == location)
            {
                isRegistered = true;
                break;
            }
        }
        return isRegistered;
    }
}