using System.Collections;
using System.Collections.Generic;
using System.Data;
using TMPro;
using UnityEngine;
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
            case 0:
                parameterNames[9].text = "Interface";
                parameterNames[10].text = "Tamanho";
                parameterNames[11].text = "Forma de armazenamento";
                parameterNames[12].text = "Capacidade (GB)";
                parameterNames[13].text = "RPM";
                parameterNames[14].text = "Velocidade de Leitura (Gb/s)";
                parameterNames[15].text = "Enterprise";
                parameterNames[16].text = "";
                break;
            case 1:
                parameterNames[9].text = "Tipo";
                parameterNames[10].text = "Capacidade (GB)";
                parameterNames[11].text = "Velocidade (MHz)";
                parameterNames[12].text = "É low voltage?";
                parameterNames[13].text = "Rank";
                parameterNames[14].text = "DIMM";
                parameterNames[15].text = "Taxa de transmissão";
                parameterNames[16].text = "Símbolo";
                break;
            case 2:
                parameterNames[9].text = "Interface";
                parameterNames[10].text = "Quantas portas?";
                parameterNames[11].text = "Quais portas?";
                parameterNames[12].text = "Suporta fibra óptica?";
                parameterNames[13].text = "Desempenho (MB/s)";
                parameterNames[14].text = "";
                parameterNames[15].text = "";
                parameterNames[16].text = "";
                break;
            case 3:
                parameterNames[9].text = "Porta";
                parameterNames[10].text = "Velocidade (GB/s)";
                parameterNames[11].text = "Entrada SD";
                parameterNames[12].text = "Servidores suportados";
                parameterNames[13].text = "";
                parameterNames[14].text = "";
                parameterNames[15].text = "";
                parameterNames[16].text = "";
                break;
            case 4:
                parameterNames[9].text = "Tipo de conexão";
                parameterNames[10].text = "Quantas portas?";
                parameterNames[11].text = "Tipos de RAID";
                parameterNames[12].text = "Capacidade máx do HD (TB)";
                parameterNames[13].text = "Até quantos HDs";
                parameterNames[14].text = "Bateria inclusa?";
                parameterNames[15].text = "Barramento";
                parameterNames[16].text = "";
                break;
            case 5:
                parameterNames[9].text = "Soquete";
                parameterNames[10].text = "Nº núcleos físicos";
                parameterNames[11].text = "Nº núcleos lógicos";
                parameterNames[12].text = "Aceita virtualização?";
                parameterNames[13].text = "Turbo boost?";
                parameterNames[14].text = "Hyper-Threading?";
                parameterNames[15].text = "";
                parameterNames[16].text = "";
                break;
            case 6:
                parameterNames[9].text = "Modelo de placa mãe";
                parameterNames[10].text = "Fonte?";
                parameterNames[11].text = "Memória?";
                parameterNames[12].text = "HD?";
                parameterNames[13].text = "Placa de vídeo?";
                parameterNames[14].text = "Leitor de DVD?";
                parameterNames[15].text = "";
                parameterNames[16].text = "";
                break;
            case 7:
                parameterNames[9].text = "Watts de potência";
                parameterNames[10].text = "Onde funciona?";
                parameterNames[11].text = "Conectores";
                parameterNames[12].text = "";
                parameterNames[13].text = "";
                parameterNames[14].text = "";
                parameterNames[15].text = "";
                parameterNames[16].text = "";
                break;
            case 8:
                parameterNames[9].text = "Quantas entradas";
                parameterNames[10].text = "Capacidade máx de cada porta (MB/s)";
                parameterNames[11].text = "";
                parameterNames[12].text = "";
                parameterNames[13].text = "";
                parameterNames[14].text = "";
                parameterNames[15].text = "";
                parameterNames[16].text = "";
                break;
            case 9:
                parameterNames[9].text = "Wireless?";
                parameterNames[10].text = "Quantas entradas?";
                parameterNames[11].text = "Banda máx (MB/s)";
                parameterNames[12].text = "";
                parameterNames[13].text = "";
                parameterNames[14].text = "";
                parameterNames[15].text = "";
                parameterNames[16].text = "";
                break;
            case 10:
                parameterNames[9].text = "Onde funciona?";
                parameterNames[10].text = "Voltagem de saída";
                parameterNames[11].text = "Amperagem de saída (mA)";
                parameterNames[12].text = "";
                parameterNames[13].text = "";
                parameterNames[14].text = "";
                parameterNames[15].text = "";
                parameterNames[16].text = "";
                break;
            case 11:
                parameterNames[9].text = "Onde funciona?";
                parameterNames[10].text = "Voltagem de saída";
                parameterNames[11].text = "Amperagem de saída (A)";
                parameterNames[12].text = "";
                parameterNames[13].text = "";
                parameterNames[14].text = "";
                parameterNames[15].text = "";
                parameterNames[16].text = "";
                break;
            case 12:
                parameterNames[9].text = "Tamanho dos HDs";
                parameterNames[10].text = "Tipos de RAID";
                parameterNames[11].text = "Tipo de HD";
                parameterNames[12].text = "Capacidade máx do HD";
                parameterNames[13].text = "Até quantos HDs";
                parameterNames[14].text = "";
                parameterNames[15].text = "";
                parameterNames[16].text = "";
                break;
            case 13:
                parameterNames[9].text = "Desempenho máx (GB/s)";
                parameterNames[10].text = "";
                parameterNames[11].text = "";
                                parameterNames[12].text = "";
                parameterNames[13].text = "";
                parameterNames[14].text = "";
                parameterNames[15].text = "";
                parameterNames[16].text = "";
                break;
            case 14:
                parameterNames[9].text = "Quantas entradas?";
                parameterNames[10].text = "Quais entradas?";
                parameterNames[11].text = "";
                parameterNames[12].text = "";
                parameterNames[13].text = "";
                parameterNames[14].text = "";
                parameterNames[15].text = "";
                parameterNames[16].text = "";
                break;
            case 15:
                parameterNames[9].text = "Quantos canais?";
                parameterNames[10].text = "";
                parameterNames[11].text = "";
                parameterNames[12].text = "";
                parameterNames[13].text = "";
                parameterNames[14].text = "";
                parameterNames[15].text = "";
                parameterNames[16].text = "";
                break;
            case 16:
                parameterNames[9].text = "Quantas entradas?";
                parameterNames[10].text = "";
                parameterNames[11].text = "";
                parameterNames[12].text = "";
                parameterNames[13].text = "";
                parameterNames[14].text = "";
                parameterNames[15].text = "";
                parameterNames[16].text = "";
                break;

            default:
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
                itemToAddFullDatabase.CapacidadeMaxHD = parameterValues[12].text;
                itemToAddFullDatabase.AteQuantosHDs = parameterValues[13].text;
                itemToAddFullDatabase.BateriaInclusa = parameterValues[14].text;
                itemToAddFullDatabase.Barramento = parameterValues[15].text;
                itemToAddSplitDatabase.QuaisConexoes = parameterValues[9].text;
                itemToAddSplitDatabase.QuantidadeDePortas = parameterValues[10].text;
                itemToAddSplitDatabase.TipoDeRAID = parameterValues[11].text;
                itemToAddSplitDatabase.CapacidadeMaxHD = parameterValues[12].text;
                itemToAddSplitDatabase.AteQuantosHDs = parameterValues[13].text;
                itemToAddSplitDatabase.BateriaInclusa = parameterValues[14].text;
                itemToAddSplitDatabase.Barramento = parameterValues[15].text;

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
                itemToAddFullDatabase.LeitorDeDVD = parameterValues[14].text;
                itemToAddSplitDatabase.Patrimonio = parameterValues[1].text;
                itemToAddSplitDatabase.ModeloPlacaMae = parameterValues[9].text;
                itemToAddSplitDatabase.Fonte = parameterValues[10].text;
                itemToAddSplitDatabase.Memoria = parameterValues[11].text;
                itemToAddSplitDatabase.HD = parameterValues[12].text;
                itemToAddSplitDatabase.PlacaDeVideo = parameterValues[13].text;
                itemToAddSplitDatabase.LeitorDeDVD = parameterValues[14].text;

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
                itemToAddSplitDatabase.Wireless = parameterValues[9].text;
                itemToAddSplitDatabase.QuantidadeDePortas = parameterValues[10].text;
                itemToAddSplitDatabase.BandaMaxima = parameterValues[11].text;

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
                itemToAddSplitDatabase.Tamanho = parameterValues[9].text;
                itemToAddSplitDatabase.TipoDeRAID = parameterValues[10].text;
                itemToAddSplitDatabase.TipoDeHD = parameterValues[11].text;
                itemToAddSplitDatabase.CapacidadeMaxHD = parameterValues[12].text;

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
            default:
                break;
        }
        InternalDatabase.Instance.fullDatabase.itens.Add(itemToAddFullDatabase);
        ShowMessage();
        EventHandler.CallDatabaseUpdatedEvent(ConstStrings.DataDatabaseSaveFile);
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
