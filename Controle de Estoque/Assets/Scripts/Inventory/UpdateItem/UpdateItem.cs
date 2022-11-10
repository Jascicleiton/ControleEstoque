using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class UpdateItem : MonoBehaviour
{
    [SerializeField] TMP_Dropdown categoryDP;
    /// <summary>
    /// 1 = Patrim�nio, 3 = Serial
    /// </summary>
    [SerializeField] TMP_InputField[] inputs;
    [SerializeField] TMP_Text[] placeholders;

    SheetColumns itemToShow;
    // Start is called before the first frame update
    void Start()
    {

    }

    private void Update()
    {
        ShowHideInputs();
    }

    private void ShowHideInputs()
    {
        foreach (var item in inputs)
        {
            item.gameObject.SetActive(true);
        }

        switch (categoryDP.value)
        {
            case 0:
                placeholders[0].text = "Interface";
                placeholders[1].text = "Tamanho";
                placeholders[2].text = "Forma de armazenamento";
                placeholders[3].text = "Capacidade (GB)";
                placeholders[4].text = "RPM";
                placeholders[5].text = "Velocidade de Leitura (Gb/s)";
                placeholders[6].text = "Enterprise";
                placeholders[7].text = "";
                break;
            case 1:
                placeholders[0].text = "Tipo";
                placeholders[1].text = "Capacidade (GB)";
                placeholders[2].text = "Velocidade (MHz)";
                placeholders[3].text = "� low voltage?";
                placeholders[4].text = "Rank";
                placeholders[5].text = "DIMM";
                placeholders[6].text = "Taxa de transmiss�o";
                placeholders[7].text = "S�mbolo";
                break;
            case 2:
                placeholders[0].text = "Interface";
                placeholders[1].text = "Quantas portas?";
                placeholders[2].text = "Quais portas?";
                placeholders[3].text = "Suporta fibra �ptica?";
                placeholders[4].text = "Desempenho (MB/s)";
                placeholders[5].text = "";
                placeholders[6].text = "";
                placeholders[7].text = "";
                break;
            case 3:
                placeholders[0].text = "Porta";
                placeholders[1].text = "Velocidade (GB/s)";
                placeholders[2].text = "Entrada SD";
                placeholders[3].text = "Servidores suportados";
                placeholders[4].text = "";
                placeholders[5].text = "";
                placeholders[6].text = "";
                placeholders[7].text = "";
                break;
            case 4:
                placeholders[0].text = "Tipo de conex�o";
                placeholders[1].text = "Quantas portas?";
                placeholders[2].text = "Tipos de RAID";
                placeholders[3].text = "Capacidade m�x do HD (TB)";
                placeholders[4].text = "At� quantos HDs";
                placeholders[5].text = "Bateria inclusa?";
                placeholders[6].text = "Barramento";
                placeholders[7].text = "";
                break;
            case 5:
                placeholders[0].text = "Soquete";
                placeholders[1].text = "N� n�cleos f�sicos";
                placeholders[2].text = "N� n�cleos l�gicos";
                placeholders[3].text = "Aceita virtualiza��o?";
                placeholders[4].text = "Turbo boost?";
                placeholders[5].text = "Hyper-Threading?";
                placeholders[6].text = "";
                placeholders[7].text = "";
                break;
            case 6:
                placeholders[0].text = "Modelo de placa m�e";
                placeholders[1].text = "Fonte?";
                placeholders[2].text = "Mem�ria?";
                placeholders[3].text = "HD?";
                placeholders[4].text = "Placa de v�deo?";
                placeholders[5].text = "Leitor de DVD?";
                placeholders[6].text = "";
                placeholders[7].text = "";
                break;
            case 7:
                placeholders[0].text = "Watts de pot�ncia";
                placeholders[1].text = "Onde funciona?";
                placeholders[2].text = "Conectores";
                placeholders[3].text = "";
                placeholders[4].text = "";
                placeholders[5].text = "";
                placeholders[6].text = "";
                placeholders[7].text = "";
                break;
            case 8:
                placeholders[0].text = "Quantas entradas";
                placeholders[1].text = "Capacidade m�x de cada porta (MB/s)";
                placeholders[2].text = "";
                placeholders[3].text = "";
                placeholders[4].text = "";
                placeholders[5].text = "";
                placeholders[6].text = "";
                placeholders[7].text = "";
                break;
            case 9:
                placeholders[0].text = "Wireless?";
                placeholders[1].text = "Quantas entradas?";
                placeholders[2].text = "Banda m�x (MB/s)";
                placeholders[3].text = "";
                placeholders[4].text = "";
                placeholders[5].text = "";
                placeholders[6].text = "";
                placeholders[7].text = "";
                break;
            case 10:
                placeholders[0].text = "Onde funciona?";
                placeholders[1].text = "Voltagem de sa�da";
                placeholders[2].text = "Amperagem de sa�da (mA)";
                placeholders[3].text = "";
                placeholders[4].text = "";
                placeholders[5].text = "";
                placeholders[6].text = "";
                placeholders[7].text = "";
                break;
            case 11:
                placeholders[0].text = "Onde funciona?";
                placeholders[1].text = "Voltagem de sa�da";
                placeholders[2].text = "Amperagem de sa�da (A)";
                placeholders[3].text = "";
                placeholders[4].text = "";
                placeholders[5].text = "";
                placeholders[6].text = "";
                placeholders[7].text = "";
                break;
            case 12:
                placeholders[0].text = "Tamanho dos HDs";
                placeholders[1].text = "Tipos de RAID";
                placeholders[2].text = "Tipo de HD";
                placeholders[3].text = "Capacidade m�x do HD";
                placeholders[4].text = "At� quantos HDs";
                                placeholders[5].text = "";
                placeholders[6].text = "";
                placeholders[7].text = "";
                break;
            case 13:
                placeholders[0].text = "Desempenho m�x (GB/s)";
                placeholders[1].text = "";
                placeholders[2].text = "";
                placeholders[3].text = "";
                placeholders[4].text = "";
                placeholders[5].text = "";
                placeholders[6].text = "";
                placeholders[7].text = "";
                break;
            case 14:
                placeholders[0].text = "Quantas entradas?";
                placeholders[1].text = "Quais entradas?";
                placeholders[2].text = "";
                placeholders[3].text = "";
                placeholders[4].text = "";
                placeholders[5].text = "";
                placeholders[6].text = "";
                placeholders[7].text = "";
                break;
            case 15:
                placeholders[0].text = "Quantos canais?";
                placeholders[1].text = "";
                placeholders[2].text = "";
                placeholders[3].text = "";
                placeholders[4].text = "";
                placeholders[5].text = "";
                placeholders[6].text = "";
                placeholders[7].text = "";
                break;
            case 16:
                placeholders[0].text = "Quantas entradas?";
                placeholders[1].text = "";
                placeholders[2].text = "";
                placeholders[3].text = "";
                placeholders[4].text = "";
                placeholders[5].text = "";
                placeholders[6].text = "";
                placeholders[7].text = "";
                break;

            default:
                break;
        }

        for (int i = 0; i < placeholders.Length; i++)
        {
            if (placeholders[i] != null && placeholders[i].text == "")
            {
                inputs[i+10].gameObject.SetActive(false);
            }
        }
    }
}
