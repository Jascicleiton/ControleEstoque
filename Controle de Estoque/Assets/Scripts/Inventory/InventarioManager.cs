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
        UnityWebRequest getInventarioRequest = new UnityWebRequest();
        switch (InternalDatabase.Instance.currentEstoque)
        {
            case CurrentEstoque.SnPro:
                getInventarioRequest = UnityWebRequest.Post(ConstStrings.PhpImportTablesFolder + "importinventario.php", getInventario);
                break;
            case CurrentEstoque.Funsoft:
                getInventarioRequest = UnityWebRequest.Post(ConstStrings.PhpImportTablesFolderFunsoft + "importinventario.php", getInventario);
                break;
            case CurrentEstoque.ESF:
                getInventarioRequest = UnityWebRequest.Post(ConstStrings.PhpImportTablesFolderESF + "importinventario.php", getInventario);
                break;
            default:
                getInventarioRequest = UnityWebRequest.Post(ConstStrings.PhpImportTablesFolder + "importinventario.php", getInventario);
                break;
        }
       
      //  MouseManager.Instance.SetWaitingCursor();
        yield return getInventarioRequest.SendWebRequest();

        Sheet tempSheet = new Sheet();
        tempSheet.itens = new List<ItemColumns>();

        if (getInventarioRequest.result == UnityWebRequest.Result.ConnectionError)
        {
            Debug.LogWarning("inventario conectionerror");
        }
        else if (getInventarioRequest.result == UnityWebRequest.Result.DataProcessingError)
        {
            Debug.LogWarning("inventario data processing error");
        }
        else if (getInventarioRequest.result == UnityWebRequest.Result.ProtocolError)
        {
            Debug.LogWarning("inventario protocol error");
        }
        if (getInventarioRequest.error == null)
        {
            string response = getInventarioRequest.downloadHandler.text;
            if (response == "Database connection error" || response == "wrong appkey" || response == "Query failed")
            {
                Debug.LogWarning("inventario: Server error");
            }
            else if (response == "Result came empty")
            {
                Debug.LogWarning("inventario: Result came empty");
            }
            else
            {
                JSONNode inventario = JSON.Parse(getInventarioRequest.downloadHandler.text);
               
                if (inventario != null)
                {
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
                        newRow.Aquisicao = item[10];
                        tempSheet.itens.Add(newRow);
                    }
                    EventHandler.CallImportFinished(true);
                }
                else
                {
                    Debug.LogWarning("inventario JSON is null");
                }
            }
        }
        else
        {
            Debug.LogWarning("inventario\n " + getInventarioRequest.error);
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
      //  MouseManager.Instance.SetDefaultCursor();

    }

    /// <summary>
    /// Import HD table from server into the internal database
    /// </summary>
    private IEnumerator ImportHDSheetToDatabase()
    {
        WWWForm getInventario = new WWWForm();
        getInventario.AddField("apppassword", "ImportDatabase");

        UnityWebRequest getInventarioRequest = new UnityWebRequest();
        switch (InternalDatabase.Instance.currentEstoque)
        {
            case CurrentEstoque.SnPro:
                getInventarioRequest = UnityWebRequest.Post(ConstStrings.PhpImportTablesFolder + "importhd.php", getInventario);
                break;
            case CurrentEstoque.Funsoft:
                getInventarioRequest = UnityWebRequest.Post(ConstStrings.PhpImportTablesFolderFunsoft + "importhd.php", getInventario);
                break;
            case CurrentEstoque.ESF:
                getInventarioRequest = UnityWebRequest.Post(ConstStrings.PhpImportTablesFolderESF + "importhd.php", getInventario);
                break;
            default:
                getInventarioRequest = UnityWebRequest.Post(ConstStrings.PhpImportTablesFolder + "importhd.php", getInventario);
                break;
        }

        //MouseManager.Instance.SetWaitingCursor();       
        yield return getInventarioRequest.SendWebRequest();

        Sheet tempSheet = new Sheet();
        tempSheet.itens = new List<ItemColumns>();

        if (getInventarioRequest.result == UnityWebRequest.Result.ConnectionError)
        {
            Debug.LogWarning("hd conection error");
        }
        else if (getInventarioRequest.result == UnityWebRequest.Result.DataProcessingError)
        {
            Debug.LogWarning("hd data processing error");
        }
        else if (getInventarioRequest.result == UnityWebRequest.Result.ProtocolError)
        {
            Debug.LogWarning("hd protocol error");
        }
        if (getInventarioRequest.error == null)
        {
            string response = getInventarioRequest.downloadHandler.text;
            if (response == "Database connection error" || response == "wrong appkey" || response == "Query failed")
            {
                Debug.LogWarning("hd: Server error");
            }
            else if (response == "Result came empty")
            {
                Debug.LogWarning("hd: Result came empty");
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
                EventHandler.CallImportFinished(false);
            }
        }
        else
        {
            Debug.LogWarning("hd \n" +getInventarioRequest.error);
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
        //MouseManager.Instance.SetDefaultCursor();
    }

    /// <summary>
    /// Import Memória.csv into the internal database
    /// </summary>
    private IEnumerator ImportMemoriaToDatabase()
    {
        WWWForm getInventario = new WWWForm();
        getInventario.AddField("apppassword", "ImportDatabase");

        UnityWebRequest getInventarioRequest = new UnityWebRequest();
        switch (InternalDatabase.Instance.currentEstoque)
        {
            case CurrentEstoque.SnPro:
                getInventarioRequest = UnityWebRequest.Post(ConstStrings.PhpImportTablesFolder + "importmemoria.php", getInventario);
                break;
            case CurrentEstoque.Funsoft:
                getInventarioRequest = UnityWebRequest.Post(ConstStrings.PhpImportTablesFolderFunsoft + "importmemoria.php", getInventario);
                break;
            case CurrentEstoque.ESF:
                getInventarioRequest = UnityWebRequest.Post(ConstStrings.PhpImportTablesFolderESF + "importmemoria.php", getInventario);
                break;
            default:
                getInventarioRequest = UnityWebRequest.Post(ConstStrings.PhpImportTablesFolder + "importmemoria.php", getInventario);
                break;
        }
        yield return getInventarioRequest.SendWebRequest();

        Sheet tempSheet = new Sheet();
        tempSheet.itens = new List<ItemColumns>();

        if (getInventarioRequest.result == UnityWebRequest.Result.ConnectionError)
        {
            Debug.LogWarning("memoria conection error");
        }
        else if (getInventarioRequest.result == UnityWebRequest.Result.DataProcessingError)
        {
            Debug.LogWarning("memoria data processing error");
        }
        else if (getInventarioRequest.result == UnityWebRequest.Result.ProtocolError)
        {
            Debug.LogWarning("memoria protocol error");
        }
        if (getInventarioRequest.error == null)
        {
            string response = getInventarioRequest.downloadHandler.text;
            if (response == "Database connection error" || response == "wrong appkey" || response == "Query failed")
            {
                Debug.LogWarning("memoria: Server error");
            }
            else if (response == "Result came empty")
            {
                Debug.LogWarning("memoria: Result came empty");
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
                EventHandler.CallImportFinished(false);
            }
        }
        else
        {
            Debug.LogWarning("memoria \n" + getInventarioRequest.error);
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
        switch (InternalDatabase.Instance.currentEstoque)
        {
            case CurrentEstoque.SnPro:
                getInventarioRequest = UnityWebRequest.Post(ConstStrings.PhpImportTablesFolder + "importplacaderede.php", getInventario);
                break;
            case CurrentEstoque.Funsoft:
                getInventarioRequest = UnityWebRequest.Post(ConstStrings.PhpImportTablesFolderFunsoft + "importplacaderede.php", getInventario);
                break;
            case CurrentEstoque.ESF:
                getInventarioRequest = UnityWebRequest.Post(ConstStrings.PhpImportTablesFolderESF + "importplacaderede.php", getInventario);
                break;
            default:
                getInventarioRequest = UnityWebRequest.Post(ConstStrings.PhpImportTablesFolder + "importplacaderede.php", getInventario);
                break;
        }

        yield return getInventarioRequest.SendWebRequest();

        Sheet tempSheet = new Sheet();
        tempSheet.itens = new List<ItemColumns>();

        if (getInventarioRequest.result == UnityWebRequest.Result.ConnectionError)
        {
            Debug.LogWarning("placa de rede conection error");
        }
        else if (getInventarioRequest.result == UnityWebRequest.Result.DataProcessingError)
        {
            Debug.LogWarning("placa de rede data processing error");
        }
        else if (getInventarioRequest.result == UnityWebRequest.Result.ProtocolError)
        {
            Debug.LogWarning("placa de rede protocol error");
        }
        if (getInventarioRequest.error == null)
        {
            string response = getInventarioRequest.downloadHandler.text;
            if (response == "Database connection error" || response == "wrong appkey" || response == "Query failed")
            {
                Debug.LogWarning("Placa de rede: Server error");
            }
            else if (response == "Result came empty")
            {
                Debug.LogWarning("Placa de rede: Result came empty");
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
                EventHandler.CallImportFinished(false);
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
        MouseManager.Instance.SetDefaultCursor();
    }

    /// <summary>
    /// Import iDrac.csv into the internal database
    /// </summary>
    private IEnumerator ImportiDracToDatabase()
    {
        WWWForm getInventario = new WWWForm();
        getInventario.AddField("apppassword", "ImportDatabase");

        UnityWebRequest getInventarioRequest = new UnityWebRequest();
        switch (InternalDatabase.Instance.currentEstoque)
        {
            case CurrentEstoque.SnPro:
                getInventarioRequest = UnityWebRequest.Post(ConstStrings.PhpImportTablesFolder + "importidrac.php", getInventario);
                break;
            case CurrentEstoque.Funsoft:
                getInventarioRequest = UnityWebRequest.Post(ConstStrings.PhpImportTablesFolderFunsoft + "importidrac.php", getInventario);
                break;
            case CurrentEstoque.ESF:
                getInventarioRequest = UnityWebRequest.Post(ConstStrings.PhpImportTablesFolderESF + "importidrac.php", getInventario);
                break;
            default:
                getInventarioRequest = UnityWebRequest.Post(ConstStrings.PhpImportTablesFolder + "importidrac.php", getInventario);
                break;
        }

        yield return getInventarioRequest.SendWebRequest();

        Sheet tempSheet = new Sheet();
        tempSheet.itens = new List<ItemColumns>();

        if (getInventarioRequest.result == UnityWebRequest.Result.ConnectionError)
        {
            Debug.LogWarning("iDrac conection error");
        }
        else if (getInventarioRequest.result == UnityWebRequest.Result.DataProcessingError)
        {
            Debug.LogWarning("iDrac data processing error");
        }
        else if (getInventarioRequest.result == UnityWebRequest.Result.ProtocolError)
        {
            Debug.LogWarning("iDrac protocol error");
        }
        if (getInventarioRequest.error == null)
        {
            string response = getInventarioRequest.downloadHandler.text;
            if (response == "Database connection error" || response == "wrong appkey" || response == "Query failed")
            {
                Debug.LogWarning("iDrac: Server error");
            }
            else if (response == "Result came empty")
            {
                Debug.LogWarning("iDrac: Result came empty");
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
                EventHandler.CallImportFinished(false);
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
        MouseManager.Instance.SetDefaultCursor();
    }

    /// <summary>
    /// Import Placa controladora.csv into the internal database
    /// </summary>
    private IEnumerator ImportPlacaControladoraToDatabase()
    {
        WWWForm getInventario = new WWWForm();
        getInventario.AddField("apppassword", "ImportDatabase");

        UnityWebRequest getInventarioRequest = new UnityWebRequest();
        switch (InternalDatabase.Instance.currentEstoque)
        {
            case CurrentEstoque.SnPro:
                getInventarioRequest = UnityWebRequest.Post(ConstStrings.PhpImportTablesFolder + "importplacacontroladora.php", getInventario);
                break;
            case CurrentEstoque.Funsoft:
                getInventarioRequest = UnityWebRequest.Post(ConstStrings.PhpImportTablesFolderFunsoft + "importplacacontroladora.php", getInventario);
                break;
            case CurrentEstoque.ESF:
                getInventarioRequest = UnityWebRequest.Post(ConstStrings.PhpImportTablesFolderESF + "importplacacontroladora.php", getInventario);
                break;
            default:
                getInventarioRequest = UnityWebRequest.Post(ConstStrings.PhpImportTablesFolder + "importplacacontroladora.php", getInventario);
                break;
        }

        yield return getInventarioRequest.SendWebRequest();

        Sheet tempSheet = new Sheet();
        tempSheet.itens = new List<ItemColumns>();

        if (getInventarioRequest.result == UnityWebRequest.Result.ConnectionError)
        {
            Debug.LogWarning("Placa controladora conection error");
        }
        else if (getInventarioRequest.result == UnityWebRequest.Result.DataProcessingError)
        {
            Debug.LogWarning("Placa controladora data processing error");
        }
        else if (getInventarioRequest.result == UnityWebRequest.Result.ProtocolError)
        {
            Debug.LogWarning("Placa controladora protocol error");
        }
        if (getInventarioRequest.error == null)
        {
            string response = getInventarioRequest.downloadHandler.text;
            if (response == "Database connection error" || response == "wrong appkey" || response == "Query failed")
            {
                Debug.LogWarning("Placa controladora: Server error");
            }
            else if (response == "Result came empty")
            {
                Debug.LogWarning("Placa controladora: Result came empty");
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
                EventHandler.CallImportFinished(false);
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
        MouseManager.Instance.SetDefaultCursor();
    }

    /// <summary>
    /// Import Processador.csv into the internal database
    /// </summary>
    private IEnumerator ImportProcessadorToDatabase()
    {
        WWWForm getInventario = new WWWForm();
        getInventario.AddField("apppassword", "ImportDatabase");

        UnityWebRequest getInventarioRequest = new UnityWebRequest();
        switch (InternalDatabase.Instance.currentEstoque)
        {
            case CurrentEstoque.SnPro:
                getInventarioRequest = UnityWebRequest.Post(ConstStrings.PhpImportTablesFolder + "importprocessador.php", getInventario);
                break;
            case CurrentEstoque.Funsoft:
                getInventarioRequest = UnityWebRequest.Post(ConstStrings.PhpImportTablesFolderFunsoft + "importprocessador.php", getInventario);
                break;
            case CurrentEstoque.ESF:
                getInventarioRequest = UnityWebRequest.Post(ConstStrings.PhpImportTablesFolderESF + "importprocessador.php", getInventario);
                break;
            default:
                getInventarioRequest = UnityWebRequest.Post(ConstStrings.PhpImportTablesFolder + "importprocessador.php", getInventario);
                break;
        }
        yield return getInventarioRequest.SendWebRequest();

        Sheet tempSheet = new Sheet();
        tempSheet.itens = new List<ItemColumns>();

        if (getInventarioRequest.result == UnityWebRequest.Result.ConnectionError)
        {
            Debug.LogWarning("Processador: conection error");
        }
        else if (getInventarioRequest.result == UnityWebRequest.Result.DataProcessingError)
        {
            Debug.LogWarning("Processador: data processing error");
        }
        else if (getInventarioRequest.result == UnityWebRequest.Result.ProtocolError)
        {
            Debug.LogWarning("Processador: protocol error");
        }
        if (getInventarioRequest.error == null)
        {
            string response = getInventarioRequest.downloadHandler.text;
            if (response == "Database connection error" || response == "wrong appkey" || response == "Query failed")
            {
                Debug.LogWarning("Processador: Server error");
            }
            else if (response == "Result came empty")
            {
                Debug.LogWarning("Processador: Result came empty");
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
                EventHandler.CallImportFinished(false);
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
        MouseManager.Instance.SetDefaultCursor();
    }

    /// <summary>
    /// Import Desktop.csv into the internal database
    /// </summary>
    private IEnumerator ImportDesktopToDatabase()
    {
        WWWForm getInventario = new WWWForm();
        getInventario.AddField("apppassword", "ImportDatabase");

        UnityWebRequest getInventarioRequest = new UnityWebRequest();
        switch (InternalDatabase.Instance.currentEstoque)
        {
            case CurrentEstoque.SnPro:
                getInventarioRequest = UnityWebRequest.Post(ConstStrings.PhpImportTablesFolder + "importdesktop.php", getInventario);
                break;
            case CurrentEstoque.Funsoft:
                getInventarioRequest = UnityWebRequest.Post(ConstStrings.PhpImportTablesFolderFunsoft + "importdesktop.php", getInventario);
                break;
            case CurrentEstoque.ESF:
                getInventarioRequest = UnityWebRequest.Post(ConstStrings.PhpImportTablesFolderESF + "importdesktop.php", getInventario);
                break;
            default:
                getInventarioRequest = UnityWebRequest.Post(ConstStrings.PhpImportTablesFolder + "importdesktop.php", getInventario);
                break;
        }
        yield return getInventarioRequest.SendWebRequest();

        Sheet tempSheet = new Sheet();
        tempSheet.itens = new List<ItemColumns>();

        if (getInventarioRequest.result == UnityWebRequest.Result.ConnectionError)
        {
            Debug.LogWarning("Desktop: conection error");
        }
        else if (getInventarioRequest.result == UnityWebRequest.Result.DataProcessingError)
        {
            Debug.LogWarning("Desktop: data processing error");
        }
        else if (getInventarioRequest.result == UnityWebRequest.Result.ProtocolError)
        {
            Debug.LogWarning("Desktop: protocol error");
        }
        if (getInventarioRequest.error == null)
        {
            string response = getInventarioRequest.downloadHandler.text;
            if (response == "Database connection error" || response == "wrong appkey" || response == "Query failed")
            {
                Debug.LogWarning("Desktop: Server error");
            }
            else if (response == "Result came empty")
            {
                Debug.LogWarning("Desktop: Result came empty");
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
                EventHandler.CallImportFinished(false);
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
        MouseManager.Instance.SetDefaultCursor();
    }

    /// <summary>
    /// Import Fonte.csv into the internal database
    /// </summary>
    private IEnumerator ImportFonteToDatabase()
    {
        WWWForm getInventario = new WWWForm();
        getInventario.AddField("apppassword", "ImportDatabase");

        UnityWebRequest getInventarioRequest = new UnityWebRequest();
        switch (InternalDatabase.Instance.currentEstoque)
        {
            case CurrentEstoque.SnPro:
                getInventarioRequest = UnityWebRequest.Post(ConstStrings.PhpImportTablesFolder + "importfonte.php", getInventario);
                break;
            case CurrentEstoque.Funsoft:
                getInventarioRequest = UnityWebRequest.Post(ConstStrings.PhpImportTablesFolderFunsoft + "importfonte.php", getInventario);
                break;
            case CurrentEstoque.ESF:
                getInventarioRequest = UnityWebRequest.Post(ConstStrings.PhpImportTablesFolderESF + "importfonte.php", getInventario);
                break;
            default:
                getInventarioRequest = UnityWebRequest.Post(ConstStrings.PhpImportTablesFolder + "importfonte.php", getInventario);
                break;
        }
        yield return getInventarioRequest.SendWebRequest();

        Sheet tempSheet = new Sheet();
        tempSheet.itens = new List<ItemColumns>();

        if (getInventarioRequest.result == UnityWebRequest.Result.ConnectionError)
        {
            Debug.LogWarning("Fonte: conection error");
        }
        else if (getInventarioRequest.result == UnityWebRequest.Result.DataProcessingError)
        {
            Debug.LogWarning("Fonte: data processing error");
        }
        else if (getInventarioRequest.result == UnityWebRequest.Result.ProtocolError)
        {
            Debug.LogWarning("Fonte: protocol error");
        }
        if (getInventarioRequest.error == null)
        {
            string response = getInventarioRequest.downloadHandler.text;
            if (response == "Database connection error" || response == "wrong appkey" || response == "Query failed")
            {
                Debug.LogWarning("Fonte: Server error");
            }
            else if (response == "Result came empty")
            {
                Debug.LogWarning("Fonte: Result came empty");
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
                EventHandler.CallImportFinished(false);
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

        UnityWebRequest getInventarioRequest = new UnityWebRequest();
        switch (InternalDatabase.Instance.currentEstoque)
        {
            case CurrentEstoque.SnPro:
                getInventarioRequest = UnityWebRequest.Post(ConstStrings.PhpImportTablesFolder + "importswitch.php", getInventario);
                break;
            case CurrentEstoque.Funsoft:
                getInventarioRequest = UnityWebRequest.Post(ConstStrings.PhpImportTablesFolderFunsoft + "importswitch.php", getInventario);
                break;
            case CurrentEstoque.ESF:
                getInventarioRequest = UnityWebRequest.Post(ConstStrings.PhpImportTablesFolderESF + "importswitch.php", getInventario);
                break;
            default:
                getInventarioRequest = UnityWebRequest.Post(ConstStrings.PhpImportTablesFolder + "importswitch.php", getInventario);
                break;
        }
        yield return getInventarioRequest.SendWebRequest();

        Sheet tempSheet = new Sheet();
        tempSheet.itens = new List<ItemColumns>();

        if (getInventarioRequest.result == UnityWebRequest.Result.ConnectionError)
        {
            Debug.LogWarning("Switch: conection error");
        }
        else if (getInventarioRequest.result == UnityWebRequest.Result.DataProcessingError)
        {
            Debug.LogWarning("Switch: data processing error");
        }
        else if (getInventarioRequest.result == UnityWebRequest.Result.ProtocolError)
        {
            Debug.LogWarning("Switch: protocol error");
        }
        if (getInventarioRequest.error == null)
        {
            string response = getInventarioRequest.downloadHandler.text;
            if (response == "Database connection error" || response == "wrong appkey" || response == "Query failed")
            {
                Debug.LogWarning("Switch: Server error");
            }
            else if (response == "Result came empty")
            {
                Debug.LogWarning("Switch: Result came empty");
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
                EventHandler.CallImportFinished(false);
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

        UnityWebRequest getInventarioRequest = new UnityWebRequest();
        switch (InternalDatabase.Instance.currentEstoque)
        {
            case CurrentEstoque.SnPro:
                getInventarioRequest = UnityWebRequest.Post(ConstStrings.PhpImportTablesFolder + "importroteador.php", getInventario);
                break;
            case CurrentEstoque.Funsoft:
                getInventarioRequest = UnityWebRequest.Post(ConstStrings.PhpImportTablesFolderFunsoft + "importroteador.php", getInventario);
                break;
            case CurrentEstoque.ESF:
                getInventarioRequest = UnityWebRequest.Post(ConstStrings.PhpImportTablesFolderESF + "importroteador.php", getInventario);
                break;
            default:
                getInventarioRequest = UnityWebRequest.Post(ConstStrings.PhpImportTablesFolder + "importroteador.php", getInventario);
                break;
        }
        yield return getInventarioRequest.SendWebRequest();

        Sheet tempSheet = new Sheet();
        tempSheet.itens = new List<ItemColumns>();

        if (getInventarioRequest.result == UnityWebRequest.Result.ConnectionError)
        {
            Debug.LogWarning("Roteador: conection error");
        }
        else if (getInventarioRequest.result == UnityWebRequest.Result.DataProcessingError)
        {
            Debug.LogWarning("Roteador: data processing error");
        }
        else if (getInventarioRequest.result == UnityWebRequest.Result.ProtocolError)
        {
            Debug.LogWarning("Roteador: protocol error");
        }
        if (getInventarioRequest.error == null)
        {
            string response = getInventarioRequest.downloadHandler.text;
            if (response == "Database connection error" || response == "wrong appkey" || response == "Query failed")
            {
                Debug.LogWarning("Roteador: Server error");
            }
            else if (response == "Result came empty")
            {
                Debug.LogWarning("Roteador: Result came empty");
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
                EventHandler.CallImportFinished(false);
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

        UnityWebRequest getInventarioRequest = new UnityWebRequest();
        switch (InternalDatabase.Instance.currentEstoque)
        {
            case CurrentEstoque.SnPro:
                getInventarioRequest = UnityWebRequest.Post(ConstStrings.PhpImportTablesFolder + "importcarregador.php", getInventario);
                break;
            case CurrentEstoque.Funsoft:
                getInventarioRequest = UnityWebRequest.Post(ConstStrings.PhpImportTablesFolderFunsoft + "importcarregador.php", getInventario);
                break;
            case CurrentEstoque.ESF:
                getInventarioRequest = UnityWebRequest.Post(ConstStrings.PhpImportTablesFolderESF + "importcarregador.php", getInventario);
                break;
            default:
                getInventarioRequest = UnityWebRequest.Post(ConstStrings.PhpImportTablesFolder + "importcarregador.php", getInventario);
                break;
        }
        yield return getInventarioRequest.SendWebRequest();

        Sheet tempSheet = new Sheet();
        tempSheet.itens = new List<ItemColumns>();

        if (getInventarioRequest.result == UnityWebRequest.Result.ConnectionError)
        {
            Debug.LogWarning("Carregador: conection error");
        }
        else if (getInventarioRequest.result == UnityWebRequest.Result.DataProcessingError)
        {
            Debug.LogWarning("Carregador: data processing error");
        }
        else if (getInventarioRequest.result == UnityWebRequest.Result.ProtocolError)
        {
            Debug.LogWarning("Carregador: protocol error");
        }
        if (getInventarioRequest.error == null)
        {
            string response = getInventarioRequest.downloadHandler.text;
            if (response == "Database connection error" || response == "wrong appkey" || response == "Query failed")
            {
                Debug.LogWarning("Carregador: Server error");
            }
            else if (response == "Result came empty")
            {
                Debug.LogWarning("Carregador: Result came empty");
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
                EventHandler.CallImportFinished(false);
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

        UnityWebRequest getInventarioRequest = new UnityWebRequest();
        switch (InternalDatabase.Instance.currentEstoque)
        {
            case CurrentEstoque.SnPro:
                getInventarioRequest = UnityWebRequest.Post(ConstStrings.PhpImportTablesFolder + "importadaptadorac.php", getInventario);
                break;
            case CurrentEstoque.Funsoft:
                getInventarioRequest = UnityWebRequest.Post(ConstStrings.PhpImportTablesFolderFunsoft + "importadaptadorac.php", getInventario);
                break;
            case CurrentEstoque.ESF:
                getInventarioRequest = UnityWebRequest.Post(ConstStrings.PhpImportTablesFolderESF + "importadaptadorac.php", getInventario);
                break;
            default:
                getInventarioRequest = UnityWebRequest.Post(ConstStrings.PhpImportTablesFolder + "importadaptadorac.php", getInventario);
                break;
        }
        yield return getInventarioRequest.SendWebRequest();

        Sheet tempSheet = new Sheet();
        tempSheet.itens = new List<ItemColumns>();

        if (getInventarioRequest.result == UnityWebRequest.Result.ConnectionError)
        {
            Debug.LogWarning("Adaptador AC: conection error");
        }
        else if (getInventarioRequest.result == UnityWebRequest.Result.DataProcessingError)
        {
            Debug.LogWarning("Adaptador AC: data processing error");
        }
        else if (getInventarioRequest.result == UnityWebRequest.Result.ProtocolError)
        {
            Debug.LogWarning("Adaptador AC: protocol error");
        }
        if (getInventarioRequest.error == null)
        {
            string response = getInventarioRequest.downloadHandler.text;
            if (response == "Database connection error" || response == "wrong appkey" || response == "Query failed")
            {
                Debug.LogWarning("Adaptador AC: Server error");
            }
            else if (response == "Result came empty")
            {
                Debug.LogWarning("Adaptador AC: Result came empty");
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
                EventHandler.CallImportFinished(false);
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

        UnityWebRequest getInventarioRequest = new UnityWebRequest();
        switch (InternalDatabase.Instance.currentEstoque)
        {
            case CurrentEstoque.SnPro:
                getInventarioRequest = UnityWebRequest.Post(ConstStrings.PhpImportTablesFolder + "importstoragenas.php", getInventario);
                break;
            case CurrentEstoque.Funsoft:
                getInventarioRequest = UnityWebRequest.Post(ConstStrings.PhpImportTablesFolderFunsoft + "importstoragenas.php", getInventario);
                break;
            case CurrentEstoque.ESF:
                getInventarioRequest = UnityWebRequest.Post(ConstStrings.PhpImportTablesFolderESF + "importstoragenas.php", getInventario);
                break;
            default:
                getInventarioRequest = UnityWebRequest.Post(ConstStrings.PhpImportTablesFolder + "importstoragenas.php", getInventario);
                break;
        }
        yield return getInventarioRequest.SendWebRequest();

        Sheet tempSheet = new Sheet();
        tempSheet.itens = new List<ItemColumns>();

        if (getInventarioRequest.result == UnityWebRequest.Result.ConnectionError)
        {
            Debug.LogWarning("Storage NAS: conection error");
        }
        else if (getInventarioRequest.result == UnityWebRequest.Result.DataProcessingError)
        {
            Debug.LogWarning("Storage NAS: data processing error");
        }
        else if (getInventarioRequest.result == UnityWebRequest.Result.ProtocolError)
        {
            Debug.LogWarning("Storage NAS: protocol error");
        }
        if (getInventarioRequest.error == null)
        {
            string response = getInventarioRequest.downloadHandler.text;
            if (response == "Database connection error" || response == "wrong appkey" || response == "Query failed")
            {
                Debug.LogWarning("Storage NAS: Server error");
            }
            else if (response == "Result came empty")
            {
                Debug.LogWarning("Storage NAS: Result came empty");
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
                EventHandler.CallImportFinished(false);
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

        UnityWebRequest getInventarioRequest = new UnityWebRequest();
        switch (InternalDatabase.Instance.currentEstoque)
        {
            case CurrentEstoque.SnPro:
                getInventarioRequest = UnityWebRequest.Post(ConstStrings.PhpImportTablesFolder + "importgbic.php", getInventario);
                break;
            case CurrentEstoque.Funsoft:
                getInventarioRequest = UnityWebRequest.Post(ConstStrings.PhpImportTablesFolderFunsoft + "importgbic.php", getInventario);
                break;
            case CurrentEstoque.ESF:
                getInventarioRequest = UnityWebRequest.Post(ConstStrings.PhpImportTablesFolderESF + "importgbic.php", getInventario);
                break;
            default:
                getInventarioRequest = UnityWebRequest.Post(ConstStrings.PhpImportTablesFolder + "importgbic.php", getInventario);
                break;
        }
        yield return getInventarioRequest.SendWebRequest();

        Sheet tempSheet = new Sheet();
        tempSheet.itens = new List<ItemColumns>();

        if (getInventarioRequest.result == UnityWebRequest.Result.ConnectionError)
        {
            Debug.LogWarning("GBIC: conection error");
        }
        else if (getInventarioRequest.result == UnityWebRequest.Result.DataProcessingError)
        {
            Debug.LogWarning("GBIC: data processing error");
        }
        else if (getInventarioRequest.result == UnityWebRequest.Result.ProtocolError)
        {
            Debug.LogWarning("GBIC: protocol error");
        }
        if (getInventarioRequest.error == null)
        {
            string response = getInventarioRequest.downloadHandler.text;
            if (response == "Database connection error" || response == "wrong appkey" || response == "Query failed")
            {
                Debug.LogWarning("GBIC: Server error");
            }
            else if (response == "Result came empty")
            {
                Debug.LogWarning("GBIC: Result came empty");
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
                EventHandler.CallImportFinished(false);
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

        UnityWebRequest getInventarioRequest = new UnityWebRequest();
        switch (InternalDatabase.Instance.currentEstoque)
        {
            case CurrentEstoque.SnPro:
                getInventarioRequest = UnityWebRequest.Post(ConstStrings.PhpImportTablesFolder + "importplacadevideo.php", getInventario);
                break;
            case CurrentEstoque.Funsoft:
                getInventarioRequest = UnityWebRequest.Post(ConstStrings.PhpImportTablesFolderFunsoft + "importplacadevideo.php", getInventario);
                break;
            case CurrentEstoque.ESF:
                getInventarioRequest = UnityWebRequest.Post(ConstStrings.PhpImportTablesFolderESF + "importplacadevideo.php", getInventario);
                break;
            default:
                getInventarioRequest = UnityWebRequest.Post(ConstStrings.PhpImportTablesFolder + "importplacadevideo.php", getInventario);
                break;
        }
        yield return getInventarioRequest.SendWebRequest();

        Sheet tempSheet = new Sheet();
        tempSheet.itens = new List<ItemColumns>();

        if (getInventarioRequest.result == UnityWebRequest.Result.ConnectionError)
        {
            Debug.LogWarning("Placa de video: conection error");
        }
        else if (getInventarioRequest.result == UnityWebRequest.Result.DataProcessingError)
        {
            Debug.LogWarning("Placa de video: data processing error");
        }
        else if (getInventarioRequest.result == UnityWebRequest.Result.ProtocolError)
        {
            Debug.LogWarning("Placa de video: protocol error");
        }
        if (getInventarioRequest.error == null)
        {
            string response = getInventarioRequest.downloadHandler.text;
            if (response == "Database connection error" || response == "wrong appkey" || response == "Query failed")
            {
                Debug.LogWarning("Placa de video: Server error");
            }
            else if (response == "Result came empty")
            {
                Debug.LogWarning("Placa de video: Result came empty");
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
                EventHandler.CallImportFinished(false);
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

        UnityWebRequest getInventarioRequest = new UnityWebRequest();
        switch (InternalDatabase.Instance.currentEstoque)
        {
            case CurrentEstoque.SnPro:
                getInventarioRequest = UnityWebRequest.Post(ConstStrings.PhpImportTablesFolder + "importplacadesom.php", getInventario);
                break;
            case CurrentEstoque.Funsoft:
                getInventarioRequest = UnityWebRequest.Post(ConstStrings.PhpImportTablesFolderFunsoft + "importplacadesom.php", getInventario);
                break;
            case CurrentEstoque.ESF:
                getInventarioRequest = UnityWebRequest.Post(ConstStrings.PhpImportTablesFolderESF + "importplacadesom.php", getInventario);
                break;
            default:
                getInventarioRequest = UnityWebRequest.Post(ConstStrings.PhpImportTablesFolder + "importplacadesom.php", getInventario);
                break;
        }
        yield return getInventarioRequest.SendWebRequest();

        Sheet tempSheet = new Sheet();
        tempSheet.itens = new List<ItemColumns>();

        if (getInventarioRequest.result == UnityWebRequest.Result.ConnectionError)
        {
            Debug.LogWarning("Placa de som: conection error");
        }
        else if (getInventarioRequest.result == UnityWebRequest.Result.DataProcessingError)
        {
            Debug.LogWarning("Placa de som: data processing error");
        }
        else if (getInventarioRequest.result == UnityWebRequest.Result.ProtocolError)
        {
            Debug.LogWarning("Placa de som: protocol error");
        }
        if (getInventarioRequest.error == null)
        {
            string response = getInventarioRequest.downloadHandler.text;
            if (response == "Database connection error" || response == "wrong appkey" || response == "Query failed")
            {
                Debug.LogWarning("Placa de som: Server error");
            }
            else if (response == "Result came empty")
            {
                Debug.LogWarning("Placa de som: Result came empty");
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
                EventHandler.CallImportFinished(false);
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

        UnityWebRequest getInventarioRequest = new UnityWebRequest();
        switch (InternalDatabase.Instance.currentEstoque)
        {
            case CurrentEstoque.SnPro:
                getInventarioRequest = UnityWebRequest.Post(ConstStrings.PhpImportTablesFolder + "importplacadecapturadevideo.php", getInventario);
                break;
            case CurrentEstoque.Funsoft:
                getInventarioRequest = UnityWebRequest.Post(ConstStrings.PhpImportTablesFolderFunsoft + "importplacadecapturadevideo.php", getInventario);
                break;
            case CurrentEstoque.ESF:
                getInventarioRequest = UnityWebRequest.Post(ConstStrings.PhpImportTablesFolderESF + "importplacadecapturadevideo.php", getInventario);
                break;
            default:
                getInventarioRequest = UnityWebRequest.Post(ConstStrings.PhpImportTablesFolder + "importplacadecapturadevideo.php", getInventario);
                break;
        }
        yield return getInventarioRequest.SendWebRequest();

        Sheet tempSheet = new Sheet();
        tempSheet.itens = new List<ItemColumns>();

        if (getInventarioRequest.result == UnityWebRequest.Result.ConnectionError)
        {
            Debug.LogWarning("Placa de captura de video: conection error");
        }
        else if (getInventarioRequest.result == UnityWebRequest.Result.DataProcessingError)
        {
            Debug.LogWarning("Placa de captura de video: data processing error");
        }
        else if (getInventarioRequest.result == UnityWebRequest.Result.ProtocolError)
        {
            Debug.LogWarning("Placa de captura de video: protocol error");
        }
        if (getInventarioRequest.error == null)
        {
            string response = getInventarioRequest.downloadHandler.text;
            if (response == "Database connection error" || response == "wrong appkey" || response == "Query failed")
            {
                Debug.LogWarning("Placa de captura de video: Server error");
            }
            else if (response == "Result came empty")
            {
                Debug.LogWarning("Placa de captura de video: Result came empty");
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
                EventHandler.CallImportFinished(false);
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

        UnityWebRequest getInventarioRequest = new UnityWebRequest();
        switch (InternalDatabase.Instance.currentEstoque)
        {
            case CurrentEstoque.SnPro:
                getInventarioRequest = UnityWebRequest.Post(ConstStrings.PhpImportTablesFolder + "importservidor.php", getInventario);
                break;
            case CurrentEstoque.Funsoft:
                getInventarioRequest = UnityWebRequest.Post(ConstStrings.PhpImportTablesFolderFunsoft + "importservidor.php", getInventario);
                break;
            case CurrentEstoque.ESF:
                getInventarioRequest = UnityWebRequest.Post(ConstStrings.PhpImportTablesFolderESF + "importservidor.php", getInventario);
                break;
            default:
                getInventarioRequest = UnityWebRequest.Post(ConstStrings.PhpImportTablesFolder + "importservidor.php", getInventario);
                break;
        }
        yield return getInventarioRequest.SendWebRequest();

        Sheet tempSheet = new Sheet();
        tempSheet.itens = new List<ItemColumns>();

        if (getInventarioRequest.result == UnityWebRequest.Result.ConnectionError)
        {
            Debug.LogWarning("Servidor: conection error");
        }
        else if (getInventarioRequest.result == UnityWebRequest.Result.DataProcessingError)
        {
            Debug.LogWarning("Servidor: data processing error");
        }
        else if (getInventarioRequest.result == UnityWebRequest.Result.ProtocolError)
        {
            Debug.LogWarning("Servidor: protocol error");
        }
        if (getInventarioRequest.error == null)
        {
            string response = getInventarioRequest.downloadHandler.text;
            if (response == "Database connection error" || response == "wrong appkey" || response == "Query failed")
            {
                Debug.LogWarning("Servidor: Server error");
            }
            else if (response == "Result came empty")
            {
                Debug.LogWarning("Servidor: Result came empty");
            }
            else
            {
                JSONNode inventario = JSON.Parse(getInventarioRequest.downloadHandler.text);
                foreach (JSONNode item in inventario)
                {
                    ItemColumns newRow = new ItemColumns();
                    newRow.Patrimonio = item[0];
                    newRow.Modelo = item[1];
                    newRow.ModeloPlacaMae = item[2];
                    newRow.Fonte = item[3];
                    newRow.Memoria = item[4];
                    newRow.HD = item[5];
                    newRow.PlacaDeVideo = item[6];
                    newRow.PlacaDeRede = item[7];
                    newRow.Processador = item[8];
                    newRow.MemoriasSuportadas = item[9];
                    newRow.QuantasMemorias = item[10];
                    newRow.OrdemDasMemorias = item[11];
                    newRow.CapacidadeRAMTotal = item[12];
                    newRow.Soquete = item[13];
                    newRow.PlacaControladora = item[14];
                    newRow.AteQuantosHDs = item[15];
                    newRow.TipoDeHD = item[16];
                    newRow.TipoDeRAID = item[17];
                    newRow.Categoria = ConstStrings.Servidor;

                    tempSheet.itens.Add(newRow);
                }
                EventHandler.CallImportFinished(false);
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

        UnityWebRequest getInventarioRequest = new UnityWebRequest();
        switch (InternalDatabase.Instance.currentEstoque)
        {
            case CurrentEstoque.SnPro:
                getInventarioRequest = UnityWebRequest.Post(ConstStrings.PhpImportTablesFolder + "importnotebook.php", getInventario);
                break;
            case CurrentEstoque.Funsoft:
                getInventarioRequest = UnityWebRequest.Post(ConstStrings.PhpImportTablesFolderFunsoft + "importnotebook.php", getInventario);
                break;
            case CurrentEstoque.ESF:
                getInventarioRequest = UnityWebRequest.Post(ConstStrings.PhpImportTablesFolderESF + "importnotebook.php", getInventario);
                break;
            default:
                getInventarioRequest = UnityWebRequest.Post(ConstStrings.PhpImportTablesFolder + "importnotebook.php", getInventario);
                break;
        }
        yield return getInventarioRequest.SendWebRequest();

        Sheet tempSheet = new Sheet();
        tempSheet.itens = new List<ItemColumns>();

        if (getInventarioRequest.result == UnityWebRequest.Result.ConnectionError)
        {
            Debug.LogWarning("Notebook: conection error");
        }
        else if (getInventarioRequest.result == UnityWebRequest.Result.DataProcessingError)
        {
            Debug.LogWarning("Notebook: data processing error");
        }
        else if (getInventarioRequest.result == UnityWebRequest.Result.ProtocolError)
        {
            Debug.LogWarning("Notebook: protocol error");
        }
        if (getInventarioRequest.error == null)
        {
            string response = getInventarioRequest.downloadHandler.text;
            if (response == "Database connection error" || response == "wrong appkey" || response == "Query failed")
            {
                Debug.LogWarning("Notebook: Server error");
            }
            else if (response == "Result came empty")
            {
                Debug.LogWarning("Notebook: Result came empty");
            }
            else
            {
                JSONNode inventario = JSON.Parse(getInventarioRequest.downloadHandler.text);
                foreach (JSONNode item in inventario)
                {
                    ItemColumns newRow = new ItemColumns();
                    newRow.Patrimonio = item[0];
                    newRow.Modelo = item[1];
                    newRow.Fabricante = item[2];
                    newRow.HD = item[3];
                    newRow.Memoria = item[4];
                    newRow.EntradaRJ49 = item[5];
                    newRow.BateriaInclusa = item[6];
                    newRow.AdaptadorAC = item[7];                 
                    newRow.Windows = item[8];
                    newRow.Categoria = ConstStrings.Notebook;

                    tempSheet.itens.Add(newRow);
                }
                EventHandler.CallImportFinished(false);
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

        UnityWebRequest getInventarioRequest = new UnityWebRequest();
        switch (InternalDatabase.Instance.currentEstoque)
        {
            case CurrentEstoque.SnPro:
                getInventarioRequest = UnityWebRequest.Post(ConstStrings.PhpImportTablesFolder + "importmonitor.php", getInventario);
                break;
            case CurrentEstoque.Funsoft:
                getInventarioRequest = UnityWebRequest.Post(ConstStrings.PhpImportTablesFolderFunsoft + "importmonitor.php", getInventario);
                break;
            case CurrentEstoque.ESF:
                getInventarioRequest = UnityWebRequest.Post(ConstStrings.PhpImportTablesFolderESF + "importmonitor.php", getInventario);
                break;
            default:
                getInventarioRequest = UnityWebRequest.Post(ConstStrings.PhpImportTablesFolder + "importmonitor.php", getInventario);
                break;
        }
        yield return getInventarioRequest.SendWebRequest();

        Sheet tempSheet = new Sheet();
        tempSheet.itens = new List<ItemColumns>();

        if (getInventarioRequest.result == UnityWebRequest.Result.ConnectionError)
        {
            Debug.LogWarning("Monitor: conection error");
        }
        else if (getInventarioRequest.result == UnityWebRequest.Result.DataProcessingError)
        {
            Debug.LogWarning("Monitor: data processing error");
        }
        else if (getInventarioRequest.result == UnityWebRequest.Result.ProtocolError)
        {
            Debug.LogWarning("Monitor: protocol error");
        }
        if (getInventarioRequest.error == null)
        {
            string response = getInventarioRequest.downloadHandler.text;
            if (response == "Database connection error" || response == "wrong appkey" || response == "Query failed")
            {
                Debug.LogWarning("Monitor: Server error");
            }
            else if (response == "Result came empty")
            {
                Debug.LogWarning("Monitor: Result came empty");
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
                EventHandler.CallImportFinished(false);
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