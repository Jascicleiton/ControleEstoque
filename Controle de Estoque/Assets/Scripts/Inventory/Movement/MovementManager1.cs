using System.Collections;
using UnityEngine;
using UnityEngine.UIElements;
using UnityEngine.Networking;
using System;
using System.IO;
using Saving;
using System.Collections.Generic;
using System.Linq;

public class MovementManager1 : MonoBehaviour
{
    private VisualElement _root;
    private TextField _itemInformationInput;
    private VisualElement _fromPanel;
    private DropdownField _fromDP;
    private TextField _fromInput;
    private VisualElement _toPanel;
    private DropdownField _toDP;
    private TextField _toInput;
    private VisualElement _whoPanel;
    private Label _whoLabel;
    private Button _resetButton;
    private Button _moveButton;
    private Button _returnButton;
    private VisualElement _itemToMoveDetailsContainer;
    private VisualTreeAsset _itemToMoveDetailsPanel;

    private int _itemToChangeIndex;
    private ItemColumns _itemToChange;
    private bool _itemFound = false;
    private bool _inputEnabled = true;
    private int _tempInt = 0; // used for all int.TryParse
    private bool _fromInputEnabled = false;
    private bool _toInputEnabled = false;

    MovementRecords _movementToRecord;

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
        if (_inputEnabled)
        {
            if (Input.GetKeyDown(KeyCode.Return) || Input.GetKeyDown(KeyCode.KeypadEnter))
            {
                if (!_itemFound)
                {                   
                        StartCoroutine(CheckIfItemExists());                   
                }
            }
        }
    }

    private void GetUIReferences()
    {
        _root = GetComponent<UIDocument>().rootVisualElement;
        _itemInformationInput = _root.Q<TextField>("ParameterTextField");
        _fromPanel = _root.Q<VisualElement>("FromContainer");
        _fromDP = _root.Q<DropdownField>("FromDP");
        _fromInput = _root.Q<TextField>("FromTextField");
        _toPanel = _root.Q<VisualElement>("ToContainer");
        _toDP = _root.Q<DropdownField>("ToDP");
        _toInput = _root.Q<TextField>("ToTextField");
        _whoPanel = _root.Q<VisualElement>("WhoContainer");
        _whoLabel = _root.Q<Label>("WhoLabel");
        _resetButton = _root.Q<Button>("ResetButton");
        _moveButton = _root.Q<Button>("MoveButton");
        _returnButton = _root.Q<Button>("ReturnButton");
        _itemToMoveDetailsPanel = Resources.Load<VisualTreeAsset>("Templates/ResultPanel");
        _itemToMoveDetailsContainer = _root.Q<VisualElement>("ItemDetailsPanel");
        SubscribeToEvents();
        FillDropDowns();
    }

    private void SubscribeToEvents()
    {
        _resetButton.clicked += () => { ResetMovement(); };
        _moveButton.clicked += () => { MoveItemClicked(); };
        _returnButton.clicked += () => { ReturnToPreviousScreen(); };
        _fromDP.RegisterCallback<ChangeEvent<string>>(ShowHideFromLocationInput);
        _toDP.RegisterCallback<ChangeEvent<string>>(ShowHideToLocationInput);
    }

    private void UnsubscribeToEvents()
    {
        _resetButton.clicked -= () => { ResetMovement(); };
        _moveButton.clicked -= () => { MoveItemClicked(); };
        _returnButton.clicked -= () => { ReturnToPreviousScreen(); };
        _fromDP.UnregisterCallback<ChangeEvent<string>>(ShowHideFromLocationInput);
        _toDP.UnregisterCallback<ChangeEvent<string>>(ShowHideToLocationInput);
    }

    /// <summary>
    /// Checks if the item that is trying to move exists on the online database and get it's information from the 
    /// local database
    /// </summary>
    private IEnumerator CheckIfItemExists()
    {
        _itemFound = false;

        if (int.TryParse(_itemInformationInput.text, out _tempInt))
        {
            _itemToChange = ConsultDatabase.Instance.ConsultPatrimonio(_tempInt, InternalDatabase.Instance.fullDatabase);
        }
        else
        {
            EventHandler.CallIsOneMessageOnlyEvent(true);
            EventHandler.CallOpenMessageEvent("Invalid patrimonio format");
            yield break;
        }

        if (InternalDatabase.Instance.isOfflineProgram)
        {
            if(_itemToChange != null)
            {
                _itemFound = true;
                ShouldHidePanels(false);
                if (CheckIfLocationIsRegistered(_itemToChange.Local))
                {
                    _fromDP.value = _itemToChange.Local;
                }
                else
                {
                    _fromDP.value = "Outros";
                    _fromInput.style.visibility = Visibility.Visible;
                    _fromInput.value = _itemToChange.Local;
                }
                _whoLabel.text = UsersManager.Instance.currentUser.GetUsername();
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
        WWWForm consultPatrimonioForm = CreateForm.GetConsultPatrimonioForm(ConstStrings.ConsultKey, _itemInformationInput.text);

        UnityWebRequest createPostRequest = CreatePostRequest.GetPostRequest(consultPatrimonioForm, "consultpatrimonio.php", 3);

        MouseManager.Instance.SetWaitingCursor();
        _inputEnabled = false;
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
                _itemFound = true;
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
        _inputEnabled = true;
        if (_itemFound)
        {
            ShouldHidePanels(false);
            if (CheckIfLocationIsRegistered(_itemToChange.Local))
            {
                _fromDP.value = _itemToChange.Local;
            }
            else
            {
                _fromDP.value = "Outros";
                _fromInput.style.visibility = Visibility.Visible;
                _fromInput.value = _itemToChange.Local;
            }
            _whoLabel.text = UsersManager.Instance.currentUser.GetUsername();
            ShowItemDetails();
        }
        else
        {
            _itemFound = false;
            EventHandler.CallIsOneMessageOnlyEvent(true);
            EventHandler.CallOpenMessageEvent("Item not found");
            //ShowMessage(itemFound);
        }
    }

    private void ShowItemDetails()
    {
        DeleteItemDetails();
        Dictionary<string, List<string>> dictionary = HelperMethods.GetParameterValuesNamesPlaceholders(_itemToChange, _itemToChange.Categoria);
        List<string> names = new List<string>();
        List<string> values = new List<string>();
        dictionary.TryGetValue("Names", out names);
        dictionary.TryGetValue("Values", out values);
        var itemPanel = _itemToMoveDetailsPanel.Instantiate();
        _itemToMoveDetailsContainer.Add(itemPanel);
        List<VisualElement> itemBoxes = itemPanel.Q<VisualElement>("Results").Children().ToList();
        List<Label> parameterNamesLabels = itemPanel.Query<Label>(name: "ParameterName").ToList();
        List<Label> parameterValuesLabels = itemPanel.Query<Label>(name: "ParameterValue").ToList();
        Label patrimonioLabel = itemPanel.Q<Label>("PatrimonioLabel");
        patrimonioLabel.text = _itemToChange.Patrimonio.ToString();
        for (int i = 0; i < parameterNamesLabels.Count; i++)
        {
            if(i < names.Count)
            {
                parameterNamesLabels[i].text = names[i];
            }
            else
            {
                break;
            }
        }

        for (int i = 0; i < parameterValuesLabels.Count; i++)
        {
            if (i < values.Count)
            {
                parameterValuesLabels[i].text = values[i];
            }
            else
            {
                break;
            }
        }

        for (int i = 0; i < parameterNamesLabels.Count; i++)
        {
            if (parameterNamesLabels[i] != null && parameterNamesLabels[i].text == "")
            {
                itemBoxes[i].style.display = DisplayStyle.None;
            }
        }
    }

    private void DeleteItemDetails()
    {
        if (_itemToMoveDetailsContainer.childCount > 0)
        {           
            _itemToMoveDetailsContainer.RemoveAt(0);
        }
    }

    /// <summary>
    /// Try to change the item location
    /// </summary>
    private IEnumerator MoveItem()
    {
        EventHandler.CallIsOneMessageOnlyEvent(true);
        WWWForm moveItemForm = new WWWForm();

        moveItemForm = CreateForm.GetMoveItemForm(ConstStrings.MoveItemKey, _itemInformationInput.text,
        _itemToChange.Serial, UsersManager.Instance.currentUser.GetUsername(), DateTime.Now.ToString("dd/MM/yyyy"),
        GetFromLocation(), GetToLocation());


        UnityWebRequest createPostRequest = CreatePostRequest.GetPostRequest(moveItemForm, ConstStrings.MoveItem, 3);
        MouseManager.Instance.SetWaitingCursor();
        _inputEnabled = false;
        yield return createPostRequest.SendWebRequest();
        if (HandlePostRequestResponse.HandleWebRequest(createPostRequest))
        {
           // Debug.LogWarning("MoveItem: itemMoved");
            _itemToChangeIndex = ConsultDatabase.Instance.GetItemIndex();
            UpdateItemToChange();
            UpdateDatabase();
            createPostRequest.Dispose();
            MouseManager.Instance.SetDefaultCursor();
            _itemFound = false;
            ResetMovement();
            EventHandler.CallChangeAnimation("HelpMovement");
        }
        else
        {
            EventHandler.CallOpenMessageEvent("Unable to move");
            createPostRequest.Dispose();
        }
        MouseManager.Instance.SetDefaultCursor();
        _inputEnabled = true;
    }

    private void MoveItemOffLine()
    {
        _itemToChangeIndex = ConsultDatabase.Instance.GetItemIndex();
        UpdateItemToChange();
        UpdateDatabase();
        ResetInputs();
        _itemFound = false;
        MovementRecords newMovement = new MovementRecords();
        newMovement.item = _itemToChange;
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
            _fromPanel.style.visibility = Visibility.Hidden;
            _toPanel.style.visibility = Visibility.Hidden;
            _whoPanel.style.visibility = Visibility.Hidden;
            _fromInput.style.visibility = Visibility.Hidden;
            _fromInput.style.visibility = Visibility.Hidden;
        }
        else
        {
            _fromPanel.style.visibility = Visibility.Visible;
            _toPanel.style.visibility = Visibility.Visible;
            _whoPanel.style.visibility = Visibility.Visible;
        }
    }

    /// <summary>
    /// Update the item "Local", "Entrada", "Saída" and create a new MovementRecords
    /// </summary>
    private void UpdateItemToChange()
    {
        _movementToRecord = new MovementRecords();
        _movementToRecord.fromWhere = _itemToChange.Local;
        _movementToRecord.toWhere = _toDP.value;
        if (_toDP.value != "Outros")
        {
            _itemToChange.Local = _toDP.value;
        }
        else
        {
            _itemToChange.Local = _toInput.value;
        }
        if (_itemToChange.Local == "Estoque")
        {
            _itemToChange.Entrada = DateTime.Now.ToString("dd/MM/yyyy");
            _itemToChange.Saida = "";
        }
        if (_fromDP.value == "Estoque")
        {
            _itemToChange.Entrada = "";
            _itemToChange.Saida = DateTime.Now.ToString("dd/MM/yyyy");
        }


        _movementToRecord.username = UsersManager.Instance.currentUser.GetUsername();
        _movementToRecord.date = DateTime.Now.ToString("dd/MM/yyyy");
        _movementToRecord.item = _itemToChange;
        if(_itemToChange.Local == "Descarte")
        {
            _itemToChange.Status = "DEFEITO";
        }
    }

    /// <summary>
    /// Update the item on the fullDatabase, save a new MovementRecords and call DatabaseUpdatedEvent
    /// </summary>
    private void UpdateDatabase()
    {
        InternalDatabase.Instance.fullDatabase.itens[_itemToChangeIndex] = _itemToChange;
        EventHandler.CallDatabaseUpdatedEvent();
    }

    /// <summary>
    /// Resets all the input texts
    /// </summary>
    private void ResetInputs()
    {
        _itemInformationInput.value = "";
        _toDP.value = "Estoque";
        _whoLabel.text = "";
        _fromInput.value = "";
        _toInput.value = "";
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
        if (_fromDP.value == "Outros")
        {
            location = _itemToChange.Local;
        }
        else
        {
            location = _fromDP.value;
        }

        return location;
    }

    /// <summary>
    /// Get the location for the "To" field
    /// </summary>
    private string GetToLocation()
    {
        string location = "";
        if (_toDP.value == "Outros")
        {
            location = _toInput.text;
        }
        else
        {
            location = _toDP.value;
        }

        return location;
    }

    /// <summary>
    /// Enable or disable the MoveButton if a item was found or not
    /// </summary>
    private void EnableDisableMoveButton()
    {
        if (_moveButton.style.visibility == Visibility.Visible)
        {
            _moveButton.style.visibility = Visibility.Hidden;
            _moveButton.pickingMode = PickingMode.Ignore;
        }
        else
        {
            _moveButton.style.visibility = Visibility.Visible;
            _moveButton.pickingMode = PickingMode.Position;
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
        DeleteItemDetails();
        _inputEnabled = true;
        _itemFound = false;
        ResetInputs();
    }

    /// <summary>
    /// Show a input for "To" field if the location is "Outros"
    /// </summary>    
    private void ShowHideToLocationInput(ChangeEvent<string> evt)
    {
        if (evt.newValue == "Outros")
        {
            _toInput.style.visibility = Visibility.Visible;
        }
        else
        {
            _toInput.style.visibility = Visibility.Hidden;
        }
    }

    /// <summary>
    /// Show a input for "From" field if the location is "Outros"
    /// </summary>    
    private void ShowHideFromLocationInput(ChangeEvent<string> evt)
    {
        if (evt.newValue == "Outros")
        {
            _fromInput.style.visibility = Visibility.Visible;
        }
        else
        {
            _fromInput.style.visibility = Visibility.Hidden;
        }
    }

    /// <summary>
    /// Start the move Coroutine when the MoveItem button is clicked
    /// </summary>
    private void MoveItemClicked()
    {
        if (_itemFound)
        {
            EventHandler.CallIsOneMessageOnlyEvent(true);
            if (_fromInputEnabled && _toInputEnabled && (GetFromLocation() == GetToLocation()))
            {
                EventHandler.CallOpenMessageEvent("Duplicate locations");
            }
            else if ((_toInputEnabled && GetToLocation() == "") || (_fromInputEnabled && GetFromLocation() == ""))
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
        _fromDP.choices = InternalDatabase.locations;
        _toDP.choices = InternalDatabase.locations;
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