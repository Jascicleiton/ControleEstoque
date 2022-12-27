using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
using SimpleJSON;

public class InventarioManager : Singleton<InventarioManager>
{
    void Start()
    {
        DontDestroyOnLoad(this.gameObject);
        ImportSheets();
    }

    /// <summary>
    /// Import all sheets to internal database
    /// </summary>
    public void ImportSheets()
    {
        StartCoroutine(ImportInventarioToDatabase());
        StartCoroutine(ImportHDSheetToDatabase());
        StartCoroutine(ImportMemoriaToDatabase());
        StartCoroutine(ImportPlacaDeRedeToDatabase());
        StartCoroutine(ImportiDracToDatabase());
        StartCoroutine(ImportPlacaControladoraToDatabase());
        StartCoroutine(ImportProcessadorToDatabase());
        StartCoroutine(ImportDesktopToDatabase());
        StartCoroutine(ImportFonteToDatabase());
        StartCoroutine(ImportSwitchToDatabase());
        StartCoroutine(ImportRoteadorToDatabase());
        StartCoroutine(ImportCarregadorToDatabase());
        StartCoroutine(ImportAdaptadorAcToDatabase());
        StartCoroutine(ImportStorageNASToDatabase());
        StartCoroutine(ImportGBICToDatabase());
        StartCoroutine(ImportPlacaDeVideoToDatabase());
        StartCoroutine(ImportPlacaDeSomToDatabase());
        StartCoroutine(ImportPlacaDeCapturaDeVideoToDatabase());
        StartCoroutine(ImportServidorToDatabase());
        StartCoroutine(ImportNotebookToDatabase());
        StartCoroutine(ImportMonitorToDatabase());
    }

    #region Import all tables to internal database
    /// <summary>
    /// Import inventario table from server into the internal database
    /// </summary>
    private IEnumerator ImportInventarioToDatabase()
    {
        WWWForm getInventario = new WWWForm();
        getInventario.AddField("apppassword", "ImportDatabase");

        UnityWebRequest getInventarioRequest = UnityWebRequest.Post(ConstStrings.PhpImportTablesFolder + "importinventario.php", getInventario);
        yield return getInventarioRequest.SendWebRequest();

        Sheet tempSheet = new Sheet();
        tempSheet.itens = new List<ItemColumns>();

        if (getInventarioRequest.result == UnityWebRequest.Result.ConnectionError)
        {
            Debug.LogWarning("conectionerror");
        }
        else if (getInventarioRequest.result == UnityWebRequest.Result.DataProcessingError)
        {
            Debug.LogWarning("data processing error");
        }
        else if (getInventarioRequest.result == UnityWebRequest.Result.ProtocolError)
        {
            Debug.LogWarning("protocol error");
        }
        if (getInventarioRequest.error == null)
        {
            string response = getInventarioRequest.downloadHandler.text;
            if (response == "1")
            {
                Debug.Log("DAtabase connection error");
            }
            else if (response == "2")
            {
                Debug.Log("Table query ran into an error");
            }
            else if (response == "3")
            {
                Debug.Log("Result came empty");
            }
            else
            {
                JSONNode inventario = JSON.Parse(getInventarioRequest.downloadHandler.text);
                foreach (JSONNode item in inventario)
                {
                    ItemColumns newRow = new ItemColumns();

                    newRow.Entrada = item[0];
                    newRow.Patrimonio = item[1];
                    newRow.Status = item[2];
                    newRow.Serial = item[3];
                    newRow.Categoria = item[4];
                    newRow.Fabricante = item[5];
                    newRow.Modelo = item[6];
                    newRow.Local = item[7];
                    newRow.Saida = item[8];
                    newRow.Observacao = item[9];
                    tempSheet.itens.Add(newRow);
                }
            }
        }
        else
        {
            Debug.LogWarning(getInventarioRequest.error);
        }

        getInventarioRequest.Dispose();
        if (!InternalDatabase.Instance.splitDatabase.ContainsKey(ConstStrings.InventarioSnPro))
        {
            InternalDatabase.Instance.splitDatabase.Add(ConstStrings.InventarioSnPro, tempSheet);
        }
        else
        {
            InternalDatabase.Instance.splitDatabase[ConstStrings.InventarioSnPro] = tempSheet;
        }

    }

    /// <summary>
    /// Import HD table from server into the internal database
    /// </summary>
    private IEnumerator ImportHDSheetToDatabase()
    {
        WWWForm getInventario = new WWWForm();
        getInventario.AddField("apppassword", "ImportDatabase");

        UnityWebRequest getInventarioRequest = UnityWebRequest.Post(ConstStrings.PhpImportTablesFolder + "importhd.php", getInventario);
        yield return getInventarioRequest.SendWebRequest();

        Sheet tempSheet = new Sheet();
        tempSheet.itens = new List<ItemColumns>();

        if (getInventarioRequest.result == UnityWebRequest.Result.ConnectionError)
        {
            Debug.LogWarning("conection error");
        }
        else if (getInventarioRequest.result == UnityWebRequest.Result.DataProcessingError)
        {
            Debug.LogWarning("data processing error");
        }
        else if (getInventarioRequest.result == UnityWebRequest.Result.ProtocolError)
        {
            Debug.LogWarning("protocol error");
        }
        if (getInventarioRequest.error == null)
        {
            string response = getInventarioRequest.downloadHandler.text;
            if (response == "1")
            {
                Debug.Log("DAtabase connection error");
            }
            else if (response == "2")
            {
                Debug.Log("Table query ran into an error");
            }
            else if (response == "3")
            {
                Debug.Log("Result came empty");
            }
            else
            {
                JSONNode inventario = JSON.Parse(getInventarioRequest.downloadHandler.text);
                foreach (JSONNode item in inventario)
                {
                    ItemColumns newRow = new ItemColumns();

                    newRow.Modelo = item[0];
                    newRow.Fabricante = item[1];
                    newRow.Interface = item[2];
                    newRow.Tamanho = item[3];
                    newRow.FormaDeArmazenamento = item[4];
                    newRow.CapacidadeEmGB = item[5];
                    newRow.RPM = item[6];
                    newRow.VelocidadeDeLeitura = item[7];
                    newRow.Enterprise = item[8];
                    newRow.EstoqueAtual = item[9];
                    newRow.Categoria = ConstStrings.HD;
                    tempSheet.itens.Add(newRow);
                }
            }
        }
        else
        {
            Debug.LogWarning(getInventarioRequest.error);
        }

        getInventarioRequest.Dispose();

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
    private IEnumerator ImportMemoriaToDatabase()
    {
        WWWForm getInventario = new WWWForm();
        getInventario.AddField("apppassword", "ImportDatabase");

        UnityWebRequest getInventarioRequest = UnityWebRequest.Post(ConstStrings.PhpImportTablesFolder + "importmemoria.php", getInventario);
        yield return getInventarioRequest.SendWebRequest();

        Sheet tempSheet = new Sheet();
        tempSheet.itens = new List<ItemColumns>();

        if (getInventarioRequest.result == UnityWebRequest.Result.ConnectionError)
        {
            Debug.LogWarning("conection error");
        }
        else if (getInventarioRequest.result == UnityWebRequest.Result.DataProcessingError)
        {
            Debug.LogWarning("data processing error");
        }
        else if (getInventarioRequest.result == UnityWebRequest.Result.ProtocolError)
        {
            Debug.LogWarning("protocol error");
        }
        if (getInventarioRequest.error == null)
        {
            string response = getInventarioRequest.downloadHandler.text;
            if (response == "1")
            {
                Debug.Log("DAtabase connection error");
            }
            else if (response == "2")
            {
                Debug.Log("Table query ran into an error");
            }
            else if (response == "3")
            {
                Debug.Log("Result came empty");
            }
            else
            {
                JSONNode inventario = JSON.Parse(getInventarioRequest.downloadHandler.text);
                foreach (JSONNode item in inventario)
                {
                    ItemColumns newRow = new ItemColumns();

                    newRow.Modelo = item[0];
                    newRow.Fabricante = item[1];
                    newRow.Tipo = item[2];
                    newRow.CapacidadeEmGB = item[3];
                    newRow.VelocidadeMHz = item[4];
                    newRow.LowVoltage = item[5];
                    newRow.Rank = item[6];
                    newRow.DIMM = item[7];
                    newRow.TaxaDeTransmissao = item[8];
                    newRow.Simbolo = item[9];
                    newRow.EstoqueAtual = item[10];
                    newRow.Categoria = ConstStrings.Memoria;

                    tempSheet.itens.Add(newRow);
                }
            }
        }
        getInventarioRequest.Dispose();
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
    private IEnumerator ImportPlacaDeRedeToDatabase()
    {
        WWWForm getInventario = new WWWForm();
        getInventario.AddField("apppassword", "ImportDatabase");

        UnityWebRequest getInventarioRequest = UnityWebRequest.Post(ConstStrings.PhpImportTablesFolder + "importplacaderede.php", getInventario);
        yield return getInventarioRequest.SendWebRequest();

        Sheet tempSheet = new Sheet();
        tempSheet.itens = new List<ItemColumns>();

        if (getInventarioRequest.result == UnityWebRequest.Result.ConnectionError)
        {
            Debug.LogWarning("conection error");
        }
        else if (getInventarioRequest.result == UnityWebRequest.Result.DataProcessingError)
        {
            Debug.LogWarning("data processing error");
        }
        else if (getInventarioRequest.result == UnityWebRequest.Result.ProtocolError)
        {
            Debug.LogWarning("protocol error");
        }
        if (getInventarioRequest.error == null)
        {
            string response = getInventarioRequest.downloadHandler.text;
            if (response == "1")
            {
                Debug.Log("Database connection error");
            }
            else if (response == "2")
            {
                Debug.Log("Table query ran into an error");
            }
            else if (response == "3")
            {
                Debug.Log("Result came empty");
            }
            else
            {
                JSONNode inventario = JSON.Parse(getInventarioRequest.downloadHandler.text);
                foreach (JSONNode item in inventario)
                {
                    ItemColumns newRow = new ItemColumns();

                    newRow.Modelo = item[0];
                    newRow.Fabricante = item[1];
                    newRow.Interface = item[2];
                    newRow.QuantidadeDePortas = item[3];
                    newRow.QuaisConexoes = item[4];
                    newRow.SuportaFibraOptica = item[5];
                    newRow.Desempenho = item[6];
                    newRow.EstoqueAtual = item[7];
                    newRow.Categoria = ConstStrings.PlacaDeRede;

                    tempSheet.itens.Add(newRow);
                }
            }
        }

        getInventarioRequest.Dispose();

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
    private IEnumerator ImportiDracToDatabase()
    {
        WWWForm getInventario = new WWWForm();
        getInventario.AddField("apppassword", "ImportDatabase");

        UnityWebRequest getInventarioRequest = UnityWebRequest.Post(ConstStrings.PhpImportTablesFolder + "importidrac.php", getInventario);
        yield return getInventarioRequest.SendWebRequest();

        Sheet tempSheet = new Sheet();
        tempSheet.itens = new List<ItemColumns>();

        if (getInventarioRequest.result == UnityWebRequest.Result.ConnectionError)
        {
            Debug.LogWarning("conection error");
        }
        else if (getInventarioRequest.result == UnityWebRequest.Result.DataProcessingError)
        {
            Debug.LogWarning("data processing error");
        }
        else if (getInventarioRequest.result == UnityWebRequest.Result.ProtocolError)
        {
            Debug.LogWarning("protocol error");
        }
        if (getInventarioRequest.error == null)
        {
            string response = getInventarioRequest.downloadHandler.text;
            if (response == "1")
            {
                Debug.Log("Database connection error");
            }
            else if (response == "2")
            {
                Debug.Log("Table query ran into an error");
            }
            else if (response == "3")
            {
                Debug.Log("Result came empty");
            }
            else
            {
                JSONNode inventario = JSON.Parse(getInventarioRequest.downloadHandler.text);
                foreach (JSONNode item in inventario)
                {
                    ItemColumns newRow = new ItemColumns();

                    newRow.Modelo = item[0];
                    newRow.Fabricante = item[1];
                    newRow.QuaisConexoes = item[2];
                    newRow.VelocidadeGBs = item[3];
                    newRow.EntradaSD = item[4];
                    newRow.ServidoresSuportados = item[5];
                    newRow.EstoqueAtual = item[6];
                    newRow.Categoria = ConstStrings.Idrac;

                    tempSheet.itens.Add(newRow);
                }
            }
        }

        getInventarioRequest.Dispose();

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
    private IEnumerator ImportPlacaControladoraToDatabase()
    {
        WWWForm getInventario = new WWWForm();
        getInventario.AddField("apppassword", "ImportDatabase");

        UnityWebRequest getInventarioRequest = UnityWebRequest.Post(ConstStrings.PhpImportTablesFolder + "importplacacontroladora.php", getInventario);
        yield return getInventarioRequest.SendWebRequest();

        Sheet tempSheet = new Sheet();
        tempSheet.itens = new List<ItemColumns>();

        if (getInventarioRequest.result == UnityWebRequest.Result.ConnectionError)
        {
            Debug.LogWarning("conection error");
        }
        else if (getInventarioRequest.result == UnityWebRequest.Result.DataProcessingError)
        {
            Debug.LogWarning("data processing error");
        }
        else if (getInventarioRequest.result == UnityWebRequest.Result.ProtocolError)
        {
            Debug.LogWarning("protocol error");
        }
        if (getInventarioRequest.error == null)
        {
            string response = getInventarioRequest.downloadHandler.text;
            if (response == "1")
            {
                Debug.Log("Database connection error");
            }
            else if (response == "2")
            {
                Debug.Log("Table query ran into an error");
            }
            else if (response == "3")
            {
                Debug.Log("Result came empty");
            }
            else
            {
                JSONNode inventario = JSON.Parse(getInventarioRequest.downloadHandler.text);
                foreach (JSONNode item in inventario)
                {
                    ItemColumns newRow = new ItemColumns();

                    newRow.Modelo = item[0];
                    newRow.QuaisConexoes = item[1];
                    newRow.QuantidadeDePortas = item[2];
                    newRow.TipoDeRAID = item[3];
                    newRow.TipoDeHD = item[4];
                    newRow.CapacidadeMaxHD = item[5];
                    newRow.AteQuantosHDs = item[6];
                    newRow.BateriaInclusa = item[7];
                    newRow.Barramento = item[8];
                    newRow.EstoqueAtual = item[9];
                    newRow.Categoria = ConstStrings.PlacaControladora;

                    tempSheet.itens.Add(newRow);
                }
            }
        }
        getInventarioRequest.Dispose();
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
    private IEnumerator ImportProcessadorToDatabase()
    {
        WWWForm getInventario = new WWWForm();
        getInventario.AddField("apppassword", "ImportDatabase");

        UnityWebRequest getInventarioRequest = UnityWebRequest.Post(ConstStrings.PhpImportTablesFolder + "importprocessador.php", getInventario);
        yield return getInventarioRequest.SendWebRequest();

        Sheet tempSheet = new Sheet();
        tempSheet.itens = new List<ItemColumns>();

        if (getInventarioRequest.result == UnityWebRequest.Result.ConnectionError)
        {
            Debug.LogWarning("conection error");
        }
        else if (getInventarioRequest.result == UnityWebRequest.Result.DataProcessingError)
        {
            Debug.LogWarning("data processing error");
        }
        else if (getInventarioRequest.result == UnityWebRequest.Result.ProtocolError)
        {
            Debug.LogWarning("protocol error");
        }
        if (getInventarioRequest.error == null)
        {
            string response = getInventarioRequest.downloadHandler.text;
            if (response == "1")
            {
                Debug.Log("Database connection error");
            }
            else if (response == "2")
            {
                Debug.Log("Table query ran into an error");
            }
            else if (response == "3")
            {
                Debug.Log("Result came empty");
            }
            else
            {
                JSONNode inventario = JSON.Parse(getInventarioRequest.downloadHandler.text);
                foreach (JSONNode item in inventario)
                {
                    ItemColumns newRow = new ItemColumns();

                    newRow.Modelo = item[0];
                    newRow.Soquete = item[1];
                    newRow.NucleosFisicos = item[2];
                    newRow.NucleosLogicos = item[3];
                    newRow.AceitaVirtualizacao = item[4];
                    newRow.TurboBoost = item[5];
                    newRow.HyperThreading = item[6];
                    newRow.EstoqueAtual = item[7];
                    newRow.Categoria = ConstStrings.Processador;

                    tempSheet.itens.Add(newRow);
                }
            }
        }
        getInventarioRequest.Dispose();
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
    private IEnumerator ImportDesktopToDatabase()
    {
        WWWForm getInventario = new WWWForm();
        getInventario.AddField("apppassword", "ImportDatabase");

        UnityWebRequest getInventarioRequest = UnityWebRequest.Post(ConstStrings.PhpImportTablesFolder + "importdesktop.php", getInventario);
        yield return getInventarioRequest.SendWebRequest();

        Sheet tempSheet = new Sheet();
        tempSheet.itens = new List<ItemColumns>();

        if (getInventarioRequest.result == UnityWebRequest.Result.ConnectionError)
        {
            Debug.LogWarning("conection error");
        }
        else if (getInventarioRequest.result == UnityWebRequest.Result.DataProcessingError)
        {
            Debug.LogWarning("data processing error");
        }
        else if (getInventarioRequest.result == UnityWebRequest.Result.ProtocolError)
        {
            Debug.LogWarning("protocol error");
        }
        if (getInventarioRequest.error == null)
        {
            string response = getInventarioRequest.downloadHandler.text;
            if (response == "1")
            {
                Debug.Log("Database connection error");
            }
            else if (response == "2")
            {
                Debug.Log("Table query ran into an error");
            }
            else if (response == "3")
            {
                Debug.Log("Result came empty");
            }
            else
            {
                JSONNode inventario = JSON.Parse(getInventarioRequest.downloadHandler.text);
                foreach (JSONNode item in inventario)
                {
                    ItemColumns newRow = new ItemColumns();

                    newRow.Patrimonio = item[0];
                    newRow.ModeloPlacaMae = item[1];
                    newRow.Fonte = item[2];
                    newRow.Memoria = item[3];
                    newRow.HD = item[4];
                    newRow.PlacaDeVideo = item[5];
                    newRow.PlacaDeRede = item[6];
                    newRow.LeitorDeDVD = item[7];
                    newRow.Processador = item[8];
                    newRow.EstoqueAtual = item[9];
                    newRow.Categoria = ConstStrings.Desktop;

                    tempSheet.itens.Add(newRow);
                }
            }
        }

        getInventarioRequest.Dispose();

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
    private IEnumerator ImportFonteToDatabase()
    {
        WWWForm getInventario = new WWWForm();
        getInventario.AddField("apppassword", "ImportDatabase");

        UnityWebRequest getInventarioRequest = UnityWebRequest.Post(ConstStrings.PhpImportTablesFolder + "importfonte.php", getInventario);
        yield return getInventarioRequest.SendWebRequest();

        Sheet tempSheet = new Sheet();
        tempSheet.itens = new List<ItemColumns>();

        if (getInventarioRequest.result == UnityWebRequest.Result.ConnectionError)
        {
            Debug.LogWarning("conection error");
        }
        else if (getInventarioRequest.result == UnityWebRequest.Result.DataProcessingError)
        {
            Debug.LogWarning("data processing error");
        }
        else if (getInventarioRequest.result == UnityWebRequest.Result.ProtocolError)
        {
            Debug.LogWarning("protocol error");
        }
        if (getInventarioRequest.error == null)
        {
            string response = getInventarioRequest.downloadHandler.text;
            if (response == "1")
            {
                Debug.Log("Database connection error");
            }
            else if (response == "2")
            {
                Debug.Log("Table query ran into an error");
            }
            else if (response == "3")
            {
                Debug.Log("Result came empty");
            }
            else
            {
                JSONNode inventario = JSON.Parse(getInventarioRequest.downloadHandler.text);
                foreach (JSONNode item in inventario)
                {
                    ItemColumns newRow = new ItemColumns();

                    newRow.Modelo = item[0];
                    newRow.Watts = item[1];
                    newRow.OndeFunciona = item[2];
                    newRow.Conectores = item[3];
                    newRow.EstoqueAtual = item[4];
                    newRow.Categoria = ConstStrings.Fonte;

                    tempSheet.itens.Add(newRow);
                }
            }
        }

        getInventarioRequest.Dispose();

        if (!InternalDatabase.Instance.splitDatabase.ContainsKey(ConstStrings.Fonte))
        {
            InternalDatabase.Instance.splitDatabase.Add(ConstStrings.Fonte, tempSheet);
        }
        else
        {
            InternalDatabase.Instance.splitDatabase[ConstStrings.Fonte] = tempSheet;
        }
    }

    ///// <summary>
    ///// Import Switch.csv into the internal database
    ///// </summary>
    private IEnumerator ImportSwitchToDatabase()
    {
        WWWForm getInventario = new WWWForm();
        getInventario.AddField("apppassword", "ImportDatabase");

        UnityWebRequest getInventarioRequest = UnityWebRequest.Post(ConstStrings.PhpImportTablesFolder + "importswitch.php", getInventario);
        yield return getInventarioRequest.SendWebRequest();

        Sheet tempSheet = new Sheet();
        tempSheet.itens = new List<ItemColumns>();

        if (getInventarioRequest.result == UnityWebRequest.Result.ConnectionError)
        {
            Debug.LogWarning("conection error");
        }
        else if (getInventarioRequest.result == UnityWebRequest.Result.DataProcessingError)
        {
            Debug.LogWarning("data processing error");
        }
        else if (getInventarioRequest.result == UnityWebRequest.Result.ProtocolError)
        {
            Debug.LogWarning("protocol error");
        }
        if (getInventarioRequest.error == null)
        {
            string response = getInventarioRequest.downloadHandler.text;
            if (response == "1")
            {
                Debug.Log("Database connection error");
            }
            else if (response == "2")
            {
                Debug.Log("Table query ran into an error");
            }
            else if (response == "3")
            {
                Debug.Log("Result came empty");
            }
            else
            {
                JSONNode inventario = JSON.Parse(getInventarioRequest.downloadHandler.text);
                foreach (JSONNode item in inventario)
                {
                    ItemColumns newRow = new ItemColumns();

                    newRow.Modelo = item[0];
                    newRow.QuantidadeDePortas = item[1];
                    newRow.Desempenho = item[2];
                    newRow.EstoqueAtual = item[3];
                    newRow.Categoria = ConstStrings.Switch;

                    tempSheet.itens.Add(newRow);
                }
            }
        }

        getInventarioRequest.Dispose();

        if (!InternalDatabase.Instance.splitDatabase.ContainsKey(ConstStrings.Switch))
        {
            InternalDatabase.Instance.splitDatabase.Add(ConstStrings.Switch, tempSheet);
        }
        else
        {
            InternalDatabase.Instance.splitDatabase[ConstStrings.Switch] = tempSheet;
        }
    }

    ///// <summary>
    ///// Import Roteador.csv into the internal database
    ///// </summary>
    private IEnumerator ImportRoteadorToDatabase()
    {
        WWWForm getInventario = new WWWForm();
        getInventario.AddField("apppassword", "ImportDatabase");

        UnityWebRequest getInventarioRequest = UnityWebRequest.Post(ConstStrings.PhpImportTablesFolder + "importroteador.php", getInventario);
        yield return getInventarioRequest.SendWebRequest();

        Sheet tempSheet = new Sheet();
        tempSheet.itens = new List<ItemColumns>();

        if (getInventarioRequest.result == UnityWebRequest.Result.ConnectionError)
        {
            Debug.LogWarning("conection error");
        }
        else if (getInventarioRequest.result == UnityWebRequest.Result.DataProcessingError)
        {
            Debug.LogWarning("data processing error");
        }
        else if (getInventarioRequest.result == UnityWebRequest.Result.ProtocolError)
        {
            Debug.LogWarning("protocol error");
        }
        if (getInventarioRequest.error == null)
        {
            string response = getInventarioRequest.downloadHandler.text;
            if (response == "1")
            {
                Debug.Log("Database connection error");
            }
            else if (response == "2")
            {
                Debug.Log("Table query ran into an error");
            }
            else if (response == "3")
            {
                Debug.Log("Result came empty");
            }
            else
            {
                JSONNode inventario = JSON.Parse(getInventarioRequest.downloadHandler.text);
                foreach (JSONNode item in inventario)
                {
                    ItemColumns newRow = new ItemColumns();

                    newRow.Modelo = item[0];
                    newRow.Wireless = item[1];
                    newRow.QuantidadeDePortas = item[2];
                    newRow.BandaMaxima = item[3];
                    newRow.VoltagemDeSaida = item[4];
                    newRow.EstoqueAtual = item[5];
                    newRow.Categoria = ConstStrings.Roteador;

                    tempSheet.itens.Add(newRow);
                }
            }
        }

        getInventarioRequest.Dispose();

        if (!InternalDatabase.Instance.splitDatabase.ContainsKey(ConstStrings.Roteador))
        {
            InternalDatabase.Instance.splitDatabase.Add(ConstStrings.Roteador, tempSheet);
        }
        else
        {
            InternalDatabase.Instance.splitDatabase[ConstStrings.Roteador] = tempSheet;
        }
    }

    ///// <summary>
    ///// Import Carregador.csv into the internal database
    ///// </summary>
    private IEnumerator ImportCarregadorToDatabase()
    {
        WWWForm getInventario = new WWWForm();
        getInventario.AddField("apppassword", "ImportDatabase");

        UnityWebRequest getInventarioRequest = UnityWebRequest.Post(ConstStrings.PhpImportTablesFolder + "importcarregador.php", getInventario);
        yield return getInventarioRequest.SendWebRequest();

        Sheet tempSheet = new Sheet();
        tempSheet.itens = new List<ItemColumns>();

        if (getInventarioRequest.result == UnityWebRequest.Result.ConnectionError)
        {
            Debug.LogWarning("conection error");
        }
        else if (getInventarioRequest.result == UnityWebRequest.Result.DataProcessingError)
        {
            Debug.LogWarning("data processing error");
        }
        else if (getInventarioRequest.result == UnityWebRequest.Result.ProtocolError)
        {
            Debug.LogWarning("protocol error");
        }
        if (getInventarioRequest.error == null)
        {
            string response = getInventarioRequest.downloadHandler.text;
            if (response == "1")
            {
                Debug.Log("Database connection error");
            }
            else if (response == "2")
            {
                Debug.Log("Table query ran into an error");
            }
            else if (response == "3")
            {
                Debug.Log("Result came empty");
            }
            else
            {
                JSONNode inventario = JSON.Parse(getInventarioRequest.downloadHandler.text);
                foreach (JSONNode item in inventario)
                {
                    ItemColumns newRow = new ItemColumns();

                    newRow.Modelo = item[0];
                    newRow.OndeFunciona = item[1];
                    newRow.VoltagemDeSaida = item[2];
                    newRow.AmperagemDeSaida = item[3];
                    newRow.EstoqueAtual = item[4];
                    newRow.Categoria = ConstStrings.Carregador;

                    tempSheet.itens.Add(newRow);
                }
            }
        }

        getInventarioRequest.Dispose();

        if (!InternalDatabase.Instance.splitDatabase.ContainsKey(ConstStrings.Carregador))
        {
            InternalDatabase.Instance.splitDatabase.Add(ConstStrings.Carregador, tempSheet);
        }
        else
        {
            InternalDatabase.Instance.splitDatabase[ConstStrings.Carregador] = tempSheet;
        }
    }

    ///// <summary>
    ///// Import Adaptador AC.csv into the internal database
    ///// </summary>
    private IEnumerator ImportAdaptadorAcToDatabase()
    {
        WWWForm getInventario = new WWWForm();
        getInventario.AddField("apppassword", "ImportDatabase");

        UnityWebRequest getInventarioRequest = UnityWebRequest.Post(ConstStrings.PhpImportTablesFolder + "importadaptadorac.php", getInventario);
        yield return getInventarioRequest.SendWebRequest();

        Sheet tempSheet = new Sheet();
        tempSheet.itens = new List<ItemColumns>();

        if (getInventarioRequest.result == UnityWebRequest.Result.ConnectionError)
        {
            Debug.LogWarning("conection error");
        }
        else if (getInventarioRequest.result == UnityWebRequest.Result.DataProcessingError)
        {
            Debug.LogWarning("data processing error");
        }
        else if (getInventarioRequest.result == UnityWebRequest.Result.ProtocolError)
        {
            Debug.LogWarning("protocol error");
        }
        if (getInventarioRequest.error == null)
        {
            string response = getInventarioRequest.downloadHandler.text;
            if (response == "1")
            {
                Debug.Log("Database connection error");
            }
            else if (response == "2")
            {
                Debug.Log("Table query ran into an error");
            }
            else if (response == "3")
            {
                Debug.Log("Result came empty");
            }
            else
            {
                JSONNode inventario = JSON.Parse(getInventarioRequest.downloadHandler.text);
                foreach (JSONNode item in inventario)
                {
                    ItemColumns newRow = new ItemColumns();

                    newRow.Modelo = item[0];
                    newRow.OndeFunciona = item[1];
                    newRow.VoltagemDeSaida = item[2];
                    newRow.AmperagemDeSaida = item[3];
                    newRow.EstoqueAtual = item[4];
                    newRow.Categoria = ConstStrings.AdaptadorAC;

                    tempSheet.itens.Add(newRow);
                }
            }
        }
        getInventarioRequest.Dispose();

        if (!InternalDatabase.Instance.splitDatabase.ContainsKey(ConstStrings.AdaptadorAC))
        {
            InternalDatabase.Instance.splitDatabase.Add(ConstStrings.AdaptadorAC, tempSheet);
        }
        else
        {
            InternalDatabase.Instance.splitDatabase[ConstStrings.AdaptadorAC] = tempSheet;
        }
    }

    ///// <summary>
    ///// Import StorageNas.csv into the internal database
    ///// </summary>
    private IEnumerator ImportStorageNASToDatabase()
    {
        WWWForm getInventario = new WWWForm();
        getInventario.AddField("apppassword", "ImportDatabase");

        UnityWebRequest getInventarioRequest = UnityWebRequest.Post(ConstStrings.PhpImportTablesFolder + "importstoragenas.php", getInventario);
        yield return getInventarioRequest.SendWebRequest();

        Sheet tempSheet = new Sheet();
        tempSheet.itens = new List<ItemColumns>();

        if (getInventarioRequest.result == UnityWebRequest.Result.ConnectionError)
        {
            Debug.LogWarning("conection error");
        }
        else if (getInventarioRequest.result == UnityWebRequest.Result.DataProcessingError)
        {
            Debug.LogWarning("data processing error");
        }
        else if (getInventarioRequest.result == UnityWebRequest.Result.ProtocolError)
        {
            Debug.LogWarning("protocol error");
        }
        if (getInventarioRequest.error == null)
        {
            string response = getInventarioRequest.downloadHandler.text;
            if (response == "1")
            {
                Debug.Log("Database connection error");
            }
            else if (response == "2")
            {
                Debug.Log("Table query ran into an error");
            }
            else if (response == "3")
            {
                Debug.Log("Result came empty");
            }
            else
            {
                JSONNode inventario = JSON.Parse(getInventarioRequest.downloadHandler.text);
                foreach (JSONNode item in inventario)
                {
                    ItemColumns newRow = new ItemColumns();

                    newRow.Modelo = item[0];
                    newRow.Tamanho = item[1];
                    newRow.TipoDeRAID = item[2];
                    newRow.TipoDeHD = item[3];
                    newRow.CapacidadeMaxHD = item[4];
                    newRow.AteQuantosHDs = item[5];
                    newRow.EstoqueAtual = item[6];
                    newRow.Categoria = ConstStrings.StorageNAS;

                    tempSheet.itens.Add(newRow);
                }
            }
        }

        getInventarioRequest.Dispose();

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
    private IEnumerator ImportGBICToDatabase()
    {
        WWWForm getInventario = new WWWForm();
        getInventario.AddField("apppassword", "ImportDatabase");

        UnityWebRequest getInventarioRequest = UnityWebRequest.Post(ConstStrings.PhpImportTablesFolder + "importgbic.php", getInventario);
        yield return getInventarioRequest.SendWebRequest();

        Sheet tempSheet = new Sheet();
        tempSheet.itens = new List<ItemColumns>();

        if (getInventarioRequest.result == UnityWebRequest.Result.ConnectionError)
        {
            Debug.LogWarning("conection error");
        }
        else if (getInventarioRequest.result == UnityWebRequest.Result.DataProcessingError)
        {
            Debug.LogWarning("data processing error");
        }
        else if (getInventarioRequest.result == UnityWebRequest.Result.ProtocolError)
        {
            Debug.LogWarning("protocol error");
        }
        if (getInventarioRequest.error == null)
        {
            string response = getInventarioRequest.downloadHandler.text;
            if (response == "1")
            {
                Debug.Log("Database connection error");
            }
            else if (response == "2")
            {
                Debug.Log("Table query ran into an error");
            }
            else if (response == "3")
            {
                Debug.Log("Result came empty");
            }
            else
            {
                JSONNode inventario = JSON.Parse(getInventarioRequest.downloadHandler.text);
                foreach (JSONNode item in inventario)
                {
                    ItemColumns newRow = new ItemColumns();

                    newRow.Modelo = item[0];
                    newRow.Fabricante = item[1];
                    newRow.Desempenho = item[2];
                    newRow.EstoqueAtual = item[3];
                    newRow.Categoria = ConstStrings.Gbic;

                    tempSheet.itens.Add(newRow);
                }
            }
        }
        getInventarioRequest.Dispose();

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
    private IEnumerator ImportPlacaDeVideoToDatabase()
    {
        WWWForm getInventario = new WWWForm();
        getInventario.AddField("apppassword", "ImportDatabase");

        UnityWebRequest getInventarioRequest = UnityWebRequest.Post(ConstStrings.PhpImportTablesFolder + "importplacadevideo.php", getInventario);
        yield return getInventarioRequest.SendWebRequest();

        Sheet tempSheet = new Sheet();
        tempSheet.itens = new List<ItemColumns>();

        if (getInventarioRequest.result == UnityWebRequest.Result.ConnectionError)
        {
            Debug.LogWarning("conection error");
        }
        else if (getInventarioRequest.result == UnityWebRequest.Result.DataProcessingError)
        {
            Debug.LogWarning("data processing error");
        }
        else if (getInventarioRequest.result == UnityWebRequest.Result.ProtocolError)
        {
            Debug.LogWarning("protocol error");
        }
        if (getInventarioRequest.error == null)
        {
            string response = getInventarioRequest.downloadHandler.text;
            if (response == "1")
            {
                Debug.Log("Database connection error");
            }
            else if (response == "2")
            {
                Debug.Log("Table query ran into an error");
            }
            else if (response == "3")
            {
                Debug.Log("Result came empty");
            }
            else
            {
                JSONNode inventario = JSON.Parse(getInventarioRequest.downloadHandler.text);
                foreach (JSONNode item in inventario)
                {
                    ItemColumns newRow = new ItemColumns();

                    newRow.Modelo = item[0];
                    newRow.QuantidadeDePortas = item[1];
                    newRow.QuaisConexoes = item[2];
                    newRow.EstoqueAtual = item[3];
                    newRow.Categoria = ConstStrings.PlacaDeVideo;

                    tempSheet.itens.Add(newRow);
                }
            }
        }
        getInventarioRequest.Dispose();

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
    private IEnumerator ImportPlacaDeSomToDatabase()
    {
        WWWForm getInventario = new WWWForm();
        getInventario.AddField("apppassword", "ImportDatabase");

        UnityWebRequest getInventarioRequest = UnityWebRequest.Post(ConstStrings.PhpImportTablesFolder + "importplacadesom.php", getInventario);
        yield return getInventarioRequest.SendWebRequest();

        Sheet tempSheet = new Sheet();
        tempSheet.itens = new List<ItemColumns>();

        if (getInventarioRequest.result == UnityWebRequest.Result.ConnectionError)
        {
            Debug.LogWarning("conection error");
        }
        else if (getInventarioRequest.result == UnityWebRequest.Result.DataProcessingError)
        {
            Debug.LogWarning("data processing error");
        }
        else if (getInventarioRequest.result == UnityWebRequest.Result.ProtocolError)
        {
            Debug.LogWarning("protocol error");
        }
        if (getInventarioRequest.error == null)
        {
            string response = getInventarioRequest.downloadHandler.text;
            if (response == "1")
            {
                Debug.Log("Database connection error");
            }
            else if (response == "2")
            {
                Debug.Log("Table query ran into an error");
            }
            else if (response == "3")
            {
                Debug.Log("Result came empty");
            }
            else
            {
                JSONNode inventario = JSON.Parse(getInventarioRequest.downloadHandler.text);
                foreach (JSONNode item in inventario)
                {
                    ItemColumns newRow = new ItemColumns();

                    newRow.Modelo = item[0];
                    newRow.QuantosCanais = item[1];
                    newRow.EstoqueAtual = item[2];
                    newRow.Categoria = ConstStrings.PlacaDeSom;

                    tempSheet.itens.Add(newRow);
                }
            }
        }

        getInventarioRequest.Dispose();

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
    private IEnumerator ImportPlacaDeCapturaDeVideoToDatabase()
    {
        WWWForm getInventario = new WWWForm();
        getInventario.AddField("apppassword", "ImportDatabase");

        UnityWebRequest getInventarioRequest = UnityWebRequest.Post(ConstStrings.PhpImportTablesFolder + "importplacadecapturadevideo.php", getInventario);
        yield return getInventarioRequest.SendWebRequest();

        Sheet tempSheet = new Sheet();
        tempSheet.itens = new List<ItemColumns>();

        if (getInventarioRequest.result == UnityWebRequest.Result.ConnectionError)
        {
            Debug.LogWarning("conection error");
        }
        else if (getInventarioRequest.result == UnityWebRequest.Result.DataProcessingError)
        {
            Debug.LogWarning("data processing error");
        }
        else if (getInventarioRequest.result == UnityWebRequest.Result.ProtocolError)
        {
            Debug.LogWarning("protocol error");
        }
        if (getInventarioRequest.error == null)
        {
            string response = getInventarioRequest.downloadHandler.text;
            if (response == "1")
            {
                Debug.Log("Database connection error");
            }
            else if (response == "2")
            {
                Debug.Log("Table query ran into an error");
            }
            else if (response == "3")
            {
                Debug.Log("Result came empty");
            }
            else
            {
                JSONNode inventario = JSON.Parse(getInventarioRequest.downloadHandler.text);
                foreach (JSONNode item in inventario)
                {
                    ItemColumns newRow = new ItemColumns();

                    newRow.Modelo = item[0];
                    newRow.QuantidadeDePortas = item[1];
                    newRow.EstoqueAtual = item[2];
                    newRow.Categoria = ConstStrings.PlacaDeCapturaDeVideo;

                    tempSheet.itens.Add(newRow);
                }
            }
        }
        getInventarioRequest.Dispose();

        if (!InternalDatabase.Instance.splitDatabase.ContainsKey(ConstStrings.PlacaDeCapturaDeVideo))
        {
            InternalDatabase.Instance.splitDatabase.Add(ConstStrings.PlacaDeCapturaDeVideo, tempSheet);
        }
        else
        {
            InternalDatabase.Instance.splitDatabase[ConstStrings.PlacaDeCapturaDeVideo] = tempSheet;
        }
    }

    /// <summary>
    /// Import Servidor.csv into the internal database
    /// </summary>
    private IEnumerator ImportServidorToDatabase()
    {
        WWWForm getInventario = new WWWForm();
        getInventario.AddField("apppassword", "ImportDatabase");

        UnityWebRequest getInventarioRequest = UnityWebRequest.Post(ConstStrings.PhpImportTablesFolder + "importservidor.php", getInventario);
        yield return getInventarioRequest.SendWebRequest();

        Sheet tempSheet = new Sheet();
        tempSheet.itens = new List<ItemColumns>();

        if (getInventarioRequest.result == UnityWebRequest.Result.ConnectionError)
        {
            Debug.LogWarning("conection error");
        }
        else if (getInventarioRequest.result == UnityWebRequest.Result.DataProcessingError)
        {
            Debug.LogWarning("data processing error");
        }
        else if (getInventarioRequest.result == UnityWebRequest.Result.ProtocolError)
        {
            Debug.LogWarning("protocol error");
        }
        if (getInventarioRequest.error == null)
        {
            string response = getInventarioRequest.downloadHandler.text;
            if (response == "1")
            {
                Debug.Log("Database connection error");
            }
            else if (response == "2")
            {
                Debug.Log("Table query ran into an error");
            }
            else if (response == "3")
            {
                Debug.Log("Result came empty");
            }
            else
            {
                JSONNode inventario = JSON.Parse(getInventarioRequest.downloadHandler.text);
                foreach (JSONNode item in inventario)
                {
                    ItemColumns newRow = new ItemColumns();

                    newRow.Modelo = item[0];
                    newRow.Fabricante = item[1];
                    newRow.EstoqueAtual = item[2];
                    newRow.Categoria = ConstStrings.Servidor;

                    tempSheet.itens.Add(newRow);
                }
            }
        }

        getInventarioRequest.Dispose();

        if (!InternalDatabase.Instance.splitDatabase.ContainsKey(ConstStrings.Servidor))
        {
            InternalDatabase.Instance.splitDatabase.Add(ConstStrings.Servidor, tempSheet);
        }
        else
        {
            InternalDatabase.Instance.splitDatabase[ConstStrings.Servidor] = tempSheet;
        }
    }

    /// <summary>
    /// Import Notebook.csv into the internal database
    /// </summary>
    private IEnumerator ImportNotebookToDatabase()
    {
        WWWForm getInventario = new WWWForm();
        getInventario.AddField("apppassword", "ImportDatabase");

        UnityWebRequest getInventarioRequest = UnityWebRequest.Post(ConstStrings.PhpImportTablesFolder + "importnotebook.php", getInventario);
        yield return getInventarioRequest.SendWebRequest();

        Sheet tempSheet = new Sheet();
        tempSheet.itens = new List<ItemColumns>();

        if (getInventarioRequest.result == UnityWebRequest.Result.ConnectionError)
        {
            Debug.LogWarning("conection error");
        }
        else if (getInventarioRequest.result == UnityWebRequest.Result.DataProcessingError)
        {
            Debug.LogWarning("data processing error");
        }
        else if (getInventarioRequest.result == UnityWebRequest.Result.ProtocolError)
        {
            Debug.LogWarning("protocol error");
        }
        if (getInventarioRequest.error == null)
        {
            string response = getInventarioRequest.downloadHandler.text;
            if (response == "1")
            {
                Debug.Log("Database connection error");
            }
            else if (response == "2")
            {
                Debug.Log("Table query ran into an error");
            }
            else if (response == "3")
            {
                Debug.Log("Result came empty");
            }
            else
            {
                JSONNode inventario = JSON.Parse(getInventarioRequest.downloadHandler.text);
                foreach (JSONNode item in inventario)
                {
                    ItemColumns newRow = new ItemColumns();

                    newRow.Modelo = item[0];
                    newRow.Fabricante = item[1];
                    newRow.EstoqueAtual = item[2];
                    newRow.Categoria = ConstStrings.Notebook;

                    tempSheet.itens.Add(newRow);
                }
            }
        }

        getInventarioRequest.Dispose();

        if (!InternalDatabase.Instance.splitDatabase.ContainsKey(ConstStrings.Notebook))
        {
            InternalDatabase.Instance.splitDatabase.Add(ConstStrings.Notebook, tempSheet);
        }
        else
        {
            InternalDatabase.Instance.splitDatabase[ConstStrings.Notebook] = tempSheet;
        }
    }

    /// <summary>
    /// Import Monitor.csv into the internal database
    /// </summary>
    private IEnumerator ImportMonitorToDatabase()
    {
        WWWForm getInventario = new WWWForm();
        getInventario.AddField("apppassword", "ImportDatabase");

        UnityWebRequest getInventarioRequest = UnityWebRequest.Post(ConstStrings.PhpImportTablesFolder + "importmonitor.php", getInventario);
        yield return getInventarioRequest.SendWebRequest();

        Sheet tempSheet = new Sheet();
        tempSheet.itens = new List<ItemColumns>();

        if (getInventarioRequest.result == UnityWebRequest.Result.ConnectionError)
        {
            Debug.LogWarning("conection error");
        }
        else if (getInventarioRequest.result == UnityWebRequest.Result.DataProcessingError)
        {
            Debug.LogWarning("data processing error");
        }
        else if (getInventarioRequest.result == UnityWebRequest.Result.ProtocolError)
        {
            Debug.LogWarning("protocol error");
        }
        if (getInventarioRequest.error == null)
        {
            string response = getInventarioRequest.downloadHandler.text;
            if (response == "1")
            {
                Debug.Log("Database connection error");
            }
            else if (response == "2")
            {
                Debug.Log("Table query ran into an error");
            }
            else if (response == "3")
            {
                Debug.Log("Result came empty");
            }
            else
            {
                JSONNode inventario = JSON.Parse(getInventarioRequest.downloadHandler.text);
                foreach (JSONNode item in inventario)
                {
                    ItemColumns newRow = new ItemColumns();

                    newRow.Modelo = item[0];
                    newRow.Fabricante = item[1];
                    newRow.Polegadas = item[2];
                    newRow.QuaisConexoes = item[3];
                    newRow.Categoria = ConstStrings.Monitor;

                    tempSheet.itens.Add(newRow);
                }
            }
        }

        getInventarioRequest.Dispose();

        if (!InternalDatabase.Instance.splitDatabase.ContainsKey(ConstStrings.Monitor))
        {
            InternalDatabase.Instance.splitDatabase.Add(ConstStrings.Monitor, tempSheet);
        }
        else
        {
            InternalDatabase.Instance.splitDatabase[ConstStrings.Monitor] = tempSheet;
        }
    }
    #endregion
}