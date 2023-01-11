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
    /// Used to show the result of consulting the database"
    /// itemName = 0: "Patrimônio"; itemName = 1: "Serial"
    /// </summary>
    public void ShowResult(ItemColumns itemToShow, int itemName)
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
            parameterNames[0].text = "Aquisição";
            parameterValues[0].text = itemToShow.Aquisicao;
            parameterNames[1].text = "Entrada";
            parameterValues[1].text = itemToShow.Entrada;
            parameterNames[2].text = "Patrimônio";
            parameterValues[2].text = itemToShow.Patrimonio;
            parameterNames[3].text = "Status";
            parameterValues[3].text = itemToShow.Status;
            parameterNames[4].text = "Serial";
            parameterValues[4].text = itemToShow.Serial;
            parameterNames[5].text = "Categoria";
            parameterValues[5].text = itemToShow.Categoria;
            parameterNames[6].text = "Fabricante";
            parameterValues[6].text = itemToShow.Fabricante;
            parameterNames[7].text = "Modelo";
            parameterValues[7].text = itemToShow.Modelo;
            parameterNames[8].text = "Local";
            parameterValues[8].text = itemToShow.Local;
            parameterNames[9].text = "Saída";
            parameterValues[9].text = itemToShow.Saida;
            parameterNames[10].text = "Observação";
            parameterValues[10].text = itemToShow.Observacao;
            switch (itemToShow.Categoria)
            {
                #region HD
                case ConstStrings.HD:
                    parameterNames[11].text = "Interface";
                    parameterValues[11].text = itemToShow.Interface;
                    parameterNames[12].text = "Tamanho";
                    parameterValues[12].text = itemToShow.Tamanho;
                    parameterNames[13].text = "Forma de armazenamento";
                    parameterValues[13].text = itemToShow.FormaDeArmazenamento;
                    parameterNames[14].text = "Capacidade (GB)";
                    parameterValues[14].text = itemToShow.CapacidadeEmGB;
                    parameterNames[15].text = "RPM";
                    parameterValues[15].text = itemToShow.RPM;
                    parameterNames[16].text = "Velocidade de Leitura (Gb/s)";
                    parameterValues[16].text = itemToShow.VelocidadeDeLeitura;
                    parameterNames[17].text = "Enterprise";
                    parameterValues[17].text = itemToShow.Enterprise;
                    break;
                #endregion
                #region Memoria
                case ConstStrings.Memoria:
                    parameterNames[11].text = "Tipo";
                    parameterValues[11].text = itemToShow.Tipo;
                    parameterNames[12].text = "Capacidade (GB)";
                    parameterValues[12].text = itemToShow.CapacidadeEmGB;
                    parameterNames[13].text = "Velocidade (MHz)";
                    parameterValues[13].text = itemToShow.VelocidadeMHz;
                    parameterNames[14].text = "É low voltage?";
                    parameterValues[14].text = itemToShow.LowVoltage;
                    parameterNames[15].text = "Rank";
                    parameterValues[15].text = itemToShow.Rank;
                    parameterNames[16].text = "DIMM";
                    parameterValues[16].text = itemToShow.DIMM;
                    parameterNames[17].text = "Taxa de transmissão";
                    parameterValues[17].text = itemToShow.TaxaDeTransmissao;
                    parameterNames[18].text = "Símbolo";
                    parameterValues[18].text = itemToShow.Simbolo;
                    break;
                #endregion
                #region Placa de rede
                case ConstStrings.PlacaDeRede:
                    parameterNames[11].text = "Interface";
                    parameterValues[11].text = itemToShow.Interface;
                    parameterNames[12].text = "Quantas portas?";
                    parameterValues[12].text = itemToShow.QuantidadeDePortas;
                    parameterNames[13].text = "Quais portas?";
                    parameterValues[13].text = itemToShow.QuaisConexoes;
                    parameterNames[14].text = "Suporta fibra óptica?";
                    parameterValues[14].text = itemToShow.SuportaFibraOptica;
                    parameterNames[15].text = "Desempenho (MB/s)";
                    parameterValues[15].text = itemToShow.Desempenho;
                    break;
                #endregion
                #region iDrac
                case ConstStrings.Idrac:
                    parameterNames[11].text = "Porta";
                    parameterValues[11].text = itemToShow.QuaisConexoes;
                    parameterNames[12].text = "Velocidade (GB/s)";
                    parameterValues[12].text = itemToShow.VelocidadeGBs;
                    parameterNames[13].text = "Entrada SD";
                    parameterValues[13].text = itemToShow.EntradaSD;
                    parameterNames[14].text = "Servidores suportados";
                    parameterValues[14].text = itemToShow.ServidoresSuportados;
                    break;
                #endregion
                #region Placa controladora
                case ConstStrings.PlacaControladora:
                    parameterNames[11].text = "Tipo de conexão";
                    parameterValues[11].text = itemToShow.QuaisConexoes;
                    parameterNames[12].text = "Quantas portas?";
                    parameterValues[12].text = itemToShow.QuantidadeDePortas;
                    parameterNames[13].text = "Tipos de RAID";
                    parameterValues[13].text = itemToShow.TipoDeRAID;
                    parameterNames[14].text = "Capacidade máx do HD (TB)";
                    parameterValues[14].text = itemToShow.CapacidadeMaxHD;
                    parameterNames[15].text = "Até quantos HDs";
                    parameterValues[15].text = itemToShow.AteQuantosHDs;
                    parameterNames[16].text = "Bateria inclusa?";
                    parameterValues[16].text = itemToShow.BateriaInclusa;
                    parameterNames[17].text = "Barramento";
                    parameterValues[17].text = itemToShow.Barramento;
                    break;
                #endregion
                #region Processador
                case ConstStrings.Processador:
                    parameterNames[11].text = "Soquete";
                    parameterValues[11].text = itemToShow.Soquete;
                    parameterNames[12].text = "Nº núcleos físicos";
                    parameterValues[12].text = itemToShow.NucleosFisicos;
                    parameterNames[13].text = "Nº núcleos lógicos";
                    parameterValues[13].text = itemToShow.NucleosLogicos;
                    parameterNames[14].text = "Aceita virtualização?";
                    parameterValues[14].text = itemToShow.AceitaVirtualizacao;
                    parameterNames[15].text = "Turbo boost?";
                    parameterValues[15].text = itemToShow.TurboBoost;
                    parameterNames[16].text = "Hyper-Threading?";
                    parameterValues[16].text = itemToShow.HyperThreading;
                    break;
                #endregion
                #region Desktop
                case ConstStrings.Desktop:
                    parameterNames[11].text = "Modelo de placa mãe";
                    parameterValues[11].text = itemToShow.ModeloPlacaMae;
                    parameterNames[12].text = "Fonte?";
                    parameterValues[12].text = itemToShow.Fonte;
                    parameterNames[13].text = "Memória?";
                    parameterValues[13].text = itemToShow.Memoria;
                    parameterNames[14].text = "HD?";
                    parameterValues[14].text = itemToShow.HD;
                    parameterNames[15].text = "Placa de vídeo?";
                    parameterValues[15].text = itemToShow.PlacaDeVideo;
                    parameterNames[16].text = "Leitor de DVD?";
                    parameterValues[16].text = itemToShow.LeitorDeDVD;
                    parameterNames[17].text = "Processador";
                    parameterValues[17].text = itemToShow.Processador;
                    break;
                #endregion
                #region Fonte
                case ConstStrings.Fonte:
                    parameterNames[11].text = "Watts de potência";
                    parameterValues[11].text = itemToShow.Watts;
                    parameterNames[12].text = "Onde funciona?";
                    parameterValues[12].text = itemToShow.OndeFunciona;
                    parameterNames[13].text = "Conectores";
                    parameterValues[13].text = itemToShow.Conectores;
                    break;
                #endregion
                #region Switch
                case ConstStrings.Switch:
                    parameterNames[11].text = "Quantas entradas";
                    parameterValues[11].text = itemToShow.QuantidadeDePortas;
                    parameterNames[12].text = "Capacidade máx de cada porta (MB/s)";
                    parameterValues[12].text = itemToShow.Desempenho;
                    break;
                #endregion
                #region Roteador
                case ConstStrings.Roteador:
                    parameterNames[11].text = "Wireless?";
                    parameterValues[11].text = itemToShow.Wireless;
                    parameterNames[12].text = "Quantas entradas?";
                    parameterValues[12].text = itemToShow.QuantidadeDePortas;
                    parameterNames[13].text = "Banda máx (MB/s)";
                    parameterValues[13].text = itemToShow.BandaMaxima;
                    break;
                #endregion
                #region Carregador
                case ConstStrings.Carregador:
                    parameterNames[11].text = "Onde funciona?";
                    parameterValues[11].text = itemToShow.OndeFunciona;
                    parameterNames[12].text = "Voltagem de saída";
                    parameterValues[12].text = itemToShow.VoltagemDeSaida;
                    parameterNames[13].text = "Amperagem de saída (mA)";
                    parameterValues[13].text = itemToShow.AmperagemDeSaida;
                    break;
                #endregion
                #region Adaptador AC
                case ConstStrings.AdaptadorAC:
                    parameterNames[11].text = "Onde funciona?";
                    parameterValues[11].text = itemToShow.OndeFunciona;
                    parameterNames[12].text = "Voltagem de saída";
                    parameterValues[12].text = itemToShow.VoltagemDeSaida;
                    parameterNames[13].text = "Amperagem de saída (A)";
                    parameterValues[13].text = itemToShow.AmperagemDeSaida;
                    break;
                #endregion
                #region Storage NAS
                case ConstStrings.StorageNAS:
                    parameterNames[11].text = "Tamanho dos HDs";
                    parameterValues[11].text = itemToShow.Tamanho;
                    parameterNames[12].text = "Tipos de RAID";
                    parameterValues[12].text = itemToShow.TipoDeRAID;
                    parameterNames[13].text = "Tipo de HD";
                    parameterValues[13].text = itemToShow.TipoDeHD;
                    parameterNames[14].text = "Capacidade máx do HD";
                    parameterValues[14].text = itemToShow.CapacidadeMaxHD;
                    parameterNames[15].text = "Até quantos HDs";
                    parameterValues[15].text = itemToShow.AteQuantosHDs;
                    break;
                #endregion
                #region GBIC
                case ConstStrings.Gbic:
                    parameterNames[11].text = "Desempenho máx (GB/s)";
                    parameterValues[11].text = itemToShow.Desempenho;
                    break;
                #endregion
                #region Placa de Video
                case ConstStrings.PlacaDeVideo:
                    parameterNames[11].text = "Quantas entradas?";
                    parameterValues[11].text = itemToShow.QuantidadeDePortas;
                    parameterNames[12].text = "Quais entradas?";
                    parameterValues[12].text = itemToShow.QuaisConexoes;
                    break;
                #endregion
                #region Placa de som
                case ConstStrings.PlacaDeSom:
                    parameterNames[11].text = "Quantos canais?";
                    parameterValues[11].text = itemToShow.QuantosCanais;
                    break;
                #endregion
                #region Placa de captura de video
                case ConstStrings.PlacaDeCapturaDeVideo:
                    parameterNames[11].text = "Quantas entradas?";
                    parameterValues[11].text = itemToShow.QuantidadeDePortas;
                    break;
                #endregion
                #region Servidor
                case ConstStrings.Servidor:
                    break;
                #endregion
                #region Notebook
                case ConstStrings.Notebook:
                    break;
                #endregion
                #region Monitor
                case ConstStrings.Monitor:
                    parameterNames[11].text = "Polegadas";
                    parameterValues[11].text = itemToShow.Polegadas;
                    parameterNames[12].text = "Quais entradas?";
                    parameterValues[12].text = itemToShow.QuaisConexoes;
                    break;
                #endregion
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
       // ChangeSize();
    }

    private void ChangeSize()
    {
        if(itemBoxes.Length < 10)
        {
            GetComponent<RectTransform>().SetSizeWithCurrentAnchors(RectTransform.Axis.Horizontal, 210f);
        }
        else if( itemBoxes.Length < 20)
        {
            GetComponent<RectTransform>().SetSizeWithCurrentAnchors(RectTransform.Axis.Horizontal, 370f);
        }
        else
        {
            GetComponent<RectTransform>().SetSizeWithCurrentAnchors(RectTransform.Axis.Horizontal, 550f);
        }
    }
}
