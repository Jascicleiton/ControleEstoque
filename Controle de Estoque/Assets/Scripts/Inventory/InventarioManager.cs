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
    /// <summary>
    /// At some point this will have to be set at runtime, draging and droping a file into the application or something
    /// similar
    /// </summary>
    #region CSVs
    [SerializeField] private TextAsset inventarioCSV;
    [SerializeField] private TextAsset hdCSV;
    [SerializeField] private TextAsset memoriaCSV;
    [SerializeField] private TextAsset placaDeRedeCSV;
    [SerializeField] private TextAsset idracCSV;
    [SerializeField] private TextAsset placaControladoraCSV;
    [SerializeField] private TextAsset processadorCSV;
    [SerializeField] private TextAsset desktopCSV;
    [SerializeField] private TextAsset fonteCSV;
    [SerializeField] private TextAsset switchCSV;
    [SerializeField] private TextAsset roteadorCSV;
    [SerializeField] private TextAsset carregadorCSV;
    [SerializeField] private TextAsset adaptadorAcCSV;
    [SerializeField] private TextAsset storageNasCSV;
    [SerializeField] private TextAsset gbicCSV;
    [SerializeField] private TextAsset placaDeVideoCSV;
    [SerializeField] private TextAsset placaDeSomCSV;
    [SerializeField] private TextAsset placaDeCapturaDeVideoCSV;
    #endregion
    
    private string fileName = "";
    [SerializeField] GameObject initialPanel;
    [SerializeField] GameObject consultInventoryPanel;
    [SerializeField] GameObject addNewItemPanel;
    [SerializeField] GameObject moveItemPanel;
    [SerializeField] TMP_Text userMessage_txt;

    // Start is called before the first frame update
    void Start()
    {
        
        //txt.text = sheet.item.Count.ToString();   
        ImportInventarioToDatabase(10);
        ImportHDSheetToDatabase(11);
    }

    private void Update()
    {


    }

    /// <summary>
    /// Creates an Inventário_Sysnetpro CSV file
    /// </summary>
    private void CreateInventarioSheet()
    {
        Sheet tempSheet = new Sheet();
        fileName = Application.dataPath + "/" + ConstStrings.InventarioSnPro + ".csv";
        TextWriter textWriter = new StreamWriter(fileName, false);
        textWriter.WriteLine("Entrada, Patrimônio, Status, Serial, Categoria, " +
                        "Fabricante, Modelo, Local, Saída, Observação");
        textWriter.Close();
        if (InternalDatabase.Instance != null)
        {
            tempSheet = InternalDatabase.splitDatabase[ConstStrings.InventarioSnPro];
            if (tempSheet != null)
            {
                foreach (SheetColumns item in tempSheet.itens)
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
        string[] data = inventarioCSV.text.Split(new string[] { ",", "\r\n" }, StringSplitOptions.None);   
        int tableSize = data.Length / numberOfColumns - 1; // it takes one off, because the first row is ignored
        Sheet tempSheet = new Sheet();
        tempSheet.itens = new List<SheetColumns>();

        for (int i = 0; i < tableSize; i++)
        {
            SheetColumns newRow = new SheetColumns();
                      
            newRow.Entrada = data[numberOfColumns * (i + 1)];
            newRow.Patrimonio = data[numberOfColumns * (i + 1) + 1];
            newRow.Status = data[numberOfColumns * (i + 1) + 2];
            newRow.Serial = data[numberOfColumns * (i + 1) + 3];
            newRow.Categoria = data[numberOfColumns * (i + 1) + 4];
            newRow.Fabricante = data[numberOfColumns * (i + 1) + 5];
            newRow.Modelo = data[numberOfColumns * (i + 1) + 6];
            newRow.Modelo.Trim();
            newRow.Local = data[numberOfColumns * (i + 1) + 7];
            newRow.Saida = data[numberOfColumns * (i + 1) + 8];
            newRow.Observacao = data[numberOfColumns * (i + 1) + 9];
            tempSheet.itens.Add(newRow);
        }

        if (!InternalDatabase.splitDatabase.ContainsKey(ConstStrings.InventarioSnPro))
        {
            InternalDatabase.splitDatabase.Add(ConstStrings.InventarioSnPro, tempSheet);
        }
        else
        {
            InternalDatabase.splitDatabase[ConstStrings.InventarioSnPro] = tempSheet;
        }

        //InternalDatabase.Instance.FillFullDatabase(); // for testing
    }

    /// <summary>
    /// Import HD.csv into the internal database
    /// </summary>
    public void ImportHDSheetToDatabase(int numberOfColumns)
    {
        string[] data = hdCSV.text.Split(new string[] { ",", "\r\n" }, StringSplitOptions.None);
        int tableSize = data.Length / numberOfColumns - 1; // it takes one off, because the first row is ignored
        Sheet tempSheet = new Sheet();
        tempSheet.itens = new List<SheetColumns>();

        for (int i = 0; i < tableSize; i++)
        {
            SheetColumns newRow = new SheetColumns();

            newRow.Modelo = data[numberOfColumns * (i + 1)];
            newRow.Modelo.Trim();
            newRow.Fabricante = data[numberOfColumns * (i + 1) + 1];
            newRow.Fabricante.Trim();
            newRow.Interface = data[numberOfColumns * (i + 1) + 2];
            newRow.Interface.Trim();
            newRow.Tamanho = data[numberOfColumns * (i + 1) + 3];
            newRow.Tamanho.Trim();
            newRow.FormaDeArmazenamento = data[numberOfColumns * (i + 1) + 4];
            newRow.CapacidadeEmGB = data[numberOfColumns * (i + 1) + 5];
            newRow.RPM = data[numberOfColumns * (i + 1) + 6];
            newRow.VelocidadeDeLeitura = data[numberOfColumns * (i + 1) + 7];
            newRow.Enterprise = data[numberOfColumns * (i + 1) + 8];
            newRow.Local = data[numberOfColumns * (i + 1) + 9];
            newRow.EstoqueAtual = data[numberOfColumns * (i + 1) + 10];
            tempSheet.itens.Add(newRow);
        }

        if (!InternalDatabase.splitDatabase.ContainsKey(ConstStrings.HD))
        {
            InternalDatabase.splitDatabase.Add(ConstStrings.HD, tempSheet);
        }
        else
        {
            InternalDatabase.splitDatabase[ConstStrings.HD] = tempSheet;
        }
        InternalDatabase.Instance.testingSheet = tempSheet;
        InternalDatabase.Instance.FillFullDatabase(); // for testing
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
