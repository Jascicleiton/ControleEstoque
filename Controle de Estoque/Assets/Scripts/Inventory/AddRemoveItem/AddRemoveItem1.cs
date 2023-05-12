using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class AddRemoveItem1 : MonoBehaviour
{
    private TMP_InputField[] parameterValues;
    private TMP_Dropdown categoryDP;
    private Button resetButton;
    private Button returnButton;
    private Button addButton;
    private Button addDetailsButton;

    GameObject messagePanel;
    TMP_Text messageText;
    ItemInformationPanelControler itemInformationPanelController;
    private List<string> parameters = new List<string>();


    bool addDetalheSuccess = false;

    bool addInventarioSuccess = false;

    private void Start()
    {
        if(itemInformationPanelController == null)
        {
            itemInformationPanelController = FindObjectOfType<ItemInformationPanelControler>();
        }
        UpdateNames();
        if(UsersManager.Instance.currentUser.GetAccessLevel() < 10)
        {
            addDetailsButton.gameObject.SetActive(false);
        }
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
    /// Set the input enabled or disabled. Called using the Event EnableInput
    /// </summary>
    private void SetInputEnabled(bool inputEnabled)
    {
        for (int i = 0; i < parameterValues.Length; i++)
        {
            if (parameterValues[i].gameObject.activeInHierarchy)
            {
                parameterValues[i].interactable = inputEnabled;
            }
        }
        categoryDP.interactable = inputEnabled;
        resetButton.interactable = inputEnabled;
        returnButton.interactable = inputEnabled;
        addButton.interactable = inputEnabled;
        addDetailsButton.interactable = inputEnabled;
    }

    /// <summary>
    /// Updates all the placeholder text according to the category selected
    /// </summary>
    private void UpdateNames()
    {
        itemInformationPanelController.ShowCategoryItemTemplate(HelperMethods.GetCategoryString(categoryDP.value));
        itemInformationPanelController.DisableItemsForAdd(HelperMethods.GetCategoryString(categoryDP.value));
        EventHandler.CallUpdateTabInputs();
    }

    /// <summary>
    /// Routine used to add a new item to the online database
    /// </summary>
    private IEnumerator AddNewItemRoutine(bool addInventario)
    {

        if (addInventario)
        {
            #region Add new item to Inventario
            parameters.Clear();
            parameters = itemInformationPanelController.GetInventoryValues();
                if (HelperMethods.GetCategoryString(categoryDP.value) == ConstStrings.Outros)
            {
                parameters.Insert(5, parameterValues[5].text);
            }
            else
            {
                parameters.Insert(5, HelperMethods.GetCategoryString(categoryDP.value));
            }
            if(InternalDatabase.Instance.currentEstoque == CurrentEstoque.Concert)
            {
                parameters.Add("");
            }
            else
            {
                parameters.Insert(9, "");
            }
            
            yield return HelperMethods.AddUpdateItem(categoryDP.value, 2, parameters, true);

            if (HelperMethods.GetAddUpdateResponse())
            {
                addInventarioSuccess = true;
                // success
            }
            else
            {
                addInventarioSuccess = false;
                addDetalheSuccess = false;
                // fail
            }
            EventHandler.CallIsOneMessageOnlyEvent(false);
            #endregion
        }
        else
        {
            EventHandler.CallIsOneMessageOnlyEvent(true);
            addInventarioSuccess = true;
        }
        if (addInventarioSuccess)
        {
            parameters.Clear();
            parameters = itemInformationPanelController.GetCategoryValues(HelperMethods.GetCategoryString(categoryDP.value));

            yield return HelperMethods.AddUpdateItem(categoryDP.value, 2, parameters, false);
            if (HelperMethods.GetAddUpdateResponse())
            {
                addDetalheSuccess = true;
            }
            else
            {
                addDetalheSuccess = false;
            }
            if (addInventario)
            {
                AddItemLocal.AddItem(parameterValues, HelperMethods.GetCategoryString(categoryDP.value));
            }
        }
        if (!addInventario)
        {
            EventHandler.CallOpenMessageEvent("Worked");
            addInventarioSuccess = true;
        }
    }

    /// <summary>
    /// Close the message. Called by the Event MessageClosed
    /// </summary>
    private void MessageClosed()
    {
        UpdateNames();
        MouseManager.Instance.SetDefaultCursor();
        SetInputEnabled(true);
    }     

    /// <summary>
    /// Called when the AddItem Button is clicked
    /// </summary>
    public void AddItemClicked()
    {
        StartCoroutine(AddNewItemRoutine(true));
    }

    /// <summary>
    /// Call UpdateNames whenever a new category is selected
    /// </summary>
    public void HandleInputData(int value)
    {
        UpdateNames();
    }

    /// <summary>
    /// Goes to InitialScene
    /// </summary>
    public void ReturnToPreviousScreen()
    {
        ChangeScreenManager.Instance.OpenScene(Scenes.AddItemScene, Scenes.InitialScene);
    }

    /// <summary>
    /// Resets all inputs to default balues
    /// </summary>
    public void ResetAddItem()
    {
        UpdateNames();
    }

    /// <summary>
    /// Add an item only to the detaisl table. Only users with access level greater or equal to 10 can use this
    /// </summary>
    public void AddDetailsItem()
    {
        StartCoroutine(AddNewItemRoutine(false));
    }
    
}
