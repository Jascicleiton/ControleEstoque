using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using System.IO;
using System.Linq;
using TMPro;
using Unity.VisualScripting;

public class InventarioManager : Singleton<InventarioManager>
{
    public TextAsset sheetCSV;
    private CSVSheetToUnity sheet;
    string fileName = "";
    [SerializeField] GameObject addPanel;
    [SerializeField] GameObject removePanel;

    [SerializeField] TMP_InputField entrada_txt;
    [SerializeField] TMP_InputField patrimonio_txt;
    [SerializeField] TMP_InputField status_txt;
    [SerializeField] TMP_InputField serial_txt;
    [SerializeField] TMP_InputField categoria_txt;
    [SerializeField] TMP_InputField fabricante_txt;
    [SerializeField] TMP_InputField modelo_txt;
    [SerializeField] TMP_InputField local_txt;
    [SerializeField] TMP_InputField saida_txt;
    [SerializeField] TMP_InputField observacao_txt;
    [SerializeField] TMP_Text txt;
    [SerializeField] TMP_InputField rem_patrimonio_txt;
    [SerializeField] TMP_InputField rem_serial_txt;

    // Start is called before the first frame update
    void Start()
    {
        sheet = new CSVSheetToUnity();
        //txt.text = sheet.item.Count.ToString();   
        // ImportInventarioToDatabase(10);
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.F7))
        {
            sheet = InternalDatabase.database[ConstStrings.InventarioSnPro];
        }
        
    }

    /// <summary>
    /// Creates an Inventário_Sysnetpro CSV file
    /// </summary>
    private void CreateInventarioSheet()
    {
        fileName = Application.dataPath + "/" + ConstStrings.InventarioSnPro + ".csv";
        TextWriter textWriter = new StreamWriter(fileName, false);
        textWriter.WriteLine("Entrada, Patrimônio, Status, Serial, Categoria, " +
                        "Fabricante, Modelo, Local, Saída, Observação");
        textWriter.Close();
        if (InternalDatabase.Instance != null)
        {
            sheet = InternalDatabase.database[ConstStrings.InventarioSnPro];
            if (sheet != null)
            {
                foreach (SheetColumns item in sheet.item)
                {
                    textWriter = new StreamWriter(fileName, true);
                    textWriter.WriteLine(item.Entrada + "," + item.Patrimonio + "," + item.Status + "," +
                        item.Serial + "," + item.Categoria + "," + item.Fabricante + "," +
                        item.Modelo + "," + item.Local + "," + item.Saida + "," + item.Observacao);

                }
            }
        }
        textWriter.Close();
    }

    /// <summary>
    /// Import Inventario_SnPro.csv into the internal database
    /// </summary>
    public void ImportInventarioToDatabase(int numberOfColumns)
    {
        string[] data = sheetCSV.text.Split(new string[] { ",", "\n" }, StringSplitOptions.None);

        // it takes one off, because the first row is ignored
        int tableSize = data.Length / numberOfColumns - 1;
        sheet.item = new List<SheetColumns>();
        for (int i = 0; i < tableSize; i++)
        {
            sheet.item.Add(new SheetColumns());
            sheet.item[i].Entrada = data[4 * (i + 1)];
            sheet.item[i].Patrimonio = data[4 * (i + 1) + 1];
            sheet.item[i].Status = data[4 * (i + 1) + 2];
            sheet.item[i].Serial = data[4 * (i + 1) + 3];
            sheet.item[i].Categoria = data[4 * (i + 1) + 4];
            sheet.item[i].Fabricante = data[4 * (i + 1) + 5];
            sheet.item[i].Modelo = data[4 * (i + 1) + 6];
            sheet.item[i].Local = data[4 * (i + 1) + 7];
            sheet.item[i].Saida = data[4 * (i + 1) + 8];
            sheet.item[i].Observacao = data[4 * (i + 1) + 9];
        }
        InternalDatabase.database.Add(ConstStrings.InventarioSnPro, sheet);
        
    }

    public void AddNewItem(string sheetName)
    {
        switch (sheetName)
        {
            case ConstStrings.InventarioSnPro:
                AddNewItem(entrada_txt.text, patrimonio_txt.text, status_txt.text, serial_txt.text, categoria_txt.text, fabricante_txt.text, modelo_txt.text, local_txt.text, saida_txt.text, observacao_txt.text);
                break;
            default:
                break;
        }
    }

    /// <summary>
    /// Add new item into Inventário SnPro inside database
    /// </summary>
    public void AddNewItem(string Entrada, string Patrimônio, string Status, string Serial, string Categoria, string Fabricante, string Modelo, string Local, string Saída, string Observação)
    {
        SheetColumns temp = new SheetColumns();
        temp.Entrada = Entrada;
        temp.Patrimonio = Patrimônio;
        temp.Status = Status;
        temp.Serial = Serial;
        temp.Categoria = Categoria;
        temp.Fabricante = Fabricante;
        temp.Modelo = Modelo;
        temp.Local = Local;
        temp.Saida = Saída;
        temp.Observacao = Observação;
        InternalDatabase.database[ConstStrings.InventarioSnPro].item.Add(temp);

    }

    // Testing
    public void RemoveItem()
    {
        RemoveItem(ConstStrings.InventarioSnPro, "Patrimonio");
    }

    public void RemoveItem(string sheetName, string referenceToCheck)
    {
        sheetName = ConstStrings.InventarioSnPro;
        foreach (SheetColumns item in InternalDatabase.database[sheetName].item)
        {
            if(referenceToCheck == "Patrimonio")
            {
                if (item.Patrimonio == rem_patrimonio_txt.text)
                {
                    InternalDatabase.database[sheetName].item.Remove(item);
                }
            }
            else if (referenceToCheck == "Serial")
            {
                if (item.Serial == rem_serial_txt.text)
                {
                    InternalDatabase.database[sheetName].item.Remove(item);
                }
            }

            


        }
        
    }
}
