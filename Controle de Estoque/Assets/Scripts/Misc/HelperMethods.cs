using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class HelperMethods
{
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
                return ConstStrings.Fonte;
            case 4:
                return ConstStrings.Gbic;
            case 5:
                return ConstStrings.HD;
            case 6:
                return ConstStrings.Idrac;
            case 7:
                return ConstStrings.Memoria;
            case 8:
                return ConstStrings.Monitor;
            case 9:
                return ConstStrings.Notebook;
            case 10:
                return ConstStrings.PlacaControladora;
            case 11:
                return ConstStrings.PlacaDeCapturaDeVideo;
            case 12:
                return ConstStrings.PlacaDeRede;
            case 13:
                return ConstStrings.PlacaDeSom;
            case 14:
                return ConstStrings.PlacaDeVideo;
            case 15:
                return ConstStrings.Processador;
            case 16:
                return ConstStrings.Roteador;
            case 17:
                return ConstStrings.Servidor;
            case 18:
                return ConstStrings.StorageNAS;
            case 19:
                return ConstStrings.Switch;
            default:
                return "Adicionar nova categoria";
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
                    default:
                        break;
                }
                break;
        }


        switch (InternalDatabase.Instance.currentEstoque)
        {
            case CurrentEstoque.SnPro:
                requestToSend = UnityWebRequest.Post(folder + phpName, form);
                break;
            case CurrentEstoque.Funsoft:
                requestToSend = UnityWebRequest.Post(folder + phpName, form);
                break;
            case CurrentEstoque.ESF:
                requestToSend = UnityWebRequest.Post(folder + phpName, form);
                break;
            default:
                requestToSend = UnityWebRequest.Post(folder + phpName, form);
                break;
        }
        return requestToSend;
    }
}
