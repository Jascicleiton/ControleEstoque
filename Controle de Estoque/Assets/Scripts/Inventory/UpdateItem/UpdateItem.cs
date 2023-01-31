using SimpleJSON;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class UpdateItem : MonoBehaviour
{
    [SerializeField] private TMP_InputField itemToUpdateParameter;
    [SerializeField] private TMP_Dropdown parameterToSearchDP;

    [SerializeField] GameObject[] parameterItems;
    [SerializeField] TMP_InputField[] parameterInputs;
    [SerializeField] TMP_Text[] placeholders;
    [SerializeField] TMP_Text[] parameterNames;

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
    private ItemColumns itemToUpdateCategory;
    private int itemToUpdateIndex;
    private int itemToUpdateCategoryIndex;

    private bool updateInventarioSuccess = false;
#pragma warning disable CS0219
    private bool updateDetailsSuccess = false;
#pragma warning restore CS0219
    void Start()
    {
        ResetInputs();
        itemToUpdate = new ItemColumns();
        inputEnabled = true;
        
    }

    private void OnEnable()
    {
        EventHandler.MessageClosed += MessageClosed;
        EventHandler.EnableInput += SetInputEnabled;
    }

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

    private void SetInputEnabled(bool inputEnabled)
    {
              parameterToSearchDP.interactable = inputEnabled;
        resetButton.interactable = inputEnabled;
        returnButton.interactable = inputEnabled;
        itemToUpdateParameter.interactable = inputEnabled;
        this.inputEnabled = inputEnabled;
    }

    /// <summary>
    /// Check if the item that is going to be updated exists on the fullDatabase
    /// </summary>
    private IEnumerator CheckIfItemExists()
    {
        WWWForm itemForm = new WWWForm();
        UnityWebRequest createItemUpdatePostRequest = new UnityWebRequest();
        if (parameterToSearchDP.value == 0)
        {
            itemForm = CreateForm.GetConsultPatrimonioForm(ConstStrings.ConsultKey, itemToUpdateParameter.text);
            createItemUpdatePostRequest = HelperMethods.GetPostRequest(itemForm, "getitempatrimoniotoupdate.php", 4);
        }
        if (parameterToSearchDP.value == 1)
        {
            itemForm = CreateForm.GetConsultSerialForm(ConstStrings.ConsultKey, itemToUpdateParameter.text);
            createItemUpdatePostRequest = HelperMethods.GetPostRequest(itemForm, "getitemserialtoupdate.php", 4);
        }

        MouseManager.Instance.SetWaitingCursor(this.gameObject);
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
                foreach (JSONNode item in inventario)
                {
                    itemToUpdate.Entrada = item[0];
                    itemToUpdate.Patrimonio = item[1];
                    itemToUpdate.Status = item[2];
                    itemToUpdate.Serial = item[3];
                    itemToUpdate.Categoria = item[4];
                    itemToUpdate.Fabricante = item[5];
                    itemToUpdate.Modelo = item[6];
                    itemToUpdate.Local = item[7];
                    itemToUpdate.Saida = item[8];
                    itemToUpdate.Observacao = item[9];
                    itemToUpdate.Aquisicao = item[10];
                    
                }
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
            EventHandler.CallOpenMessageEvent("Item a ser atualizado não foi encontrado");
        }
    }
    
    private IEnumerator UpdateDatabaseRoutine()
    {
        EventHandler.CallIsOneMessageOnlyEvent(false);
        #region Update inventario
        parameters.Clear();
        parameters = itemInformationPanelControler.GetInventoryValues();
        parameters.Insert(0, itemToUpdate.Aquisicao);
        parameters.Insert(1, itemToUpdate.Entrada);
    
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
    /// Updates the full database
    /// </summary>
    private void UpdateFullDatabase()
    {
        List<string> parameters = new List<string>();
        parameters.Add(itemToUpdate.Aquisicao);
        parameters.Add(itemToUpdate.Entrada);
        for (int i = 0; i < parameterInputs.Length; i++)
        {
            if (parameterInputs[i] != null)
            {
                parameters.Add(parameterInputs[i].text);
            }
        }
        InternalDatabase.Instance.UpdateDatabase(parameters, itemToUpdateIndex);     
    }

    /// <summary>
    /// Shows the itemToUpdate values on the placeholder of all the inputs
    /// </summary>
    private void ShowUpdateItem()
    {
        ItemColumns tempItem = ConsultDatabase.Instance.ConsultSerial(itemToUpdate.Serial, InternalDatabase.Instance.fullDatabase);
        if (tempItem != null)
        {
            itemToUpdate = tempItem;
            itemToUpdateIndex = ConsultDatabase.Instance.GetItemIndex();
        }
        else
        {
            //TODO: update the internal database and try again
        }
        inputsPanel.SetActive(true);
        itemInformationPanelControler.ShowItem(tempItem);
        itemInformationPanelControler.DisableInputForUpdate();
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

    public void MessageClosed()
    {
        searchingItem = true;
        ResetInputs();
        
        itemToUpdateParameter.text = "";
        MouseManager.Instance.SetDefaultCursor();
        StartCoroutine(WaitASecond());
        }
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
        SceneManager.LoadScene(ConstStrings.SceneInitial);
    }

    public void ResetUpdate()
    {
        ResetInputs();
        searchingItem = true;
    }
}
