using System;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ItemInformationPanelControler : MonoBehaviour
{
    [SerializeField] private Image[] itemBoxes;
    [SerializeField] private TMP_Text[] parameterNames;
    /// <summary>
    /// Patrim�nio = 2, Fabricante = 6, Modelo = 7
    /// </summary>
    [SerializeField] private TMP_InputField[] parameterValues;
    [SerializeField] private TMP_Text[] parameterValuesText;
    [SerializeField] private TabInputHandler tabInputHandler;

    private int numberOfActiveBoxes = 0;

    private void Start()
    {
        if(tabInputHandler == null)
        {
            tabInputHandler = GetComponent<TabInputHandler>();
        }
        if (tabInputHandler != null)
        {
            tabInputHandler.isWithItemInformationPanelController = true;
        }
    }

    private void OnEnable()
    {
        EventHandler.EnableInput += SetInputEnabled;
    }

    private void OnDisable()
    {
        EventHandler.EnableInput -= SetInputEnabled;
    }

    /// <summary>
    /// Enable or disable input. Called by EnableInput event.
    /// </summary>
    private void SetInputEnabled(bool inputEnabled)
    {
        foreach (var item in parameterValues)
        {
            item.interactable = inputEnabled;
        }
    }

    /// <summary>
    /// Hide all item boxes that have a empty name
    /// </summary>
    private void HideEmptyItemBox()
    {
        for (int i = 0; i < parameterNames.Length; i++)
        {
            if (parameterNames[i] != null && parameterNames[i].text == "")
            {
                itemBoxes[i].gameObject.SetActive(false);
            }
        }
    }

    /// <summary>
    /// Activate all item boxes
    /// </summary>
    private void ActivateAllItemBoxes()
    {
        for (int i = 0; i < itemBoxes.Length; i++)
        {
            itemBoxes[i].gameObject.SetActive(true);
            if (parameterValues.Length > 0)
            {
                parameterValues[i].interactable = true;
            }
        }
    }

    /// <summary>
    /// Fill the names of all item boxes that should get a name
    /// </summary>
    private void FillNames(List<string> names)
    {
        for (int i = 0; i < parameterNames.Length; i++)
        {
            if (i < names.Count)
            {
                parameterNames[i].text = names[i];
            }
            else
            {
                break;
            }
        }
    }

    /// <summary>
    /// Fill the values of all item boxes that should get a value. Used when the item box have an inputField object
    /// </summary>
    private void FillValues(List<string> values)
    {
        for (int i = 0; i < parameterValues.Length; i++)
        {
            if (i < values.Count)
            {
                parameterValues[i].text = values[i];
            }
            else
            {
                break;
            }
        }
    }

    /// <summary>
    /// Fill the values of all item boxes that should get a value. Used when the item box have an text object
    /// </summary>
    private void FillValuesTexts(List<string> values)
    {
        for (int i = 0; i < parameterValuesText.Length; i++)
        {
            if (i < values.Count)
            {
                parameterValuesText[i].text = values[i];
                            }
            else
            {
                break;
            }
        }
    } 

    /// <summary>
    /// Fill the placeholders of input fields
    /// </summary>
    private void FillPlaceHolders(List<string> values)
    {
        for (int i = 0; i < parameterValuesText.Length; i++)
        {
            if (i < values.Count)
            {
                parameterValuesText[i].text = values[i] + "...";
            }
            else
            {
                break;
            }
        }
    }

    /// <summary>
    /// Change the size of the panel based on the number of active item boxes
    /// </summary>
    private void ChangeSize()
    {
        numberOfActiveBoxes = GetNumberOfActiveBoxes();
        if (numberOfActiveBoxes < 7)
        {
            GetComponent<RectTransform>().sizeDelta = new Vector2(0f, 135f);
        }
        else if (numberOfActiveBoxes < 13)
        {
            GetComponent<RectTransform>().sizeDelta = new Vector2(0f, 260f);
        }
        else if(numberOfActiveBoxes < 19)
        {
            GetComponent<RectTransform>().sizeDelta = new Vector2(0f, 380f);
        }
        else if( numberOfActiveBoxes < 25)
        {
            GetComponent<RectTransform>().sizeDelta = new Vector2(0f, 500f);
        }
        else
        {
            GetComponent<RectTransform>().sizeDelta = new Vector2(0f, 620f);
        }
    }

    /// <summary>
    /// Get the number of active boxes
    /// </summary>
    public int GetNumberOfActiveBoxes()
    {
        numberOfActiveBoxes = 0;
        for (int i = 0; i < itemBoxes.Length; i++)
        {
            if (itemBoxes[i].gameObject.activeInHierarchy)
            {
                numberOfActiveBoxes++;
            }
        }
        return numberOfActiveBoxes;
    }

    /// <summary>
    /// Reset the values of all item boxes to empty
    /// </summary>
    public void ResetValues()
    {
        if (parameterValues != null && parameterValues.Length > 0)
        {
            for (int i = 0; i < parameterValues.Length; i++)
            {
                if (parameterValues[i] != null)
                {
                    parameterValues[i].text = "";
                    parameterValues[i].interactable = true;
                }
            }
        }
        if (parameterValuesText != null && parameterValuesText.Length > 0)
        {
            for (int i = 0; i < parameterValuesText.Length; i++)
            {
                if (parameterValuesText[i] != null)
                {
                    parameterValuesText[i].text = "";
                }
            }
        }
    }

    /// <summary>
    /// Reset the names of all item boxes to ""
    /// </summary>
    private void ResetNames()
    {
        for (int i = 0; i < parameterNames.Length; i++)
        {
            if (parameterNames[i] != null)
            {
                parameterNames[i].text = "";
            }
        }
    }

    /// <summary>
    /// Resets the names and values of all item boxes
    /// </summary>
    public void ResetItems()
    {
        for (int i = 0; i < itemBoxes.Length; i++)
        {
            itemBoxes[i].gameObject.SetActive(true);
        }
        ResetNames();
        ResetValues();
        for (int i = 0; i < itemBoxes.Length; i++)
        {
            itemBoxes[i].gameObject.SetActive(false);
        }
    }

    /// <summary>
    /// Show all informations of a specific item.
    /// </summary>
    public void ShowItem(ItemColumns itemToShow)
    {
        ActivateAllItemBoxes();
        if (itemToShow != null)
        {
            Dictionary<string, List<string>> dictionary = new Dictionary<string, List<string>>();
            dictionary = HelperMethods.GetParameterValuesNamesPlaceholders(itemToShow, itemToShow.Categoria);
            List<string> names = new List<string>();
            List<string> values = new List<string>();
            List<string> placeholders = new List<string>();
            dictionary.TryGetValue("Names", out names);
            dictionary.TryGetValue("Values", out values);
            dictionary.TryGetValue("Placeholders", out placeholders);
            FillNames(names);
            FillValues(values);
            FillPlaceHolders(placeholders);
        }
        else
        {
            print("Item to show is null");
        }
        HideEmptyItemBox();

        ChangeSize();
    }

    /// <summary>
    /// Show all informations of a specific item on the consult scene
    /// </summary>
    public void ShowItemConsult(ItemColumns itemToShow)
    {
        ActivateAllItemBoxes();
        if (itemToShow != null)
        {
            Dictionary<string, List<string>> dictionary = new Dictionary<string, List<string>>();
            dictionary = HelperMethods.GetParameterValuesNamesPlaceholders(itemToShow, itemToShow.Categoria);
            List<string> names = new List<string>();
            List<string> values = new List<string>();
            dictionary.TryGetValue("Names", out names);
            dictionary.TryGetValue("Values", out values);
            FillNames(names);
            FillValuesTexts(values);
        }
        else
        {
            print("Item to show is null");
        }
        HideEmptyItemBox();
    }

    /// <summary>
    /// Show the template of an item based on it's category.
    /// </summary>
    public void ShowCategoryItemTemplate(string category)
    {
        ActivateAllItemBoxes();
        ResetNames();
        ResetValues();
        Dictionary<string, List<string>> dictionary = new Dictionary<string, List<string>>();
        dictionary = HelperMethods.GetParameterValuesNamesPlaceholders(null, category);
        List<string> names = new List<string>();
        List<string> placeholders = new List<string>();
        dictionary.TryGetValue("Names", out names);
        dictionary.TryGetValue("Placeholders", out placeholders);
        FillNames(names);
        FillPlaceHolders(placeholders);
        parameterValues[0].text = DateTime.Now.ToString("dd/MM/yyyy");
        parameterValues[1].text = DateTime.Now.ToString("dd/MM/yyyy");
       parameterValues[8].text = "Estoque";
        HideEmptyItemBox();
    }

    /// <summary>
    /// Disable input of certain item boxes on UpdateItemScene
    /// </summary>
    public void DisableInputForUpdate()
    {
        itemBoxes[0].gameObject.SetActive(false);
        itemBoxes[1].gameObject.SetActive(false);
       // parameterValues[5].interactable = false;
       // parameterValues[7].interactable = false;
       // parameterValues[8].interactable = false;
      //  parameterValues[9].interactable = false;
    }

    /// <summary>
    /// Disable the input of certain item boxes  based on the category of the item
    /// </summary>
    public void DisableItemsForAdd(string category)
    {
        if (category == ConstStrings.C_Outros)
        {
            itemBoxes[5].gameObject.SetActive(true);
        }
        else
        {
            itemBoxes[5].gameObject.SetActive(false);
        }
        switch (InternalDatabase.Instance.currentEstoque)
        {
            case CurrentEstoque.Concert:
                itemBoxes[11].gameObject.SetActive(false);
                break;
            default:
                itemBoxes[9].gameObject.SetActive(false);
                break;
        }       
    }

    /// <summary>
    /// Get all the inventory values that are/were set on the input boxes
    /// </summary>
    public List<string> GetInventoryValues()
    {
       List<string> valuesList = new List<string>();
        foreach (var item in parameterValues)
        {
            if (item.IsActive())
            {
                valuesList.Add(item.text);
            }
        }       
        return valuesList;
    }

    /// <summary>
    /// Get all the values that are specific to each category that are/were set on the input boxes
    /// </summary>
    public List<string> GetCategoryValues(string category)
    {
        List<string> valuesList = new List<string>();
        switch (category)
        {
            #region Adaptador AC
            case ConstStrings.C_AdaptadorAC:
                valuesList.Clear();
                switch (InternalDatabase.Instance.currentEstoque)
                {
                    case CurrentEstoque.SnPro:
                    case CurrentEstoque.Testing:
                                                valuesList.Add(parameterValues[7].text);
                        valuesList.Add(parameterValues[11].text);
                        valuesList.Add(parameterValues[12].text);
                        valuesList.Add(parameterValues[13].text);
                        break;
                    case CurrentEstoque.Fumsoft:                       
                    case CurrentEstoque.ESF:
                    case CurrentEstoque.Clientes:
                        valuesList.Add(parameterValues[2].text);
                        valuesList.Add(parameterValues[7].text);
                        valuesList.Add(parameterValues[11].text);
                        valuesList.Add(parameterValues[12].text);
                        break;
                    case CurrentEstoque.Concert:
                        valuesList.Add(parameterValues[2].text);
                        valuesList.Add(parameterValues[7].text);
                        valuesList.Add(parameterValues[12].text);
                        valuesList.Add(parameterValues[13].text);
                        break;
                    default:
                        break;
                }
                break;
            #endregion
            #region Carregador
            case ConstStrings.C_Carregador:
                valuesList.Clear();
                switch (InternalDatabase.Instance.currentEstoque)
                {
                    case CurrentEstoque.Fumsoft:
                    case CurrentEstoque.ESF:
                    case CurrentEstoque.Clientes:
                        valuesList.Add(parameterValues[2].text);
                        valuesList.Add(parameterValues[7].text);
                        valuesList.Add(parameterValues[11].text);
                        valuesList.Add(parameterValues[12].text);
                        break;
                    case CurrentEstoque.Concert:
                        valuesList.Add(parameterValues[2].text);
                        valuesList.Add(parameterValues[7].text);
                        valuesList.Add(parameterValues[12].text);
                        valuesList.Add(parameterValues[13].text);
                        break;
                    default:
                        valuesList.Add(parameterValues[7].text);
                        valuesList.Add(parameterValues[11].text);
                        valuesList.Add(parameterValues[12].text);
                        valuesList.Add(parameterValues[13].text);
                        break;
                }                
                break;
            #endregion
            #region Desktop
            case ConstStrings.C_Desktop:
                valuesList.Clear();
                switch (InternalDatabase.Instance.currentEstoque)
                {
                    case CurrentEstoque.Concert:
                        valuesList.Add(parameterValues[2].text);
                        valuesList.Add(parameterValues[7].text);
                        valuesList.Add(parameterValues[6].text);
                        valuesList.Add(parameterValues[12].text);
                        valuesList.Add(parameterValues[13].text);
                        valuesList.Add(parameterValues[14].text);
                        valuesList.Add(parameterValues[15].text);
                        valuesList.Add(parameterValues[16].text);
                        valuesList.Add(parameterValues[17].text);
                        break;
                    case CurrentEstoque.Fumsoft:
                    case CurrentEstoque.ESF:
                        valuesList.Add(parameterValues[2].text);
                        valuesList.Add(parameterValues[7].text);
                        valuesList.Add(parameterValues[6].text);
                        valuesList.Add(parameterValues[11].text);
                        valuesList.Add(parameterValues[12].text);
                        valuesList.Add(parameterValues[13].text);
                        valuesList.Add(parameterValues[14].text);
                        valuesList.Add(parameterValues[15].text);
                        valuesList.Add(parameterValues[16].text);
                        break;
                    default:
                        valuesList.Add(parameterValues[2].text);
                        valuesList.Add(parameterValues[11].text);
                        valuesList.Add(parameterValues[12].text);
                        valuesList.Add(parameterValues[13].text);
                        valuesList.Add(parameterValues[14].text);
                        valuesList.Add(parameterValues[15].text);
                        valuesList.Add(parameterValues[16].text);
                        valuesList.Add(parameterValues[17].text);
                        valuesList.Add(parameterValues[18].text);
                        break;
                }
                break;
            #endregion
            #region Fonte
            case ConstStrings.C_Fonte:
                valuesList.Clear();
                switch (InternalDatabase.Instance.currentEstoque)
                {
                    case CurrentEstoque.Concert:
                        valuesList.Add(parameterValues[7].text);
                        valuesList.Add(parameterValues[12].text);
                        valuesList.Add(parameterValues[13].text);
                        valuesList.Add(parameterValues[14].text);
                        break;
                    default:
                        valuesList.Add(parameterValues[7].text);
                        valuesList.Add(parameterValues[11].text);
                        valuesList.Add(parameterValues[12].text);
                        valuesList.Add(parameterValues[13].text);
                        break;
                }                
                break;
            #endregion
            #region GBIC
            case ConstStrings.C_Gbic:
                valuesList.Clear();
                switch (InternalDatabase.Instance.currentEstoque)
                {
                    case CurrentEstoque.Concert:
                        valuesList.Add(parameterValues[7].text);
                        valuesList.Add(parameterValues[6].text);
                        valuesList.Add(parameterValues[12].text);
                        break;
                    default:
                        valuesList.Add(parameterValues[7].text);
                        valuesList.Add(parameterValues[6].text);
                        valuesList.Add(parameterValues[11].text);
                        break;
                }                           
                break;
            #endregion
            #region HD
            case ConstStrings.C_HD:
                valuesList.Clear();
                valuesList.Add(parameterValues[7].text);
                valuesList.Add(parameterValues[6].text);
                valuesList.Add(parameterValues[11].text);
                valuesList.Add(parameterValues[12].text);
                valuesList.Add(parameterValues[13].text);
                valuesList.Add(parameterValues[14].text);
                valuesList.Add(parameterValues[15].text);
                valuesList.Add(parameterValues[16].text);
                valuesList.Add(parameterValues[17].text);
                break;
            #endregion
            #region iDRAC
            case ConstStrings.C_Idrac:
                valuesList.Clear();
                valuesList.Add(parameterValues[7].text);
                valuesList.Add(parameterValues[6].text);
                valuesList.Add(parameterValues[11].text);
                valuesList.Add(parameterValues[12].text);
                valuesList.Add(parameterValues[13].text);
                valuesList.Add(parameterValues[14].text);
                break;
            #endregion
            #region Mem�ria
            case ConstStrings.C_Memoria:
                valuesList.Clear();
                valuesList.Add(parameterValues[7].text);
                valuesList.Add(parameterValues[6].text);
                valuesList.Add(parameterValues[11].text);
                valuesList.Add(parameterValues[12].text);
                valuesList.Add(parameterValues[13].text);
                valuesList.Add(parameterValues[14].text);
                valuesList.Add(parameterValues[15].text);
                valuesList.Add(parameterValues[16].text);
                valuesList.Add(parameterValues[17].text);
                valuesList.Add(parameterValues[18].text);
                break;
            #endregion
            #region Monitor
            case ConstStrings.C_Monitor:
                valuesList.Clear();
                switch (InternalDatabase.Instance.currentEstoque)
                {
                    case CurrentEstoque.Concert:
                        valuesList.Add(parameterValues[2].text);
                        valuesList.Add(parameterValues[7].text);
                        valuesList.Add(parameterValues[6].text);
                        valuesList.Add(parameterValues[12].text);
                        valuesList.Add(parameterValues[13].text);
                        break;
                    case CurrentEstoque.SnPro:
                        valuesList.Add(parameterValues[7].text);
                        valuesList.Add(parameterValues[6].text);
                        valuesList.Add(parameterValues[11].text);
                        valuesList.Add(parameterValues[12].text);
                        break;
                    default:
                        valuesList.Add(parameterValues[2].text);
                        valuesList.Add(parameterValues[7].text);
                        valuesList.Add(parameterValues[6].text);
                        valuesList.Add(parameterValues[11].text);
                        valuesList.Add(parameterValues[12].text);
                        break;
                }
                break;
            #endregion
            #region Notebook
            case ConstStrings.C_Notebook:
                valuesList.Clear();
                switch (InternalDatabase.Instance.currentEstoque)
                {
                    case CurrentEstoque.Concert:
                        valuesList.Add(parameterValues[2].text);
                        valuesList.Add(parameterValues[7].text);
                        valuesList.Add(parameterValues[6].text);
                        valuesList.Add(parameterValues[12].text);
                        valuesList.Add(parameterValues[13].text);
                        valuesList.Add(parameterValues[14].text);
                        valuesList.Add(parameterValues[15].text);
                        valuesList.Add(parameterValues[16].text);
                        valuesList.Add(parameterValues[17].text);
                        break;
                    default:
                        valuesList.Add(parameterValues[2].text);
                        valuesList.Add(parameterValues[7].text);
                        valuesList.Add(parameterValues[6].text);
                        valuesList.Add(parameterValues[11].text);
                        valuesList.Add(parameterValues[12].text);
                        valuesList.Add(parameterValues[13].text);
                        valuesList.Add(parameterValues[14].text);
                        valuesList.Add(parameterValues[15].text);
                        valuesList.Add(parameterValues[16].text);
                        break;
                }
                break;
            #endregion
            #region Placa controladora
            case ConstStrings.C_PlacaControladora:
                valuesList.Clear();
                         valuesList.Add(parameterValues[7].text);
                valuesList.Add(parameterValues[11].text);
                valuesList.Add(parameterValues[12].text);
                valuesList.Add(parameterValues[13].text);
                valuesList.Add(parameterValues[14].text);
                valuesList.Add(parameterValues[15].text);
                valuesList.Add(parameterValues[16].text);
                valuesList.Add(parameterValues[17].text);
                valuesList.Add(parameterValues[18].text);
                break;
            #endregion
            #region Placa de captura de v�deo
            case ConstStrings.C_PlacaDeCapturaDeVideo:
                valuesList.Clear();
                valuesList.Add(parameterValues[7].text);
                valuesList.Add(parameterValues[11].text);
                break;
            #endregion
            #region Placa de rede
            case ConstStrings.C_PlacaDeRede:
                valuesList.Clear();
                valuesList.Add(parameterValues[7].text);
                valuesList.Add(parameterValues[6].text);
                valuesList.Add(parameterValues[11].text);
                valuesList.Add(parameterValues[12].text);
                valuesList.Add(parameterValues[13].text);
                valuesList.Add(parameterValues[14].text);
                valuesList.Add(parameterValues[15].text);
                break;
            #endregion
            #region Placa de som
            case ConstStrings.C_PlacaDeSom:
                valuesList.Clear();
                valuesList.Add(parameterValues[7].text);
                valuesList.Add(parameterValues[11].text);
                break;
            #endregion
            #region Placa de v�deo
            case ConstStrings.C_PlacaDeVideo:
                valuesList.Clear();
                valuesList.Add(parameterValues[7].text);
                valuesList.Add(parameterValues[11].text);
                valuesList.Add(parameterValues[12].text);
                break;
            #endregion
            #region Placa SAS
            case ConstStrings.C_PlacaSAS:
                valuesList.Clear();
                valuesList.Add(parameterValues[7].text);
                valuesList.Add(parameterValues[11].text);
                valuesList.Add(parameterValues[12].text);
                break;
            #endregion
            #region Processador
            case ConstStrings.C_Processador:
                valuesList.Clear();
                valuesList.Add(parameterValues[7].text);
                valuesList.Add(parameterValues[11].text);
                valuesList.Add(parameterValues[12].text);
                valuesList.Add(parameterValues[13].text);       
                break;
            #endregion
            #region Roteador
            case ConstStrings.C_Roteador:
                valuesList.Clear();
                switch (InternalDatabase.Instance.currentEstoque)
                {
                    case CurrentEstoque.Concert:
                        valuesList.Add(parameterValues[2].text);
                        valuesList.Add(parameterValues[7].text);
                        valuesList.Add(parameterValues[6].text);
                        valuesList.Add(parameterValues[12].text);
                        valuesList.Add(parameterValues[13].text);
                        break;
                    case CurrentEstoque.SnPro:
                        valuesList.Add(parameterValues[7].text);
                        valuesList.Add(parameterValues[11].text);
                        valuesList.Add(parameterValues[12].text);
                        valuesList.Add(parameterValues[13].text);
                        valuesList.Add(parameterValues[14].text);
                        break;
                    default:
                        valuesList.Add(parameterValues[2].text);
                        valuesList.Add(parameterValues[7].text);
                        valuesList.Add(parameterValues[6].text);
                        valuesList.Add(parameterValues[11].text);
                        valuesList.Add(parameterValues[12].text);
                        break;
                }
                break;
            #endregion
            #region Servidor
            case ConstStrings.C_Servidor:
                valuesList.Clear();
                switch (InternalDatabase.Instance.currentEstoque)
                {
                    case CurrentEstoque.Concert:
                        valuesList.Add(parameterValues[2].text);
                        valuesList.Add(parameterValues[7].text);
                        valuesList.Add(parameterValues[6].text);
                        valuesList.Add(parameterValues[12].text);
                        valuesList.Add(parameterValues[13].text);
                        valuesList.Add(parameterValues[14].text);
                        valuesList.Add(parameterValues[15].text);
                        valuesList.Add(parameterValues[16].text);
                        valuesList.Add(parameterValues[17].text);
                        break;
                    case CurrentEstoque.SnPro:
                        valuesList.Add(parameterValues[2].text);
                        valuesList.Add(parameterValues[7].text);
                        valuesList.Add(parameterValues[6].text);
                        valuesList.Add(parameterValues[11].text);
                        valuesList.Add(parameterValues[12].text);
                        valuesList.Add(parameterValues[13].text);
                        valuesList.Add(parameterValues[14].text);
                        valuesList.Add(parameterValues[15].text);
                        valuesList.Add(parameterValues[16].text);
                        valuesList.Add(parameterValues[17].text);
                        valuesList.Add(parameterValues[18].text);
                        valuesList.Add(parameterValues[19].text);
                        valuesList.Add(parameterValues[20].text);
                        valuesList.Add(parameterValues[21].text);
                        valuesList.Add(parameterValues[22].text);
                        valuesList.Add(parameterValues[23].text);
                        valuesList.Add(parameterValues[24].text);
                        valuesList.Add(parameterValues[25].text);
                        valuesList.Add(parameterValues[26].text);
                        break;
                    default:
                        valuesList.Add(parameterValues[2].text);
                        valuesList.Add(parameterValues[7].text);
                        valuesList.Add(parameterValues[6].text);
                        valuesList.Add(parameterValues[11].text);
                        valuesList.Add(parameterValues[12].text);
                        valuesList.Add(parameterValues[13].text);
                        valuesList.Add(parameterValues[14].text);
                        valuesList.Add(parameterValues[15].text);
                        valuesList.Add(parameterValues[16].text);
                        break;
                }
                break;
            #endregion
            #region Storage NAS
            case ConstStrings.C_StorageNAS:
                valuesList.Clear();
                valuesList.Add(parameterValues[7].text);
                valuesList.Add(parameterValues[11].text);
                valuesList.Add(parameterValues[12].text);
                valuesList.Add(parameterValues[13].text);
                valuesList.Add(parameterValues[14].text);
                valuesList.Add(parameterValues[15].text);
                break;
            #endregion
            #region Switch
            case ConstStrings.C_Switch:
                valuesList.Clear();
                switch (InternalDatabase.Instance.currentEstoque)
                {
                    case CurrentEstoque.Concert:
                        valuesList.Add(parameterValues[2].text);
                        valuesList.Add(parameterValues[7].text);
                        valuesList.Add(parameterValues[6].text);
                        valuesList.Add(parameterValues[12].text);
                                                break;
                    case CurrentEstoque.SnPro:
                        valuesList.Add(parameterValues[7].text);
                        valuesList.Add(parameterValues[11].text);
                        valuesList.Add(parameterValues[12].text);
                        break;
                    default:
                        valuesList.Add(parameterValues[2].text);
                        valuesList.Add(parameterValues[7].text);
                        valuesList.Add(parameterValues[6].text);
                        valuesList.Add(parameterValues[11].text);
                        break;
                }
                break;
            #endregion         
            default:
                break;
        }
        return valuesList;
    }

    /// <summary>
    /// Force the TabInputHandler to get the active inputs, to prevent bugs where the active inputs are incorrectly set
    /// </summary>
    public void GetTabActiveInputs()
    {
        if (tabInputHandler != null)
        {
            tabInputHandler.GetActiveInputs();
        }
    }
}
