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
    public const string SceneAddItem = "AddItemScene";
    public const string SceneInitial = "InitialScene";
    public const string SceneMovement = "MovementScene";
    public const string SceneSplash = "SplashScreen";
    public const string SceneUpdateItem = "UpdateItemScene";
    public const string SceneExportTables = "ExportTablesScene";
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
    public const string PhpRecoverBKPFolder = "https://sysnetpro.com.br/controleestoque/RecoverBKP/";
    #endregion

    #region PHP folder URL - Controle de estoque Funsoft
    public const string PhpRootFolderFumsoft = "https://sysnetpro.com.br/Fumsoft/";
    public const string PhpImportTablesFolderFumsoft = "https://sysnetpro.com.br/Fumsoft/Import/";
    public const string PhpAdditemsFolderFumsoft = "https://sysnetpro.com.br/Fumsoft/AddItems/";
    public const string PhpMovementsFolderFumsoft = "https://sysnetpro.com.br/Fumsoft/Movements/";
    public const string PhpUpdateItemsFolderFumsoft = "https://sysnetpro.com.br/Fumsoft/UpdateItems/";
    public const string PhpNoPaNoSeItemsFolderFumsoft = "https://sysnetpro.com.br/Fumsoft/NoPaNoSeItems/";
    public const string PhpRecoverBKPFolderFumsoft = "https://sysnetpro.com.br/Fumsoft/RecoverBKP/";
    #endregion

    #region PHP folder URL - Controle de estoque ESF - Encontre sua franquia
    public const string PhpRootFolderESF = "https://sysnetpro.com.br/ESF/";
    public const string PhpImportTablesFolderESF = "https://sysnetpro.com.br/ESF/Import/";
    public const string PhpAdditemsFolderESF = "https://sysnetpro.com.br/ESF/AddItems/";
    public const string PhpMovementsFolderESF = "https://sysnetpro.com.br/ESF/Movements/";
    public const string PhpUpdateItemsFolderESF = "https://sysnetpro.com.br/ESF/UpdateItems/";
    public const string PhpNoPaNoSeItemsFolderESF = "https://sysnetpro.com.br/ESF/NoPaNoSeItems/";
    public const string PhpRecoverBKPFolderESF = "https://sysnetpro.com.br/ESF/RecoverBKP/";
    #endregion

    #region PHP folder URL - Testing server
    public const string PhpRootFolderTesting = "https://sysnetpro.com.br/Testing/";
    public const string PhpImportTablesFolderTesting = "https://sysnetpro.com.br/Testing/Import/";
    public const string PhpAdditemsFolderTesting = "https://sysnetpro.com.br/Testing/AddItems/";
    public const string PhpMovementsFolderTesting = "https://sysnetpro.com.br/Testing/Movements/";
    public const string PhpUpdateItemsFolderTesting = "https://sysnetpro.com.br/Testing/UpdateItems/";
    public const string PhpNoPaNoSeItemsFolderTesting = "https://sysnetpro.com.br/Testing/NoPaNoSeItems/";
    public const string PhpRecoverBKPFolderTesting = "https://sysnetpro.com.br/Testing/RecoverBKP/";
    #endregion

    #region PHP folder URL - Clientes
    public const string PhpRootFolderClientes = "https://sysnetpro.com.br/Clientes/";
    public const string PhpImportTablesFolderClientes = "https://sysnetpro.com.br/Clientes/Import/";
    public const string PhpAdditemsFolderClientes = "https://sysnetpro.com.br/Clientes/AddItems/";
    public const string PhpMovementsFolderClientes = "https://sysnetpro.com.br/Clientes/Movements/";
    public const string PhpUpdateItemsFolderClientes = "https://sysnetpro.com.br/Clientes/UpdateItems/";
    public const string PhpNoPaNoSeItemsFolderClientes = "https://sysnetpro.com.br/Clientes/NoPaNoSeItems/";
    public const string PhpRecoverBKPFolderClientes = "https://sysnetpro.com.br/Clientes/RecoverBKP/";
    #endregion

    #region PHP folder URL - Concert
    public const string PhpRootFolderConcert = "https://sysnetpro.com.br/Concert/";
    public const string PhpImportTablesFolderConcert = "https://sysnetpro.com.br/Concert/Import/";
    public const string PhpAdditemsFolderConcert = "https://sysnetpro.com.br/Concert/AddItems/";
    public const string PhpMovementsFolderConcert = "https://sysnetpro.com.br/Concert/Movements/";
    public const string PhpUpdateItemsFolderConcert = "https://sysnetpro.com.br/Concert/UpdateItems/";
    public const string PhpNoPaNoSeItemsFolderConcert = "https://sysnetpro.com.br/Concert/NoPaNoSeItems/";
    public const string PhpRecoverBKPFolderConcert = "https://sysnetpro.com.br/Concert/RecoverBKP/";
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
    public const string RecoverBKPKey = "RecoverBackup";
    #endregion

    #region PHPs names
    #region Add Items
    public const string AddNewAdaptadorAC = "adaptadorac.php";
    public const string AddNewCarregador = "carregador.php";
    public const string AddNewDesktop = "desktop.php";
    public const string AddNewFonte = "fonte.php";
    public const string AddNewGbic = "gbic.php";
    public const string AddNewHd = "hd.php";
    public const string AddNewIdrac = "idrac.php";
    public const string AddNewInventario = "inventario.php";
    public const string AddNewMemoria = "memoria.php";
    public const string AddNewMonitor = "monitor.php";
    public const string AddNewNotebook = "tebook.php";
    public const string AddNewPlacaControladora = "placacontroladora.php";
    public const string AddNewPlacaDeCapturaDeVideo = "placadecapturadevideo.php";
    public const string AddNewPlacaDeRede = "placaderede.php";
    public const string AddNewPlacaDeSom = "placadesom.php";
    public const string AddNewPlacaDeVideo = "placadevideo.php";
    public const string AddNewProcessador = "processador.php";
    public const string AddNewRoteador = "roteador.php";
    public const string AddNewServidor = "servidor.php";
    public const string AddNewStorageNas = "storagenas.php";
    public const string AddNewSwitch = "switch.php";
    #endregion
    #region Import
    public const string ImportAdaptadorAC = "importadaptadorac.php";
    public const string ImportAllNoPaNoSeItems = "importallnopanoseitems.php";
    public const string ImportCarregador = "importcarregador.php";
    public const string ImportCategories = "importcategories.php";
    public const string ImportDesktop = "importdesktop.php";
    public const string ImportFonte = "importfonte.php";
    public const string ImportGbic = "importgbic.php";
    public const string ImportHd = "importhd.php";
    public const string ImportIdrac = "importidrac.php";
    public const string ImportInventario = "importinventario.php";
    public const string ImportLocations = "importlocations.php";
    public const string ImportMemoria = "importmemoria.php";
    public const string ImportMonitor = "importmonitor.php";
    public const string ImportNoPaNoSeMovements = "importnopanosemovements.php";
    public const string ImportNotebook = "importnotebook.php";
    public const string ImportPlacaControladora = "importplacacontroladora.php";
    public const string ImportPlacaDeCapturaDeVideo = "importplacadecapturadevideo.php";
    public const string ImportPlacaDeRede = "importplacaderede.php";
    public const string ImportPlacaDeSom = "importplacadesom.php";
    public const string ImportPlacaDeVideo = "importplacadevideo.php";
    public const string ImportProcessador = "importprocessador.php";
    public const string ImportRegularMovements = "importregularmovements.php";
    public const string ImportRoteador = "importroteador.php";
    public const string ImportServidor = "importservidor.php";
    public const string ImportStorageNas = "importstoragenas.php";
    public const string ImportSwitch = "importswitch.php";
    #endregion
    #region Movements
    public const string ConsultPatrimonio = "consultpatrimonio.php";
    public const string ConsultSerial = "consultserial.php";
    public const string GetNoPaNoSeMovements = "getnopanosemovements.php";
    public const string GetPatrimonioMovements = "getpatrimoniomovements.php";
    public const string MoveItem = "moveitem.php";
    #endregion
    #region NoPaNoSe
    public const string AddNoPaNoSe = "addnopanoseitem.php";  
    public const string MoveNoPaNoSe = "movenopanose.php";
    public const string UpdateNoPaNoSe = "updatenopanose.php";
    #endregion
    #region Recover BKP
    public const string RecoverBKPPHP = "recoverinventory.php";
    #endregion
    #region Update Items
    public const string GetItemPatrimonioToUpdate = "getitempatrimoniotoupdate.php";
    public const string GetItemSerialToUpdate = "getitemserialtoupdate.php";
    #endregion
    #region Root
    public const string CheckAccessLevel = "checkaccesslevel.php";
    public const string CheckUserExist = "checkuserexist.php";
    public const string CheckLoginUser = "loginuser.php";
    public const string CheckNewUser = "newuser.php";
    #endregion
    #endregion
}
