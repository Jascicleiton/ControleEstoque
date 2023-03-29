using System.Collections.Generic;

public class ConstStrings
{
    #region Save files
    public const string DataDatabaseSaveFile = "InventoryDatabase";
    public const string UserDatabaseSaveFile = "UserDatabase";
    public const string extension = ".json";
    #endregion

    #region Sheets names
    public const string Inventario = "Inventário";
    public const string HD = "HD";
    public const string Memoria = "Memória";
    public const string PlacaDeRede = "Placa de rede";
    public const string Idrac = "iDrac";
    public const string PlacaControladora = "Placa controladora";
    public const string Processador = "Processador";
    public const string Desktop = "Desktop";
    public const string Fonte = "Fonte";
    public const string Switch = "Switch";
    public const string Roteador = "Roteador";
    public const string Carregador = "Carregador";
    public const string AdaptadorAC = "Adaptador AC";
    public const string StorageNAS = "Storage NAS";
    public const string Gbic = "Gbic";
    public const string PlacaDeVideo = "Placa de vídeo";
    public const string PlacaDeSom = "Placa de som";
    public const string PlacaDeCapturaDeVideo = "Placa de captura de vídeo";
    public const string Servidor = "Servidor";
    public const string Notebook = "Notebook";
    public const string Monitor = "Monitor";
    public const string Movimentacao = "Movimentação";
    public const string Mouse = "Mouse";
    public const string Teclado = "Teclado";
    public const string FoneRamal = "Fone para ramal";
    public const string Ramal = "Ramal";
    public const string Nobreak = "No break";
    public const string Outros = "Outros";
    #endregion

    #region Categories arrays
    public static readonly string[] AllCategories = { AdaptadorAC, Carregador, Desktop, FoneRamal, Fonte, Gbic, HD, Idrac, Memoria, Monitor, 
    Mouse, Nobreak, Notebook, PlacaControladora, PlacaDeCapturaDeVideo, PlacaDeRede, PlacaDeSom, PlacaDeVideo, Processador, Ramal,Roteador,
    Servidor, StorageNAS, Switch, Teclado, Outros };
    public static readonly string[] SNPCategories = { AdaptadorAC, Carregador, Desktop, Fonte, Gbic, HD, Idrac, Memoria, Monitor, Nobreak, 
    Notebook, PlacaControladora, PlacaDeCapturaDeVideo, PlacaDeRede, PlacaDeSom, PlacaDeVideo, Processador, Roteador, Servidor, StorageNAS, 
    Switch, Outros };
    public static readonly string[] ConcertCategories = { AdaptadorAC, Desktop, Monitor, Notebook, Roteador, Servidor, Switch, Outros };
    #endregion

    #region Tags
    public const string ItemResultsParent = "ItemResultsParent";
    public const string TabTarget = "TabTarget";
    #endregion

    #region Scene names
    public const string SceneMainMenu = "MainMenu";
    public const string SceneConsult = "ConsultScene";
    public const string SceneAddRemoveItem = "AddRemoveItemScene";
    public const string SceneInitial = "InitialScene";
    public const string SceneMovement = "MovementScene";
    public const string SceneSplash = "SplashScreen";
    public const string SceneUpdateItem = "UpdateItemScene";
    public const string SceneExportSheets = "ExportCSVsScene";
    public const string SceneConsultInventoryAll = "ConsultSceneAllinventory";
    public const string SceneConsultDetailsAll = "ConsultSceneAllDetails";
    public const string SceneNoPaNoSe = "NoPaNoSeScene";
    public const string SceneShowAllMovements = "ShowAllMovementsScene";
    #endregion

    #region PHP folder URL - Controle de estoque SNPro
    public const string PhpRootFolder = "https://sysnetpro.com.br/controleestoque/";
    public const string PhpImportTablesFolder = "https://sysnetpro.com.br/controleestoque/Import/";
    public const string PhpAdditemsFolder = "https://sysnetpro.com.br/controleestoque/AddItems/";
    public const string PhpMovementsFolder = "https://sysnetpro.com.br/controleestoque/Movements/";
    public const string PhpUpdateItemsFolder = "https://sysnetpro.com.br/controleestoque/UpdateItems/";
    public const string PhpNoPaNoSeItemsFolder = "https://sysnetpro.com.br/controleestoque/NoPaNoSeItems/";
    #endregion

    #region PHP folder URL - Controle de estoque Funsoft
    public const string PhpRootFolderFunsoft = "https://sysnetpro.com.br/Funsoft/";
    public const string PhpImportTablesFolderFunsoft = "https://sysnetpro.com.br/Funsoft/Import/";
    public const string PhpAdditemsFolderFunsoft = "https://sysnetpro.com.br/Funsoft/AddItems/";
    public const string PhpMovementsFolderFunsoft = "https://sysnetpro.com.br/Funsoft/Movements/";
    public const string PhpUpdateItemsFolderFunsoft = "https://sysnetpro.com.br/Funsoft/UpdateItems/";
    public const string PhpNoPaNoSeItemsFolderFunsoft = "https://sysnetpro.com.br/Funsoft/NoPaNoSeItems/";
    #endregion

    #region PHP folder URL - Controle de estoque ESF - Encontre sua franquia
    public const string PhpRootFolderESF = "https://sysnetpro.com.br/ESF/";
    public const string PhpImportTablesFolderESF = "https://sysnetpro.com.br/ESF/Import/";
    public const string PhpAdditemsFolderESF = "https://sysnetpro.com.br/ESF/AddItems/";
    public const string PhpMovementsFolderESF = "https://sysnetpro.com.br/ESF/Movements/";
    public const string PhpUpdateItemsFolderESF = "https://sysnetpro.com.br/ESF/UpdateItems/";
    public const string PhpNoPaNoSeItemsFolderESF = "https://sysnetpro.com.br/ESF/NoPaNoSeItems/";
    #endregion

    #region PHP folder URL - Testing server
    public const string PhpRootFolderTesting = "https://sysnetpro.com.br/Testing/";
    public const string PhpImportTablesFolderTesting = "https://sysnetpro.com.br/Testing/Import/";
    public const string PhpAdditemsFolderTesting = "https://sysnetpro.com.br/Testing/AddItems/";
    public const string PhpMovementsFolderTesting = "https://sysnetpro.com.br/Testing/Movements/";
    public const string PhpUpdateItemsFolderTesting = "https://sysnetpro.com.br/Testing/UpdateItems/";
    public const string PhpNoPaNoSeItemsFolderTesting = "https://sysnetpro.com.br/Testing/NoPaNoSeItems/";
    #endregion

    #region PHP folder URL - Clientes
    public const string PhpRootFolderClientes = "https://sysnetpro.com.br/Clientes/";
    public const string PhpImportTablesFolderClientes = "https://sysnetpro.com.br/Clientes/Import/";
    public const string PhpAdditemsFolderClientes = "https://sysnetpro.com.br/Clientes/AddItems/";
    public const string PhpMovementsFolderClientes = "https://sysnetpro.com.br/Clientes/Movements/";
    public const string PhpUpdateItemsFolderClientes = "https://sysnetpro.com.br/Clientes/UpdateItems/";
    public const string PhpNoPaNoSeItemsFolderClientes = "https://sysnetpro.com.br/Clientes/NoPaNoSeItems/";
    #endregion

    #region PHP folder URL - Concert
    public const string PhpRootFolderConcert = "https://sysnetpro.com.br/Concert/";
    public const string PhpImportTablesFolderConcert = "https://sysnetpro.com.br/Concert/Import/";
    public const string PhpAdditemsFolderConcert = "https://sysnetpro.com.br/Concert/AddItems/";
    public const string PhpMovementsFolderConcert = "https://sysnetpro.com.br/Concert/Movements/";
    public const string PhpUpdateItemsFolderConcert = "https://sysnetpro.com.br/Concert/UpdateItems/";
    public const string PhpNoPaNoSeItemsFolderConcert = "https://sysnetpro.com.br/Concert/NoPaNoSeItems/";
    #endregion


    #region Save parameters
    public const string showWarningMessage = "ShowWarningMessage";
    #endregion

    #region AppKeys
    public const string ConsultKey = "ConsultDatabase";
    public const string InsertUserKey = "InsertNewUser";
    public const string LoginKey = "LoginUser";
    public const string CheckUserExistKey = "CheckIfUserExist";
    public const string ImportDatabaseKey = "ImportDatabase";
    public const string AddNewItemKey = "AddNewItem";
    public const string MoveItemKey = "MoveItem";
    public const string ExportDatabaseKey = "ExportDatabase";
    public const string UpdateItemKey = "UpdateItem";

    #endregion

}
