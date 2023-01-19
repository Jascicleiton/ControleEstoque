using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using TMPro;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class AddRemoveItem : MonoBehaviour
{
    [SerializeField] private TMP_Text[] parameterNames;
    [SerializeField] private TMP_InputField[] parameterValues;
    [SerializeField] private GameObject[] itensToShow;
    [SerializeField] private TMP_Dropdown categoryDP;
    [SerializeField] private Button resetButton;
    [SerializeField] private Button returnButton;
    [SerializeField] private Button addButton;
    [SerializeField] private Button addDetailsButton;

    [SerializeField] GameObject messagePanel;
    [SerializeField] TMP_Text messageText;
    private List<string> parameters = new List<string>();

    private void Start()
    {
        UpdateNames();
    }

    private void OnEnable()
    {
        EventHandler.MessageClosed += MessageClosed;
        EventHandler.EnableInput += SetInputEnabled;
    }

    private void OnDisable()
    {
        EventHandler.MessageClosed -= MessageClosed;
        EventHandler.EnableInput -= SetInputEnabled;
    }

    private void SetInputEnabled(bool inputEnabled)
    {
        for (int i = 0; i < parameterValues.Length; i++)
        {
            if (parameterValues[i].gameObject.activeInHierarchy)
            {
                parameterValues[i].interactable = inputEnabled;
            }
        }
        categoryDP.interactable = inputEnabled;
        resetButton.interactable = inputEnabled;
        returnButton.interactable = inputEnabled;
        addButton.interactable = inputEnabled;
        addDetailsButton.interactable = inputEnabled;
    }

    /// <summary>
    /// Updates all the placeholder text according to the category selected
    /// </summary>
    private void UpdateNames()
    {
        for (int i = 0; i < itensToShow.Length; i++)
        {
            itensToShow[i].SetActive(true);
            parameterValues[i].text = "";
        }

        parameterNames[0].text = "Aquisição";
        parameterValues[0].text = DateTime.Now.ToString("dd/MM/yyyy");
        parameterNames[1].text = "Entrada no estoque";
        parameterValues[1].text = DateTime.Now.ToString("dd/MM/yyyy");
        parameterNames[2].text = "Patrimônio";
        parameterNames[3].text = "Status";
        parameterNames[4].text = "Serial";
        parameterNames[5].text = "Fabricante";
        parameterNames[6].text = "Modelo";
        parameterNames[7].text = "Local";
        parameterNames[8].text = "Observação";

        switch (categoryDP.value)
        {
            case 0: // Adaptador AC
                parameterNames[9].text = "Onde funciona?";
                parameterNames[10].text = "Voltagem de saída";
                parameterNames[11].text = "Amperagem de saída (A)";
                parameterNames[12].text = "";
                parameterNames[13].text = "";
                parameterNames[14].text = "";
                parameterNames[15].text = "";
                parameterNames[16].text = "";
                break;
            case 1: // Carregador
                parameterNames[9].text = "Onde funciona?";
                parameterNames[10].text = "Voltagem de saída";
                parameterNames[11].text = "Amperagem de saída (mA)";
                parameterNames[12].text = "";
                parameterNames[13].text = "";
                parameterNames[14].text = "";
                parameterNames[15].text = "";
                parameterNames[16].text = "";
                break;
            case 2: // Desktop
                parameterNames[9].text = "Modelo de placa mãe";
                parameterNames[10].text = "Fonte?";
                parameterNames[11].text = "Memória?";
                parameterNames[12].text = "HD?";
                parameterNames[13].text = "Placa de vídeo?";
                parameterNames[14].text = "Placa de rede?";
                parameterNames[15].text = "Leitor de DVD?";
                parameterNames[16].text = "Processador?";
                break;
            case 3: // Fone para ramal
                parameterNames[9].text = "";
                parameterNames[10].text = "";
                parameterNames[11].text = "";
                parameterNames[12].text = "";
                parameterNames[13].text = "";
                parameterNames[14].text = "";
                parameterNames[15].text = "";
                parameterNames[16].text = "";
                break;
            case 4: // Fonte
                parameterNames[9].text = "Watts de potência";
                parameterNames[10].text = "Onde funciona?";
                parameterNames[11].text = "Conectores";
                parameterNames[12].text = "";
                parameterNames[13].text = "";
                parameterNames[14].text = "";
                parameterNames[15].text = "";
                parameterNames[16].text = "";
                break;
            case 5: // Gbic
                parameterNames[9].text = "Desempenho máx (GB/s)";
                parameterNames[10].text = "";
                parameterNames[11].text = "";
                parameterNames[12].text = "";
                parameterNames[13].text = "";
                parameterNames[14].text = "";
                parameterNames[15].text = "";
                parameterNames[16].text = "";
                break;
            case 6: // HD
                parameterNames[9].text = "Interface";
                parameterNames[10].text = "Tamanho";
                parameterNames[11].text = "Forma de armazenamento";
                parameterNames[12].text = "Capacidade (GB)";
                parameterNames[13].text = "RPM";
                parameterNames[14].text = "Velocidade de Leitura (Gb/s)";
                parameterNames[15].text = "Enterprise";
                parameterNames[16].text = "";
                break;
            case 7: // idrac
                parameterNames[9].text = "Porta";
                parameterNames[10].text = "Velocidade (GB/s)";
                parameterNames[11].text = "Entrada SD";
                parameterNames[12].text = "Servidores suportados";
                parameterNames[13].text = "";
                parameterNames[14].text = "";
                parameterNames[15].text = "";
                parameterNames[16].text = "";
                break;
            case 8: // Memoria
                parameterNames[9].text = "Tipo";
                parameterNames[10].text = "Capacidade (GB)";
                parameterNames[11].text = "Velocidade (MHz)";
                parameterNames[12].text = "É low voltage?";
                parameterNames[13].text = "Rank";
                parameterNames[14].text = "DIMM";
                parameterNames[15].text = "Taxa de transmissão";
                parameterNames[16].text = "Símbolo";
                break;
            case 9: // Monitor
                parameterNames[9].text = "Polegadas";
                parameterNames[10].text = "Tipos de entradas";
                parameterNames[11].text = "";
                parameterNames[12].text = "";
                parameterNames[13].text = "";
                parameterNames[14].text = "";
                parameterNames[15].text = "";
                parameterNames[16].text = "";
                break;
            case 10: // Mouse
                parameterNames[9].text = "";
                parameterNames[10].text = "";
                parameterNames[11].text = "";
                parameterNames[12].text = "";
                parameterNames[13].text = "";
                parameterNames[14].text = "";
                parameterNames[15].text = "";
                parameterNames[16].text = "";
                break;
            case 11: // Notebook
                parameterNames[9].text = "";
                parameterNames[10].text = "";
                parameterNames[11].text = "";
                parameterNames[12].text = "";
                parameterNames[13].text = "";
                parameterNames[14].text = "";
                parameterNames[15].text = "";
                parameterNames[16].text = "";
                break;
            case 12: // Placa controladora
                parameterNames[9].text = "Tipo de conexão";
                parameterNames[10].text = "Quantas portas?";
                parameterNames[11].text = "Tipos de RAID";
                parameterNames[12].text = "Tipo de HD";
                parameterNames[13].text = "Capacidade máx do HD (TB)";
                parameterNames[14].text = "Até quantos HDs";
                parameterNames[15].text = "Bateria inclusa?";
                parameterNames[16].text = "Barramento";
                break;
            case 13: // Placa de captura de vídeo
                parameterNames[9].text = "Quantas entradas?";
                parameterNames[10].text = "";
                parameterNames[11].text = "";
                parameterNames[12].text = "";
                parameterNames[13].text = "";
                parameterNames[14].text = "";
                parameterNames[15].text = "";
                parameterNames[16].text = "";
                break;
            case 14: // Placa de rede
                parameterNames[9].text = "Interface";
                parameterNames[10].text = "Quantas portas?";
                parameterNames[11].text = "Quais portas?";
                parameterNames[12].text = "Suporta fibra óptica?";
                parameterNames[13].text = "Desempenho (MB/s)";
                parameterNames[14].text = "";
                parameterNames[15].text = "";
                parameterNames[16].text = "";
                break;
            case 15: // Placa de som
                parameterNames[9].text = "Quantos canais?";
                parameterNames[10].text = "";
                parameterNames[11].text = "";
                parameterNames[12].text = "";
                parameterNames[13].text = "";
                parameterNames[14].text = "";
                parameterNames[15].text = "";
                parameterNames[16].text = "";
                break;
            case 16: // Placa de vídeo
                parameterNames[9].text = "Quantas entradas?";
                parameterNames[10].text = "Quais entradas?";
                parameterNames[11].text = "";
                parameterNames[12].text = "";
                parameterNames[13].text = "";
                parameterNames[14].text = "";
                parameterNames[15].text = "";
                parameterNames[16].text = "";
                break;
            case 17: // Processador
                parameterNames[9].text = "Soquete";
                parameterNames[10].text = "Nº núcleos físicos";
                parameterNames[11].text = "Nº núcleos lógicos";
                parameterNames[12].text = "Aceita virtualização?";
                parameterNames[13].text = "Turbo boost?";
                parameterNames[14].text = "Hyper-Threading?";
                parameterNames[15].text = "";
                parameterNames[16].text = "";
                break;
            case 18: // Ramal
                parameterNames[9].text = "";
                parameterNames[10].text = "";
                parameterNames[11].text = "";
                parameterNames[12].text = "";
                parameterNames[13].text = "";
                parameterNames[14].text = "";
                parameterNames[15].text = "";
                parameterNames[16].text = "";
                break;
            case 19: // Roteador
                parameterNames[9].text = "Wireless?";
                parameterNames[10].text = "Quantas entradas?";
                parameterNames[11].text = "Banda máx (MB/s)";
                parameterNames[12].text = "Voltagem";
                parameterNames[13].text = "";
                parameterNames[14].text = "";
                parameterNames[15].text = "";
                parameterNames[16].text = "";
                break;
            case 20: // Servidor
                parameterNames[9].text = "";
                parameterNames[10].text = "";
                parameterNames[11].text = "";
                parameterNames[12].text = "";
                parameterNames[13].text = "";
                parameterNames[14].text = "";
                parameterNames[15].text = "";
                parameterNames[16].text = "";
                break;
            case 21: // Storage NAS
                parameterNames[9].text = "Tamanho dos HDs";
                parameterNames[10].text = "Tipos de RAID";
                parameterNames[11].text = "Tipo de HD";
                parameterNames[12].text = "Capacidade máx do HD";
                parameterNames[13].text = "Até quantos HDs";
                parameterNames[14].text = "";
                parameterNames[15].text = "";
                parameterNames[16].text = "";
                break;

            case 22: // Switch
                parameterNames[9].text = "Quantas entradas";
                parameterNames[10].text = "Capacidade máx de cada porta (MB/s)";
                parameterNames[11].text = "";
                parameterNames[12].text = "";
                parameterNames[13].text = "";
                parameterNames[14].text = "";
                parameterNames[15].text = "";
                parameterNames[16].text = "";
                break;
            case 23: // Teclado
                parameterNames[9].text = "";
                parameterNames[10].text = "";
                parameterNames[11].text = "";
                parameterNames[12].text = "";
                parameterNames[13].text = "";
                parameterNames[14].text = "";
                parameterNames[15].text = "";
                parameterNames[16].text = "";
                break;
            default:
                parameterNames[9].text = "";
                parameterNames[10].text = "";
                parameterNames[11].text = "";
                parameterNames[12].text = "";
                parameterNames[13].text = "";
                parameterNames[14].text = "";
                parameterNames[15].text = "";
                parameterNames[16].text = "";
                break;
        }

        for (int i = 0; i < itensToShow.Length; i++)
        {
            if (parameterNames[i].text == "")
            {
                itensToShow[i].SetActive(false);
            }
        }
    }

    private IEnumerator AddNewItemRoutine(bool addInventario)
    {
#pragma warning disable CS0219 // Variable is assigned but its value is never used
        bool addDetalheSuccess = false;
#pragma warning restore CS0219 // Variable is assigned but its value is never used
        bool addInventarioSuccess = false;
        if (addInventario)
        {
            #region Add new item to Inventario
            parameters.Clear();
            parameters.Add(parameterValues[0].text);
            parameters.Add(parameterValues[1].text);
            parameters.Add(parameterValues[2].text);
            parameters.Add(parameterValues[3].text);
            parameters.Add(parameterValues[4].text);
            parameters.Add(HelperMethods.GetCategoryString(categoryDP.value));
            parameters.Add(parameterValues[5].text);
            parameters.Add(parameterValues[6].text);
            parameters.Add(parameterValues[7].text);
            parameters.Add("");
            parameters.Add(parameterValues[8].text);

            yield return HelperMethods.AddUpdateItem(categoryDP.value, 2, parameters, true);

            if (HelperMethods.GetAddUpdateResponse())
            {
                addInventarioSuccess = true;
                // success
            }
            else
            {
                addInventarioSuccess = false;
                addDetalheSuccess = false;
                // fail
            }

            #endregion
        }
        switch (HelperMethods.GetCategoryString(categoryDP.value))
        {
            #region Adaptador AC
            case ConstStrings.AdaptadorAC:
                parameters.Clear();
                parameters.Add(parameterValues[6].text);
                parameters.Add(parameterValues[9].text);
                parameters.Add(parameterValues[10].text);
                parameters.Add(parameterValues[11].text);
                break;
            #endregion
            #region Carregador
            case ConstStrings.Carregador:
                parameters.Clear();
                parameters.Add(parameterValues[6].text);
                parameters.Add(parameterValues[9].text);
                parameters.Add(parameterValues[10].text);
                parameters.Add(parameterValues[11].text);
                break;
            #endregion
            #region Desktop
            case ConstStrings.Desktop:
                parameters.Clear();
                parameters.Add(parameterValues[2].text);
                parameters.Add(parameterValues[9].text);
                parameters.Add(parameterValues[10].text);
                parameters.Add(parameterValues[11].text);
                parameters.Add(parameterValues[12].text);
                parameters.Add(parameterValues[13].text);
                parameters.Add(parameterValues[14].text);
                parameters.Add(parameterValues[15].text);
                parameters.Add(parameterValues[16].text);
                break;
            #endregion
            #region Fone para ramal
            case ConstStrings.FoneRamal:
                break;
            #endregion
            #region Fonte
            case ConstStrings.Fonte:
                parameters.Clear();
                parameters.Add(parameterValues[6].text);
                parameters.Add(parameterValues[9].text);
                parameters.Add(parameterValues[10].text);
                parameters.Add(parameterValues[11].text);
                break;
            #endregion
            #region GBIC
            case ConstStrings.Gbic:
                parameters.Clear();
                parameters.Add(parameterValues[6].text);
                parameters.Add(parameterValues[5].text);
                parameters.Add(parameterValues[9].text);
                break;
            #endregion
            #region HD
            case ConstStrings.HD:
                parameters.Clear();
                parameters.Add(parameterValues[6].text);
                parameters.Add(parameterValues[5].text);
                parameters.Add(parameterValues[9].text);
                parameters.Add(parameterValues[10].text);
                parameters.Add(parameterValues[11].text);
                parameters.Add(parameterValues[12].text);
                parameters.Add(parameterValues[13].text);
                parameters.Add(parameterValues[14].text);
                parameters.Add(parameterValues[15].text);
                break;
            #endregion
            #region iDrac
            case ConstStrings.Idrac:
                parameters.Clear();
                parameters.Add(parameterValues[6].text);
                parameters.Add(parameterValues[5].text);
                parameters.Add(parameterValues[9].text);
                parameters.Add(parameterValues[10].text);
                parameters.Add(parameterValues[11].text);
                parameters.Add(parameterValues[12].text);
                break;
            #endregion
            #region Memoria
            case ConstStrings.Memoria:
                parameters.Clear();
                parameters.Add(parameterValues[6].text);
                parameters.Add(parameterValues[5].text);
                parameters.Add(parameterValues[9].text);
                parameters.Add(parameterValues[10].text);
                parameters.Add(parameterValues[11].text);
                parameters.Add(parameterValues[12].text);
                parameters.Add(parameterValues[13].text);
                parameters.Add(parameterValues[14].text);
                parameters.Add(parameterValues[15].text);
                parameters.Add(parameterValues[16].text);
                break;
            #endregion
            #region Monitor
            case ConstStrings.Monitor:
                parameters.Clear();
                parameters.Add(parameterValues[6].text);
                parameters.Add(parameterValues[5].text);
                parameters.Add(parameterValues[9].text);
                parameters.Add(parameterValues[10].text);
                break;
            #endregion
            #region Mouse
            case ConstStrings.Mouse:
                break;
            #endregion
            #region Notebook
            case ConstStrings.Notebook:
                parameters.Clear();
                parameters.Add(parameterValues[6].text);
                parameters.Add(parameterValues[5].text);
                break;
            #endregion
            #region Placa controladora
            case ConstStrings.PlacaControladora:
                parameters.Clear();
                parameters.Add(parameterValues[6].text);
                parameters.Add(parameterValues[9].text);
                parameters.Add(parameterValues[10].text);
                parameters.Add(parameterValues[11].text);
                parameters.Add(parameterValues[12].text);
                parameters.Add(parameterValues[13].text);
                parameters.Add(parameterValues[14].text);
                parameters.Add(parameterValues[15].text);
                parameters.Add(parameterValues[16].text);
                break;
            #endregion
            #region Placa de captura de video
            case ConstStrings.PlacaDeCapturaDeVideo:
                parameters.Clear();
                parameters.Add(parameterValues[6].text);
                parameters.Add(parameterValues[9].text);
                break;
            #endregion
            #region Placa de rede
            case ConstStrings.PlacaDeRede:
                parameters.Clear();
                parameters.Add(parameterValues[6].text);
                parameters.Add(parameterValues[5].text);
                parameters.Add(parameterValues[9].text);
                parameters.Add(parameterValues[10].text);
                parameters.Add(parameterValues[11].text);
                parameters.Add(parameterValues[12].text);
                parameters.Add(parameterValues[13].text);
                break;
            #endregion
            #region Placa de som
            case ConstStrings.PlacaDeSom:
                parameters.Clear();
                parameters.Add(parameterValues[6].text);
                parameters.Add(parameterValues[9].text);
                break;
            #endregion
            #region Placa de Video
            case ConstStrings.PlacaDeVideo:
                parameters.Clear();
                parameters.Add(parameterValues[6].text);
                parameters.Add(parameterValues[9].text);
                parameters.Add(parameterValues[10].text);
                break;
            #endregion
            #region Processador
            case ConstStrings.Processador:
                parameters.Clear();
                parameters.Add(parameterValues[6].text);
                parameters.Add(parameterValues[9].text);
                parameters.Add(parameterValues[10].text);
                parameters.Add(parameterValues[11].text);
                parameters.Add(parameterValues[12].text);
                parameters.Add(parameterValues[13].text);
                parameters.Add(parameterValues[14].text);
                break;
            #endregion
            #region Ramal
            case ConstStrings.Ramal:
                break;
            #endregion
            #region Roteador
            case ConstStrings.Roteador:
                parameters.Clear();
                parameters.Add(parameterValues[6].text);
                parameters.Add(parameterValues[9].text);
                parameters.Add(parameterValues[10].text);
                parameters.Add(parameterValues[11].text);
                parameters.Add(parameterValues[12].text);
                break;
            #endregion
            #region Servidor
            case ConstStrings.Servidor:
                parameters.Clear();
                parameters.Add(parameterValues[6].text);
                parameters.Add(parameterValues[5].text);
                break;
            #endregion
            #region Storage NAS
            case ConstStrings.StorageNAS:
                parameters.Clear();
                parameters.Add(parameterValues[6].text);
                parameters.Add(parameterValues[9].text);
                parameters.Add(parameterValues[10].text);
                parameters.Add(parameterValues[11].text);
                parameters.Add(parameterValues[12].text);
                parameters.Add(parameterValues[13].text);
                break;
            #endregion
            #region Switch
            case ConstStrings.Switch:
                parameters.Clear();
                parameters.Add(parameterValues[6].text);
                parameters.Add(parameterValues[9].text);
                parameters.Add(parameterValues[10].text);
                break;
            #endregion
            #region Teclado
            case ConstStrings.Teclado:

                break;
            #endregion
            #region No break
            case ConstStrings.Nobreak:

                break;
            #endregion
            default:
                break;
        }

        yield return HelperMethods.AddUpdateItem(categoryDP.value, 2, parameters, false);
        if (HelperMethods.GetAddUpdateResponse())
        {
            addDetalheSuccess = true;
        }
        else
        {
            addDetalheSuccess = false;
        }
        if (!addInventario)
        {
            EventHandler.CallOpenMessageEvent("Worked");
            addInventarioSuccess = true;
        }

        if (addInventarioSuccess)
        {
            AddItem();
        }
    }

    /// <summary>
    /// Close the message. It is public to be used on the button too
    /// </summary>
    public void MessageClosed()
    {
        UpdateNames();
        MouseManager.Instance.SetDefaultCursor();
    }

    /// <summary>
    /// Add a new item to all databases
    /// </summary>
    private void AddItem()
    {
        ItemColumns itemToAddFullDatabase = new ItemColumns();
        ItemColumns itemToAddSplitDatabase = new ItemColumns();

        itemToAddFullDatabase.Aquisicao = parameterValues[0].text;
        itemToAddFullDatabase.Entrada = parameterValues[1].text;
        itemToAddFullDatabase.Patrimonio = parameterValues[2].text;
        itemToAddFullDatabase.Status = parameterValues[3].text;
        itemToAddFullDatabase.Serial = parameterValues[4].text;
        itemToAddFullDatabase.Fabricante = parameterValues[5].text;
        itemToAddFullDatabase.Modelo = parameterValues[6].text;
        itemToAddSplitDatabase.Modelo = parameterValues[6].text;
        itemToAddFullDatabase.Local = parameterValues[7].text;
        itemToAddFullDatabase.Saida = "";
        itemToAddFullDatabase.Observacao = parameterValues[8].text;
        switch (categoryDP.value)
        {
            #region HD
            case 0:
                itemToAddFullDatabase.Categoria = ConstStrings.HD;
                InternalDatabase.Instance.splitDatabase[ConstStrings.InventarioSnPro].itens.Add(itemToAddFullDatabase);

                itemToAddFullDatabase.Interface = parameterValues[9].text;
                itemToAddSplitDatabase.Interface = parameterValues[9].text;
                itemToAddFullDatabase.Tamanho = parameterValues[10].text;
                itemToAddSplitDatabase.Tamanho = parameterValues[10].text;
                itemToAddFullDatabase.FormaDeArmazenamento = parameterValues[11].text;
                itemToAddSplitDatabase.FormaDeArmazenamento = parameterValues[11].text;
                itemToAddFullDatabase.CapacidadeEmGB = parameterValues[12].text;
                itemToAddSplitDatabase.CapacidadeEmGB = parameterValues[12].text;
                itemToAddFullDatabase.RPM = parameterValues[13].text;
                itemToAddSplitDatabase.RPM = parameterValues[13].text;
                itemToAddFullDatabase.VelocidadeDeLeitura = parameterValues[14].text;
                itemToAddSplitDatabase.VelocidadeDeLeitura = parameterValues[14].text;
                itemToAddFullDatabase.Enterprise = parameterValues[15].text;
                itemToAddSplitDatabase.Enterprise = parameterValues[15].text;

                InternalDatabase.Instance.splitDatabase[ConstStrings.HD].itens.Add(itemToAddSplitDatabase);
                InternalDatabase.hd.itens.Add(itemToAddFullDatabase);
                break;
            #endregion
            #region Memoria
            case 1:
                itemToAddFullDatabase.Categoria = ConstStrings.Memoria;
                InternalDatabase.Instance.splitDatabase[ConstStrings.InventarioSnPro].itens.Add(itemToAddFullDatabase);
                itemToAddFullDatabase.Tipo = parameterValues[9].text;
                itemToAddSplitDatabase.Tipo = parameterValues[9].text;
                itemToAddFullDatabase.CapacidadeEmGB = parameterValues[10].text;
                itemToAddSplitDatabase.CapacidadeEmGB = parameterValues[10].text;
                itemToAddFullDatabase.VelocidadeMHz = parameterValues[11].text;
                itemToAddSplitDatabase.VelocidadeMHz = parameterValues[11].text;
                itemToAddFullDatabase.LowVoltage = parameterValues[12].text;
                itemToAddSplitDatabase.LowVoltage = parameterValues[12].text;
                itemToAddFullDatabase.Rank = parameterValues[13].text;
                itemToAddSplitDatabase.Rank = parameterValues[13].text;
                itemToAddFullDatabase.DIMM = parameterValues[14].text;
                itemToAddSplitDatabase.DIMM = parameterValues[14].text;
                itemToAddFullDatabase.TaxaDeTransmissao = parameterValues[15].text;
                itemToAddSplitDatabase.TaxaDeTransmissao = parameterValues[15].text;
                itemToAddFullDatabase.Simbolo = parameterValues[16].text;
                itemToAddSplitDatabase.Simbolo = parameterValues[16].text;

                InternalDatabase.Instance.splitDatabase[ConstStrings.Memoria].itens.Add(itemToAddSplitDatabase);
                InternalDatabase.memoria.itens.Add(itemToAddFullDatabase);
                break;
            #endregion
            #region Placa de rede
            case 2:
                itemToAddFullDatabase.Categoria = ConstStrings.PlacaDeRede;
                InternalDatabase.Instance.splitDatabase[ConstStrings.InventarioSnPro].itens.Add(itemToAddFullDatabase);
                itemToAddFullDatabase.Interface = parameterValues[9].text;
                itemToAddFullDatabase.QuantidadeDePortas = parameterValues[10].text;
                itemToAddFullDatabase.QuaisConexoes = parameterValues[11].text;
                itemToAddFullDatabase.SuportaFibraOptica = parameterValues[12].text;
                itemToAddFullDatabase.Desempenho = parameterValues[13].text;
                itemToAddSplitDatabase.Interface = parameterValues[9].text;
                itemToAddSplitDatabase.QuantidadeDePortas = parameterValues[10].text;
                itemToAddSplitDatabase.QuaisConexoes = parameterValues[11].text;
                itemToAddSplitDatabase.SuportaFibraOptica = parameterValues[12].text;
                itemToAddSplitDatabase.Desempenho = parameterValues[13].text;

                InternalDatabase.Instance.splitDatabase[ConstStrings.PlacaDeRede].itens.Add(itemToAddSplitDatabase);
                InternalDatabase.placaDeRede.itens.Add(itemToAddFullDatabase);
                break;
            #endregion
            #region iDrac
            case 3:
                itemToAddFullDatabase.Categoria = ConstStrings.Idrac;
                InternalDatabase.Instance.splitDatabase[ConstStrings.InventarioSnPro].itens.Add(itemToAddFullDatabase);
                itemToAddFullDatabase.QuaisConexoes = parameterValues[9].text;
                itemToAddFullDatabase.VelocidadeGBs = parameterValues[10].text;
                itemToAddFullDatabase.EntradaSD = parameterValues[11].text;
                itemToAddFullDatabase.ServidoresSuportados = parameterValues[12].text;
                itemToAddSplitDatabase.QuaisConexoes = parameterValues[9].text;
                itemToAddSplitDatabase.VelocidadeGBs = parameterValues[10].text;
                itemToAddSplitDatabase.EntradaSD = parameterValues[11].text;
                itemToAddSplitDatabase.ServidoresSuportados = parameterValues[12].text;

                InternalDatabase.Instance.splitDatabase[ConstStrings.Idrac].itens.Add(itemToAddSplitDatabase);
                InternalDatabase.idrac.itens.Add(itemToAddFullDatabase);
                break;
            #endregion
            #region Placa controladora
            case 4:
                itemToAddFullDatabase.Categoria = ConstStrings.PlacaControladora;
                InternalDatabase.Instance.splitDatabase[ConstStrings.InventarioSnPro].itens.Add(itemToAddFullDatabase);
                itemToAddFullDatabase.QuaisConexoes = parameterValues[9].text;
                itemToAddFullDatabase.QuantidadeDePortas = parameterValues[10].text;
                itemToAddFullDatabase.TipoDeRAID = parameterValues[11].text;
                itemToAddFullDatabase.TipoDeHD = parameterValues[12].text;
                itemToAddFullDatabase.CapacidadeMaxHD = parameterValues[13].text;
                itemToAddFullDatabase.AteQuantosHDs = parameterValues[14].text;
                itemToAddFullDatabase.BateriaInclusa = parameterValues[15].text;
                itemToAddFullDatabase.Barramento = parameterValues[16].text;
                itemToAddSplitDatabase.QuaisConexoes = parameterValues[9].text;
                itemToAddSplitDatabase.QuantidadeDePortas = parameterValues[10].text;
                itemToAddSplitDatabase.TipoDeRAID = parameterValues[11].text;
                itemToAddSplitDatabase.TipoDeHD = parameterValues[12].text;
                itemToAddSplitDatabase.CapacidadeMaxHD = parameterValues[13].text;
                itemToAddSplitDatabase.AteQuantosHDs = parameterValues[14].text;
                itemToAddSplitDatabase.BateriaInclusa = parameterValues[15].text;
                itemToAddSplitDatabase.Barramento = parameterValues[16].text;

                InternalDatabase.Instance.splitDatabase[ConstStrings.PlacaControladora].itens.Add(itemToAddSplitDatabase);
                InternalDatabase.placaControladora.itens.Add(itemToAddFullDatabase);
                break;
            #endregion
            #region Processador
            case 5:
                itemToAddFullDatabase.Categoria = ConstStrings.Processador;
                InternalDatabase.Instance.splitDatabase[ConstStrings.InventarioSnPro].itens.Add(itemToAddFullDatabase);
                itemToAddFullDatabase.Soquete = parameterValues[9].text;
                itemToAddFullDatabase.NucleosFisicos = parameterValues[10].text;
                itemToAddFullDatabase.NucleosLogicos = parameterValues[11].text;
                itemToAddFullDatabase.AceitaVirtualizacao = parameterValues[12].text;
                itemToAddFullDatabase.TurboBoost = parameterValues[13].text;
                itemToAddFullDatabase.HyperThreading = parameterValues[14].text;
                itemToAddSplitDatabase.Soquete = parameterValues[9].text;
                itemToAddSplitDatabase.NucleosFisicos = parameterValues[10].text;
                itemToAddSplitDatabase.NucleosLogicos = parameterValues[11].text;
                itemToAddSplitDatabase.AceitaVirtualizacao = parameterValues[12].text;
                itemToAddSplitDatabase.TurboBoost = parameterValues[13].text;
                itemToAddSplitDatabase.HyperThreading = parameterValues[14].text;

                InternalDatabase.Instance.splitDatabase[ConstStrings.Processador].itens.Add(itemToAddSplitDatabase);
                InternalDatabase.processador.itens.Add(itemToAddFullDatabase);
                break;
            #endregion
            #region Desktop
            case 6:
                itemToAddFullDatabase.Categoria = ConstStrings.Desktop;
                InternalDatabase.Instance.splitDatabase[ConstStrings.InventarioSnPro].itens.Add(itemToAddFullDatabase);
                itemToAddFullDatabase.ModeloPlacaMae = parameterValues[9].text;
                itemToAddFullDatabase.Fonte = parameterValues[10].text;
                itemToAddFullDatabase.Memoria = parameterValues[11].text;
                itemToAddFullDatabase.HD = parameterValues[12].text;
                itemToAddFullDatabase.PlacaDeVideo = parameterValues[13].text;
                itemToAddFullDatabase.PlacaDeRede = parameterValues[14].text;
                itemToAddFullDatabase.LeitorDeDVD = parameterValues[15].text;
                itemToAddFullDatabase.Processador = parameterValues[16].text;
                itemToAddSplitDatabase.Patrimonio = parameterValues[1].text;
                itemToAddSplitDatabase.ModeloPlacaMae = parameterValues[9].text;
                itemToAddSplitDatabase.Fonte = parameterValues[10].text;
                itemToAddSplitDatabase.Memoria = parameterValues[11].text;
                itemToAddSplitDatabase.HD = parameterValues[12].text;
                itemToAddSplitDatabase.PlacaDeVideo = parameterValues[13].text;
                itemToAddSplitDatabase.PlacaDeRede = parameterValues[14].text;
                itemToAddSplitDatabase.LeitorDeDVD = parameterValues[15].text;
                itemToAddSplitDatabase.Processador = parameterValues[16].text;

                InternalDatabase.Instance.splitDatabase[ConstStrings.Desktop].itens.Add(itemToAddSplitDatabase);
                InternalDatabase.desktop.itens.Add(itemToAddFullDatabase);
                break;
            #endregion
            #region Fonte
            case 7:
                itemToAddFullDatabase.Categoria = ConstStrings.Fonte;
                InternalDatabase.Instance.splitDatabase[ConstStrings.InventarioSnPro].itens.Add(itemToAddFullDatabase);
                itemToAddFullDatabase.Watts = parameterValues[9].text;
                itemToAddFullDatabase.OndeFunciona = parameterValues[10].text;
                itemToAddFullDatabase.Conectores = parameterValues[11].text;
                itemToAddSplitDatabase.Watts = parameterValues[9].text;
                itemToAddSplitDatabase.OndeFunciona = parameterValues[10].text;
                itemToAddSplitDatabase.Conectores = parameterValues[11].text;

                InternalDatabase.Instance.splitDatabase[ConstStrings.Fonte].itens.Add(itemToAddSplitDatabase);
                InternalDatabase.fonte.itens.Add(itemToAddFullDatabase);
                break;
            #endregion
            #region Switch
            case 8:
                itemToAddFullDatabase.Categoria = ConstStrings.Switch;
                InternalDatabase.Instance.splitDatabase[ConstStrings.InventarioSnPro].itens.Add(itemToAddFullDatabase);
                itemToAddFullDatabase.QuantidadeDePortas = parameterValues[9].text;
                itemToAddFullDatabase.Desempenho = parameterValues[10].text;
                itemToAddSplitDatabase.QuantidadeDePortas = parameterValues[9].text;
                itemToAddSplitDatabase.Desempenho = parameterValues[10].text;

                InternalDatabase.Instance.splitDatabase[ConstStrings.Switch].itens.Add(itemToAddSplitDatabase);
                InternalDatabase.Switch.itens.Add(itemToAddFullDatabase);
                break;
            #endregion
            #region Roteador
            case 9:
                itemToAddFullDatabase.Categoria = ConstStrings.Roteador;
                InternalDatabase.Instance.splitDatabase[ConstStrings.InventarioSnPro].itens.Add(itemToAddFullDatabase);
                itemToAddFullDatabase.Wireless = parameterValues[9].text;
                itemToAddFullDatabase.QuantidadeDePortas = parameterValues[10].text;
                itemToAddFullDatabase.BandaMaxima = parameterValues[11].text;
                itemToAddFullDatabase.VoltagemDeSaida = parameterValues[12].text;
                itemToAddSplitDatabase.Wireless = parameterValues[9].text;
                itemToAddSplitDatabase.QuantidadeDePortas = parameterValues[10].text;
                itemToAddSplitDatabase.BandaMaxima = parameterValues[11].text;
                itemToAddSplitDatabase.VoltagemDeSaida = parameterValues[12].text;

                InternalDatabase.Instance.splitDatabase[ConstStrings.Roteador].itens.Add(itemToAddSplitDatabase);
                InternalDatabase.roteador.itens.Add(itemToAddFullDatabase);
                break;
            #endregion
            #region Carregador
            case 10:
                itemToAddFullDatabase.Categoria = ConstStrings.Carregador;
                InternalDatabase.Instance.splitDatabase[ConstStrings.InventarioSnPro].itens.Add(itemToAddFullDatabase);
                itemToAddFullDatabase.OndeFunciona = parameterValues[9].text;
                itemToAddFullDatabase.VoltagemDeSaida = parameterValues[10].text;
                itemToAddFullDatabase.AmperagemDeSaida = parameterValues[11].text;
                itemToAddSplitDatabase.OndeFunciona = parameterValues[9].text;
                itemToAddSplitDatabase.VoltagemDeSaida = parameterValues[10].text;
                itemToAddSplitDatabase.AmperagemDeSaida = parameterValues[11].text;

                InternalDatabase.Instance.splitDatabase[ConstStrings.Carregador].itens.Add(itemToAddSplitDatabase);
                InternalDatabase.carregador.itens.Add(itemToAddFullDatabase);
                break;
            #endregion
            #region Adaptador AC
            case 11:
                itemToAddFullDatabase.Categoria = ConstStrings.AdaptadorAC;
                InternalDatabase.Instance.splitDatabase[ConstStrings.InventarioSnPro].itens.Add(itemToAddFullDatabase);
                itemToAddFullDatabase.OndeFunciona = parameterValues[9].text;
                itemToAddFullDatabase.VoltagemDeSaida = parameterValues[10].text;
                itemToAddFullDatabase.AmperagemDeSaida = parameterValues[11].text;
                itemToAddSplitDatabase.OndeFunciona = parameterValues[9].text;
                itemToAddSplitDatabase.VoltagemDeSaida = parameterValues[10].text;
                itemToAddSplitDatabase.AmperagemDeSaida = parameterValues[11].text;

                InternalDatabase.Instance.splitDatabase[ConstStrings.AdaptadorAC].itens.Add(itemToAddSplitDatabase);
                InternalDatabase.adaptadorAC.itens.Add(itemToAddFullDatabase);
                break;
            #endregion
            #region Storage NAS
            case 12:
                itemToAddFullDatabase.Categoria = ConstStrings.StorageNAS;
                InternalDatabase.Instance.splitDatabase[ConstStrings.InventarioSnPro].itens.Add(itemToAddFullDatabase);
                itemToAddFullDatabase.Tamanho = parameterValues[9].text;
                itemToAddFullDatabase.TipoDeRAID = parameterValues[10].text;
                itemToAddFullDatabase.TipoDeHD = parameterValues[11].text;
                itemToAddFullDatabase.CapacidadeMaxHD = parameterValues[12].text;
                itemToAddFullDatabase.AteQuantosHDs = parameterValues[13].text;
                itemToAddSplitDatabase.Tamanho = parameterValues[9].text;
                itemToAddSplitDatabase.TipoDeRAID = parameterValues[10].text;
                itemToAddSplitDatabase.TipoDeHD = parameterValues[11].text;
                itemToAddSplitDatabase.CapacidadeMaxHD = parameterValues[12].text;
                itemToAddSplitDatabase.AteQuantosHDs = parameterValues[13].text;

                InternalDatabase.Instance.splitDatabase[ConstStrings.StorageNAS].itens.Add(itemToAddSplitDatabase);
                InternalDatabase.storageNAS.itens.Add(itemToAddFullDatabase);
                break;
            #endregion
            #region GBIC
            case 13:
                itemToAddFullDatabase.Categoria = ConstStrings.Gbic;
                InternalDatabase.Instance.splitDatabase[ConstStrings.InventarioSnPro].itens.Add(itemToAddFullDatabase);
                itemToAddFullDatabase.Desempenho = parameterValues[9].text;
                itemToAddSplitDatabase.Desempenho = parameterValues[9].text;

                InternalDatabase.Instance.splitDatabase[ConstStrings.Gbic].itens.Add(itemToAddSplitDatabase);
                InternalDatabase.gbic.itens.Add(itemToAddFullDatabase);
                break;
            #endregion
            #region Placa de Video
            case 14:
                itemToAddFullDatabase.Categoria = ConstStrings.PlacaDeVideo;
                InternalDatabase.Instance.splitDatabase[ConstStrings.InventarioSnPro].itens.Add(itemToAddFullDatabase);
                itemToAddFullDatabase.QuantidadeDePortas = parameterValues[9].text;
                itemToAddFullDatabase.QuaisConexoes = parameterValues[10].text;
                itemToAddSplitDatabase.QuantidadeDePortas = parameterValues[9].text;
                itemToAddSplitDatabase.QuaisConexoes = parameterValues[10].text;

                InternalDatabase.Instance.splitDatabase[ConstStrings.PlacaDeVideo].itens.Add(itemToAddSplitDatabase);
                InternalDatabase.placaDeVideo.itens.Add(itemToAddFullDatabase);
                break;
            #endregion
            #region Placa de som
            case 15:
                itemToAddFullDatabase.Categoria = ConstStrings.PlacaDeSom;
                InternalDatabase.Instance.splitDatabase[ConstStrings.InventarioSnPro].itens.Add(itemToAddFullDatabase);
                itemToAddFullDatabase.QuantosCanais = parameterValues[9].text;
                itemToAddSplitDatabase.QuantosCanais = parameterValues[9].text;

                InternalDatabase.Instance.splitDatabase[ConstStrings.PlacaDeSom].itens.Add(itemToAddSplitDatabase);
                InternalDatabase.placaDeSom.itens.Add(itemToAddFullDatabase);
                break;
            #endregion
            #region Placa de captura de video
            case 16:
                itemToAddFullDatabase.Categoria = ConstStrings.PlacaDeCapturaDeVideo;
                InternalDatabase.Instance.splitDatabase[ConstStrings.InventarioSnPro].itens.Add(itemToAddFullDatabase);
                itemToAddFullDatabase.QuantidadeDePortas = parameterValues[9].text;
                itemToAddSplitDatabase.QuantidadeDePortas = parameterValues[9].text;

                InternalDatabase.Instance.splitDatabase[ConstStrings.PlacaDeCapturaDeVideo].itens.Add(itemToAddSplitDatabase);
                InternalDatabase.placaDeCapturaDeVideo.itens.Add(itemToAddFullDatabase);
                break;
            #endregion
            #region Servidor
            case 17:
                itemToAddFullDatabase.Categoria = ConstStrings.Servidor;
                InternalDatabase.Instance.splitDatabase[ConstStrings.InventarioSnPro].itens.Add(itemToAddFullDatabase);

                InternalDatabase.Instance.splitDatabase[ConstStrings.Servidor].itens.Add(itemToAddSplitDatabase);
                InternalDatabase.servidor.itens.Add(itemToAddFullDatabase);
                break;
            #endregion
            #region Notebook
            case 18:
                itemToAddFullDatabase.Categoria = ConstStrings.Notebook;
                InternalDatabase.Instance.splitDatabase[ConstStrings.InventarioSnPro].itens.Add(itemToAddFullDatabase);

                InternalDatabase.Instance.splitDatabase[ConstStrings.Notebook].itens.Add(itemToAddSplitDatabase);
                InternalDatabase.notebook.itens.Add(itemToAddFullDatabase);
                break;
            #endregion
            #region Monitor
            case 19:
                itemToAddFullDatabase.Categoria = ConstStrings.Monitor;
                InternalDatabase.Instance.splitDatabase[ConstStrings.InventarioSnPro].itens.Add(itemToAddFullDatabase);
                itemToAddFullDatabase.Polegadas = parameterValues[9].text;
                itemToAddSplitDatabase.Polegadas = parameterValues[9].text;
                itemToAddFullDatabase.QuaisConexoes = parameterValues[10].text;
                itemToAddSplitDatabase.QuaisConexoes = parameterValues[10].text;

                InternalDatabase.Instance.splitDatabase[ConstStrings.Monitor].itens.Add(itemToAddSplitDatabase);
                InternalDatabase.monitor.itens.Add(itemToAddFullDatabase);
                break;
            #endregion
            default:
                break;
        }
        InternalDatabase.Instance.fullDatabase.itens.Add(itemToAddFullDatabase);
        // ShowMessage();
        EventHandler.CallDatabaseUpdatedEvent(ConstStrings.DataDatabaseSaveFile);
    }

    public void AddItemClicked()
    {
        StartCoroutine(AddNewItemRoutine(true));
    }

    // MAYBE will be implemented
    public void RemoveItem()
    {

    }

    /// <summary>
    /// Call UpdateNames whenever a new category is selected
    /// </summary>
    public void HandleInputData(int value)
    {
        UpdateNames();
    }

    /// <summary>
    /// Goes to InitialScene
    /// </summary>
    public void ReturnToPreviousScreen()
    {
        SceneManager.LoadScene(ConstStrings.SceneInitial);
    }

    public void ResetAddItem()
    {
        UpdateNames();
    }

    #region TEsting
    public void AddDetailsItem()
    {
        StartCoroutine(AddNewItemRoutine(false));
    }
    #endregion
}
