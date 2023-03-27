using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using TMPro;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class AddRemoveItem : MonoBehaviour
{
    [SerializeField] private TMP_InputField[] parameterValues;
    [SerializeField] private TMP_Dropdown categoryDP;
    [SerializeField] private Button resetButton;
    [SerializeField] private Button returnButton;
    [SerializeField] private Button addButton;
    [SerializeField] private Button addDetailsButton;

    [SerializeField] GameObject messagePanel;
    [SerializeField] TMP_Text messageText;
    [SerializeField] ItemInformationPanelControler itemInformationPanelController;
    private List<string> parameters = new List<string>();

    private void Start()
    {
        if(itemInformationPanelController == null)
        {
            itemInformationPanelController = FindObjectOfType<ItemInformationPanelControler>();
        }
        UpdateNames();
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
#pragma warning disable CS0219 // Variable is assigned but its value is never used
        bool addDetalheSuccess = false;
#pragma warning restore CS0219 // Variable is assigned but its value is never used
        bool addInventarioSuccess = false;
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

    // MAYBE will be implemented
    public void RemoveItem()
    {

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
        SceneManager.LoadScene(ConstStrings.SceneInitial);
    }

    /// <summary>
    /// Resets all inputs to default balues
    /// </summary>
    public void ResetAddItem()
    {
        UpdateNames();
    }

    #region TEsting
    public void AddDetailsItem()
    {
        StartCoroutine(AddNewItemRoutine(false));
    }
    #endregion
}
