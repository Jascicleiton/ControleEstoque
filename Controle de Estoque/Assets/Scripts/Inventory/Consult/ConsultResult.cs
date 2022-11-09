using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ConsultResult : MonoBehaviour
{
    [SerializeField] private TMP_Text itemName; // shows either the item "Serial" or "Patrimônio"
    [SerializeField] private Image[] itemBoxes;
    [SerializeField] private TMP_Text[] parameterNames;
    [SerializeField] private TMP_Text[] parameterValues;

    /// <summary>
    /// Used to show the result of consulting using either "Patrimônio" or "Serial"
    /// itemName = 0: "Patrimônio"; itemName = 1: "Serial"
    /// </summary>
    public void ShowResult(SheetColumns itemToShow, int itemName)
    {
        if (itemToShow != null)
        {
            if (itemName == 0)
            {
                this.itemName.text = itemToShow.Patrimonio;
            }
            else if (itemName == 1)
            {
                this.itemName.text = itemToShow.Serial;
            }
            parameterNames[0].text = "Entrada";
            parameterValues[0].text = itemToShow.Entrada;
            parameterNames[1].text = "Patrimônio";
            parameterValues[1].text = itemToShow.Patrimonio;
            parameterNames[2].text = "Status";
            parameterValues[2].text = itemToShow.Status;
            parameterNames[3].text = "Serial";
            parameterValues[3].text = itemToShow.Serial;
            parameterNames[4].text = "Fabricante";
            parameterValues[4].text = itemToShow.Fabricante;
            parameterNames[5].text = "Modelo";
            parameterValues[5].text = itemToShow.Modelo;
            parameterNames[6].text = "Local";
            parameterValues[6].text = itemToShow.Local;
            parameterNames[7].text = "Saída";
            parameterValues[7].text = itemToShow.Saida;
            parameterNames[8].text = "Observação";
            switch (itemToShow.Categoria)
            {
                case ConstStrings.HD:
                    parameterNames[9].text = "Interface";
                    parameterValues[9].text = itemToShow.Interface;
                    parameterNames[10].text = "Tamanho";
                    parameterValues[10].text = itemToShow.Tamanho;
                    parameterNames[11].text = "Forma de armazenamento";
                    parameterValues[11].text = itemToShow.FormaDeArmazenamento;
                    parameterNames[12].text = "Capacidade (GB)";
                    parameterValues[12].text = itemToShow.CapacidadeEmGB;
                    parameterNames[13].text = "RPM";
                    parameterValues[13].text = itemToShow.RPM;
                    parameterNames[14].text = "Velocidade de Leitura (Gb/s)";
                    parameterValues[14].text = itemToShow.VelocidadeDeLeitura;
                    parameterNames[15].text = "Enterprise";
                    parameterValues[15].text = itemToShow.Enterprise;
                    break;
                case ConstStrings.Memoria:
                    parameterNames[9].text = "Tipo";
                    parameterValues[9].text = itemToShow.Tipo;
                    parameterNames[10].text = "Capacidade (GB)";
                    parameterValues[10].text = itemToShow.CapacidadeEmGB;
                    parameterNames[11].text = "Velocidade (MHz)";
                    parameterValues[11].text = itemToShow.VelocidadeMHz;
                    parameterNames[12].text = "É low voltage?";
                    parameterValues[12].text = itemToShow.LowVoltage;
                    parameterNames[13].text = "Rank";
                    parameterValues[13].text = itemToShow.Rank;
                    parameterNames[14].text = "DIMM";
                    parameterValues[14].text = itemToShow.DIMM;
                    parameterNames[15].text = "Taxa de transmissão";
                    parameterValues[15].text = itemToShow.TaxaDeTransmissao;
                    parameterNames[16].text = "Símbolo";
                    parameterValues[16].text = itemToShow.Simbolo;
                    break;
                case ConstStrings.PlacaDeRede:
                    parameterNames[9].text = "Interface";
                    parameterValues[9].text = itemToShow.Interface;
                    parameterNames[10].text = "Quantas portas?";
                    parameterValues[10].text = itemToShow.QuantidadeDePortas;
                    parameterNames[11].text = "Quais portas?";
                    parameterValues[11].text = itemToShow.QuaisConexoes;
                    parameterNames[12].text = "Suporta fibra óptica?";
                    parameterValues[12].text = itemToShow.SuportaFibraOptica;
                    parameterNames[13].text = "Desempenho (MB/s)";
                    parameterValues[13].text = itemToShow.Desempenho;
                    break;
                case ConstStrings.Idrac:
                    parameterNames[9].text = "Porta";
                    parameterValues[9].text = itemToShow.QuaisConexoes;
                    parameterNames[10].text = "Velocidade (GB/s)";
                    parameterValues[10].text = itemToShow.VelocidadeGBs;
                    parameterNames[11].text = "Entrada SD";
                    parameterValues[11].text = itemToShow.EntradaSD;
                    parameterNames[12].text = "Servidores suportados";
                    parameterValues[12].text = itemToShow.ServidoresSuportados;
                    break;
                case ConstStrings.PlacaControladora:
                    parameterNames[9].text = "Tipo de conexão";
                    parameterValues[9].text = itemToShow.QuaisConexoes;
                    parameterNames[10].text = "Quantas portas?";
                    parameterValues[10].text = itemToShow.QuantidadeDePortas;
                    parameterNames[11].text = "Tipos de RAID";
                    parameterValues[11].text = itemToShow.TipoDeRAID;
                    parameterNames[12].text = "Capacidade máx do HD (TB)";
                    parameterValues[12].text = itemToShow.CapacidadeMaxHD;
                    parameterNames[13].text = "Até quantos HDs";
                    parameterValues[13].text = itemToShow.AteQuantosHDs;
                    parameterNames[14].text = "Bateria inclusa?";
                    parameterValues[14].text = itemToShow.BateriaInclusa;
                    parameterNames[15].text = "Barramento";
                    parameterValues[15].text = itemToShow.Barramento;
                    break;
                case ConstStrings.Processador:
                    parameterNames[9].text = "Soquete";
                    parameterValues[9].text = itemToShow.Soquete;
                    parameterNames[10].text = "Nº núcleos físicos";
                    parameterValues[10].text = itemToShow.NucleosFisicos;
                    parameterNames[11].text = "Nº núcleos lógicos";
                    parameterValues[11].text = itemToShow.NucleosLogicos;
                    parameterNames[12].text = "Aceita virtualização?";
                    parameterValues[12].text = itemToShow.AceitaVirtualizacao;
                    parameterNames[13].text = "Turbo boost?";
                    parameterValues[13].text = itemToShow.TurboBoost;
                    parameterNames[14].text = "Hyper-Threading?";
                    parameterValues[14].text = itemToShow.HyperThreading;
                    break;
                case ConstStrings.Desktop:
                    parameterNames[9].text = "Modelo de placa mãe";
                    parameterValues[9].text = itemToShow.ModeloPlacaMae;
                    parameterNames[10].text = "Fonte?";
                    parameterValues[10].text = itemToShow.Fonte;
                    parameterNames[11].text = "Memória?";
                    parameterValues[11].text = itemToShow.Memoria;
                    parameterNames[12].text = "HD?";
                    parameterValues[12].text = itemToShow.HD;
                    parameterNames[13].text = "Placa de vídeo?";
                    parameterValues[13].text = itemToShow.PlacaDeVideo;
                    parameterNames[14].text = "Leitor de DVD?";
                    parameterValues[14].text = itemToShow.LeitorDeDVD;
                    break;
                case ConstStrings.Fonte:
                    parameterNames[9].text = "Watts de potência";
                    parameterValues[9].text = itemToShow.Watts;
                    parameterNames[10].text = "Onde funciona?";
                    parameterValues[10].text = itemToShow.OndeFunciona;
                    parameterNames[11].text = "Conectores";
                    parameterValues[11].text = itemToShow.Conectores;
                    break;
                case ConstStrings.Switch:
                    parameterNames[9].text = "Quantas entradas";
                    parameterValues[9].text = itemToShow.QuantidadeDePortas;
                    parameterNames[10].text = "Capacidade máx de cada porta (MB/s)";
                    parameterValues[10].text = itemToShow.Desempenho;
                    break;
                case ConstStrings.Roteador:
                    parameterNames[9].text = "Wireless?";
                    parameterValues[9].text = itemToShow.Wireless;
                    parameterNames[10].text = "Quantas entradas?";
                    parameterValues[10].text = itemToShow.QuantidadeDePortas;
                    parameterNames[11].text = "Banda máx (MB/s)";
                    parameterValues[11].text = itemToShow.BandaMaxima;
                    break;
                case ConstStrings.Carregador:
                    parameterNames[9].text = "Onde funciona?";
                    parameterValues[9].text = itemToShow.OndeFunciona;
                    parameterNames[10].text = "Voltagem de saída";
                    parameterValues[10].text = itemToShow.VoltagemDeSaida;
                    parameterNames[11].text = "Amperagem de saída (mA)";
                    parameterValues[11].text = itemToShow.AmperagemDeSaida;
                    break;
                case ConstStrings.AdaptadorAC:
                    parameterNames[9].text = "Onde funciona?";
                    parameterValues[9].text = itemToShow.OndeFunciona;
                    parameterNames[10].text = "Voltagem de saída";
                    parameterValues[10].text = itemToShow.VoltagemDeSaida;
                    parameterNames[11].text = "Amperagem de saída (A)";
                    parameterValues[11].text = itemToShow.AmperagemDeSaida;
                    break;
                case ConstStrings.StorageNAS:
                    parameterNames[9].text = "Tamanho dos HDs";
                    parameterValues[9].text = itemToShow.Tamanho;
                    parameterNames[10].text = "Tipos de RAID";
                    parameterValues[10].text = itemToShow.TipoDeRAID;
                    parameterNames[11].text = "Tipo de HD";
                    parameterValues[11].text = itemToShow.TipoDeHD;
                    parameterNames[12].text = "Capacidade máx do HD";
                    parameterValues[12].text = itemToShow.CapacidadeMaxHD;
                    parameterNames[13].text = "Até quantos HDs";
                    break;
                case ConstStrings.Gbic:
                    parameterNames[9].text = "Desempenho máx (GB/s)";
                    parameterValues[9].text = itemToShow.Desempenho;
                    break;
                case ConstStrings.PlacaDeVideo:
                    parameterNames[9].text = "Quantas entradas?";
                    parameterValues[9].text = itemToShow.QuantidadeDePortas;
                    parameterNames[10].text = "Quais entradas?";
                    parameterValues[10].text = itemToShow.QuaisConexoes;
                    break;
                case ConstStrings.PlacaDeSom:
                    parameterNames[9].text = "Quantos canais?";
                    parameterValues[9].text = itemToShow.QuantosCanais;
                    break;
                case ConstStrings.PlacaDeCapturaDeVideo:
                    parameterNames[9].text = "Quantas entradas?";
                    parameterValues[9].text = itemToShow.QuantidadeDePortas;
                    break;

                default:
                    break;
            }
        }
        else
        {
            print("Item to show is null");
        }

        for (int i = 0; i < parameterNames.Length; i++)
        {
            if (parameterNames[i] != null && parameterNames[i].text == "")
            {
                itemBoxes[i].gameObject.SetActive(false);
            }
        }
    }
}
