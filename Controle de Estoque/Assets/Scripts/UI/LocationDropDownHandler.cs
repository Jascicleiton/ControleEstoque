using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class LocationDropDownHandler : MonoBehaviour
{
    [SerializeField] TMP_Dropdown dropdown;
   
    /// <summary>
    /// Handles all locations that an item can go to or come from
    /// </summary>
    void Start()
    {
        if (dropdown == null)
        {
            dropdown = GetComponent<TMP_Dropdown>();
        }
        dropdown.ClearOptions();
        switch (InternalDatabase.Instance.currentEstoque)
        {
            case CurrentEstoque.SnPro:
                break;
            case CurrentEstoque.Funsoft:
                break;
            case CurrentEstoque.ESF:
                InternalDatabase.locations.Clear();
                InternalDatabase.locations.Add("Estoque");
                InternalDatabase.locations.Add("Estoque de itens defeituosos");
                InternalDatabase.locations.Add("Em uso");
                break;
            case CurrentEstoque.Testing:
                break;
            case CurrentEstoque.Clientes:
                break;
            case CurrentEstoque.Concert:

                break;
            default:
                break;
        }
        if (InternalDatabase.locations.Count > 0)
        {
            dropdown.AddOptions(InternalDatabase.locations);
        }
        dropdown.value = HelperMethods.GetLocationDPValue("Estoque");
    }

    public void ResetDropDown()
    {
        dropdown.value = HelperMethods.GetLocationDPValue("Estoque");
    }
}
