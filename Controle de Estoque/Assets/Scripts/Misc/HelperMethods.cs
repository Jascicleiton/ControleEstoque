using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices.WindowsRuntime;
using UnityEngine;
using UnityEngine.Networking;

public class HelperMethods
{
    public static bool addUpdateResponse = false;
    public static bool isSingleMessage = false;

    public static string GetCategoryString(int value)
    {
        switch (value)
        {
            case 0:
                return ConstStrings.AdaptadorAC;
            case 1:
                return ConstStrings.Carregador;
            case 2:
                return ConstStrings.Desktop;
            case 3:
                return ConstStrings.FoneRamal;
            case 4:
                return ConstStrings.Fonte;
            case 5:
                return ConstStrings.Gbic;
            case 6:
                return ConstStrings.HD;
            case 7:
                return ConstStrings.Idrac;
            case 8:
                return ConstStrings.Memoria;
            case 9:
                return ConstStrings.Monitor;
            case 10:
                return ConstStrings.Mouse;
            case 11:
                return ConstStrings.Nobreak;
            case 12:
                return ConstStrings.Notebook;
            case 13:
                return ConstStrings.PlacaControladora;
            case 14:
                return ConstStrings.PlacaDeCapturaDeVideo;
            case 15:
                return ConstStrings.PlacaDeRede;
            case 16:
                return ConstStrings.PlacaDeSom;
            case 17:
                return ConstStrings.PlacaDeVideo;
            case 18:
                return ConstStrings.Processador;
            case 19:
                return ConstStrings.Roteador;
            case 20:
                return ConstStrings.Ramal;
            case 21:
                return ConstStrings.Servidor;
            case 22:
                return ConstStrings.StorageNAS;
            case 23:
                return ConstStrings.Switch;
            case 24:
                return ConstStrings.Teclado;
            default:
                return "Adicionar nova categoria";
        }
    }

    public static int GetCategoryInt(string category)
    {
        switch (category)
        {
            case ConstStrings.AdaptadorAC:
                return 0;
            case ConstStrings.Carregador:
                return 1;
            case ConstStrings.Desktop:
                return 2;
            case ConstStrings.FoneRamal:
                return 3;
            case ConstStrings.Fonte:
                return 4;
            case ConstStrings.Gbic:
                return 5;
            case ConstStrings.HD:
                return 6;
            case ConstStrings.Idrac:
                return 7;
            case ConstStrings.Memoria:
                return 8;
            case ConstStrings.Monitor:
                return 9;
            case ConstStrings.Mouse:
                return 10;
            case ConstStrings.Nobreak:
                return 11;
            case ConstStrings.Notebook:
                return 12;
            case ConstStrings.PlacaControladora:
                return 13;
            case ConstStrings.PlacaDeCapturaDeVideo:
                return 14;
            case ConstStrings.PlacaDeRede:
                return 15;
            case ConstStrings.PlacaDeSom:
                return 16;
            case ConstStrings.PlacaDeVideo:
                return 17;
            case ConstStrings.Processador:
                return 18;
            case ConstStrings.Roteador:
                return 19;
            case ConstStrings.Ramal:
                return 20;
            case ConstStrings.Servidor: 
                return 21;
            case ConstStrings.StorageNAS:
                return 22;
            case ConstStrings.Switch:
                return 23;
            default:
                return 666;
        }
    }

    /// <summary>
    /// Root = 0, Import = 1, AddItem = 2, Movements = 3, Update = 4, NoPaNoSe = 5
    /// </summary>
    public static UnityWebRequest GetPostRequest(WWWForm form, string phpName, int folderID)
    {
        UnityWebRequest requestToSend = new UnityWebRequest();
        string folder = "";
        switch (folderID)
        {
            case 0:
                switch (InternalDatabase.Instance.currentEstoque)
                {
                    case CurrentEstoque.SnPro:
                        folder = ConstStrings.PhpRootFolder;
                        break;
                    case CurrentEstoque.Funsoft:
                        folder = ConstStrings.PhpRootFolderFunsoft;
                        break;
                    case CurrentEstoque.ESF:
                        folder = ConstStrings.PhpRootFolderESF;
                        break;
                    case CurrentEstoque.Testing:
                        folder = ConstStrings.PhpRootFolderTesting;
                        break;
                    default:
                        break;
                }                
                break;
            case 1:
                switch (InternalDatabase.Instance.currentEstoque)
                {
                    case CurrentEstoque.SnPro:
                        folder = ConstStrings.PhpImportTablesFolder;
                        break;
                    case CurrentEstoque.Funsoft:
                        folder = ConstStrings.PhpImportTablesFolderFunsoft;
                        break;
                    case CurrentEstoque.ESF:
                        folder = ConstStrings.PhpImportTablesFolderESF;
                        break;
                    case CurrentEstoque.Testing:
                        folder = ConstStrings.PhpImportTablesFolderTesting;
                        break;
                    default:
                        break;
                }
                break;
            case 2:
                switch (InternalDatabase.Instance.currentEstoque)
                {
                    case CurrentEstoque.SnPro:
                        folder = ConstStrings.PhpAdditemsFolder;
                        break;
                    case CurrentEstoque.Funsoft:
                        folder = ConstStrings.PhpAdditemsFolderFunsoft;
                        break;
                    case CurrentEstoque.ESF:
                        folder = ConstStrings.PhpAdditemsFolderESF;
                        break;
                    case CurrentEstoque.Testing:
                        folder = ConstStrings.PhpAdditemsFolderTesting;
                        break;
                    default:
                        break;
                }
                break;
            case 3:
                switch (InternalDatabase.Instance.currentEstoque)
                {
                    case CurrentEstoque.SnPro:
                        folder = ConstStrings.PhpMovementsFolder;
                        break;
                    case CurrentEstoque.Funsoft:
                        folder = ConstStrings.PhpMovementsFolderFunsoft;
                        break;
                    case CurrentEstoque.ESF:
                        folder = ConstStrings.PhpMovementsFolderESF;
                        break;
                    case CurrentEstoque.Testing:
                        folder = ConstStrings.PhpMovementsFolderTesting;
                        break;
                    default:
                        break;
                }
                break;
            case 4:
                switch (InternalDatabase.Instance.currentEstoque)
                {
                    case CurrentEstoque.SnPro:
                        folder = ConstStrings.PhpUpdateItemsFolder;
                        break;
                    case CurrentEstoque.Funsoft:
                        folder = ConstStrings.PhpUpdateItemsFolderFunsoft;
                        break;
                    case CurrentEstoque.ESF:
                        folder = ConstStrings.PhpUpdateItemsFolderESF;
                        break;
                    case CurrentEstoque.Testing:
                        folder = ConstStrings.PhpUpdateItemsFolderTesting;
                        break;
                    default:
                        break;
                }
                break;
            case 5:
                switch (InternalDatabase.Instance.currentEstoque)
                {
                    case CurrentEstoque.SnPro:
                        folder = ConstStrings.PhpNoPaNoSeItemsFolder;
                        break;
                    case CurrentEstoque.Funsoft:
                        folder = ConstStrings.PhpNoPaNoSeItemsFolderFunsoft;
                        break;
                    case CurrentEstoque.ESF:
                        folder = ConstStrings.PhpNoPaNoSeItemsFolderESF;
                        break;
                    case CurrentEstoque.Testing:
                        folder = ConstStrings.PhpNoPaNoSeItemsFolderTesting;
                        break;
                    default:
                        break;
                }
                break;
            default:
                switch (InternalDatabase.Instance.currentEstoque)
                {
                    case CurrentEstoque.SnPro:
                        folder = ConstStrings.PhpRootFolder;
                        break;
                    case CurrentEstoque.Funsoft:
                        folder = ConstStrings.PhpRootFolderFunsoft;
                        break;
                    case CurrentEstoque.ESF:
                        folder = ConstStrings.PhpRootFolderESF;
                        break;
                    case CurrentEstoque.Testing:
                        folder = ConstStrings.PhpRootFolderTesting;
                        break;
                    default:
                        break;
                }
                break;
        }

        requestToSend = UnityWebRequest.Post(folder + phpName, form);
        
        return requestToSend;
    }

    private static void SendWebRequestHandler(UnityWebRequest requestToHandle)
    {
        if (requestToHandle.result == UnityWebRequest.Result.ConnectionError)
        {
            Debug.LogWarning("AddUpdate: conectionerror");
            EventHandler.CallOpenMessageEvent("Server error: 1");
            addUpdateResponse = false;
        }
        else if (requestToHandle.result == UnityWebRequest.Result.DataProcessingError)
        {
            Debug.LogWarning("AddUpdate: data processing error");
            addUpdateResponse = false;
            EventHandler.CallOpenMessageEvent("Server error: 2");
        }
        else if (requestToHandle.result == UnityWebRequest.Result.ProtocolError)
        {
            Debug.LogWarning("AddUpdate: protocol error");
            addUpdateResponse = false;
            EventHandler.CallOpenMessageEvent("Server error: 3");
        }

        if (requestToHandle.error == null)
        {
            string response = requestToHandle.downloadHandler.text;
            if (response == "Conection error")
            {
                Debug.LogWarning("AddUpdate: Server error");
                addUpdateResponse = false;
                EventHandler.CallOpenMessageEvent("Server error: 4");
            }
            else if (response == "Update failed")
            {
                Debug.LogWarning("Update: UpdateQuery failed");
                addUpdateResponse = false;
                EventHandler.CallOpenMessageEvent("Server error: 5");
            }
            else if (response == "insert item failed")
            {
                Debug.LogWarning("Insert: InsertQuery failed");
                addUpdateResponse = false;
                EventHandler.CallOpenMessageEvent("Server error: 6");
            }
            else if(response == "Patrimônio found")
            {
                addUpdateResponse = false;
                EventHandler.CallOpenMessageEvent("Patrimônio já existe");
            }
            else if (response == "Serial found")
            {
                addUpdateResponse = false;
                EventHandler.CallOpenMessageEvent("Serial já existe");
            }
            #region Check if already exists Queries
            else if (response == "Patrimônio query failed")
            {
                Debug.LogWarning("Insert inventario: Patrimonio query failed");
                addUpdateResponse = false;
                EventHandler.CallOpenMessageEvent("Server error: 7.1");
            }
            else if (response == "Serial query failed")
            {
                Debug.LogWarning("Insert Inventario: Serial query failed");
                addUpdateResponse = false;
                EventHandler.CallOpenMessageEvent("Server error: 7.2");
            }
            else if (response == "Desktop Patrimonio query failed")
            {
                Debug.LogWarning("Insert Desktop: Patrimonio query failed");
                addUpdateResponse = false;
                EventHandler.CallOpenMessageEvent("Server error: 7.3");
            }
            else if (response == "Modelo query failed")
            {
                Debug.LogWarning("Insert: Modelo query failed");
                addUpdateResponse = false;
                EventHandler.CallOpenMessageEvent("Server error: 7.4");
            }
            #endregion
            else if (response == "Wrong app key")
            {
                Debug.LogWarning("AddUpdate: app key");
                addUpdateResponse = false;
                EventHandler.CallOpenMessageEvent("Server error: 8");
            }
            else if (response == "Worked")
            {
                Debug.Log("Worked");
                addUpdateResponse = true;
                EventHandler.CallOpenMessageEvent("Worked");
            }
            else if (response == "Updated")
            {
                Debug.Log("Updated");
                addUpdateResponse = true;
                EventHandler.CallOpenMessageEvent("Updated");
            }
            else
            {
                Debug.LogWarning("AddUpdate: " + response);
                addUpdateResponse = false;
                EventHandler.CallOpenMessageEvent("Server error: 9");                
            }
        }
        else
        {
            Debug.LogWarning("AddUpdate: " + requestToHandle.error);
            addUpdateResponse = false;
            EventHandler.CallOpenMessageEvent("Server error: 10");
        }
        requestToHandle.Dispose();
    }

    /// <summary>
    /// Root = 0, Import = 1, AddItem = 2, Movements = 3, Update = 4, NoPaNoSe = 5
    /// </summary>
    public static IEnumerator AddUpdateItem(int catedoryDpValue, int folderID, List<string> parameters, bool addInventario)
    {
        isSingleMessage = !addInventario;
        EventHandler.CallEnableInput(false);
        string appKey = "";
        string phpName = "";
        if (folderID == 2)
        {
            appKey = ConstStrings.AddNewItemKey;
            phpName = "addnewitem";
        }
        else if(folderID == 4)
        {
            appKey = ConstStrings.UpdateItemKey;
            phpName = "update";
        }
        else
        {
            //returnResponse = "Wrong or invalid folderID";
        }
        if (addInventario)
        {
            WWWForm itemForm = CreateForm.GetInventarioForm(appKey, parameters[0], parameters[1], parameters[2], parameters[3],
            parameters[4], parameters[5], parameters[6], parameters[7], parameters[8], parameters[9], parameters[10]);

            UnityWebRequest createUpdateInventarioRequest = GetPostRequest(itemForm, phpName + "inventario.php", folderID);
            MouseManager.Instance.SetWaitingCursor();

            yield return createUpdateInventarioRequest.SendWebRequest();

            SendWebRequestHandler(createUpdateInventarioRequest);
            if (!addUpdateResponse)
            {
                EventHandler.CallIsOneMessageOnlyEvent(true);
                EventHandler.CallOpenMessageEvent("Inventario Failed");
                yield break;
            }
        }       
        else
        {
            switch (GetCategoryString(catedoryDpValue))
            {
                #region HD
                case ConstStrings.HD:
                    WWWForm hdForm = CreateForm.GetHDForm(appKey, parameters[0], parameters[1], parameters[2], parameters[3],
                    parameters[4], parameters[5], parameters[6], parameters[7], parameters[8]);

                    UnityWebRequest createHDPostRequest = GetPostRequest(hdForm, phpName + "hd.php", folderID);

                    yield return createHDPostRequest.SendWebRequest();

                    SendWebRequestHandler(createHDPostRequest);
                    break;
                #endregion
                #region Memoria
                case ConstStrings.Memoria:
                    WWWForm memoriaForm = CreateForm.GetMemoriaForm(appKey, parameters[0], parameters[1], parameters[2], parameters[3],
                    parameters[4], parameters[5], parameters[6], parameters[7], parameters[8], parameters[9]);

                    UnityWebRequest createMemoriaPostRequest = GetPostRequest(memoriaForm, phpName + "memoria.php", folderID);

                    yield return createMemoriaPostRequest.SendWebRequest();

                    SendWebRequestHandler(createMemoriaPostRequest);

                    break;
                #endregion
                #region Placa de rede
                case ConstStrings.PlacaDeRede:
                    WWWForm placaDeRedeForm = CreateForm.GetPlacaDeRedeForm(appKey, parameters[0], parameters[1], parameters[2], parameters[3],
                    parameters[4], parameters[5], parameters[6]);

                    UnityWebRequest createPlacaDeRedePostRequest = GetPostRequest(placaDeRedeForm, phpName + "placarede.php", folderID);

                    yield return createPlacaDeRedePostRequest.SendWebRequest();

                    SendWebRequestHandler(createPlacaDeRedePostRequest);
                    break;
                #endregion
                #region iDrac
                case ConstStrings.Idrac:
                    WWWForm iDracForm = CreateForm.GetiDracForm(appKey, parameters[0], parameters[1], parameters[2], parameters[3],
                   parameters[4], parameters[5]);

                    UnityWebRequest createIdracPostRequest = GetPostRequest(iDracForm, phpName + "idrac.php", folderID);

                    yield return createIdracPostRequest.SendWebRequest();

                    SendWebRequestHandler(createIdracPostRequest);
                    break;
                #endregion
                #region Placa controladora
                case ConstStrings.PlacaControladora:
                    WWWForm placaControladoraForm = CreateForm.GetPlacaControladoraForm(appKey, parameters[0], parameters[1], parameters[2],
                    parameters[3], parameters[4], parameters[5], parameters[6], parameters[7], parameters[8]);

                    UnityWebRequest createPlacaControladoraPostRequest = GetPostRequest(placaControladoraForm, phpName + "placacontroladora.php", folderID);

                    yield return createPlacaControladoraPostRequest.SendWebRequest();

                    SendWebRequestHandler(createPlacaControladoraPostRequest);
                    break;
                #endregion
                #region Processador
                case ConstStrings.Processador:
                    WWWForm processadorForm = CreateForm.GetProcessadorForm(appKey, parameters[0], parameters[1], parameters[2], parameters[3],
                  parameters[4], parameters[5], parameters[6]);

                    UnityWebRequest createProcessadorPostRequest = GetPostRequest(processadorForm, phpName + "processador.php", folderID);

                    yield return createProcessadorPostRequest.SendWebRequest();

                    SendWebRequestHandler(createProcessadorPostRequest);
                    break;
                #endregion
                #region Desktop
                case ConstStrings.Desktop:
                    WWWForm desktopForm = CreateForm.GetDesktopForm(appKey, parameters[0], parameters[1], parameters[2], parameters[3],
                  parameters[4], parameters[5], parameters[6], parameters[7], parameters[8]);

                    UnityWebRequest createDesktopPostRequest = GetPostRequest(desktopForm, phpName + "desktop.php", folderID);

                    yield return createDesktopPostRequest.SendWebRequest();

                    SendWebRequestHandler(createDesktopPostRequest);
                    break;
                #endregion
                #region Fonte
                case ConstStrings.Fonte:
                    WWWForm fonteForm = CreateForm.GetFonteForm(appKey, parameters[0], parameters[1], parameters[2], parameters[3]);

                    UnityWebRequest createFontePostRequest = GetPostRequest(fonteForm, phpName + "fonte.php", folderID);

                    yield return createFontePostRequest.SendWebRequest();

                    SendWebRequestHandler(createFontePostRequest);
                    break;
                #endregion
                #region Switch
                case ConstStrings.Switch:
                    WWWForm switchForm = CreateForm.GetSwitchForm(appKey, parameters[0], parameters[1], parameters[2]);

                    UnityWebRequest createSwitchPostRequest = GetPostRequest(switchForm, phpName + "switch.php", folderID);

                    yield return createSwitchPostRequest.SendWebRequest();

                    SendWebRequestHandler(createSwitchPostRequest);
                    break;
                #endregion
                #region Roteador
                case ConstStrings.Roteador:
                    WWWForm roteadorForm = CreateForm.GetRoteadorForm(appKey, parameters[0], parameters[1], parameters[2], parameters[3],
                  parameters[4]);

                    UnityWebRequest createRoteadorPostRequest = GetPostRequest(roteadorForm, phpName + "roteador.php", folderID);

                    yield return createRoteadorPostRequest.SendWebRequest();

                    SendWebRequestHandler(createRoteadorPostRequest);
                    break;
                #endregion
                #region Carregador
                case ConstStrings.Carregador:
                    WWWForm carregadorForm = CreateForm.GetCarregadorForm(appKey, parameters[0], parameters[1], parameters[2], parameters[3]);

                    UnityWebRequest createCarregadorPostRequest = GetPostRequest(carregadorForm, phpName + "carregador.php", folderID);

                    yield return createCarregadorPostRequest.SendWebRequest();

                    SendWebRequestHandler(createCarregadorPostRequest);
                    break;
                #endregion
                #region Adaptador AC
                case ConstStrings.AdaptadorAC:
                    WWWForm adaptadorAcForm = CreateForm.GetAdaptadorACForm(appKey, parameters[0], parameters[1], parameters[2], parameters[3]);

                    UnityWebRequest createAdaptadorACPostRequest = GetPostRequest(adaptadorAcForm, phpName + "adaptadorac.php", folderID);

                    yield return createAdaptadorACPostRequest.SendWebRequest();

                    SendWebRequestHandler(createAdaptadorACPostRequest);
                    break;
                #endregion
                #region Storage Nas
                case ConstStrings.StorageNAS:
                    WWWForm storageNasForm = CreateForm.GetStorageNASForm(appKey, parameters[0], parameters[1], parameters[2], parameters[3],
                  parameters[4], parameters[5]);

                    UnityWebRequest createStorageNasPostRequest = GetPostRequest(storageNasForm, phpName + "storagenas.php", folderID);

                    yield return createStorageNasPostRequest.SendWebRequest();

                    SendWebRequestHandler(createStorageNasPostRequest);
                    break;
                #endregion
                #region GBIC
                case ConstStrings.Gbic:
                    WWWForm gbicForm = CreateForm.GetGBICForm(appKey, parameters[0], parameters[1], parameters[2]);

                    UnityWebRequest createGBICPostRequest = GetPostRequest(gbicForm, phpName + "gbic.php", folderID);

                    yield return createGBICPostRequest.SendWebRequest();

                    SendWebRequestHandler(createGBICPostRequest);
                    break;
                #endregion
                #region Placa de vídeo
                case ConstStrings.PlacaDeVideo:
                    WWWForm placaDeVideoForm = CreateForm.GetPlacaVideoForm(appKey, parameters[0], parameters[1], parameters[2]);

                    UnityWebRequest createPlacaDeVideoPostRequest = GetPostRequest(placaDeVideoForm, phpName + "placadevideo.php", folderID);

                    yield return createPlacaDeVideoPostRequest.SendWebRequest();

                    SendWebRequestHandler(createPlacaDeVideoPostRequest);
                    break;
                #endregion
                #region Placa de som
                case ConstStrings.PlacaDeSom:
                    WWWForm placaDeSomForm = CreateForm.GetPlacaSomForm(appKey, parameters[0], parameters[1]);

                    UnityWebRequest createPlacaDeSomPostRequest = GetPostRequest(placaDeSomForm, phpName + "placadesom.php", folderID);

                    yield return createPlacaDeSomPostRequest.SendWebRequest();

                    SendWebRequestHandler(createPlacaDeSomPostRequest);
                    break;
                #endregion
                #region Placa de captura de vídeo
                case ConstStrings.PlacaDeCapturaDeVideo:
                    WWWForm placaDeCapturaDeVideoForm = CreateForm.GetPlacaCapturaVideoForm(appKey, parameters[0], parameters[1]);

                    UnityWebRequest createPlacaDeCapturaDeVideoPostRequest = GetPostRequest(placaDeCapturaDeVideoForm, phpName + "placacapturavideo.php", folderID);

                    yield return createPlacaDeCapturaDeVideoPostRequest.SendWebRequest();

                    SendWebRequestHandler(createPlacaDeCapturaDeVideoPostRequest);
                    break;
                #endregion
                #region Servidor
                case ConstStrings.Servidor:
                    WWWForm servidorForm = CreateForm.GetServidorForm(appKey, parameters[0], parameters[1]);

                    UnityWebRequest createServidorPostRequest = GetPostRequest(servidorForm, phpName + "servidor.php", folderID);

                    yield return createServidorPostRequest.SendWebRequest();

                    SendWebRequestHandler(createServidorPostRequest);
                    break;
                #endregion
                #region Notebook
                case ConstStrings.Notebook:
                    WWWForm notebookForm = CreateForm.GetNotebookForm(appKey, parameters[0], parameters[1]);

                    UnityWebRequest createNotebookPostRequest = GetPostRequest(notebookForm, phpName + "notebook.php", folderID);

                    yield return createNotebookPostRequest.SendWebRequest();

                    SendWebRequestHandler(createNotebookPostRequest);
                    break;
                #endregion
                #region Monitor
                case ConstStrings.Monitor:
                    WWWForm monitorForm = CreateForm.GetMonitorForm(appKey, parameters[0], parameters[1], parameters[2], parameters[3]);

                    UnityWebRequest createMonitorPostRequest = GetPostRequest(monitorForm, phpName + "monitor.php", folderID);

                    yield return createMonitorPostRequest.SendWebRequest();

                    SendWebRequestHandler(createMonitorPostRequest);
                    break;
                #endregion
                #region Mouse
                case ConstStrings.Mouse:
                    addUpdateResponse = true;
                    EventHandler.CallOpenMessageEvent("Worked");
                    //  WWWForm mouseForm = CreateForm.GetMouseForm(appKey, parameters[0], parameters[1], parameters[2], parameters[3],
                    //parameters[4], parameters[5]);

                    //  UnityWebRequest createMousePostRequest = GetPostRequest(iDracForm, phpName + "idrac.php", folderID);

                    //  yield return createMousePostRequest.SendWebRequest();

                    //  SendWebRequestHandler(createMousePostRequest);
                    break;
                #endregion
                #region Teclado
                case ConstStrings.Teclado:
                    addUpdateResponse = true;
                    EventHandler.CallOpenMessageEvent("Worked");
                    //  WWWForm iDracForm = CreateForm.GetiDracForm(appKey, parameters[0], parameters[1], parameters[2], parameters[3],
                    //parameters[4], parameters[5]);

                    //  UnityWebRequest createIdracPostRequest = GetPostRequest(iDracForm, phpName + "idrac.php", folderID);

                    //  yield return createIdracPostRequest.SendWebRequest();

                    //  SendWebRequestHandler(createIdracPostRequest);
                    break;
                #endregion
                #region Fone ramal
                case ConstStrings.FoneRamal:
                    addUpdateResponse = true;
                    EventHandler.CallOpenMessageEvent("Worked");
                    //  WWWForm iDracForm = CreateForm.GetiDracForm(appKey, parameters[0], parameters[1], parameters[2], parameters[3],
                    //parameters[4], parameters[5]);

                    //  UnityWebRequest createIdracPostRequest = GetPostRequest(iDracForm, phpName + "idrac.php", folderID);

                    //  yield return createIdracPostRequest.SendWebRequest();

                    //  SendWebRequestHandler(createIdracPostRequest);
                    break;
                #endregion
                #region Ramal
                case ConstStrings.Ramal:
                    addUpdateResponse = true;
                    EventHandler.CallOpenMessageEvent("Worked");
                    //  WWWForm iDracForm = CreateForm.GetiDracForm(appKey, parameters[0], parameters[1], parameters[2], parameters[3],
                    //parameters[4], parameters[5]);

                    //  UnityWebRequest createIdracPostRequest = GetPostRequest(iDracForm, phpName + "idrac.php", folderID);

                    //  yield return createIdracPostRequest.SendWebRequest();

                    //  SendWebRequestHandler(createIdracPostRequest);
                    break;
                #endregion
                #region Nobreak
                case ConstStrings.Nobreak:
                    addUpdateResponse = true;
                    EventHandler.CallOpenMessageEvent("Worked");
                    //  WWWForm iDracForm = CreateForm.GetiDracForm(appKey, parameters[0], parameters[1], parameters[2], parameters[3],
                    //parameters[4], parameters[5]);

                    //  UnityWebRequest createIdracPostRequest = GetPostRequest(iDracForm, phpName + "idrac.php", folderID);

                    //  yield return createIdracPostRequest.SendWebRequest();

                    //  SendWebRequestHandler(createIdracPostRequest);
                    break;
                #endregion
                default:
                    break;
            }
        }
       // return returnResponse;
    }

    public static bool GetAddUpdateResponse()
    {
        return addUpdateResponse;
    }

}
