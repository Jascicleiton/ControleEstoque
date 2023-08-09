using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class SearchCategoryDropDownHandler : MonoBehaviour
{
    private List<TextField> searchParamentersTextFields;

    private List<string> names = new List<string>();
   private  Dictionary<string, List<string>> dictionary = new Dictionary<string, List<string>>();
    private DropdownField categoryDP;


    private void OnEnable()
    {
        VisualElement root = GetComponent<UIDocument>().rootVisualElement;
        searchParamentersTextFields = root.Query(name: "SearchParametersContainer").Descendents<TextField>().ToList();
        categoryDP = root.Q<DropdownField>("CategoryDP");
        categoryDP.RegisterCallback<ChangeEvent<string>>(HandleInputData);
        EventHandler.UpdateConsultInputs += UpdateFirstTime;
    }

   
    private void OnDisable()
    {
        categoryDP.UnregisterCallback<ChangeEvent<string>>(HandleInputData);
        EventHandler.UpdateConsultInputs -= UpdateFirstTime;
    }

    private void UpdateFirstTime()
    {
        foreach (var item in searchParamentersTextFields)
        {
            item.style.display = DisplayStyle.Flex;
        }

        names.Clear();
        dictionary = HelperMethods.GetParameterValuesNamesPlaceholders(null, InternalDatabase.categories[0]);
        dictionary.TryGetValue("Placeholders", out names);
        SetParameterPlaceholders(InternalDatabase.categories[0]);
    }

    private void HandleInputData(ChangeEvent<string> evt)
    {
        foreach (var item in searchParamentersTextFields)
        {
            item.style.display = DisplayStyle.Flex;
        }

        names.Clear();
        dictionary = HelperMethods.GetParameterValuesNamesPlaceholders(null, evt.newValue);
        dictionary.TryGetValue("Placeholders", out names);
        SetParameterPlaceholders(evt.newValue);
    }

    /// <summary>
    /// Reset all the search parameters to their default values
    /// </summary>
    private void ResetParameterNames()
    {
        for (int i = 0; i < searchParamentersTextFields.Count; i++)
        {
            searchParamentersTextFields[i].value = "";
        }
    }

    /// <summary>
    /// Set all search parameters each time a new category is selected
    /// </summary>
    private void SetParameterPlaceholders(string category)
    {
        ResetParameterNames();
        if(names != null && names.Count > 0)
        {
            SetPlaceholderText(searchParamentersTextFields[0], names[3]);
            SetPlaceholderText(searchParamentersTextFields[1], names[7]);
            SetPlaceholderText(searchParamentersTextFields[2], names[8]);
            for (int i = 11; i < names.Count; i++)
            {
                SetPlaceholderText(searchParamentersTextFields[i - 8], names[i]);                
            }
        }
        if (category == ConstStrings.C_Outros)
        {
            SetPlaceholderText(searchParamentersTextFields[3], names[5]);
        }

        for (int i = 0; i < searchParamentersTextFields.Count; i++)
        {
            if (searchParamentersTextFields[i].value == "" || searchParamentersTextFields[i].value == names[i])
            {
                searchParamentersTextFields[i].style.display = DisplayStyle.None;
            }
        }
        EventHandler.CallUpdateTabInputs();
    }

    private void SetPlaceholderText(TextField textField, string placeholder)
    {
        string placeholderClass = TextField.ussClassName + "__placeholder";

        onFocusOut();
        textField.RegisterCallback<FocusInEvent>(evt => onFocusIn());
        textField.RegisterCallback<FocusOutEvent>(evt => onFocusOut());

        void onFocusIn()
        {
            if (textField.ClassListContains(placeholderClass))
            {
                textField.value = string.Empty;
                textField.RemoveFromClassList(placeholderClass);
            }
        }

        void onFocusOut()
        {
            if (string.IsNullOrEmpty(textField.text))
            {
                textField.SetValueWithoutNotify(placeholder);
                textField.AddToClassList(placeholderClass);
            }
        }
    }

}
