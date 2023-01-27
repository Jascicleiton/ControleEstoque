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

    public void HideItems()
    {
        ResetNames();
        ResetValues();
        for (int i = 0; i < itemBoxes.Length; i++)
        {

            itemBoxes[i].gameObject.SetActive(true);
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
}
