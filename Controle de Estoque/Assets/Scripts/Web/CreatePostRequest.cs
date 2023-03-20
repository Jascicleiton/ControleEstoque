using UnityEngine.Networking;
using UnityEngine;

/// <summary>
/// Creates all necessary Post Requests
/// </summary>
public class CreatePostRequest 
{
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
                    case CurrentEstoque.Fumsoft:
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
                    case CurrentEstoque.Concert:
                        folder = ConstStrings.PhpRootFolderConcert;
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
                    case CurrentEstoque.Fumsoft:
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
                    case CurrentEstoque.Concert:
                        folder = ConstStrings.PhpImportTablesFolderConcert;
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
                    case CurrentEstoque.Fumsoft:
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
                    case CurrentEstoque.Concert:
                        folder = ConstStrings.PhpAdditemsFolderConcert;
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
                    case CurrentEstoque.Fumsoft:
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
                    case CurrentEstoque.Concert:
                        folder = ConstStrings.PhpMovementsFolderConcert;
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
                    case CurrentEstoque.Fumsoft:
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
                    case CurrentEstoque.Concert:
                        folder = ConstStrings.PhpUpdateItemsFolderConcert;
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
                    case CurrentEstoque.Fumsoft:
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
                    case CurrentEstoque.Concert:
                        folder = ConstStrings.PhpNoPaNoSeItemsFolderConcert;
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
                    case CurrentEstoque.Fumsoft:
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
                    case CurrentEstoque.Concert:
                        folder = ConstStrings.PhpRootFolderConcert;
                        break;
                    default:
                        break;
                }
                break;
        }

        requestToSend = UnityWebRequest.Post(folder + phpName, form);

        return requestToSend;
    }
}
