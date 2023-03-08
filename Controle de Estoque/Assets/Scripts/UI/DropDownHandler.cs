using System.Collections;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using UnityEngine;

public class DropDownHandler : MonoBehaviour
{
    [SerializeField] TMP_Dropdown dropdown;
    List<string> itemsToInclude;

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
        switch (InternalDatabase.Instance.currentEstoque)
        {
            case CurrentEstoque.SnPro:
                itemsToInclude = ConstStrings.SNPCategories.ToList<string>();
                break;
            case CurrentEstoque.Funsoft:
                itemsToInclude = ConstStrings.AllCategories.ToList<string>();
                break;
            case CurrentEstoque.ESF:
                itemsToInclude = ConstStrings.AllCategories.ToList<string>();
                break;
            case CurrentEstoque.Testing:
                itemsToInclude = ConstStrings.AllCategories.ToList<string>();
                break;
            case CurrentEstoque.Clientes:
                itemsToInclude = ConstStrings.AllCategories.ToList<string>();
                break;
            case CurrentEstoque.Concert:
                itemsToInclude = ConstStrings.AllCategories.ToList<string>();
                break;
            default:
                itemsToInclude = ConstStrings.AllCategories.ToList<string>();
                break;
            
        }
        if (itemsToInclude.Count > 0)
        {
            dropdown.AddOptions(itemsToInclude);          
        }
    }    
}