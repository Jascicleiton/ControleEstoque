using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Novo Inventario", menuName = "Assets/Novo Inventario")]
public class InventarioListSO : ScriptableObject
{
    public List<InventarioColumns> item = new List<InventarioColumns>();
}
