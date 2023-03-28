using System.Collections;
using UnityEngine;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System.Collections.Generic;

public class CreateListFromSheetToSave
{
    public static JArray GetJObject(Sheet sheetToConvert)
    {
        JArray state = new JArray();
        IList<JToken> stateList = state;
        foreach (var item in sheetToConvert.itens)
        {
            JObject jObjectToReturn = new JObject();
            IDictionary<string, JToken> stateDict = jObjectToReturn;
            stateDict["Aquisicao"] = item.Aquisicao;
            stateDict["Entrada"] = item.Entrada;
            stateDict["Patrimonio"] = item.Patrimonio;
            stateDict["Status"] = item.Status;
            stateDict["Serial"] = item.Serial;
            stateDict["Categoria"] = item.Categoria;
            stateDict["Fabricante"] = item.Fabricante;
            stateDict["Modelo"] = item.Modelo;
            stateDict["Local"] = item.Local;
            stateDict["Saida"] = item.Saida;
            stateDict["Observacao"] = item.Observacao;
            stateDict["Interface"] = item.Interface;
            stateDict["Tamanho"] = item.Tamanho;
            stateDict["FormaDeArmazenamento"] = item.FormaDeArmazenamento;
            stateDict["CapacidadeEmGB"] = item.CapacidadeEmGB;
            stateDict["RPM"] = item.RPM;
            stateDict["VelocidadeDeLeitura"] = item.VelocidadeDeLeitura;
            stateDict["Enterprise"] = item.Enterprise;
            stateDict["EstoqueAtual"] = item.EstoqueAtual;
            stateDict["Tipo"] = item.Tipo;
            stateDict["VelocidadeMHz"] = item.VelocidadeMHz;
            stateDict["LowVoltage"] = item.LowVoltage;
            stateDict["Rank"] = item.Rank;
            stateDict["DIMM"] = item.DIMM;
            stateDict["TaxaDeTransmissao"] = item.TaxaDeTransmissao;
            stateDict["Simbolo"] = item.Simbolo;
            stateDict["QuantidadeDePortas"] = item.QuantidadeDePortas;
            stateDict["QuaisConexoes"] = item.QuaisConexoes;
            stateDict["SuportaFibraOptica"] = item.SuportaFibraOptica;
            stateDict["Desempenho"] = item.Desempenho;
            stateDict["VelocidadeGBs"] = item.VelocidadeGBs;
            stateDict["EntradaSD"] = item.EntradaSD;
            stateDict["ServidoresSuportados"] = item.ServidoresSuportados;
            stateDict["TipoDeHD"] = item.TipoDeHD;
            stateDict["TipoDeRAID"] = item.TipoDeRAID;
            stateDict["CapacidadeMaxHD"] = item.CapacidadeMaxHD;
            stateDict["AteQuantosHDs"] = item.AteQuantosHDs;
            stateDict["BateriaInclusa"] = item.BateriaInclusa;
            stateDict["Barramento"] = item.Barramento;
            stateDict["Soquete"] = item.Soquete;
            stateDict["NucleosFisicos"] = item.NucleosFisicos;
            stateDict["NucleosLogicos"] = item.NucleosLogicos;
            stateDict["ModeloPlacaMae"] = item.ModeloPlacaMae;
            stateDict["Fonte"] = item.Fonte;
            stateDict["Memoria"] = item.Memoria;
            stateDict["HD"] = item.HD;
            stateDict["PlacaDeVideo"] = item.PlacaDeVideo;
            stateDict["PlacaDeRede"] = item.PlacaDeRede;
            stateDict["LeitorDeDVD"] = item.LeitorDeDVD;
            stateDict["Watts"] = item.Watts;
            stateDict["OndeFunciona"] = item.OndeFunciona;
            stateDict["Conectores"] = item.Conectores;
            stateDict["Wireless"] = item.Wireless;
            stateDict["BandaMaxima"] = item.BandaMaxima;
            stateDict["VoltagemDeSaida"] = item.VoltagemDeSaida;
            stateDict["AmperagemDeSaida"] = item.AmperagemDeSaida;
            stateDict["QuantosCanais"] = item.QuantosCanais;
            stateDict["Polegadas"] = item.Polegadas;
            stateDict["Processador"] = item.Processador;
            stateDict["MemoriasSuportadas"] = item.MemoriasSuportadas;
            stateDict["QuantasMemorias"] = item.QuantasMemorias;
            stateDict["OrdemDasMemorias"] = item.OrdemDasMemorias;
            stateDict["CapacidadeRAMTotal"] = item.CapacidadeRAMTotal;
            stateDict["PlacaControladora"] = item.PlacaControladora;
            stateDict["EntradaRJ45"] = item.EntradaRJ45;
            stateDict["AdaptadorAC"] = item.AdaptadorAC;
            stateDict["Windows"] = item.Windows;
            stateDict["CapacidadeRAMTotal"] = item.CapacidadeRAMTotal;
            stateDict["CentroDeCusto"] = item.CentroDeCusto;
            stateList.Add(jObjectToReturn);
        }
        return state;
    }
}