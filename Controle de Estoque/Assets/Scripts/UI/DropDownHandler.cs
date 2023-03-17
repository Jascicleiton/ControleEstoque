using System.Collections;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using UnityEngine;

public class DropDownHandler : MonoBehaviour
{
    [SerializeField] TMP_Dropdown dropdown;

    /// <summary>
    /// Handle which items are available on the category dropdown based on which "estoque" is selected
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
    
    
}