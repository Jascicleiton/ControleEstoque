using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ItemInformationPanelControler : MonoBehaviour
{
    [SerializeField] private Image[] itemBoxes;
    [SerializeField] private TMP_Text[] parameterNames;
    [SerializeField] private TMP_InputField[] parameterValues;
    [SerializeField] private TMP_Text[] parameterValuesText;

    private void OnEnable()
    {
        EventHandler.EnableInput += SetInputEnabled;
    }

    private void OnDisable()
    {
        EventHandler.EnableInput -= SetInputEnabled;
    }

    private void SetInputEnabled(bool inputEnabled)
    {
        foreach (var item in parameterValues)
        {
            item.interactable = inputEnabled;
        }
    }

    private void HideEmpityItemBox()
    {
        for (int i = 0; i < parameterNames.Length; i++)
        {
            if (parameterNames[i] != null && parameterNames[i].text == "")
            {
                itemBoxes[i].gameObject.SetActive(false);
            }
        }
    }

    private void ActivateAllItemBoxes()
    {
        for (int i = 0; i < itemBoxes.Length; i++)
        {
            itemBoxes[i].gameObject.SetActive(true);
            parameterValues[i].interactable = true;
        }
    }

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

    public void ShowItem(ItemColumns itemToShow)
    {
        ActivateAllItemBoxes();
        if (itemToShow != null)
        {
            Dictionary<string, List<string>> dictionary = new Dictionary<string, List<string>>();
            dictionary = HelperMethods.GetParameterValuesAndNames(itemToShow, itemToShow.Categoria);
            List<string> names = new List<string>();
            List<string> values = new List<string>();
            dictionary.TryGetValue("Names", out names);
            dictionary.TryGetValue("Values", out values);
            FillNames(names);
            FillValues(values);
        }
        else
        {
            print("Item to show is null");
        }
        HideEmpityItemBox();
    }

    public void ShowItemConsult(ItemColumns itemToShow)
    {
        ActivateAllItemBoxes();
        if (itemToShow != null)
        {
            Dictionary<string, List<string>> dictionary = new Dictionary<string, List<string>>();
            dictionary = HelperMethods.GetParameterValuesAndNames(itemToShow, itemToShow.Categoria);
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
        HideEmpityItemBox();
    }

    public void ShowCategoryItemTemplate(string category)
    {
        ActivateAllItemBoxes();
        ResetNames();
        ResetValues();
        Dictionary<string, List<string>> dictionary = new Dictionary<string, List<string>>();
        dictionary = HelperMethods.GetParameterValuesAndNames(null, category);
        List<string> names = new List<string>();
        List<string> values = new List<string>();
        dictionary.TryGetValue("Names", out names);
        FillNames(names);

        parameterValues[0].text = DateTime.Now.ToString("dd/MM/yyyy");
        parameterValues[1].text = DateTime.Now.ToString("dd/MM/yyyy");

        HideEmpityItemBox();
    }

    public void DisableInputForUpdate()
    {
        itemBoxes[0].gameObject.SetActive(false);
        itemBoxes[1].gameObject.SetActive(false);
        parameterValues[5].interactable = false;
        parameterValues[7].interactable = false;
        parameterValues[8].interactable = false;
        parameterValues[9].interactable = false;
    }

    public void DisableItemsForAdd()
    {
        itemBoxes[5].gameObject.SetActive(false);
        itemBoxes[9].gameObject.SetActive(false);
    }

    public List<string> GetInventoryValues()
    {
       List<string> valuesList = new List<string>();
        for (int i = 0; i < 11; i++)
        {
            if (parameterValues[i].IsActive())
            {
                valuesList.Add(parameterValues[i].text);
            }
        }
        return valuesList;
    }

    public List<string> GetCategoryValues(string category)
    {
        List<string> valuesList = new List<string>();
        switch (category)
        {
            #region HD
            case ConstStrings.HD:
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
            #region Memória
            case ConstStrings.Memoria:
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
            #region Placa de rede
            case ConstStrings.PlacaDeRede:
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
            #region iDRAC
            case ConstStrings.Idrac:
                valuesList.Clear();
                valuesList.Add(parameterValues[7].text);
                valuesList.Add(parameterValues[6].text);
                valuesList.Add(parameterValues[11].text);
                valuesList.Add(parameterValues[12].text);
                valuesList.Add(parameterValues[13].text);
                valuesList.Add(parameterValues[14].text);
                break;
            #endregion
            #region Placa controladora
            case ConstStrings.PlacaControladora:
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
            #region Processador
            case ConstStrings.Processador:
                valuesList.Clear();
                valuesList.Add(parameterValues[7].text);
                valuesList.Add(parameterValues[11].text);
                valuesList.Add(parameterValues[12].text);
                valuesList.Add(parameterValues[13].text);
                valuesList.Add(parameterValues[14].text);
                valuesList.Add(parameterValues[15].text);
                valuesList.Add(parameterValues[16].text);
                break;
            #endregion
            #region Desktop
            case ConstStrings.Desktop:
                valuesList.Clear();
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
            #endregion
            #region Fonte
            case ConstStrings.Fonte:
                valuesList.Clear();
                valuesList.Add(parameterValues[7].text);
                valuesList.Add(parameterValues[11].text);
                valuesList.Add(parameterValues[12].text);
                valuesList.Add(parameterValues[13].text);
                break;
            #endregion
            #region Switch
            case ConstStrings.Switch:
                valuesList.Clear();
                valuesList.Add(parameterValues[7].text);
                valuesList.Add(parameterValues[11].text);
                valuesList.Add(parameterValues[12].text);
                break;
            #endregion
            #region Roteador
            case ConstStrings.Roteador:
                valuesList.Clear();
                valuesList.Add(parameterValues[7].text);
                valuesList.Add(parameterValues[11].text);
                valuesList.Add(parameterValues[12].text);
                valuesList.Add(parameterValues[13].text);
                valuesList.Add(parameterValues[14].text);
                break;
            #endregion
            #region Carregador
            case ConstStrings.Carregador:
                valuesList.Clear();
                valuesList.Add(parameterValues[7].text);
                valuesList.Add(parameterValues[11].text);
                valuesList.Add(parameterValues[12].text);
                valuesList.Add(parameterValues[13].text);
                break;
            #endregion
            #region Adaptador AC
            case ConstStrings.AdaptadorAC:
                valuesList.Clear();
                valuesList.Add(parameterValues[7].text);
                valuesList.Add(parameterValues[11].text);
                valuesList.Add(parameterValues[12].text);
                valuesList.Add(parameterValues[13].text);
                break;
            #endregion
            #region Storage NAS
            case ConstStrings.StorageNAS:
                valuesList.Clear();
                valuesList.Add(parameterValues[7].text);
                valuesList.Add(parameterValues[11].text);
                valuesList.Add(parameterValues[12].text);
                valuesList.Add(parameterValues[13].text);
                valuesList.Add(parameterValues[14].text);
                valuesList.Add(parameterValues[15].text);
                break;
            #endregion
            #region GBIC
            case ConstStrings.Gbic:
                valuesList.Clear();
                valuesList.Add(parameterValues[7].text);
                valuesList.Add(parameterValues[6].text);
                valuesList.Add(parameterValues[11].text);
                break;
            #endregion
            #region Placa de vídeo
            case ConstStrings.PlacaDeVideo:
                valuesList.Clear();
                valuesList.Add(parameterValues[7].text);
                valuesList.Add(parameterValues[11].text);
                valuesList.Add(parameterValues[12].text);
                break;
            #endregion
            #region Placa de som
            case ConstStrings.PlacaDeSom:
                valuesList.Clear();
                valuesList.Add(parameterValues[7].text);
                valuesList.Add(parameterValues[11].text);
                break;
            #endregion
            #region Placa de captura de vídeo
            case ConstStrings.PlacaDeCapturaDeVideo:
                valuesList.Clear();
                valuesList.Add(parameterValues[7].text);
                valuesList.Add(parameterValues[11].text);
                break;
            #endregion
            #region Servidor
            case ConstStrings.Servidor:
                valuesList.Clear();
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
                break;
            #endregion
            #region Notebook
            case ConstStrings.Notebook:
                valuesList.Clear();
                valuesList.Add(parameterValues[7].text);
                valuesList.Add(parameterValues[6].text);
                valuesList.Add(parameterValues[11].text);
                valuesList.Add(parameterValues[12].text);
                valuesList.Add(parameterValues[13].text);
                valuesList.Add(parameterValues[14].text);
                valuesList.Add(parameterValues[15].text);
                valuesList.Add(parameterValues[16].text);
                break;
            #endregion
            #region Monitor
            case ConstStrings.Monitor:
                valuesList.Clear();
                valuesList.Add(parameterValues[7].text);
                valuesList.Add(parameterValues[6].text);
                valuesList.Add(parameterValues[11].text);
                valuesList.Add(parameterValues[12].text);
                valuesList.Add(parameterValues[13].text);
                break;
            #endregion
            default:
                break;
        }

        return valuesList;
    }
}
