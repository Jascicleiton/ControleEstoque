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
    [SerializeField] private TMP_InputField[] parameterValues;
    [SerializeField] private TMP_Dropdown categoryDP;
    [SerializeField] private Button resetButton;
    [SerializeField] private Button returnButton;
    [SerializeField] private Button addButton;
    [SerializeField] private Button addDetailsButton;

    [SerializeField] GameObject messagePanel;
    [SerializeField] TMP_Text messageText;
    [SerializeField] ItemInformationPanelControler itemInformationPanelController;
    private List<string> parameters = new List<string>();

    private void Start()
    {
        if(itemInformationPanelController == null)
        {
            itemInformationPanelController = FindObjectOfType<ItemInformationPanelControler>();
        }
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

    /// <summary>
    /// Set the input enabled or disabled. Called using the Event EnableInput
    /// </summary>
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
        itemInformationPanelController.ShowCategoryItemTemplate(HelperMethods.GetCategoryString(categoryDP.value));
        itemInformationPanelController.DisableItemsForAdd(HelperMethods.GetCategoryString(categoryDP.value));
        EventHandler.CallUpdateTabInputs();
    }

    /// <summary>
    /// Routine used to add a new item to the online database
    /// </summary>
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
            parameters = itemInformationPanelController.GetInventoryValues();
                if (HelperMethods.GetCategoryString(categoryDP.value) == ConstStrings.Outros)
            {
                parameters.Insert(5, parameterValues[5].text);
            }
            else
            {
                parameters.Insert(5, HelperMethods.GetCategoryString(categoryDP.value));
            }
            if(InternalDatabase.Instance.currentEstoque == CurrentEstoque.Concert)
            {
                parameters.Add("");
            }
            else
            {
                parameters.Insert(9, "");
            }
            
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
            EventHandler.CallIsOneMessageOnlyEvent(false);
            #endregion
        }
        else
        {
            EventHandler.CallIsOneMessageOnlyEvent(true);
            addInventarioSuccess = true;
        }
        if (addInventarioSuccess)
        {
            parameters.Clear();
            parameters = itemInformationPanelController.GetCategoryValues(HelperMethods.GetCategoryString(categoryDP.value));

            yield return HelperMethods.AddUpdateItem(categoryDP.value, 2, parameters, false);
            if (HelperMethods.GetAddUpdateResponse())
            {
                addDetalheSuccess = true;
            }
            else
            {
                addDetalheSuccess = false;
            }
            if (addInventario)
            {
              //  AddItem();
            }
        }
        if (!addInventario)
        {
            EventHandler.CallOpenMessageEvent("Worked");
            addInventarioSuccess = true;
        }
    }

    /// <summary>
    /// Close the message. Called by the Event MessageClosed
    /// </summary>
    private void MessageClosed()
    {
        UpdateNames();
        MouseManager.Instance.SetDefaultCursor();
        SetInputEnabled(true);
    }

    /// <summary>
    /// Add a new item to all internal databases
    /// </summary>
    private void AddItem()
    {
        ItemColumns itemToAddFullDatabase = new ItemColumns();
        ItemColumns itemToAddSplitDatabase = new ItemColumns();

        itemToAddFullDatabase.Aquisicao = parameterValues[0].text;
        itemToAddFullDatabase.Entrada = parameterValues[1].text;
        itemToAddFullDatabase.Patrimonio = int.Parse(parameterValues[2].text);
        itemToAddFullDatabase.Status = parameterValues[3].text;
        itemToAddFullDatabase.Serial = parameterValues[4].text;
        itemToAddFullDatabase.Fabricante = parameterValues[6].text;
        itemToAddFullDatabase.Modelo = parameterValues[7].text;
        itemToAddFullDatabase.Local = parameterValues[8].text;
        itemToAddFullDatabase.Saida = "";
        itemToAddFullDatabase.Observacao = parameterValues[10].text;
       

        switch (HelperMethods.GetCategoryString(categoryDP.value))
        {
            #region HD
            case ConstStrings.HD:
                itemToAddFullDatabase.Categoria = ConstStrings.HD;
                InternalDatabase.Instance.splitDatabase[ConstStrings.InventarioSnPro].itens.Add(itemToAddFullDatabase);

                itemToAddFullDatabase.Interface = parameterValues[11].text;
                itemToAddSplitDatabase.Interface = parameterValues[11].text;
                itemToAddFullDatabase.Tamanho = float.Parse(parameterValues[12].text);
                itemToAddSplitDatabase.Tamanho = float.Parse(parameterValues[12].text);
                itemToAddFullDatabase.FormaDeArmazenamento = parameterValues[13].text;
                itemToAddSplitDatabase.FormaDeArmazenamento = parameterValues[13].text;
                itemToAddFullDatabase.CapacidadeEmGB = int.Parse(parameterValues[14].text);
                itemToAddSplitDatabase.CapacidadeEmGB = int.Parse(parameterValues[14].text);
                itemToAddFullDatabase.RPM = int.Parse(parameterValues[15].text);
                itemToAddSplitDatabase.RPM = int.Parse(parameterValues[15].text);
                itemToAddFullDatabase.VelocidadeDeLeitura = float.Parse(parameterValues[16].text);
                itemToAddSplitDatabase.VelocidadeDeLeitura = float.Parse(parameterValues[16].text);
                itemToAddFullDatabase.Enterprise = parameterValues[17].text;
                itemToAddSplitDatabase.Enterprise = parameterValues[17].text;

                InternalDatabase.Instance.splitDatabase[ConstStrings.HD].itens.Add(itemToAddSplitDatabase);
                InternalDatabase.hd.itens.Add(itemToAddFullDatabase);
                break;
            #endregion
            #region Memoria
            case ConstStrings.Memoria:
                itemToAddFullDatabase.Categoria = ConstStrings.Memoria;
                InternalDatabase.Instance.splitDatabase[ConstStrings.InventarioSnPro].itens.Add(itemToAddFullDatabase);
                itemToAddFullDatabase.Tipo = parameterValues[11].text;
                itemToAddSplitDatabase.Tipo = parameterValues[11].text;
                itemToAddFullDatabase.CapacidadeEmGB = int.Parse(parameterValues[12].text);
                itemToAddSplitDatabase.CapacidadeEmGB = int.Parse(parameterValues[12].text);
                itemToAddFullDatabase.VelocidadeMHz = int.Parse(parameterValues[13].text);
                itemToAddSplitDatabase.VelocidadeMHz = int.Parse(parameterValues[13].text);
                itemToAddFullDatabase.LowVoltage = parameterValues[14].text;
                itemToAddSplitDatabase.LowVoltage = parameterValues[14].text;
                itemToAddFullDatabase.Rank = parameterValues[15].text;
                itemToAddSplitDatabase.Rank = parameterValues[15].text;
                itemToAddFullDatabase.DIMM = parameterValues[16].text;
                itemToAddSplitDatabase.DIMM = parameterValues[16].text;
                itemToAddFullDatabase.TaxaDeTransmissao = int.Parse(parameterValues[17].text);
                itemToAddSplitDatabase.TaxaDeTransmissao = int.Parse(parameterValues[17].text);
                itemToAddFullDatabase.Simbolo = parameterValues[18].text;
                itemToAddSplitDatabase.Simbolo = parameterValues[18].text;

                InternalDatabase.Instance.splitDatabase[ConstStrings.Memoria].itens.Add(itemToAddSplitDatabase);
                InternalDatabase.memoria.itens.Add(itemToAddFullDatabase);
                break;
            #endregion
            #region Placa de rede
            case ConstStrings.PlacaDeRede:
                itemToAddFullDatabase.Categoria = ConstStrings.PlacaDeRede;
                InternalDatabase.Instance.splitDatabase[ConstStrings.InventarioSnPro].itens.Add(itemToAddFullDatabase);
                itemToAddFullDatabase.Interface = parameterValues[11].text;
                itemToAddFullDatabase.QuantidadeDePortas = int.Parse(parameterValues[12].text);
                itemToAddFullDatabase.QuaisConexoes = parameterValues[13].text;
                itemToAddFullDatabase.SuportaFibraOptica = parameterValues[14].text;
                itemToAddFullDatabase.Desempenho = parameterValues[15].text;
                itemToAddSplitDatabase.Interface = parameterValues[11].text;
                itemToAddSplitDatabase.QuantidadeDePortas = int.Parse(parameterValues[12].text);
                itemToAddSplitDatabase.QuaisConexoes = parameterValues[13].text;
                itemToAddSplitDatabase.SuportaFibraOptica = parameterValues[14].text;
                itemToAddSplitDatabase.Desempenho = parameterValues[15].text;

                InternalDatabase.Instance.splitDatabase[ConstStrings.PlacaDeRede].itens.Add(itemToAddSplitDatabase);
                InternalDatabase.placaDeRede.itens.Add(itemToAddFullDatabase);
                break;
            #endregion
            #region iDrac
            case ConstStrings.Idrac:
                itemToAddFullDatabase.Categoria = ConstStrings.Idrac;
                InternalDatabase.Instance.splitDatabase[ConstStrings.InventarioSnPro].itens.Add(itemToAddFullDatabase);
                itemToAddFullDatabase.QuaisConexoes = parameterValues[11].text;
                itemToAddFullDatabase.VelocidadeGBs = float.Parse(parameterValues[12].text);
                itemToAddFullDatabase.EntradaSD = parameterValues[13].text;
                itemToAddFullDatabase.ServidoresSuportados = parameterValues[14].text;
                itemToAddSplitDatabase.QuaisConexoes = parameterValues[11].text;
                itemToAddSplitDatabase.VelocidadeGBs = float.Parse(parameterValues[12].text);
                itemToAddSplitDatabase.EntradaSD = parameterValues[13].text;
                itemToAddSplitDatabase.ServidoresSuportados = parameterValues[14].text;

                InternalDatabase.Instance.splitDatabase[ConstStrings.Idrac].itens.Add(itemToAddSplitDatabase);
                InternalDatabase.idrac.itens.Add(itemToAddFullDatabase);
                break;
            #endregion
            #region Placa controladora
            case ConstStrings.PlacaControladora:
                itemToAddFullDatabase.Categoria = ConstStrings.PlacaControladora;
                InternalDatabase.Instance.splitDatabase[ConstStrings.InventarioSnPro].itens.Add(itemToAddFullDatabase);
                itemToAddFullDatabase.QuaisConexoes = parameterValues[11].text;
                itemToAddFullDatabase.QuantidadeDePortas = int.Parse(parameterValues[12].text);
                itemToAddFullDatabase.TipoDeRAID = parameterValues[13].text;
                itemToAddFullDatabase.TipoDeHD = parameterValues[14].text;
                itemToAddFullDatabase.CapacidadeMaxHD = parameterValues[15].text;
                itemToAddFullDatabase.AteQuantosHDs = int.Parse(parameterValues[16].text);
                itemToAddFullDatabase.BateriaInclusa = parameterValues[17].text;
                itemToAddFullDatabase.Barramento = parameterValues[18].text;
                itemToAddSplitDatabase.QuaisConexoes = parameterValues[11].text;
                itemToAddSplitDatabase.QuantidadeDePortas = int.Parse(parameterValues[12].text);
                itemToAddSplitDatabase.TipoDeRAID = parameterValues[13].text;
                itemToAddSplitDatabase.TipoDeHD = parameterValues[14].text;
                itemToAddSplitDatabase.CapacidadeMaxHD = parameterValues[15].text;
                itemToAddSplitDatabase.AteQuantosHDs = int.Parse(parameterValues[16].text);
                itemToAddSplitDatabase.BateriaInclusa = parameterValues[17].text;
                itemToAddSplitDatabase.Barramento = parameterValues[18].text;

                InternalDatabase.Instance.splitDatabase[ConstStrings.PlacaControladora].itens.Add(itemToAddSplitDatabase);
                InternalDatabase.placaControladora.itens.Add(itemToAddFullDatabase);
                break;
            #endregion
            #region Processador
            case ConstStrings.Processador:
                itemToAddFullDatabase.Categoria = ConstStrings.Processador;
                InternalDatabase.Instance.splitDatabase[ConstStrings.InventarioSnPro].itens.Add(itemToAddFullDatabase);
                itemToAddFullDatabase.Soquete = parameterValues[11].text;
                itemToAddFullDatabase.NucleosFisicos = int.Parse(parameterValues[12].text);
                itemToAddFullDatabase.NucleosLogicos = int.Parse(parameterValues[13].text);
                itemToAddFullDatabase.AceitaVirtualizacao = parameterValues[14].text;
                itemToAddFullDatabase.TurboBoost = parameterValues[15].text;
                itemToAddFullDatabase.HyperThreading = parameterValues[16].text;
                itemToAddSplitDatabase.Soquete = parameterValues[11].text;
                itemToAddSplitDatabase.NucleosFisicos = int.Parse(parameterValues[12].text);
                itemToAddSplitDatabase.NucleosLogicos = int.Parse(parameterValues[13].text);
                itemToAddSplitDatabase.AceitaVirtualizacao = parameterValues[14].text;
                itemToAddSplitDatabase.TurboBoost = parameterValues[15].text;
                itemToAddSplitDatabase.HyperThreading = parameterValues[16].text;

                InternalDatabase.Instance.splitDatabase[ConstStrings.Processador].itens.Add(itemToAddSplitDatabase);
                InternalDatabase.processador.itens.Add(itemToAddFullDatabase);
                break;
            #endregion
            #region Desktop
            case ConstStrings.Desktop:
                itemToAddFullDatabase.Categoria = ConstStrings.Desktop;
                InternalDatabase.Instance.splitDatabase[ConstStrings.InventarioSnPro].itens.Add(itemToAddFullDatabase);
                itemToAddFullDatabase.ModeloPlacaMae = parameterValues[11].text;
                itemToAddFullDatabase.Fonte = parameterValues[12].text;
                itemToAddFullDatabase.Memoria = parameterValues[13].text;
                itemToAddFullDatabase.HD = parameterValues[14].text;
                itemToAddFullDatabase.PlacaDeVideo = parameterValues[15].text;
                itemToAddFullDatabase.PlacaDeRede = parameterValues[16].text;
                itemToAddFullDatabase.LeitorDeDVD = parameterValues[17].text;
                itemToAddFullDatabase.Processador = parameterValues[18].text;
                itemToAddSplitDatabase.Patrimonio = int.Parse(parameterValues[2].text);
                itemToAddSplitDatabase.ModeloPlacaMae = parameterValues[11].text;
                itemToAddSplitDatabase.Fonte = parameterValues[12].text;
                itemToAddSplitDatabase.Memoria = parameterValues[13].text;
                itemToAddSplitDatabase.HD = parameterValues[14].text;
                itemToAddSplitDatabase.PlacaDeVideo = parameterValues[15].text;
                itemToAddSplitDatabase.PlacaDeRede = parameterValues[16].text;
                itemToAddSplitDatabase.LeitorDeDVD = parameterValues[17].text;
                itemToAddSplitDatabase.Processador = parameterValues[18].text;

                InternalDatabase.Instance.splitDatabase[ConstStrings.Desktop].itens.Add(itemToAddSplitDatabase);
                InternalDatabase.desktop.itens.Add(itemToAddFullDatabase);
                break;
            #endregion
            #region Fonte
            case ConstStrings.Fonte:
                itemToAddFullDatabase.Categoria = ConstStrings.Fonte;
                InternalDatabase.Instance.splitDatabase[ConstStrings.InventarioSnPro].itens.Add(itemToAddFullDatabase);
                itemToAddFullDatabase.Watts = int.Parse(parameterValues[11].text);
                itemToAddFullDatabase.OndeFunciona = parameterValues[12].text;
                itemToAddFullDatabase.Conectores = parameterValues[13].text;
                itemToAddSplitDatabase.Watts = int.Parse(parameterValues[11].text);
                itemToAddSplitDatabase.OndeFunciona = parameterValues[12].text;
                itemToAddSplitDatabase.Conectores = parameterValues[13].text;

                InternalDatabase.Instance.splitDatabase[ConstStrings.Fonte].itens.Add(itemToAddSplitDatabase);
                InternalDatabase.fonte.itens.Add(itemToAddFullDatabase);
                break;
            #endregion
            #region Switch
            case ConstStrings.Switch:
                itemToAddFullDatabase.Categoria = ConstStrings.Switch;
                InternalDatabase.Instance.splitDatabase[ConstStrings.InventarioSnPro].itens.Add(itemToAddFullDatabase);
                itemToAddFullDatabase.QuantidadeDePortas = int.Parse(parameterValues[11].text);
                itemToAddFullDatabase.Desempenho = parameterValues[12].text;
                itemToAddSplitDatabase.QuantidadeDePortas = int.Parse(parameterValues[11].text);
                itemToAddSplitDatabase.Desempenho = parameterValues[12].text;

                InternalDatabase.Instance.splitDatabase[ConstStrings.Switch].itens.Add(itemToAddSplitDatabase);
                InternalDatabase.Switch.itens.Add(itemToAddFullDatabase);
                break;
            #endregion
            #region Roteador
            case ConstStrings.Roteador:
                itemToAddFullDatabase.Categoria = ConstStrings.Roteador;
                InternalDatabase.Instance.splitDatabase[ConstStrings.InventarioSnPro].itens.Add(itemToAddFullDatabase);
                itemToAddFullDatabase.Wireless = parameterValues[11].text;
                itemToAddFullDatabase.QuantidadeDePortas = int.Parse(parameterValues[12].text);
                itemToAddFullDatabase.BandaMaxima = int.Parse(parameterValues[13].text);
                itemToAddFullDatabase.VoltagemDeSaida = float.Parse(parameterValues[14].text);
                itemToAddSplitDatabase.Wireless = parameterValues[11].text;
                itemToAddSplitDatabase.QuantidadeDePortas = int.Parse(parameterValues[12].text);
                itemToAddSplitDatabase.BandaMaxima = int.Parse(parameterValues[13].text);
                itemToAddSplitDatabase.VoltagemDeSaida = int.Parse(parameterValues[14].text);

                InternalDatabase.Instance.splitDatabase[ConstStrings.Roteador].itens.Add(itemToAddSplitDatabase);
                InternalDatabase.roteador.itens.Add(itemToAddFullDatabase);
                break;
            #endregion
            #region Carregador
            case ConstStrings.Carregador:
                itemToAddFullDatabase.Categoria = ConstStrings.Carregador;
                InternalDatabase.Instance.splitDatabase[ConstStrings.InventarioSnPro].itens.Add(itemToAddFullDatabase);
                itemToAddFullDatabase.OndeFunciona = parameterValues[11].text;
                itemToAddFullDatabase.VoltagemDeSaida = float.Parse(parameterValues[12].text);
                itemToAddFullDatabase.AmperagemDeSaida = float.Parse(parameterValues[13].text);
                itemToAddSplitDatabase.OndeFunciona = parameterValues[11].text;
                itemToAddSplitDatabase.VoltagemDeSaida = float.Parse(parameterValues[12].text);
                itemToAddSplitDatabase.AmperagemDeSaida = float.Parse(parameterValues[13].text);

                InternalDatabase.Instance.splitDatabase[ConstStrings.Carregador].itens.Add(itemToAddSplitDatabase);
                InternalDatabase.carregador.itens.Add(itemToAddFullDatabase);
                break;
            #endregion
            #region Adaptador AC
            case ConstStrings.AdaptadorAC:
                itemToAddFullDatabase.Categoria = ConstStrings.AdaptadorAC;
                InternalDatabase.Instance.splitDatabase[ConstStrings.InventarioSnPro].itens.Add(itemToAddFullDatabase);
                itemToAddFullDatabase.OndeFunciona = parameterValues[11].text;
                itemToAddFullDatabase.VoltagemDeSaida = float.Parse(parameterValues[12].text);
                itemToAddFullDatabase.AmperagemDeSaida = float.Parse(parameterValues[13].text);
                itemToAddSplitDatabase.OndeFunciona = parameterValues[11].text;
                itemToAddSplitDatabase.VoltagemDeSaida = float.Parse(parameterValues[12].text);
                itemToAddSplitDatabase.AmperagemDeSaida = float.Parse(parameterValues[13].text);

                InternalDatabase.Instance.splitDatabase[ConstStrings.AdaptadorAC].itens.Add(itemToAddSplitDatabase);
                InternalDatabase.adaptadorAC.itens.Add(itemToAddFullDatabase);
                break;
            #endregion
            #region Storage NAS
            case ConstStrings.StorageNAS:
                itemToAddFullDatabase.Categoria = ConstStrings.StorageNAS;
                InternalDatabase.Instance.splitDatabase[ConstStrings.InventarioSnPro].itens.Add(itemToAddFullDatabase);
                itemToAddFullDatabase.Tamanho = float.Parse(parameterValues[11].text);
                itemToAddFullDatabase.TipoDeRAID = parameterValues[12].text;
                itemToAddFullDatabase.TipoDeHD = parameterValues[13].text;
                itemToAddFullDatabase.CapacidadeMaxHD = parameterValues[14].text;
                itemToAddFullDatabase.AteQuantosHDs = int.Parse(parameterValues[15].text);
                itemToAddSplitDatabase.Tamanho = float.Parse(parameterValues[11].text);
                itemToAddSplitDatabase.TipoDeRAID = parameterValues[12].text;
                itemToAddSplitDatabase.TipoDeHD = parameterValues[13].text;
                itemToAddSplitDatabase.CapacidadeMaxHD = parameterValues[14].text;
                itemToAddSplitDatabase.AteQuantosHDs = int.Parse(parameterValues[15].text);

                InternalDatabase.Instance.splitDatabase[ConstStrings.StorageNAS].itens.Add(itemToAddSplitDatabase);
                InternalDatabase.storageNAS.itens.Add(itemToAddFullDatabase);
                break;
            #endregion
            #region GBIC
            case ConstStrings.Gbic:
                itemToAddFullDatabase.Categoria = ConstStrings.Gbic;
                InternalDatabase.Instance.splitDatabase[ConstStrings.InventarioSnPro].itens.Add(itemToAddFullDatabase);
                itemToAddFullDatabase.Desempenho = parameterValues[11].text;
                itemToAddSplitDatabase.Desempenho = parameterValues[11].text;

                InternalDatabase.Instance.splitDatabase[ConstStrings.Gbic].itens.Add(itemToAddSplitDatabase);
                InternalDatabase.gbic.itens.Add(itemToAddFullDatabase);
                break;
            #endregion
            #region Placa de Video
            case ConstStrings.PlacaDeVideo:
                itemToAddFullDatabase.Categoria = ConstStrings.PlacaDeVideo;
                InternalDatabase.Instance.splitDatabase[ConstStrings.InventarioSnPro].itens.Add(itemToAddFullDatabase);
                itemToAddFullDatabase.QuantidadeDePortas = int.Parse(parameterValues[11].text);
                itemToAddFullDatabase.QuaisConexoes = parameterValues[12].text;
                itemToAddSplitDatabase.QuantidadeDePortas = int.Parse(parameterValues[11].text);
                itemToAddSplitDatabase.QuaisConexoes = parameterValues[12].text;

                InternalDatabase.Instance.splitDatabase[ConstStrings.PlacaDeVideo].itens.Add(itemToAddSplitDatabase);
                InternalDatabase.placaDeVideo.itens.Add(itemToAddFullDatabase);
                break;
            #endregion
            #region Placa de som
            case ConstStrings.PlacaDeSom:
                itemToAddFullDatabase.Categoria = ConstStrings.PlacaDeSom;
                InternalDatabase.Instance.splitDatabase[ConstStrings.InventarioSnPro].itens.Add(itemToAddFullDatabase);
                itemToAddFullDatabase.QuantosCanais = int.Parse(parameterValues[11].text);
                itemToAddSplitDatabase.QuantosCanais = int.Parse(parameterValues[11].text);

                InternalDatabase.Instance.splitDatabase[ConstStrings.PlacaDeSom].itens.Add(itemToAddSplitDatabase);
                InternalDatabase.placaDeSom.itens.Add(itemToAddFullDatabase);
                break;
            #endregion
            #region Placa de captura de video
            case ConstStrings.PlacaDeCapturaDeVideo:
                itemToAddFullDatabase.Categoria = ConstStrings.PlacaDeCapturaDeVideo;
                InternalDatabase.Instance.splitDatabase[ConstStrings.InventarioSnPro].itens.Add(itemToAddFullDatabase);
                itemToAddFullDatabase.QuantidadeDePortas = int.Parse(parameterValues[11].text);
                itemToAddSplitDatabase.QuantidadeDePortas = int.Parse(parameterValues[11].text);

                InternalDatabase.Instance.splitDatabase[ConstStrings.PlacaDeCapturaDeVideo].itens.Add(itemToAddSplitDatabase);
                InternalDatabase.placaDeCapturaDeVideo.itens.Add(itemToAddFullDatabase);
                break;
            #endregion
            #region Servidor
            case ConstStrings.Servidor:
                itemToAddFullDatabase.Categoria = ConstStrings.Servidor;
                InternalDatabase.Instance.splitDatabase[ConstStrings.InventarioSnPro].itens.Add(itemToAddFullDatabase);
                itemToAddFullDatabase.ModeloPlacaMae = parameterValues[11].text;
                itemToAddFullDatabase.Fonte = parameterValues[12].text;
                itemToAddFullDatabase.Memoria = parameterValues[13].text;
                itemToAddFullDatabase.HD = parameterValues[14].text;
                itemToAddFullDatabase.PlacaDeVideo = parameterValues[15].text;
                itemToAddFullDatabase.PlacaDeRede = parameterValues[16].text;
                itemToAddFullDatabase.Processador = parameterValues[17].text;
                itemToAddFullDatabase.MemoriasSuportadas = parameterValues[18].text;
                itemToAddFullDatabase.QuantasMemorias = int.Parse(parameterValues[19].text);
                itemToAddFullDatabase.OrdemDasMemorias = parameterValues[20].text;
                itemToAddFullDatabase.CapacidadeRAMTotal = int.Parse(parameterValues[21].text);
                itemToAddFullDatabase.Soquete = parameterValues[22].text;
                itemToAddFullDatabase.PlacaControladora = parameterValues[23].text;
                itemToAddFullDatabase.AteQuantosHDs = int.Parse(parameterValues[24].text);
                itemToAddFullDatabase.TipoDeHD = parameterValues[25].text;
                itemToAddFullDatabase.TipoDeRAID = parameterValues[26].text;
                itemToAddSplitDatabase.Patrimonio = int.Parse(parameterValues[2].text);
                itemToAddSplitDatabase.Modelo = parameterValues[7].text;
                itemToAddSplitDatabase.Fabricante = parameterValues[6].text;
                itemToAddSplitDatabase.ModeloPlacaMae = parameterValues[11].text;
                itemToAddSplitDatabase.Fonte = parameterValues[12].text;
                itemToAddSplitDatabase.Memoria = parameterValues[13].text;
                itemToAddSplitDatabase.HD = parameterValues[14].text;
                itemToAddSplitDatabase.PlacaDeVideo = parameterValues[15].text;
                itemToAddSplitDatabase.PlacaDeRede = parameterValues[16].text;
                itemToAddSplitDatabase.Processador = parameterValues[17].text;
                itemToAddSplitDatabase.MemoriasSuportadas = parameterValues[18].text;
                itemToAddSplitDatabase.QuantasMemorias = int.Parse(parameterValues[19].text);
                itemToAddSplitDatabase.OrdemDasMemorias = parameterValues[20].text;
                itemToAddSplitDatabase.CapacidadeRAMTotal = int.Parse(parameterValues[21].text);
                itemToAddSplitDatabase.Soquete = parameterValues[22].text;
                itemToAddSplitDatabase.PlacaControladora = parameterValues[23].text;
                itemToAddSplitDatabase.AteQuantosHDs = int.Parse(parameterValues[24].text);
                itemToAddSplitDatabase.TipoDeHD = parameterValues[25].text;
                itemToAddSplitDatabase.TipoDeRAID = parameterValues[26].text;

                InternalDatabase.Instance.splitDatabase[ConstStrings.Servidor].itens.Add(itemToAddSplitDatabase);
                InternalDatabase.servidor.itens.Add(itemToAddFullDatabase);
                break;
            #endregion
            #region Notebook
            case ConstStrings.Notebook:
                itemToAddFullDatabase.Categoria = ConstStrings.Notebook;
                InternalDatabase.Instance.splitDatabase[ConstStrings.InventarioSnPro].itens.Add(itemToAddFullDatabase);
                itemToAddFullDatabase.HD = parameterValues[11].text;
                itemToAddFullDatabase.Memoria = parameterValues[12].text;
                itemToAddFullDatabase.EntradaRJ45 = parameterValues[13].text;
                itemToAddFullDatabase.BateriaInclusa = parameterValues[14].text;
                itemToAddFullDatabase.AdaptadorAC = parameterValues[15].text;
                itemToAddFullDatabase.Windows = parameterValues[16].text;

                itemToAddSplitDatabase.Patrimonio = int.Parse(parameterValues[2].text);
                itemToAddSplitDatabase.Modelo = parameterValues[7].text;
                itemToAddSplitDatabase.Fabricante = parameterValues[6].text;
                itemToAddSplitDatabase.HD = parameterValues[11].text;
                itemToAddSplitDatabase.Memoria = parameterValues[12].text;
                itemToAddSplitDatabase.EntradaRJ45 = parameterValues[13].text;
                itemToAddSplitDatabase.BateriaInclusa = parameterValues[14].text;
                itemToAddSplitDatabase.AdaptadorAC = parameterValues[15].text;
                itemToAddSplitDatabase.Windows = parameterValues[16].text;

                InternalDatabase.Instance.splitDatabase[ConstStrings.Notebook].itens.Add(itemToAddSplitDatabase);
                InternalDatabase.notebook.itens.Add(itemToAddFullDatabase);
                break;
            #endregion
            #region Monitor
            case ConstStrings.Monitor:
                itemToAddFullDatabase.Categoria = ConstStrings.Monitor;
                InternalDatabase.Instance.splitDatabase[ConstStrings.InventarioSnPro].itens.Add(itemToAddFullDatabase);
                itemToAddFullDatabase.Polegadas = float.Parse(parameterValues[11].text);
                itemToAddSplitDatabase.Polegadas = float.Parse(parameterValues[11].text);
                itemToAddFullDatabase.QuaisConexoes = parameterValues[12].text;
                itemToAddSplitDatabase.QuaisConexoes = parameterValues[12].text;

                InternalDatabase.Instance.splitDatabase[ConstStrings.Monitor].itens.Add(itemToAddSplitDatabase);
                InternalDatabase.monitor.itens.Add(itemToAddFullDatabase);
                break;
            #endregion
            #region Outros
            default:
                itemToAddFullDatabase.Categoria = parameterValues[5].text;
                itemToAddSplitDatabase.Categoria = parameterValues[5].text;
                InternalDatabase.Instance.splitDatabase[ConstStrings.Outros].itens.Add(itemToAddSplitDatabase);
                InternalDatabase.outros.itens.Add(itemToAddFullDatabase);
                break;
                #endregion
        }
        InternalDatabase.Instance.fullDatabase.itens.Add(itemToAddFullDatabase);
        // ShowMessage();
       // EventHandler.CallDatabaseUpdatedEvent(ConstStrings.DataDatabaseSaveFile);
    }

    /// <summary>
    /// Called when the AddItem Button is clicked
    /// </summary>
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

    /// <summary>
    /// Resets all inputs to default balues
    /// </summary>
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
