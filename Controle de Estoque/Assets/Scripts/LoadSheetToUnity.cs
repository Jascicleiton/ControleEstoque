using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class LoadSheetToUnity : MonoBehaviour
{
    
    public TextAsset textAsetData;
    public InventarioList inventarioList = new InventarioList();

    // Start is called before the first frame update
    void Start()
    {
        ReadInventario();
    }

    private void ReadInventario()
    {
        string[] data = textAsetData.text.Split(new string[] { ",", "\n" }, StringSplitOptions.None);
        
        int tableSize = data.Length / 10 - 1;
        inventarioList.item = new InventarioColumns[tableSize];

        for (int i = 0; i < tableSize; i++)
        {
            inventarioList.item[i] = new InventarioColumns();
            inventarioList.item[i].Entrada = data[10 * (i + 1)];
            inventarioList.item[i].Patrimônio = int.Parse(data[10 * (i + 1)+ 1]);
            inventarioList.item[i].Status = data[10 * (i + 1)+2];
            inventarioList.item[i].Serial = data[10 * (i + 1)+3];
            inventarioList.item[i].Categoria = data[10 * (i + 1)+4];
            inventarioList.item[i].Fabricante = data[10 * (i + 1)+5];
            inventarioList.item[i].Modelo = data[10 * (i + 1)+6];
            inventarioList.item[i].Local = data[10 * (i + 1)+7];
            inventarioList.item[i].Saída = data[10 * (i + 1)+8];
            inventarioList.item[i].Observação = data[10 * (i + 1)+9];
        }
    }
    
}
