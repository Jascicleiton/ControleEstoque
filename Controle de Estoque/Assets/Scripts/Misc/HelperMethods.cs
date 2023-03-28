using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices.WindowsRuntime;
using System.Text.RegularExpressions;
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
        if(value < InternalDatabase.categories.Count)
        {
            return InternalDatabase.categories[value];
        }
        else
        {
            return "Adicionar nova categoria";
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
        for (int i = 0; i < InternalDatabase.categories.Count; i++)
        {
            if (category == ConstStrings.SNPCategories[i])
            {
                return i;
            }
        }
        return 666;
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
            WWWForm itemForm = CreateForm.GetInventarioForm(appKey, parameters);
                       
            UnityWebRequest createUpdateInventarioRequest = CreatePostRequest.GetPostRequest(itemForm, phpName + "inventario.php", folderID);
            MouseManager.Instance.SetWaitingCursor();

            yield return createUpdateInventarioRequest.SendWebRequest();

           addUpdateResponse = HandlePostRequestResponse.HandleWebRequest(createUpdateInventarioRequest);
            if (!addUpdateResponse)
            {
                //EventHandler.CallIsOneMessageOnlyEvent(true);
                //EventHandler.CallOpenMessageEvent("Inventario Failed");
                yield break;
            }
        }
        else
        {
            switch (GetCategoryString(catedoryDpValue))
            {
                #region Adaptador AC
                case ConstStrings.AdaptadorAC:
                    WWWForm adaptadorAcForm = CreateForm.GetAdaptadorACForm(appKey, parameters);

                    UnityWebRequest createAdaptadorACPostRequest = CreatePostRequest.GetPostRequest(adaptadorAcForm, phpName + "adaptadorac.php", folderID);

                    yield return createAdaptadorACPostRequest.SendWebRequest();

                    addUpdateResponse = HandlePostRequestResponse.HandleWebRequest(createAdaptadorACPostRequest);
                    break;
                #endregion
                #region Carregador
                case ConstStrings.Carregador:
                    WWWForm carregadorForm = CreateForm.GetCarregadorForm(appKey, parameters);

                    UnityWebRequest createCarregadorPostRequest = CreatePostRequest.GetPostRequest(carregadorForm, phpName + "carregador.php", folderID);

                    yield return createCarregadorPostRequest.SendWebRequest();

                    addUpdateResponse = HandlePostRequestResponse.HandleWebRequest(createCarregadorPostRequest);
                    break;
                #endregion
                #region Desktop
                case ConstStrings.Desktop:
                    WWWForm desktopForm = CreateForm.GetDesktopForm(appKey, parameters);
                    
                    UnityWebRequest createDesktopPostRequest = CreatePostRequest.GetPostRequest(desktopForm, phpName + "desktop.php", folderID);

                    yield return createDesktopPostRequest.SendWebRequest();

                    addUpdateResponse = HandlePostRequestResponse.HandleWebRequest(createDesktopPostRequest);
                    break;
                #endregion
                #region Fonte
                case ConstStrings.Fonte:
                    WWWForm fonteForm = CreateForm.GetFonteForm(appKey, parameters[0], parameters[1], parameters[2], parameters[3]);

                    UnityWebRequest createFontePostRequest = CreatePostRequest.GetPostRequest(fonteForm, phpName + "fonte.php", folderID);

                    yield return createFontePostRequest.SendWebRequest();

                    addUpdateResponse = HandlePostRequestResponse.HandleWebRequest(createFontePostRequest);
                    break;
                #endregion
                #region GBIC
                case ConstStrings.Gbic:
                    WWWForm gbicForm = CreateForm.GetGBICForm(appKey, parameters[0], parameters[1], parameters[2]);

                    UnityWebRequest createGBICPostRequest = CreatePostRequest.GetPostRequest(gbicForm, phpName + "gbic.php", folderID);

                    yield return createGBICPostRequest.SendWebRequest();

                    addUpdateResponse = HandlePostRequestResponse.HandleWebRequest(createGBICPostRequest);
                    break;
                #endregion
                #region HD
                case ConstStrings.HD:
                    WWWForm hdForm = CreateForm.GetHDForm(appKey, parameters[0], parameters[1], parameters[2], parameters[3],
                    parameters[4], parameters[5], parameters[6], parameters[7], parameters[8]);

                    UnityWebRequest createHDPostRequest = CreatePostRequest.GetPostRequest(hdForm, phpName + "hd.php", folderID);

                    yield return createHDPostRequest.SendWebRequest();

                    addUpdateResponse = HandlePostRequestResponse.HandleWebRequest(createHDPostRequest);
                    break;
                #endregion
                #region iDrac
                case ConstStrings.Idrac:
                    WWWForm iDracForm = CreateForm.GetiDracForm(appKey, parameters[0], parameters[1], parameters[2], parameters[3],
                   parameters[4], parameters[5]);

                    UnityWebRequest createIdracPostRequest = CreatePostRequest.GetPostRequest(iDracForm, phpName + "idrac.php", folderID);

                    yield return createIdracPostRequest.SendWebRequest();

                    addUpdateResponse = HandlePostRequestResponse.HandleWebRequest(createIdracPostRequest);
                    break;
                #endregion
                #region Memoria
                case ConstStrings.Memoria:
                    WWWForm memoriaForm = CreateForm.GetMemoriaForm(appKey, parameters[0], parameters[1], parameters[2], parameters[3],
                    parameters[4], parameters[5], parameters[6], parameters[7], parameters[8], parameters[9]);

                    UnityWebRequest createMemoriaPostRequest = CreatePostRequest.GetPostRequest(memoriaForm, phpName + "memoria.php", folderID);

                    yield return createMemoriaPostRequest.SendWebRequest();

                    addUpdateResponse = HandlePostRequestResponse.HandleWebRequest(createMemoriaPostRequest);

                    break;
                #endregion
                #region Monitor
                case ConstStrings.Monitor:
                    WWWForm monitorForm = CreateForm.GetMonitorForm(appKey, parameters);
                   
                    UnityWebRequest createMonitorPostRequest = CreatePostRequest.GetPostRequest(monitorForm, phpName + "monitor.php", folderID);

                    yield return createMonitorPostRequest.SendWebRequest();

                    addUpdateResponse = HandlePostRequestResponse.HandleWebRequest(createMonitorPostRequest);
                    break;
                #endregion
                #region Notebook
                case ConstStrings.Notebook:
                    WWWForm notebookForm = CreateForm.GetNotebookForm(appKey, parameters);
                    
                    UnityWebRequest createNotebookPostRequest = CreatePostRequest.GetPostRequest(notebookForm, phpName + "notebook.php", folderID);

                    yield return createNotebookPostRequest.SendWebRequest();

                    addUpdateResponse = HandlePostRequestResponse.HandleWebRequest(createNotebookPostRequest);
                    break;
                #endregion
                #region Placa controladora
                case ConstStrings.PlacaControladora:
                    WWWForm placaControladoraForm = CreateForm.GetPlacaControladoraForm(appKey, parameters[0], parameters[1], parameters[2],
                    parameters[3], parameters[4], parameters[5], parameters[6], parameters[7], parameters[8]);

                    UnityWebRequest createPlacaControladoraPostRequest = CreatePostRequest.GetPostRequest(placaControladoraForm, phpName + "placacontroladora.php", folderID);

                    yield return createPlacaControladoraPostRequest.SendWebRequest();

                    addUpdateResponse = HandlePostRequestResponse.HandleWebRequest(createPlacaControladoraPostRequest);
                    break;
                #endregion
                #region Placa de captura de vídeo
                case ConstStrings.PlacaDeCapturaDeVideo:
                    WWWForm placaDeCapturaDeVideoForm = CreateForm.GetPlacaDeCapturaDeVideoForm(appKey, parameters[0], parameters[1]);

                    UnityWebRequest createPlacaDeCapturaDeVideoPostRequest = CreatePostRequest.GetPostRequest(placaDeCapturaDeVideoForm, phpName + "placacapturavideo.php", folderID);

                    yield return createPlacaDeCapturaDeVideoPostRequest.SendWebRequest();

                    addUpdateResponse = HandlePostRequestResponse.HandleWebRequest(createPlacaDeCapturaDeVideoPostRequest);
                    break;
                #endregion
                #region Placa de rede
                case ConstStrings.PlacaDeRede:
                    WWWForm placaDeRedeForm = CreateForm.GetPlacaDeRedeForm(appKey, parameters[0], parameters[1], parameters[2], parameters[3],
                    parameters[4], parameters[5], parameters[6]);

                    UnityWebRequest createPlacaDeRedePostRequest = CreatePostRequest.GetPostRequest(placaDeRedeForm, phpName + "placarede.php", folderID);

                    yield return createPlacaDeRedePostRequest.SendWebRequest();

                    addUpdateResponse = HandlePostRequestResponse.HandleWebRequest(createPlacaDeRedePostRequest);
                    break;
                #endregion
                #region Placa de som
                case ConstStrings.PlacaDeSom:
                    WWWForm placaDeSomForm = CreateForm.GetPlacaSomForm(appKey, parameters[0], parameters[1]);

                    UnityWebRequest createPlacaDeSomPostRequest = CreatePostRequest.GetPostRequest(placaDeSomForm, phpName + "placadesom.php", folderID);

                    yield return createPlacaDeSomPostRequest.SendWebRequest();

                    addUpdateResponse = HandlePostRequestResponse.HandleWebRequest(createPlacaDeSomPostRequest);
                    break;
                #endregion
                #region Placa de vídeo
                case ConstStrings.PlacaDeVideo:
                    WWWForm placaDeVideoForm = CreateForm.GetPlacaVideoForm(appKey, parameters[0], parameters[1], parameters[2]);

                    UnityWebRequest createPlacaDeVideoPostRequest = CreatePostRequest.GetPostRequest(placaDeVideoForm, phpName + "placadevideo.php", folderID);

                    yield return createPlacaDeVideoPostRequest.SendWebRequest();

                    addUpdateResponse = HandlePostRequestResponse.HandleWebRequest(createPlacaDeVideoPostRequest);
                    break;
                #endregion
                #region Processador
                case ConstStrings.Processador:
                    WWWForm processadorForm = CreateForm.GetProcessadorForm(appKey, parameters[0], parameters[1], parameters[2], parameters[3]);

                    UnityWebRequest createProcessadorPostRequest = CreatePostRequest.GetPostRequest(processadorForm, phpName + "processador.php", folderID);

                    yield return createProcessadorPostRequest.SendWebRequest();

                    addUpdateResponse = HandlePostRequestResponse.HandleWebRequest(createProcessadorPostRequest);
                    break;
                #endregion
                #region Roteador
                case ConstStrings.Roteador:
                    WWWForm roteadorForm = CreateForm.GetRoteadorForm(appKey, parameters);

                    UnityWebRequest createRoteadorPostRequest = CreatePostRequest.GetPostRequest(roteadorForm, phpName + "roteador.php", folderID);

                    yield return createRoteadorPostRequest.SendWebRequest();

                    addUpdateResponse = HandlePostRequestResponse.HandleWebRequest(createRoteadorPostRequest);
                    break;
                #endregion
                #region Servidor
                case ConstStrings.Servidor:

                    WWWForm servidorForm = CreateForm.GetServidorForm(appKey, parameters);
                   
                    UnityWebRequest createServidorPostRequest = CreatePostRequest.GetPostRequest(servidorForm, phpName + "servidor.php", folderID);

                    yield return createServidorPostRequest.SendWebRequest();

                    addUpdateResponse = HandlePostRequestResponse.HandleWebRequest(createServidorPostRequest);
                    break;
                #endregion
                #region Storage Nas
                case ConstStrings.StorageNAS:
                    WWWForm storageNasForm = CreateForm.GetStorageNASForm(appKey, parameters[0], parameters[1], parameters[2], parameters[3],
                  parameters[4], parameters[5]);

                    UnityWebRequest createStorageNasPostRequest = CreatePostRequest.GetPostRequest(storageNasForm, phpName + "storagenas.php", folderID);

                    yield return createStorageNasPostRequest.SendWebRequest();

                    addUpdateResponse = HandlePostRequestResponse.HandleWebRequest(createStorageNasPostRequest);
                    break;
                #endregion
                #region Switch
                case ConstStrings.Switch:
                    WWWForm switchForm = CreateForm.GetSwitchForm(appKey, parameters);
                    
                    UnityWebRequest createSwitchPostRequest = CreatePostRequest.GetPostRequest(switchForm, phpName + "switch.php", folderID);

                    yield return createSwitchPostRequest.SendWebRequest();

                    addUpdateResponse = HandlePostRequestResponse.HandleWebRequest(createSwitchPostRequest);
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
            switch (InternalDatabase.Instance.currentEstoque)
            {
                case CurrentEstoque.Concert:
                    dictionary["Values"].Add(itemToShow.Aquisicao);
                    dictionary["Values"].Add(itemToShow.Entrada);
                    dictionary["Values"].Add(itemToShow.Patrimonio.ToString());
                    dictionary["Values"].Add(itemToShow.Status);
                    dictionary["Values"].Add(itemToShow.Serial);
                    dictionary["Values"].Add(itemToShow.Categoria);
                    dictionary["Values"].Add(itemToShow.Fabricante);
                    dictionary["Values"].Add(itemToShow.Modelo);
                    dictionary["Values"].Add(itemToShow.Local);
                    dictionary["Values"].Add(itemToShow.Pessoa);
                    dictionary["Values"].Add(itemToShow.CentroDeCusto);
                    dictionary["Values"].Add(itemToShow.Saida);

                    break;
                default:
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
                    break;
            }
            #endregion
            switch (itemToShow.Categoria)
            {
                #region Adaptador AC
                case ConstStrings.AdaptadorAC:
                    switch (InternalDatabase.Instance.currentEstoque)
                    {
                        case CurrentEstoque.SnPro:
                            dictionary["Values"].Add(itemToShow.OndeFunciona);
                            dictionary["Values"].Add(itemToShow.VoltagemDeSaida.ToString());
                            dictionary["Values"].Add(itemToShow.AmperagemDeSaida.ToString());
                            break;
                        default:
                            dictionary["Values"].Add(itemToShow.VoltagemDeSaida.ToString());
                            dictionary["Values"].Add(itemToShow.AmperagemDeSaida.ToString());
                            break;
                    }
                    break;
                #endregion
                #region Carregador
                case ConstStrings.Carregador:
                    switch (InternalDatabase.Instance.currentEstoque)
                    {
                        case CurrentEstoque.SnPro:
                            dictionary["Values"].Add(itemToShow.OndeFunciona);
                            dictionary["Values"].Add(itemToShow.VoltagemDeSaida.ToString());
                            dictionary["Values"].Add(itemToShow.AmperagemDeSaida.ToString());
                            break;
                        default:
                            dictionary["Values"].Add(itemToShow.VoltagemDeSaida.ToString());
                            dictionary["Values"].Add(itemToShow.AmperagemDeSaida.ToString());
                            break;
                    }
                    break;
                #endregion
                #region Desktop
                case ConstStrings.Desktop:
                    switch (InternalDatabase.Instance.currentEstoque)
                    {
                        case CurrentEstoque.SnPro:
                            dictionary["Values"].Add(itemToShow.ModeloPlacaMae);
                            dictionary["Values"].Add(itemToShow.Fonte);
                            dictionary["Values"].Add(itemToShow.Memoria);
                            dictionary["Values"].Add(itemToShow.HD);
                            dictionary["Values"].Add(itemToShow.PlacaDeVideo);
                            dictionary["Values"].Add(itemToShow.LeitorDeDVD);
                            dictionary["Values"].Add(itemToShow.Processador);
                            break;
                        default:
                            dictionary["Values"].Add(itemToShow.HD);
                            dictionary["Values"].Add(itemToShow.Memoria);
                            dictionary["Values"].Add(itemToShow.Processador);
                            dictionary["Values"].Add(itemToShow.Windows);
                            break;
                    }
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
                #region Notebook
                case ConstStrings.Notebook:
                    switch (InternalDatabase.Instance.currentEstoque)
                    {
                        case CurrentEstoque.SnPro:
                            dictionary["Values"].Add(itemToShow.HD);
                            dictionary["Values"].Add(itemToShow.Memoria);
                            dictionary["Values"].Add(itemToShow.EntradaRJ45);
                            dictionary["Values"].Add(itemToShow.BateriaInclusa);
                            dictionary["Values"].Add(itemToShow.AdaptadorAC);
                            dictionary["Values"].Add(itemToShow.Windows);
                            break;
                        default:
                            dictionary["Values"].Add(itemToShow.HD);
                            dictionary["Values"].Add(itemToShow.Memoria);
                            dictionary["Values"].Add(itemToShow.Processador);
                            dictionary["Values"].Add(itemToShow.Windows);
                            break;
                    }
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
                                   break;
                #endregion
                #region Roteador
                case ConstStrings.Roteador:
                    switch (InternalDatabase.Instance.currentEstoque)
                    {
                        case CurrentEstoque.SnPro:
                            dictionary["Values"].Add(itemToShow.Wireless);
                            dictionary["Values"].Add(itemToShow.QuantidadeDePortas.ToString());
                            dictionary["Values"].Add(itemToShow.BandaMaxima.ToString());
                            break;
                        default:
                            dictionary["Values"].Add(itemToShow.Wireless);
                            dictionary["Values"].Add(itemToShow.QuantidadeDePortas.ToString());
                            break;
                    }
                    break;
                #endregion
                #region Servidor
                case ConstStrings.Servidor:
                    switch (InternalDatabase.Instance.currentEstoque)
                    {
                        case CurrentEstoque.SnPro:
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
                        default:
                            dictionary["Values"].Add(itemToShow.HD);
                            dictionary["Values"].Add(itemToShow.Memoria);
                            dictionary["Values"].Add(itemToShow.Processador);
                            dictionary["Values"].Add(itemToShow.Windows);
                            break;
                    }
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
                    switch (InternalDatabase.Instance.currentEstoque)
                    {
                        case CurrentEstoque.SnPro:
                            dictionary["Values"].Add(itemToShow.QuantidadeDePortas.ToString());
                            dictionary["Values"].Add(itemToShow.Desempenho);
                            break;
                        default:
                            dictionary["Values"].Add(itemToShow.QuaisConexoes);
                            break;
                    }
                    break;
                #endregion
                default:
                    break;
            }
        }
        #endregion
        #region Names
        #region Inventário
        switch (InternalDatabase.Instance.currentEstoque)
        {
            case CurrentEstoque.Concert:
                dictionary["Names"].Add("Aquisição");
                dictionary["Names"].Add("Entrada");
                dictionary["Names"].Add("Patrimônio");
                dictionary["Names"].Add("Status");
                dictionary["Names"].Add("Serial");
                dictionary["Names"].Add("Categoria");
                dictionary["Names"].Add("Fabricante");
                dictionary["Names"].Add("Modelo");
                dictionary["Names"].Add("Local");
                dictionary["Names"].Add("Pessoa");
                dictionary["Names"].Add("Centro de Custo");
                dictionary["Names"].Add("Saída");
                break;
            default:
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
                break;
        }
        #endregion
        switch (category)
        {
            #region Adaptador AC
            case ConstStrings.AdaptadorAC:
                switch (InternalDatabase.Instance.currentEstoque)
                {
                    case CurrentEstoque.SnPro:
                        dictionary["Names"].Add("Onde funciona?");
                        dictionary["Names"].Add("Voltagem de saída");
                        dictionary["Names"].Add("Amperagem de saída (A)");
                        break;
                    default:
                        dictionary["Names"].Add("Voltagem de saída");
                        dictionary["Names"].Add("Amperagem de saída (A)");
                        break;
                }
                break;
            #endregion
            #region Carregador
            case ConstStrings.Carregador:
                switch (InternalDatabase.Instance.currentEstoque)
                {
                    case CurrentEstoque.SnPro:
                        dictionary["Names"].Add("Onde funciona?");
                        dictionary["Names"].Add("Voltagem de saída");
                        dictionary["Names"].Add("Amperagem de saída (mA)");
                        break;
                    default:
                        dictionary["Names"].Add("Voltagem de saída");
                        dictionary["Names"].Add("Amperagem de saída (mA)");
                        break;
                }
                break;
            #endregion
            #region Desktop
            case ConstStrings.Desktop:
                switch (InternalDatabase.Instance.currentEstoque)
                {
                    case CurrentEstoque.SnPro:
                        dictionary["Names"].Add("Modelo de placa mãe");
                        dictionary["Names"].Add("Fonte?");
                        dictionary["Names"].Add("Memória?");
                        dictionary["Names"].Add("HD?");
                        dictionary["Names"].Add("Placa de vídeo?");
                        dictionary["Names"].Add("Leitor de DVD?");
                        dictionary["Names"].Add("Processador");
                        break;
                    default:
                        dictionary["Names"].Add("HD?");
                        dictionary["Names"].Add("Memória?");
                        dictionary["Names"].Add("Processador?");
                        dictionary["Names"].Add("Qual windows?");
                        break;
                }
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
            #region No break
            case ConstStrings.Nobreak:
                break;
            #endregion
            #region Notebook
            case ConstStrings.Notebook:
                switch (InternalDatabase.Instance.currentEstoque)
                {
                    case CurrentEstoque.SnPro:
                        dictionary["Names"].Add("HD");
                        dictionary["Names"].Add("Memória");
                        dictionary["Names"].Add("Entrada RJ49");
                        dictionary["Names"].Add("Bateria");
                        dictionary["Names"].Add("AdaptadorAC");
                        dictionary["Names"].Add("Windows");
                        break;
                    default:
                        dictionary["Names"].Add("HD?");
                        dictionary["Names"].Add("Memória?");
                        dictionary["Names"].Add("Processador?");
                        dictionary["Names"].Add("Qual windows?");
                        break;
                }
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
                break;
            #endregion
            #region Roteador
            case ConstStrings.Roteador:
                switch (InternalDatabase.Instance.currentEstoque)
                {
                    case CurrentEstoque.SnPro:
                        dictionary["Names"].Add("Wireless?");
                        dictionary["Names"].Add("Quantas entradas?");
                        dictionary["Names"].Add("Banda máx (MB/s)");
                        break;
                    default:
                        dictionary["Names"].Add("Wireless?");
                        dictionary["Names"].Add("Quantas entradas?");
                        break;
                }
                break;
            #endregion
            #region Servidor
            case ConstStrings.Servidor:
                switch (InternalDatabase.Instance.currentEstoque)
                {
                    case CurrentEstoque.SnPro:
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
                    default:
                        dictionary["Names"].Add("HD?");
                        dictionary["Names"].Add("Memória?");
                        dictionary["Names"].Add("Processador?");
                        dictionary["Names"].Add("Qual windows?");
                        break;
                }
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
                switch (InternalDatabase.Instance.currentEstoque)
                {
                    case CurrentEstoque.SnPro:
                        dictionary["Names"].Add("Quantas entradas");
                        dictionary["Names"].Add("Capacidade máx de cada porta (MB/s)");
                        break;
                    default:
                        dictionary["Names"].Add("Quantas e quais portas");
                        break;
                }
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
                //return float.TryParse(parameter1) > float.TryParse(parameter2);
            case "!=":
                return parameter1 != parameter2;
            case "<=":
                //return float.TryParse(parameter1) <= float.TryParse(parameter2);
            case ">=":
               // return float.TryParse(parameter1) >= float.TryParse(parameter2);
            default:
                return false;
        }
    }

    public static Sheet GetCategoryDatabaseToConsult(string category)
    {
        switch (category)
        {
            case ConstStrings.AdaptadorAC:
                return InternalDatabase.adaptadorAC;
            case ConstStrings.Carregador:
                return InternalDatabase.carregador;
            case ConstStrings.Desktop:
                return InternalDatabase.desktop;
            case ConstStrings.FoneRamal:
                return InternalDatabase.foneRamal;
            case ConstStrings.Fonte:
                return InternalDatabase.fonte;
            case ConstStrings.Gbic:
                return InternalDatabase.gbic;
            case ConstStrings.HD:
                return InternalDatabase.hd;
            case ConstStrings.Idrac:
                return InternalDatabase.idrac;
            case ConstStrings.Memoria:
                return InternalDatabase.memoria;
            case ConstStrings.Monitor:
                return InternalDatabase.monitor;
            case ConstStrings.Mouse:
                return InternalDatabase.mouse;
            case ConstStrings.Nobreak:
                return InternalDatabase.nobreak;
            case ConstStrings.Notebook:
                return InternalDatabase.notebook;
            case ConstStrings.PlacaControladora:
                return InternalDatabase.placaControladora;
            case ConstStrings.PlacaDeCapturaDeVideo:
                return InternalDatabase.placaDeCapturaDeVideo;
            case ConstStrings.PlacaDeRede:
                return InternalDatabase.placaDeRede;
            case ConstStrings.PlacaDeSom:
                return InternalDatabase.placaDeSom;
            case ConstStrings.PlacaDeVideo:
                return InternalDatabase.placaDeVideo;
            case ConstStrings.Processador:
                return InternalDatabase.processador;
            case ConstStrings.Ramal:
                return InternalDatabase.ramal;
            case ConstStrings.Roteador:
                return InternalDatabase.roteador;
            case ConstStrings.Servidor:
                return InternalDatabase.servidor;
            case ConstStrings.StorageNAS:
                return InternalDatabase.storageNAS;
            case ConstStrings.Switch:
                return InternalDatabase.Switch;
            case ConstStrings.Teclado:
                return InternalDatabase.teclado;
            case ConstStrings.Outros:
                return InternalDatabase.outros;
            default:
                return InternalDatabase.Instance.fullDatabase;
        }
    }

    public static void RemoveWhiteSpacesFromSingleString(string singleString, out string trimmedString)
    {
        trimmedString = Regex.Replace(singleString, @"\s+", "");
    }

    public static void RemoveWhiteSpacesFromMultipleStrings(List<string> multipleStrings, out List<string> trimmedList)
    {
        string tempString = "";
        trimmedList = new List<string>();
        foreach (string item in multipleStrings)
        {
            tempString = Regex.Replace(item, @"\s+", "");
            trimmedList.Add(tempString);
        }
    }

    public static void LoadDataToAllFullDetailsSHeets(List<Sheet> loadedSheets)
    {
        switch (InternalDatabase.Instance.currentEstoque)
        {
            case CurrentEstoque.SnPro:
                InternalDatabase.adaptadorAC = loadedSheets[0];
                                InternalDatabase.carregador = loadedSheets[1];
                InternalDatabase.desktop = loadedSheets[2];
                InternalDatabase.fonte = loadedSheets[3];
                InternalDatabase.gbic = loadedSheets[4];
                InternalDatabase.hd = loadedSheets[5];
                InternalDatabase.idrac = loadedSheets[6];
                InternalDatabase.memoria = loadedSheets[7];
                InternalDatabase.monitor = loadedSheets[8];
                InternalDatabase.nobreak = loadedSheets[9];
                InternalDatabase.notebook = loadedSheets[10];
                 InternalDatabase.placaControladora = loadedSheets[11];
                InternalDatabase.placaDeCapturaDeVideo = loadedSheets[12];
                InternalDatabase.placaDeRede = loadedSheets[13];
                InternalDatabase.placaDeSom = loadedSheets[14];
                InternalDatabase.placaDeVideo = loadedSheets[15];
                            InternalDatabase.processador = loadedSheets[16];
                InternalDatabase.roteador = loadedSheets[17];
                InternalDatabase.servidor = loadedSheets[18];
                InternalDatabase.storageNAS = loadedSheets[19]; 
                InternalDatabase.Switch = loadedSheets[20];
                InternalDatabase.outros = loadedSheets[21];
                break;           
            default:
                InternalDatabase.adaptadorAC = loadedSheets[0];
                InternalDatabase.carregador = loadedSheets[1];
                InternalDatabase.desktop = loadedSheets[2];
                InternalDatabase.foneRamal = loadedSheets[3];
                InternalDatabase.fonte = loadedSheets[4];
                InternalDatabase.gbic = loadedSheets[5];
                InternalDatabase.hd = loadedSheets[6];
                InternalDatabase.idrac = loadedSheets[7];
                InternalDatabase.memoria = loadedSheets[8];
                InternalDatabase.monitor = loadedSheets[9];
                InternalDatabase.mouse = loadedSheets[10];
                InternalDatabase.nobreak = loadedSheets[11];
                InternalDatabase.notebook = loadedSheets[12];
                InternalDatabase.placaControladora = loadedSheets[13];
                InternalDatabase.placaDeCapturaDeVideo = loadedSheets[14];
                InternalDatabase.placaDeRede = loadedSheets[15];
                InternalDatabase.placaDeSom = loadedSheets[16];
                InternalDatabase.placaDeVideo = loadedSheets[17];
                InternalDatabase.processador = loadedSheets[18];
                InternalDatabase.ramal = loadedSheets[19];
                InternalDatabase.roteador = loadedSheets[20];
                InternalDatabase.servidor = loadedSheets[21];
                InternalDatabase.storageNAS = loadedSheets[22];
                InternalDatabase.Switch = loadedSheets[23];
                InternalDatabase.teclado = loadedSheets[24];
                InternalDatabase.outros = loadedSheets[25];
                break;
        }
    }
}
