using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;

public class ExportSheet : MonoBehaviour
{
    string fileName = "";
    public InventarioList inventarioList = new InventarioList();

    // Start is called before the first frame update
    void Start()
    {
        fileName = Application.dataPath + "/Sheets/Test.csv";
    }

    public void ExportSheetCSV(bool firstTime)
    {
        if (firstTime)
        {
            if (inventarioList.item.Length > 0)
            {
                TextWriter text = new StreamWriter(fileName, false);
                text.WriteLine("ENTRADA, PATRIMÔNIO, STATUS, SERIAL, CATEGORIA, FABRICANTE, MODELO, LOCAL, SAÍDA, OBSERVAÇÃO");
                text.Close();
            }
        }
        else
        {
            if (inventarioList.item.Length > 0)
            {
                TextWriter text = new StreamWriter(fileName, true);
                for (int i = 0; i < inventarioList.item.Length; i++)
                {
                    text.WriteLine(inventarioList.item[i].Entrada + "," + inventarioList.item[i].Patrimônio + "," +
                                    inventarioList.item[i].Status + "," + inventarioList.item[i].Serial + "," +
                                    inventarioList.item[i].Categoria + "," + inventarioList.item[i].Fabricante + "," +
                                    inventarioList.item[i].Modelo + "," + inventarioList.item[i].Local + "," +
                                    inventarioList.item[i].Saída + "," + inventarioList.item[i].Observação);
                }
                text.Close();
            }
        }
    }
}
