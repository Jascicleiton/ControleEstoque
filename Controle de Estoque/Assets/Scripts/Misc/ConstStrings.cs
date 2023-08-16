using System.Collections.Generic;
using UnityEngine.UIElements;

public class ConstStrings
{
    #region Save files
    public const string DataDatabaseSaveFile = "InventoryDatabase";
    public const string UserDatabaseSaveFile = "UserDatabase";
    public const string extension = ".json";
    public const string SavePath = "D:/Controle de estoque/ControleEstoque/Saves/";
    #endregion

    #region ParameterNames
    public const string Aquisicao = nameof(Aquisicao);
    public const string Entrada = nameof(Entrada);
    public const string Patrimonio_I = nameof(Patrimonio_I);
    public const string Status = nameof(Status);
    public const string Serial = nameof(Serial);
    public const string Categoria = nameof(Categoria);
    public const string Fabricante = nameof(Fabricante);
    public const string Modelo = nameof(Modelo);
    public const string Local = nameof(Local);
    public const string Saida = nameof(Saida);
    public const string Observacao = nameof(Observacao);
    public const string ModeloPlacaMae = nameof(ModeloPlacaMae);
    public const string Processador = nameof(Processador);
    public const string Fonte = nameof(Fonte);
    public const string Hd = nameof(Hd);
    public const string Memoria = nameof(Memoria);
    public const string PlacaDeRede = nameof(PlacaDeRede);
    public const string PlacaDeVideo = nameof(PlacaDeVideo);
    public const string LeitorDeDvd = nameof(LeitorDeDvd);
    public const string Porta = nameof(Porta);
    public const string Velocidade_I = nameof(Velocidade_I);
    public const string EntradaSD = nameof(EntradaSD);
    public const string ServidoresSuportados = nameof(ServidoresSuportados);
    public const string Tipo = nameof(Tipo);
    public const string Capacidade_I = nameof(Capacidade_I);
    public const string LowVoltage = nameof(LowVoltage);
    public const string Rank = nameof(Rank);
    public const string DIMM = nameof(DIMM);
    public const string TaxaDeTransmissao_I = nameof(TaxaDeTransmissao_I);
    public const string Simbolo = nameof(Simbolo);
    public const string OndeFunciona = nameof(OndeFunciona);
    public const string Voltagem_D = nameof(Voltagem_D);
    public const string Amperagem_D = nameof(Amperagem_D);
    public const string Interface = nameof(Interface);
    public const string Tamanho_D = nameof(Tamanho_D);
    public const string FormaDeArmazenamento = nameof(FormaDeArmazenamento);
    public const string RPM_I = nameof(RPM_I);
    public const string VelocidadeDeLeitura_I = nameof(VelocidadeDeLeitura_I);
    public const string Enterprise = nameof(Enterprise);
    public const string DesempenhoMax_I = nameof(DesempenhoMax_I);
    public const string Watts_I = nameof(Watts_I);
    public const string Conectores = nameof(Conectores);
    public const string Polegadas = nameof(Polegadas);
    public const string QuaisEntradas = nameof(QuaisEntradas);
    public const string QualWindows = nameof(QualWindows);
    public const string Bateria = nameof(Bateria);
    public const string FonteDeAlimentacao = nameof(FonteDeAlimentacao);
    public const string TipoDeConexao = nameof(TipoDeConexao);
    public const string QuantasPortas_I = nameof(QuantasPortas_I);
    public const string TiposDeRaid = nameof(TiposDeRaid);
    public const string CapacidadeMaxHD_I = nameof(CapacidadeMaxHD_I);
    public const string AteQuantosHds_I = nameof(AteQuantosHds_I);
    public const string BateriaInclusa = nameof(BateriaInclusa);
    public const string QuantasEntradas_I = nameof(QuantasEntradas_I);
    public const string QuaisPortas = nameof(QuaisPortas);
    public const string SuportaFibra = nameof(SuportaFibra);
    public const string QuantosCanais_I = nameof(QuantosCanais_I);
    public const string Soquete = nameof(Soquete);
    public const string NucleosFisicos_I = nameof(NucleosFisicos_I);
    public const string NucleosLogicos_I = nameof(NucleosLogicos_I);
    public const string Wireless = nameof(Wireless);
    public const string BandaMax_I = nameof(BandaMax_I);
    public const string MemoriasSuportadas = nameof(MemoriasSuportadas);
    public const string AteQuantasMemorias_I = nameof(AteQuantasMemorias_I);
    public const string OrdemDasMemorias = nameof(OrdemDasMemorias);
    public const string CapacidadeRamTotal_I = nameof(CapacidadeRamTotal_I);
    public const string PlacaControladora = nameof(PlacaControladora);
    public const string TiposDeHd = nameof(TiposDeHd);
    public const string TamanhoDosHds = nameof(TamanhoDosHds);
    public const string QuantasEQuaisPortas = nameof(QuantasEQuaisPortas);
    public const string CapacidadeMaxCadaPorta = nameof(CapacidadeMaxCadaPorta);
    #endregion

    #region categories names
    public const string C_Inventario = "Inventário";
    public const string C_HD = "HD";
    public const string C_Memoria = "Memória";
    public const string C_PlacaDeRede = "Placa de rede";
    public const string C_Idrac = "iDrac";
    public const string C_PlacaControladora = "Placa controladora";
    public const string C_Processador = "Processador";
    public const string C_Desktop = "Desktop";
    public const string C_Fonte = "Fonte";
    public const string C_Switch = "Switch";
    public const string C_Roteador = "Roteador";
    public const string C_Carregador = "Carregador";
    public const string C_AdaptadorAC = "Adaptador AC";
    public const string C_StorageNAS = "Storage NAS";
    public const string C_Gbic = "Gbic";
    public const string C_PlacaDeVideo = "Placa de vídeo";
    public const string C_PlacaDeSom = "Placa de som";
    public const string C_PlacaDeCapturaDeVideo = "Placa de captura de vídeo";
    public const string C_Servidor = "Servidor";
    public const string C_Notebook = "Notebook";
    public const string C_Monitor = "Monitor";
    public const string C_Movimentacao = "Movimentação";
    public const string C_Mouse = "Mouse";
    public const string C_Teclado = "Teclado";
    public const string C_FoneRamal = "Fone para ramal";
    public const string C_Ramal = "Ramal";
    public const string C_Nobreak = "No break";
    public const string C_Outros = "Outros";
    public const string C_USB = "USB";
    public const string C_PlacaSAS = "Placa SAS";
    #endregion

    #region Categories arrays
    public static readonly string[] AllCategories = { C_AdaptadorAC, C_Carregador, C_Desktop, C_FoneRamal, C_Fonte, C_Gbic, C_HD, C_Idrac, C_Memoria, C_Monitor,
    C_Mouse, C_Nobreak, C_Notebook, C_PlacaControladora, C_PlacaDeCapturaDeVideo, C_PlacaDeRede, C_PlacaDeSom, C_PlacaDeVideo, C_Processador, C_Ramal,C_Roteador,
    C_Servidor, C_StorageNAS, C_Switch, C_Teclado, C_Outros };
    public static readonly string[] SNPCategories = { C_AdaptadorAC, C_Carregador, C_Desktop, C_Fonte, C_Gbic, C_HD, C_Idrac, C_Memoria, C_Monitor, C_Mouse, C_Nobreak,
    C_Notebook, C_Outros, C_PlacaControladora, C_PlacaDeCapturaDeVideo, C_PlacaDeRede, C_PlacaDeSom, C_PlacaDeVideo, C_Processador, C_Roteador, C_Servidor, C_StorageNAS,
    C_Switch, C_Teclado, C_USB };
    public static readonly string[] ConcertCategories = { C_AdaptadorAC, C_Desktop, C_Monitor, C_Notebook, C_Roteador, C_Servidor, C_Switch, C_Outros };
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
    public const string SavingCounter = "SavingCounter";
    public const string BkpIndex = "BkpIndex";
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
    public const string AddNewNotebook = "notebook.php";
    public const string AddNewPlacaControladora = "placacontroladora.php";
    public const string AddNewPlacaDeCapturaDeVideo = "placadecapturadevideo.php";
    public const string AddNewPlacaDeRede = "placaderede.php";
    public const string AddNewPlacaDeSom = "placadesom.php";
    public const string AddNewPlacaDeVideo = "placadevideo.php";
    public const string AddNewPlacaSAS = "placasas.php";
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
    public const string ImportPlacaSAS = "importplacasas.php";
    public const string ImportProcessador = "importprocessador.php";
    public const string ImportRegularMovements = "importregularmovements.php";
    public const string ImportRoteador = "importroteador.php";
    public const string ImportServidor = "importservidor.php";
    public const string ImportStorageNas = "importstoragenas.php";
    public const string ImportSwitch = "importswitch.php";
    public const string ImportUsers = "importusers.php";
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
