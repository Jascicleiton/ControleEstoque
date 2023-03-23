using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
using System;
using UnityEngine.SceneManagement;
using UnityEngine.Networking;

public class MovementManager : MonoBehaviour
{
    [SerializeField] TMP_Dropdown itemInformationDP;
    [SerializeField] TMP_InputField itemInformationInput;
    [SerializeField] GameObject fromPanel;
    [SerializeField] TMP_Dropdown fromDP;
    [SerializeField] TMP_InputField fromInput;
    [SerializeField] GameObject toPanel;
    [SerializeField] TMP_Dropdown toDP;
    [SerializeField] TMP_InputField toInput;
    [SerializeField] GameObject whoPanel;
    [SerializeField] TMP_InputField whoInput;
    [SerializeField] private GameObject messagePanel;
    [SerializeField] private TMP_Text messageText;
    [SerializeField] private GameObject moveButton;

    private int itemToChangeIndex;
    private ItemColumns itemToChange;
    private bool itemFound = false;
    private bool inputEnabled = true;
    private int tempInt = 0; // used for all int.TryParse

    MovementRecords movementToRecord;

    private void Start()
    {
        itemInformationDP.value = 0;
        ShouldHidePanels(true);
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
                if(!itemFound)
                {
                    StartCoroutine(CheckIfItemExists());
                }
            }
        }
    }

    /// <summary>
    /// Checks if the item that is trying to move exists on the fulldatabase
    /// </summary>
    private IEnumerator CheckIfItemExists()
    {
        itemFound = false;
        if (itemInformationDP.value == 0)
        {
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
        }
        else if (itemInformationDP.value == 1)
        {
            itemToChange = ConsultDatabase.Instance.ConsultSerial(itemInformationInput.text, InternalDatabase.Instance.fullDatabase);
            WWWForm consultSerialForm = CreateForm.GetConsultSerialForm(ConstStrings.ConsultKey, itemInformationInput.text);

            UnityWebRequest createPostRequest = CreatePostRequest.GetPostRequest(consultSerialForm, "consultserial.php", 3);
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
        }
        yield return new WaitForSeconds(0.5f);
        MouseManager.Instance.SetDefaultCursor();
        inputEnabled = true;
        if (itemFound)
        {
            ShouldHidePanels(false);
            print(itemToChange.Local);
            fromDP.value = HelperMethods.GetLocationDPValue(itemToChange.Local);
            if (HelperMethods.GetLocationFromDP(fromDP.value) == "Outros")
            {
                fromInput.GetComponent<CanvasGroup>().alpha = 1;
                fromInput.text = itemToChange.Local;
            }
            whoInput.text = UsersManager.Instance.currentUser.GetUsername();
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
        if (itemInformationDP.value == 0)
        {
            moveItemForm = CreateForm.GetMoveItemForm(ConstStrings.MoveItemKey, itemInformationInput.text, 
            itemToChange.Serial, UsersManager.Instance.currentUser.GetUsername(), DateTime.Now.ToString("dd/MM/yyyy"), 
            GetFromLocation(), GetToLocation());
        }
        else if (itemInformationDP.value == 1)
        {
            moveItemForm = CreateForm.GetMoveItemForm(ConstStrings.MoveItemKey, itemToChange.Patrimonio.ToString(), itemInformationInput.text,
            UsersManager.Instance.currentUser.GetUsername(), DateTime.Now.ToString("dd/MM/yyyy"), GetFromLocation(), GetToLocation());
        }
     
        UnityWebRequest createPostRequest = CreatePostRequest.GetPostRequest(moveItemForm, "moveitem.php", 3);
        MouseManager.Instance.SetWaitingCursor();
        inputEnabled = false;
        yield return createPostRequest.SendWebRequest();

        if (createPostRequest.result == UnityWebRequest.Result.ConnectionError)
        {
            Debug.LogWarning("MoveItem: conectionerror");
        }
        else if (createPostRequest.result == UnityWebRequest.Result.DataProcessingError)
        {
            Debug.LogWarning("MoveItem: data processing error");
        }
        else if (createPostRequest.result == UnityWebRequest.Result.ProtocolError)
        {
            Debug.LogWarning("MoveItem: protocol error");
        }

        if (createPostRequest.error == null)
        {

            string response = createPostRequest.downloadHandler.text;
            if (response == "Database connection error" || response == "Wrong appkey")
            {
                Debug.LogWarning("MoveItem: Server error");
                // TODO: show message to user
            }
            else if (response == "Movement query failed")
            {
                Debug.LogWarning("MoveItem: Movement query failed");
                // TODO: show message to user
            }
            else if (response == "Date query failed")
            {
                Debug.LogWarning("MoveItem: Date query failed");
                // TODO: show message to user
            }
            else if (response == "Location query failed")
            {
                Debug.LogWarning("MoveItem: Location query failed");
                // TODO: show message to user
            }
            else if (response == "Item moved")
            {
                Debug.LogWarning("MoveItem: itemMoved");
                itemToChangeIndex = ConsultDatabase.Instance.GetItemIndex();
                UpdateItemToChange(itemToChange);
                UpdateDatabase();
                //ShowMessage(true);
                EventHandler.CallOpenMessageEvent("Item moved");
                createPostRequest.Dispose();
                MouseManager.Instance.SetDefaultCursor();
                inputEnabled = true;
                yield break;
            }
            else
            {
                Debug.LogWarning("MoveItem: " + response);
                // TODO: show message to user
            }
        }
        else
        {
            Debug.LogWarning(createPostRequest.error);
        }
        EventHandler.CallOpenMessageEvent("Unable to move");
        createPostRequest.Dispose();
        MouseManager.Instance.SetDefaultCursor();
        inputEnabled = true;       
    }

    /// <summary>
    /// Hide or shows the panels that hold the inputs
    /// </summary>
    private void ShouldHidePanels(bool shouldHide)
    {
        if (shouldHide)
        {
            fromPanel.GetComponent<CanvasGroup>().alpha = 0;
            fromDP.enabled = false;
            toPanel.GetComponent<CanvasGroup>().alpha = 0;
            toDP.enabled = false;
            whoPanel.GetComponent<CanvasGroup>().alpha = 0;
            whoInput.enabled = false;
        }
        else
        {
            fromPanel.GetComponent<CanvasGroup>().alpha = 1;
            fromDP.enabled = true;
            toPanel.GetComponent<CanvasGroup>().alpha = 1;
            toDP.enabled = true;
            whoPanel.GetComponent<CanvasGroup>().alpha = 1;
            whoInput.enabled = true;
        }
    }

    /// <summary>
    /// Update the item "Local", "Entrada", "Saída" and create a new MovementRecords
    /// </summary>
    private void UpdateItemToChange(ItemColumns item)
    {
        movementToRecord = new MovementRecords();
        movementToRecord.fromWhere = item.Local;
        movementToRecord.toWhere = HelperMethods.GetLocationFromDP(toDP.value);
        if (HelperMethods.GetLocationFromDP(toDP.value) != "Outros")
        {
            itemToChange.Local = HelperMethods.GetLocationFromDP(toDP.value);
        }
        else
        {
            itemToChange.Local = toInput.text;
        }
        if (itemToChange.Local == "Estoque")
        {
            itemToChange.Entrada = DateTime.Now.ToString("dd/MM/yyyy");
            itemToChange.Saida = "";
        }
        if (HelperMethods.GetLocationFromDP(fromDP.value) == "Estoque")
        {
            itemToChange.Entrada = "";
            itemToChange.Saida = DateTime.Now.ToString("dd/MM/yyyy");
        }


        movementToRecord.username = UsersManager.Instance.currentUser.GetUsername();
        movementToRecord.date = DateTime.Now.ToString("dd/MM/yyyy");
        movementToRecord.item = item;
    }

    /// <summary>
    /// Update the item on the fullDatabase, save a new MovementRecords and call DatabaseUpdatedEvent
    /// </summary>
    private void UpdateDatabase()
    {       
        InternalDatabase.Instance.fullDatabase.itens[itemToChangeIndex] = itemToChange;
       // InternalDatabase.movementRecords.Add(movementToRecord);
        EventHandler.CallDatabaseUpdatedEvent(ConstStrings.DataDatabaseSaveFile);
    }

    /// <summary>
    /// Shows a message if the item was not found or if it was moved
    /// </summary>
    private void ShowMessage(bool itemFound)
    {
        inputEnabled = false;
        messagePanel.SetActive(true);
        if (itemFound)
        {
            messageText.text = "Item movido com sucesso";
            ResetInputs();
            itemFound = false;
        }
        else
        {
            messageText.text = "Item não encontrado";
            ResetInputs();
        }
        StartCoroutine(CloseRoutine());
    }

    /// <summary>
    /// Wait a few seconds before closing the message panel
    /// </summary>
    private IEnumerator CloseRoutine()
    {
        yield return new WaitForSeconds(5f);
        CloseMessagePanel();
    }

    /// <summary>
    /// Resets all the input texts
    /// </summary>
    private void ResetInputs()
    {
        itemInformationInput.text = "";
        fromDP.value = 0;
        toDP.value = 0;
        whoInput.text = "";
        fromInput.text = "";
        toInput.text = "";
        ShouldHidePanels(true);
    }

    /// <summary>
    /// Get the location from the inputField if the "From" field is "Outros"
    /// </summary>
    private string GetFromLocation()
    {
        string location = "";
        if (HelperMethods.GetLocationFromDP(fromDP.value) == "Outros")
        {
            location = itemToChange.Local;
        }
        else
        {
            location = HelperMethods.GetLocationFromDP(fromDP.value);
        }

            return location;
    }

    /// <summary>
    /// Get the location from the inputField if the "To" field is "Outros"
    /// </summary>
    private string GetToLocation()
    {
        string location = "";
        if (HelperMethods.GetLocationFromDP(toDP.value) == "Outros")
        {
            location = toInput.text;
        }
        else
        {
            location = HelperMethods.GetLocationFromDP(toDP.value);
        }

        return location;
    }

    /// <summary>
    /// Enable or disable the MoveButton if a item was found or not
    /// </summary>
    private void EnableDisableMoveButton()
    {
        moveButton.SetActive(itemFound);
    }

    /// <summary>
    /// Close the message panel. It is public to be used on the button too
    /// </summary>
    public void CloseMessagePanel()
    {
        itemFound = false;
        EnableDisableMoveButton();
        messagePanel.SetActive(false);
        StartCoroutine(WaitASecond());
    }

    private IEnumerator WaitASecond()
    {
        yield return new WaitForSecondsRealtime(1f);
        inputEnabled = true;
    }

    /// <summary>
    /// Changes the text shown on itemInformationInput based on the selection of the itemInformationDP
    /// </summary>
    public void HandleInputData(int value)
    {

        if (value == 0)
        {
            itemInformationInput.placeholder.GetComponent<TextMeshProUGUI>().text = "Patrimônio";
        }
        if (value == 1)
        {
            itemInformationInput.placeholder.GetComponent<TextMeshProUGUI>().text = "Serial";
        }
    }

    /// <summary>
    /// Returns to InitialScene
    /// </summary>
    public void ReturnToPreviousScreen()
    {
        SceneManager.LoadScene(ConstStrings.SceneInitial);
    }

    /// <summary>
    /// Resets all inputs to default values
    /// </summary>
    public void ResetMovement()
    {
        inputEnabled = true;
        itemFound = false;
        ResetInputs();
    }

    /// <summary>
    /// Show a input for "To" field if the location is "Outros"
    /// </summary>    
    public void ShowHideToLocationInput(int value)
    {
        if(value == HelperMethods.GetLocationDPValue("Outros"))
        {
            toInput.GetComponent<CanvasGroup>().alpha = 1;
        }
        else
        {
            toInput.GetComponent<CanvasGroup>().alpha = 0;
        }
    }

    /// <summary>
    /// Show a input for "From" field if the location is "Outros"
    /// </summary>    
    public void ShowHideFromLocationInput(int value)
    {
        if (value == HelperMethods.GetLocationDPValue("Outros"))
        {
            fromInput.GetComponent<CanvasGroup>().alpha = 1;
        }
        else
        {
            fromInput.GetComponent<CanvasGroup>().alpha = 0;
        }
    }

    /// <summary>
    /// Start the move Coroutine when the MoveItem button is clicked
    /// </summary>
    public void MoveItemClicked()
    {
        EventHandler.CallIsOneMessageOnlyEvent(true);
        if (GetFromLocation() == GetToLocation())
        {
            EventHandler.CallOpenMessageEvent("Duplicate locations");
        }
        else if (GetToLocation() == "" || GetFromLocation() == "")
        {
            EventHandler.CallOpenMessageEvent("Empty location");
        }
        else
        {
            StartCoroutine(MoveItem());
        }
    }
}