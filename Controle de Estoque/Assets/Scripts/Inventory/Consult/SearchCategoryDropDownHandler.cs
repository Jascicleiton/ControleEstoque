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

    /// <summary>
    /// Handles the inputs placeholder texts for each category
    /// </summary>
    public void HandleInputData(int value)
    {
        foreach (var item in searchParamenters)
        {
            item.gameObject.SetActive(true);
        }
        switch (value)
        {
            #region Adaptador AC
            case 0:
                searchParamenters[3].placeholder.GetComponent<TMP_Text>().text = "Onde funciona?...";
                searchParamenters[4].placeholder.GetComponent<TMP_Text>().text = "Voltagem de sa�da...";
                searchParamenters[5].placeholder.GetComponent<TMP_Text>().text = "Amperagem de sa�da (A)...";
                searchParamenters[6].gameObject.SetActive(false);
                searchParamenters[7].gameObject.SetActive(false);
                searchParamenters[8].gameObject.SetActive(false);
                searchParamenters[9].gameObject.SetActive(false);
                searchParamenters[10].gameObject.SetActive(false);
                searchParamenters[11].gameObject.SetActive(false);
                break;
            #endregion
            #region Carregador
            case 1:
                searchParamenters[3].placeholder.GetComponent<TMP_Text>().text = "Onde funciona?...";
                searchParamenters[4].placeholder.GetComponent<TMP_Text>().text = "Voltagem de sa�da...";
                searchParamenters[5].placeholder.GetComponent<TMP_Text>().text = "Amperagem de sa�da (mA)...";
                searchParamenters[6].gameObject.SetActive(false);
                searchParamenters[7].gameObject.SetActive(false);
                searchParamenters[8].gameObject.SetActive(false);
                searchParamenters[9].gameObject.SetActive(false);
                searchParamenters[10].gameObject.SetActive(false);
                searchParamenters[11].gameObject.SetActive(false);
                break;
            #endregion
            #region Desktop
            case 2:
                searchParamenters[3].placeholder.GetComponent<TMP_Text>().text = "Modelo de placa m�e...";
                searchParamenters[4].placeholder.GetComponent<TMP_Text>().text = "Fonte?...";
                searchParamenters[5].placeholder.GetComponent<TMP_Text>().text = "Mem�ria?...";
                searchParamenters[6].placeholder.GetComponent<TMP_Text>().text = "HD?...";
                searchParamenters[7].placeholder.GetComponent<TMP_Text>().text = "Placa de v�deo?...";
                searchParamenters[8].placeholder.GetComponent<TMP_Text>().text = "Leitor de DVD?...";
                searchParamenters[9].placeholder.GetComponent<TMP_Text>().text = "Processador...";
                searchParamenters[10].gameObject.SetActive(false);
                searchParamenters[11].gameObject.SetActive(false);
                break;
            #endregion
            #region Fonte
            case 3:
                searchParamenters[3].placeholder.GetComponent<TMP_Text>().text = "Watts de pot�ncia...";
                searchParamenters[4].placeholder.GetComponent<TMP_Text>().text = "Onde funciona?...";
                searchParamenters[5].placeholder.GetComponent<TMP_Text>().text = "Conectores...";
                searchParamenters[6].gameObject.SetActive(false);
                searchParamenters[7].gameObject.SetActive(false);
                searchParamenters[8].gameObject.SetActive(false);
                searchParamenters[9].gameObject.SetActive(false);
                searchParamenters[10].gameObject.SetActive(false);
                searchParamenters[11].gameObject.SetActive(false);
                break;
            #endregion
            #region GBIC
            case 4:
                searchParamenters[3].placeholder.GetComponent<TMP_Text>().text = "Desempenho m�x (GB/s)...";
                searchParamenters[4].gameObject.SetActive(false);
                searchParamenters[5].gameObject.SetActive(false);
                searchParamenters[6].gameObject.SetActive(false);
                searchParamenters[7].gameObject.SetActive(false);
                searchParamenters[8].gameObject.SetActive(false);
                searchParamenters[9].gameObject.SetActive(false);
                searchParamenters[10].gameObject.SetActive(false);
                searchParamenters[11].gameObject.SetActive(false);
                break;
            #endregion
                        #region HD
            case 5:
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
            #endregion
            #region iDrac
            case 6:
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
            #endregion
            #region Mem�ria
            case 7:
                searchParamenters[3].placeholder.GetComponent<TMP_Text>().text = "Tipo...";
                searchParamenters[4].placeholder.GetComponent<TMP_Text>().text = "Capacidade (GB)...";
                searchParamenters[5].placeholder.GetComponent<TMP_Text>().text = "Velocidade (MHz)...";
                searchParamenters[6].placeholder.GetComponent<TMP_Text>().text = "� low voltage?...";
                searchParamenters[7].placeholder.GetComponent<TMP_Text>().text = "Rank...";
                searchParamenters[8].placeholder.GetComponent<TMP_Text>().text = "DIMM...";
                searchParamenters[9].placeholder.GetComponent<TMP_Text>().text = "Taxa de transmiss�o...";
                searchParamenters[10].placeholder.GetComponent<TMP_Text>().text = "S�mbolo...";
                searchParamenters[11].gameObject.SetActive(false);
                break;
            #endregion
            #region Monitor
            case 8:
                searchParamenters[3].placeholder.GetComponent<TMP_Text>().text = "Polegadas...";
                searchParamenters[4].placeholder.GetComponent<TMP_Text>().text = "Quais entradas?...";
                searchParamenters[5].gameObject.SetActive(false);
                searchParamenters[6].gameObject.SetActive(false);
                searchParamenters[7].gameObject.SetActive(false);
                searchParamenters[8].gameObject.SetActive(false);
                searchParamenters[9].gameObject.SetActive(false);
                searchParamenters[10].gameObject.SetActive(false);
                searchParamenters[11].gameObject.SetActive(false);
                break;
            #endregion
            #region Notebook
            case 9:
                searchParamenters[3].gameObject.SetActive(false);
                searchParamenters[4].gameObject.SetActive(false);
                searchParamenters[5].gameObject.SetActive(false);
                searchParamenters[6].gameObject.SetActive(false);
                searchParamenters[7].gameObject.SetActive(false);
                searchParamenters[8].gameObject.SetActive(false);
                searchParamenters[9].gameObject.SetActive(false);
                searchParamenters[10].gameObject.SetActive(false);
                searchParamenters[11].gameObject.SetActive(false);
                break;
            #endregion
            #region Placa controladora
            case 10:
                searchParamenters[3].placeholder.GetComponent<TMP_Text>().text = "Tipo de conex�o...";
                searchParamenters[4].placeholder.GetComponent<TMP_Text>().text = "Quantas portas?...";
                searchParamenters[5].placeholder.GetComponent<TMP_Text>().text = "Tipos de RAID...";
                searchParamenters[6].placeholder.GetComponent<TMP_Text>().text = "Capacidade m�x do HD (TB)...";
                searchParamenters[7].placeholder.GetComponent<TMP_Text>().text = "At� quantos HDs...";
                searchParamenters[8].placeholder.GetComponent<TMP_Text>().text = "Bateria inclusa?...";
                searchParamenters[9].placeholder.GetComponent<TMP_Text>().text = "Barramento...";
                searchParamenters[10].gameObject.SetActive(false);
                searchParamenters[11].gameObject.SetActive(false);
                break;
            #endregion
            #region Placa de captura de v�deo
            case 11:
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
            #endregion
            #region Placa de rede
            case 12:
                searchParamenters[3].placeholder.GetComponent<TMP_Text>().text = "Interface...";
                searchParamenters[4].placeholder.GetComponent<TMP_Text>().text = "Quantas portas?...";
                searchParamenters[5].placeholder.GetComponent<TMP_Text>().text = "Quais portas?...";
                searchParamenters[6].placeholder.GetComponent<TMP_Text>().text = "Suporta fibra �ptica...";
                searchParamenters[7].placeholder.GetComponent<TMP_Text>().text = "Desempenho (MB/s)...";
                searchParamenters[8].gameObject.SetActive(false);
                searchParamenters[9].gameObject.SetActive(false);
                searchParamenters[10].gameObject.SetActive(false);
                searchParamenters[11].gameObject.SetActive(false);
                break;
            #endregion
            #region Placa de som
            case 13:
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
            #endregion
            #region Placa de v�deo
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
            #endregion          
            #region Processador
            case 15:
                searchParamenters[3].placeholder.GetComponent<TMP_Text>().text = "Soquete...";
                searchParamenters[4].placeholder.GetComponent<TMP_Text>().text = "N� n�cleos f�sicos...";
                searchParamenters[5].placeholder.GetComponent<TMP_Text>().text = "N� n�cleos l�gicos...";
                searchParamenters[6].placeholder.GetComponent<TMP_Text>().text = "Aceita virtualiza��o?...";
                searchParamenters[7].placeholder.GetComponent<TMP_Text>().text = "Turbo boost?...";
                searchParamenters[8].placeholder.GetComponent<TMP_Text>().text = "Hyper-Threading?...";
                searchParamenters[9].gameObject.SetActive(false);
                searchParamenters[10].gameObject.SetActive(false);
                searchParamenters[11].gameObject.SetActive(false);
                break;
            #endregion
            #region Roteador
            case 16:
                searchParamenters[3].placeholder.GetComponent<TMP_Text>().text = "Wireless?...";
                searchParamenters[4].placeholder.GetComponent<TMP_Text>().text = "Quantas entradas?...";
                searchParamenters[5].placeholder.GetComponent<TMP_Text>().text = "Banda m�x (MB/s)...";
                searchParamenters[6].gameObject.SetActive(false);
                searchParamenters[7].gameObject.SetActive(false);
                searchParamenters[8].gameObject.SetActive(false);
                searchParamenters[9].gameObject.SetActive(false);
                searchParamenters[10].gameObject.SetActive(false);
                searchParamenters[11].gameObject.SetActive(false);
                break;
            #endregion
            #region Servidor
            case 17:
                searchParamenters[3].gameObject.SetActive(false);
                searchParamenters[4].gameObject.SetActive(false);
                searchParamenters[5].gameObject.SetActive(false);
                searchParamenters[6].gameObject.SetActive(false);
                searchParamenters[7].gameObject.SetActive(false);
                searchParamenters[8].gameObject.SetActive(false);
                searchParamenters[9].gameObject.SetActive(false);
                searchParamenters[10].gameObject.SetActive(false);
                searchParamenters[11].gameObject.SetActive(false);
                break;
            #endregion
            #region Storage NAS
            case 18:
                searchParamenters[3].placeholder.GetComponent<TMP_Text>().text = "Tamanho dos HDs?...";
                searchParamenters[4].placeholder.GetComponent<TMP_Text>().text = "Tipos de RAID...";
                searchParamenters[5].placeholder.GetComponent<TMP_Text>().text = "Tipo de HD...";
                searchParamenters[6].placeholder.GetComponent<TMP_Text>().text = "Capacidade m�x do HD...";
                searchParamenters[7].placeholder.GetComponent<TMP_Text>().text = "At� quantos HDs...";
                searchParamenters[8].gameObject.SetActive(false);
                searchParamenters[9].gameObject.SetActive(false);
                searchParamenters[10].gameObject.SetActive(false);
                searchParamenters[11].gameObject.SetActive(false);
                break;
            #endregion
            #region Switch
            case 19:
                searchParamenters[3].placeholder.GetComponent<TMP_Text>().text = "Quantas entradas...";
                searchParamenters[4].placeholder.GetComponent<TMP_Text>().text = "Capacidade m�x de cada porta (MB/s)...";
                searchParamenters[5].gameObject.SetActive(false);
                searchParamenters[6].gameObject.SetActive(false);
                searchParamenters[7].gameObject.SetActive(false);
                searchParamenters[8].gameObject.SetActive(false);
                searchParamenters[9].gameObject.SetActive(false);
                searchParamenters[10].gameObject.SetActive(false);
                searchParamenters[11].gameObject.SetActive(false);
                break;
            #endregion






            #region 
            case 20:
                break;
            #endregion
            default:
                break;
        }
    }
}
