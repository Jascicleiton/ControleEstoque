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
    /// </summary>
    public void ShowResult(SheetColumns itemToShow)
    {
        if (itemToShow != null)
        {
            switch (itemToShow.Categoria)
            {
                case ConstStrings.HD:
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
                    parameterValues[8].text = itemToShow.Observacao;
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
                    parameterValues[8].text = itemToShow.Observacao;
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
                    parameterNames[15].text = "Símbolo";
                    parameterValues[15].text = itemToShow.Simbolo;
                    break;
                case ConstStrings.PlacaDeRede:
                    break;
                case ConstStrings.Idrac:
                    break;
                case ConstStrings.PlacaControladora:
                    break;
                case ConstStrings.Processador:
                    break;
                case ConstStrings.Desktop:
                    break;
                case ConstStrings.Fonte:
                    break;
                case ConstStrings.Switch:
                    break;
                case ConstStrings.Roteador:
                    break;
                case ConstStrings.Carregador:
                    break;
                case ConstStrings.AdaptadorAC:
                    break;
                case ConstStrings.StorageNAS:
                    break;
                case ConstStrings.Gbic:
                    break;
                case ConstStrings.PlacaDeVideo:
                    break;
                case ConstStrings.PlacaDeSom:
                    break;
                case ConstStrings.PlacaDeCapturaDeVideo:
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
            if (parameterNames[i].text == "")
            {
                
                itemBoxes[i].gameObject.SetActive(false);
            }
            else
            {
                
            }
        }
        
    }

   
}
