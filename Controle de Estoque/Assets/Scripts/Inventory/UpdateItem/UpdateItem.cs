using SimpleJSON;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class UpdateItem : MonoBehaviour
{
    [SerializeField] private TMP_InputField itemToUpdateParameter;
    [SerializeField] private TMP_Dropdown parameterToSearchDP;

    [SerializeField] private GameObject inputsPanel;
    [SerializeField] private GameObject messagePanel;
    [SerializeField] private TMP_Text messageText;

    [SerializeField] private ItemInformationPanelControler itemInformationPanelControler;

    [SerializeField] Button resetButton;
    [SerializeField] Button returnButton;

    private bool searchingItem = true;
    [SerializeField] private bool inputEnabled = true;

    private List<string> parameters = new List<string>();
    private ItemColumns itemToUpdate;
    private int itemToUpdateIndex;

    private bool updateInventarioSuccess = false;

    void Start()
    {
        ResetInputs();
        itemToUpdate = new ItemColumns();
        inputEnabled = true;       
    }

    /// <summary>
    /// Subscribes to MessageClosed and SetInputEnabled events
    /// </summary>
    private void OnEnable()
    {
        EventHandler.MessageClosed += MessageClosed;
        EventHandler.EnableInput += SetInputEnabled;
    }

    /// <summary>
    /// Unsubscribes to MessageClosed and SetInputEnabled events
    /// </summary>
    private void OnDisable()
    {
        EventHandler.MessageClosed -= MessageClosed;
        EventHandler.EnableInput -= SetInputEnabled;
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
                inputEnabled = false;
                if (searchingItem)
                {
                    StartCoroutine(CheckIfItemExists());
                }
                else
                {
                    StartCoroutine(UpdateDatabaseRoutine());
                }
            }
        }
    }

    /// <summary>
    /// Enables or disables the input. Called by the Event EnableInput
    /// </summary>
    private void SetInputEnabled(bool inputEnabled)
    {
              parameterToSearchDP.interactable = inputEnabled;
        resetButton.interactable = inputEnabled;
        returnButton.interactable = inputEnabled;
        itemToUpdateParameter.interactable = inputEnabled;
        this.inputEnabled = inputEnabled;
    }

    /// <summary>
    /// Check if the item that is going to be updated exists on the online database
    /// </summary>
    private IEnumerator CheckIfItemExists()
    {
        WWWForm itemForm = new WWWForm();
        itemToUpdate = new ItemColumns();
        UnityWebRequest createItemUpdatePostRequest = new UnityWebRequest();
        if (parameterToSearchDP.value == 0)
        {
            itemForm = CreateForm.GetConsultPatrimonioForm(ConstStrings.ConsultKey, itemToUpdateParameter.text);
            createItemUpdatePostRequest = CreatePostRequest.GetPostRequest(itemForm, ConstStrings.GetItemPatrimonioToUpdate, 4);
        }
        if (parameterToSearchDP.value == 1)
        {
            itemForm = CreateForm.GetConsultSerialForm(ConstStrings.ConsultKey, itemToUpdateParameter.text);
            createItemUpdatePostRequest = CreatePostRequest.GetPostRequest(itemForm, ConstStrings.GetItemSerialToUpdate, 4);
        }

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
                itemToUpdate = tempSheet.itens[0];
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

        if (itemToUpdate != null)
        {
            searchingItem = false;
            inputsPanel.SetActive(true);
            EventHandler.CallEnableInput(true);
            ShowUpdateItem();
           
        }
        else
        {
            searchingItem = true;
            EventHandler.CallOpenMessageEvent("Item a ser atualizado n�o foi encontrado");
        }
    }
    
    /// <summary>
    /// Update the item on the online database
    /// </summary>
    private IEnumerator UpdateDatabaseRoutine()
    {
        EventHandler.CallIsOneMessageOnlyEvent(false);
        #region Update inventario
        parameters.Clear();
        parameters = itemInformationPanelControler.GetInventoryValues();
        if (InternalDatabase.Instance.currentEstoque == CurrentEstoque.SnPro)
        {
            parameters.Insert(0, itemToUpdate.Aquisicao);
            parameters.Insert(1, itemToUpdate.Entrada);
        }
        else
        {
            parameters.Insert(0, itemToUpdate.Entrada);
        }  
        yield return HelperMethods.AddUpdateItem(HelperMethods.GetCategoryInt(itemToUpdate.Categoria), 4, parameters, true);
        if (HelperMethods.GetAddUpdateResponse())
        {
            updateInventarioSuccess = true;
          }
        else
        {
            updateInventarioSuccess = false;
          }

        #endregion
        #region Update the details tables
        parameters.Clear();
        parameters = itemInformationPanelControler.GetCategoryValues(itemToUpdate.Categoria);
        #endregion
        yield return HelperMethods.AddUpdateItem(HelperMethods.GetCategoryInt(itemToUpdate.Categoria), 4, parameters, false);
        if (HelperMethods.GetAddUpdateResponse())
        {
            updateInventarioSuccess = true;
        }
        else
        {
            updateInventarioSuccess = false;
        }
        if(updateInventarioSuccess)
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
        if (InternalDatabase.Instance.currentEstoque == CurrentEstoque.SnPro)
        {
            parameters.Add(itemToUpdate.Aquisicao);
        }
        parameters.Add(itemToUpdate.Entrada);
        parameters.AddRange(itemInformationPanelControler.GetInventoryValues());
        parameters.AddRange(itemInformationPanelControler.GetCategoryValues(itemToUpdate.Categoria));
        
        UpdateDatabaseItem.UpdateItem(parameters, itemToUpdateIndex);
        EventHandler.CallDatabaseUpdatedEvent();
    }

    /// <summary>
    /// Shows the itemToUpdate values on the text of all the inputs
    /// </summary>
    private void ShowUpdateItem()
    {
        ItemColumns tempItem = ConsultDatabase.Instance.ConsultPatrimonio(itemToUpdate.Patrimonio, HelperMethods.GetCategoryDatabaseToConsult(itemToUpdate.Categoria));
        if (tempItem != null)
        {
            itemToUpdate = tempItem;
            tempItem = ConsultDatabase.Instance.ConsultPatrimonio(itemToUpdate.Patrimonio, InternalDatabase.Instance.fullDatabase);
            itemToUpdateIndex = ConsultDatabase.Instance.GetItemIndex();
        }
        else
        {
            //TODO: update the internal database and try again
        }
        inputsPanel.SetActive(true);
        itemInformationPanelControler.ShowItem(itemToUpdate);
        itemInformationPanelControler.DisableInputForUpdate();
        StartCoroutine(WaitATick());
    }

    /// <summary>
    /// Wait half a second before updating the TabInputs, to allow all the inputs to be loaded
    /// </summary>
    private IEnumerator WaitATick()
    {
        yield return new WaitForSeconds(0.5f);
        EventHandler.CallUpdateTabInputs();
        itemInformationPanelControler.GetTabActiveInputs();
    }

    /// <summary>
    /// Resets the texts on all inputs
    /// </summary>
    private void ResetInputs()
    {
        inputsPanel.SetActive(true);
        if (itemInformationPanelControler == null)
        {
            itemInformationPanelControler = inputsPanel.GetComponent<ItemInformationPanelControler>();
        }
        itemInformationPanelControler.ResetItems();
        itemInformationPanelControler.ResetValues();
        inputsPanel.SetActive(false);      
    }

    /// <summary>
    /// Called by the Event MessageClosed to reset all inputs and enable input after tha message is closed
    /// </summary>
    private void MessageClosed()
    {
        searchingItem = true;
        ResetInputs();
        
        itemToUpdateParameter.text = "";
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

    /// <summary>
    /// Resets all inputs to default values and set searchingItem to true, so an item can be searched
    /// </summary>
    public void ResetUpdate()
    {
        ResetInputs();
        searchingItem = true;
    }

    public void HandleInput(int value)
    {
        if(value == 0)
        {
            if(itemToUpdateParameter.placeholder.GetComponent<TMP_Text>().text != null)
            {
                itemToUpdateParameter.placeholder.GetComponent<TMP_Text>().text = "Digite o n�mero do patrim�nio do item a ser atualizado";
            }
        }
        else if (value == 1)
        {
            if (itemToUpdateParameter.placeholder.GetComponent<TMP_Text>().text != null)
            {
                itemToUpdateParameter.placeholder.GetComponent<TMP_Text>().text = "Digite o n�mero do serial do item a ser atualizado";
            }
        }
    }
}
