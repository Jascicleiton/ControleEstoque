using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class SearchCategoryDropDownHandler : MonoBehaviour
{
    [SerializeField] private TMP_Text[] searchParamenters;
    [SerializeField] private TMP_InputField[] searchParamentersInput;

    List<string> names = new List<string>();
    Dictionary<string, List<string>> dictionary = new Dictionary<string, List<string>>();
    
    private void Start()
    {
        HandleInputData(0);

    }

    /// <summary>
    /// Handles the inputs placeholder texts for each category
    /// </summary>
    public void HandleInputData(int value)
    {
        foreach (var item in searchParamentersInput)
        {
            item.gameObject.SetActive(true);
        }
        names.Clear();       
        dictionary = HelperMethods.GetParameterValuesAndNames(null, HelperMethods.GetCategoryString(value));
        dictionary.TryGetValue("Names", out names);
        SetParameterPlaceholders();
    }

    private void ResetParameterNames()
    {
        for (int i = 3; i < searchParamenters.Length; i++)
        {
            searchParamenters[i].text = "";
        }
    }

    private void SetParameterPlaceholders()
    {
        ResetParameterNames();
        if(names != null && names.Count > 0)
        {
            for (int i = 11; i < names.Count; i++)
            {
                searchParamenters[i - 8].text = names[i] + "...";
            }
        }

        for (int i = 0; i < searchParamenters.Length; i++)
        {
            if (searchParamenters[i].text == "")
            {
                searchParamentersInput[i].gameObject.SetActive(false);
            }
        }
    }  
}
