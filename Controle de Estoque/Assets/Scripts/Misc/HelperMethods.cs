using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices.WindowsRuntime;
using UnityEngine;
using UnityEngine.Networking;

public class HelperMethods
{
    public static bool addUpdateResponse = false;
    public static bool isSingleMessage = false;

    /// <summary>
    /// Get the category "name" based on the value of the dropdown item selected
    /// </summary>
    public static string GetCategoryString(int value)
    {
        switch (InternalDatabase.Instance.currentEstoque)
        {
            case CurrentEstoque.SnPro:
                if (value < ConstStrings.SNPCategories.Length)
                {
                    return ConstStrings.SNPCategories[value];
                }
                else
                {
                    return "Adicionar nova categoria";
                }
            case CurrentEstoque.Funsoft:
                if (value < ConstStrings.AllCategories.Length)
                {
                    return ConstStrings.AllCategories[value];
                }
                else
                {
                    return "Adicionar nova categoria";
                }
            case CurrentEstoque.ESF:
                if (value < ConstStrings.AllCategories.Length)
                {
                    return ConstStrings.AllCategories[value];
                }
                else
                {
                    return "Adicionar nova categoria";
                }
            case CurrentEstoque.Testing:
                if (value < ConstStrings.AllCategories.Length)
                {
                    return ConstStrings.AllCategories[value];
                }
                else
                {
                    return "Adicionar nova categoria";
                }
            case CurrentEstoque.Clientes:
                if (value < ConstStrings.AllCategories.Length)
                {
                    return ConstStrings.AllCategories[value];
                }
                else
                {
                    return "Adicionar nova categoria";
                }
            default:
                if (value < ConstStrings.AllCategories.Length)
                {
                    return ConstStrings.AllCategories[value];
                }
                else
                {
                    return "Adicionar nova categoria";
                }
        }
    }

    /// <summary>
    /// Transforms an array of Sheets into a List of Sheets
    /// </summary>
    public static List<Sheet> CreateSheetListFromArray(Sheet[] array)
    {
        List<Sheet> list = new List<Sheet>();
        if (array.Length > 0)
        {
            for (int i = 0; i < array.Length; i++)
            {
                list.Add(array[i]);
            }
        }
        return list;
    }

    /// <summary>
    /// Get the index of a category based on its name
    /// </summary>
    public static int GetCategoryInt(string category)
    {
        switch (InternalDatabase.Instance.currentEstoque)
        {
            case CurrentEstoque.SnPro:
                for (int i = 0; i < ConstStrings.SNPCategories.Length; i++)
                {
                    if (category == ConstStrings.SNPCategories[i])
                    {
                        return i;
                    }
                }
                return 666;
            case CurrentEstoque.Funsoft:
                for (int i = 0; i < ConstStrings.AllCategories.Length; i++)
                {
                    if (category == ConstStrings.AllCategories[i])
                    {
                        return i;
                    }
                }
                return 666;
            case CurrentEstoque.ESF:
                for (int i = 0; i < ConstStrings.AllCategories.Length; i++)
                {
                    if (category == ConstStrings.AllCategories[i])
                    {
                        return i;
                    }
                }
                return 666;
            case CurrentEstoque.Testing:
                for (int i = 0; i < ConstStrings.AllCategories.Length; i++)
                {
                    if (category == ConstStrings.AllCategories[i])
                    {
                        return i;
                    }
                }
                return 666;
            default:
                for (int i = 0; i < ConstStrings.SNPCategories.Length; i++)
                {
                    if (category == ConstStrings.SNPCategories[i])
                    {
                        return i;
                    }
                }
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
                    case CurrentEstoque.Clientes:
                        folder = ConstStrings.PhpRootFolderClientes;
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
                    case CurrentEstoque.Clientes:
                        folder = ConstStrings.PhpImportTablesFolderClientes;
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
                    case CurrentEstoque.Clientes:
                        folder = ConstStrings.PhpAdditemsFolderClientes;
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
                    case CurrentEstoque.Clientes:
                        folder = ConstStrings.PhpMovementsFolderClientes;
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
                    case CurrentEstoque.Clientes:
                        folder = ConstStrings.PhpUpdateItemsFolderClientes;
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
                    case CurrentEstoque.Clientes:
                        folder = ConstStrings.PhpNoPaNoSeItemsFolderClientes;
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
                    case CurrentEstoque.Clientes:
                        folder = ConstStrings.PhpRootFolderClientes;
                        break;
                    default:
                        break;
                }
                break;
        }

        requestToSend = UnityWebRequest.Post(folder + phpName, form);

        return requestToSend;
    }

    /// <summary>
    /// Handles what happens after a web request is sent
    /// </summary>
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
            else if (response == "Patrimônio found")
            {
                Debug.Log("Patrimônio duplicado");
                addUpdateResponse = false;
                EventHandler.CallIsOneMessageOnlyEvent(true);
                EventHandler.CallOpenMessageEvent("Patrimônio já existe");
            }
            else if (response == "Serial found")
            {
                addUpdateResponse = false;
                EventHandler.CallIsOneMessageOnlyEvent(true);
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
        else if (folderID == 4)
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
             //   EventHandler.CallIsOneMessageOnlyEvent(true);
               // EventHandler.CallOpenMessageEvent("Inventario Failed");
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
                    WWWForm placaDeCapturaDeVideoForm = CreateForm.GetPlacaDeCapturaDeVideoForm(appKey, parameters[0], parameters[1]);

                    UnityWebRequest createPlacaDeCapturaDeVideoPostRequest = GetPostRequest(placaDeCapturaDeVideoForm, phpName + "placacapturavideo.php", folderID);

                    yield return createPlacaDeCapturaDeVideoPostRequest.SendWebRequest();

                    SendWebRequestHandler(createPlacaDeCapturaDeVideoPostRequest);
                    break;
                #endregion
                #region Servidor
                case ConstStrings.Servidor:
                    Debug.Log(parameters.Count);
                    WWWForm servidorForm = CreateForm.GetServidorForm(appKey, parameters[0], parameters[1], parameters[2], parameters[3],
                    parameters[4], parameters[5], parameters[6], parameters[7], parameters[8], parameters[9], parameters[10], parameters[11],
                     parameters[12], parameters[13], parameters[14], parameters[15], parameters[16], parameters[17], parameters[18]);

                    UnityWebRequest createServidorPostRequest = GetPostRequest(servidorForm, phpName + "servidor.php", folderID);

                    yield return createServidorPostRequest.SendWebRequest();

                    SendWebRequestHandler(createServidorPostRequest);
                    break;
                #endregion
                #region Notebook
                case ConstStrings.Notebook:
                    WWWForm notebookForm = CreateForm.GetNotebookForm(appKey, parameters[0], parameters[1], parameters[2], parameters[3], parameters[4],
                    parameters[5], parameters[6], parameters[7], parameters[8]);

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
                    addUpdateResponse = true;
                    EventHandler.CallOpenMessageEvent("Worked");
                    break;
            }
        }
        // return returnResponse;
    }

    /// <summary>
    /// Get if the Add or Update is true or false
    /// </summary>
    public static bool GetAddUpdateResponse()
    {
        return addUpdateResponse;
    }

    /// <summary>
    /// Create a dictionary with all the parameters values and names of a specific item to be shown to the user
    /// </summary>
    public static Dictionary<string, List<string>> GetParameterValuesAndNames(ItemColumns itemToShow, string category)
    {
        Dictionary<string, List<string>> dictionary = new Dictionary<string, List<string>>();
        dictionary.Add("Names", new List<string>());
        dictionary.Add("Values", new List<string>());
        #region Values
        if (itemToShow != null)
        {
            #region Inventário
            dictionary["Values"].Add(itemToShow.Aquisicao);
            dictionary["Values"].Add(itemToShow.Entrada);
            dictionary["Values"].Add(itemToShow.Patrimonio.ToString());
            dictionary["Values"].Add(itemToShow.Status);
            dictionary["Values"].Add(itemToShow.Serial);
            dictionary["Values"].Add(itemToShow.Categoria);
            dictionary["Values"].Add(itemToShow.Fabricante);
            dictionary["Values"].Add(itemToShow.Modelo);
            dictionary["Values"].Add(itemToShow.Local);
            dictionary["Values"].Add(itemToShow.Saida);
            dictionary["Values"].Add(itemToShow.Observacao);
            #endregion
            switch (itemToShow.Categoria)
            {
                #region Adaptador AC
                case ConstStrings.AdaptadorAC:
                    dictionary["Values"].Add(itemToShow.OndeFunciona);
                    dictionary["Values"].Add(itemToShow.VoltagemDeSaida.ToString());
                    dictionary["Values"].Add(itemToShow.AmperagemDeSaida.ToString());
                    break;
                #endregion
                #region Carregador
                case ConstStrings.Carregador:
                    dictionary["Values"].Add(itemToShow.OndeFunciona);
                    dictionary["Values"].Add(itemToShow.VoltagemDeSaida.ToString());
                    dictionary["Values"].Add(itemToShow.AmperagemDeSaida.ToString());
                    break;
                #endregion
                #region Desktop
                case ConstStrings.Desktop:
                    dictionary["Values"].Add(itemToShow.ModeloPlacaMae);
                    dictionary["Values"].Add(itemToShow.Fonte);
                    dictionary["Values"].Add(itemToShow.Memoria);
                    dictionary["Values"].Add(itemToShow.HD);
                    dictionary["Values"].Add(itemToShow.PlacaDeVideo);
                    dictionary["Values"].Add(itemToShow.LeitorDeDVD);
                    dictionary["Values"].Add(itemToShow.Processador);
                    break;
                #endregion
                #region Fone para ramal
                case ConstStrings.FoneRamal:
                    break;
                #endregion
                #region Fonte
                case ConstStrings.Fonte:
                    dictionary["Values"].Add(itemToShow.Watts.ToString());
                    dictionary["Values"].Add(itemToShow.OndeFunciona);
                    dictionary["Values"].Add(itemToShow.Conectores);
                    break;
                #endregion
                #region GBIC
                case ConstStrings.Gbic:
                    dictionary["Values"].Add(itemToShow.Desempenho);
                    break;
                #endregion
                #region HD
                case ConstStrings.HD:
                    dictionary["Values"].Add(itemToShow.Interface);
                    dictionary["Values"].Add(itemToShow.Tamanho.ToString());
                    dictionary["Values"].Add(itemToShow.FormaDeArmazenamento);
                    dictionary["Values"].Add(itemToShow.CapacidadeEmGB.ToString());
                    dictionary["Values"].Add(itemToShow.RPM.ToString());
                    dictionary["Values"].Add(itemToShow.VelocidadeDeLeitura.ToString());
                    dictionary["Values"].Add(itemToShow.Enterprise);
                    break;
                #endregion
                #region iDrac
                case ConstStrings.Idrac:
                    dictionary["Values"].Add(itemToShow.QuaisConexoes);
                    dictionary["Values"].Add(itemToShow.VelocidadeGBs.ToString());
                    dictionary["Values"].Add(itemToShow.EntradaSD);
                    dictionary["Values"].Add(itemToShow.ServidoresSuportados);
                    break;
                #endregion
                #region Memoria
                case ConstStrings.Memoria:
                    dictionary["Values"].Add(itemToShow.Tipo);
                    dictionary["Values"].Add(itemToShow.CapacidadeEmGB.ToString());
                    dictionary["Values"].Add(itemToShow.VelocidadeMHz.ToString());
                    dictionary["Values"].Add(itemToShow.LowVoltage);
                    dictionary["Values"].Add(itemToShow.Rank);
                    dictionary["Values"].Add(itemToShow.DIMM);
                    dictionary["Values"].Add(itemToShow.TaxaDeTransmissao.ToString());
                    dictionary["Values"].Add(itemToShow.Simbolo);
                    break;
                #endregion
                #region Monitor
                case ConstStrings.Monitor:
                    dictionary["Values"].Add(itemToShow.Polegadas.ToString());
                    dictionary["Values"].Add(itemToShow.QuaisConexoes);
                    break;
                #endregion
                #region Mouse
                case ConstStrings.Mouse:

                    break;
                #endregion
                #region No break
                case ConstStrings.Nobreak:
                    break;
                #endregion
                #region Notebook
                case ConstStrings.Notebook:
                    dictionary["Values"].Add(itemToShow.HD);
                    dictionary["Values"].Add(itemToShow.Memoria);
                    dictionary["Values"].Add(itemToShow.EntradaRJ49);
                    dictionary["Values"].Add(itemToShow.BateriaInclusa);
                    dictionary["Values"].Add(itemToShow.AdaptadorAC);
                    dictionary["Values"].Add(itemToShow.Windows);
                    break;
                #endregion
                #region Placa controladora
                case ConstStrings.PlacaControladora:
                    dictionary["Values"].Add(itemToShow.QuaisConexoes);
                    dictionary["Values"].Add(itemToShow.QuantidadeDePortas.ToString());
                    dictionary["Values"].Add(itemToShow.TipoDeRAID);
                    dictionary["Values"].Add(itemToShow.CapacidadeMaxHD);
                    dictionary["Values"].Add(itemToShow.AteQuantosHDs.ToString());
                    dictionary["Values"].Add(itemToShow.BateriaInclusa);
                    dictionary["Values"].Add(itemToShow.Barramento);
                    break;
                #endregion
                #region Placa de captura de video
                case ConstStrings.PlacaDeCapturaDeVideo:
                    dictionary["Values"].Add(itemToShow.QuantidadeDePortas.ToString());
                    break;
                #endregion
                #region Placa de rede
                case ConstStrings.PlacaDeRede:
                    dictionary["Values"].Add(itemToShow.Interface);
                    dictionary["Values"].Add(itemToShow.QuantidadeDePortas.ToString());
                    dictionary["Values"].Add(itemToShow.QuaisConexoes);
                    dictionary["Values"].Add(itemToShow.SuportaFibraOptica);
                    dictionary["Values"].Add(itemToShow.Desempenho);
                    break;
                #endregion
                #region Placa de som
                case ConstStrings.PlacaDeSom:
                    dictionary["Values"].Add(itemToShow.QuantosCanais.ToString());
                    break;
                #endregion
                #region Placa de Video
                case ConstStrings.PlacaDeVideo:
                    dictionary["Values"].Add(itemToShow.QuantidadeDePortas.ToString());
                    dictionary["Values"].Add(itemToShow.QuaisConexoes);
                    break;
                #endregion
                #region Processador
                case ConstStrings.Processador:
                    dictionary["Values"].Add(itemToShow.Soquete);
                    dictionary["Values"].Add(itemToShow.NucleosFisicos.ToString());
                    dictionary["Values"].Add(itemToShow.NucleosLogicos.ToString());
                    dictionary["Values"].Add(itemToShow.AceitaVirtualizacao);
                    dictionary["Values"].Add(itemToShow.TurboBoost);
                    dictionary["Values"].Add(itemToShow.HyperThreading);
                    break;
                #endregion
                #region Ramal
                case ConstStrings.Ramal:
                    break;
                #endregion
                #region Roteador
                case ConstStrings.Roteador:
                    dictionary["Values"].Add(itemToShow.Wireless);
                    dictionary["Values"].Add(itemToShow.QuantidadeDePortas.ToString());
                    dictionary["Values"].Add(itemToShow.BandaMaxima.ToString());
                    break;
                #endregion
                #region Servidor
                case ConstStrings.Servidor:
                    dictionary["Values"].Add(itemToShow.ModeloPlacaMae);
                    dictionary["Values"].Add(itemToShow.Fonte);
                    dictionary["Values"].Add(itemToShow.Memoria);
                    dictionary["Values"].Add(itemToShow.HD);
                    dictionary["Values"].Add(itemToShow.PlacaDeVideo);
                    dictionary["Values"].Add(itemToShow.PlacaDeRede);
                    dictionary["Values"].Add(itemToShow.Processador);
                    dictionary["Values"].Add(itemToShow.MemoriasSuportadas);
                    dictionary["Values"].Add(itemToShow.QuantasMemorias.ToString());
                    dictionary["Values"].Add(itemToShow.OrdemDasMemorias);
                    dictionary["Values"].Add(itemToShow.CapacidadeRAMTotal.ToString());
                    dictionary["Values"].Add(itemToShow.Soquete);
                    dictionary["Values"].Add(itemToShow.PlacaControladora);
                    dictionary["Values"].Add(itemToShow.AteQuantosHDs.ToString());
                    dictionary["Values"].Add(itemToShow.TipoDeHD);
                    dictionary["Values"].Add(itemToShow.TipoDeRAID);
                    break;
                #endregion
                #region Storage NAS
                case ConstStrings.StorageNAS:
                    dictionary["Values"].Add(itemToShow.Tamanho.ToString());
                    dictionary["Values"].Add(itemToShow.TipoDeRAID);
                    dictionary["Values"].Add(itemToShow.TipoDeHD);
                    dictionary["Values"].Add(itemToShow.CapacidadeMaxHD);
                    dictionary["Values"].Add(itemToShow.AteQuantosHDs.ToString());
                    break;
                #endregion
                #region Switch
                case ConstStrings.Switch:
                    dictionary["Values"].Add(itemToShow.QuantidadeDePortas.ToString());
                    dictionary["Values"].Add(itemToShow.Desempenho);
                    break;
                #endregion
                #region Teclado
                case ConstStrings.Teclado:
                    break;
                #endregion
                default:
                    break;
            }
        }
        #endregion
        #region Names
        #region Inventário
        dictionary["Names"].Add("Aquisição");
        dictionary["Names"].Add("Entrada");
        dictionary["Names"].Add("Patrimônio");
        dictionary["Names"].Add("Status");
        dictionary["Names"].Add("Serial");
        dictionary["Names"].Add("Categoria");
        dictionary["Names"].Add("Fabricante");
        dictionary["Names"].Add("Modelo");
        dictionary["Names"].Add("Local");
        dictionary["Names"].Add("Saída");
        dictionary["Names"].Add("Observação");
        #endregion
        switch (category)
        {
            #region Adaptador AC
            case ConstStrings.AdaptadorAC:
                dictionary["Names"].Add("Onde funciona?");
                dictionary["Names"].Add("Voltagem de saída");
                dictionary["Names"].Add("Amperagem de saída (A)");
                break;
            #endregion
            #region Carregador
            case ConstStrings.Carregador:
                dictionary["Names"].Add("Onde funciona?");
                dictionary["Names"].Add("Voltagem de saída");
                dictionary["Names"].Add("Amperagem de saída (mA)");
                break;
            #endregion
            #region Desktop
            case ConstStrings.Desktop:
                dictionary["Names"].Add("Modelo de placa mãe");
                dictionary["Names"].Add("Fonte?");
                dictionary["Names"].Add("Memória?");
                dictionary["Names"].Add("HD?");
                dictionary["Names"].Add("Placa de vídeo?");
                dictionary["Names"].Add("Leitor de DVD?");
                dictionary["Names"].Add("Processador");
                break;
            #endregion
            #region Fone para ramal
            case ConstStrings.FoneRamal:
                break;
            #endregion
            #region Fonte
            case ConstStrings.Fonte:
                dictionary["Names"].Add("Watts de potência");
                dictionary["Names"].Add("Onde funciona?");
                dictionary["Names"].Add("Conectores");
                break;
            #endregion
            #region GBIC
            case ConstStrings.Gbic:
                dictionary["Names"].Add("Desempenho máx (GB/s)");
                break;
            #endregion
            #region HD
            case ConstStrings.HD:
                dictionary["Names"].Add("Interface");
                dictionary["Names"].Add("Tamanho");
                dictionary["Names"].Add("Forma de armazenamento");
                dictionary["Names"].Add("Capacidade (GB)");
                dictionary["Names"].Add("RPM");
                dictionary["Names"].Add("Velocidade de Leitura (Gb/s)");
                dictionary["Names"].Add("Enterprise");
                break;
            #endregion
            #region iDrac
            case ConstStrings.Idrac:
                dictionary["Names"].Add("Porta");
                dictionary["Names"].Add("Velocidade (GB/s)");
                dictionary["Names"].Add("Entrada SD");
                dictionary["Names"].Add("Servidores suportados");
                break;
            #endregion
            #region Memoria
            case ConstStrings.Memoria:
                dictionary["Names"].Add("Tipo");
                dictionary["Names"].Add("Capacidade (GB)");
                dictionary["Names"].Add("Velocidade (MHz)");
                dictionary["Names"].Add("É low voltage?");
                dictionary["Names"].Add("Rank");
                dictionary["Names"].Add("DIMM");
                dictionary["Names"].Add("Taxa de transmissão");
                dictionary["Names"].Add("Símbolo");
                break;
            #endregion
            #region Monitor
            case ConstStrings.Monitor:
                dictionary["Names"].Add("Polegadas");
                dictionary["Names"].Add("Quais entradas?");
                break;
            #endregion
            #region Mouse
            case ConstStrings.Mouse:

                break;
            #endregion
            #region No break
            case ConstStrings.Nobreak:
                break;
            #endregion
            #region Notebook
            case ConstStrings.Notebook:
                dictionary["Names"].Add("HD");
                dictionary["Names"].Add("Memória");
                dictionary["Names"].Add("Entrada RJ49");
                dictionary["Names"].Add("Bateria");
                dictionary["Names"].Add("AdaptadorAC");
                dictionary["Names"].Add("Windows");
                break;
            #endregion
            #region Placa controladora
            case ConstStrings.PlacaControladora:
                dictionary["Names"].Add("Tipo de conexão");
                dictionary["Names"].Add("Quantas portas?");
                dictionary["Names"].Add("Tipos de RAID");
                dictionary["Names"].Add("Capacidade máx do HD (TB)");
                dictionary["Names"].Add("Até quantos HDs");
                dictionary["Names"].Add("Bateria inclusa?");
                dictionary["Names"].Add("Barramento");
                break;
            #endregion
            #region Placa de captura de video
            case ConstStrings.PlacaDeCapturaDeVideo:
                dictionary["Names"].Add("Quantas entradas?");
                break;
            #endregion
            #region Placa de rede
            case ConstStrings.PlacaDeRede:
                dictionary["Names"].Add("Interface");
                dictionary["Names"].Add("Quantas portas?");
                dictionary["Names"].Add("Quais portas?");
                dictionary["Names"].Add("Suporta fibra óptica?");
                dictionary["Names"].Add("Desempenho (MB/s)");
                break;
            #endregion
            #region Placa de som
            case ConstStrings.PlacaDeSom:
                dictionary["Names"].Add("Quantos canais?");
                break;
            #endregion
            #region Placa de Video
            case ConstStrings.PlacaDeVideo:
                dictionary["Names"].Add("Quantas entradas?");
                dictionary["Names"].Add("Quais entradas?");
                break;
            #endregion
            #region Processador
            case ConstStrings.Processador:
                dictionary["Names"].Add("Soquete");
                dictionary["Names"].Add("Nº núcleos físicos");
                dictionary["Names"].Add("Nº núcleos lógicos");
                dictionary["Names"].Add("Aceita virtualização?");
                dictionary["Names"].Add("Turbo boost?");
                dictionary["Names"].Add("Hyper-Threading?");
                break;
            #endregion
            #region Ramal
            case ConstStrings.Ramal:
                break;
            #endregion
            #region Roteador
            case ConstStrings.Roteador:
                dictionary["Names"].Add("Wireless?");
                dictionary["Names"].Add("Quantas entradas?");
                dictionary["Names"].Add("Banda máx (MB/s)");
                break;
            #endregion
            #region Servidor
            case ConstStrings.Servidor:
                dictionary["Names"].Add("Modelo da placa mãe");
                dictionary["Names"].Add("Fonte");
                dictionary["Names"].Add("Memórias instaladas");
                dictionary["Names"].Add("HD instalado");
                dictionary["Names"].Add("Placa de vídeo");
                dictionary["Names"].Add("Placa de rede");
                dictionary["Names"].Add("Processadores instalados");
                dictionary["Names"].Add("Memórias suportadas");
                dictionary["Names"].Add("Até quantas memórias");
                dictionary["Names"].Add("Ordem das memórias");
                dictionary["Names"].Add("Capacidade RAM total");
                dictionary["Names"].Add("Soquete do processador");
                dictionary["Names"].Add("Placa controladora");
                dictionary["Names"].Add("Até quantos HDs");
                dictionary["Names"].Add("Tipos de HD");
                dictionary["Names"].Add("Tipos de RAID");
                break;
            #endregion
            #region Storage NAS
            case ConstStrings.StorageNAS:
                dictionary["Names"].Add("Tamanho dos HDs");
                dictionary["Names"].Add("Tipos de RAID");
                dictionary["Names"].Add("Tipo de HD");
                dictionary["Names"].Add("Capacidade máx do HD");
                dictionary["Names"].Add("Até quantos HDs");
                break;
            #endregion
            #region Switch
            case ConstStrings.Switch:
                dictionary["Names"].Add("Quantas entradas");
                dictionary["Names"].Add("Capacidade máx de cada porta (MB/s)");
                break;
            #endregion
            #region Teclado
            case ConstStrings.Teclado:
                break;
            #endregion
            default:
                break;
        }
        #endregion
        return dictionary;
    }
     /// <summary>
     /// Get the a location string based on the value of the dropdown
     /// </summary>
         public static string GetLocationFromDP(int value)
    {
        string location = InternalDatabase.locations[value];
        return location;
    }

    /// <summary>
    /// Get the dropdown value based on a string location
    /// </summary>
    public static int GetLocationDPValue(string location)
    {
        int dpValue = 13;
        for (int i = 0; i < InternalDatabase.locations.Count; i++)
        {
            if(location == InternalDatabase.locations[i])
            {
                dpValue = i;
                break;
            }
        }
        return dpValue;
    }

    /// <summary>
    /// Compares two strings using = or < or > or != or <= or >= operators
    /// </summary>
        public static bool CompareStrings(string parameter1, string parameter2, string Operator)
    {
         //Debug.Log(parameter1 + ", " + parameter2 + ", " + Operator);
        switch (Operator)
        {
            case "=":
                return parameter1 == parameter2;
            case "<":              
                return double.Parse(parameter1) < double.Parse(parameter2);
            case ">":
                return float.Parse(parameter1) > float.Parse(parameter2);
            case "!=":
                return parameter1 != parameter2;
            case "<=":
                return float.Parse(parameter1) <= float.Parse(parameter2);
            case ">=":
                return float.Parse(parameter1) >= float.Parse(parameter2);
            default:
                return false;
        }
    }
}
