using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
using SimpleJSON;
using Assets.Scripts.Inventory.Database;
using Assets.Scripts.Misc;
using Assets.Scripts.Mouse;
using Assets.Scripts.Web;

namespace Assets.Scripts.Inventory
{
    public class InventarioManager : Singleton<InventarioManager>
    {
        //[SerializeField] private CurrentEstoque testingEstoque = CurrentEstoque.SnPro;
        void Start()
        {
            //   DontDestroyOnLoad(this.gameObject);
            if (!InternalDatabase.Instance.isOfflineProgram)
            {
                ImportSheets();
            }
        }

        /// <summary>
        /// Import all sheets to internal database. Import different sheets depending of the currentEstoque variable
        /// </summary>
        private void ImportSheets()
        {
            StartCoroutine(ImportInventarioToDatabase());
            switch (InternalDatabase.Instance.currentEstoque)
            {
                case CurrentEstoque.SnPro:
                    ImportSnPro();
                    break;
                case CurrentEstoque.Fumsoft:
                    ImportFumsoft();
                    break;
                case CurrentEstoque.ESF:
                    ImportESF();
                    break;
                case CurrentEstoque.Testing:
                    ImportTesting();
                    break;
                case CurrentEstoque.Clientes:
                    ImportClientes();
                    break;
                case CurrentEstoque.Concert:
                    ImportConcert();
                    break;
                default:
                    break;
            }
        }

        /// <summary>
        /// Call the imports for all categories that exists on the Concert database
        /// </summary>
        private void ImportConcert()
        {
            StartCoroutine(ImportAdaptadorAcToDatabase());
            StartCoroutine(ImportDesktopToDatabase());
            StartCoroutine(ImportMonitorToDatabase());
            StartCoroutine(ImportNotebookToDatabase());
            StartCoroutine(ImportRoteadorToDatabase());
            StartCoroutine(ImportServidorToDatabase());
            StartCoroutine(ImportSwitchToDatabase());
        }

        /// <summary>
        /// Call the imports for all categories that exists on the Clientes database
        /// </summary>
        private void ImportClientes()
        {
            StartCoroutine(ImportHDSheetToDatabase());
            StartCoroutine(ImportMemoriaToDatabase());
            StartCoroutine(ImportPlacaDeVideoToDatabase());
            StartCoroutine(ImportSwitchToDatabase());
        }

        /// <summary>
        /// Call the imports for all categories that exists on the Testing database
        /// </summary>
        private void ImportTesting()
        {
            ImportSnPro();
        }

        /// <summary>
        /// Call the imports for all categories that exists on the ESF database
        /// </summary>
        private void ImportESF()
        {
            StartCoroutine(ImportDesktopToDatabase());
            StartCoroutine(ImportMonitorToDatabase());
            StartCoroutine(ImportNotebookToDatabase());
            StartCoroutine(ImportSwitchToDatabase());
        }

        /// <summary>
        /// Call the imports for all categories that exists on the Fumsoft database
        /// </summary>
        private void ImportFumsoft()
        {
            StartCoroutine(ImportAdaptadorAcToDatabase());
            StartCoroutine(ImportDesktopToDatabase());
            StartCoroutine(ImportMonitorToDatabase());
            StartCoroutine(ImportNotebookToDatabase());
            StartCoroutine(ImportRoteadorToDatabase());
            StartCoroutine(ImportServidorToDatabase());
            StartCoroutine(ImportSwitchToDatabase());
        }

        /// <summary>
        /// Call the imports for all categories that exists on the SnPro database
        /// </summary>
        private void ImportSnPro()
        {
            StartCoroutine(ImportAdaptadorAcToDatabase());
            StartCoroutine(ImportCarregadorToDatabase());
            StartCoroutine(ImportDesktopToDatabase());
            StartCoroutine(ImportFonteToDatabase());
            StartCoroutine(ImportGBICToDatabase());
            StartCoroutine(ImportHDSheetToDatabase());
            StartCoroutine(ImportiDracToDatabase());
            StartCoroutine(ImportMemoriaToDatabase());
            StartCoroutine(ImportMonitorToDatabase());
            StartCoroutine(ImportNotebookToDatabase());
            StartCoroutine(ImportPlacaControladoraToDatabase());
            StartCoroutine(ImportPlacaDeCapturaDeVideoToDatabase());
            StartCoroutine(ImportPlacaDeRedeToDatabase());
            StartCoroutine(ImportPlacaDeSomToDatabase());
            StartCoroutine(ImportPlacaDeVideoToDatabase());
            StartCoroutine(ImportPlacaSASToDatabase());
            StartCoroutine(ImportProcessadorToDatabase());
            StartCoroutine(ImportRoteadorToDatabase());
            StartCoroutine(ImportServidorToDatabase());
            StartCoroutine(ImportStorageNASToDatabase());
            StartCoroutine(ImportSwitchToDatabase());
        }

        #region Import all tables to internal database

        /// <summary>
        /// Import Adaptador AC from server into the internal database
        /// </summary>
        private IEnumerator ImportAdaptadorAcToDatabase()
        {
            WWWForm getInventario = new WWWForm();
            getInventario.AddField("apppassword", "ImportDatabase");

            UnityWebRequest getInventarioRequest = CreatePostRequest.GetPostRequest(getInventario, ConstStrings.ImportAdaptadorAC, 1);

            yield return getInventarioRequest.SendWebRequest();

            Sheet tempSheet = new Sheet();

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
                    ImportingInventoryFunctions.ImportAdaptadorAC(inventario, out tempSheet);

                }
            }
            EventHandler.CallImportFinished(false);
            getInventarioRequest.Dispose();


            if (!InternalDatabase.Instance.splitDatabase.ContainsKey(ConstStrings.C_AdaptadorAC))
            {
                InternalDatabase.Instance.splitDatabase.Add(ConstStrings.C_AdaptadorAC, tempSheet);
            }
            else
            {
                InternalDatabase.Instance.splitDatabase[ConstStrings.C_AdaptadorAC] = tempSheet;
            }
        }

        /// <summary>
        /// Import Carregador from server into the internal database
        /// </summary>
        private IEnumerator ImportCarregadorToDatabase()
        {
            WWWForm getInventario = new WWWForm();
            getInventario.AddField("apppassword", "ImportDatabase");

            UnityWebRequest getInventarioRequest = CreatePostRequest.GetPostRequest(getInventario, ConstStrings.ImportCarregador, 1);

            yield return getInventarioRequest.SendWebRequest();

            Sheet tempSheet = new Sheet();

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
                    ImportingInventoryFunctions.ImportCarregador(inventario, out tempSheet);

                }
            }
            EventHandler.CallImportFinished(false);
            getInventarioRequest.Dispose();

            if (!InternalDatabase.Instance.splitDatabase.ContainsKey(ConstStrings.C_Carregador))
            {
                InternalDatabase.Instance.splitDatabase.Add(ConstStrings.C_Carregador, tempSheet);
            }
            else
            {
                InternalDatabase.Instance.splitDatabase[ConstStrings.C_Carregador] = tempSheet;
            }
        }

        /// <summary>
        /// Import Desktop from server into the internal database
        /// </summary>
        private IEnumerator ImportDesktopToDatabase()
        {
            WWWForm getInventario = new WWWForm();
            getInventario.AddField("apppassword", "ImportDatabase");

            UnityWebRequest getInventarioRequest = CreatePostRequest.GetPostRequest(getInventario, ConstStrings.ImportDesktop, 1);

            yield return getInventarioRequest.SendWebRequest();

            Sheet tempSheet = new Sheet();

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
                    ImportingInventoryFunctions.ImportDesktop(inventario, out tempSheet);

                }
            }
            EventHandler.CallImportFinished(false);
            getInventarioRequest.Dispose();

            if (!InternalDatabase.Instance.splitDatabase.ContainsKey(ConstStrings.C_Desktop))
            {
                InternalDatabase.Instance.splitDatabase.Add(ConstStrings.C_Desktop, tempSheet);
            }
            else
            {
                InternalDatabase.Instance.splitDatabase[ConstStrings.C_Desktop] = tempSheet;
            }
            MouseManager.Instance.SetDefaultCursor();
        }

        /// <summary>
        /// Import Fonte from server into the internal database
        /// </summary>
        private IEnumerator ImportFonteToDatabase()
        {
            WWWForm getInventario = new WWWForm();
            getInventario.AddField("apppassword", "ImportDatabase");

            UnityWebRequest getInventarioRequest = CreatePostRequest.GetPostRequest(getInventario, ConstStrings.ImportFonte, 1);

            yield return getInventarioRequest.SendWebRequest();

            Sheet tempSheet = new Sheet();

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
                    ImportingInventoryFunctions.ImportFonte(inventario, out tempSheet);

                }
            }
            EventHandler.CallImportFinished(false);
            getInventarioRequest.Dispose();

            if (!InternalDatabase.Instance.splitDatabase.ContainsKey(ConstStrings.C_Fonte))
            {
                InternalDatabase.Instance.splitDatabase.Add(ConstStrings.C_Fonte, tempSheet);
            }
            else
            {
                InternalDatabase.Instance.splitDatabase[ConstStrings.C_Fonte] = tempSheet;
            }
        }

        /// <summary>
        /// Import Gbic from server into the internal database
        /// </summary>
        private IEnumerator ImportGBICToDatabase()
        {
            WWWForm getInventario = new WWWForm();
            getInventario.AddField("apppassword", "ImportDatabase");

            UnityWebRequest getInventarioRequest = CreatePostRequest.GetPostRequest(getInventario, ConstStrings.ImportGbic, 1);

            yield return getInventarioRequest.SendWebRequest();

            Sheet tempSheet = new Sheet();

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
                    ImportingInventoryFunctions.ImportGBIC(inventario, out tempSheet);

                }
            }
            EventHandler.CallImportFinished(false);
            getInventarioRequest.Dispose();

            if (!InternalDatabase.Instance.splitDatabase.ContainsKey(ConstStrings.C_Gbic))
            {
                InternalDatabase.Instance.splitDatabase.Add(ConstStrings.C_Gbic, tempSheet);
            }
            else
            {
                InternalDatabase.Instance.splitDatabase[ConstStrings.C_Gbic] = tempSheet;
            }
        }

        /// <summary>
        /// Import HD table from server into the internal database
        /// </summary>
        private IEnumerator ImportHDSheetToDatabase()
        {
            WWWForm getInventario = new WWWForm();
            getInventario.AddField("apppassword", "ImportDatabase");

            UnityWebRequest getInventarioRequest = CreatePostRequest.GetPostRequest(getInventario, ConstStrings.ImportHd, 1);

            //MouseManager.Instance.SetWaitingCursor();       
            yield return getInventarioRequest.SendWebRequest();

            Sheet tempSheet = new Sheet();

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
                    ImportingInventoryFunctions.ImportHD(inventario, out tempSheet);

                }
            }
            else
            {
                Debug.LogWarning("hd \n" + getInventarioRequest.error);
            }
            EventHandler.CallImportFinished(false);
            getInventarioRequest.Dispose();

            if (!InternalDatabase.Instance.splitDatabase.ContainsKey(ConstStrings.C_HD))
            {
                InternalDatabase.Instance.splitDatabase.Add(ConstStrings.C_HD, tempSheet);
            }
            else
            {
                InternalDatabase.Instance.splitDatabase[ConstStrings.C_HD] = tempSheet;
            }
        }

        /// <summary>
        /// Import iDrac from server into the internal database
        /// </summary>
        private IEnumerator ImportiDracToDatabase()
        {
            WWWForm getInventario = new WWWForm();
            getInventario.AddField("apppassword", "ImportDatabase");

            UnityWebRequest getInventarioRequest = CreatePostRequest.GetPostRequest(getInventario, ConstStrings.ImportIdrac, 1);

            yield return getInventarioRequest.SendWebRequest();

            Sheet tempSheet = new Sheet();

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
                    ImportingInventoryFunctions.ImportIdrac(inventario, out tempSheet);

                }
            }
            EventHandler.CallImportFinished(false);
            getInventarioRequest.Dispose();

            if (!InternalDatabase.Instance.splitDatabase.ContainsKey(ConstStrings.C_Idrac))
            {
                InternalDatabase.Instance.splitDatabase.Add(ConstStrings.C_Idrac, tempSheet);
            }
            else
            {
                InternalDatabase.Instance.splitDatabase[ConstStrings.C_Idrac] = tempSheet;
            }
        }

        /// <summary>
        /// Import inventario table from server into the internal database
        /// </summary>
        private IEnumerator ImportInventarioToDatabase()
        {
            WWWForm getInventario = new WWWForm();
            getInventario.AddField("apppassword", "ImportDatabase");
            UnityWebRequest getInventarioRequest = CreatePostRequest.GetPostRequest(getInventario, ConstStrings.ImportInventario, 1);

            yield return getInventarioRequest.SendWebRequest();

            Sheet tempSheet = new Sheet();

            if (getInventarioRequest.result == UnityWebRequest.Result.ConnectionError)
            {
                Debug.LogWarning("inventario conectionerror");
                EventHandler.CallDisconectedFromInternet();
            }
            else if (getInventarioRequest.result == UnityWebRequest.Result.DataProcessingError)
            {
                Debug.LogWarning("inventario data processing error");
                EventHandler.CallDisconectedFromInternet();

            }
            else if (getInventarioRequest.result == UnityWebRequest.Result.ProtocolError)
            {
                Debug.LogWarning("inventario protocol error");
                EventHandler.CallDisconectedFromInternet();
            }
            if (getInventarioRequest.error == null)
            {
                string response = getInventarioRequest.downloadHandler.text;
                if (response == "Database connection error" || response == "wrong appkey" || response == "Query failed")
                {
                    Debug.LogWarning("inventario: Server error");
                    EventHandler.CallDisconectedFromInternet();
                }
                else if (response == "Result came empty")
                {
                    Debug.LogWarning("inventario: Result came empty");
                }
                else
                {
                    JSONNode inventario = JSON.Parse(getInventarioRequest.downloadHandler.text);
                    ImportingInventoryFunctions.ImportInventory(inventario, out tempSheet);
                }
            }
            else
            {
                Debug.LogWarning("inventario\n " + getInventarioRequest.error);
                EventHandler.CallDisconectedFromInternet();
            }
            EventHandler.CallImportFinished(true);
            getInventarioRequest.Dispose();

            if (!InternalDatabase.Instance.splitDatabase.ContainsKey(ConstStrings.C_Inventario))
            {
                InternalDatabase.Instance.splitDatabase.Add(ConstStrings.C_Inventario, tempSheet);
            }
            else
            {
                InternalDatabase.Instance.splitDatabase[ConstStrings.C_Inventario] = tempSheet;
            }
        }

        /// <summary>
        /// Import Memória from server into the internal database
        /// </summary>
        private IEnumerator ImportMemoriaToDatabase()
        {
            WWWForm getInventario = new WWWForm();
            getInventario.AddField("apppassword", "ImportDatabase");

            UnityWebRequest getInventarioRequest = CreatePostRequest.GetPostRequest(getInventario, ConstStrings.ImportMemoria, 1);

            yield return getInventarioRequest.SendWebRequest();

            Sheet tempSheet = new Sheet();

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
                    ImportingInventoryFunctions.ImportMemoria(inventario, out tempSheet);

                }
            }
            else
            {
                Debug.LogWarning("memoria \n" + getInventarioRequest.error);
            }
            EventHandler.CallImportFinished(false);
            getInventarioRequest.Dispose();

            if (!InternalDatabase.Instance.splitDatabase.ContainsKey(ConstStrings.C_Memoria))
            {
                InternalDatabase.Instance.splitDatabase.Add(ConstStrings.C_Memoria, tempSheet);
            }
            else
            {
                InternalDatabase.Instance.splitDatabase[ConstStrings.C_Memoria] = tempSheet;
            }
        }

        /// <summary>
        /// Import Monitor from server into the internal database
        /// </summary>
        private IEnumerator ImportMonitorToDatabase()
        {
            WWWForm getInventario = new WWWForm();
            getInventario.AddField("apppassword", "ImportDatabase");

            UnityWebRequest getInventarioRequest = CreatePostRequest.GetPostRequest(getInventario, ConstStrings.ImportMonitor, 1);

            yield return getInventarioRequest.SendWebRequest();

            Sheet tempSheet = new Sheet();

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
                    ImportingInventoryFunctions.ImportMonitor(inventario, out tempSheet);

                }
            }
            EventHandler.CallImportFinished(false);
            getInventarioRequest.Dispose();

            if (!InternalDatabase.Instance.splitDatabase.ContainsKey(ConstStrings.C_Monitor))
            {
                InternalDatabase.Instance.splitDatabase.Add(ConstStrings.C_Monitor, tempSheet);
            }
            else
            {
                InternalDatabase.Instance.splitDatabase[ConstStrings.C_Monitor] = tempSheet;
            }
        }

        /// <summary>
        /// Import Notebook from server into the internal database
        /// </summary>
        private IEnumerator ImportNotebookToDatabase()
        {
            WWWForm getInventario = new WWWForm();
            getInventario.AddField("apppassword", "ImportDatabase");

            UnityWebRequest getInventarioRequest = CreatePostRequest.GetPostRequest(getInventario, ConstStrings.ImportNotebook, 1);

            yield return getInventarioRequest.SendWebRequest();

            Sheet tempSheet = new Sheet();

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
                    ImportingInventoryFunctions.ImportNotebook(inventario, out tempSheet);

                }
            }
            EventHandler.CallImportFinished(false);
            getInventarioRequest.Dispose();

            if (!InternalDatabase.Instance.splitDatabase.ContainsKey(ConstStrings.C_Notebook))
            {
                InternalDatabase.Instance.splitDatabase.Add(ConstStrings.C_Notebook, tempSheet);
            }
            else
            {
                InternalDatabase.Instance.splitDatabase[ConstStrings.C_Notebook] = tempSheet;
            }
        }

        /// <summary>
        /// Import Placa controladora from server into the internal database
        /// </summary>
        private IEnumerator ImportPlacaControladoraToDatabase()
        {
            WWWForm getInventario = new WWWForm();
            getInventario.AddField("apppassword", "ImportDatabase");

            UnityWebRequest getInventarioRequest = CreatePostRequest.GetPostRequest(getInventario, ConstStrings.ImportPlacaControladora, 1);

            yield return getInventarioRequest.SendWebRequest();

            Sheet tempSheet = new Sheet();

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
                    ImportingInventoryFunctions.ImportPlacaControladora(inventario, out tempSheet);

                }
            }
            getInventarioRequest.Dispose();
            EventHandler.CallImportFinished(false);
            if (!InternalDatabase.Instance.splitDatabase.ContainsKey(ConstStrings.C_PlacaControladora))
            {
                InternalDatabase.Instance.splitDatabase.Add(ConstStrings.C_PlacaControladora, tempSheet);
            }
            else
            {
                InternalDatabase.Instance.splitDatabase[ConstStrings.C_PlacaControladora] = tempSheet;
            }
            MouseManager.Instance.SetDefaultCursor();
        }

        /// <summary>
        /// Import Placa de captura de video from server into the internal database
        /// </summary>
        private IEnumerator ImportPlacaDeCapturaDeVideoToDatabase()
        {
            WWWForm getInventario = new WWWForm();
            getInventario.AddField("apppassword", "ImportDatabase");

            UnityWebRequest getInventarioRequest = CreatePostRequest.GetPostRequest(getInventario, ConstStrings.ImportPlacaDeCapturaDeVideo, 1);

            yield return getInventarioRequest.SendWebRequest();

            Sheet tempSheet = new Sheet();

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
                    ImportingInventoryFunctions.ImportPlacaDeCapturaDeVideo(inventario, out tempSheet);

                }
            }
            EventHandler.CallImportFinished(false);
            getInventarioRequest.Dispose();

            if (!InternalDatabase.Instance.splitDatabase.ContainsKey(ConstStrings.C_PlacaDeCapturaDeVideo))
            {
                InternalDatabase.Instance.splitDatabase.Add(ConstStrings.C_PlacaDeCapturaDeVideo, tempSheet);
            }
            else
            {
                InternalDatabase.Instance.splitDatabase[ConstStrings.C_PlacaDeCapturaDeVideo] = tempSheet;
            }
        }

        /// <summary>
        /// Import Placa de Rede from server into the internal database
        /// </summary>
        private IEnumerator ImportPlacaDeRedeToDatabase()
        {
            WWWForm getInventario = new WWWForm();
            getInventario.AddField("apppassword", "ImportDatabase");

            UnityWebRequest getInventarioRequest = CreatePostRequest.GetPostRequest(getInventario, ConstStrings.ImportPlacaDeRede, 1);

            yield return getInventarioRequest.SendWebRequest();

            Sheet tempSheet = new Sheet();

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
                    ImportingInventoryFunctions.ImportPlacaDeRede(inventario, out tempSheet);

                }
            }
            EventHandler.CallImportFinished(false);
            getInventarioRequest.Dispose();

            if (!InternalDatabase.Instance.splitDatabase.ContainsKey(ConstStrings.C_PlacaDeRede))
            {
                InternalDatabase.Instance.splitDatabase.Add(ConstStrings.C_PlacaDeRede, tempSheet);
            }
            else
            {
                InternalDatabase.Instance.splitDatabase[ConstStrings.C_PlacaDeRede] = tempSheet;
            }
            MouseManager.Instance.SetDefaultCursor();
        }

        /// <summary>
        /// Import PlacaDeSom from server into the internal database
        /// </summary>
        private IEnumerator ImportPlacaDeSomToDatabase()
        {
            WWWForm getInventario = new WWWForm();
            getInventario.AddField("apppassword", "ImportDatabase");

            UnityWebRequest getInventarioRequest = CreatePostRequest.GetPostRequest(getInventario, ConstStrings.ImportPlacaDeSom, 1);

            yield return getInventarioRequest.SendWebRequest();

            Sheet tempSheet = new Sheet();

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
                    ImportingInventoryFunctions.ImportPlacaDeSom(inventario, out tempSheet);

                }
            }
            EventHandler.CallImportFinished(false);
            getInventarioRequest.Dispose();

            if (!InternalDatabase.Instance.splitDatabase.ContainsKey(ConstStrings.C_PlacaDeSom))
            {
                InternalDatabase.Instance.splitDatabase.Add(ConstStrings.C_PlacaDeSom, tempSheet);
            }
            else
            {
                InternalDatabase.Instance.splitDatabase[ConstStrings.C_PlacaDeSom] = tempSheet;
            }
        }

        /// <summary>
        /// Import PlacaDeVideo from server into the internal database
        /// </summary>
        private IEnumerator ImportPlacaDeVideoToDatabase()
        {
            WWWForm getInventario = new WWWForm();
            getInventario.AddField("apppassword", "ImportDatabase");

            UnityWebRequest getInventarioRequest = CreatePostRequest.GetPostRequest(getInventario, ConstStrings.ImportPlacaDeVideo, 1);

            yield return getInventarioRequest.SendWebRequest();

            Sheet tempSheet = new Sheet();

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
                    ImportingInventoryFunctions.ImportPlacaDeVideo(inventario, out tempSheet);

                }
            }
            EventHandler.CallImportFinished(false);
            getInventarioRequest.Dispose();

            if (!InternalDatabase.Instance.splitDatabase.ContainsKey(ConstStrings.C_PlacaDeVideo))
            {
                InternalDatabase.Instance.splitDatabase.Add(ConstStrings.C_PlacaDeVideo, tempSheet);
            }
            else
            {
                InternalDatabase.Instance.splitDatabase[ConstStrings.C_PlacaDeVideo] = tempSheet;
            }
        }

        /// <summary>
        /// Import PlacaSAS from server into the internal database
        /// </summary>
        private IEnumerator ImportPlacaSASToDatabase()
        {
            WWWForm getInventario = new WWWForm();
            getInventario.AddField("apppassword", "ImportDatabase");

            UnityWebRequest getInventarioRequest = CreatePostRequest.GetPostRequest(getInventario, ConstStrings.ImportPlacaSAS, 1);

            yield return getInventarioRequest.SendWebRequest();

            Sheet tempSheet = new Sheet();

            if (getInventarioRequest.result == UnityWebRequest.Result.ConnectionError)
            {
                Debug.LogWarning("Placa SAS: conection error");
            }
            else if (getInventarioRequest.result == UnityWebRequest.Result.DataProcessingError)
            {
                Debug.LogWarning("Placa SAS: data processing error");
            }
            else if (getInventarioRequest.result == UnityWebRequest.Result.ProtocolError)
            {
                Debug.LogWarning("Placa SAS: protocol error");
            }
            if (getInventarioRequest.error == null)
            {
                string response = getInventarioRequest.downloadHandler.text;
                if (response == "Database connection error" || response == "wrong appkey" || response == "Query failed")
                {
                    Debug.LogWarning("Placa SAS: Server error");
                }
                else if (response == "Result came empty")
                {
                    Debug.LogWarning("Placa SAS: Result came empty");
                }
                else
                {
                    JSONNode inventario = JSON.Parse(getInventarioRequest.downloadHandler.text);
                    ImportingInventoryFunctions.ImportPlacaSAS(inventario, out tempSheet);

                }
            }
            EventHandler.CallImportFinished(false);
            getInventarioRequest.Dispose();

            if (!InternalDatabase.Instance.splitDatabase.ContainsKey(ConstStrings.C_PlacaSAS))
            {
                InternalDatabase.Instance.splitDatabase.Add(ConstStrings.C_PlacaSAS, tempSheet);
            }
            else
            {
                InternalDatabase.Instance.splitDatabase[ConstStrings.C_PlacaSAS] = tempSheet;
            }
        }

        /// <summary>
        /// Import Processador from server into the internal database
        /// </summary>
        private IEnumerator ImportProcessadorToDatabase()
        {
            WWWForm getInventario = new WWWForm();
            getInventario.AddField("apppassword", "ImportDatabase");

            UnityWebRequest getInventarioRequest = CreatePostRequest.GetPostRequest(getInventario, ConstStrings.ImportProcessador, 1);

            yield return getInventarioRequest.SendWebRequest();

            Sheet tempSheet = new Sheet();

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
                    ImportingInventoryFunctions.ImportProcessador(inventario, out tempSheet);

                }
            }
            EventHandler.CallImportFinished(false);
            getInventarioRequest.Dispose();
            if (!InternalDatabase.Instance.splitDatabase.ContainsKey(ConstStrings.C_Processador))
            {
                InternalDatabase.Instance.splitDatabase.Add(ConstStrings.C_Processador, tempSheet);
            }
            else
            {
                InternalDatabase.Instance.splitDatabase[ConstStrings.C_Processador] = tempSheet;
            }
            MouseManager.Instance.SetDefaultCursor();
        }

        /// <summary>
        /// Import Roteador from server into the internal database
        /// </summary>
        private IEnumerator ImportRoteadorToDatabase()
        {
            WWWForm getInventario = new WWWForm();
            getInventario.AddField("apppassword", "ImportDatabase");

            UnityWebRequest getInventarioRequest = CreatePostRequest.GetPostRequest(getInventario, ConstStrings.ImportRoteador, 1);

            yield return getInventarioRequest.SendWebRequest();

            Sheet tempSheet = new Sheet();

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
                    ImportingInventoryFunctions.ImportRoteador(inventario, out tempSheet);

                }
            }
            EventHandler.CallImportFinished(false);
            getInventarioRequest.Dispose();

            if (!InternalDatabase.Instance.splitDatabase.ContainsKey(ConstStrings.C_Roteador))
            {
                InternalDatabase.Instance.splitDatabase.Add(ConstStrings.C_Roteador, tempSheet);
            }
            else
            {
                InternalDatabase.Instance.splitDatabase[ConstStrings.C_Roteador] = tempSheet;
            }
        }

        /// <summary>
        /// Import Servidor from server into the internal database
        /// </summary>
        private IEnumerator ImportServidorToDatabase()
        {
            WWWForm getInventario = new WWWForm();
            getInventario.AddField("apppassword", "ImportDatabase");

            UnityWebRequest getInventarioRequest = CreatePostRequest.GetPostRequest(getInventario, ConstStrings.ImportServidor, 1);

            yield return getInventarioRequest.SendWebRequest();

            Sheet tempSheet = new Sheet();

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
                    ImportingInventoryFunctions.ImportServidor(inventario, out tempSheet);

                }
            }
            EventHandler.CallImportFinished(false);
            getInventarioRequest.Dispose();

            if (!InternalDatabase.Instance.splitDatabase.ContainsKey(ConstStrings.C_Servidor))
            {
                InternalDatabase.Instance.splitDatabase.Add(ConstStrings.C_Servidor, tempSheet);
            }
            else
            {
                InternalDatabase.Instance.splitDatabase[ConstStrings.C_Servidor] = tempSheet;
            }
        }

        /// <summary>
        /// Import StorageNas from server into the internal database
        /// </summary>
        private IEnumerator ImportStorageNASToDatabase()
        {
            WWWForm getInventario = new WWWForm();
            getInventario.AddField("apppassword", "ImportDatabase");

            UnityWebRequest getInventarioRequest = CreatePostRequest.GetPostRequest(getInventario, ConstStrings.ImportStorageNas, 1);

            yield return getInventarioRequest.SendWebRequest();

            Sheet tempSheet = new Sheet();

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
                    ImportingInventoryFunctions.ImportStorageNas(inventario, out tempSheet);

                }
            }
            EventHandler.CallImportFinished(false);
            getInventarioRequest.Dispose();

            if (!InternalDatabase.Instance.splitDatabase.ContainsKey(ConstStrings.C_StorageNAS))
            {
                InternalDatabase.Instance.splitDatabase.Add(ConstStrings.C_StorageNAS, tempSheet);
            }
            else
            {
                InternalDatabase.Instance.splitDatabase[ConstStrings.C_StorageNAS] = tempSheet;
            }
        }

        /// <summary>
        /// Import Switch from server into the internal database
        /// </summary>
        private IEnumerator ImportSwitchToDatabase()
        {
            WWWForm getInventario = new WWWForm();
            getInventario.AddField("apppassword", "ImportDatabase");

            UnityWebRequest getInventarioRequest = CreatePostRequest.GetPostRequest(getInventario, ConstStrings.ImportSwitch, 1);

            yield return getInventarioRequest.SendWebRequest();

            Sheet tempSheet = new Sheet();

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
                    ImportingInventoryFunctions.ImportSwitch(inventario, out tempSheet);

                }
            }
            EventHandler.CallImportFinished(false);
            getInventarioRequest.Dispose();

            if (!InternalDatabase.Instance.splitDatabase.ContainsKey(ConstStrings.C_Switch))
            {
                InternalDatabase.Instance.splitDatabase.Add(ConstStrings.C_Switch, tempSheet);
            }
            else
            {
                InternalDatabase.Instance.splitDatabase[ConstStrings.C_Switch] = tempSheet;
            }
        }
        #endregion
    }
}