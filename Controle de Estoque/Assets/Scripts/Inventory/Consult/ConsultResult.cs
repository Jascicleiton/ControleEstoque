using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UIElements;

public class ConsultResult : MonoBehaviour
{
    private Label itemName; // shows either the item "Serial" or "Patrimônio"
    private VisualElement[] itemBoxes;
    private List<Label> parameterNames;
    private List<Label> parameterValues;

    private void OnEnable()
    {
       VisualElement root = GetComponent<UIDocument>().rootVisualElement;
       itemBoxes = root.Q<VisualElement>("Results").Children().ToArray<VisualElement>();
        parameterNames = root.Query(name: "ResultPanel").Descendents<Label>(name: "ParameterName").ToList();
        parameterValues = root.Query(name: "ResultPanel").Descendents<Label>(name: "ParameterValue").ToList();
    }

    public VisualElement[] GetItemBoxes()
    {
        return itemBoxes;
    }

    public List<Label> GetParameterNames()
    {
        return parameterNames;
    }

    public List<Label> GetParameterValues()
    {
        return parameterValues;
    }

    /// <summary>
    /// Used to show the result of consulting the database"
    /// itemName = 0: "Patrimônio"; itemName = 1: "Serial"
    /// </summary>
    public void ShowResult(ItemColumns itemToShow)
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
            FillValues(values);
        }
        else
        {
            print("Item to show is null");
        }
        HideEmptyItemBox();
    }

    /// <summary>
    /// Activate all item boxes
    /// </summary>
    private void ActivateAllItemBoxes()
    {
        for (int i = 0; i < itemBoxes.Length; i++)
        {
            itemBoxes[i].style.display = DisplayStyle.Flex;     
        }
    }

    /// <summary>
    /// Fill the names of all item boxes that should get a name
    /// </summary>
    private void FillNames(List<string> names)
    {
        for (int i = 0; i < parameterNames.Count; i++)
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
        for (int i = 0; i < parameterValues.Count; i++)
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
    /// Hide all item boxes that have a empty name
    /// </summary>
    private void HideEmptyItemBox()
    {
        for (int i = 0; i < parameterNames.Count; i++)
        {
            if (parameterNames[i] != null && parameterNames[i].text == "")
            {
                itemBoxes[i].style.display = DisplayStyle.None;
            }
        }
    }
}
