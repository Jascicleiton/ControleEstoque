using Assets.Scripts.Inventory.Database;
using Assets.Scripts.Misc;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

namespace Assets.Scripts.Inventory.Consult
{
    public class SearchCategoryDropDownHandler : MonoBehaviour
    {
        private List<TextField> _searchParamentersTextFields;

        private List<string> _names = new List<string>();
        private Dictionary<string, List<string>> _dictionary = new Dictionary<string, List<string>>();
        private DropdownField _categoryDP;


        private void OnEnable()
        {
            VisualElement root = GetComponent<UIDocument>().rootVisualElement;
            _searchParamentersTextFields = root.Query(name: "SearchParametersContainer").Descendents<TextField>().ToList();
            _categoryDP = root.Q<DropdownField>("CategoryDP");
            _categoryDP.RegisterCallback<ChangeEvent<string>>(HandleInputData);
            EventHandler.UpdateConsultInputs += UpdateFirstTime;
        }


        private void OnDisable()
        {
            _categoryDP.UnregisterCallback<ChangeEvent<string>>(HandleInputData);
            EventHandler.UpdateConsultInputs -= UpdateFirstTime;
        }

        private void UpdateFirstTime()
        {
            foreach (var item in _searchParamentersTextFields)
            {
                item.style.display = DisplayStyle.Flex;
            }

            _names.Clear();
            _dictionary = HelperMethods.GetParameterValuesNamesPlaceholders(null, InternalDatabase.categories[0]);
            _dictionary.TryGetValue("Placeholders", out _names);
            SetParameterPlaceholders(InternalDatabase.categories[0]);
        }

        private void HandleInputData(ChangeEvent<string> evt)
        {
            foreach (var item in _searchParamentersTextFields)
            {
                item.style.display = DisplayStyle.Flex;
            }

            _names.Clear();
            _dictionary = HelperMethods.GetParameterValuesNamesPlaceholders(null, evt.newValue);
            _dictionary.TryGetValue("Placeholders", out _names);
            SetParameterPlaceholders(evt.newValue);
        }

        /// <summary>
        /// Reset all the search parameters to their default values
        /// </summary>
        private void ResetParameterNames()
        {
            for (int i = 0; i < _searchParamentersTextFields.Count; i++)
            {
                _searchParamentersTextFields[i].value = "";
            }
        }

        /// <summary>
        /// Set all search parameters each time a new category is selected
        /// </summary>
        private void SetParameterPlaceholders(string category)
        {
            ResetParameterNames();
            if (_names != null && _names.Count > 0)
            {
                _searchParamentersTextFields[0].textEdition.placeholder = _names[3];
                _searchParamentersTextFields[1].textEdition.placeholder = _names[7];
                _searchParamentersTextFields[2].textEdition.placeholder = _names[8];

                for (int i = 11; i < _names.Count; i++)
                {
                    _searchParamentersTextFields[i - 8].textEdition.placeholder = _names[i];
                }
            }
            if (category == ConstStrings.C_Outros)
            {
                _searchParamentersTextFields[3].textEdition.placeholder = _names[5];
            }

            for (int i = 0; i < _searchParamentersTextFields.Count; i++)
            {
                if (_searchParamentersTextFields[i].textEdition.placeholder == "" || _searchParamentersTextFields[i].value == _names[i])
                {
                    _searchParamentersTextFields[i].style.display = DisplayStyle.None;
                }
            }
            EventHandler.CallUpdateTabInputs();
        }
    }
}