using System.Collections;
using System.Collections.Generic;
using System.Data;
using TMPro;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.SceneManagement;

public class AddRemoveItem : MonoBehaviour
{
    [SerializeField] private TMP_Text[] parameterNames;
    [SerializeField] private TMP_InputField[] parameterValues;
    [SerializeField] private GameObject[] itensToShow;
    [SerializeField] private TMP_Dropdown categoryDP;

    [SerializeField] GameObject messagePanel;
    [SerializeField] TMP_Text messageText;

    private void Start()
    {
        UpdateNames();
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

        parameterNames[0].text = "Entrada no estoque";
        parameterNames[1].text = "Patrimônio";
        parameterNames[2].text = "Status";
        parameterNames[3].text = "Serial";
        parameterNames[4].text = "Fabricante";
        parameterNames[5].text = "Modelo";
        parameterNames[6].text = "Local";
        parameterNames[7].text = "Saída do estoque";
        parameterNames[8].text = "Observação";

        switch (categoryDP.value)
        {
            case 0: // HD
                parameterNames[9].text = "Interface";
                parameterNames[10].text = "Tamanho";
                parameterNames[11].text = "Forma de armazenamento";
                parameterNames[12].text = "Capacidade (GB)";
                parameterNames[13].text = "RPM";
                parameterNames[14].text = "Velocidade de Leitura (Gb/s)";
                parameterNames[15].text = "Enterprise";
                parameterNames[16].text = "";
                break;
            case 1: // Memoria
                parameterNames[9].text = "Tipo";
                parameterNames[10].text = "Capacidade (GB)";
                parameterNames[11].text = "Velocidade (MHz)";
                parameterNames[12].text = "É low voltage?";
                parameterNames[13].text = "Rank";
                parameterNames[14].text = "DIMM";
                parameterNames[15].text = "Taxa de transmissão";
                parameterNames[16].text = "Símbolo";
                break;
            case 2: // Placa de rede
                parameterNames[9].text = "Interface";
                parameterNames[10].text = "Quantas portas?";
                parameterNames[11].text = "Quais portas?";
                parameterNames[12].text = "Suporta fibra óptica?";
                parameterNames[13].text = "Desempenho (MB/s)";
                parameterNames[14].text = "";
                parameterNames[15].text = "";
                parameterNames[16].text = "";
                break;
            case 3: // idrac
                parameterNames[9].text = "Porta";
                parameterNames[10].text = "Velocidade (GB/s)";
                parameterNames[11].text = "Entrada SD";
                parameterNames[12].text = "Servidores suportados";
                parameterNames[13].text = "";
                parameterNames[14].text = "";
                parameterNames[15].text = "";
                parameterNames[16].text = "";
                break;
            case 4: // Placa controladora
                parameterNames[9].text = "Tipo de conexão";
                parameterNames[10].text = "Quantas portas?";
                parameterNames[11].text = "Tipos de RAID";
                parameterNames[12].text = "Tipo de HD";
                parameterNames[13].text = "Capacidade máx do HD (TB)";
                parameterNames[14].text = "Até quantos HDs";
                parameterNames[15].text = "Bateria inclusa?";
                parameterNames[16].text = "Barramento";
                break;
            case 5: // Processador
                parameterNames[9].text = "Soquete";
                parameterNames[10].text = "Nº núcleos físicos";
                parameterNames[11].text = "Nº núcleos lógicos";
                parameterNames[12].text = "Aceita virtualização?";
                parameterNames[13].text = "Turbo boost?";
                parameterNames[14].text = "Hyper-Threading?";
                parameterNames[15].text = "";
                parameterNames[16].text = "";
                break;
            case 6: // Desktop
                parameterNames[9].text = "Modelo de placa mãe";
                parameterNames[10].text = "Fonte?";
                parameterNames[11].text = "Memória?";
                parameterNames[12].text = "HD?";
                parameterNames[13].text = "Placa de vídeo?";
                parameterNames[14].text = "Placa de rede?";
                parameterNames[15].text = "Leitor de DVD?";
                parameterNames[16].text = "Processador?";
                break;
            case 7: // Fonte
                parameterNames[9].text = "Watts de potência";
                parameterNames[10].text = "Onde funciona?";
                parameterNames[11].text = "Conectores";
                parameterNames[12].text = "";
                parameterNames[13].text = "";
                parameterNames[14].text = "";
                parameterNames[15].text = "";
                parameterNames[16].text = "";
                break;
            case 8: // Switch
                parameterNames[9].text = "Quantas entradas";
                parameterNames[10].text = "Capacidade máx de cada porta (MB/s)";
                parameterNames[11].text = "";
                parameterNames[12].text = "";
                parameterNames[13].text = "";
                parameterNames[14].text = "";
                parameterNames[15].text = "";
                parameterNames[16].text = "";
                break;
            case 9: // Roteador
                parameterNames[9].text = "Wireless?";
                parameterNames[10].text = "Quantas entradas?";
                parameterNames[11].text = "Banda máx (MB/s)";
                parameterNames[12].text = "Voltagem";
                parameterNames[13].text = "";
                parameterNames[14].text = "";
                parameterNames[15].text = "";
                parameterNames[16].text = "";
                break;
            case 10: // Carregador
                parameterNames[9].text = "Onde funciona?";
                parameterNames[10].text = "Voltagem de saída";
                parameterNames[11].text = "Amperagem de saída (mA)";
                parameterNames[12].text = "";
                parameterNames[13].text = "";
                parameterNames[14].text = "";
                parameterNames[15].text = "";
                parameterNames[16].text = "";
                break;
            case 11: // Adaptador AC
                parameterNames[9].text = "Onde funciona?";
                parameterNames[10].text = "Voltagem de saída";
                parameterNames[11].text = "Amperagem de saída (A)";
                parameterNames[12].text = "";
                parameterNames[13].text = "";
                parameterNames[14].text = "";
                parameterNames[15].text = "";
                parameterNames[16].text = "";
                break;
            case 12: // Storage NAS
                parameterNames[9].text = "Tamanho dos HDs";
                parameterNames[10].text = "Tipos de RAID";
                parameterNames[11].text = "Tipo de HD";
                parameterNames[12].text = "Capacidade máx do HD";
                parameterNames[13].text = "Até quantos HDs";
                parameterNames[14].text = "";
                parameterNames[15].text = "";
                parameterNames[16].text = "";
                break;
            case 13: // Gbic
                parameterNames[9].text = "Desempenho máx (GB/s)";
                parameterNames[10].text = "";
                parameterNames[11].text = "";
                parameterNames[12].text = "";
                parameterNames[13].text = "";
                parameterNames[14].text = "";
                parameterNames[15].text = "";
                parameterNames[16].text = "";
                break;
            case 14: // Placa de vídeo
                parameterNames[9].text = "Quantas entradas?";
                parameterNames[10].text = "Quais entradas?";
                parameterNames[11].text = "";
                parameterNames[12].text = "";
                parameterNames[13].text = "";
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
            case 16: // Placa de captura de vídeo
                parameterNames[9].text = "Quantas entradas?";
                parameterNames[10].text = "";
                parameterNames[11].text = "";
                parameterNames[12].text = "";
                parameterNames[13].text = "";
                parameterNames[14].text = "";
                parameterNames[15].text = "";
                parameterNames[16].text = "";
                break;
            case 17: // Servidor
                parameterNames[9].text = "";
                parameterNames[10].text = "";
                parameterNames[11].text = "";
                parameterNames[12].text = "";
                parameterNames[13].text = "";
                parameterNames[14].text = "";
                parameterNames[15].text = "";
                parameterNames[16].text = "";
                break;
            case 18: // Notebook
                parameterNames[9].text = "";
                parameterNames[10].text = "";
                parameterNames[11].text = "";
                parameterNames[12].text = "";
                parameterNames[13].text = "";
                parameterNames[14].text = "";
                parameterNames[15].text = "";
                parameterNames[16].text = "";
                break;
            case 19: // Monitor
                parameterNames[9].text = "Polegadas";
                parameterNames[10].text = "Tipos de entradas";
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
    /// <summary>
    /// Shows the message that the item was added
    /// </summary>
    private void ShowMessage()
    {
        messagePanel.SetActive(true);
        StartCoroutine(CloseMessageRoutine());
    }

    /// <summary>
    /// Wait for a few seconds before automatically closing the message
    /// </summary>
    private IEnumerator CloseMessageRoutine()
    {
        yield return new WaitForSeconds(5f);
        CloseMessage();
    }

    /// <summary>
    /// Close the message. It is public to be used on the button too
    /// </summary>
    public void CloseMessage()
    {
        UpdateNames();
        messagePanel.SetActive(false);
        StopCoroutine(CloseMessageRoutine());
    }

    /// <summary>
    /// Add a new item to all databases
    /// </summary>
    public void AddItem()
    {
        ItemColumns itemToAddFullDatabase = new ItemColumns();
        ItemColumns itemToAddSplitDatabase = new ItemColumns();

        itemToAddFullDatabase.Entrada = parameterValues[0].text;
        itemToAddFullDatabase.Patrimonio = parameterValues[1].text;
        itemToAddFullDatabase.Status = parameterValues[2].text;
        itemToAddFullDatabase.Serial = parameterValues[3].text;
        itemToAddFullDatabase.Fabricante = parameterValues[4].text;
        itemToAddFullDatabase.Modelo = parameterValues[5].text;
        itemToAddSplitDatabase.Modelo = parameterValues[5].text;
        itemToAddFullDatabase.Local = parameterValues[6].text;
        itemToAddFullDatabase.Saida = parameterValues[7].text;
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
        StartCoroutine(AddNewItemRoutine());
        ShowMessage();
        EventHandler.CallDatabaseUpdatedEvent(ConstStrings.DataDatabaseSaveFile);
    }

    private IEnumerator AddNewItemRoutine()
    {
        #region Add new item to Inventario
        WWWForm inventarioForm = CreateAddItemForm.GetInventarioForm(parameterValues[0].text, parameterValues[1].text,
        parameterValues[2].text, parameterValues[3].text, parameterValues[4].text,
        HelperMethods.GetCategoryString(categoryDP.value), parameterValues[5].text, parameterValues[6].text,
        parameterValues[7].text, parameterValues[8].text);
        UnityWebRequest createInventarioPostRequest = UnityWebRequest.Post(ConstStrings.PhpAdditemsFolder + "addnewiteminventario.php", inventarioForm);
        yield return createInventarioPostRequest.SendWebRequest();

        if (createInventarioPostRequest.result == UnityWebRequest.Result.ConnectionError)
        {
            Debug.LogWarning("conectionerror");
        }
        else if (createInventarioPostRequest.result == UnityWebRequest.Result.DataProcessingError)
        {
            Debug.LogWarning("data processing error");
        }
        else if (createInventarioPostRequest.result == UnityWebRequest.Result.ProtocolError)
        {
            Debug.LogWarning("protocol error");
        }

        if (createInventarioPostRequest.error == null)
        {

            string response = createInventarioPostRequest.downloadHandler.text;
            if (response == "1" || response == "2" || response == "5")
            {

            }
            else if (response == "3")
            {

            }
            else if (response == "4")
            {

            }
            else
            {

            }

        }
        else
        {

        }
        createInventarioPostRequest.Dispose();
        #endregion
        switch (categoryDP.value)
        {
            #region HD
            case 0:
                WWWForm hdForm = CreateAddItemForm.GetHDForm(parameterValues[5].text, parameterValues[4].text,
                parameterValues[9].text, parameterValues[10].text, parameterValues[11].text, parameterValues[12].text,
                parameterValues[13].text, parameterValues[14].text, parameterValues[15].text);
                UnityWebRequest createHDPostRequest = UnityWebRequest.Post(ConstStrings.PhpAdditemsFolder + "addnewitemhd.php", hdForm);
                yield return createHDPostRequest.SendWebRequest();

                if (createHDPostRequest.result == UnityWebRequest.Result.ConnectionError)
                {
                    Debug.LogWarning("conectionerror");
                }
                else if (createHDPostRequest.result == UnityWebRequest.Result.DataProcessingError)
                {
                    Debug.LogWarning("data processing error");
                }
                else if (createHDPostRequest.result == UnityWebRequest.Result.ProtocolError)
                {
                    Debug.LogWarning("protocol error");
                }

                if (createHDPostRequest.error == null)
                {

                    string response = createHDPostRequest.downloadHandler.text;
                    if (response == "1" || response == "2" || response == "5")
                    {

                    }
                    else if (response == "3")
                    {

                    }
                    else if (response == "4")
                    {

                    }
                    else
                    {

                    }

                }
                else
                {

                }
                createHDPostRequest.Dispose();
                break;
            #endregion
            #region Memoria
            case 1:
                WWWForm memoriaForm = CreateAddItemForm.GetMemoriaForm(parameterValues[5].text, parameterValues[4].text,
                parameterValues[9].text, parameterValues[10].text, parameterValues[11].text, parameterValues[12].text,
                parameterValues[13].text, parameterValues[14].text, parameterValues[15].text, parameterValues[16].text);
                UnityWebRequest createMemoriaPostRequest = UnityWebRequest.Post(ConstStrings.PhpAdditemsFolder + "addnewitemmemoria.php", memoriaForm);
                yield return createMemoriaPostRequest.SendWebRequest();

                if (createMemoriaPostRequest.result == UnityWebRequest.Result.ConnectionError)
                {
                    Debug.LogWarning("conectionerror");
                }
                else if (createMemoriaPostRequest.result == UnityWebRequest.Result.DataProcessingError)
                {
                    Debug.LogWarning("data processing error");
                }
                else if (createMemoriaPostRequest.result == UnityWebRequest.Result.ProtocolError)
                {
                    Debug.LogWarning("protocol error");
                }

                if (createMemoriaPostRequest.error == null)
                {

                    string response = createMemoriaPostRequest.downloadHandler.text;
                    if (response == "1" || response == "2" || response == "5")
                    {

                    }
                    else if (response == "3")
                    {

                    }
                    else if (response == "4")
                    {

                    }
                    else
                    {

                    }

                }
                else
                {

                }
                createMemoriaPostRequest.Dispose();
                break;
            #endregion
            #region Placa de rede
            case 2:
                WWWForm placaDeRedeForm = CreateAddItemForm.GetPlacaDeRedeForm(parameterValues[5].text,
                parameterValues[4].text, parameterValues[9].text, parameterValues[10].text, parameterValues[11].text,
                parameterValues[12].text, parameterValues[13].text);
                UnityWebRequest createPlacaDeRedePostRequest = UnityWebRequest.Post(ConstStrings.PhpAdditemsFolder + "addnewitemplacarede.php", placaDeRedeForm);
                yield return createPlacaDeRedePostRequest.SendWebRequest();

                if (createPlacaDeRedePostRequest.result == UnityWebRequest.Result.ConnectionError)
                {
                    Debug.LogWarning("conectionerror");
                }
                else if (createPlacaDeRedePostRequest.result == UnityWebRequest.Result.DataProcessingError)
                {
                    Debug.LogWarning("data processing error");
                }
                else if (createPlacaDeRedePostRequest.result == UnityWebRequest.Result.ProtocolError)
                {
                    Debug.LogWarning("protocol error");
                }

                if (createPlacaDeRedePostRequest.error == null)
                {

                    string response = createPlacaDeRedePostRequest.downloadHandler.text;
                    if (response == "1" || response == "2" || response == "5")
                    {

                    }
                    else if (response == "3")
                    {

                    }
                    else if (response == "4")
                    {

                    }
                    else
                    {

                    }

                }
                else
                {

                }
                createPlacaDeRedePostRequest.Dispose();
                break;
            #endregion
            #region iDrac
            case 3:
                WWWForm iDracForm = CreateAddItemForm.GetiDracForm(parameterValues[5].text, parameterValues[4].text,
                parameterValues[9].text, parameterValues[10].text, parameterValues[11].text, parameterValues[12].text);
                UnityWebRequest createiDracPostRequest = UnityWebRequest.Post(ConstStrings.PhpAdditemsFolder + "addnewitemidrac.php", iDracForm);
                yield return createiDracPostRequest.SendWebRequest();

                if (createiDracPostRequest.result == UnityWebRequest.Result.ConnectionError)
                {
                    Debug.LogWarning("conectionerror");
                }
                else if (createiDracPostRequest.result == UnityWebRequest.Result.DataProcessingError)
                {
                    Debug.LogWarning("data processing error");
                }
                else if (createiDracPostRequest.result == UnityWebRequest.Result.ProtocolError)
                {
                    Debug.LogWarning("protocol error");
                }

                if (createiDracPostRequest.error == null)
                {

                    string response = createiDracPostRequest.downloadHandler.text;
                    if (response == "1" || response == "2" || response == "5")
                    {

                    }
                    else if (response == "3")
                    {

                    }
                    else if (response == "4")
                    {

                    }
                    else
                    {

                    }

                }
                else
                {

                }
                createiDracPostRequest.Dispose();
                break;
            #endregion
            #region Placa controladora
            case 4:
                WWWForm placaControladoraForm = CreateAddItemForm.GetPlacaControladoraForm(parameterValues[5].text,
                parameterValues[9].text, parameterValues[10].text, parameterValues[11].text, parameterValues[12].text,
                parameterValues[13].text, parameterValues[14].text, parameterValues[15].text, parameterValues[16].text);
                UnityWebRequest createPlacaControladoraPostRequest = UnityWebRequest.Post(ConstStrings.PhpAdditemsFolder + "addnewitemplacacontroladora.php", placaControladoraForm);
                yield return createPlacaControladoraPostRequest.SendWebRequest();

                if (createPlacaControladoraPostRequest.result == UnityWebRequest.Result.ConnectionError)
                {
                    Debug.LogWarning("conectionerror");
                }
                else if (createPlacaControladoraPostRequest.result == UnityWebRequest.Result.DataProcessingError)
                {
                    Debug.LogWarning("data processing error");
                }
                else if (createPlacaControladoraPostRequest.result == UnityWebRequest.Result.ProtocolError)
                {
                    Debug.LogWarning("protocol error");
                }

                if (createPlacaControladoraPostRequest.error == null)
                {

                    string response = createPlacaControladoraPostRequest.downloadHandler.text;
                    if (response == "1" || response == "2" || response == "5")
                    {

                    }
                    else if (response == "3")
                    {

                    }
                    else if (response == "4")
                    {

                    }
                    else
                    {

                    }

                }
                else
                {

                }
                createPlacaControladoraPostRequest.Dispose();
                break;
            #endregion
            #region Processador
            case 5:
                WWWForm processadorForm = CreateAddItemForm.GetProcessadorForm(parameterValues[5].text,
                parameterValues[9].text, parameterValues[10].text, parameterValues[11].text, parameterValues[12].text,
                parameterValues[13].text, parameterValues[14].text);
                UnityWebRequest createProcessadorPostRequest = UnityWebRequest.Post(ConstStrings.PhpAdditemsFolder + "addnewitemprocessador.php", processadorForm);
                yield return createProcessadorPostRequest.SendWebRequest();

                if (createProcessadorPostRequest.result == UnityWebRequest.Result.ConnectionError)
                {
                    Debug.LogWarning("conectionerror");
                }
                else if (createProcessadorPostRequest.result == UnityWebRequest.Result.DataProcessingError)
                {
                    Debug.LogWarning("data processing error");
                }
                else if (createProcessadorPostRequest.result == UnityWebRequest.Result.ProtocolError)
                {
                    Debug.LogWarning("protocol error");
                }

                if (createProcessadorPostRequest.error == null)
                {

                    string response = createProcessadorPostRequest.downloadHandler.text;
                    if (response == "1" || response == "2" || response == "5")
                    {

                    }
                    else if (response == "3")
                    {

                    }
                    else if (response == "4")
                    {

                    }
                    else
                    {

                    }

                }
                else
                {

                }
                createProcessadorPostRequest.Dispose();
                break;
            #endregion
            #region Desktop
            case 6:
                WWWForm desktopForm = CreateAddItemForm.GetDesktopForm(parameterValues[1].text,
                parameterValues[9].text, parameterValues[10].text, parameterValues[11].text, parameterValues[12].text,
                parameterValues[13].text, parameterValues[14].text, parameterValues[15].text, parameterValues[16].text);
                UnityWebRequest createDesktopPostRequest = UnityWebRequest.Post(ConstStrings.PhpAdditemsFolder + "addnewitemdesktop.php", desktopForm);
                yield return createDesktopPostRequest.SendWebRequest();

                if (createDesktopPostRequest.result == UnityWebRequest.Result.ConnectionError)
                {
                    Debug.LogWarning("conectionerror");
                }
                else if (createDesktopPostRequest.result == UnityWebRequest.Result.DataProcessingError)
                {
                    Debug.LogWarning("data processing error");
                }
                else if (createDesktopPostRequest.result == UnityWebRequest.Result.ProtocolError)
                {
                    Debug.LogWarning("protocol error");
                }

                if (createDesktopPostRequest.error == null)
                {

                    string response = createDesktopPostRequest.downloadHandler.text;
                    if (response == "1" || response == "2" || response == "5")
                    {

                    }
                    else if (response == "3")
                    {

                    }
                    else if (response == "4")
                    {

                    }
                    else
                    {

                    }

                }
                else
                {

                }
                createDesktopPostRequest.Dispose();
                break;
            #endregion
            #region Fonte
            case 7:
                WWWForm fonteForm = CreateAddItemForm.GetFonteForm(parameterValues[5].text,
                parameterValues[9].text, parameterValues[10].text, parameterValues[11].text);
                UnityWebRequest createFontePostRequest = UnityWebRequest.Post(ConstStrings.PhpAdditemsFolder + "addnewitemfonte.php", fonteForm);
                yield return createFontePostRequest.SendWebRequest();

                if (createFontePostRequest.result == UnityWebRequest.Result.ConnectionError)
                {
                    Debug.LogWarning("conectionerror");
                }
                else if (createFontePostRequest.result == UnityWebRequest.Result.DataProcessingError)
                {
                    Debug.LogWarning("data processing error");
                }
                else if (createFontePostRequest.result == UnityWebRequest.Result.ProtocolError)
                {
                    Debug.LogWarning("protocol error");
                }

                if (createFontePostRequest.error == null)
                {

                    string response = createFontePostRequest.downloadHandler.text;
                    if (response == "1" || response == "2" || response == "5")
                    {

                    }
                    else if (response == "3")
                    {

                    }
                    else if (response == "4")
                    {

                    }
                    else
                    {

                    }

                }
                else
                {

                }
                createFontePostRequest.Dispose();
                break;
            #endregion
            #region Switch
            case 8:
                WWWForm switchForm = CreateAddItemForm.GetSwitchForm(parameterValues[5].text, parameterValues[9].text,
                parameterValues[10].text);
                UnityWebRequest createSwitchPostRequest = UnityWebRequest.Post("addnewitemswitch.php", switchForm);
                yield return createSwitchPostRequest.SendWebRequest();

                if (createSwitchPostRequest.result == UnityWebRequest.Result.ConnectionError)
                {
                    Debug.LogWarning("conectionerror");
                }
                else if (createSwitchPostRequest.result == UnityWebRequest.Result.DataProcessingError)
                {
                    Debug.LogWarning("data processing error");
                }
                else if (createSwitchPostRequest.result == UnityWebRequest.Result.ProtocolError)
                {
                    Debug.LogWarning("protocol error");
                }

                if (createSwitchPostRequest.error == null)
                {

                    string response = createSwitchPostRequest.downloadHandler.text;
                    if (response == "1" || response == "2" || response == "5")
                    {

                    }
                    else if (response == "3")
                    {

                    }
                    else if (response == "4")
                    {

                    }
                    else
                    {

                    }

                }
                else
                {

                }
                createSwitchPostRequest.Dispose();
                break;
            #endregion
            #region Roteador
            case 9:
                WWWForm roteadorForm = CreateAddItemForm.GetRoteadorForm(parameterValues[5].text,
                parameterValues[9].text, parameterValues[10].text, parameterValues[11].text, parameterValues[12].text);
                UnityWebRequest createRoteadorPostRequest = UnityWebRequest.Post(ConstStrings.PhpAdditemsFolder + "addnewitemroteador.php", roteadorForm);
                yield return createRoteadorPostRequest.SendWebRequest();

                if (createRoteadorPostRequest.result == UnityWebRequest.Result.ConnectionError)
                {
                    Debug.LogWarning("conectionerror");
                }
                else if (createRoteadorPostRequest.result == UnityWebRequest.Result.DataProcessingError)
                {
                    Debug.LogWarning("data processing error");
                }
                else if (createRoteadorPostRequest.result == UnityWebRequest.Result.ProtocolError)
                {
                    Debug.LogWarning("protocol error");
                }

                if (createRoteadorPostRequest.error == null)
                {

                    string response = createRoteadorPostRequest.downloadHandler.text;
                    if (response == "1" || response == "2" || response == "5")
                    {

                    }
                    else if (response == "3")
                    {

                    }
                    else if (response == "4")
                    {

                    }
                    else
                    {

                    }

                }
                else
                {

                }
                createRoteadorPostRequest.Dispose();
                break;
            #endregion
            #region Carregador
            case 10:
                WWWForm carregadorForm = CreateAddItemForm.GetCarregadorForm(parameterValues[5].text,
                parameterValues[9].text, parameterValues[10].text, parameterValues[11].text);
                UnityWebRequest createCarregadorPostRequest = UnityWebRequest.Post(ConstStrings.PhpAdditemsFolder + "addnewitemcarregador.php", carregadorForm);
                yield return createCarregadorPostRequest.SendWebRequest();

                if (createCarregadorPostRequest.result == UnityWebRequest.Result.ConnectionError)
                {
                    Debug.LogWarning("conectionerror");
                }
                else if (createCarregadorPostRequest.result == UnityWebRequest.Result.DataProcessingError)
                {
                    Debug.LogWarning("data processing error");
                }
                else if (createCarregadorPostRequest.result == UnityWebRequest.Result.ProtocolError)
                {
                    Debug.LogWarning("protocol error");
                }

                if (createCarregadorPostRequest.error == null)
                {

                    string response = createCarregadorPostRequest.downloadHandler.text;
                    if (response == "1" || response == "2" || response == "5")
                    {

                    }
                    else if (response == "3")
                    {

                    }
                    else if (response == "4")
                    {

                    }
                    else
                    {

                    }

                }
                else
                {

                }
                createCarregadorPostRequest.Dispose();
                break;
            #endregion
            #region Adaptador AC
            case 11:
                WWWForm adaptadorACForm = CreateAddItemForm.GetAdaptadorACForm(parameterValues[5].text,
                parameterValues[9].text, parameterValues[10].text, parameterValues[11].text);
                UnityWebRequest createAdaptadorAcPostRequest = UnityWebRequest.Post(ConstStrings.PhpAdditemsFolder + "addnewitemadaptadorac.php", adaptadorACForm);
                yield return createAdaptadorAcPostRequest.SendWebRequest();

                if (createAdaptadorAcPostRequest.result == UnityWebRequest.Result.ConnectionError)
                {
                    Debug.LogWarning("conectionerror");
                }
                else if (createAdaptadorAcPostRequest.result == UnityWebRequest.Result.DataProcessingError)
                {
                    Debug.LogWarning("data processing error");
                }
                else if (createAdaptadorAcPostRequest.result == UnityWebRequest.Result.ProtocolError)
                {
                    Debug.LogWarning("protocol error");
                }

                if (createAdaptadorAcPostRequest.error == null)
                {

                    string response = createAdaptadorAcPostRequest.downloadHandler.text;
                    if (response == "1" || response == "2" || response == "5")
                    {

                    }
                    else if (response == "3")
                    {

                    }
                    else if (response == "4")
                    {

                    }
                    else
                    {

                    }

                }
                else
                {

                }
                createAdaptadorAcPostRequest.Dispose();
                break;
            #endregion
            #region Storage NAS
            case 12:
                WWWForm storageNasForm = CreateAddItemForm.GetStorageNASForm(parameterValues[5].text,
                parameterValues[9].text, parameterValues[10].text, parameterValues[11].text, parameterValues[12].text,
                parameterValues[13].text);
                UnityWebRequest createStorageNasPostRequest = UnityWebRequest.Post(ConstStrings.PhpAdditemsFolder + "addnewitemstoragenas.php", storageNasForm);
                yield return createStorageNasPostRequest.SendWebRequest();

                if (createStorageNasPostRequest.result == UnityWebRequest.Result.ConnectionError)
                {
                    Debug.LogWarning("conectionerror");
                }
                else if (createStorageNasPostRequest.result == UnityWebRequest.Result.DataProcessingError)
                {
                    Debug.LogWarning("data processing error");
                }
                else if (createStorageNasPostRequest.result == UnityWebRequest.Result.ProtocolError)
                {
                    Debug.LogWarning("protocol error");
                }

                if (createStorageNasPostRequest.error == null)
                {

                    string response = createStorageNasPostRequest.downloadHandler.text;
                    if (response == "1" || response == "2" || response == "5")
                    {

                    }
                    else if (response == "3")
                    {

                    }
                    else if (response == "4")
                    {

                    }
                    else
                    {

                    }

                }
                else
                {

                }
                createStorageNasPostRequest.Dispose();
                break;
            #endregion
            #region GBIC
            case 13:
                WWWForm gbicForm = CreateAddItemForm.GetGBICForm(parameterValues[5].text, parameterValues[4].text,
                parameterValues[9].text);
                UnityWebRequest createGbicPostRequest = UnityWebRequest.Post(ConstStrings.PhpAdditemsFolder + "addnewitemgbic.php", gbicForm);
                yield return createGbicPostRequest.SendWebRequest();

                if (createGbicPostRequest.result == UnityWebRequest.Result.ConnectionError)
                {
                    Debug.LogWarning("conectionerror");
                }
                else if (createGbicPostRequest.result == UnityWebRequest.Result.DataProcessingError)
                {
                    Debug.LogWarning("data processing error");
                }
                else if (createGbicPostRequest.result == UnityWebRequest.Result.ProtocolError)
                {
                    Debug.LogWarning("protocol error");
                }

                if (createGbicPostRequest.error == null)
                {

                    string response = createGbicPostRequest.downloadHandler.text;
                    if (response == "1" || response == "2" || response == "5")
                    {

                    }
                    else if (response == "3")
                    {

                    }
                    else if (response == "4")
                    {

                    }
                    else
                    {

                    }

                }
                else
                {

                }
                createGbicPostRequest.Dispose();
                break;
            #endregion
            #region Placa de Video
            case 14:
                WWWForm placaDeVideoForm = CreateAddItemForm.GetPlacaVideoForm(parameterValues[5].text,
                parameterValues[9].text, parameterValues[10].text);
                UnityWebRequest createPlacaDeVideoPostRequest = UnityWebRequest.Post(ConstStrings.PhpAdditemsFolder + "addnewitemplacadevideo.php", placaDeVideoForm);
                yield return createPlacaDeVideoPostRequest.SendWebRequest();

                if (createPlacaDeVideoPostRequest.result == UnityWebRequest.Result.ConnectionError)
                {
                    Debug.LogWarning("conectionerror");
                }
                else if (createPlacaDeVideoPostRequest.result == UnityWebRequest.Result.DataProcessingError)
                {
                    Debug.LogWarning("data processing error");
                }
                else if (createPlacaDeVideoPostRequest.result == UnityWebRequest.Result.ProtocolError)
                {
                    Debug.LogWarning("protocol error");
                }

                if (createPlacaDeVideoPostRequest.error == null)
                {

                    string response = createPlacaDeVideoPostRequest.downloadHandler.text;
                    if (response == "1" || response == "2" || response == "5")
                    {

                    }
                    else if (response == "3")
                    {

                    }
                    else if (response == "4")
                    {

                    }
                    else
                    {

                    }

                }
                else
                {

                }
                createPlacaDeVideoPostRequest.Dispose();
                break;
            #endregion
            #region Placa de som
            case 15:
                WWWForm placaDeSomForm = CreateAddItemForm.GetPlacaSomForm(parameterValues[5].text,
                parameterValues[9].text);
                UnityWebRequest createPlacaDeSomPostRequest = UnityWebRequest.Post(ConstStrings.PhpAdditemsFolder + "addnewitemplacadesom.php", placaDeSomForm);
                yield return createPlacaDeSomPostRequest.SendWebRequest();

                if (createPlacaDeSomPostRequest.result == UnityWebRequest.Result.ConnectionError)
                {
                    Debug.LogWarning("conectionerror");
                }
                else if (createPlacaDeSomPostRequest.result == UnityWebRequest.Result.DataProcessingError)
                {
                    Debug.LogWarning("data processing error");
                }
                else if (createPlacaDeSomPostRequest.result == UnityWebRequest.Result.ProtocolError)
                {
                    Debug.LogWarning("protocol error");
                }

                if (createPlacaDeSomPostRequest.error == null)
                {

                    string response = createPlacaDeSomPostRequest.downloadHandler.text;
                    if (response == "1" || response == "2" || response == "5")
                    {

                    }
                    else if (response == "3")
                    {

                    }
                    else if (response == "4")
                    {

                    }
                    else
                    {

                    }

                }
                else
                {

                }
                createPlacaDeSomPostRequest.Dispose();
                break;
            #endregion
            #region Placa de captura de video
            case 16:
                WWWForm placaDeCapturaDeVideoForm = CreateAddItemForm.GetPlacaCapturaVideoForm(parameterValues[5].text,
                parameterValues[9].text);
                UnityWebRequest createPlacaDeCapturaDeVideoPostRequest = UnityWebRequest.Post("addnewitemplacacapturavideo.php", placaDeCapturaDeVideoForm);
                yield return createPlacaDeCapturaDeVideoPostRequest.SendWebRequest();

                if (createPlacaDeCapturaDeVideoPostRequest.result == UnityWebRequest.Result.ConnectionError)
                {
                    Debug.LogWarning("conectionerror");
                }
                else if (createPlacaDeCapturaDeVideoPostRequest.result == UnityWebRequest.Result.DataProcessingError)
                {
                    Debug.LogWarning("data processing error");
                }
                else if (createPlacaDeCapturaDeVideoPostRequest.result == UnityWebRequest.Result.ProtocolError)
                {
                    Debug.LogWarning("protocol error");
                }

                if (createPlacaDeCapturaDeVideoPostRequest.error == null)
                {

                    string response = createPlacaDeCapturaDeVideoPostRequest.downloadHandler.text;
                    if (response == "1" || response == "2" || response == "5")
                    {

                    }
                    else if (response == "3")
                    {

                    }
                    else if (response == "4")
                    {

                    }
                    else
                    {

                    }

                }
                else
                {

                }
                createPlacaDeCapturaDeVideoPostRequest.Dispose();
                break;
            #endregion
            #region Servidor
            case 17:
                WWWForm servidorForm = CreateAddItemForm.GetServidorForm(parameterValues[5].text,
                parameterValues[4].text);
                UnityWebRequest createServidorPostRequest = UnityWebRequest.Post(ConstStrings.PhpAdditemsFolder + "addnewitemservidor.php", servidorForm);
                yield return createServidorPostRequest.SendWebRequest();

                if (createServidorPostRequest.result == UnityWebRequest.Result.ConnectionError)
                {
                    Debug.LogWarning("conectionerror");
                }
                else if (createServidorPostRequest.result == UnityWebRequest.Result.DataProcessingError)
                {
                    Debug.LogWarning("data processing error");
                }
                else if (createServidorPostRequest.result == UnityWebRequest.Result.ProtocolError)
                {
                    Debug.LogWarning("protocol error");
                }

                if (createServidorPostRequest.error == null)
                {

                    string response = createServidorPostRequest.downloadHandler.text;
                    if (response == "1" || response == "2" || response == "5")
                    {

                    }
                    else if (response == "3")
                    {

                    }
                    else if (response == "4")
                    {

                    }
                    else
                    {

                    }

                }
                else
                {

                }
                createServidorPostRequest.Dispose();
                break;
            #endregion
            #region Notebook
            case 18:
                WWWForm notebookForm = CreateAddItemForm.GetNotebookForm(parameterValues[5].text,
                parameterValues[4].text);
                UnityWebRequest createNotebookPostRequest = UnityWebRequest.Post(ConstStrings.PhpAdditemsFolder + "addnewitemnotebook.php", notebookForm);
                yield return createNotebookPostRequest.SendWebRequest();

                if (createNotebookPostRequest.result == UnityWebRequest.Result.ConnectionError)
                {
                    Debug.LogWarning("conectionerror");
                }
                else if (createNotebookPostRequest.result == UnityWebRequest.Result.DataProcessingError)
                {
                    Debug.LogWarning("data processing error");
                }
                else if (createNotebookPostRequest.result == UnityWebRequest.Result.ProtocolError)
                {
                    Debug.LogWarning("protocol error");
                }

                if (createNotebookPostRequest.error == null)
                {

                    string response = createNotebookPostRequest.downloadHandler.text;
                    if (response == "1" || response == "2" || response == "5")
                    {

                    }
                    else if (response == "3")
                    {

                    }
                    else if (response == "4")
                    {

                    }
                    else
                    {

                    }

                }
                else
                {

                }
                createNotebookPostRequest.Dispose();
                break;
            #endregion
            #region Monitor
            case 19:
                WWWForm monitorForm = CreateAddItemForm.GetMonitorForm(parameterValues[5].text,
                parameterValues[4].text, parameterValues[9].text, parameterValues[10].text);
                UnityWebRequest createMonitorPostRequest = UnityWebRequest.Post(ConstStrings.PhpAdditemsFolder + "adnewitemmonitor.php", monitorForm);
                yield return createMonitorPostRequest.SendWebRequest();

                if (createMonitorPostRequest.result == UnityWebRequest.Result.ConnectionError)
                {
                    Debug.LogWarning("conectionerror");
                }
                else if (createMonitorPostRequest.result == UnityWebRequest.Result.DataProcessingError)
                {
                    Debug.LogWarning("data processing error");
                }
                else if (createMonitorPostRequest.result == UnityWebRequest.Result.ProtocolError)
                {
                    Debug.LogWarning("protocol error");
                }

                if (createMonitorPostRequest.error == null)
                {

                    string response = createMonitorPostRequest.downloadHandler.text;
                    if (response == "1" || response == "2" || response == "5")
                    {

                    }
                    else if (response == "3")
                    {

                    }
                    else if (response == "4")
                    {

                    }
                    else
                    {

                    }

                }
                else
                {

                }
                createMonitorPostRequest.Dispose();
                break;
            #endregion
            default:
                break;
        }
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
}
