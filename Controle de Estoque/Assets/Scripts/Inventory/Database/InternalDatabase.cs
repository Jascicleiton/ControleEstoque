using Mono.Cecil;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InternalDatabase : Singleton<InternalDatabase>
{
    //private class Databases
    //{
    //    public Dictionary<string, Sheet> splitDatabase = new Dictionary<string, Sheet>();
    //    public Sheet fullDatabase = new Sheet();
    //    public List<Sheet> categoryDatabases = new List<Sheet>();
    //    public List<MovementRecords> movementRecords = new List<MovementRecords>();
    //}

    [SerializeField] GameObject importingWidget;
    [SerializeField] GameObject exportManagerPrefab;

    public Dictionary<string, Sheet> splitDatabase = new Dictionary<string, Sheet>();
    public Sheet fullDatabase = new Sheet();

    public Sheet testingSheet = new Sheet();
    public static List<MovementRecords> movementRecords;
    public static List<string> locations = new List<string>();

    #region Sheets with all information divided by "Categoria"
    public static Sheet adaptadorAC = new Sheet();
    public static Sheet carregador = new Sheet();
    public static Sheet desktop = new Sheet();
    public static Sheet foneRamal = new Sheet();
    public static Sheet fonte = new Sheet();
    public static Sheet gbic = new Sheet();
    public static Sheet hd = new Sheet();
    public static Sheet idrac = new Sheet();
    public static Sheet memoria = new Sheet();
    public static Sheet monitor = new Sheet();
    public static Sheet mouse = new Sheet();
    public static Sheet nobreak = new Sheet();
    public static Sheet notebook = new Sheet();
    public static Sheet placaControladora = new Sheet();
    public static Sheet placaDeCapturaDeVideo = new Sheet();
    public static Sheet placaDeRede = new Sheet();
    public static Sheet placaDeSom = new Sheet();
    public static Sheet placaDeVideo = new Sheet();
    public static Sheet processador = new Sheet();
    public static Sheet roteador = new Sheet();
    public static Sheet ramal = new Sheet();
    public static Sheet servidor = new Sheet();
    public static Sheet storageNAS = new Sheet();
    public static Sheet Switch = new Sheet();
    public static Sheet teclado = new Sheet();
    public static Sheet outros = new Sheet();
    #endregion
    public static List<Sheet> allFullDetailsSheets = new List<Sheet>();

    private bool fullDatabaseFilled = false;

    public CurrentEstoque currentEstoque = CurrentEstoque.SnPro;

    private void Start()
    {
        DontDestroyOnLoad(this.gameObject);
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.F5))
        {
            ReImport();
        }
        if (Input.GetKeyDown(KeyCode.F12))
        {
            if (UsersManager.Instance.currentUser.username == "marcelo.fonseca")
            {
                Instantiate(exportManagerPrefab);
            }
        }
    }

     private void UpdateFullDatabase()
    {
        /// Try to get all sheets that are available on splitdatabase
        #region Sheets
        Sheet inventario = new Sheet();
        splitDatabase.TryGetValue(ConstStrings.InventarioSnPro, out inventario);
        Sheet adaptadorACTemp = new Sheet();
        splitDatabase.TryGetValue(ConstStrings.AdaptadorAC, out adaptadorACTemp);
        Sheet carregadorTemp = new Sheet();
        splitDatabase.TryGetValue(ConstStrings.Carregador, out carregadorTemp);
        Sheet desktopTemp = new Sheet();
        splitDatabase.TryGetValue(ConstStrings.Desktop, out desktopTemp);
        Sheet foneRamalTemp = new Sheet();
        splitDatabase.TryGetValue(ConstStrings.FoneRamal, out foneRamalTemp);
        Sheet fonteTemp = new Sheet();
        splitDatabase.TryGetValue(ConstStrings.Fonte, out fonteTemp);
        Sheet gbicTemp = new Sheet();
        splitDatabase.TryGetValue(ConstStrings.Gbic, out gbicTemp);
        Sheet hdTemp = new Sheet();
        splitDatabase.TryGetValue(ConstStrings.HD, out hdTemp);
        Sheet idracTemp = new Sheet();
        splitDatabase.TryGetValue(ConstStrings.Idrac, out idracTemp);
        Sheet memoriaTemp = new Sheet();
        splitDatabase.TryGetValue(ConstStrings.Memoria, out memoriaTemp);
        Sheet monitorTemp = new Sheet();
        splitDatabase.TryGetValue(ConstStrings.Monitor, out monitorTemp);
        Sheet mouseTemp = new Sheet();
        splitDatabase.TryGetValue(ConstStrings.Monitor, out mouseTemp);
        Sheet nobreakTemp = new Sheet();
        splitDatabase.TryGetValue(ConstStrings.Monitor, out nobreakTemp);
        Sheet notebookTemp = new Sheet();
        splitDatabase.TryGetValue(ConstStrings.Notebook, out notebookTemp);
        Sheet placaControladoraTemp = new Sheet();
        splitDatabase.TryGetValue(ConstStrings.PlacaControladora, out placaControladoraTemp);
        Sheet placaDeCapturaDeVideoTemp = new Sheet();
        splitDatabase.TryGetValue(ConstStrings.PlacaDeCapturaDeVideo, out placaDeCapturaDeVideoTemp);
        Sheet placaDeRedeTemp = new Sheet();
        splitDatabase.TryGetValue(ConstStrings.PlacaDeRede, out placaDeRedeTemp);
        Sheet placaDeSomTemp = new Sheet();
        splitDatabase.TryGetValue(ConstStrings.PlacaDeSom, out placaDeSomTemp);
        Sheet placaDeVideoTemp = new Sheet();
        splitDatabase.TryGetValue(ConstStrings.PlacaDeVideo, out placaDeVideoTemp);
        Sheet processadorTemp = new Sheet();
        splitDatabase.TryGetValue(ConstStrings.Processador, out processadorTemp);
        Sheet roteadorTemp = new Sheet();
        splitDatabase.TryGetValue(ConstStrings.Roteador, out roteadorTemp);
        Sheet ramalTemp = new Sheet();
        splitDatabase.TryGetValue(ConstStrings.Roteador, out ramalTemp);
        Sheet servidorTemp = new Sheet();
        splitDatabase.TryGetValue(ConstStrings.Servidor, out servidorTemp);
        Sheet storageNASTemp = new Sheet();
        splitDatabase.TryGetValue(ConstStrings.StorageNAS, out storageNASTemp);
        Sheet switchTemp = new Sheet();
        splitDatabase.TryGetValue(ConstStrings.Switch, out switchTemp);
        Sheet tecladoTemp = new Sheet();
        splitDatabase.TryGetValue(ConstStrings.StorageNAS, out tecladoTemp);
        Sheet outrosTemp = new Sheet();
        splitDatabase.TryGetValue(ConstStrings.Outros, out outrosTemp);
        #endregion

        // Get all itens from "Inventario SnPro into the full database
        if (inventario != null && inventario.itens.Count > 0)
        {
            for (int i = 0; i < fullDatabase.itens.Count; i++)
            {
                fullDatabase.itens[i] = inventario.itens[i];
            }
        }

        // Get the values of the detail sheet based on the "modelo" of the item on Inventario SnPro
        foreach (ItemColumns item in fullDatabase.itens)
        {
            if (item.Categoria.Trim() == ConstStrings.AdaptadorAC.Trim())
            {
                if (adaptadorACTemp != null)
                {
                    if (adaptadorACTemp.itens.Count > 0)
                    {
                        foreach (ItemColumns adaptadorAcItem in adaptadorACTemp.itens)
                        {
                            if (item.Modelo != null && adaptadorAcItem.Modelo != null)
                            {
                                if (item.Modelo.Trim().Equals(adaptadorAcItem.Modelo.Trim()))
                                {
                                    item.OndeFunciona = adaptadorAcItem.OndeFunciona;
                                    item.VoltagemDeSaida = adaptadorAcItem.VoltagemDeSaida;
                                    item.AmperagemDeSaida = adaptadorAcItem.AmperagemDeSaida;
                                    adaptadorAC.itens.Add(item);
                                }
                            }
                        }
                    }
                    else
                    {
                        Debug.LogWarning("Planilha adaptador AC vazia");
                    }
                }
                else
                {
                    Debug.LogWarning("Planilha adaptador AC não encontrada");
                }
            }
            else if (item.Categoria.Trim() == ConstStrings.Carregador.Trim())
            {
                if (carregadorTemp != null)
                {
                    if (carregadorTemp.itens.Count > 0)
                    {
                        foreach (ItemColumns carregadorItem in carregadorTemp.itens)
                        {
                            if (item.Modelo != null && carregadorItem.Modelo != null)
                            {
                                if (item.Modelo.Trim().Equals(carregadorItem.Modelo.Trim()))
                                {
                                    item.OndeFunciona = carregadorItem.OndeFunciona;
                                    item.VoltagemDeSaida = carregadorItem.VoltagemDeSaida;
                                    item.AmperagemDeSaida = carregadorItem.AmperagemDeSaida;
                                    carregador.itens.Add(item);
                                }
                            }
                        }
                    }
                    else
                    {
                        Debug.LogWarning("Planilha carregador vazia");
                    }
                }
                else
                {
                    Debug.LogWarning("Planilha carregador não encontrada");
                }
            }
            else if (item.Categoria.Trim() == ConstStrings.Desktop.Trim())
            {
                if (desktopTemp != null)
                {
                    if (desktopTemp.itens.Count > 0)
                    {
                        foreach (ItemColumns desktopItem in desktopTemp.itens)
                        {

                            if (item.Patrimonio == desktopItem.Patrimonio)
                            {
                                item.ModeloPlacaMae = desktopItem.ModeloPlacaMae;
                                item.Fonte = desktopItem.Fonte;
                                item.Memoria = desktopItem.Memoria;
                                item.HD = desktopItem.HD;
                                item.PlacaDeVideo = desktopItem.PlacaDeVideo;
                                item.PlacaDeRede = desktopItem.PlacaDeRede;
                                item.LeitorDeDVD = desktopItem.LeitorDeDVD;
                                item.Processador = desktopItem.Processador;
                                desktop.itens.Add(item);
                            }

                        }

                    }
                    else
                    {
                        Debug.LogWarning("Planilha desktop vazia");
                    }
                }
                else
                {
                    Debug.LogWarning("Planilha desktop não encontrada");
                }
            }
            else if (item.Categoria.Trim() == ConstStrings.FoneRamal.Trim())
            {
                foneRamal.itens.Add(item);
            }
            else if (item.Categoria.Trim() == ConstStrings.Fonte.Trim())
            {
                if (fonteTemp != null)
                {
                    if (fonteTemp.itens.Count > 0)
                    {
                        foreach (ItemColumns fonteItem in fonteTemp.itens)
                        {
                            if (item.Modelo != null && fonteItem.Modelo != null)
                            {
                                if (item.Modelo.Trim().Equals(fonteItem.Modelo.Trim()))
                                {
                                    item.Watts = fonteItem.Watts;
                                    item.OndeFunciona = fonteItem.OndeFunciona;
                                    item.Conectores = fonteItem.Conectores;
                                    fonte.itens.Add(item);
                                }
                            }
                        }
                    }
                    else
                    {
                        Debug.LogWarning("Planilha fonte vazia");
                    }
                }
                else
                {
                    Debug.LogWarning("Planilha fonte não encontrada");
                }
            }
            else if (item.Categoria.Trim() == ConstStrings.Gbic.Trim())
            {
                if (gbicTemp != null)
                {
                    if (gbicTemp.itens.Count > 0)
                    {
                        foreach (ItemColumns gbicItem in gbicTemp.itens)
                        {
                            if (item.Modelo != null && gbicItem.Modelo != null)
                            {
                                if (item.Modelo.Trim().Equals(gbicItem.Modelo.Trim()))
                                {
                                    item.Desempenho = gbicItem.Desempenho;
                                    gbic.itens.Add(item);
                                }
                            }
                        }
                    }
                    else
                    {
                        Debug.LogWarning("Planilha gbic vazia");
                    }
                }
                else
                {
                    Debug.LogWarning("Planilha fonte gbic encontrada");
                }
            }
            else if (item.Categoria.Trim() == ConstStrings.HD.Trim())
            {
                if (hdTemp != null)
                {
                    if (hdTemp.itens.Count > 0)
                    {
                        foreach (ItemColumns hdItem in hdTemp.itens)
                        {
                            if (item.Modelo != null && hdItem.Modelo != null)
                            {
                                if (item.Modelo.Trim().Equals(hdItem.Modelo.Trim()))
                                {
                                    item.Interface = hdItem.Interface;
                                    item.Tamanho = hdItem.Tamanho;
                                    item.FormaDeArmazenamento = hdItem.FormaDeArmazenamento;
                                    item.CapacidadeEmGB = hdItem.CapacidadeEmGB;
                                    item.RPM = hdItem.RPM;
                                    item.VelocidadeDeLeitura = hdItem.VelocidadeDeLeitura;
                                    item.Enterprise = hdItem.Enterprise;
                                    hd.itens.Add(item);
                                }
                            }
                        }
                    }
                    else
                    {
                        Debug.LogWarning("Planilha hd vazia");
                    }
                }
                else
                {
                    Debug.LogWarning("Planilha hd não encontrada");
                }
            }
            else if (item.Categoria.Trim() == ConstStrings.Idrac.Trim())
            {
                if (idracTemp != null)
                {
                    if (idracTemp.itens.Count > 0)
                    {
                        foreach (ItemColumns idracItem in idracTemp.itens)
                        {
                            if (item.Modelo != null && idracItem.Modelo != null)
                            {
                                if (item.Modelo.Trim().Equals(idracItem.Modelo.Trim()))
                                {
                                    item.QuaisConexoes = idracItem.QuaisConexoes;
                                    item.VelocidadeGBs = idracItem.VelocidadeGBs;
                                    item.EntradaSD = idracItem.EntradaSD;
                                    item.ServidoresSuportados = idracItem.ServidoresSuportados;
                                    idrac.itens.Add(item);
                                }
                            }
                        }
                    }
                    else
                    {
                        Debug.LogWarning("Planilha iDrac vazia");
                    }
                }
                else
                {
                    Debug.LogWarning("Planilha iDrac não encontrada");
                }
            }
            else if (item.Categoria.Trim() == ConstStrings.Memoria.Trim())
            {
                if (memoriaTemp != null)
                {
                    if (memoriaTemp.itens.Count > 0)
                    {
                        foreach (ItemColumns memoriaItem in memoriaTemp.itens)
                        {
                            if (item.Modelo != null && memoriaItem.Modelo != null)
                            {
                                if (item.Modelo.Trim().Equals(memoriaItem.Modelo.Trim()))
                                {
                                    item.Tipo = memoriaItem.Tipo;
                                    item.CapacidadeEmGB = memoriaItem.CapacidadeEmGB;
                                    item.VelocidadeMHz = memoriaItem.VelocidadeMHz;
                                    item.LowVoltage = memoriaItem.LowVoltage;
                                    item.Rank = memoriaItem.Rank;
                                    item.DIMM = memoriaItem.DIMM;
                                    item.TaxaDeTransmissao = memoriaItem.TaxaDeTransmissao;
                                    item.Simbolo = memoriaItem.Simbolo;
                                    memoria.itens.Add(item);
                                }
                            }
                        }
                    }
                    else
                    {
                        Debug.LogWarning("Planilha Placa memória vazia");
                    }
                }
                else
                {
                    Debug.LogWarning("Planilha memória não encontrada");
                }
            }
            else if (item.Categoria.Trim() == ConstStrings.Monitor.Trim())
            {
                if (monitorTemp != null)
                {
                    if (monitorTemp.itens.Count > 0)
                    {
                        foreach (ItemColumns monitorItem in monitorTemp.itens)
                        {
                            if (item.Modelo != null && monitorItem.Modelo != null)
                            {
                                if (item.Modelo.Trim().Equals(monitorItem.Modelo.Trim()))
                                {
                                    item.Polegadas = monitorItem.Polegadas;
                                    item.QuaisConexoes = monitorItem.QuaisConexoes;
                                    monitor.itens.Add(item);
                                }
                            }
                        }
                    }
                    else
                    {
                        Debug.LogWarning("Planilha placa de captura de vídeo vazia");
                    }
                }
                else
                {
                    Debug.LogWarning("Planilha placa de captura de vídeo não encontrada");
                }
            }
            else if (item.Categoria.Trim() == ConstStrings.Mouse.Trim())
            {
                mouseTemp.itens.Add(item);
            }
            else if (item.Categoria.Trim() == ConstStrings.Nobreak.Trim())
            {
                nobreak.itens.Add(item);
            }
            else if (item.Categoria.Trim() == ConstStrings.Notebook.Trim())
            {
                if (notebookTemp != null)
                {
                    if (notebookTemp.itens.Count > 0)
                    {
                        foreach (ItemColumns notebookItem in notebookTemp.itens)
                        {

                            if (item.Patrimonio == notebookItem.Patrimonio)
                            {
                                item.HD = notebookItem.HD;
                                item.Memoria = notebookItem.Memoria;
                                item.EntradaRJ49 = notebookItem.EntradaRJ49;
                                item.BateriaInclusa = notebookItem.BateriaInclusa;
                                item.AdaptadorAC = notebookItem.AdaptadorAC;
                                item.Windows = notebookItem.Windows;
                                notebook.itens.Add(item);
                            }

                        }
                    }
                    else
                    {
                        Debug.LogWarning("Planilha Notebook vazia");
                    }
                }
                else
                {
                    Debug.LogWarning("Planilha Notebook encontrada");
                }
            }
            else if (item.Categoria.Trim() == ConstStrings.PlacaControladora.Trim())
            {
                if (placaControladoraTemp != null)
                {
                    if (placaControladoraTemp.itens.Count > 0)
                    {
                        foreach (ItemColumns placaControladoraItem in placaControladoraTemp.itens)
                        {
                            if (item.Modelo != null && placaControladoraItem.Modelo != null)
                            {
                                if (item.Modelo.Trim().Equals(placaControladoraItem.Modelo.Trim()))
                                {
                                    item.QuaisConexoes = placaControladoraItem.QuaisConexoes;
                                    item.QuantidadeDePortas = placaControladoraItem.QuantidadeDePortas;
                                    item.TipoDeRAID = placaControladoraItem.TipoDeRAID;
                                    item.TipoDeHD = placaControladoraItem.TipoDeHD;
                                    item.CapacidadeMaxHD = placaControladoraItem.CapacidadeMaxHD;
                                    item.AteQuantosHDs = placaControladoraItem.AteQuantosHDs;
                                    item.BateriaInclusa = placaControladoraItem.BateriaInclusa;
                                    item.Barramento = placaControladoraItem.Barramento;
                                    placaControladora.itens.Add(item);
                                }
                            }
                        }
                    }
                    else
                    {
                        Debug.LogWarning("Planilha Placa controladora vazia");
                    }
                }
                else
                {
                    Debug.LogWarning("Planilha Placa controladora não encontrada");
                }
            }
            else if (item.Categoria.Trim() == ConstStrings.PlacaDeCapturaDeVideo.Trim())
            {
                if (placaDeCapturaDeVideoTemp != null)
                {
                    if (placaDeCapturaDeVideoTemp.itens.Count > 0)
                    {
                        foreach (ItemColumns placaDeCapturaDeVideoItem in placaDeCapturaDeVideoTemp.itens)
                        {
                            if (item.Modelo != null && placaDeCapturaDeVideoItem.Modelo != null)
                            {
                                if (item.Modelo.Trim().Equals(placaDeCapturaDeVideoItem.Modelo.Trim()))
                                {
                                    item.QuantidadeDePortas = placaDeCapturaDeVideoItem.QuantidadeDePortas;
                                    placaDeCapturaDeVideo.itens.Add(item);
                                }
                            }
                        }
                    }
                    else
                    {
                        Debug.LogWarning("Planilha placa de captura de vídeo vazia");
                    }
                }
                else
                {
                    Debug.LogWarning("Planilha placa de captura de vídeo não encontrada");
                }
            }
            else if (item.Categoria.Trim() == ConstStrings.PlacaDeRede.Trim())
            {
                if (placaDeRedeTemp != null)
                {
                    if (placaDeRedeTemp.itens.Count > 0)
                    {
                        foreach (ItemColumns placaDeRedeItem in placaDeRedeTemp.itens)
                        {
                            if (item.Modelo != null && placaDeRedeItem.Modelo != null)
                            {
                                if (item.Modelo.Trim().Equals(placaDeRedeItem.Modelo.Trim()))
                                {
                                    item.Interface = placaDeRedeItem.Interface;
                                    item.QuantidadeDePortas = placaDeRedeItem.QuantidadeDePortas;
                                    item.QuaisConexoes = placaDeRedeItem.QuaisConexoes;
                                    item.SuportaFibraOptica = placaDeRedeItem.SuportaFibraOptica;
                                    item.Desempenho = placaDeRedeItem.Desempenho;
                                    placaDeRede.itens.Add(item);
                                }
                            }
                        }
                    }
                    else
                    {
                        Debug.LogWarning("Planilha Placa de rede vazia");
                    }
                }
                else
                {
                    Debug.LogWarning("Planilha Placa de Rede não encontrada");
                }
            }
            else if (item.Categoria.Trim() == ConstStrings.PlacaDeSom.Trim())
            {
                if (placaDeSomTemp != null)
                {
                    if (placaDeSomTemp.itens.Count > 0)
                    {
                        foreach (ItemColumns placaDeSomItem in placaDeSomTemp.itens)
                        {
                            if (item.Modelo != null && placaDeSomItem.Modelo != null)
                            {
                                if (item.Modelo.Trim().Equals(placaDeSomItem.Modelo.Trim()))
                                {
                                    item.QuantidadeDePortas = placaDeSomItem.QuantidadeDePortas;
                                    placaDeSom.itens.Add(item);
                                }
                            }
                        }
                    }
                    else
                    {
                        Debug.LogWarning("Planilha placa de som vazia");
                    }
                }
                else
                {
                    Debug.LogWarning("Planilha placa de som não encontrada");
                }
            }
            else if (item.Categoria.Trim() == ConstStrings.PlacaDeVideo.Trim())
            {
                if (placaDeVideoTemp != null)
                {
                    if (placaDeVideoTemp.itens.Count > 0)
                    {
                        foreach (ItemColumns placaDeVideoItem in placaDeVideoTemp.itens)
                        {
                            if (item.Modelo != null && placaDeVideoItem.Modelo != null)
                            {
                                if (item.Modelo.Trim().Equals(placaDeVideoItem.Modelo.Trim()))
                                {
                                    item.QuantidadeDePortas = placaDeVideoItem.QuantidadeDePortas;
                                    item.QuaisConexoes = placaDeVideoItem.QuaisConexoes;
                                    placaDeVideo.itens.Add(item);
                                }
                            }
                        }
                    }
                    else
                    {
                        Debug.LogWarning("Planilha placa de vídeo vazia");
                    }
                }
                else
                {
                    Debug.LogWarning("Planilha placa de vídeo não encontrada");
                }
            }
            else if (item.Categoria.Trim() == ConstStrings.Processador.Trim())
            {
                if (processadorTemp != null)
                {
                    if (processadorTemp.itens.Count > 0)
                    {
                        foreach (ItemColumns processadorItem in processadorTemp.itens)
                        {
                            if (item.Modelo != null && processadorItem.Modelo != null)
                            {
                                if (item.Modelo.Trim().Equals(processadorItem.Modelo.Trim()))
                                {
                                    item.Soquete = processadorItem.Soquete;
                                    item.NucleosFisicos = processadorItem.NucleosFisicos;
                                    item.NucleosLogicos = processadorItem.NucleosLogicos;
                                    item.AceitaVirtualizacao = processadorItem.AceitaVirtualizacao;
                                    item.TurboBoost = processadorItem.TurboBoost;
                                    item.HyperThreading = processadorItem.HyperThreading;
                                    processador.itens.Add(item);
                                }
                            }
                        }
                    }
                    else
                    {
                        Debug.LogWarning("Planilha processador vazia");
                    }
                }
                else
                {
                    Debug.LogWarning("Planilha Processador não encontrada");
                }
            }
            else if (item.Categoria.Trim() == ConstStrings.Ramal.Trim())
            {
                ramal.itens.Add(item);
            }
            else if (item.Categoria.Trim() == ConstStrings.Roteador.Trim())
            {
                if (roteadorTemp != null)
                {
                    if (roteadorTemp.itens.Count > 0)
                    {
                        foreach (ItemColumns roteadorItem in roteadorTemp.itens)
                        {
                            if (item.Modelo != null && roteadorItem.Modelo != null)
                            {
                                if (item.Modelo.Trim().Equals(roteadorItem.Modelo.Trim()))
                                {
                                    item.Wireless = roteadorItem.Wireless;
                                    item.QuantidadeDePortas = roteadorItem.QuantidadeDePortas;
                                    item.BandaMaxima = roteadorItem.BandaMaxima;
                                    item.VoltagemDeSaida = roteadorItem.VoltagemDeSaida;
                                    roteador.itens.Add(item);
                                }
                            }
                        }
                    }
                    else
                    {
                        Debug.LogWarning("Planilha roteador vazia");
                    }
                }
                else
                {
                    Debug.LogWarning("Planilha roteador não encontrada");
                }
            }
            else if (item.Categoria.Trim() == ConstStrings.Servidor.Trim())
            {
                if (servidorTemp != null)
                {
                    if (servidorTemp.itens.Count > 0)
                    {
                        foreach (ItemColumns servidorItem in servidorTemp.itens)
                        {

                            if (item.Patrimonio == servidorItem.Patrimonio)
                            {
                                item.Patrimonio = servidorItem.Patrimonio;
                                item.Modelo = servidorItem.Modelo;
                                item.Fabricante = servidorItem.Fabricante;
                                item.ModeloPlacaMae = servidorItem.ModeloPlacaMae;
                                item.Fonte = servidorItem.Fonte;
                                item.Memoria = servidorItem.Memoria;
                                item.HD = servidorItem.HD;
                                item.PlacaDeVideo = servidorItem.PlacaDeVideo;
                                item.PlacaDeRede = servidorItem.PlacaDeRede;
                                item.Processador = servidorItem.Processador;
                                item.MemoriasSuportadas = servidorItem.MemoriasSuportadas;
                                item.QuantasMemorias = servidorItem.QuantasMemorias;
                                item.OrdemDasMemorias = servidorItem.OrdemDasMemorias;
                                item.CapacidadeRAMTotal = servidorItem.CapacidadeRAMTotal;
                                item.Soquete = servidorItem.Soquete;
                                item.PlacaControladora = servidorItem.PlacaControladora;
                                item.AteQuantosHDs = servidorItem.AteQuantosHDs;
                                item.TipoDeHD = servidorItem.TipoDeHD;
                                item.TipoDeRAID = servidorItem.TipoDeRAID;
                                servidor.itens.Add(item);
                            }

                        }
                    }
                    else
                    {
                        Debug.LogWarning("Planilha Servidor vazia");
                    }
                }
                else
                {
                    Debug.LogWarning("Planilha Servidor não encontrada");
                }
            }
            else if (item.Categoria.Trim() == ConstStrings.StorageNAS.Trim())
            {
                if (storageNASTemp != null)
                {
                    if (storageNASTemp.itens.Count > 0)
                    {
                        foreach (ItemColumns storageNasItem in storageNASTemp.itens)
                        {
                            if (item.Modelo != null && storageNasItem.Modelo != null)
                            {
                                if (item.Modelo.Trim().Equals(storageNasItem.Modelo.Trim()))
                                {
                                    item.Tamanho = storageNasItem.Tamanho;
                                    item.TipoDeRAID = storageNasItem.TipoDeRAID;
                                    item.TipoDeHD = storageNasItem.TipoDeHD;
                                    item.CapacidadeMaxHD = storageNasItem.CapacidadeMaxHD;
                                    item.CapacidadeMaxHD = storageNasItem.CapacidadeMaxHD;
                                    item.AteQuantosHDs = storageNasItem.AteQuantosHDs;
                                    storageNAS.itens.Add(item);
                                }
                            }
                        }
                    }
                    else
                    {
                        Debug.LogWarning("Planilha Storage NAS vazia");
                    }
                }
                else
                {
                    Debug.LogWarning("Planilha storage NAS não encontrada");
                }
            }
            else if (item.Categoria.Trim() == ConstStrings.Switch.Trim())
            {
                if (switchTemp != null)
                {
                    if (switchTemp.itens.Count > 0)
                    {
                        foreach (ItemColumns switchItem in switchTemp.itens)
                        {
                            if (item.Modelo != null && switchItem.Modelo != null)
                            {
                                if (item.Modelo.Trim().Equals(switchItem.Modelo.Trim()))
                                {
                                    item.QuantidadeDePortas = switchItem.QuantidadeDePortas;
                                    item.Desempenho = switchItem.Desempenho;
                                    Switch.itens.Add(item);
                                }
                            }
                        }
                    }
                    else
                    {
                        Debug.LogWarning("Planilha Switch vazia");
                    }
                }
                else
                {
                    Debug.LogWarning("Planilha Switch não encontrada");
                }
            }
            else if (item.Categoria.Trim() == ConstStrings.Teclado.Trim())
            {
                if (tecladoTemp != null)
                {
                    if (tecladoTemp.itens.Count > 0)
                    {
                        foreach (ItemColumns tecladoItem in tecladoTemp.itens)
                        {

                            if (item.Patrimonio == tecladoItem.Patrimonio)
                            {
                                teclado.itens.Add(item);
                            }

                        }
                    }
                    else
                    {
                        Debug.Log("Planilha Teclado vazia");
                    }
                }
                else
                {
                    Debug.LogWarning("Planilha teclado não encontrada");
                }
            }
            else
            {
                outros.itens.Add(item);
            }
        }
    }

    public void ReImport()
    {
        Instantiate(importingWidget);
        ImportLocations.Instance.ReImport();
        InventarioManager.Instance.ImportSheets();
        UpdateFullDatabase();
    }

    /// <summary>
    /// Get all Sheet classes saved on splitDatabase and join them into a single Sheet class
    /// </summary>
    public void FillFullDatabase()
    {
        if (!fullDatabaseFilled)
        {
            /// Try to get all sheets that are available on splitdatabase
            #region Sheets
            Sheet inventario = new Sheet();
            splitDatabase.TryGetValue(ConstStrings.InventarioSnPro, out inventario);
            Sheet adaptadorACTemp = new Sheet();
            splitDatabase.TryGetValue(ConstStrings.AdaptadorAC, out adaptadorACTemp);
            Sheet carregadorTemp = new Sheet();
            splitDatabase.TryGetValue(ConstStrings.Carregador, out carregadorTemp);
            Sheet desktopTemp = new Sheet();
            splitDatabase.TryGetValue(ConstStrings.Desktop, out desktopTemp);
            Sheet foneRamalTemp = new Sheet();
            splitDatabase.TryGetValue(ConstStrings.FoneRamal, out foneRamalTemp);
            Sheet fonteTemp = new Sheet();
            splitDatabase.TryGetValue(ConstStrings.Fonte, out fonteTemp);
            Sheet gbicTemp = new Sheet();
            splitDatabase.TryGetValue(ConstStrings.Gbic, out gbicTemp);
            Sheet hdTemp = new Sheet();
            splitDatabase.TryGetValue(ConstStrings.HD, out hdTemp);
            Sheet idracTemp = new Sheet();
            splitDatabase.TryGetValue(ConstStrings.Idrac, out idracTemp);
            Sheet memoriaTemp = new Sheet();
            splitDatabase.TryGetValue(ConstStrings.Memoria, out memoriaTemp);
            Sheet monitorTemp = new Sheet();
            splitDatabase.TryGetValue(ConstStrings.Monitor, out monitorTemp);
            Sheet mouseTemp = new Sheet();
            splitDatabase.TryGetValue(ConstStrings.Monitor, out mouseTemp);
            Sheet nobreakTemp = new Sheet();
            splitDatabase.TryGetValue(ConstStrings.Monitor, out nobreakTemp);
            Sheet notebookTemp = new Sheet();
            splitDatabase.TryGetValue(ConstStrings.Notebook, out notebookTemp);
            Sheet placaControladoraTemp = new Sheet();
            splitDatabase.TryGetValue(ConstStrings.PlacaControladora, out placaControladoraTemp);
            Sheet placaDeCapturaDeVideoTemp = new Sheet();
            splitDatabase.TryGetValue(ConstStrings.PlacaDeCapturaDeVideo, out placaDeCapturaDeVideoTemp);
            Sheet placaDeRedeTemp = new Sheet();
            splitDatabase.TryGetValue(ConstStrings.PlacaDeRede, out placaDeRedeTemp);
            Sheet placaDeSomTemp = new Sheet();
            splitDatabase.TryGetValue(ConstStrings.PlacaDeSom, out placaDeSomTemp);
            Sheet placaDeVideoTemp = new Sheet();
            splitDatabase.TryGetValue(ConstStrings.PlacaDeVideo, out placaDeVideoTemp);
            Sheet processadorTemp = new Sheet();
            splitDatabase.TryGetValue(ConstStrings.Processador, out processadorTemp);
            Sheet roteadorTemp = new Sheet();
            splitDatabase.TryGetValue(ConstStrings.Roteador, out roteadorTemp);
            Sheet ramalTemp = new Sheet();
            splitDatabase.TryGetValue(ConstStrings.Roteador, out ramalTemp);
            Sheet servidorTemp = new Sheet();
            splitDatabase.TryGetValue(ConstStrings.Servidor, out servidorTemp);
            Sheet storageNASTemp = new Sheet();
            splitDatabase.TryGetValue(ConstStrings.StorageNAS, out storageNASTemp);
            Sheet switchTemp = new Sheet();
            splitDatabase.TryGetValue(ConstStrings.Switch, out switchTemp);
            Sheet tecladoTemp = new Sheet();
            splitDatabase.TryGetValue(ConstStrings.StorageNAS, out tecladoTemp);
            Sheet outrosTemp = new Sheet();
            splitDatabase.TryGetValue(ConstStrings.Outros, out outrosTemp);
            #endregion
            // testingSheet = hdTemp;
            // Get all itens from "Inventario SnPro into the full database
            if (inventario != null && inventario.itens.Count > 0)
            {
                foreach (ItemColumns item in inventario.itens)
                {
                    fullDatabase.itens.Add(item);
                }
            }

            // Get the values of the detail sheet based on the "modelo" of the item on Inventario SnPro
            foreach (ItemColumns item in fullDatabase.itens)
            {
                if (item.Categoria.Trim() == ConstStrings.AdaptadorAC.Trim())
                {
                    if (adaptadorACTemp != null)
                    {
                        if (adaptadorACTemp.itens.Count > 0)
                        {
                            foreach (ItemColumns adaptadorAcItem in adaptadorACTemp.itens)
                            {
                                if (item.Modelo != null && adaptadorAcItem.Modelo != null)
                                {
                                    if (item.Modelo.Trim().Equals(adaptadorAcItem.Modelo.Trim()))
                                    {
                                        item.OndeFunciona = adaptadorAcItem.OndeFunciona;
                                        item.VoltagemDeSaida = adaptadorAcItem.VoltagemDeSaida;
                                        item.AmperagemDeSaida = adaptadorAcItem.AmperagemDeSaida;
                                        adaptadorAC.itens.Add(item);
                                    }
                                }
                            }
                        }
                        else
                        {
                            Debug.LogWarning("Planilha adaptador AC vazia");
                        }
                    }
                    else
                    {
                        Debug.LogWarning("Planilha adaptador AC não encontrada");
                    }
                }
                else if (item.Categoria.Trim() == ConstStrings.Carregador.Trim())
                {
                    if (carregadorTemp != null)
                    {
                        if (carregadorTemp.itens.Count > 0)
                        {
                            foreach (ItemColumns carregadorItem in carregadorTemp.itens)
                            {
                                if (item.Modelo != null && carregadorItem.Modelo != null)
                                {
                                    if (item.Modelo.Trim().Equals(carregadorItem.Modelo.Trim()))
                                    {
                                        item.OndeFunciona = carregadorItem.OndeFunciona;
                                        item.VoltagemDeSaida = carregadorItem.VoltagemDeSaida;
                                        item.AmperagemDeSaida = carregadorItem.AmperagemDeSaida;
                                        carregador.itens.Add(item);
                                    }
                                }
                            }
                        }
                        else
                        {
                            Debug.LogWarning("Planilha carregador vazia");
                        }
                    }
                    else
                    {
                        Debug.LogWarning("Planilha carregador não encontrada");
                    }
                }
                else if (item.Categoria.Trim() == ConstStrings.Desktop.Trim())
                {
                    if (desktopTemp != null)
                    {
                        if (desktopTemp.itens.Count > 0)
                        {
                            foreach (ItemColumns desktopItem in desktopTemp.itens)
                            {

                                if (item.Patrimonio == desktopItem.Patrimonio)
                                {
                                    item.ModeloPlacaMae = desktopItem.ModeloPlacaMae;
                                    item.Fonte = desktopItem.Fonte;
                                    item.Memoria = desktopItem.Memoria;
                                    item.HD = desktopItem.HD;
                                    item.PlacaDeVideo = desktopItem.PlacaDeVideo;
                                    item.PlacaDeRede = desktopItem.PlacaDeRede;
                                    item.LeitorDeDVD = desktopItem.LeitorDeDVD;
                                    item.Processador = desktopItem.Processador;
                                    desktop.itens.Add(item);
                                }

                            }

                        }
                        else
                        {
                            Debug.LogWarning("Planilha desktop vazia");
                        }
                    }
                    else
                    {
                        Debug.LogWarning("Planilha desktop não encontrada");
                    }
                }
                else if (item.Categoria.Trim() == ConstStrings.FoneRamal.Trim())
                {
                    foneRamal.itens.Add(item);
                }
                else if (item.Categoria.Trim() == ConstStrings.Fonte.Trim())
                {
                    if (fonteTemp != null)
                    {
                        if (fonteTemp.itens.Count > 0)
                        {
                            foreach (ItemColumns fonteItem in fonteTemp.itens)
                            {
                                if (item.Modelo != null && fonteItem.Modelo != null)
                                {
                                    if (item.Modelo.Trim().Equals(fonteItem.Modelo.Trim()))
                                    {
                                        item.Watts = fonteItem.Watts;
                                        item.OndeFunciona = fonteItem.OndeFunciona;
                                        item.Conectores = fonteItem.Conectores;
                                        fonte.itens.Add(item);
                                    }
                                }
                            }
                        }
                        else
                        {
                            Debug.LogWarning("Planilha fonte vazia");
                        }
                    }
                    else
                    {
                        Debug.LogWarning("Planilha fonte não encontrada");
                    }
                }
                else if (item.Categoria.Trim() == ConstStrings.Gbic.Trim())
                {
                    if (gbicTemp != null)
                    {
                        if (gbicTemp.itens.Count > 0)
                        {
                            foreach (ItemColumns gbicItem in gbicTemp.itens)
                            {
                                if (item.Modelo != null && gbicItem.Modelo != null)
                                {
                                    if (item.Modelo.Trim().Equals(gbicItem.Modelo.Trim()))
                                    {
                                        item.Desempenho = gbicItem.Desempenho;
                                        gbic.itens.Add(item);
                                    }
                                }
                            }
                        }
                        else
                        {
                            Debug.LogWarning("Planilha gbic vazia");
                        }
                    }
                    else
                    {
                        Debug.LogWarning("Planilha fonte gbic encontrada");
                    }
                }
                else if (item.Categoria.Trim() == ConstStrings.HD.Trim())
                {
                    if (hdTemp != null)
                    {
                        if (hdTemp.itens.Count > 0)
                        {
                            foreach (ItemColumns hdItem in hdTemp.itens)
                            {
                                if (item.Modelo != null && hdItem.Modelo != null)
                                {
                                    if (item.Modelo.Trim().Equals(hdItem.Modelo.Trim()))
                                    {
                                        item.Interface = hdItem.Interface;
                                        item.Tamanho = hdItem.Tamanho;
                                        item.FormaDeArmazenamento = hdItem.FormaDeArmazenamento;
                                        item.CapacidadeEmGB = hdItem.CapacidadeEmGB;
                                        item.RPM = hdItem.RPM;
                                        item.VelocidadeDeLeitura = hdItem.VelocidadeDeLeitura;
                                        item.Enterprise = hdItem.Enterprise;
                                        hd.itens.Add(item);
                                    }
                                }
                            }
                        }
                        else
                        {
                            Debug.LogWarning("Planilha hd vazia");
                        }
                    }
                    else
                    {
                        Debug.LogWarning("Planilha hd não encontrada");
                    }
                }
                else if (item.Categoria.Trim() == ConstStrings.Idrac.Trim())
                {
                    if (idracTemp != null)
                    {
                        if (idracTemp.itens.Count > 0)
                        {
                            foreach (ItemColumns idracItem in idracTemp.itens)
                            {
                                if (item.Modelo != null && idracItem.Modelo != null)
                                {
                                    if (item.Modelo.Trim().Equals(idracItem.Modelo.Trim()))
                                    {
                                        item.QuaisConexoes = idracItem.QuaisConexoes;
                                        item.VelocidadeGBs = idracItem.VelocidadeGBs;
                                        item.EntradaSD = idracItem.EntradaSD;
                                        item.ServidoresSuportados = idracItem.ServidoresSuportados;
                                        idrac.itens.Add(item);
                                    }
                                }
                            }
                        }
                        else
                        {
                            Debug.LogWarning("Planilha iDrac vazia");
                        }
                    }
                    else
                    {
                        Debug.LogWarning("Planilha iDrac não encontrada");
                    }
                }
                else if (item.Categoria.Trim() == ConstStrings.Memoria.Trim())
                {
                    if (memoriaTemp != null)
                    {
                        if (memoriaTemp.itens.Count > 0)
                        {
                            foreach (ItemColumns memoriaItem in memoriaTemp.itens)
                            {
                                if (item.Modelo != null && memoriaItem.Modelo != null)
                                {
                                    if (item.Modelo.Trim().Equals(memoriaItem.Modelo.Trim()))
                                    {
                                        item.Tipo = memoriaItem.Tipo;
                                        item.CapacidadeEmGB = memoriaItem.CapacidadeEmGB;
                                        item.VelocidadeMHz = memoriaItem.VelocidadeMHz;
                                        item.LowVoltage = memoriaItem.LowVoltage;
                                        item.Rank = memoriaItem.Rank;
                                        item.DIMM = memoriaItem.DIMM;
                                        item.TaxaDeTransmissao = memoriaItem.TaxaDeTransmissao;
                                        item.Simbolo = memoriaItem.Simbolo;
                                        memoria.itens.Add(item);
                                    }
                                }
                            }
                        }
                        else
                        {
                            Debug.LogWarning("Planilha Placa memória vazia");
                        }
                    }
                    else
                    {
                        Debug.LogWarning("Planilha memória não encontrada");
                    }
                }
                else if (item.Categoria.Trim() == ConstStrings.Monitor.Trim())
                {
                    if (monitorTemp != null)
                    {
                        if (monitorTemp.itens.Count > 0)
                        {
                            foreach (ItemColumns monitorItem in monitorTemp.itens)
                            {
                                if (item.Modelo != null && monitorItem.Modelo != null)
                                {
                                    if (item.Modelo.Trim().Equals(monitorItem.Modelo.Trim()))
                                    {
                                        item.Polegadas = monitorItem.Polegadas;
                                        item.QuaisConexoes = monitorItem.QuaisConexoes;
                                        monitor.itens.Add(item);
                                    }
                                }
                            }
                        }
                        else
                        {
                            Debug.LogWarning("Planilha placa de captura de vídeo vazia");
                        }
                    }
                    else
                    {
                        Debug.LogWarning("Planilha placa de captura de vídeo não encontrada");
                    }
                }
                else if (item.Categoria.Trim() == ConstStrings.Mouse.Trim())
                {
                    mouse.itens.Add(item);
                    if(currentEstoque == CurrentEstoque.SnPro)
                    {
                        outros.itens.Add(item);
                    }
                }
                else if (item.Categoria.Trim() == ConstStrings.Nobreak.Trim())
                {
                    nobreak.itens.Add(item);
                }
                else if (item.Categoria.Trim() == ConstStrings.Notebook.Trim())
                {
                    if (notebookTemp != null)
                    {
                        if (notebookTemp.itens.Count > 0)
                        {
                            foreach (ItemColumns notebookItem in notebookTemp.itens)
                            {

                                if (item.Patrimonio == notebookItem.Patrimonio)
                                {
                                    item.HD = notebookItem.HD;
                                    item.Memoria = notebookItem.Memoria;
                                    item.EntradaRJ49 = notebookItem.EntradaRJ49;
                                    item.BateriaInclusa = notebookItem.BateriaInclusa;
                                    item.AdaptadorAC = notebookItem.AdaptadorAC;
                                    item.Windows = notebookItem.Windows;
                                    notebook.itens.Add(item);
                                }

                            }
                        }
                        else
                        {
                            Debug.LogWarning("Planilha Notebook vazia");
                        }
                    }
                    else
                    {
                        Debug.LogWarning("Planilha Notebook encontrada");
                    }
                }
                else if (item.Categoria.Trim() == ConstStrings.PlacaControladora.Trim())
                {
                    if (placaControladoraTemp != null)
                    {
                        if (placaControladoraTemp.itens.Count > 0)
                        {
                            foreach (ItemColumns placaControladoraItem in placaControladoraTemp.itens)
                            {
                                if (item.Modelo != null && placaControladoraItem.Modelo != null)
                                {
                                    if (item.Modelo.Trim().Equals(placaControladoraItem.Modelo.Trim()))
                                    {
                                        item.QuaisConexoes = placaControladoraItem.QuaisConexoes;
                                        item.QuantidadeDePortas = placaControladoraItem.QuantidadeDePortas;
                                        item.TipoDeRAID = placaControladoraItem.TipoDeRAID;
                                        item.TipoDeHD = placaControladoraItem.TipoDeHD;
                                        item.CapacidadeMaxHD = placaControladoraItem.CapacidadeMaxHD;
                                        item.AteQuantosHDs = placaControladoraItem.AteQuantosHDs;
                                        item.BateriaInclusa = placaControladoraItem.BateriaInclusa;
                                        item.Barramento = placaControladoraItem.Barramento;
                                        placaControladora.itens.Add(item);
                                    }
                                }
                            }
                        }
                        else
                        {
                            Debug.LogWarning("Planilha Placa controladora vazia");
                        }
                    }
                    else
                    {
                        Debug.LogWarning("Planilha Placa controladora não encontrada");
                    }
                }
                else if (item.Categoria.Trim() == ConstStrings.PlacaDeCapturaDeVideo.Trim())
                {
                    if (placaDeCapturaDeVideoTemp != null)
                    {
                        if (placaDeCapturaDeVideoTemp.itens.Count > 0)
                        {
                            foreach (ItemColumns placaDeCapturaDeVideoItem in placaDeCapturaDeVideoTemp.itens)
                            {
                                if (item.Modelo != null && placaDeCapturaDeVideoItem.Modelo != null)
                                {
                                    if (item.Modelo.Trim().Equals(placaDeCapturaDeVideoItem.Modelo.Trim()))
                                    {
                                        item.QuantidadeDePortas = placaDeCapturaDeVideoItem.QuantidadeDePortas;
                                        placaDeCapturaDeVideo.itens.Add(item);
                                    }
                                }
                            }
                        }
                        else
                        {
                            Debug.LogWarning("Planilha placa de captura de vídeo vazia");
                        }
                    }
                    else
                    {
                        Debug.LogWarning("Planilha placa de captura de vídeo não encontrada");
                    }
                }
                else if (item.Categoria.Trim() == ConstStrings.PlacaDeRede.Trim())
                {
                    if (placaDeRedeTemp != null)
                    {
                        if (placaDeRedeTemp.itens.Count > 0)
                        {
                            foreach (ItemColumns placaDeRedeItem in placaDeRedeTemp.itens)
                            {
                                if (item.Modelo != null && placaDeRedeItem.Modelo != null)
                                {
                                    if (item.Modelo.Trim().Equals(placaDeRedeItem.Modelo.Trim()))
                                    {
                                        item.Interface = placaDeRedeItem.Interface;
                                        item.QuantidadeDePortas = placaDeRedeItem.QuantidadeDePortas;
                                        item.QuaisConexoes = placaDeRedeItem.QuaisConexoes;
                                        item.SuportaFibraOptica = placaDeRedeItem.SuportaFibraOptica;
                                        item.Desempenho = placaDeRedeItem.Desempenho;
                                        placaDeRede.itens.Add(item);
                                    }
                                }
                            }
                        }
                        else
                        {
                            Debug.LogWarning("Planilha Placa de rede vazia");
                        }
                    }
                    else
                    {
                        Debug.LogWarning("Planilha Placa de Rede não encontrada");
                    }
                }
                else if (item.Categoria.Trim() == ConstStrings.PlacaDeSom.Trim())
                {
                    if (placaDeSomTemp != null)
                    {
                        if (placaDeSomTemp.itens.Count > 0)
                        {
                            foreach (ItemColumns placaDeSomItem in placaDeSomTemp.itens)
                            {
                                if (item.Modelo != null && placaDeSomItem.Modelo != null)
                                {
                                    if (item.Modelo.Trim().Equals(placaDeSomItem.Modelo.Trim()))
                                    {
                                        item.QuantidadeDePortas = placaDeSomItem.QuantidadeDePortas;
                                        placaDeSom.itens.Add(item);
                                    }
                                }
                            }
                        }
                        else
                        {
                            Debug.LogWarning("Planilha placa de som vazia");
                        }
                    }
                    else
                    {
                        Debug.LogWarning("Planilha placa de som não encontrada");
                    }
                }
                else if (item.Categoria.Trim() == ConstStrings.PlacaDeVideo.Trim())
                {
                    if (placaDeVideoTemp != null)
                    {
                        if (placaDeVideoTemp.itens.Count > 0)
                        {
                            foreach (ItemColumns placaDeVideoItem in placaDeVideoTemp.itens)
                            {
                                if (item.Modelo != null && placaDeVideoItem.Modelo != null)
                                {
                                    if (item.Modelo.Trim().Equals(placaDeVideoItem.Modelo.Trim()))
                                    {
                                        item.QuantidadeDePortas = placaDeVideoItem.QuantidadeDePortas;
                                        item.QuaisConexoes = placaDeVideoItem.QuaisConexoes;
                                        placaDeVideo.itens.Add(item);
                                    }
                                }
                            }
                        }
                        else
                        {
                            Debug.LogWarning("Planilha placa de vídeo vazia");
                        }
                    }
                    else
                    {
                        Debug.LogWarning("Planilha placa de vídeo não encontrada");
                    }
                }
                else if (item.Categoria.Trim() == ConstStrings.Processador.Trim())
                {
                    if (processadorTemp != null)
                    {
                        if (processadorTemp.itens.Count > 0)
                        {
                            foreach (ItemColumns processadorItem in processadorTemp.itens)
                            {
                                if (item.Modelo != null && processadorItem.Modelo != null)
                                {
                                    if (item.Modelo.Trim().Equals(processadorItem.Modelo.Trim()))
                                    {
                                        item.Soquete = processadorItem.Soquete;
                                        item.NucleosFisicos = processadorItem.NucleosFisicos;
                                        item.NucleosLogicos = processadorItem.NucleosLogicos;
                                        item.AceitaVirtualizacao = processadorItem.AceitaVirtualizacao;
                                        item.TurboBoost = processadorItem.TurboBoost;
                                        item.HyperThreading = processadorItem.HyperThreading;
                                        processador.itens.Add(item);
                                    }
                                }
                            }
                        }
                        else
                        {
                            Debug.LogWarning("Planilha processador vazia");
                        }
                    }
                    else
                    {
                        Debug.LogWarning("Planilha Processador não encontrada");
                    }
                }
                else if (item.Categoria.Trim() == ConstStrings.Ramal.Trim())
                {
                    ramal.itens.Add(item);
                }
                else if (item.Categoria.Trim() == ConstStrings.Roteador.Trim())
                {
                    if (roteadorTemp != null)
                    {
                        if (roteadorTemp.itens.Count > 0)
                        {
                            foreach (ItemColumns roteadorItem in roteadorTemp.itens)
                            {
                                if (item.Modelo != null && roteadorItem.Modelo != null)
                                {
                                    if (item.Modelo.Trim().Equals(roteadorItem.Modelo.Trim()))
                                    {
                                        item.Wireless = roteadorItem.Wireless;
                                        item.QuantidadeDePortas = roteadorItem.QuantidadeDePortas;
                                        item.BandaMaxima = roteadorItem.BandaMaxima;
                                        item.VoltagemDeSaida = roteadorItem.VoltagemDeSaida;
                                        roteador.itens.Add(item);
                                    }
                                }
                            }
                        }
                        else
                        {
                            Debug.LogWarning("Planilha roteador vazia");
                        }
                    }
                    else
                    {
                        Debug.LogWarning("Planilha roteador não encontrada");
                    }
                }
                else if (item.Categoria.Trim() == ConstStrings.Servidor.Trim())
                {
                    if (servidorTemp != null)
                    {
                        if (servidorTemp.itens.Count > 0)
                        {
                            foreach (ItemColumns servidorItem in servidorTemp.itens)
                            {
                                if (item.Patrimonio == servidorItem.Patrimonio)
                                {
                                    item.Patrimonio = servidorItem.Patrimonio;
                                    item.Modelo = servidorItem.Modelo;
                                    item.Fabricante = servidorItem.Fabricante;
                                    item.ModeloPlacaMae = servidorItem.ModeloPlacaMae;
                                    item.Fonte = servidorItem.Fonte;
                                    item.Memoria = servidorItem.Memoria;
                                    item.HD = servidorItem.HD;
                                    item.PlacaDeVideo = servidorItem.PlacaDeVideo;
                                    item.PlacaDeRede = servidorItem.PlacaDeRede;
                                    item.Processador = servidorItem.Processador;
                                    item.MemoriasSuportadas = servidorItem.MemoriasSuportadas;
                                    item.QuantasMemorias = servidorItem.QuantasMemorias;
                                    item.OrdemDasMemorias = servidorItem.OrdemDasMemorias;
                                    item.CapacidadeRAMTotal = servidorItem.CapacidadeRAMTotal;
                                    item.Soquete = servidorItem.Soquete;
                                    item.PlacaControladora = servidorItem.PlacaControladora;
                                    item.AteQuantosHDs = servidorItem.AteQuantosHDs;
                                    item.TipoDeHD = servidorItem.TipoDeHD;
                                    item.TipoDeRAID = servidorItem.TipoDeRAID;
                                    servidor.itens.Add(item);
                                }

                            }
                        }
                        else
                        {
                            Debug.LogWarning("Planilha Servidor vazia");
                        }
                    }
                    else
                    {
                        Debug.LogWarning("Planilha Servidor não encontrada");
                    }
                }
                else if (item.Categoria.Trim() == ConstStrings.StorageNAS.Trim())
                {
                    if (storageNASTemp != null)
                    {
                        if (storageNASTemp.itens.Count > 0)
                        {
                            foreach (ItemColumns storageNasItem in storageNASTemp.itens)
                            {
                                if (item.Modelo != null && storageNasItem.Modelo != null)
                                {
                                    if (item.Modelo.Trim().Equals(storageNasItem.Modelo.Trim()))
                                    {
                                        item.Tamanho = storageNasItem.Tamanho;
                                        item.TipoDeRAID = storageNasItem.TipoDeRAID;
                                        item.TipoDeHD = storageNasItem.TipoDeHD;
                                        item.CapacidadeMaxHD = storageNasItem.CapacidadeMaxHD;
                                        item.CapacidadeMaxHD = storageNasItem.CapacidadeMaxHD;
                                        item.AteQuantosHDs = storageNasItem.AteQuantosHDs;
                                        storageNAS.itens.Add(item);
                                    }
                                }
                            }
                        }
                        else
                        {
                            Debug.LogWarning("Planilha Storage NAS vazia");
                        }
                    }
                    else
                    {
                        Debug.LogWarning("Planilha storage NAS não encontrada");
                    }
                }
                else if (item.Categoria.Trim() == ConstStrings.Switch.Trim())
                {
                    if (switchTemp != null)
                    {
                        if (switchTemp.itens.Count > 0)
                        {
                            foreach (ItemColumns switchItem in switchTemp.itens)
                            {
                                if (item.Modelo != null && switchItem.Modelo != null)
                                {
                                    if (item.Modelo.Trim().Equals(switchItem.Modelo.Trim()))
                                    {
                                        item.QuantidadeDePortas = switchItem.QuantidadeDePortas;
                                        item.Desempenho = switchItem.Desempenho;
                                        Switch.itens.Add(item);
                                    }
                                }
                            }
                        }
                        else
                        {
                            Debug.LogWarning("Planilha Switch vazia");
                        }
                    }
                    else
                    {
                        Debug.LogWarning("Planilha Switch não encontrada");
                    }
                }
                else if (item.Categoria.Trim() == ConstStrings.Teclado.Trim())
                {
                    if (currentEstoque == CurrentEstoque.SnPro)
                    {
                        outros.itens.Add(item);
                    }
                    if (tecladoTemp != null)
                    {
                        if (tecladoTemp.itens.Count > 0)
                        {
                            foreach (ItemColumns tecladoItem in tecladoTemp.itens)
                            {
                                if (item.Patrimonio == tecladoItem.Patrimonio)
                                {
                                    teclado.itens.Add(item);
                                }

                            }
                        }
                        else
                        {
                            Debug.Log("Planilha Teclado vazia");
                        }
                    }
                    else
                    {
                        Debug.LogWarning("Planilha teclado não encontrada");
                    }
                }
                else
                {
                    outros.itens.Add(item);
                }
            }
            switch (currentEstoque)
            {
                case CurrentEstoque.SnPro:
                    allFullDetailsSheets = HelperMethods.CreateSheetListFromArray(new Sheet[] { adaptadorAC,
            carregador, desktop, fonte, gbic, hd, idrac, memoria, monitor, nobreak,
            notebook, placaControladora, placaDeCapturaDeVideo, placaDeRede, placaDeSom, placaDeVideo,
            processador,roteador, servidor, storageNAS, Switch, outros});
                    break;
                case CurrentEstoque.Funsoft:
                case CurrentEstoque.ESF:
                case CurrentEstoque.Testing:
                    allFullDetailsSheets = HelperMethods.CreateSheetListFromArray(new Sheet[] { adaptadorAC,
            carregador, desktop, foneRamal, fonte, gbic, hd, idrac, memoria, monitor, mouse, nobreak,
            notebook, placaControladora, placaDeCapturaDeVideo, placaDeRede, placaDeSom, placaDeVideo,
            processador, ramal, roteador, servidor, storageNAS, Switch, teclado, outros });
                    break;
                default:
                    break;
            }

            fullDatabaseFilled = true;
        }
    }



    /// <summary>
    /// Updates ONE specific item on all relevant sheets in the internal database
    /// </summary>
    public void UpdateDatabaseItem(List<string> parameters, int itemIndexFullDatabase)
    {
        int categoryItemToUpdateIndex = 0;

        ItemColumns itemToUpdate = new ItemColumns();
        itemToUpdate.Aquisicao = parameters[0];
        itemToUpdate.Entrada = parameters[1];
        itemToUpdate.Patrimonio = int.Parse(parameters[2]);
        itemToUpdate.Status = parameters[3];
        itemToUpdate.Serial = parameters[4];
        itemToUpdate.Categoria = parameters[5];
        itemToUpdate.Fabricante = parameters[6];
        itemToUpdate.Modelo = parameters[7];
        itemToUpdate.Local = parameters[8];
        itemToUpdate.Saida = parameters[9];
        itemToUpdate.Observacao = parameters[10];
        switch (itemToUpdate.Categoria)
        {
            case ConstStrings.AdaptadorAC:
                itemToUpdate.OndeFunciona = parameters[11];
                itemToUpdate.VoltagemDeSaida = float.Parse(parameters[12]);
                itemToUpdate.AmperagemDeSaida = float.Parse(parameters[13]);
                categoryItemToUpdateIndex = ConsultDatabase.Instance.GetCategoryItemIndex(adaptadorAC, itemToUpdate.Patrimonio);
                adaptadorAC.itens[categoryItemToUpdateIndex] = itemToUpdate;
                break;
            case ConstStrings.Carregador:
                itemToUpdate.OndeFunciona = parameters[11];
                itemToUpdate.VoltagemDeSaida = float.Parse(parameters[12]);
                itemToUpdate.AmperagemDeSaida = float.Parse(parameters[13]);
                categoryItemToUpdateIndex = ConsultDatabase.Instance.GetCategoryItemIndex(carregador, itemToUpdate.Patrimonio);
                carregador.itens[categoryItemToUpdateIndex] = itemToUpdate;
                break;
            case ConstStrings.Desktop:
                itemToUpdate.ModeloPlacaMae = parameters[11];
                itemToUpdate.Fonte = parameters[12];
                itemToUpdate.Memoria = parameters[13];
                itemToUpdate.HD = parameters[14];
                itemToUpdate.PlacaDeVideo = parameters[15];
                itemToUpdate.LeitorDeDVD = parameters[16];
                categoryItemToUpdateIndex = ConsultDatabase.Instance.GetCategoryItemIndex(desktop, itemToUpdate.Patrimonio);
                desktop.itens[categoryItemToUpdateIndex] = itemToUpdate;
                break;
            case ConstStrings.FoneRamal:
                categoryItemToUpdateIndex = ConsultDatabase.Instance.GetCategoryItemIndex(foneRamal, itemToUpdate.Patrimonio);
                foneRamal.itens[categoryItemToUpdateIndex] = itemToUpdate;
                break;
            case ConstStrings.Fonte:
                itemToUpdate.Watts = int.Parse(parameters[11]);
                itemToUpdate.OndeFunciona = parameters[12];
                itemToUpdate.Conectores = parameters[13];
                categoryItemToUpdateIndex = ConsultDatabase.Instance.GetCategoryItemIndex(fonte, itemToUpdate.Patrimonio);
                fonte.itens[categoryItemToUpdateIndex] = itemToUpdate;
                break;
            case ConstStrings.Gbic:
                itemToUpdate.Desempenho = parameters[11];
                categoryItemToUpdateIndex = ConsultDatabase.Instance.GetCategoryItemIndex(gbic, itemToUpdate.Patrimonio);
                gbic.itens[categoryItemToUpdateIndex] = itemToUpdate;
                break;
            case ConstStrings.HD:
                itemToUpdate.Interface = parameters[11];
                itemToUpdate.Tamanho = float.Parse(parameters[12]);
                itemToUpdate.FormaDeArmazenamento = parameters[13];
                itemToUpdate.CapacidadeEmGB = int.Parse(parameters[14]);
                itemToUpdate.RPM = int.Parse(parameters[15]);
                itemToUpdate.VelocidadeDeLeitura = float.Parse(parameters[16]);
                itemToUpdate.Enterprise = parameters[17];
                categoryItemToUpdateIndex = ConsultDatabase.Instance.GetCategoryItemIndex(hd, itemToUpdate.Patrimonio);
                hd.itens[categoryItemToUpdateIndex] = itemToUpdate;
                break;
            case ConstStrings.Idrac:
                itemToUpdate.QuaisConexoes = parameters[11];
                itemToUpdate.VelocidadeGBs = float.Parse(parameters[12]);
                itemToUpdate.EntradaSD = parameters[13];
                itemToUpdate.ServidoresSuportados = parameters[14];
                categoryItemToUpdateIndex = ConsultDatabase.Instance.GetCategoryItemIndex(idrac, itemToUpdate.Patrimonio);
                idrac.itens[categoryItemToUpdateIndex] = itemToUpdate;
                break;
            case ConstStrings.Memoria:
                itemToUpdate.Tipo = parameters[11];
                itemToUpdate.CapacidadeEmGB = int.Parse(parameters[12]);
                itemToUpdate.VelocidadeMHz = int.Parse(parameters[13]);
                itemToUpdate.LowVoltage = parameters[14];
                itemToUpdate.Rank = parameters[15];
                itemToUpdate.DIMM = parameters[16];
                itemToUpdate.TaxaDeTransmissao = int.Parse(parameters[17]);
                itemToUpdate.Simbolo = parameters[18];
                categoryItemToUpdateIndex = ConsultDatabase.Instance.GetCategoryItemIndex(memoria, itemToUpdate.Patrimonio);
                memoria.itens[categoryItemToUpdateIndex] = itemToUpdate;
                break;
            case ConstStrings.Monitor:
                itemToUpdate.Polegadas = float.Parse(parameters[11]);
                itemToUpdate.QuaisConexoes = parameters[12];
                categoryItemToUpdateIndex = ConsultDatabase.Instance.GetCategoryItemIndex(monitor, itemToUpdate.Patrimonio);
                monitor.itens[categoryItemToUpdateIndex] = itemToUpdate;
                break;
            case ConstStrings.Mouse:
                categoryItemToUpdateIndex = ConsultDatabase.Instance.GetCategoryItemIndex(mouse, itemToUpdate.Patrimonio);
                mouse.itens[categoryItemToUpdateIndex] = itemToUpdate;
                break;
            case ConstStrings.Nobreak:
                categoryItemToUpdateIndex = ConsultDatabase.Instance.GetCategoryItemIndex(nobreak, itemToUpdate.Patrimonio);
                nobreak.itens[categoryItemToUpdateIndex] = itemToUpdate;
                break;
            case ConstStrings.Notebook:
                itemToUpdate.HD = parameters[12];
                itemToUpdate.Memoria = parameters[13];
                itemToUpdate.EntradaRJ49 = parameters[14];
                itemToUpdate.BateriaInclusa = parameters[15];
                itemToUpdate.AdaptadorAC = parameters[16];
                itemToUpdate.Windows = parameters[17];
                categoryItemToUpdateIndex = ConsultDatabase.Instance.GetCategoryItemIndex(notebook, itemToUpdate.Patrimonio);
                notebook.itens[categoryItemToUpdateIndex] = itemToUpdate;
                break;
            case ConstStrings.PlacaControladora:
                itemToUpdate.QuaisConexoes = parameters[11];
                itemToUpdate.QuantidadeDePortas = int.Parse(parameters[12]);
                itemToUpdate.TipoDeRAID = parameters[13];
                itemToUpdate.CapacidadeMaxHD = parameters[14];
                itemToUpdate.AteQuantosHDs = int.Parse(parameters[15]);
                itemToUpdate.BateriaInclusa = parameters[16];
                itemToUpdate.Barramento = parameters[17];
                categoryItemToUpdateIndex = ConsultDatabase.Instance.GetCategoryItemIndex(placaControladora, itemToUpdate.Patrimonio);
                placaControladora.itens[categoryItemToUpdateIndex] = itemToUpdate;
                break;
            case ConstStrings.PlacaDeCapturaDeVideo:
                itemToUpdate.QuantidadeDePortas = int.Parse(parameters[11]);
                categoryItemToUpdateIndex = ConsultDatabase.Instance.GetCategoryItemIndex(placaDeCapturaDeVideo, itemToUpdate.Patrimonio);
                placaDeCapturaDeVideo.itens[categoryItemToUpdateIndex] = itemToUpdate;
                break;
            case ConstStrings.PlacaDeRede:
                itemToUpdate.Interface = parameters[11];
                itemToUpdate.QuantidadeDePortas = int.Parse(parameters[12]);
                itemToUpdate.QuaisConexoes = parameters[13];
                itemToUpdate.SuportaFibraOptica = parameters[14];
                itemToUpdate.Desempenho = parameters[15];
                categoryItemToUpdateIndex = ConsultDatabase.Instance.GetCategoryItemIndex(placaDeRede, itemToUpdate.Patrimonio);
                placaDeRede.itens[categoryItemToUpdateIndex] = itemToUpdate;
                break;
            case ConstStrings.PlacaDeSom:
                itemToUpdate.QuantosCanais = int.Parse(parameters[11]);
                categoryItemToUpdateIndex = ConsultDatabase.Instance.GetCategoryItemIndex(placaDeSom, itemToUpdate.Patrimonio);
                placaDeSom.itens[categoryItemToUpdateIndex] = itemToUpdate;
                break;
            case ConstStrings.PlacaDeVideo:
                itemToUpdate.QuantidadeDePortas = int.Parse(parameters[11]);
                itemToUpdate.QuaisConexoes = parameters[12];
                categoryItemToUpdateIndex = ConsultDatabase.Instance.GetCategoryItemIndex(placaDeVideo, itemToUpdate.Patrimonio);
                placaDeVideo.itens[categoryItemToUpdateIndex] = itemToUpdate;
                break;
            case ConstStrings.Processador:
                itemToUpdate.Soquete = parameters[11];
                itemToUpdate.NucleosFisicos = int.Parse(parameters[12]);
                itemToUpdate.NucleosLogicos = int.Parse(parameters[13]);
                itemToUpdate.AceitaVirtualizacao = parameters[14];
                itemToUpdate.TurboBoost = parameters[15];
                itemToUpdate.HyperThreading = parameters[16];
                categoryItemToUpdateIndex = ConsultDatabase.Instance.GetCategoryItemIndex(processador, itemToUpdate.Patrimonio);
                processador.itens[categoryItemToUpdateIndex] = itemToUpdate;
                break;
            case ConstStrings.Ramal:
                categoryItemToUpdateIndex = ConsultDatabase.Instance.GetCategoryItemIndex(ramal, itemToUpdate.Patrimonio);
                ramal.itens[categoryItemToUpdateIndex] = itemToUpdate;
                break;
            case ConstStrings.Roteador:
                itemToUpdate.Wireless = parameters[11];
                itemToUpdate.QuantidadeDePortas = int.Parse(parameters[12]);
                itemToUpdate.BandaMaxima = int.Parse(parameters[13]);
                categoryItemToUpdateIndex = ConsultDatabase.Instance.GetCategoryItemIndex(roteador, itemToUpdate.Patrimonio);
                roteador.itens[categoryItemToUpdateIndex] = itemToUpdate;
                break;
            case ConstStrings.Servidor:
                itemToUpdate.ModeloPlacaMae = parameters[11];
                itemToUpdate.Fonte = parameters[12];
                itemToUpdate.Memoria = parameters[13];
                itemToUpdate.HD = parameters[14];
                itemToUpdate.PlacaDeVideo = parameters[15];
                itemToUpdate.PlacaDeRede = parameters[16];
                itemToUpdate.Processador = parameters[17];
                itemToUpdate.MemoriasSuportadas = parameters[18];
                itemToUpdate.QuantasMemorias = int.Parse(parameters[19]);
                itemToUpdate.OrdemDasMemorias = parameters[20];
                itemToUpdate.CapacidadeRAMTotal = int.Parse(parameters[21]);
                itemToUpdate.Soquete = parameters[22];
                itemToUpdate.PlacaControladora = parameters[23];
                itemToUpdate.AteQuantosHDs = int.Parse(parameters[24]);
                itemToUpdate.TipoDeHD = parameters[25];
                itemToUpdate.TipoDeRAID = parameters[26];
                categoryItemToUpdateIndex = ConsultDatabase.Instance.GetCategoryItemIndex(servidor, itemToUpdate.Patrimonio);
                servidor.itens[categoryItemToUpdateIndex] = itemToUpdate;
                break;
            case ConstStrings.StorageNAS:
                itemToUpdate.Tamanho = float.Parse(parameters[11]);
                itemToUpdate.TipoDeRAID = parameters[12];
                itemToUpdate.TipoDeHD = parameters[13];
                itemToUpdate.CapacidadeMaxHD = parameters[14];
                categoryItemToUpdateIndex = ConsultDatabase.Instance.GetCategoryItemIndex(storageNAS, itemToUpdate.Patrimonio);
                storageNAS.itens[categoryItemToUpdateIndex] = itemToUpdate;
                break;
            case ConstStrings.Switch:
                itemToUpdate.QuantidadeDePortas = int.Parse(parameters[11]);
                itemToUpdate.Desempenho = parameters[12];
                categoryItemToUpdateIndex = ConsultDatabase.Instance.GetCategoryItemIndex(Switch, itemToUpdate.Patrimonio);
                Switch.itens[categoryItemToUpdateIndex] = itemToUpdate;
                break;
            case ConstStrings.Teclado:
                categoryItemToUpdateIndex = ConsultDatabase.Instance.GetCategoryItemIndex(teclado, itemToUpdate.Patrimonio);
                teclado.itens[categoryItemToUpdateIndex] = itemToUpdate;
                break;
            default:
                categoryItemToUpdateIndex = ConsultDatabase.Instance.GetCategoryItemIndex(outros, itemToUpdate.Patrimonio);
                outros.itens[categoryItemToUpdateIndex] = itemToUpdate;
                break;
        }
        fullDatabase.itens[itemIndexFullDatabase] = itemToUpdate;
    }
}
