using Assets.Scripts.Inventory;
using Assets.Scripts.Inventory.Database;
using Assets.Scripts.Mouse;
using Assets.Scripts.Web;
using System.Collections;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using UnityEngine;
using UnityEngine.Networking;

namespace Assets.Scripts.Misc
{
    public class HelperMethods
    {
        public static bool addUpdateResponse = false;
        public static bool isSingleMessage = false;

        /// <summary>
        /// Get the category "name" based on the value of the dropdown item selected
        /// </summary>
        public static string GetCategoryString(int value)
        {
            if (value < InternalDatabase.categories.Count)
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
                if (category == InternalDatabase.categories[i])
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

                UnityWebRequest createUpdateInventarioRequest = CreatePostRequest.GetPostRequest(itemForm, phpName + ConstStrings.AddNewInventario, folderID);
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
                    case ConstStrings.C_AdaptadorAC:
                        WWWForm adaptadorAcForm = CreateForm.GetAdaptadorACForm(appKey, parameters);

                        UnityWebRequest createAdaptadorACPostRequest = CreatePostRequest.GetPostRequest(adaptadorAcForm, phpName +
                        ConstStrings.AddNewAdaptadorAC, folderID);

                        yield return createAdaptadorACPostRequest.SendWebRequest();

                        addUpdateResponse = HandlePostRequestResponse.HandleWebRequest(createAdaptadorACPostRequest);
                        break;
                    #endregion
                    #region Carregador
                    case ConstStrings.C_Carregador:
                        WWWForm carregadorForm = CreateForm.GetCarregadorForm(appKey, parameters);

                        UnityWebRequest createCarregadorPostRequest = CreatePostRequest.GetPostRequest(carregadorForm, phpName +
                        ConstStrings.AddNewCarregador, folderID);

                        yield return createCarregadorPostRequest.SendWebRequest();

                        addUpdateResponse = HandlePostRequestResponse.HandleWebRequest(createCarregadorPostRequest);
                        break;
                    #endregion
                    #region Desktop
                    case ConstStrings.C_Desktop:
                        WWWForm desktopForm = CreateForm.GetDesktopForm(appKey, parameters);

                        UnityWebRequest createDesktopPostRequest = CreatePostRequest.GetPostRequest(desktopForm, phpName +
                        ConstStrings.AddNewDesktop, folderID);

                        yield return createDesktopPostRequest.SendWebRequest();

                        addUpdateResponse = HandlePostRequestResponse.HandleWebRequest(createDesktopPostRequest);
                        break;
                    #endregion
                    #region Fonte
                    case ConstStrings.C_Fonte:
                        WWWForm fonteForm = CreateForm.GetFonteForm(appKey, parameters[0], parameters[1], parameters[2], parameters[3]);

                        UnityWebRequest createFontePostRequest = CreatePostRequest.GetPostRequest(fonteForm, phpName +
                        ConstStrings.AddNewFonte, folderID);

                        yield return createFontePostRequest.SendWebRequest();

                        addUpdateResponse = HandlePostRequestResponse.HandleWebRequest(createFontePostRequest);
                        break;
                    #endregion
                    #region GBIC
                    case ConstStrings.C_Gbic:
                        WWWForm gbicForm = CreateForm.GetGBICForm(appKey, parameters[0], parameters[1], parameters[2]);

                        UnityWebRequest createGBICPostRequest = CreatePostRequest.GetPostRequest(gbicForm, phpName +
                        ConstStrings.AddNewGbic, folderID);

                        yield return createGBICPostRequest.SendWebRequest();

                        addUpdateResponse = HandlePostRequestResponse.HandleWebRequest(createGBICPostRequest);
                        break;
                    #endregion
                    #region HD
                    case ConstStrings.C_HD:
                        WWWForm hdForm = CreateForm.GetHDForm(appKey, parameters[0], parameters[1], parameters[2], parameters[3],
                        parameters[4], parameters[5], parameters[6], parameters[7], parameters[8]);

                        UnityWebRequest createHDPostRequest = CreatePostRequest.GetPostRequest(hdForm, phpName +
                        ConstStrings.AddNewHd, folderID);

                        yield return createHDPostRequest.SendWebRequest();

                        addUpdateResponse = HandlePostRequestResponse.HandleWebRequest(createHDPostRequest);
                        break;
                    #endregion
                    #region iDrac
                    case ConstStrings.C_Idrac:
                        WWWForm iDracForm = CreateForm.GetiDracForm(appKey, parameters[0], parameters[1], parameters[2], parameters[3],
                       parameters[4], parameters[5]);

                        UnityWebRequest createIdracPostRequest = CreatePostRequest.GetPostRequest(iDracForm, phpName +
                        ConstStrings.AddNewIdrac, folderID);

                        yield return createIdracPostRequest.SendWebRequest();

                        addUpdateResponse = HandlePostRequestResponse.HandleWebRequest(createIdracPostRequest);
                        break;
                    #endregion
                    #region Memoria
                    case ConstStrings.C_Memoria:
                        WWWForm memoriaForm = CreateForm.GetMemoriaForm(appKey, parameters[0], parameters[1], parameters[2], parameters[3],
                        parameters[4], parameters[5], parameters[6], parameters[7], parameters[8], parameters[9]);

                        UnityWebRequest createMemoriaPostRequest = CreatePostRequest.GetPostRequest(memoriaForm, phpName +
                        ConstStrings.AddNewMemoria, folderID);

                        yield return createMemoriaPostRequest.SendWebRequest();

                        addUpdateResponse = HandlePostRequestResponse.HandleWebRequest(createMemoriaPostRequest);

                        break;
                    #endregion
                    #region Monitor
                    case ConstStrings.C_Monitor:
                        WWWForm monitorForm = CreateForm.GetMonitorForm(appKey, parameters);

                        UnityWebRequest createMonitorPostRequest = CreatePostRequest.GetPostRequest(monitorForm, phpName +
                            ConstStrings.AddNewMonitor, folderID);

                        yield return createMonitorPostRequest.SendWebRequest();

                        addUpdateResponse = HandlePostRequestResponse.HandleWebRequest(createMonitorPostRequest);
                        break;
                    #endregion
                    #region Notebook
                    case ConstStrings.C_Notebook:
                        WWWForm notebookForm = CreateForm.GetNotebookForm(appKey, parameters);

                        UnityWebRequest createNotebookPostRequest = CreatePostRequest.GetPostRequest(notebookForm, phpName +
                            ConstStrings.AddNewNotebook, folderID);

                        yield return createNotebookPostRequest.SendWebRequest();

                        addUpdateResponse = HandlePostRequestResponse.HandleWebRequest(createNotebookPostRequest);
                        break;
                    #endregion
                    #region Placa controladora
                    case ConstStrings.C_PlacaControladora:
                        WWWForm placaControladoraForm = CreateForm.GetPlacaControladoraForm(appKey, parameters[0], parameters[1], parameters[2],
                        parameters[3], parameters[4], parameters[5], parameters[6], parameters[7], parameters[8]);

                        UnityWebRequest createPlacaControladoraPostRequest = CreatePostRequest.GetPostRequest(placaControladoraForm, phpName +
                            ConstStrings.AddNewPlacaControladora, folderID);

                        yield return createPlacaControladoraPostRequest.SendWebRequest();

                        addUpdateResponse = HandlePostRequestResponse.HandleWebRequest(createPlacaControladoraPostRequest);
                        break;
                    #endregion
                    #region Placa de captura de vídeo
                    case ConstStrings.C_PlacaDeCapturaDeVideo:
                        WWWForm placaDeCapturaDeVideoForm = CreateForm.GetPlacaDeCapturaDeVideoForm(appKey, parameters[0], parameters[1]);

                        UnityWebRequest createPlacaDeCapturaDeVideoPostRequest = CreatePostRequest.GetPostRequest(placaDeCapturaDeVideoForm,
                        phpName + ConstStrings.AddNewPlacaDeCapturaDeVideo, folderID);

                        yield return createPlacaDeCapturaDeVideoPostRequest.SendWebRequest();

                        addUpdateResponse = HandlePostRequestResponse.HandleWebRequest(createPlacaDeCapturaDeVideoPostRequest);
                        break;
                    #endregion
                    #region Placa de rede
                    case ConstStrings.C_PlacaDeRede:
                        WWWForm placaDeRedeForm = CreateForm.GetPlacaDeRedeForm(appKey, parameters[0], parameters[1], parameters[2], parameters[3],
                        parameters[4], parameters[5], parameters[6]);

                        UnityWebRequest createPlacaDeRedePostRequest = CreatePostRequest.GetPostRequest(placaDeRedeForm, phpName +
                            ConstStrings.AddNewPlacaDeRede, folderID);

                        yield return createPlacaDeRedePostRequest.SendWebRequest();

                        addUpdateResponse = HandlePostRequestResponse.HandleWebRequest(createPlacaDeRedePostRequest);
                        break;
                    #endregion
                    #region Placa de som
                    case ConstStrings.C_PlacaDeSom:
                        WWWForm placaDeSomForm = CreateForm.GetPlacaSomForm(appKey, parameters[0], parameters[1]);

                        UnityWebRequest createPlacaDeSomPostRequest = CreatePostRequest.GetPostRequest(placaDeSomForm, phpName +
                            ConstStrings.AddNewPlacaDeSom, folderID);

                        yield return createPlacaDeSomPostRequest.SendWebRequest();

                        addUpdateResponse = HandlePostRequestResponse.HandleWebRequest(createPlacaDeSomPostRequest);
                        break;
                    #endregion
                    #region Placa de vídeo
                    case ConstStrings.C_PlacaDeVideo:
                        WWWForm placaDeVideoForm = CreateForm.GetPlacaVideoForm(appKey, parameters[0], parameters[1], parameters[2]);

                        UnityWebRequest createPlacaDeVideoPostRequest = CreatePostRequest.GetPostRequest(placaDeVideoForm, phpName +
                            ConstStrings.AddNewPlacaDeVideo, folderID);

                        yield return createPlacaDeVideoPostRequest.SendWebRequest();

                        addUpdateResponse = HandlePostRequestResponse.HandleWebRequest(createPlacaDeVideoPostRequest);
                        break;
                    #endregion
                    #region Placa SAS
                    case ConstStrings.C_PlacaSAS:
                        WWWForm placaSASForm = CreateForm.GetPlacaVideoForm(appKey, parameters[0], parameters[1], parameters[2]);

                        UnityWebRequest createPlacaSASPostRequest = CreatePostRequest.GetPostRequest(placaSASForm, phpName +
                            ConstStrings.AddNewPlacaSAS, folderID);

                        yield return createPlacaSASPostRequest.SendWebRequest();

                        addUpdateResponse = HandlePostRequestResponse.HandleWebRequest(createPlacaSASPostRequest);
                        break;
                    #endregion
                    #region Processador
                    case ConstStrings.C_Processador:
                        WWWForm processadorForm = CreateForm.GetProcessadorForm(appKey, parameters[0], parameters[1], parameters[2], parameters[3]);

                        UnityWebRequest createProcessadorPostRequest = CreatePostRequest.GetPostRequest(processadorForm, phpName +
                            ConstStrings.AddNewProcessador, folderID);

                        yield return createProcessadorPostRequest.SendWebRequest();

                        addUpdateResponse = HandlePostRequestResponse.HandleWebRequest(createProcessadorPostRequest);
                        break;
                    #endregion
                    #region Roteador
                    case ConstStrings.C_Roteador:
                        WWWForm roteadorForm = CreateForm.GetRoteadorForm(appKey, parameters);

                        UnityWebRequest createRoteadorPostRequest = CreatePostRequest.GetPostRequest(roteadorForm, phpName +
                            ConstStrings.AddNewRoteador, folderID);

                        yield return createRoteadorPostRequest.SendWebRequest();

                        addUpdateResponse = HandlePostRequestResponse.HandleWebRequest(createRoteadorPostRequest);
                        break;
                    #endregion
                    #region Servidor
                    case ConstStrings.C_Servidor:

                        WWWForm servidorForm = CreateForm.GetServidorForm(appKey, parameters);

                        UnityWebRequest createServidorPostRequest = CreatePostRequest.GetPostRequest(servidorForm, phpName +
                            ConstStrings.AddNewServidor, folderID);

                        yield return createServidorPostRequest.SendWebRequest();

                        addUpdateResponse = HandlePostRequestResponse.HandleWebRequest(createServidorPostRequest);
                        break;
                    #endregion
                    #region Storage Nas
                    case ConstStrings.C_StorageNAS:
                        WWWForm storageNasForm = CreateForm.GetStorageNASForm(appKey, parameters[0], parameters[1], parameters[2], parameters[3],
                      parameters[4], parameters[5]);

                        UnityWebRequest createStorageNasPostRequest = CreatePostRequest.GetPostRequest(storageNasForm, phpName +
                            ConstStrings.AddNewStorageNas, folderID);

                        yield return createStorageNasPostRequest.SendWebRequest();

                        addUpdateResponse = HandlePostRequestResponse.HandleWebRequest(createStorageNasPostRequest);
                        break;
                    #endregion
                    #region Switch
                    case ConstStrings.C_Switch:
                        WWWForm switchForm = CreateForm.GetSwitchForm(appKey, parameters);

                        UnityWebRequest createSwitchPostRequest = CreatePostRequest.GetPostRequest(switchForm, phpName +
                            ConstStrings.AddNewSwitch, folderID);

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
        public static Dictionary<string, List<string>> GetParameterValuesNamesPlaceholders(ItemColumns itemToShow, string category)
        {
            Dictionary<string, List<string>> dictionary = new Dictionary<string, List<string>>();
            dictionary.Add("Names", new List<string>());
            dictionary.Add("Values", new List<string>());
            dictionary.Add("Placeholders", new List<string>());
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
                    case ConstStrings.C_AdaptadorAC:
                        switch (InternalDatabase.Instance.currentEstoque)
                        {
                            case CurrentEstoque.SnPro:
                            case CurrentEstoque.Testing:
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
                    case ConstStrings.C_Carregador:
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
                    case ConstStrings.C_Desktop:
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
                    case ConstStrings.C_Fonte:
                        dictionary["Values"].Add(itemToShow.Watts.ToString());
                        dictionary["Values"].Add(itemToShow.OndeFunciona);
                        dictionary["Values"].Add(itemToShow.Conectores);
                        break;
                    #endregion
                    #region GBIC
                    case ConstStrings.C_Gbic:
                        dictionary["Values"].Add(itemToShow.Desempenho);
                        break;
                    #endregion
                    #region HD
                    case ConstStrings.C_HD:
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
                    case ConstStrings.C_Idrac:
                        dictionary["Values"].Add(itemToShow.QuaisConexoes);
                        dictionary["Values"].Add(itemToShow.VelocidadeGBs.ToString());
                        dictionary["Values"].Add(itemToShow.EntradaSD);
                        dictionary["Values"].Add(itemToShow.ServidoresSuportados);
                        break;
                    #endregion
                    #region Memoria
                    case ConstStrings.C_Memoria:
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
                    case ConstStrings.C_Monitor:
                        dictionary["Values"].Add(itemToShow.Polegadas.ToString());
                        dictionary["Values"].Add(itemToShow.QuaisConexoes);
                        break;
                    #endregion
                    #region Notebook
                    case ConstStrings.C_Notebook:
                        switch (InternalDatabase.Instance.currentEstoque)
                        {
                            case CurrentEstoque.SnPro:
                            case CurrentEstoque.Testing:
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
                    case ConstStrings.C_PlacaControladora:
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
                    case ConstStrings.C_PlacaDeCapturaDeVideo:
                        dictionary["Values"].Add(itemToShow.QuantidadeDePortas.ToString());
                        break;
                    #endregion
                    #region Placa de rede
                    case ConstStrings.C_PlacaDeRede:
                        dictionary["Values"].Add(itemToShow.Interface);
                        dictionary["Values"].Add(itemToShow.QuantidadeDePortas.ToString());
                        dictionary["Values"].Add(itemToShow.QuaisConexoes);
                        dictionary["Values"].Add(itemToShow.SuportaFibraOptica);
                        dictionary["Values"].Add(itemToShow.Desempenho);
                        break;
                    #endregion
                    #region Placa de som
                    case ConstStrings.C_PlacaDeSom:
                        dictionary["Values"].Add(itemToShow.QuantosCanais.ToString());
                        break;
                    #endregion
                    #region Placa de Video
                    case ConstStrings.C_PlacaDeVideo:
                        dictionary["Values"].Add(itemToShow.QuantidadeDePortas.ToString());
                        dictionary["Values"].Add(itemToShow.QuaisConexoes);
                        break;
                    #endregion
                    #region Placa de Video
                    case ConstStrings.C_PlacaSAS:
                        dictionary["Values"].Add(itemToShow.QuantidadeDePortas.ToString());
                        dictionary["Values"].Add(itemToShow.VelocidadeGBs.ToString());
                        break;
                    #endregion
                    #region Processador
                    case ConstStrings.C_Processador:
                        dictionary["Values"].Add(itemToShow.Soquete);
                        dictionary["Values"].Add(itemToShow.NucleosFisicos.ToString());
                        dictionary["Values"].Add(itemToShow.NucleosLogicos.ToString());
                        break;
                    #endregion
                    #region Roteador
                    case ConstStrings.C_Roteador:
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
                    case ConstStrings.C_Servidor:
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
                    case ConstStrings.C_StorageNAS:
                        dictionary["Values"].Add(itemToShow.Tamanho.ToString());
                        dictionary["Values"].Add(itemToShow.TipoDeRAID);
                        dictionary["Values"].Add(itemToShow.TipoDeHD);
                        dictionary["Values"].Add(itemToShow.CapacidadeMaxHD);
                        dictionary["Values"].Add(itemToShow.AteQuantosHDs.ToString());
                        break;
                    #endregion
                    #region Switch
                    case ConstStrings.C_Switch:
                        switch (InternalDatabase.Instance.currentEstoque)
                        {
                            case CurrentEstoque.SnPro:
                            case CurrentEstoque.Testing:
                                dictionary["Values"].Add(itemToShow.QuaisConexoes);
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
                case ConstStrings.C_AdaptadorAC:
                    switch (InternalDatabase.Instance.currentEstoque)
                    {
                        case CurrentEstoque.SnPro:
                        case CurrentEstoque.Testing:
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
                case ConstStrings.C_Carregador:
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
                case ConstStrings.C_Desktop:
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
                case ConstStrings.C_Fonte:
                    dictionary["Names"].Add("Watts de potência");
                    dictionary["Names"].Add("Onde funciona?");
                    dictionary["Names"].Add("Conectores");
                    break;
                #endregion
                #region GBIC
                case ConstStrings.C_Gbic:
                    dictionary["Names"].Add("Desempenho máx (GB/s)");
                    break;
                #endregion
                #region HD
                case ConstStrings.C_HD:
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
                case ConstStrings.C_Idrac:
                    dictionary["Names"].Add("Porta");
                    dictionary["Names"].Add("Velocidade (GB/s)");
                    dictionary["Names"].Add("Entrada SD");
                    dictionary["Names"].Add("Servidores suportados");
                    break;
                #endregion
                #region Memoria
                case ConstStrings.C_Memoria:
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
                case ConstStrings.C_Monitor:
                    dictionary["Names"].Add("Polegadas");
                    dictionary["Names"].Add("Quais entradas?");
                    break;
                #endregion
                #region No break
                case ConstStrings.C_Nobreak:
                    break;
                #endregion
                #region Notebook
                case ConstStrings.C_Notebook:
                    switch (InternalDatabase.Instance.currentEstoque)
                    {
                        case CurrentEstoque.SnPro:
                        case CurrentEstoque.Testing:
                            dictionary["Names"].Add("HD");
                            dictionary["Names"].Add("Memória");
                            dictionary["Names"].Add("Entrada RJ45");
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
                case ConstStrings.C_PlacaControladora:
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
                case ConstStrings.C_PlacaDeCapturaDeVideo:
                    dictionary["Names"].Add("Quantas entradas?");
                    break;
                #endregion
                #region Placa de rede
                case ConstStrings.C_PlacaDeRede:
                    dictionary["Names"].Add("Interface");
                    dictionary["Names"].Add("Quantas portas?");
                    dictionary["Names"].Add("Quais portas?");
                    dictionary["Names"].Add("Suporta fibra óptica?");
                    dictionary["Names"].Add("Desempenho (MB/s)");
                    break;
                #endregion
                #region Placa de som
                case ConstStrings.C_PlacaDeSom:
                    dictionary["Names"].Add("Quantos canais?");
                    break;
                #endregion
                #region Placa de Video
                case ConstStrings.C_PlacaDeVideo:
                    dictionary["Names"].Add("Quantas entradas?");
                    dictionary["Names"].Add("Quais entradas?");
                    break;
                #endregion
                #region Placa SAS
                case ConstStrings.C_PlacaSAS:
                    dictionary["Names"].Add("Quantas entradas?");
                    dictionary["Names"].Add("Velocidade de transmissão");
                    break;
                #endregion
                #region Processador
                case ConstStrings.C_Processador:
                    dictionary["Names"].Add("Soquete");
                    dictionary["Names"].Add("Nº núcleos físicos");
                    dictionary["Names"].Add("Nº núcleos lógicos");
                    break;
                #endregion
                #region Roteador
                case ConstStrings.C_Roteador:
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
                case ConstStrings.C_Servidor:
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
                case ConstStrings.C_StorageNAS:
                    dictionary["Names"].Add("Tamanho dos HDs");
                    dictionary["Names"].Add("Tipos de RAID");
                    dictionary["Names"].Add("Tipo de HD");
                    dictionary["Names"].Add("Capacidade máx do HD");
                    dictionary["Names"].Add("Até quantos HDs");
                    break;
                #endregion
                #region Switch
                case ConstStrings.C_Switch:
                    switch (InternalDatabase.Instance.currentEstoque)
                    {
                        case CurrentEstoque.SnPro:
                        case CurrentEstoque.Testing:
                            dictionary["Names"].Add("Quantas e quais portas");
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
            #region Placeholders
            #region Inventário
            switch (InternalDatabase.Instance.currentEstoque)
            {
                case CurrentEstoque.Concert:
                    dictionary["Placeholders"].Add("Data de aquisição");
                    dictionary["Placeholders"].Add("Data de entrada no estoque");
                    dictionary["Placeholders"].Add("Número do patrimônio");
                    dictionary["Placeholders"].Add("Funciona, DEFEITO");
                    dictionary["Placeholders"].Add("Serial");
                    dictionary["Placeholders"].Add("Categoria");
                    dictionary["Placeholders"].Add("Fabricante");
                    dictionary["Placeholders"].Add("Modelo");
                    dictionary["Placeholders"].Add("Estoque, Descarte, SNP02, etc");
                    dictionary["Placeholders"].Add("Pessoa alocada ao item");
                    dictionary["Placeholders"].Add("Centro de Custo");
                    dictionary["Placeholders"].Add("Data de saída do estoque");
                    break;
                case CurrentEstoque.ESF:
                    dictionary["Placeholders"].Add("Data de aquisição");
                    dictionary["Placeholders"].Add("Data de entrada no estoque");
                    dictionary["Placeholders"].Add("Número do patrimônio");
                    dictionary["Placeholders"].Add("Funciona, DEFEITO, Não testado...");
                    dictionary["Placeholders"].Add("Serial");
                    dictionary["Placeholders"].Add("Desktop, Servidor, Roteador....");
                    dictionary["Placeholders"].Add("Fabricante");
                    dictionary["Placeholders"].Add("Modelo");
                    dictionary["Placeholders"].Add("Estoque, Em uso, Estoque de itens defeituosos, outros");
                    dictionary["Placeholders"].Add("Saída do estoque");
                    dictionary["Placeholders"].Add("Informações adicionais");
                    break;
                default:
                    dictionary["Placeholders"].Add("Data de aquisição");
                    dictionary["Placeholders"].Add("Data de entrada no estoque");
                    dictionary["Placeholders"].Add("Número do patrimônio");
                    dictionary["Placeholders"].Add("Funciona, DEFEITO...");
                    dictionary["Placeholders"].Add("Serial");
                    dictionary["Placeholders"].Add("Desktop, Servidor, HD....");
                    dictionary["Placeholders"].Add("Fabricante");
                    dictionary["Placeholders"].Add("Modelo");
                    dictionary["Placeholders"].Add("Estoque, Descarte...");
                    dictionary["Placeholders"].Add("Data de saída do estoque");
                    dictionary["Placeholders"].Add("Informações adicionais");
                    break;
            }
            #endregion
            #region Categorias
            switch (category)
            {
                #region Adaptador AC
                case ConstStrings.C_AdaptadorAC:
                    switch (InternalDatabase.Instance.currentEstoque)
                    {
                        case CurrentEstoque.SnPro:
                        case CurrentEstoque.Testing:
                            dictionary["Placeholders"].Add("Patrimônio(s) compatível(is)...");
                            dictionary["Placeholders"].Add("Voltagem de saída...");
                            dictionary["Placeholders"].Add("Amperagem de saída (A)...");
                            break;
                        default:
                            dictionary["Placeholders"].Add("Voltagem de saída...");
                            dictionary["Placeholders"].Add("Amperagem de saída (A)...");
                            break;
                    }
                    break;
                #endregion
                #region Carregador
                case ConstStrings.C_Carregador:
                    switch (InternalDatabase.Instance.currentEstoque)
                    {
                        case CurrentEstoque.SnPro:
                            dictionary["Placeholders"].Add("Patrimônio(s) compatível(is)...");
                            dictionary["Placeholders"].Add("Voltagem de saída...");
                            dictionary["Placeholders"].Add("Amperagem de saída (mA)...");
                            break;
                        default:
                            dictionary["Placeholders"].Add("Voltagem de saída...");
                            dictionary["Placeholders"].Add("Amperagem de saída (mA)...");
                            break;
                    }
                    break;
                #endregion
                #region Desktop
                case ConstStrings.C_Desktop:
                    switch (InternalDatabase.Instance.currentEstoque)
                    {
                        case CurrentEstoque.SnPro:
                            dictionary["Placeholders"].Add("Modelo de placa mãe...");
                            dictionary["Placeholders"].Add("Nº do patrimônio da fonte...");
                            dictionary["Placeholders"].Add("Nº do(s) patrimônio(s) da(s) memória(s)...");
                            dictionary["Placeholders"].Add("Nº do(s) patrimônio(s) do(s) HD(s)...");
                            dictionary["Placeholders"].Add("Nº do patrimônio da placa de vídeo...");
                            dictionary["Placeholders"].Add("Possui leitor de DVD?...");
                            dictionary["Placeholders"].Add("Modelo ou patrimônio do processador...");
                            break;
                        default:
                            dictionary["Placeholders"].Add("Nº do(s) patrimônio(s) do(s) HD(s)...");
                            dictionary["Placeholders"].Add("Nº do(s) patrimônio(s) da(s) memória(s)...");
                            dictionary["Placeholders"].Add("Modelo do processador...");
                            dictionary["Placeholders"].Add("Qual windows instalado?...");
                            break;
                    }
                    break;
                #endregion
                #region Fonte
                case ConstStrings.C_Fonte:
                    dictionary["Placeholders"].Add("Watts de potência...");
                    dictionary["Placeholders"].Add("Patrimônio(s) compatível(is)...");
                    dictionary["Placeholders"].Add("Quantos e quais conectores possui...");
                    break;
                #endregion
                #region GBIC
                case ConstStrings.C_Gbic:
                    dictionary["Placeholders"].Add("Desempenho máx (GB/s)...");
                    break;
                #endregion
                #region HD
                case ConstStrings.C_HD:
                    dictionary["Placeholders"].Add("SATA, SAS...");
                    dictionary["Placeholders"].Add("3,5\" ou 2,5\"...");
                    dictionary["Placeholders"].Add("SSD, HDD ou HDSSD...");
                    dictionary["Placeholders"].Add("Capacidade em GB...");
                    dictionary["Placeholders"].Add("RPM, SSD é 0...");
                    dictionary["Placeholders"].Add("3, 6....");
                    dictionary["Placeholders"].Add("É enterprise?...");
                    break;
                #endregion
                #region iDrac
                case ConstStrings.C_Idrac:
                    dictionary["Placeholders"].Add("Qual tipo de porta possui?...");
                    dictionary["Placeholders"].Add("Velocidade em GB/s...");
                    dictionary["Placeholders"].Add("Possui entrada SD?");
                    dictionary["Placeholders"].Add("Modelos de servidores suportados...");
                    break;
                #endregion
                #region Memoria
                case ConstStrings.C_Memoria:
                    dictionary["Placeholders"].Add("DDR2, DDR3...");
                    dictionary["Placeholders"].Add("Capacidade em GB...");
                    dictionary["Placeholders"].Add("Velocidade em MHz...");
                    dictionary["Placeholders"].Add("É low voltage? (< 1.5V)...");
                    dictionary["Placeholders"].Add("1Rx4, 2Rx8...");
                    dictionary["Placeholders"].Add("UDIMM, SODIMM, RDIMM...");
                    dictionary["Placeholders"].Add("Taxa de transmissão...");
                    dictionary["Placeholders"].Add("Letra no final da taxa de transmissão...");
                    break;
                #endregion
                #region Monitor
                case ConstStrings.C_Monitor:
                    dictionary["Placeholders"].Add("Polegadas...");
                    dictionary["Placeholders"].Add("Quais entradas?...");
                    break;
                #endregion
                #region No break
                case ConstStrings.C_Nobreak:
                    break;
                #endregion
                #region Notebook
                case ConstStrings.C_Notebook:
                    switch (InternalDatabase.Instance.currentEstoque)
                    {
                        case CurrentEstoque.SnPro:
                        case CurrentEstoque.Testing:
                            dictionary["Placeholders"].Add("Quantidade e capacidade do(s) HD(s)...");
                            dictionary["Placeholders"].Add("Quatidade e capacidade da(s) memória(s) instalada(s)...");
                            dictionary["Placeholders"].Add("Possui entrada RJ45?...");
                            dictionary["Placeholders"].Add("Estado da bateria...");
                            dictionary["Placeholders"].Add("Nº do patrimônio da fonte de alimentação...");
                            dictionary["Placeholders"].Add("Qual windows instalado?...");
                            break;
                        default:
                            dictionary["Placeholders"].Add("Quantidade e capacidade do(s) HD(s)...");
                            dictionary["Placeholders"].Add("Quatidade e capacidade da(s) memória(s) instalada(s)...");
                            dictionary["Placeholders"].Add("Modelo do processador...");
                            dictionary["Placeholders"].Add("Qual windows instalado?...");
                            break;
                    }
                    break;
                #endregion
                #region Placa controladora
                case ConstStrings.C_PlacaControladora:
                    dictionary["Placeholders"].Add("Tipo de conexão...");
                    dictionary["Placeholders"].Add("Quantas portas?...");
                    dictionary["Placeholders"].Add("Tipos de RAID...");
                    dictionary["Placeholders"].Add("Capacidade máx do HD em TB...");
                    dictionary["Placeholders"].Add("Suporta até quantos HDs...");
                    dictionary["Placeholders"].Add("Bateria inclusa?...");
                    dictionary["Placeholders"].Add("Tipo de barramento...");
                    break;
                #endregion
                #region Placa de captura de video
                case ConstStrings.C_PlacaDeCapturaDeVideo:
                    dictionary["Placeholders"].Add("Quantas entradas?...");
                    break;
                #endregion
                #region Placa de rede
                case ConstStrings.C_PlacaDeRede:
                    dictionary["Placeholders"].Add("Tipo de barramento...");
                    dictionary["Placeholders"].Add("Quantas portas?...");
                    dictionary["Placeholders"].Add("Quais portas?...");
                    dictionary["Placeholders"].Add("Suporta fibra óptica?...");
                    dictionary["Placeholders"].Add("Desempenho (MB/s)...");
                    break;
                #endregion
                #region Placa de som
                case ConstStrings.C_PlacaDeSom:
                    dictionary["Placeholders"].Add("Quantos canais?...");
                    break;
                #endregion
                #region Placa de Video
                case ConstStrings.C_PlacaDeVideo:
                    dictionary["Placeholders"].Add("Quantas entradas?...");
                    dictionary["Placeholders"].Add("Quais entradas?...");
                    break;
                #endregion
                #region Placa de Video
                case ConstStrings.C_PlacaSAS:
                    dictionary["Placeholders"].Add("Quantas entradas?");
                    dictionary["Placeholders"].Add("Velocidade de transmissão");
                    break;
                #endregion
                #region Processador
                case ConstStrings.C_Processador:
                    dictionary["Placeholders"].Add("Soquete...");
                    dictionary["Placeholders"].Add("Nº núcleos físicos...");
                    dictionary["Placeholders"].Add("Nº núcleos lógicos...");
                    break;
                #endregion
                #region Roteador
                case ConstStrings.C_Roteador:
                    switch (InternalDatabase.Instance.currentEstoque)
                    {
                        case CurrentEstoque.SnPro:
                            dictionary["Placeholders"].Add("Tem wireless?...");
                            dictionary["Placeholders"].Add("Quantas entradas?...");
                            dictionary["Placeholders"].Add("Banda máx em MB/s...");
                            break;
                        default:
                            dictionary["Placeholders"].Add("Tem wireless?...");
                            dictionary["Placeholders"].Add("Quantas entradas?...");
                            break;
                    }
                    break;
                #endregion
                #region Servidor
                case ConstStrings.C_Servidor:
                    switch (InternalDatabase.Instance.currentEstoque)
                    {
                        case CurrentEstoque.SnPro:
                            dictionary["Placeholders"].Add("Modelo da placa mãe...");
                            dictionary["Placeholders"].Add("nº do(s) patrimônio(s) da(s) fonte(s)...");
                            dictionary["Placeholders"].Add("nº do patrimônio das memórias instaladas...");
                            dictionary["Placeholders"].Add("nº do patrimônio do(s) HD(s) instalado(s)...");
                            dictionary["Placeholders"].Add("nº do patrimônio da placa de vídeo...");
                            dictionary["Placeholders"].Add("nº do(s) patrimônio(s) da(s) placa(s) de rede...");
                            dictionary["Placeholders"].Add("modelos ou patrimônios dos processadores instalados...");
                            dictionary["Placeholders"].Add("Configuração das memórias suportadas...");
                            dictionary["Placeholders"].Add("Até quantas memórias podem ser usadas...");
                            dictionary["Placeholders"].Add("Ordem de uso das memórias nos DIMMs...");
                            dictionary["Placeholders"].Add("Capacidade RAM máxima suportada...");
                            dictionary["Placeholders"].Add("Soquete do processador...");
                            dictionary["Placeholders"].Add("Patrimônio da placa controladora...");
                            dictionary["Placeholders"].Add("Suporta até quantos HDs...");
                            dictionary["Placeholders"].Add("SATA, SAS...");
                            dictionary["Placeholders"].Add("Tipos de RAID suportadas...");
                            break;
                        default:
                            dictionary["Placeholders"].Add("Patrimônio(s) do(s) HD(s) instalado(s)...");
                            dictionary["Placeholders"].Add("Patrimônio(s) da(s) memória(s) instalada(s)...");
                            dictionary["Placeholders"].Add("Modelo do(s) processador(es) instalado(s)...");
                            dictionary["Placeholders"].Add("Qual sistema operacional instalado...");
                            break;
                    }
                    break;
                #endregion
                #region Storage NAS
                case ConstStrings.C_StorageNAS:
                    dictionary["Placeholders"].Add("3,5\" e/ou 2,5\"...");
                    dictionary["Placeholders"].Add("Tipos de RAID suportadas...");
                    dictionary["Placeholders"].Add("SATA ou SAS...");
                    dictionary["Placeholders"].Add("Capacidade máxima suportada do HD...");
                    dictionary["Placeholders"].Add("Suporta até quantos HDs...");
                    break;
                #endregion
                #region Switch
                case ConstStrings.C_Switch:
                    switch (InternalDatabase.Instance.currentEstoque)
                    {
                        case CurrentEstoque.SnPro:
                        case CurrentEstoque.Testing:
                            dictionary["Placeholders"].Add("Quantas e quais portas possui...");
                            dictionary["Placeholders"].Add("Capacidade máx de cada porta em MB/s...");
                            break;
                        default:
                            dictionary["Placeholders"].Add("Quantas e quais portas possui...");
                            break;
                    }
                    break;
                #endregion
                default:
                    break;
            }
            #endregion
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
                if (location == InternalDatabase.locations[i])
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
            string parameterToCheck1 = "";
            string parameterToCheck2 = "";
            if (parameter1 != null)
            {
                parameterToCheck1 = parameter1.ToLower();
            }
            if (parameter2 != null)
            {
                parameterToCheck2 = parameter2.ToLower();
            }
            else
            {
                parameter2 = "";
            }

            switch (Operator)
            {
                case "=":
                    return string.Equals(parameterToCheck1, parameterToCheck2);
                case "<":
                //   return double.Parse(parameter1) < double.Parse(parameter2);
                case ">":
                //return float.TryParse(parameter1) > float.TryParse(parameter2);
                case "≠":
                    return !string.Equals(parameterToCheck1, parameterToCheck2);
                case "<=":
                //return float.TryParse(parameter1) <= float.TryParse(parameter2);
                case ">=":
                // return float.TryParse(parameter1) >= float.TryParse(parameter2);
                default:
                    return false;
            }
        }
        /// <summary>
        /// Get the category Sheet based on it's name (string value)
        /// </summary>
        public static Sheet GetCategoryDatabaseToConsult(string category)
        {
            switch (category)
            {
                case ConstStrings.C_AdaptadorAC:
                    return InternalDatabase.adaptadorAC;
                case ConstStrings.C_Carregador:
                    return InternalDatabase.carregador;
                case ConstStrings.C_Desktop:
                    return InternalDatabase.desktop;
                case ConstStrings.C_FoneRamal:
                    return InternalDatabase.foneRamal;
                case ConstStrings.C_Fonte:
                    return InternalDatabase.fonte;
                case ConstStrings.C_Gbic:
                    return InternalDatabase.gbic;
                case ConstStrings.C_HD:
                    return InternalDatabase.hd;
                case ConstStrings.C_Idrac:
                    return InternalDatabase.idrac;
                case ConstStrings.C_Memoria:
                    return InternalDatabase.memoria;
                case ConstStrings.C_Monitor:
                    return InternalDatabase.monitor;
                case ConstStrings.C_Mouse:
                    return InternalDatabase.mouse;
                case ConstStrings.C_Nobreak:
                    return InternalDatabase.nobreak;
                case ConstStrings.C_Notebook:
                    return InternalDatabase.notebook;
                case ConstStrings.C_PlacaControladora:
                    return InternalDatabase.placaControladora;
                case ConstStrings.C_PlacaDeCapturaDeVideo:
                    return InternalDatabase.placaDeCapturaDeVideo;
                case ConstStrings.C_PlacaDeRede:
                    return InternalDatabase.placaDeRede;
                case ConstStrings.C_PlacaDeSom:
                    return InternalDatabase.placaDeSom;
                case ConstStrings.C_PlacaDeVideo:
                    return InternalDatabase.placaDeVideo;
                case ConstStrings.C_PlacaSAS:
                    return InternalDatabase.placaSAS;
                case ConstStrings.C_Processador:
                    return InternalDatabase.processador;
                case ConstStrings.C_Ramal:
                    return InternalDatabase.ramal;
                case ConstStrings.C_Roteador:
                    return InternalDatabase.roteador;
                case ConstStrings.C_Servidor:
                    return InternalDatabase.servidor;
                case ConstStrings.C_StorageNAS:
                    return InternalDatabase.storageNAS;
                case ConstStrings.C_Switch:
                    return InternalDatabase.Switch;
                case ConstStrings.C_Teclado:
                    return InternalDatabase.teclado;
                case ConstStrings.C_Outros:
                    return InternalDatabase.outros;
                default:
                    return InternalDatabase.Instance.fullDatabase;
            }
        }

        /// <summary>
        /// Removes all the white spaces from a single string
        /// </summary>
        public static void RemoveWhiteSpacesFromSingleString(string singleString, out string trimmedString)
        {
            trimmedString = Regex.Replace(singleString, @"\s+", "");
        }

        /// <summary>
        /// Removes all the white spaces from multiple strings
        /// </summary>
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
    }
}