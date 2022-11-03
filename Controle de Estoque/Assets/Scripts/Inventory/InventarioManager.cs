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
    private Sheet sheet;
    private string fileName = "";
    [SerializeField] GameObject initialPanel;
    [SerializeField] GameObject consultInventoryPanel;
    [SerializeField] GameObject addNewItemPanel;
    [SerializeField] GameObject moveItemPanel;
    [SerializeField] TMP_Text userMessage_txt;

    // Start is called before the first frame update
    void Start()
    {
        sheet = new Sheet();
        //txt.text = sheet.item.Count.ToString();   
         ImportInventarioToDatabase(10);
    }

    private void Update()
    {


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
            sheet = InternalDatabase.splitDatabase[ConstStrings.InventarioSnPro];
            if (sheet != null)
            {
                foreach (SheetColumns item in sheet.itens)
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

        
        int tableSize = data.Length / numberOfColumns - 1; // it takes one off, because the first row is ignored

        sheet.itens = new List<SheetColumns>();
        for (int i = 0; i < tableSize; i++)
        {
            SheetColumns newColumn = new SheetColumns();
                      
            newColumn.Entrada = data[numberOfColumns * (i + 1)];
            newColumn.Patrimonio = data[numberOfColumns * (i + 1) + 1];
            newColumn.Status = data[numberOfColumns * (i + 1) + 2];
            newColumn.Serial = data[numberOfColumns * (i + 1) + 3];
            newColumn.Categoria = data[numberOfColumns * (i + 1) + 4];
            newColumn.Fabricante = data[numberOfColumns * (i + 1) + 5];
            newColumn.Modelo = data[numberOfColumns * (i + 1) + 6];
            newColumn.Local = data[numberOfColumns * (i + 1) + 7];
            newColumn.Saida = data[numberOfColumns * (i + 1) + 8];
            newColumn.Observacao = data[numberOfColumns * (i + 1) + 9];
            sheet.itens.Add(newColumn);
        }
        InternalDatabase.splitDatabase.Add(ConstStrings.InventarioSnPro, sheet);
        //InternalDatabase.Instance.FillFullDatabase(); // for testing

    }

    /// <summary>
    /// Closes all panels and open the selected panel
    /// </summary>
    public void OpenClosePanels(GameObject panelToOpen)
    {
        consultInventoryPanel.SetActive(false);
        addNewItemPanel.SetActive(false);
        moveItemPanel.SetActive(false);
        initialPanel.SetActive(false);
        panelToOpen.SetActive(true);
    }

    /// <summary>
    /// Add new item into Inventário SnPro inside database
    /// </summary>
    public void AddItem(string Entrada, string Patrimônio, string Status, string Serial, string Categoria, string Fabricante, string Modelo, string Local, string Saída, string Observação)
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
        InternalDatabase.splitDatabase[ConstStrings.InventarioSnPro].itens.Add(temp);

    }

    public void RemoveItem(string sheetName, string referenceToCheck, string referenceValue)
    {
        sheetName = ConstStrings.InventarioSnPro;
        foreach (SheetColumns item in InternalDatabase.splitDatabase[sheetName].itens)
        {
            if (referenceToCheck == "Patrimonio")
            {
                if (item.Patrimonio == referenceValue)
                {
                    InternalDatabase.splitDatabase[sheetName].itens.Remove(item);
                }
            }
            else if (referenceToCheck == "Serial")
            {
                if (item.Serial == referenceValue)
                {
                    InternalDatabase.splitDatabase[sheetName].itens.Remove(item);
                }
            }
        }
    }

    public void MoveItem(string newPlace)
    {

    }
}
