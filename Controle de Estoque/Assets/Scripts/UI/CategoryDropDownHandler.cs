using TMPro;
using UnityEngine;

public class CategoryDropDownHandler : MonoBehaviour
{
    [SerializeField] TMP_Dropdown dropdown;

    /// <summary>
    /// Handle which items are available on the category dropdown
    /// </summary>
    void Start()
    {      
        if(dropdown == null)
        {
            dropdown = GetComponent<TMP_Dropdown>();
        }
        dropdown.ClearOptions();

        if (InternalDatabase.categories.Count > 0)
        {
            dropdown.AddOptions(InternalDatabase.categories);          
        }
    }

    /// <summary>
    /// Handle which items are available on the category dropdown - also called on Enable to prevent a bug on offline version
    /// </summary>
    private void OnEnable()
    {
         if (dropdown == null)
            {
                dropdown = GetComponent<TMP_Dropdown>();
            }
         if(dropdown.options.Count == 0)
        {
            dropdown.AddOptions(InternalDatabase.categories);
        }
    }


}