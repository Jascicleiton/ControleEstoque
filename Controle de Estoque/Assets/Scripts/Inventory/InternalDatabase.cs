using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Saving;
using UnityEditor;

public class InternalDatabase : Singleton<InternalDatabase>, ISaveable
{
    public static Dictionary<string, Sheet> splitDatabase = new Dictionary<string, Sheet>();
    public static Sheet fullDatabase = new Sheet();
    public Sheet testingSheet = new Sheet();

    #region Sheets with all information divided by "Categoria"
    public static Sheet hd = new Sheet();
    public static Sheet memoria = new Sheet();
    public static Sheet placaDeRede = new Sheet();
    public static Sheet idrac = new Sheet();
    public static Sheet placaControladora = new Sheet();
    public static Sheet processador = new Sheet();
    public static Sheet desktop = new Sheet();
    public static Sheet fonte = new Sheet();
    public static Sheet Switch = new Sheet();
    public static Sheet roteador = new Sheet();
    public static Sheet carregador = new Sheet();
    public static Sheet adaptadorAC = new Sheet();
    public static Sheet storageNAS = new Sheet();
    public static Sheet gbic = new Sheet();
    public static Sheet placaDeVideo = new Sheet();
    public static Sheet placaDeSom = new Sheet();
    public static Sheet placaDeCapturaDeVideo = new Sheet();
    #endregion
    private void Update()
    {
        if(Input.GetKeyDown(KeyCode.P))
        {
            FillFullDatabase();
            testingSheet = splitDatabase[ConstStrings.InventarioSnPro];
        }
    }

    /// <summary>
    /// Get all Sheet classes saved on splitDatabase and join them into a single Sheet class
    /// </summary>
    public void FillFullDatabase()
    {
        Sheet inventario = new Sheet();
        splitDatabase.TryGetValue(ConstStrings.InventarioSnPro, out inventario);
        Sheet hdTemp = new Sheet();
        splitDatabase.TryGetValue(ConstStrings.HD, out hdTemp);
        Sheet memoriaTemp = new Sheet();
        splitDatabase.TryGetValue(ConstStrings.Memoria, out memoriaTemp);
        Sheet placaDeRedeTemp = new Sheet();
        splitDatabase.TryGetValue(ConstStrings.PlacaDeRede, out placaDeRedeTemp);
        Sheet idracTemp = new Sheet();
        splitDatabase.TryGetValue(ConstStrings.Idrac, out idracTemp);
        Sheet placaControladoraTemp = new Sheet();
        splitDatabase.TryGetValue(ConstStrings.PlacaControladora, out placaControladoraTemp);
        Sheet processadorTemp = new Sheet();
        splitDatabase.TryGetValue(ConstStrings.Processador, out processadorTemp);
        Sheet desktopTemp = new Sheet();
        splitDatabase.TryGetValue(ConstStrings.Desktop, out desktopTemp);
        Sheet fonteTemp = new Sheet();
        splitDatabase.TryGetValue(ConstStrings.Fonte, out fonteTemp);
        Sheet switchTemp = new Sheet();
        splitDatabase.TryGetValue(ConstStrings.Switch, out switchTemp);
        Sheet roteadorTemp = new Sheet();
        splitDatabase.TryGetValue(ConstStrings.Roteador, out roteadorTemp);
        Sheet carregadorTemp = new Sheet();
        splitDatabase.TryGetValue(ConstStrings.Carregador, out carregadorTemp);
        Sheet adaptadorACTemp = new Sheet();
        splitDatabase.TryGetValue(ConstStrings.AdaptadorAC, out adaptadorACTemp);
        Sheet storageNASTemp = new Sheet();
        splitDatabase.TryGetValue(ConstStrings.StorageNAS, out storageNASTemp);
        Sheet gbicTemp = new Sheet();
        splitDatabase.TryGetValue(ConstStrings.Gbic, out gbicTemp);
        Sheet placaDeVideoTemp = new Sheet();
        splitDatabase.TryGetValue(ConstStrings.PlacaDeVideo, out placaDeVideoTemp);
        Sheet placaDeSomTemp = new Sheet();
        splitDatabase.TryGetValue(ConstStrings.PlacaDeSom, out placaDeSomTemp);
        Sheet placaDeCapturaDeVideoTemp = new Sheet();
        splitDatabase.TryGetValue(ConstStrings.PlacaDeCapturaDeVideo, out placaDeCapturaDeVideoTemp);

        // Get all itens from "Inventario SnPro into the full database
        if (inventario != null && inventario.itens.Count > 0)
        {
            foreach (SheetColumns item in inventario.itens)
            {
                fullDatabase.itens.Add(item);
            }
        }

        // Get the values of the detail sheet based on the "modelo" of the item on Inventario SnPro
        foreach (SheetColumns item in fullDatabase.itens)
        {
            if (item.Categoria.Trim() == ConstStrings.HD.Trim())
            {
                if (hdTemp != null)
                {
                    if (hdTemp.itens.Count > 0)
                    {
                        foreach (SheetColumns hdItem in hdTemp.itens)
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
            else if (item.Categoria.Trim() == ConstStrings.Memoria.Trim())
            {
                if (memoriaTemp != null)
                {
                    if (memoriaTemp.itens.Count > 0)
                    {
                        foreach (SheetColumns memoriaItem in memoriaTemp.itens)
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
            else if (item.Categoria.Trim() == ConstStrings.PlacaDeRede.Trim())
            {
                if (placaDeRedeTemp != null)
                {
                    if (placaDeRedeTemp.itens.Count > 0)
                    {
                        foreach (SheetColumns placaDeRedeItem in placaDeRedeTemp.itens)
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
            else if (item.Categoria.Trim() == ConstStrings.Idrac.Trim())
            {
                if (idracTemp != null)
                {
                    if (idracTemp.itens.Count > 0)
                    {
                        foreach (SheetColumns idracItem in idracTemp.itens)
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
            else if (item.Categoria.Trim() == ConstStrings.PlacaControladora.Trim())
            {
                if (placaControladoraTemp != null)
                {
                    if (placaControladoraTemp.itens.Count > 0)
                    {
                        foreach (SheetColumns placaControladoraItem in placaControladoraTemp.itens)
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
            else if (item.Categoria.Trim() == ConstStrings.Processador.Trim())
            {
                if (processadorTemp != null)
                {
                    if (processadorTemp.itens.Count > 0)
                    {
                        foreach (SheetColumns processadorItem in processadorTemp.itens)
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
            else if (item.Categoria.Trim() == ConstStrings.Desktop.Trim())
            {
                if (desktopTemp != null)
                {
                    if (desktopTemp.itens.Count > 0)
                    {
                        foreach (SheetColumns desktopItem in desktopTemp.itens)
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
            else if (item.Categoria.Trim() == ConstStrings.Fonte.Trim())
            {
                if (fonteTemp != null)
                {
                    if (fonteTemp.itens.Count > 0)
                    {
                        foreach (SheetColumns fonteItem in fonteTemp.itens)
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
            else if (item.Categoria.Trim() == ConstStrings.Switch.Trim())
            {
                if (switchTemp != null)
                {
                    if (switchTemp.itens.Count > 0)
                    {
                        foreach (SheetColumns switchItem in switchTemp.itens)
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
            else if (item.Categoria.Trim() == ConstStrings.Roteador.Trim())
            {
                if (roteadorTemp != null)
                {
                    if (roteadorTemp.itens.Count > 0)
                    {
                        foreach (SheetColumns roteadorItem in roteadorTemp.itens)
                        {

                            if (item.Modelo.Trim().Equals(roteadorItem.Modelo.Trim()))
                            {
                                item.Wireless = roteadorItem.Wireless;
                                item.QuantidadeDePortas = roteadorItem.QuantidadeDePortas;
                                item.BandaMaxima = roteadorItem.BandaMaxima;             
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
            else if (item.Categoria.Trim() == ConstStrings.Carregador.Trim())
            {
                if (carregadorTemp != null)
                {
                    if (carregadorTemp.itens.Count > 0)
                    {
                        foreach (SheetColumns carregadorItem in carregadorTemp.itens)
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
            else if (item.Categoria.Trim() == ConstStrings.AdaptadorAC.Trim())
            {
                if (adaptadorACTemp != null)
                {
                    if (adaptadorACTemp.itens.Count > 0)
                    {
                        foreach (SheetColumns adaptadorAcItem in adaptadorACTemp.itens)
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
            else if (item.Categoria.Trim() == ConstStrings.StorageNAS.Trim())
            {
                if (storageNASTemp != null)
                {
                    if (storageNASTemp.itens.Count > 0)
                    {
                        foreach (SheetColumns storageNasItem in storageNASTemp.itens)
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
            else if (item.Categoria.Trim() == ConstStrings.Gbic.Trim())
            {
                if (gbicTemp != null)
                {
                    if (gbicTemp.itens.Count > 0)
                    {
                        foreach (SheetColumns gbicItem in gbicTemp.itens)
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
            else if (item.Categoria.Trim() == ConstStrings.PlacaDeVideo.Trim())
            {
                if (placaDeVideoTemp != null)
                {
                    if (placaDeVideoTemp.itens.Count > 0)
                    {
                        foreach (SheetColumns placaDeVideoItem in placaDeVideoTemp.itens)
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
            else if (item.Categoria.Trim() == ConstStrings.PlacaDeSom.Trim())
            {
                if (placaDeSomTemp != null)
                {
                    if (placaDeSomTemp.itens.Count > 0)
                    {
                        foreach (SheetColumns placaDeSomItem in placaDeSomTemp.itens)
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
            else if (item.Categoria.Trim() == ConstStrings.PlacaDeCapturaDeVideo.Trim())
            {
                if (placaDeCapturaDeVideoTemp != null)
                {
                    if (placaDeCapturaDeVideoTemp.itens.Count > 0)
                    {
                        foreach (SheetColumns placaDeCapturaDeVideoItem in placaDeCapturaDeVideoTemp.itens)
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
        }
        testingSheet = fullDatabase;
    }

    private class Databases
    {
       public Dictionary<string, Sheet> splitDatabase;
       public Sheet fullDatabase;
    }

    public object CaptureState()
    {
        Databases databasesTosave = new Databases();
        databasesTosave.splitDatabase = splitDatabase;
        databasesTosave.fullDatabase = fullDatabase;
        return databasesTosave;
    }

    public void RestoreState(object state)
    {
        Databases databasesToLoad = (Databases)state;
        splitDatabase = databasesToLoad.splitDatabase;
        fullDatabase = databasesToLoad.fullDatabase;
    }
}
