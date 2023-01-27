using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Saving;
using UnityEditor;
using Mirror;
using System;

public class InternalDatabase : Singleton<InternalDatabase>
{
    //private class Databases
    //{
    //    public Dictionary<string, Sheet> splitDatabase = new Dictionary<string, Sheet>();
    //    public Sheet fullDatabase = new Sheet();
    //    public List<Sheet> categoryDatabases = new List<Sheet>();
    //    public List<MovementRecords> movementRecords = new List<MovementRecords>();
    //}

   public  Dictionary<string, Sheet> splitDatabase = new Dictionary<string, Sheet>();
   public  Sheet fullDatabase = new Sheet();

    public Sheet testingSheet = new Sheet();
    public static List<MovementRecords> movementRecords;

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
    #endregion
    public static List<Sheet> allFullDetailsSheets = new List<Sheet>();

    private bool fullDatabaseFilled = false;

    public CurrentEstoque currentEstoque = CurrentEstoque.SnPro;

    private void Start()
    {
        DontDestroyOnLoad(this.gameObject);
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

                                if (item.Modelo.Trim().Equals(adaptadorAcItem.Modelo.Trim()))
                                {
                                    item.OndeFunciona = adaptadorAcItem.OndeFunciona;
                                    item.VoltagemDeSaida = adaptadorAcItem.VoltagemDeSaida;
                                    item.AmperagemDeSaida = adaptadorAcItem.AmperagemDeSaida;
                                    adaptadorAC.itens.Add(item);
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

                                if (item.Modelo.Trim().Equals(carregadorItem.Modelo.Trim()))
                                {
                                    item.OndeFunciona = carregadorItem.OndeFunciona;
                                    item.VoltagemDeSaida = carregadorItem.VoltagemDeSaida;
                                    item.AmperagemDeSaida = carregadorItem.AmperagemDeSaida;
                                    carregador.itens.Add(item);
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

                                if (item.Patrimonio.Trim().Equals(desktopItem.Patrimonio.Trim()))
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
                    if (foneRamalTemp != null)
                    {
                        if (foneRamal.itens.Count > 0)
                        {
                            foreach (ItemColumns foneRamalItem in foneRamalTemp.itens)
                            {
                                if (item.Patrimonio.Trim().Equals(foneRamalItem.Patrimonio.Trim()))
                                {
                                    foneRamal.itens.Add(item);
                                }
                            }
                        }
                        else
                        {
                            Debug.LogWarning("Planilha foneRamal vazia");
                        }
                    }
                    else
                    {
                        Debug.LogWarning("Planilha foneRamal não encontrada");
                    }
                }
                else if (item.Categoria.Trim() == ConstStrings.Fonte.Trim())
                {
                    if (fonteTemp != null)
                    {
                        if (fonteTemp.itens.Count > 0)
                        {
                            foreach (ItemColumns fonteItem in fonteTemp.itens)
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

                                if (item.Modelo.Trim().Equals(gbicItem.Modelo.Trim()))
                                {
                                    item.Desempenho = gbicItem.Desempenho;
                                    gbic.itens.Add(item);
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
                            //  testingSheet = hd;

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

                                if (item.Modelo.Trim().Equals(monitorItem.Modelo.Trim()))
                                {
                                    item.Polegadas = monitorItem.Polegadas;
                                    item.QuaisConexoes = monitorItem.QuaisConexoes;
                                    monitor.itens.Add(item);
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
                    if (mouseTemp != null)
                    {
                        if (mouseTemp.itens.Count > 0)
                        {
                            foreach (ItemColumns mouseItem in mouseTemp.itens)
                            {

                                if (item.Modelo.Trim().Equals(mouseItem.Modelo.Trim()))
                                {
                                    mouse.itens.Add(item);
                                }
                            }
                        }
                        else
                        {
                            Debug.LogWarning("Planilha mouse vazia");
                        }
                    }
                    else
                    {
                        Debug.LogWarning("Planilha mouse não encontrada");
                    }
                }
                else if (item.Categoria.Trim() == ConstStrings.Nobreak.Trim())
                {
                    if (nobreakTemp != null)
                    {
                        if (nobreakTemp.itens.Count > 0)
                        {
                            foreach (ItemColumns notebookItem in nobreakTemp.itens)
                            {
                                if (item.Modelo.Trim().Equals(notebookItem.Modelo.Trim()))
                                {
                                    nobreak.itens.Add(item);
                                }
                            }
                        }
                        else
                        {
                            Debug.LogWarning("Planilha nobreak vazia");
                        }
                    }
                    else
                    {
                        Debug.LogWarning("Planilha nobreak encontrada");
                    }
                }
                else if (item.Categoria.Trim() == ConstStrings.Notebook.Trim())
                {
                    if (notebookTemp != null)
                    {
                        if (notebookTemp.itens.Count > 0)
                        {
                            foreach (ItemColumns notebookItem in notebookTemp.itens)
                            {

                                if (item.Patrimonio.Trim().Equals(notebookItem.Patrimonio.Trim()))
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

                                if (item.Modelo.Trim().Equals(placaDeCapturaDeVideoItem.Modelo.Trim()))
                                {
                                    item.QuantidadeDePortas = placaDeCapturaDeVideoItem.QuantidadeDePortas;
                                    placaDeCapturaDeVideo.itens.Add(item);
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

                                if (item.Modelo.Trim().Equals(placaDeSomItem.Modelo.Trim()))
                                {
                                    item.QuantidadeDePortas = placaDeSomItem.QuantidadeDePortas;
                                    placaDeSom.itens.Add(item);
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

                                if (item.Modelo.Trim().Equals(placaDeVideoItem.Modelo.Trim()))
                                {
                                    item.QuantidadeDePortas = placaDeVideoItem.QuantidadeDePortas;
                                    item.QuaisConexoes = placaDeVideoItem.QuaisConexoes;
                                    placaDeVideo.itens.Add(item);
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
                    if (ramalTemp != null)
                    {
                        if (ramalTemp.itens.Count > 0)
                        {
                            foreach (ItemColumns ramalItem in ramalTemp.itens)
                            {
                                if (item.Patrimonio.Trim().Equals(ramalItem.Patrimonio.Trim()))
                                {
                                    ramal.itens.Add(item);
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
                else if (item.Categoria.Trim() == ConstStrings.Roteador.Trim())
                {
                    if (roteadorTemp != null)
                    {
                        if (roteadorTemp.itens.Count > 0)
                        {
                            foreach (ItemColumns roteadorItem in roteadorTemp.itens)
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

                                if (item.Modelo.Trim().Equals(servidorItem.Modelo.Trim()))
                                {
                                    // need to update the "Servidor" sheet with informations
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

                                if (item.Modelo.Trim().Equals(switchItem.Modelo.Trim()))
                                {
                                    item.QuantidadeDePortas = switchItem.QuantidadeDePortas;
                                    item.Desempenho = switchItem.Desempenho;
                                    Switch.itens.Add(item);
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

                                if (item.Patrimonio.Trim().Equals(tecladoItem.Patrimonio.Trim()))
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
            }
            allFullDetailsSheets = HelperMethods.CreateSheetListFromArray(new Sheet[] { adaptadorAC,
            carregador, desktop, foneRamal, fonte, gbic, hd, idrac, memoria, monitor, mouse, nobreak,
            notebook, placaControladora, placaDeCapturaDeVideo, placaDeRede, placaDeSom, placaDeVideo,
            processador, ramal, roteador, servidor, storageNAS, Switch, teclado });
            fullDatabaseFilled = true;
        }
    }

    //    /// <summary>
    //    /// Used to save the informations of this class locally
    //    /// </summary>
    //    public object CaptureState()
    //    {
    //        Databases databasesTosave = new Databases();
    //        databasesTosave.splitDatabase = splitDatabase;
    //        databasesTosave.fullDatabase = fullDatabase;
    //        databasesTosave.categoryDatabases = new List<Sheet>();
    //        databasesTosave.categoryDatabases.Add(hd);
    //        databasesTosave.categoryDatabases.Add(memoria);
    //        databasesTosave.categoryDatabases.Add(placaDeRede);
    //        databasesTosave.categoryDatabases.Add(idrac);
    //        databasesTosave.categoryDatabases.Add(placaControladora);
    //        databasesTosave.categoryDatabases.Add(processador);
    //        databasesTosave.categoryDatabases.Add(desktop);
    //        databasesTosave.categoryDatabases.Add(fonte);
    //        databasesTosave.categoryDatabases.Add(Switch);
    //        databasesTosave.categoryDatabases.Add(roteador);
    //        databasesTosave.categoryDatabases.Add(carregador);
    //        databasesTosave.categoryDatabases.Add(adaptadorAC);
    //        databasesTosave.categoryDatabases.Add(storageNAS);
    //        databasesTosave.categoryDatabases.Add(gbic);
    //        databasesTosave.categoryDatabases.Add(placaDeVideo);
    //        databasesTosave.categoryDatabases.Add(placaDeSom);
    //        databasesTosave.categoryDatabases.Add(placaDeCapturaDeVideo);
    //        databasesTosave.movementRecords = movementRecords;
    //        return databasesTosave;
    //    }

    //    /// <summary>
    //    /// Used to load the informations of this class
    //    /// </summary>
    //    public void RestoreState(object state)
    //    {
    //        Databases databasesToLoad = (Databases)state;
    //        splitDatabase = databasesToLoad.splitDatabase;
    //        fullDatabase = databasesToLoad.fullDatabase;
    //        hd = databasesToLoad.categoryDatabases[0];
    //        memoria = databasesToLoad.categoryDatabases[1];
    //        placaDeRede = databasesToLoad.categoryDatabases[2];
    //        idrac = databasesToLoad.categoryDatabases[3];
    //        placaControladora = databasesToLoad.categoryDatabases[4];
    //        processador = databasesToLoad.categoryDatabases[5];
    //        desktop = databasesToLoad.categoryDatabases[6];
    //        fonte = databasesToLoad.categoryDatabases[7];
    //        Switch = databasesToLoad.categoryDatabases[8];
    //        roteador = databasesToLoad.categoryDatabases[9];
    //        carregador = databasesToLoad.categoryDatabases[10];
    //        adaptadorAC = databasesToLoad.categoryDatabases[11];
    //        storageNAS = databasesToLoad.categoryDatabases[12];
    //        gbic = databasesToLoad.categoryDatabases[13];
    //        placaDeVideo = databasesToLoad.categoryDatabases[14];
    //        placaDeSom = databasesToLoad.categoryDatabases[15];
    //        placaDeCapturaDeVideo = databasesToLoad.categoryDatabases[16];
    //        movementRecords = databasesToLoad.movementRecords;
    //    }
}
