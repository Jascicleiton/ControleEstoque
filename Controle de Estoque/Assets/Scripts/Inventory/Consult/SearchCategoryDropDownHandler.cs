using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class SearchCategoryDropDownHandler : MonoBehaviour
{
    [SerializeField] private TMP_Text[] searchParameters;
    [SerializeField] private TMP_InputField[] searchParamentersInput;

    List<string> names = new List<string>();
    Dictionary<string, List<string>> dictionary = new Dictionary<string, List<string>>();
    
    private void Start()
    {
        StartCoroutine(WaitATick());
    }

    /// <summary>
    /// Wait half a second before initializing, to guarantee everythin is loaded
    /// </summary>
    private IEnumerator WaitATick()
    {
        yield return new WaitForSecondsRealtime(0.5f);
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

    /// <summary>
    /// Reset all the search parameters to their default values
    /// </summary>
    private void ResetParameterNames()
    {
        for (int i = 3; i < searchParameters.Length; i++)
        {
            searchParameters[i].text = "";
                    }
    }

    /// <summary>
    /// Set all search parameters each time a new category is selected
    /// </summary>
    private void SetParameterPlaceholders()
    {
        ResetParameterNames();
        if(names != null && names.Count > 0)
        {
            for (int i = 11; i < names.Count; i++)
            {
                searchParameters[i - 8].text = names[i] + "...";
            }
        }

        for (int i = 0; i < searchParameters.Length; i++)
        {
            if (searchParameters[i].text == "")
            {
                searchParamentersInput[i].gameObject.SetActive(false);
            }
        }
        EventHandler.CallUpdateTabInputs();
    }  
}
