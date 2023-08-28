using Assets.Scripts.Inventory.Database;
using UnityEngine;
using UnityEngine.UIElements;

namespace Assets.Scripts.UI
{
    public class CategoryDropDownHandler : MonoBehaviour
    {
        private DropdownField dropdown;

        private void OnEnable()
        {
            VisualElement root = GetComponent<UIDocument>().rootVisualElement;
            dropdown = root.Q<DropdownField>("CategoryDP");
            if (InternalDatabase.categories.Count > 0)
            {
                dropdown.choices = InternalDatabase.categories;
                dropdown.value = dropdown.choices[0];
                dropdown.formatListItemCallback = (element) => element.ToString();
                // dropdown.formatSelectedValueCallback = (element) => element.ToString();               
            }
        }

        public string GetDropdownValue()
        {
            return dropdown.value;
        }
    }
}