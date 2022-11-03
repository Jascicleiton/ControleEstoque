using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Each instance of this class is a differente sheet, with different informations
/// </summary>
[System.Serializable]
public class Sheet
{
    public List<SheetColumns> itens = new List<SheetColumns>();
}
