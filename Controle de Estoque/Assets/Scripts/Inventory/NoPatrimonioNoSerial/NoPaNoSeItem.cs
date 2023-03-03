using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Item that do not have a "serial" nor a "patrimônio"
/// </summary>
[System.Serializable]
public class NoPaNoSeItem
{
    public string ItemName;
    public int Quantity;

    public NoPaNoSeItem()
    {
        ItemName = "";
        Quantity = 0;
    }

    public  NoPaNoSeItem(string itemName, int quantity)
    {
        this.ItemName = itemName;
        this.Quantity = quantity;
    }
}
