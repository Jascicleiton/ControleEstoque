using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class AddRemoveItem : MonoBehaviour
{
    [SerializeField] private TMP_Text[] parameterNames;
    [SerializeField] private TMP_Text[] parameterValues;
    [SerializeField] private TMP_Dropdown categoryDP;

    private void UpdateNames()
    {
        SheetColumns itemToAdd = new SheetColumns();

        parameterNames[0].text = "Entrada";    
        parameterNames[1].text = "Patrim�nio";
       
        parameterNames[2].text = "Status";
       
        parameterNames[3].text = "Serial";
       
        parameterNames[4].text = "Fabricante";
       
        parameterNames[5].text = "Modelo";
       
        parameterNames[6].text = "Local";
       
        parameterNames[7].text = "Sa�da";
       
        parameterNames[8].text = "Observa��o";
        switch (categoryDP.value)
        {
            case 0:
                parameterNames[9].text = "Interface";
                                parameterNames[10].text = "Tamanho";
                                parameterNames[11].text = "Forma de armazenamento";
                                parameterNames[12].text = "Capacidade (GB)";
                                parameterNames[13].text = "RPM";
                                parameterNames[14].text = "Velocidade de Leitura (Gb/s)";
                                parameterNames[15].text = "Enterprise";
                                break;
            case 1:
                parameterNames[9].text = "Tipo";
                                parameterNames[10].text = "Capacidade (GB)";
                                parameterNames[11].text = "Velocidade (MHz)";
                                parameterNames[12].text = "� low voltage?";
                                parameterNames[13].text = "Rank";
                                parameterNames[14].text = "DIMM";
                                parameterNames[15].text = "Taxa de transmiss�o";
                                parameterNames[16].text = "S�mbolo";
                                break;
            case 2:
                parameterNames[9].text = "Interface";
                                parameterNames[10].text = "Quantas portas?";
                                parameterNames[11].text = "Quais portas?";
                                parameterNames[12].text = "Suporta fibra �ptica?";
                                parameterNames[13].text = "Desempenho (MB/s)";
                                break;
            case 3:
                parameterNames[9].text = "Porta";
                                parameterNames[10].text = "Velocidade (GB/s)";
                                parameterNames[11].text = "Entrada SD";
                                parameterNames[12].text = "Servidores suportados";
                                break;
            case 4:
                parameterNames[9].text = "Tipo de conex�o";
                                parameterNames[10].text = "Quantas portas?";
                                parameterNames[11].text = "Tipos de RAID";
                                parameterNames[12].text = "Capacidade m�x do HD (TB)";
                                parameterNames[13].text = "At� quantos HDs";
                                parameterNames[14].text = "Bateria inclusa?";
                                parameterNames[15].text = "Barramento";
                                break;
            case 5:
                parameterNames[9].text = "Soquete";
                                parameterNames[10].text = "N� n�cleos f�sicos";
                                parameterNames[11].text = "N� n�cleos l�gicos";
                                parameterNames[12].text = "Aceita virtualiza��o?";
                                parameterNames[13].text = "Turbo boost?";
                                parameterNames[14].text = "Hyper-Threading?";
                                break;
            case 6:
                parameterNames[9].text = "Modelo de placa m�e";
                                parameterNames[10].text = "Fonte?";
                                parameterNames[11].text = "Mem�ria?";
                                parameterNames[12].text = "HD?";
                                parameterNames[13].text = "Placa de v�deo?";
                                parameterNames[14].text = "Leitor de DVD?";
                                break;
            case 7:
                parameterNames[9].text = "Watts de pot�ncia";
                                parameterNames[10].text = "Onde funciona?";
                                parameterNames[11].text = "Conectores";
                                break;
            case 8:
                parameterNames[9].text = "Quantas entradas";
                                parameterNames[10].text = "Capacidade m�x de cada porta (MB/s)";
                                break;
            case 9:
                parameterNames[9].text = "Wireless?";
                                parameterNames[10].text = "Quantas entradas?";
                                parameterNames[11].text = "Banda m�x (MB/s)";
                                break;
            case 10:
                parameterNames[9].text = "Onde funciona?";
                                parameterNames[10].text = "Voltagem de sa�da";
                                parameterNames[11].text = "Amperagem de sa�da (mA)";
                                break;
            case 11:
                parameterNames[9].text = "Onde funciona?";
                                parameterNames[10].text = "Voltagem de sa�da";
                                parameterNames[11].text = "Amperagem de sa�da (A)";
                                break;
            case 12:
                parameterNames[9].text = "Tamanho dos HDs";
                                parameterNames[10].text = "Tipos de RAID";
                                parameterNames[11].text = "Tipo de HD";
                                parameterNames[12].text = "Capacidade m�x do HD";
                                parameterNames[13].text = "At� quantos HDs";
                break;
            case 13:
                parameterNames[9].text = "Desempenho m�x (GB/s)";
                                break;
            case 14:
                parameterNames[9].text = "Quantas entradas?";
                                parameterNames[10].text = "Quais entradas?";
                                break;
            case 15:
                parameterNames[9].text = "Quantos canais?";
                                break;
            case 16:
                parameterNames[9].text = "Quantas entradas?";
                                break;

            default:
                break;
        }
    }

    public void AddItem()
    {

    }

    public void RemoveItem()
    {

    }

    public void ReturnToPreviousScreen()
    {
        SceneManager.LoadScene("InitialScene");
    }
}
