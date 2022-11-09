using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class SearchCategoryDropDownHandler : MonoBehaviour
{
    [SerializeField] private TMP_InputField[] searchParamenters;
    private void Start()
    {
        HandleInputData(0);
    }

    public void HandleInputData(int value)
    {
        foreach (var item in searchParamenters)
        {
            item.gameObject.SetActive(true);
        }
        switch (value)
        {
            case 0:
                searchParamenters[3].placeholder.GetComponent<TMP_Text>().text = "Interface...";
                searchParamenters[4].placeholder.GetComponent<TMP_Text>().text = "Tamanho...";
                searchParamenters[5].placeholder.GetComponent<TMP_Text>().text = "Forma de armazenamento...";
                searchParamenters[6].placeholder.GetComponent<TMP_Text>().text = "Capacidade (GB)...";
                searchParamenters[7].placeholder.GetComponent<TMP_Text>().text = "RPM...";
                searchParamenters[8].placeholder.GetComponent<TMP_Text>().text = "Velocidade de Leitura (Gb/s)...";
                searchParamenters[9].placeholder.GetComponent<TMP_Text>().text = "Enterprise?...";
                searchParamenters[10].gameObject.SetActive(false);
                searchParamenters[11].gameObject.SetActive(false);
                break;
            case 1:
                searchParamenters[3].placeholder.GetComponent<TMP_Text>().text = "Tipo...";
                searchParamenters[4].placeholder.GetComponent<TMP_Text>().text = "Capacidade (GB)...";
                searchParamenters[5].placeholder.GetComponent<TMP_Text>().text = "Velocidade (MHz)...";
                searchParamenters[6].placeholder.GetComponent<TMP_Text>().text = "É low voltage?...";
                searchParamenters[7].placeholder.GetComponent<TMP_Text>().text = "Rank...";
                searchParamenters[8].placeholder.GetComponent<TMP_Text>().text = "DIMM...";
                searchParamenters[9].placeholder.GetComponent<TMP_Text>().text = "Taxa de transmissão...";
                searchParamenters[10].placeholder.GetComponent<TMP_Text>().text = "Símbolo...";
                searchParamenters[11].gameObject.SetActive(false);
                break;
            case 2:
                searchParamenters[3].placeholder.GetComponent<TMP_Text>().text = "Interface...";
                searchParamenters[4].placeholder.GetComponent<TMP_Text>().text = "Quantas portas?...";
                searchParamenters[5].placeholder.GetComponent<TMP_Text>().text = "Quais portas?...";
                searchParamenters[6].placeholder.GetComponent<TMP_Text>().text = "Suporta fibra óptica...";
                searchParamenters[7].placeholder.GetComponent<TMP_Text>().text = "Desempenho (MB/s)...";
                searchParamenters[8].gameObject.SetActive(false);
                searchParamenters[9].gameObject.SetActive(false);
                searchParamenters[10].gameObject.SetActive(false);
                searchParamenters[11].gameObject.SetActive(false);
                break;
            case 3:
                searchParamenters[3].placeholder.GetComponent<TMP_Text>().text = "Porta...";
                searchParamenters[4].placeholder.GetComponent<TMP_Text>().text = "Velocidade (GB/s)...";
                searchParamenters[5].placeholder.GetComponent<TMP_Text>().text = "Enstrada SD...";
                searchParamenters[6].placeholder.GetComponent<TMP_Text>().text = "Servidores suportados...";
                searchParamenters[7].gameObject.SetActive(false);
                searchParamenters[8].gameObject.SetActive(false);
                searchParamenters[9].gameObject.SetActive(false);
                searchParamenters[10].gameObject.SetActive(false);
                searchParamenters[11].gameObject.SetActive(false);
                break;
            case 4:
                searchParamenters[3].placeholder.GetComponent<TMP_Text>().text = "Tipo de conexão...";
                searchParamenters[4].placeholder.GetComponent<TMP_Text>().text = "Quantas portas?...";
                searchParamenters[5].placeholder.GetComponent<TMP_Text>().text = "Tipos de RAID...";
                searchParamenters[6].placeholder.GetComponent<TMP_Text>().text = "Capacidade máx do HD (TB)...";
                searchParamenters[7].placeholder.GetComponent<TMP_Text>().text = "Até quantos HDs...";
                searchParamenters[8].placeholder.GetComponent<TMP_Text>().text = "Bateria inclusa?...";
                searchParamenters[9].placeholder.GetComponent<TMP_Text>().text = "Barramento...";
                searchParamenters[10].gameObject.SetActive(false);
                searchParamenters[11].gameObject.SetActive(false);
                break;
            case 5:
                searchParamenters[3].placeholder.GetComponent<TMP_Text>().text = "Soquete...";
                searchParamenters[4].placeholder.GetComponent<TMP_Text>().text = "Nº núcleos físicos...";
                searchParamenters[5].placeholder.GetComponent<TMP_Text>().text = "Nº núcleos lógicos...";
                searchParamenters[6].placeholder.GetComponent<TMP_Text>().text = "Aceita virtualização?...";
                searchParamenters[7].placeholder.GetComponent<TMP_Text>().text = "Turbo boost?...";
                searchParamenters[8].placeholder.GetComponent<TMP_Text>().text = "Hyper-Threading?...";
                searchParamenters[9].gameObject.SetActive(false);
                searchParamenters[10].gameObject.SetActive(false);
                searchParamenters[11].gameObject.SetActive(false);
                break;
            case 6:
                searchParamenters[3].placeholder.GetComponent<TMP_Text>().text = "Modelo de placa mãe...";
                searchParamenters[4].placeholder.GetComponent<TMP_Text>().text = "Fonte?...";
                searchParamenters[5].placeholder.GetComponent<TMP_Text>().text = "Memória?...";
                searchParamenters[6].placeholder.GetComponent<TMP_Text>().text = "HD?...";
                searchParamenters[7].placeholder.GetComponent<TMP_Text>().text = "Placa de vídeo?...";
                searchParamenters[8].placeholder.GetComponent<TMP_Text>().text = "Leitor de DVD?...";
                searchParamenters[9].gameObject.SetActive(false);
                searchParamenters[10].gameObject.SetActive(false);
                searchParamenters[11].gameObject.SetActive(false);
                break;
            case 7:
                searchParamenters[3].placeholder.GetComponent<TMP_Text>().text = "Watts de potência...";
                searchParamenters[4].placeholder.GetComponent<TMP_Text>().text = "Onde funciona?...";
                searchParamenters[5].placeholder.GetComponent<TMP_Text>().text = "Conectores...";
                searchParamenters[6].gameObject.SetActive(false);
                searchParamenters[7].gameObject.SetActive(false);
                searchParamenters[8].gameObject.SetActive(false);
                searchParamenters[9].gameObject.SetActive(false);
                searchParamenters[10].gameObject.SetActive(false);
                searchParamenters[11].gameObject.SetActive(false);
                break;
            case 8:
                searchParamenters[3].placeholder.GetComponent<TMP_Text>().text = "Quantas entradas...";
                searchParamenters[4].placeholder.GetComponent<TMP_Text>().text = "Capacidade máx de cada porta (MB/s)...";
                searchParamenters[5].gameObject.SetActive(false);
                searchParamenters[6].gameObject.SetActive(false);
                searchParamenters[7].gameObject.SetActive(false);
                searchParamenters[8].gameObject.SetActive(false);
                searchParamenters[9].gameObject.SetActive(false);
                searchParamenters[10].gameObject.SetActive(false);
                searchParamenters[11].gameObject.SetActive(false);
                break;
            case 9:
                searchParamenters[3].placeholder.GetComponent<TMP_Text>().text = "Wireless?...";
                searchParamenters[4].placeholder.GetComponent<TMP_Text>().text = "Quantas entradas?...";
                searchParamenters[5].placeholder.GetComponent<TMP_Text>().text = "Banda máx (MB/s)...";
                searchParamenters[6].gameObject.SetActive(false);
                searchParamenters[7].gameObject.SetActive(false);
                searchParamenters[8].gameObject.SetActive(false);
                searchParamenters[9].gameObject.SetActive(false);
                searchParamenters[10].gameObject.SetActive(false);
                searchParamenters[11].gameObject.SetActive(false);
                break;
            case 10:
                searchParamenters[3].placeholder.GetComponent<TMP_Text>().text = "Onde funciona?...";
                searchParamenters[4].placeholder.GetComponent<TMP_Text>().text = "Voltagem de saída...";
                searchParamenters[5].placeholder.GetComponent<TMP_Text>().text = "Amperagem de saída (mA)...";
                searchParamenters[6].gameObject.SetActive(false);
                searchParamenters[7].gameObject.SetActive(false);
                searchParamenters[8].gameObject.SetActive(false);
                searchParamenters[9].gameObject.SetActive(false);
                searchParamenters[10].gameObject.SetActive(false);
                searchParamenters[11].gameObject.SetActive(false);
                break;
            case 11:
                searchParamenters[3].placeholder.GetComponent<TMP_Text>().text = "Onde funciona?...";
                searchParamenters[4].placeholder.GetComponent<TMP_Text>().text = "Voltagem de saída...";
                searchParamenters[5].placeholder.GetComponent<TMP_Text>().text = "Amperagem de saída (A)...";
                searchParamenters[6].gameObject.SetActive(false);
                searchParamenters[7].gameObject.SetActive(false);
                searchParamenters[8].gameObject.SetActive(false);
                searchParamenters[9].gameObject.SetActive(false);
                searchParamenters[10].gameObject.SetActive(false);
                searchParamenters[11].gameObject.SetActive(false);
                break;
            case 12:
                searchParamenters[3].placeholder.GetComponent<TMP_Text>().text = "Tamanho dos HDs?...";
                searchParamenters[4].placeholder.GetComponent<TMP_Text>().text = "Tipos de RAID...";
                searchParamenters[5].placeholder.GetComponent<TMP_Text>().text = "Tipo de HD...";
                searchParamenters[6].placeholder.GetComponent<TMP_Text>().text = "Capacidade máx do HD...";
                searchParamenters[7].placeholder.GetComponent<TMP_Text>().text = "Até quantos HDs...";
                searchParamenters[8].gameObject.SetActive(false);
                searchParamenters[9].gameObject.SetActive(false);
                searchParamenters[10].gameObject.SetActive(false);
                searchParamenters[11].gameObject.SetActive(false);
                break;
            case 13:
                searchParamenters[3].placeholder.GetComponent<TMP_Text>().text = "Desempenho máx (GB/s)...";
                searchParamenters[4].gameObject.SetActive(false);
                searchParamenters[5].gameObject.SetActive(false);
                searchParamenters[6].gameObject.SetActive(false);
                searchParamenters[7].gameObject.SetActive(false);
                searchParamenters[8].gameObject.SetActive(false);
                searchParamenters[9].gameObject.SetActive(false);
                searchParamenters[10].gameObject.SetActive(false);
                searchParamenters[11].gameObject.SetActive(false);
                break;
            case 14:
                searchParamenters[3].placeholder.GetComponent<TMP_Text>().text = "Quantas entradas?...";
                searchParamenters[4].placeholder.GetComponent<TMP_Text>().text = "Quais entradas?...";
                searchParamenters[5].gameObject.SetActive(false);
                searchParamenters[6].gameObject.SetActive(false);
                searchParamenters[7].gameObject.SetActive(false);
                searchParamenters[8].gameObject.SetActive(false);
                searchParamenters[9].gameObject.SetActive(false);
                searchParamenters[10].gameObject.SetActive(false);
                searchParamenters[11].gameObject.SetActive(false);
                break;
            case 15:
                searchParamenters[3].placeholder.GetComponent<TMP_Text>().text = "Quantos canais?...";
                searchParamenters[4].gameObject.SetActive(false);
                searchParamenters[5].gameObject.SetActive(false);
                searchParamenters[6].gameObject.SetActive(false);
                searchParamenters[7].gameObject.SetActive(false);
                searchParamenters[8].gameObject.SetActive(false);
                searchParamenters[9].gameObject.SetActive(false);
                searchParamenters[10].gameObject.SetActive(false);
                searchParamenters[11].gameObject.SetActive(false);
                break;
            case 16:
                searchParamenters[3].placeholder.GetComponent<TMP_Text>().text = "Quantas entradas?...";
                searchParamenters[4].gameObject.SetActive(false);
                searchParamenters[5].gameObject.SetActive(false);
                searchParamenters[6].gameObject.SetActive(false);
                searchParamenters[7].gameObject.SetActive(false);
                searchParamenters[8].gameObject.SetActive(false);
                searchParamenters[9].gameObject.SetActive(false);
                searchParamenters[10].gameObject.SetActive(false);
                searchParamenters[11].gameObject.SetActive(false);
                break;
            case 17:
                break;
            case 18:
                break;
            case 19:
                break;
            case 20:
                break;
            default:
                break;
        }
    }
}
