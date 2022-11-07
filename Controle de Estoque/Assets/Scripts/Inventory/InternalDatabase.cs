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
        Sheet hd = new Sheet();
        splitDatabase.TryGetValue(ConstStrings.HD, out hd);
        Sheet memoria = new Sheet();
        splitDatabase.TryGetValue(ConstStrings.Memoria, out memoria);
        Sheet placaDeRede = new Sheet();
        splitDatabase.TryGetValue(ConstStrings.PlacaDeRede, out placaDeRede);
        Sheet idrac = new Sheet();
        splitDatabase.TryGetValue(ConstStrings.Idrac, out idrac);
        Sheet placaControladora = new Sheet();
        splitDatabase.TryGetValue(ConstStrings.PlacaControladora, out placaControladora);
        Sheet processador = new Sheet();
        splitDatabase.TryGetValue(ConstStrings.Processador, out processador);
        Sheet desktop = new Sheet();
        splitDatabase.TryGetValue(ConstStrings.Desktop, out desktop);
        Sheet fonte = new Sheet();
        splitDatabase.TryGetValue(ConstStrings.Fonte, out fonte);
        Sheet Switch = new Sheet();
        splitDatabase.TryGetValue(ConstStrings.Switch, out Switch);
        Sheet roteador = new Sheet();
        splitDatabase.TryGetValue(ConstStrings.Roteador, out roteador);
        Sheet carregador = new Sheet();
        splitDatabase.TryGetValue(ConstStrings.Carregador, out carregador);
        Sheet adaptadorAC = new Sheet();
        splitDatabase.TryGetValue(ConstStrings.AdaptadorAC, out adaptadorAC);
        Sheet storageNAS = new Sheet();
        splitDatabase.TryGetValue(ConstStrings.StorageNAS, out storageNAS);
        Sheet gbic = new Sheet();
        splitDatabase.TryGetValue(ConstStrings.Gbic, out gbic);
        Sheet placaDeVideo = new Sheet();
        splitDatabase.TryGetValue(ConstStrings.PlacaDeVideo, out placaDeVideo);
        Sheet placaDeSom = new Sheet();
        splitDatabase.TryGetValue(ConstStrings.PlacaDeSom, out placaDeSom);
        Sheet placaDeCapturaDeVideo = new Sheet();
        splitDatabase.TryGetValue(ConstStrings.PlacaDeCapturaDeVideo, out placaDeCapturaDeVideo);

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
                if (hd != null)
                {
                    if (hd.itens.Count > 0)
                    {
                        foreach (SheetColumns hdItem in hd.itens)
                        {

                            if (item.Modelo.Trim().Equals(hdItem.Modelo.Trim()))
                            {
                                item.Interface = hdItem.Interface;
                                item.Tamanho = hdItem.Tamanho;
                                item.FormaDeArmazenamento = hdItem.FormaDeArmazenamento;
                                item.CapacidadeEmGB = hdItem.FormaDeArmazenamento;
                                item.RPM = hdItem.RPM;
                                item.VelocidadeDeLeitura = hdItem.VelocidadeDeLeitura;
                                item.Enterprise = hdItem.Enterprise;
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
                if (memoria != null)
                {
                    if (memoria.itens.Count > 0)
                    {
                        foreach (SheetColumns memoriaItem in memoria.itens)
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
                if (placaDeRede != null)
                {
                    if (placaDeRede.itens.Count > 0)
                    {
                        foreach (SheetColumns placaDeRedeItem in placaDeRede.itens)
                        {

                            if (item.Modelo.Trim().Equals(placaDeRedeItem.Modelo.Trim()))
                            {
                                item.Interface = placaDeRedeItem.Interface;
                                item.QuantidadeDePortas = placaDeRedeItem.QuantidadeDePortas;
                                item.QuaisConexoes = placaDeRedeItem.QuaisConexoes;
                                item.SuportaFibraOptica = placaDeRedeItem.SuportaFibraOptica;
                                item.Desempenho = placaDeRedeItem.Desempenho;
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
                if (idrac != null)
                {
                    if (idrac.itens.Count > 0)
                    {
                        foreach (SheetColumns idracItem in idrac.itens)
                        {

                            if (item.Modelo.Trim().Equals(idracItem.Modelo.Trim()))
                            {
                                item.QuaisConexoes = idracItem.QuaisConexoes;
                                item.VelocidadeGBs = idracItem.VelocidadeGBs;
                                item.EntradaSD = idracItem.EntradaSD;
                                item.ServidoresSuportados = idracItem.ServidoresSuportados;
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
                if (placaControladora != null)
                {
                    if (placaControladora.itens.Count > 0)
                    {
                        foreach (SheetColumns placaControladoraItem in placaControladora.itens)
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
                if (processador != null)
                {
                    if (processador.itens.Count > 0)
                    {
                        foreach (SheetColumns processadorItem in processador.itens)
                        {

                            if (item.Modelo.Trim().Equals(processadorItem.Modelo.Trim()))
                            {
                                item.Soquete = processadorItem.Soquete;
                                item.NucleosFisicos = processadorItem.NucleosFisicos;
                                item.NucleosLogicos = processadorItem.NucleosLogicos;
                                item.AceitaVirtualizacao = processadorItem.AceitaVirtualizacao;
                                item.TurboBoost = processadorItem.TurboBoost;
                                item.HyperThreading = processadorItem.HyperThreading;
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
                if (desktop != null)
                {
                    if (desktop.itens.Count > 0)
                    {
                        foreach (SheetColumns desktopItem in desktop.itens)
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
                if (fonte != null)
                {
                    if (fonte.itens.Count > 0)
                    {
                        foreach (SheetColumns fonteItem in fonte.itens)
                        {

                            if (item.Modelo.Trim().Equals(fonteItem.Modelo.Trim()))
                            {
                                item.Watts = fonteItem.Watts;
                                item.OndeFunciona = fonteItem.OndeFunciona;
                                item.Conectores = fonteItem.Conectores;
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
                if (Switch != null)
                {
                    if (Switch.itens.Count > 0)
                    {
                        foreach (SheetColumns switchItem in Switch.itens)
                        {

                            if (item.Modelo.Trim().Equals(switchItem.Modelo.Trim()))
                            {
                                item.QuantidadeDePortas = switchItem.QuantidadeDePortas;
                                item.Desempenho = switchItem.Desempenho;                            
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
                if (roteador != null)
                {
                    if (roteador.itens.Count > 0)
                    {
                        foreach (SheetColumns roteadorItem in roteador.itens)
                        {

                            if (item.Modelo.Trim().Equals(roteadorItem.Modelo.Trim()))
                            {
                                item.Wireless = roteadorItem.Wireless;
                                item.QuantidadeDePortas = roteadorItem.QuantidadeDePortas;
                                item.BandaMaxima = roteadorItem.BandaMaxima;                                
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
                if (carregador != null)
                {
                    if (carregador.itens.Count > 0)
                    {
                        foreach (SheetColumns carregadorItem in carregador.itens)
                        {

                            if (item.Modelo.Trim().Equals(carregadorItem.Modelo.Trim()))
                            {
                                item.OndeFunciona = carregadorItem.OndeFunciona;
                                item.VoltagemDeSaida = carregadorItem.VoltagemDeSaida;
                                item.AmperagemDeSaida = carregadorItem.AmperagemDeSaida;                               
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
                if (adaptadorAC != null)
                {
                    if (adaptadorAC.itens.Count > 0)
                    {
                        foreach (SheetColumns adaptadorAcItem in adaptadorAC.itens)
                        {

                            if (item.Modelo.Trim().Equals(adaptadorAcItem.Modelo.Trim()))
                            {
                                item.OndeFunciona = adaptadorAcItem.OndeFunciona;
                                item.VoltagemDeSaida = adaptadorAcItem.VoltagemDeSaida;
                                item.AmperagemDeSaida = adaptadorAcItem.AmperagemDeSaida;                               
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
                if (storageNAS != null)
                {
                    if (storageNAS.itens.Count > 0)
                    {
                        foreach (SheetColumns storageNasItem in storageNAS.itens)
                        {

                            if (item.Modelo.Trim().Equals(storageNasItem.Modelo.Trim()))
                            {
                                item.Tamanho = storageNasItem.Tamanho;
                                item.TipoDeRAID = storageNasItem.TipoDeRAID;
                                item.TipoDeHD = storageNasItem.TipoDeHD;
                                item.CapacidadeMaxHD = storageNasItem.CapacidadeMaxHD;
                                item.CapacidadeMaxHD = storageNasItem.CapacidadeMaxHD;
                                item.AteQuantosHDs = storageNasItem.AteQuantosHDs;                               
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
                if (gbic != null)
                {
                    if (gbic.itens.Count > 0)
                    {
                        foreach (SheetColumns gbicItem in gbic.itens)
                        {

                            if (item.Modelo.Trim().Equals(gbicItem.Modelo.Trim()))
                            {
                                item.Desempenho = gbicItem.Desempenho;                                
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
                if (placaDeVideo != null)
                {
                    if (placaDeVideo.itens.Count > 0)
                    {
                        foreach (SheetColumns placaDeVideoItem in placaDeVideo.itens)
                        {

                            if (item.Modelo.Trim().Equals(placaDeVideoItem.Modelo.Trim()))
                            {
                                item.QuantidadeDePortas = placaDeVideoItem.QuantidadeDePortas;
                                item.QuaisConexoes = placaDeVideoItem.QuaisConexoes;                                
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
                if (placaDeSom != null)
                {
                    if (placaDeSom.itens.Count > 0)
                    {
                        foreach (SheetColumns placaDeSomItem in placaDeSom.itens)
                        {

                            if (item.Modelo.Trim().Equals(placaDeSomItem.Modelo.Trim()))
                            {
                                item.QuantidadeDePortas = placaDeSomItem.QuantidadeDePortas;                                
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
                if (placaDeCapturaDeVideo != null)
                {
                    if (placaDeCapturaDeVideo.itens.Count > 0)
                    {
                        foreach (SheetColumns placaDeCapturaDeVideoItem in placaDeCapturaDeVideo.itens)
                        {

                            if (item.Modelo.Trim().Equals(placaDeCapturaDeVideoItem.Modelo.Trim()))
                            {
                                item.QuantidadeDePortas = placaDeCapturaDeVideoItem.QuantidadeDePortas;
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
