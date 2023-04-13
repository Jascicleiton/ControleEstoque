using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using System.Runtime.InteropServices;

public class ExportCSVs : MonoBehaviour
{
    [DllImport("__Internal")]
    private static extern void CreateCsv(string joined, string filename);

    private string fileName = "";

    public void ExportInventory()
    {
        WWWForm form = CreateForm.GetExportInventoryForm();
    }

    public void CreateInventarioSheet()
    {
        Sheet tempSheet = new Sheet();
        fileName = Application.persistentDataPath + "/Inventario.csv";
        TextWriter textWriter = new StreamWriter(fileName, false); // It is false to create an empty file with the first line being what is typed below
        textWriter.WriteLine("Aquisição, Entrada, Patrimônio, Status, Serial, Categoria, " +
                        "Fabricante, Modelo, Local, Pessoa, Centro de Custo, Saída");
        textWriter.Close();
        textWriter.Dispose();
        if (InternalDatabase.Instance != null)
        {
            tempSheet = InternalDatabase.Instance.splitDatabase[ConstStrings.Inventario];
            if (tempSheet != null)
            {
                foreach (ItemColumns item in tempSheet.itens)
                {
                    textWriter = new StreamWriter(fileName, true); // it is true to add new lines to the already created file
                    textWriter.WriteLine(item.Aquisicao + "," + item.Entrada + "," + item.Patrimonio + "," + item.Status + "," +
                        item.Serial + "," + item.Categoria + "," + item.Fabricante + "," +
                        item.Modelo + "," + item.Local + "," + item.Pessoa + "," + item.CentroDeCusto + ","  + item.Saida);
                }
            }
        }
        textWriter.Close();
    }

    public void CreateInventarioSheet2()
    {
        List<string[]> rowData = new List<string[]>();
        List<string> rowDataTemp = new List<string>();
        switch (InternalDatabase.Instance.currentEstoque)
        {
            case CurrentEstoque.SnPro:
                rowDataTemp.Add("Aquisição");
                rowDataTemp.Add("Entrada");
                rowDataTemp.Add("Patrimônio");
                rowDataTemp.Add("Status");
                rowDataTemp.Add("Serial");
                rowDataTemp.Add("Categoria");
                rowDataTemp.Add("Fabricante");
                rowDataTemp.Add("Modelo");
                rowDataTemp.Add("Local");
                rowDataTemp.Add("Saída");
                rowDataTemp.Add("Observação");
                break;
            case CurrentEstoque.Fumsoft:
                break;
            case CurrentEstoque.ESF:
                break;
            case CurrentEstoque.Testing:
                break;
            case CurrentEstoque.Clientes:
                break;
            case CurrentEstoque.Concert:
                rowDataTemp.Add("Aquisição");
                rowDataTemp.Add("Entrada");
                rowDataTemp.Add("Patrimônio");
                rowDataTemp.Add("Status");
                rowDataTemp.Add("Serial");
                rowDataTemp.Add("Categoria");
                rowDataTemp.Add("Fabricante");
                rowDataTemp.Add("Modelo");
                rowDataTemp.Add("Local");
                rowDataTemp.Add("Pessoa");
                rowDataTemp.Add("Centro de Custo");
                rowDataTemp.Add("Saída");
                break;
            default:
                break;
        }
        foreach (ItemColumns item in InternalDatabase.Instance.splitDatabase[ConstStrings.Inventario].itens)
        {
            rowDataTemp.Add(item.Aquisicao);
            rowDataTemp.Add(item.Entrada);
            rowDataTemp.Add(item.Patrimonio.ToString());
            rowDataTemp.Add(item.Status);
            rowDataTemp.Add(item.Serial);
            rowDataTemp.Add(item.Categoria);
            rowDataTemp.Add(item.Fabricante);
            rowDataTemp.Add(item.Modelo);
            rowDataTemp.Add(item.Local);
            rowDataTemp.Add(item.Saida);
            rowDataTemp.Add(item.Observacao);
            rowData.Add(rowDataTemp.ToArray());
        }

        string joined = "";

        for (int i = 0; i < rowData.Count; i++)
        {
            for (int j = 0; j < rowData[i].Length; j++)
            {
                joined += rowData[i][j] + "|";
            }
            joined += ",";
        }

        CreateCsv(joined, "Inventário.csv");
    }

    
}
