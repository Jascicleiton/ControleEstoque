using System.Collections.Generic;
using UnityEngine;
using Newtonsoft.Json.Linq;
using Assets.Scripts.Inventory.NoPatrimonioItem;
using Assets.Scripts.Inventory.Movement;
using Assets.Scripts.Misc;
using Assets.Scripts.Saving;

namespace Assets.Scripts.Inventory.Database
{
    public class InternalDatabase : Singleton<InternalDatabase>, IJsonSaveable
    {
        // [SerializeField] GameObject importingWidget;
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
        public static Sheet placaSAS = new Sheet();
        public static Sheet outros = new Sheet();
        #endregion
        public static List<Sheet> allFullDetailsSheets = new List<Sheet>();

        //private bool fullDatabaseFilled = false;
        public bool isOfflineProgram = false;

        public CurrentEstoque currentEstoque = CurrentEstoque.SnPro;
        public List<string> testing = new List<string>();
        public Sheet sheetToLoad = new Sheet();

        [SerializeField] private int lastProcessador;

        private void Start()
        {
            if (isOfflineProgram)
            {
                SavingWrapper.Instance.Load();
            }
        }

        private void OnEnable()
        {
            EventHandler.FillInternalDatabase += FillFullDatabase;
        }

        private void OnDisable()
        {
            EventHandler.FillInternalDatabase -= FillFullDatabase;
        }

        private void Update()
        {
            if (Input.GetKeyDown(KeyCode.F5))
            {
                UpdateLastProcessadorPatrimonio();
            }
        }

        /// <summary>
        /// Fills all internal category databases
        /// </summary>
        private void FillCategoryDatabases()
        {
            /// Try to get all sheets that are available on splitdatabase
            #region Sheets    
            Sheet adaptadorACTemp = new Sheet();
            splitDatabase.TryGetValue(ConstStrings.C_AdaptadorAC, out adaptadorACTemp);
            Sheet carregadorTemp = new Sheet();
            splitDatabase.TryGetValue(ConstStrings.C_Carregador, out carregadorTemp);
            Sheet desktopTemp = new Sheet();
            splitDatabase.TryGetValue(ConstStrings.C_Desktop, out desktopTemp);
            Sheet foneRamalTemp = new Sheet();
            splitDatabase.TryGetValue(ConstStrings.C_FoneRamal, out foneRamalTemp);
            Sheet fonteTemp = new Sheet();
            splitDatabase.TryGetValue(ConstStrings.C_Fonte, out fonteTemp);
            Sheet gbicTemp = new Sheet();
            splitDatabase.TryGetValue(ConstStrings.C_Gbic, out gbicTemp);
            Sheet hdTemp = new Sheet();
            splitDatabase.TryGetValue(ConstStrings.C_HD, out hdTemp);
            Sheet idracTemp = new Sheet();
            splitDatabase.TryGetValue(ConstStrings.C_Idrac, out idracTemp);
            Sheet memoriaTemp = new Sheet();
            splitDatabase.TryGetValue(ConstStrings.C_Memoria, out memoriaTemp);
            Sheet monitorTemp = new Sheet();
            splitDatabase.TryGetValue(ConstStrings.C_Monitor, out monitorTemp);
            Sheet mouseTemp = new Sheet();
            splitDatabase.TryGetValue(ConstStrings.C_Monitor, out mouseTemp);
            Sheet nobreakTemp = new Sheet();
            splitDatabase.TryGetValue(ConstStrings.C_Monitor, out nobreakTemp);
            Sheet notebookTemp = new Sheet();
            splitDatabase.TryGetValue(ConstStrings.C_Notebook, out notebookTemp);
            Sheet placaControladoraTemp = new Sheet();
            splitDatabase.TryGetValue(ConstStrings.C_PlacaControladora, out placaControladoraTemp);
            Sheet placaDeCapturaDeVideoTemp = new Sheet();
            splitDatabase.TryGetValue(ConstStrings.C_PlacaDeCapturaDeVideo, out placaDeCapturaDeVideoTemp);
            Sheet placaDeRedeTemp = new Sheet();
            splitDatabase.TryGetValue(ConstStrings.C_PlacaDeRede, out placaDeRedeTemp);
            Sheet placaDeSomTemp = new Sheet();
            splitDatabase.TryGetValue(ConstStrings.C_PlacaDeSom, out placaDeSomTemp);
            Sheet placaDeVideoTemp = new Sheet();
            splitDatabase.TryGetValue(ConstStrings.C_PlacaDeVideo, out placaDeVideoTemp);
            Sheet placaSASTemp = new Sheet();
            splitDatabase.TryGetValue(ConstStrings.C_PlacaSAS, out placaSASTemp);
            Sheet processadorTemp = new Sheet();
            splitDatabase.TryGetValue(ConstStrings.C_Processador, out processadorTemp);
            Sheet roteadorTemp = new Sheet();
            splitDatabase.TryGetValue(ConstStrings.C_Roteador, out roteadorTemp);
            Sheet ramalTemp = new Sheet();
            splitDatabase.TryGetValue(ConstStrings.C_Roteador, out ramalTemp);
            Sheet servidorTemp = new Sheet();
            splitDatabase.TryGetValue(ConstStrings.C_Servidor, out servidorTemp);
            Sheet storageNASTemp = new Sheet();
            splitDatabase.TryGetValue(ConstStrings.C_StorageNAS, out storageNASTemp);
            Sheet switchTemp = new Sheet();
            splitDatabase.TryGetValue(ConstStrings.C_Switch, out switchTemp);
            Sheet tecladoTemp = new Sheet();
            splitDatabase.TryGetValue(ConstStrings.C_StorageNAS, out tecladoTemp);
            Sheet outrosTemp = new Sheet();
            splitDatabase.TryGetValue(ConstStrings.C_Outros, out outrosTemp);
            #endregion

            foreach (ItemColumns item in fullDatabase.itens)
            {
                if (item.Categoria.Trim() == ConstStrings.C_AdaptadorAC.Trim())
                {
                    FillCategoryDatabasesFunctions.AdaptadorAC(item, adaptadorACTemp);
                }
                else if (item.Categoria.Trim() == ConstStrings.C_Carregador.Trim())
                {
                    FillCategoryDatabasesFunctions.Carregador(item, carregadorTemp);
                }
                else if (item.Categoria.Trim() == ConstStrings.C_Desktop.Trim())
                {
                    FillCategoryDatabasesFunctions.Desktop(item, desktopTemp);
                }
                else if (item.Categoria.Trim() == ConstStrings.C_FoneRamal.Trim())
                {
                    foneRamal.itens.Add(item);
                }
                else if (item.Categoria.Trim() == ConstStrings.C_Fonte)
                {
                    FillCategoryDatabasesFunctions.Fonte(item, fonteTemp);
                }
                else if (item.Categoria.Trim() == ConstStrings.C_Gbic.Trim())
                {
                    FillCategoryDatabasesFunctions.Gbic(item, gbicTemp);
                }
                else if (item.Categoria.Trim() == ConstStrings.C_HD.Trim())
                {
                    FillCategoryDatabasesFunctions.HD(item, hdTemp);
                }
                else if (item.Categoria.Trim() == ConstStrings.C_Idrac.Trim())
                {
                    FillCategoryDatabasesFunctions.Idrac(item, idracTemp);
                }
                else if (item.Categoria.Trim() == ConstStrings.C_Memoria.Trim())
                {
                    FillCategoryDatabasesFunctions.Memoria(item, memoriaTemp);
                }
                else if (item.Categoria.Trim() == ConstStrings.C_Monitor.Trim())
                {
                    FillCategoryDatabasesFunctions.Monitor(item, monitorTemp);
                }
                else if (item.Categoria.Trim() == ConstStrings.C_Mouse.Trim())
                {
                    mouse.itens.Add(item);
                }
                else if (item.Categoria.Trim() == ConstStrings.C_Nobreak.Trim())
                {
                    nobreak.itens.Add(item);
                }
                else if (item.Categoria.Trim() == ConstStrings.C_Notebook.Trim())
                {
                    FillCategoryDatabasesFunctions.Notebook(item, notebookTemp);
                }
                else if (item.Categoria.Trim() == ConstStrings.C_PlacaControladora.Trim())
                {
                    FillCategoryDatabasesFunctions.PlacaControladora(item, placaControladoraTemp);
                }
                else if (item.Categoria.Trim() == ConstStrings.C_PlacaDeCapturaDeVideo.Trim())
                {
                    FillCategoryDatabasesFunctions.PlacaDeCapturaDeVideo(item, placaDeCapturaDeVideoTemp);
                }
                else if (item.Categoria.Trim() == ConstStrings.C_PlacaDeRede.Trim())
                {
                    FillCategoryDatabasesFunctions.PlacaDeRede(item, placaDeRedeTemp);
                }
                else if (item.Categoria.Trim() == ConstStrings.C_PlacaDeSom.Trim())
                {
                    FillCategoryDatabasesFunctions.PlacaDeSom(item, placaDeSomTemp);
                }
                else if (item.Categoria.Trim() == ConstStrings.C_PlacaDeVideo.Trim())
                {
                    FillCategoryDatabasesFunctions.PlacaDeVideo(item, placaDeVideoTemp);
                }
                else if (item.Categoria.Trim() == ConstStrings.C_PlacaSAS.Trim())
                {
                    FillCategoryDatabasesFunctions.PlacaSAS(item, placaSASTemp);
                }
                else if (item.Categoria.Trim() == ConstStrings.C_Processador.Trim())
                {
                    FillCategoryDatabasesFunctions.Processador(item, processadorTemp);
                }
                else if (item.Categoria.Trim() == ConstStrings.C_Ramal.Trim())
                {
                    ramal.itens.Add(item);
                }
                else if (item.Categoria.Trim() == ConstStrings.C_Roteador.Trim())
                {
                    FillCategoryDatabasesFunctions.Roteador(item, roteadorTemp);
                }
                else if (item.Categoria.Trim() == ConstStrings.C_Servidor.Trim())
                {
                    FillCategoryDatabasesFunctions.Servidor(item, servidorTemp);
                }
                else if (item.Categoria.Trim() == ConstStrings.C_StorageNAS.Trim())
                {
                    FillCategoryDatabasesFunctions.StorageNas(item, storageNASTemp);
                }
                else if (item.Categoria.Trim() == ConstStrings.C_Switch.Trim())
                {
                    FillCategoryDatabasesFunctions.Switch(item, switchTemp);
                }
                else if (item.Categoria.Trim() == ConstStrings.C_Teclado.Trim())
                {
                    teclado.itens.Add(item);
                }
                else
                {
                    outros.itens.Add(item);
                }
            }

        }

        /// <summary>
        /// Get all Sheet classes saved on splitDatabase and join them into a single Sheet class
        /// </summary>
        private void FillFullDatabase()
        {
            Sheet inventarioTemp = new Sheet();
            splitDatabase.TryGetValue(ConstStrings.C_Inventario, out inventarioTemp);
            // testingSheet = inventarioTemp;
            // Get all itens from "Inventario SnPro into the full database
            if (inventarioTemp != null && inventarioTemp.itens.Count > 0)
            {
                for (int i = 0; i < inventarioTemp.itens.Count; i++)
                {
                    fullDatabase.itens.Add(inventarioTemp.itens[i]);
                }
            }

            //UpdateLastProcessadorPatrimonio();
            // Get the values of the detail sheet based on the "modelo" of the item on Inventario SnPro
            FillCategoryDatabases();

            switch (currentEstoque)
            {
                case CurrentEstoque.SnPro:
                case CurrentEstoque.Testing:
                    allFullDetailsSheets = HelperMethods.CreateSheetListFromArray(new Sheet[] { adaptadorAC,
            carregador, desktop, fonte, gbic, hd, idrac, memoria, monitor, nobreak,
            notebook, placaControladora, placaDeCapturaDeVideo, placaDeRede, placaDeSom, placaDeVideo,
            processador,roteador, servidor, storageNAS, Switch, outros});
                    break;
                case CurrentEstoque.Fumsoft:
                case CurrentEstoque.ESF:

                default:
                    allFullDetailsSheets = HelperMethods.CreateSheetListFromArray(new Sheet[] { adaptadorAC,
            carregador, desktop, foneRamal, fonte, gbic, hd, idrac, memoria, monitor, mouse, nobreak,
            notebook, placaControladora, placaDeCapturaDeVideo, placaDeRede, placaDeSom, placaDeVideo,
            processador, ramal, roteador, servidor, storageNAS, Switch, teclado, outros });
                    break;
            }
        }

        public void UpdateLastProcessadorPatrimonio()
        {
            testingSheet = processador;
            testingSheet.itens.Sort((a, b) => a.Patrimonio.CompareTo(b.Patrimonio));
            lastProcessador = testingSheet.itens[0].Patrimonio;
        }

        /// <summary>
        /// Used by the save system to save the pertinent information from this class
        /// </summary>
        public JToken CaptureAsJToken()
        {
            JArray state = HandleSheetsForSaveAndLoad.GetJObject(fullDatabase);

            return state;
        }

        /// <summary>
        /// Used by the save system to load the pertinent information from this class
        /// </summary>
        public void RestoreFromJToken(JToken state)
        {
            HandleSheetsForSaveAndLoad.LoadJObject(state, out sheetToLoad);
            fullDatabase = sheetToLoad;
            FillCategoryDatabases();
        }
    }
}