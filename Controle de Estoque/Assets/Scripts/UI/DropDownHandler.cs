using System.Collections;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using UnityEngine;

public class DropDownHandler : MonoBehaviour
{
    [SerializeField] TMP_Dropdown dropdown;
    List<string> itemsToInclude;
    // Start is called before the first frame update
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
            default:
                break;
        }
        if (itemsToInclude.Count > 0)
        {
            dropdown.AddOptions(itemsToInclude);          
        }
    }    
}