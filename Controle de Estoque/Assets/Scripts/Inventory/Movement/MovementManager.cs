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
    [SerializeField] TMP_InputField fromInput;
    [SerializeField] GameObject toPanel;
    [SerializeField] TMP_InputField toInput;
    [SerializeField] GameObject whoPanel;
    [SerializeField] TMP_InputField whoInput;
    [SerializeField] private GameObject messagePanel;
    [SerializeField] private TMP_Text messageText;

    private int itemToChangeIndex;
    private ItemColumns itemToChange;
    private bool itemFound = false;
    private bool inputEnabled = true;

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
                if (itemFound)
                {
                    StartCoroutine(MoveItem());
                }
                else
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
        //print("hi");
        if (itemInformationDP.value == 0)
        {
            itemToChange = ConsultDatabase.Instance.ConsultPatrimonio(itemInformationInput.text,InternalDatabase.Instance.fullDatabase);
            WWWForm consultPatrimonioForm = CreateForm.GetConsultPatrimonioForm(ConstStrings.ConsultKey, itemInformationInput.text);
           
            UnityWebRequest createPostRequest = HelperMethods.GetPostRequest(consultPatrimonioForm, "consultpatrimonio.php", 3);
          
            MouseManager.Instance.SetWaitingCursor(this.gameObject);
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
                    // TODO: show message to user
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

            UnityWebRequest createPostRequest = HelperMethods.GetPostRequest(consultSerialForm, "consultserial.php", 3);
            MouseManager.Instance.SetWaitingCursor(this.gameObject);
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
                    // TODO: show message to user
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
            
            fromInput.text = itemToChange.Local;
            whoInput.text = UsersManager.Instance.currentUser.username;
        }
        else
        {
            itemFound = false;
            ShowMessage(itemFound);
        }

    }

    /// <summary>
    /// Try to change the item location
    /// </summary>
    private IEnumerator MoveItem()
    {
        WWWForm moveItemForm = new WWWForm();
        if (itemInformationDP.value == 0)
        {
            moveItemForm = CreateForm.GetMoveItemForm(ConstStrings.MoveItemKey, itemInformationInput.text, itemToChange.Serial, UsersManager.Instance.currentUser.username, DateTime.Now.ToString("ddMMyyyy"), fromInput.text, toInput.text);
        }
        else if (itemInformationDP.value == 1)
        {
            moveItemForm = CreateForm.GetMoveItemForm(ConstStrings.MoveItemKey, itemToChange.Patrimonio, itemInformationInput.text, UsersManager.Instance.currentUser.username, DateTime.Now.ToString("ddMMyyyy"), fromInput.text, toInput.text);
        }

        UnityWebRequest createPostRequest = HelperMethods.GetPostRequest(moveItemForm, "moveitem.php", 3);
        MouseManager.Instance.SetWaitingCursor(this.gameObject);
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
                ShowMessage(true);
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
            fromInput.enabled = false;
            toPanel.GetComponent<CanvasGroup>().alpha = 0;
            toInput.enabled = false;
            whoPanel.GetComponent<CanvasGroup>().alpha = 0;
            whoInput.enabled = false;
        }
        else
        {
            fromPanel.GetComponent<CanvasGroup>().alpha = 1;
            fromInput.enabled = true;
            toPanel.GetComponent<CanvasGroup>().alpha = 1;
            toInput.enabled = true;
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
        movementToRecord.toWhere = toInput.text;

        itemToChange.Local = toInput.text;
        if (itemToChange.Local == "Estoque")
        {
            itemToChange.Entrada = DateTime.Now.ToString("dd/MM/yyyy");
            itemToChange.Saida = "";
        }
        if (fromInput.text == "Estoque" || fromInput.text == "estoque")
        {
            itemToChange.Entrada = "";
            itemToChange.Saida = DateTime.Now.ToString("dd/MM/yyyy");
        }


        movementToRecord.username = UsersManager.Instance.currentUser.username;
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
        fromInput.text = "";
        toInput.text = "";
        whoInput.text = "";
        ShouldHidePanels(true);
    }

    /// <summary>
    /// Close the message panel. It is public to be used on the button too
    /// </summary>
    public void CloseMessagePanel()
    {
        itemFound = false;
        messagePanel.SetActive(false);
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

    public void ResetMovement()
    {
        inputEnabled = true;
        itemFound = false;
        ResetInputs();
    }
}