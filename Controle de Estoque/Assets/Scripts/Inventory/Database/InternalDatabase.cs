using Mono.Cecil;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Saving;
using Newtonsoft.Json.Linq;

public class InternalDatabase : Singleton<InternalDatabase>, IJsonSaveable
{
    [SerializeField] GameObject importingWidget;
    [SerializeField] GameObject exportManagerPrefab;
    
    public string currentVersion = "1.0"; // current program version - it is here for lazy reasons

    public Dictionary<string, Sheet> splitDatabase = new Dictionary<string, Sheet>();
    public Sheet fullDatabase = new Sheet();

    public Sheet testingSheet = new Sheet();
    public static List<MovementRecords> movementRecords;
    public static List<string> locations = new List<string>();
    public static List<string> categories = new List<string>();

    #region Sheets with all information divided by "Categoria"
    public static Sheet adaptadorAC = new Sheet();
    public static Sheet carregador = new Sheet();
    public static Sheet desktop = new Sheet();
    public static Sheet foneRamal = new Sheet();
    public static Sheet fonte = new Sheet();
    public static Sheet gbic = new Sheet();
    public static Sheet hd = new Sheet();
    public static Sheet idrac = new Sheet();
    public static Sheet memoria = new Sheet();
    public static Sheet monitor = new Sheet();
    public static Sheet mouse = new Sheet();
    public static Sheet nobreak = new Sheet();
    public static Sheet notebook = new Sheet();
    public static Sheet placaControladora = new Sheet();
    public static Sheet placaDeCapturaDeVideo = new Sheet();
    public static Sheet placaDeRede = new Sheet();
    public static Sheet placaDeSom = new Sheet();
    public static Sheet placaDeVideo = new Sheet();
    public static Sheet processador = new Sheet();
    public static Sheet roteador = new Sheet();
    public static Sheet ramal = new Sheet();
    public static Sheet servidor = new Sheet();
    public static Sheet storageNAS = new Sheet();
    public static Sheet Switch = new Sheet();
    public static Sheet teclado = new Sheet();
    public static Sheet outros = new Sheet();
    #endregion
    public static List<Sheet> allFullDetailsSheets = new List<Sheet>();

    private bool fullDatabaseFilled = false;
    private OfflineProgram offlineProgram = null;

    public CurrentEstoque currentEstoque = CurrentEstoque.SnPro;
    public List<string> testing = new List<string>();
    public Sheet sheetToLoad = new Sheet();
    
    private void Start()
    {
        DontDestroyOnLoad(this.gameObject);
        offlineProgram = GetComponent<OfflineProgram>();
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.F5))
        {
            ReImport();
        }
    }

    private void FillCategoryDatabases()
    {
        /// Try to get all sheets that are available on splitdatabase
        #region Sheets    
        Sheet adaptadorACTemp = new Sheet();
        splitDatabase.TryGetValue(ConstStrings.AdaptadorAC, out adaptadorACTemp);
        Sheet carregadorTemp = new Sheet();
        splitDatabase.TryGetValue(ConstStrings.Carregador, out carregadorTemp);
        Sheet desktopTemp = new Sheet();
        splitDatabase.TryGetValue(ConstStrings.Desktop, out desktopTemp);
        Sheet foneRamalTemp = new Sheet();
        splitDatabase.TryGetValue(ConstStrings.FoneRamal, out foneRamalTemp);
        Sheet fonteTemp = new Sheet();
        splitDatabase.TryGetValue(ConstStrings.Fonte, out fonteTemp);
        Sheet gbicTemp = new Sheet();
        splitDatabase.TryGetValue(ConstStrings.Gbic, out gbicTemp);
        Sheet hdTemp = new Sheet();
        splitDatabase.TryGetValue(ConstStrings.HD, out hdTemp);
        Sheet idracTemp = new Sheet();
        splitDatabase.TryGetValue(ConstStrings.Idrac, out idracTemp);
        Sheet memoriaTemp = new Sheet();
        splitDatabase.TryGetValue(ConstStrings.Memoria, out memoriaTemp);
        Sheet monitorTemp = new Sheet();
        splitDatabase.TryGetValue(ConstStrings.Monitor, out monitorTemp);
        Sheet mouseTemp = new Sheet();
        splitDatabase.TryGetValue(ConstStrings.Monitor, out mouseTemp);
        Sheet nobreakTemp = new Sheet();
        splitDatabase.TryGetValue(ConstStrings.Monitor, out nobreakTemp);
        Sheet notebookTemp = new Sheet();
        splitDatabase.TryGetValue(ConstStrings.Notebook, out notebookTemp);
        Sheet placaControladoraTemp = new Sheet();
        splitDatabase.TryGetValue(ConstStrings.PlacaControladora, out placaControladoraTemp);
        Sheet placaDeCapturaDeVideoTemp = new Sheet();
        splitDatabase.TryGetValue(ConstStrings.PlacaDeCapturaDeVideo, out placaDeCapturaDeVideoTemp);
        Sheet placaDeRedeTemp = new Sheet();
        splitDatabase.TryGetValue(ConstStrings.PlacaDeRede, out placaDeRedeTemp);
        Sheet placaDeSomTemp = new Sheet();
        splitDatabase.TryGetValue(ConstStrings.PlacaDeSom, out placaDeSomTemp);
        Sheet placaDeVideoTemp = new Sheet();
        splitDatabase.TryGetValue(ConstStrings.PlacaDeVideo, out placaDeVideoTemp);
        Sheet processadorTemp = new Sheet();
        splitDatabase.TryGetValue(ConstStrings.Processador, out processadorTemp);
        Sheet roteadorTemp = new Sheet();
        splitDatabase.TryGetValue(ConstStrings.Roteador, out roteadorTemp);
        Sheet ramalTemp = new Sheet();
        splitDatabase.TryGetValue(ConstStrings.Roteador, out ramalTemp);
        Sheet servidorTemp = new Sheet();
        splitDatabase.TryGetValue(ConstStrings.Servidor, out servidorTemp);
        Sheet storageNASTemp = new Sheet();
        splitDatabase.TryGetValue(ConstStrings.StorageNAS, out storageNASTemp);
        Sheet switchTemp = new Sheet();
        splitDatabase.TryGetValue(ConstStrings.Switch, out switchTemp);
        Sheet tecladoTemp = new Sheet();
        splitDatabase.TryGetValue(ConstStrings.StorageNAS, out tecladoTemp);
        Sheet outrosTemp = new Sheet();
        splitDatabase.TryGetValue(ConstStrings.Outros, out outrosTemp);
        #endregion

        foreach (ItemColumns item in fullDatabase.itens)
        {
            if (item.Categoria.Trim() == ConstStrings.AdaptadorAC.Trim())
            {
                FillCategoryDatabasesFunctions.AdaptadorAC(item, adaptadorACTemp);
            }
            else if (item.Categoria.Trim() == ConstStrings.Carregador.Trim())
            {
                FillCategoryDatabasesFunctions.Carregador(item, carregadorTemp);
            }
            else if (item.Categoria.Trim() == ConstStrings.Desktop.Trim())
            {
                FillCategoryDatabasesFunctions.Desktop(item, desktopTemp);
            }
            else if (item.Categoria.Trim() == ConstStrings.FoneRamal.Trim())
            {
                foneRamal.itens.Add(item);
            }
            else if (item.Categoria.Trim() == ConstStrings.Fonte.Trim())
            {
                FillCategoryDatabasesFunctions.Fonte(item, fonteTemp);
            }
            else if (item.Categoria.Trim() == ConstStrings.Gbic.Trim())
            {
                FillCategoryDatabasesFunctions.Gbic(item, gbicTemp);
            }
            else if (item.Categoria.Trim() == ConstStrings.HD.Trim())
            {
                FillCategoryDatabasesFunctions.HD(item, hdTemp);
            }
            else if (item.Categoria.Trim() == ConstStrings.Idrac.Trim())
            {
                FillCategoryDatabasesFunctions.Idrac(item, idracTemp);
            }
            else if (item.Categoria.Trim() == ConstStrings.Memoria.Trim())
            {
                FillCategoryDatabasesFunctions.Memoria(item, memoriaTemp);
            }
            else if (item.Categoria.Trim() == ConstStrings.Monitor.Trim())
            {
                FillCategoryDatabasesFunctions.Monitor(item, monitorTemp);
            }
            else if (item.Categoria.Trim() == ConstStrings.Mouse.Trim())
            {
                mouseTemp.itens.Add(item);
            }
            else if (item.Categoria.Trim() == ConstStrings.Nobreak.Trim())
            {
                nobreak.itens.Add(item);
            }
            else if (item.Categoria.Trim() == ConstStrings.Notebook.Trim())
            {
                FillCategoryDatabasesFunctions.Notebook(item, notebookTemp);
            }
            else if (item.Categoria.Trim() == ConstStrings.PlacaControladora.Trim())
            {
                FillCategoryDatabasesFunctions.PlacaControladora(item, placaControladoraTemp);
            }
            else if (item.Categoria.Trim() == ConstStrings.PlacaDeCapturaDeVideo.Trim())
            {
                FillCategoryDatabasesFunctions.PlacaDeCapturaDeVideo(item, placaDeCapturaDeVideoTemp);
            }
            else if (item.Categoria.Trim() == ConstStrings.PlacaDeRede.Trim())
            {
                FillCategoryDatabasesFunctions.PlacaDeRede(item, placaDeRedeTemp);
            }
            else if (item.Categoria.Trim() == ConstStrings.PlacaDeSom.Trim())
            {
                FillCategoryDatabasesFunctions.PlacaDeSom(item, placaDeSomTemp);
            }
            else if (item.Categoria.Trim() == ConstStrings.PlacaDeVideo.Trim())
            {
                FillCategoryDatabasesFunctions.PlacaDeVideo(item, placaDeVideoTemp);
            }
            else if (item.Categoria.Trim() == ConstStrings.Processador.Trim())
            {
                FillCategoryDatabasesFunctions.Processador(item, processadorTemp);
            }
            else if (item.Categoria.Trim() == ConstStrings.Ramal.Trim())
            {
                ramal.itens.Add(item);
            }
            else if (item.Categoria.Trim() == ConstStrings.Roteador.Trim())
            {
                FillCategoryDatabasesFunctions.Roteador(item, roteadorTemp);
            }
            else if (item.Categoria.Trim() == ConstStrings.Servidor.Trim())
            {
                FillCategoryDatabasesFunctions.Servidor(item, servidorTemp);
            }
            else if (item.Categoria.Trim() == ConstStrings.StorageNAS.Trim())
            {
                FillCategoryDatabasesFunctions.StorageNas(item, storageNASTemp);
            }
            else if (item.Categoria.Trim() == ConstStrings.Switch.Trim())
            {
                FillCategoryDatabasesFunctions.Switch(item, switchTemp);
            }
            else if (item.Categoria.Trim() == ConstStrings.Teclado.Trim())
            {
                teclado.itens.Add(item);
            }
            else
            {
                outros.itens.Add(item);
            }
        }
    }

    public void ReImport()
    {
        Instantiate(importingWidget);
        ImportUISettings.Instance.ReImport();
        InventarioManager.Instance.ImportSheets();
        fullDatabaseFilled = false;
        FillFullDatabase();
    }

    /// <summary>
    /// Get all Sheet classes saved on splitDatabase and join them into a single Sheet class
    /// </summary>
    public void FillFullDatabase()
    {
        if (!fullDatabaseFilled)
        {
            Sheet inventarioTemp = new Sheet();
            splitDatabase.TryGetValue(ConstStrings.Inventario, out inventarioTemp);
            testingSheet = inventarioTemp;
            // Get all itens from "Inventario SnPro into the full database
            if (inventarioTemp != null && inventarioTemp.itens.Count > 0)
            {
                for (int i = 0; i < inventarioTemp.itens.Count; i++)
                {
                    fullDatabase.itens.Add(inventarioTemp.itens[i]);
                }
            }
            // Get the values of the detail sheet based on the "modelo" of the item on Inventario SnPro
            FillCategoryDatabases();
        }
        switch (currentEstoque)
        {
            case CurrentEstoque.SnPro:
                allFullDetailsSheets = HelperMethods.CreateSheetListFromArray(new Sheet[] { adaptadorAC,
            carregador, desktop, fonte, gbic, hd, idrac, memoria, monitor, nobreak,
            notebook, placaControladora, placaDeCapturaDeVideo, placaDeRede, placaDeSom, placaDeVideo,
            processador,roteador, servidor, storageNAS, Switch, outros});
                break;
            case CurrentEstoque.Fumsoft:
            case CurrentEstoque.ESF:
            case CurrentEstoque.Testing:
            default:
                allFullDetailsSheets = HelperMethods.CreateSheetListFromArray(new Sheet[] { adaptadorAC,
            carregador, desktop, foneRamal, fonte, gbic, hd, idrac, memoria, monitor, mouse, nobreak,
            notebook, placaControladora, placaDeCapturaDeVideo, placaDeRede, placaDeSom, placaDeVideo,
            processador, ramal, roteador, servidor, storageNAS, Switch, teclado, outros });
                break;
        }

        fullDatabaseFilled = true;
    }

    public JToken CaptureAsJToken()
    {
        JArray state = HandleSheetsForSaveAndLoad.GetJObject(fullDatabase);   

        return state;
    }

    public void RestoreFromJToken(JToken state)
    {
        HandleSheetsForSaveAndLoad.LoadJObject(state, out sheetToLoad);
        fullDatabase = sheetToLoad;
        FillCategoryDatabases();
    }
}
