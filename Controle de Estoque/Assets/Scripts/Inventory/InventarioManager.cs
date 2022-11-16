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

    void Start()
    {
        DontDestroyOnLoad(this.gameObject);
        //txt.text = sheet.item.Count.ToString();   

        ImportSheets();
        if(inventarioCSV == null)
        {
            inventarioCSV = GameObject.Find("Assets/Sheets/Inventario_Sysnetpro").GetComponent<TextAsset>();
            ImportSheets();
        }
       StartCoroutine(WaitToFillFullDatabase());

    }

    /// <summary>
    /// Wait a few seconds to call FillFullDatabase to give some time for ImportSheets to finish
    /// </summary>
    private IEnumerator WaitToFillFullDatabase()
    {
        yield return new WaitForSeconds(3f);
        InternalDatabase.Instance.FillFullDatabase();
    }

    /// <summary>
    /// Import all sheets to internal database
    /// </summary>
    private void ImportSheets()
    {
        ImportInventarioToDatabase(10);
        ImportHDSheetToDatabase(10);
        ImportMemoriaToDatabase(11);
        ImportPlacaDeRedeToDatabase(8);
        ImportiDracToDatabase(7);
        ImportPlacaControladoraToDatabase(10);
        ImportProcessadorToDatabase(8);
        ImportDesktopToDatabase(9);
        ImportFonteToDatabase(5);
        ImportSwitchToDatabase(4);
        ImportRoteadorToDatabase(5);
        ImportCarregadorToDatabase(5);
        ImportAdaptadorAcToDatabase(5);
        ImportStorageNASToDatabase(7);
        ImportGBICToDatabase(4);
        ImportPlacaDeVideoToDatabase(4);
        ImportPlacaDeSomToDatabase(3);
        ImportPlacaDeCapturaDeVideoToDatabase(3);
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
            tempSheet = InternalDatabase.Instance.splitDatabase[ConstStrings.InventarioSnPro];
            if (tempSheet != null)
            {
                foreach (ItemColumns item in tempSheet.itens)
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

    #region Import all CSVs to internal database
    /// <summary>
    /// Import Inventario_SnPro.csv into the internal database
    /// </summary>
    public void ImportInventarioToDatabase(int numberOfColumns)
    {
        string[] data = inventarioCSV.text.Split(new string[] { ",", "\n" }, StringSplitOptions.None);   
        int tableSize = data.Length / numberOfColumns - 1; // it takes one off, because the first row is ignored
        Sheet tempSheet = new Sheet();
        tempSheet.itens = new List<ItemColumns>();

        for (int i = 0; i < tableSize; i++)
        {
            ItemColumns newRow = new ItemColumns();
                      
            newRow.Entrada = data[numberOfColumns * (i + 1)];
            newRow.Patrimonio = data[numberOfColumns * (i + 1) + 1];
            newRow.Status = data[numberOfColumns * (i + 1) + 2];
            newRow.Serial = data[numberOfColumns * (i + 1) + 3];
            newRow.Categoria = data[numberOfColumns * (i + 1) + 4];
            newRow.Fabricante = data[numberOfColumns * (i + 1) + 5];
            newRow.Modelo = data[numberOfColumns * (i + 1) + 6];
            newRow.Local = data[numberOfColumns * (i + 1) + 7];
            newRow.Saida = data[numberOfColumns * (i + 1) + 8];
            newRow.Observacao = data[numberOfColumns * (i + 1) + 9];
            tempSheet.itens.Add(newRow);
        }

        if (!InternalDatabase.Instance.splitDatabase.ContainsKey(ConstStrings.InventarioSnPro))
        {
            InternalDatabase.Instance.splitDatabase.Add(ConstStrings.InventarioSnPro, tempSheet);
        }
        else
        {
            InternalDatabase.Instance.splitDatabase[ConstStrings.InventarioSnPro] = tempSheet;
        }

        //InternalDatabase.Instance.FillFullDatabase(); // for testing
    }

    /// <summary>
    /// Import HD.csv into the internal database
    /// </summary>
    public void ImportHDSheetToDatabase(int numberOfColumns)
    {
        string[] data = hdCSV.text.Split(new string[] { ",", "\n" }, StringSplitOptions.None);
        int tableSize = data.Length / numberOfColumns - 1; // it takes one off, because the first row is ignored
        Sheet tempSheet = new Sheet();
        tempSheet.itens = new List<ItemColumns>();

        for (int i = 0; i < tableSize; i++)
        {
            ItemColumns newRow = new ItemColumns();

            newRow.Modelo = data[numberOfColumns * (i + 1)];
            newRow.Fabricante = data[numberOfColumns * (i + 1) + 1];
            newRow.Interface = data[numberOfColumns * (i + 1) + 2];
            newRow.Tamanho = data[numberOfColumns * (i + 1) + 3];
            newRow.FormaDeArmazenamento = data[numberOfColumns * (i + 1) + 4];
            newRow.CapacidadeEmGB = data[numberOfColumns * (i + 1) + 5];
            newRow.RPM = data[numberOfColumns * (i + 1) + 6];
            newRow.VelocidadeDeLeitura = data[numberOfColumns * (i + 1) + 7];
            newRow.Enterprise = data[numberOfColumns * (i + 1) + 8];
                        newRow.EstoqueAtual = data[numberOfColumns * (i + 1) + 9];

            tempSheet.itens.Add(newRow);
        }

        if (!InternalDatabase.Instance.splitDatabase.ContainsKey(ConstStrings.HD))
        {
            InternalDatabase.Instance.splitDatabase.Add(ConstStrings.HD, tempSheet);
        }
        else
        {
            InternalDatabase.Instance.splitDatabase[ConstStrings.HD] = tempSheet;
        }
        
        
    }

    /// <summary>
    /// Import Memória.csv into the internal database
    /// </summary>
    public void ImportMemoriaToDatabase(int numberOfColumns)
    {
        string[] data = memoriaCSV.text.Split(new string[] { ",", "\n" }, StringSplitOptions.None);
        int tableSize = data.Length / numberOfColumns - 1; // it takes one off, because the first row is ignored
        Sheet tempSheet = new Sheet();
        tempSheet.itens = new List<ItemColumns>();

        for (int i = 0; i < tableSize; i++)
        {
            ItemColumns newRow = new ItemColumns();

            newRow.Modelo = data[numberOfColumns * (i + 1)];
            newRow.Fabricante = data[numberOfColumns * (i + 1) + 1];
            newRow.Tipo = data[numberOfColumns * (i + 1) + 2];
            newRow.CapacidadeEmGB = data[numberOfColumns * (i + 1) + 3];
            newRow.VelocidadeMHz = data[numberOfColumns * (i + 1) + 4];
            newRow.LowVoltage = data[numberOfColumns * (i + 1) + 5];
            newRow.Rank = data[numberOfColumns * (i + 1) + 6];
            newRow.DIMM = data[numberOfColumns * (i + 1) + 7];
            newRow.TaxaDeTransmissao = data[numberOfColumns * (i + 1) + 8];
            newRow.Simbolo = data[numberOfColumns * (i + 1) + 9];
                        newRow.EstoqueAtual = data[numberOfColumns * (i + 1) + 10];

            tempSheet.itens.Add(newRow);
        }
        
        if (!InternalDatabase.Instance.splitDatabase.ContainsKey(ConstStrings.Memoria))
        {
            InternalDatabase.Instance.splitDatabase.Add(ConstStrings.Memoria, tempSheet);
        }
        else
        {
            InternalDatabase.Instance.splitDatabase[ConstStrings.Memoria] = tempSheet;
        }        
    }

    /// <summary>
    /// Import Placa de Rede.csv into the internal database
    /// </summary>
    public void ImportPlacaDeRedeToDatabase(int numberOfColumns)
    {
        string[] data = placaDeRedeCSV.text.Split(new string[] { ",", "\n" }, StringSplitOptions.None);
        int tableSize = data.Length / numberOfColumns - 1; // it takes one off, because the first row is ignored
        Sheet tempSheet = new Sheet();
        tempSheet.itens = new List<ItemColumns>();

        for (int i = 0; i < tableSize; i++)
        {
            ItemColumns newRow = new ItemColumns();

            newRow.Modelo = data[numberOfColumns * (i + 1)];
            newRow.Fabricante = data[numberOfColumns * (i + 1) + 1];
            newRow.Interface = data[numberOfColumns * (i + 1) + 2];
            newRow.QuantidadeDePortas = data[numberOfColumns * (i + 1) + 3];
            newRow.QuaisConexoes = data[numberOfColumns * (i + 1) + 4];
            newRow.SuportaFibraOptica = data[numberOfColumns * (i + 1) + 5];
            newRow.Desempenho = data[numberOfColumns * (i + 1) + 6] + " Mb/s";
                        newRow.EstoqueAtual = data[numberOfColumns * (i + 1) + 7];

            tempSheet.itens.Add(newRow);
        }

        if (!InternalDatabase.Instance.splitDatabase.ContainsKey(ConstStrings.PlacaDeRede))
        {
            InternalDatabase.Instance.splitDatabase.Add(ConstStrings.PlacaDeRede, tempSheet);
        }
        else
        {
            InternalDatabase.Instance.splitDatabase[ConstStrings.PlacaDeRede] = tempSheet;
        }
    }

    /// <summary>
    /// Import iDrac.csv into the internal database
    /// </summary>
    public void ImportiDracToDatabase(int numberOfColumns)
    {
        string[] data = idracCSV.text.Split(new string[] { ",", "\n" }, StringSplitOptions.None);
        int tableSize = data.Length / numberOfColumns - 1; // it takes one off, because the first row is ignored
        Sheet tempSheet = new Sheet();
        tempSheet.itens = new List<ItemColumns>();

        for (int i = 0; i < tableSize; i++)
        {
            ItemColumns newRow = new ItemColumns();

            newRow.Modelo = data[numberOfColumns * (i + 1)];
            newRow.Fabricante = data[numberOfColumns * (i + 1) + 1];
            newRow.QuaisConexoes = data[numberOfColumns * (i + 1) + 2];
            newRow.QuantidadeDePortas = data[numberOfColumns * (i + 1) + 3];
            newRow.QuaisConexoes = data[numberOfColumns * (i + 1) + 4];
            newRow.EntradaSD = data[numberOfColumns * (i + 1) + 5];
            newRow.ServidoresSuportados = data[numberOfColumns * (i + 1) + 6];
                        newRow.EstoqueAtual = data[numberOfColumns * (i + 1) + 7];

            tempSheet.itens.Add(newRow);
        }

        if (!InternalDatabase.Instance.splitDatabase.ContainsKey(ConstStrings.Idrac))
        {
            InternalDatabase.Instance.splitDatabase.Add(ConstStrings.Idrac, tempSheet);
        }
        else
        {
            InternalDatabase.Instance.splitDatabase[ConstStrings.Idrac] = tempSheet;
        }
    }

    /// <summary>
    /// Import Placa controladora.csv into the internal database
    /// </summary>
    public void ImportPlacaControladoraToDatabase(int numberOfColumns)
    {
        string[] data = placaControladoraCSV.text.Split(new string[] { ",", "\n" }, StringSplitOptions.None);
        int tableSize = data.Length / numberOfColumns - 1; // it takes one off, because the first row is ignored
        Sheet tempSheet = new Sheet();
        tempSheet.itens = new List<ItemColumns>();

        for (int i = 0; i < tableSize; i++)
        {
            ItemColumns newRow = new ItemColumns();

            newRow.Modelo = data[numberOfColumns * (i + 1)];
            newRow.QuaisConexoes = data[numberOfColumns * (i + 1) + 1];
            newRow.QuantidadeDePortas = data[numberOfColumns * (i + 1) + 2];
            newRow.TipoDeRAID = data[numberOfColumns * (i + 1) + 3];
            newRow.TipoDeHD = data[numberOfColumns * (i + 1) + 4];
            newRow.CapacidadeMaxHD = data[numberOfColumns * (i + 1) + 5];
            newRow.AteQuantosHDs = data[numberOfColumns * (i + 1) + 6];
            newRow.BateriaInclusa = data[numberOfColumns * (i + 1) + 7];
            newRow.Barramento = data[numberOfColumns * (i + 1) + 8];
                        newRow.EstoqueAtual = data[numberOfColumns * (i + 1) + 9];

            tempSheet.itens.Add(newRow);
        }

        if (!InternalDatabase.Instance.splitDatabase.ContainsKey(ConstStrings.PlacaControladora))
        {
            InternalDatabase.Instance.splitDatabase.Add(ConstStrings.PlacaControladora, tempSheet);
        }
        else
        {
            InternalDatabase.Instance.splitDatabase[ConstStrings.PlacaControladora] = tempSheet;
        }
    }

    /// <summary>
    /// Import Processador.csv into the internal database
    /// </summary>
    public void ImportProcessadorToDatabase(int numberOfColumns)
    {
        string[] data = processadorCSV.text.Split(new string[] { ",", "\n" }, StringSplitOptions.None);
        int tableSize = data.Length / numberOfColumns - 1; // it takes one off, because the first row is ignored
        Sheet tempSheet = new Sheet();
        tempSheet.itens = new List<ItemColumns>();

        for (int i = 0; i < tableSize; i++)
        {
            ItemColumns newRow = new ItemColumns();

            newRow.Modelo = data[numberOfColumns * (i + 1)];
            newRow.Soquete = data[numberOfColumns * (i + 1) + 1];
            newRow.NucleosFisicos = data[numberOfColumns * (i + 1) + 2];
            newRow.NucleosLogicos = data[numberOfColumns * (i + 1) + 3];
            newRow.AceitaVirtualizacao = data[numberOfColumns * (i + 1) + 4];
            newRow.TurboBoost = data[numberOfColumns * (i + 1) + 5];
            newRow.HyperThreading = data[numberOfColumns * (i + 1) + 6];
            newRow.EstoqueAtual = data[numberOfColumns * (i + 1) + 7];

            tempSheet.itens.Add(newRow);
        }

        if (!InternalDatabase.Instance.splitDatabase.ContainsKey(ConstStrings.Processador))
        {
            InternalDatabase.Instance.splitDatabase.Add(ConstStrings.Processador, tempSheet);
        }
        else
        {
            InternalDatabase.Instance.splitDatabase[ConstStrings.Processador] = tempSheet;
        }
    }

    /// <summary>
    /// Import Desktop.csv into the internal database
    /// </summary>
    public void ImportDesktopToDatabase(int numberOfColumns)
    {
        string[] data = desktopCSV.text.Split(new string[] { ",", "\n" }, StringSplitOptions.None);
        int tableSize = data.Length / numberOfColumns - 1; // it takes one off, because the first row is ignored
        Sheet tempSheet = new Sheet();
        tempSheet.itens = new List<ItemColumns>();

        for (int i = 0; i < tableSize; i++)
        {
            ItemColumns newRow = new ItemColumns();

            newRow.Patrimonio = data[numberOfColumns * (i + 1)];
            newRow.ModeloPlacaMae = data[numberOfColumns * (i + 1) + 1];
            newRow.Fonte = data[numberOfColumns * (i + 1) + 2];
            newRow.Memoria = data[numberOfColumns * (i + 1) + 3];
            newRow.HD = data[numberOfColumns * (i + 1) + 4];
            newRow.PlacaDeVideo = data[numberOfColumns * (i + 1) + 5];
            newRow.PlacaDeRede = data[numberOfColumns * (i + 1) + 6];
            newRow.LeitorDeDVD = data[numberOfColumns * (i + 1) + 7];
            newRow.EstoqueAtual = data[numberOfColumns * (i + 1) + 8];

            tempSheet.itens.Add(newRow);
        }

        if (!InternalDatabase.Instance.splitDatabase.ContainsKey(ConstStrings.Desktop))
        {
            InternalDatabase.Instance.splitDatabase.Add(ConstStrings.Desktop, tempSheet);
        }
        else
        {
            InternalDatabase.Instance.splitDatabase[ConstStrings.Desktop] = tempSheet;
        }
    }

    /// <summary>
    /// Import Fonte.csv into the internal database
    /// </summary>
    public void ImportFonteToDatabase(int numberOfColumns)
    {
        string[] data = fonteCSV.text.Split(new string[] { ",", "\n" }, StringSplitOptions.None);
        int tableSize = data.Length / numberOfColumns - 1; // it takes one off, because the first row is ignored
        Sheet tempSheet = new Sheet();
        tempSheet.itens = new List<ItemColumns>();

        for (int i = 0; i < tableSize; i++)
        {
            ItemColumns newRow = new ItemColumns();

            newRow.Modelo = data[numberOfColumns * (i + 1)];
            newRow.Watts = data[numberOfColumns * (i + 1) + 1];
            newRow.OndeFunciona = data[numberOfColumns * (i + 1) + 2];
            newRow.Conectores = data[numberOfColumns * (i + 1) + 3];
            newRow.EstoqueAtual = data[numberOfColumns * (i + 1) + 4];           

            tempSheet.itens.Add(newRow);
        }

        if (!InternalDatabase.Instance.splitDatabase.ContainsKey(ConstStrings.Fonte))
        {
            InternalDatabase.Instance.splitDatabase.Add(ConstStrings.Fonte, tempSheet);
        }
        else
        {
            InternalDatabase.Instance.splitDatabase[ConstStrings.Fonte] = tempSheet;
        }
    }

    /// <summary>
    /// Import Switch.csv into the internal database
    /// </summary>
    public void ImportSwitchToDatabase(int numberOfColumns)
    {
        string[] data = switchCSV.text.Split(new string[] { ",", "\n" }, StringSplitOptions.None);
        int tableSize = data.Length / numberOfColumns - 1; // it takes one off, because the first row is ignored
        Sheet tempSheet = new Sheet();
        tempSheet.itens = new List<ItemColumns>();

        for (int i = 0; i < tableSize; i++)
        {
            ItemColumns newRow = new ItemColumns();

            newRow.Modelo = data[numberOfColumns * (i + 1)];
            newRow.QuantidadeDePortas = data[numberOfColumns * (i + 1) + 1];
            newRow.Desempenho = data[numberOfColumns * (i + 1) + 2] + " MB/s";
            newRow.EstoqueAtual = data[numberOfColumns * (i + 1) + 3];
           
            tempSheet.itens.Add(newRow);
        }

        if (!InternalDatabase.Instance.splitDatabase.ContainsKey(ConstStrings.Switch))
        {
            InternalDatabase.Instance.splitDatabase.Add(ConstStrings.Switch, tempSheet);
        }
        else
        {
            InternalDatabase.Instance.splitDatabase[ConstStrings.Switch] = tempSheet;
        }
    }

    /// <summary>
    /// Import Roteador.csv into the internal database
    /// </summary>
    public void ImportRoteadorToDatabase(int numberOfColumns)
    {
        string[] data = roteadorCSV.text.Split(new string[] { ",", "\n" }, StringSplitOptions.None);
        int tableSize = data.Length / numberOfColumns - 1; // it takes one off, because the first row is ignored
        Sheet tempSheet = new Sheet();
        tempSheet.itens = new List<ItemColumns>();

        for (int i = 0; i < tableSize; i++)
        {
            ItemColumns newRow = new ItemColumns();

            newRow.Modelo = data[numberOfColumns * (i + 1)];
            newRow.Wireless = data[numberOfColumns * (i + 1) + 1];
            newRow.QuantidadeDePortas = data[numberOfColumns * (i + 1) + 2];
            newRow.BandaMaxima = data[numberOfColumns * (i + 1) + 3];
            newRow.EstoqueAtual = data[numberOfColumns * (i + 1) + 4];
            
            tempSheet.itens.Add(newRow);
        }

        if (!InternalDatabase.Instance.splitDatabase.ContainsKey(ConstStrings.Roteador))
        {
            InternalDatabase.Instance.splitDatabase.Add(ConstStrings.Roteador, tempSheet);
        }
        else
        {
            InternalDatabase.Instance.splitDatabase[ConstStrings.Roteador] = tempSheet;
        }
    }

    /// <summary>
    /// Import Carregador.csv into the internal database
    /// </summary>
    public void ImportCarregadorToDatabase(int numberOfColumns)
    {
        string[] data = carregadorCSV.text.Split(new string[] { ",", "\n" }, StringSplitOptions.None);
        int tableSize = data.Length / numberOfColumns - 1; // it takes one off, because the first row is ignored
        Sheet tempSheet = new Sheet();
        tempSheet.itens = new List<ItemColumns>();

        for (int i = 0; i < tableSize; i++)
        {
            ItemColumns newRow = new ItemColumns();

            newRow.Modelo = data[numberOfColumns * (i + 1)];
            newRow.OndeFunciona = data[numberOfColumns * (i + 1) + 1];
            newRow.VoltagemDeSaida = data[numberOfColumns * (i + 1) + 2];
            newRow.AmperagemDeSaida = data[numberOfColumns * (i + 1) + 3];         
            newRow.EstoqueAtual = data[numberOfColumns * (i + 1) + 4];
            
            tempSheet.itens.Add(newRow);
        }

        if (!InternalDatabase.Instance.splitDatabase.ContainsKey(ConstStrings.Carregador))
        {
            InternalDatabase.Instance.splitDatabase.Add(ConstStrings.Carregador, tempSheet);
        }
        else
        {
            InternalDatabase.Instance.splitDatabase[ConstStrings.Carregador] = tempSheet;
        }
    }

    /// <summary>
    /// Import Adaptador AC.csv into the internal database
    /// </summary>
    public void ImportAdaptadorAcToDatabase(int numberOfColumns)
    {
        string[] data = adaptadorAcCSV.text.Split(new string[] { ",", "\n" }, StringSplitOptions.None);
        int tableSize = data.Length / numberOfColumns - 1; // it takes one off, because the first row is ignored
        Sheet tempSheet = new Sheet();
        tempSheet.itens = new List<ItemColumns>();

        for (int i = 0; i < tableSize; i++)
        {
            ItemColumns newRow = new ItemColumns();

            newRow.Modelo = data[numberOfColumns * (i + 1)];
            newRow.OndeFunciona = data[numberOfColumns * (i + 1) + 1];
            newRow.VoltagemDeSaida = data[numberOfColumns * (i + 1) + 2];
            newRow.AmperagemDeSaida = data[numberOfColumns * (i + 1) + 3];
                        newRow.EstoqueAtual = data[numberOfColumns * (i + 1) + 4];

            tempSheet.itens.Add(newRow);
        }

        if (!InternalDatabase.Instance.splitDatabase.ContainsKey(ConstStrings.AdaptadorAC))
        {
            InternalDatabase.Instance.splitDatabase.Add(ConstStrings.AdaptadorAC, tempSheet);
        }
        else
        {
            InternalDatabase.Instance.splitDatabase[ConstStrings.AdaptadorAC] = tempSheet;
        }
    }

    /// <summary>
    /// Import StorageNas.csv into the internal database
    /// </summary>
    public void ImportStorageNASToDatabase(int numberOfColumns)
    {
        string[] data = storageNasCSV.text.Split(new string[] { ",", "\n" }, StringSplitOptions.None);
        int tableSize = data.Length / numberOfColumns - 1; // it takes one off, because the first row is ignored
        Sheet tempSheet = new Sheet();
        tempSheet.itens = new List<ItemColumns>();

        for (int i = 0; i < tableSize; i++)
        {
            ItemColumns newRow = new ItemColumns();

            newRow.Modelo = data[numberOfColumns * (i + 1)];
            newRow.Tamanho = data[numberOfColumns * (i + 1) + 1];
            newRow.TipoDeRAID = data[numberOfColumns * (i + 1) + 2];
            newRow.TipoDeHD = data[numberOfColumns * (i + 1) + 3];
            newRow.CapacidadeMaxHD = data[numberOfColumns * (i + 1) + 4];
            newRow.AteQuantosHDs = data[numberOfColumns * (i + 1) + 5];
                       newRow.EstoqueAtual = data[numberOfColumns * (i + 1) + 6];

            tempSheet.itens.Add(newRow);
        }

        if (!InternalDatabase.Instance.splitDatabase.ContainsKey(ConstStrings.StorageNAS))
        {
            InternalDatabase.Instance.splitDatabase.Add(ConstStrings.StorageNAS, tempSheet);
        }
        else
        {
            InternalDatabase.Instance.splitDatabase[ConstStrings.StorageNAS] = tempSheet;
        }
    }

    /// <summary>
    /// Import Gbic.csv into the internal database
    /// </summary>
    public void ImportGBICToDatabase(int numberOfColumns)
    {
        string[] data = gbicCSV.text.Split(new string[] { ",", "\n" }, StringSplitOptions.None);
        int tableSize = data.Length / numberOfColumns - 1; // it takes one off, because the first row is ignored
        Sheet tempSheet = new Sheet();
        tempSheet.itens = new List<ItemColumns>();

        for (int i = 0; i < tableSize; i++)
        {
            ItemColumns newRow = new ItemColumns();

            newRow.Modelo = data[numberOfColumns * (i + 1)];
            newRow.Fabricante = data[numberOfColumns * (i + 1) + 1];
            newRow.Desempenho = data[numberOfColumns * (i + 1) + 2] + " GB";    
            newRow.EstoqueAtual = data[numberOfColumns * (i + 1) + 3];

            tempSheet.itens.Add(newRow);
        }

        if (!InternalDatabase.Instance.splitDatabase.ContainsKey(ConstStrings.Gbic))
        {
            InternalDatabase.Instance.splitDatabase.Add(ConstStrings.Gbic, tempSheet);
        }
        else
        {
            InternalDatabase.Instance.splitDatabase[ConstStrings.Gbic] = tempSheet;
        }
    }

    /// <summary>
    /// Import PlacaDeVideo.csv into the internal database
    /// </summary>
    public void ImportPlacaDeVideoToDatabase(int numberOfColumns)
    {
        string[] data = placaDeVideoCSV.text.Split(new string[] { ",", "\n" }, StringSplitOptions.None);
        int tableSize = data.Length / numberOfColumns - 1; // it takes one off, because the first row is ignored
        Sheet tempSheet = new Sheet();
        tempSheet.itens = new List<ItemColumns>();

        for (int i = 0; i < tableSize; i++)
        {
            ItemColumns newRow = new ItemColumns();

            newRow.Modelo = data[numberOfColumns * (i + 1)];
            newRow.QuantidadeDePortas = data[numberOfColumns * (i + 1) + 1];
            newRow.QuaisConexoes = data[numberOfColumns * (i + 1) + 2];
                        newRow.EstoqueAtual = data[numberOfColumns * (i + 1) + 3];

            tempSheet.itens.Add(newRow);
        }

        if (!InternalDatabase.Instance.splitDatabase.ContainsKey(ConstStrings.PlacaDeVideo))
        {
            InternalDatabase.Instance.splitDatabase.Add(ConstStrings.PlacaDeVideo, tempSheet);
        }
        else
        {
            InternalDatabase.Instance.splitDatabase[ConstStrings.PlacaDeVideo] = tempSheet;
        }
    }

    /// <summary>
    /// Import PlacaDeSom.csv into the internal database
    /// </summary>
    public void ImportPlacaDeSomToDatabase(int numberOfColumns)
    {
        string[] data = placaDeSomCSV.text.Split(new string[] { ",", "\n" }, StringSplitOptions.None);
        int tableSize = data.Length / numberOfColumns - 1; // it takes one off, because the first row is ignored
        Sheet tempSheet = new Sheet();
        tempSheet.itens = new List<ItemColumns>();

        for (int i = 0; i < tableSize; i++)
        {
            ItemColumns newRow = new ItemColumns();

            newRow.Modelo = data[numberOfColumns * (i + 1)];
            newRow.QuantosCanais = data[numberOfColumns * (i + 1) + 1];
                        newRow.EstoqueAtual = data[numberOfColumns * (i + 1) + 2];

            tempSheet.itens.Add(newRow);
        }

        if (!InternalDatabase.Instance.splitDatabase.ContainsKey(ConstStrings.PlacaDeSom))
        {
            InternalDatabase.Instance.splitDatabase.Add(ConstStrings.PlacaDeSom, tempSheet);
        }
        else
        {
            InternalDatabase.Instance.splitDatabase[ConstStrings.PlacaDeSom] = tempSheet;
        }
    }

    /// <summary>
    /// Import Placa de captura de video.csv into the internal database
    /// </summary>
    public void ImportPlacaDeCapturaDeVideoToDatabase(int numberOfColumns)
    {
        string[] data = placaDeCapturaDeVideoCSV.text.Split(new string[] { ",", "\n" }, StringSplitOptions.None);
        int tableSize = data.Length / numberOfColumns - 1; // it takes one off, because the first row is ignored
        Sheet tempSheet = new Sheet();
        tempSheet.itens = new List<ItemColumns>();

        for (int i = 0; i < tableSize; i++)
        {
            ItemColumns newRow = new ItemColumns();

            newRow.Modelo = data[numberOfColumns * (i + 1)];
            newRow.QuantidadeDePortas = data[numberOfColumns * (i + 1) + 1];
                        newRow.EstoqueAtual = data[numberOfColumns * (i + 1) + 2];

            tempSheet.itens.Add(newRow);
        }

        if (!InternalDatabase.Instance.splitDatabase.ContainsKey(ConstStrings.PlacaDeCapturaDeVideo))
        {
            InternalDatabase.Instance.splitDatabase.Add(ConstStrings.PlacaDeCapturaDeVideo, tempSheet);
        }
        else
        {
            InternalDatabase.Instance.splitDatabase[ConstStrings.PlacaDeCapturaDeVideo] = tempSheet;
        }
    }
    #endregion

    /// <summary>
    /// Closes all panels and open the selected panel
    /// </summary>
   }