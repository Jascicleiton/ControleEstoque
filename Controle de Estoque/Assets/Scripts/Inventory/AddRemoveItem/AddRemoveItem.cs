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
    //private void AddItem()
    //{
    //    ItemColumns itemToAddFullDatabase = new ItemColumns();
    //    ItemColumns itemToAddSplitDatabase = new ItemColumns();

    //    itemToAddFullDatabase.Aquisicao = parameterValues[0].text;
    //    itemToAddFullDatabase.Entrada = parameterValues[1].text;
    //    if (!int.TryParse(parameterValues[2].text, out itemToAddFullDatabase.Patrimonio))
    //    {
    //        itemToAddFullDatabase.Patrimonio = -666;
    //    }
    //    itemToAddFullDatabase.Status = parameterValues[3].text;
    //    itemToAddFullDatabase.Serial = parameterValues[4].text;
    //    itemToAddFullDatabase.Fabricante = parameterValues[6].text;
    //    itemToAddFullDatabase.Modelo = parameterValues[7].text;
    //    itemToAddFullDatabase.Local = parameterValues[8].text;
    //    itemToAddFullDatabase.Saida = "";
    //    itemToAddFullDatabase.Observacao = parameterValues[10].text;
       

    //    switch (HelperMethods.GetCategoryString(categoryDP.value))
    //    {
    //        #region HD
    //        case ConstStrings.HD:
    //            itemToAddFullDatabase.Categoria = ConstStrings.HD;
    //            InternalDatabase.Instance.splitDatabase[ConstStrings.InventarioSnPro].itens.Add(itemToAddFullDatabase);

    //            itemToAddFullDatabase.Interface = parameterValues[11].text;
    //            itemToAddSplitDatabase.Interface = parameterValues[11].text;
    //            if(!float.TryParse(parameterValues[12].text, out itemToAddFullDatabase.Tamanho))
    //            {
    //                itemToAddFullDatabase.Tamanho = 0f;
    //                itemToAddSplitDatabase.Tamanho = 0f;
    //            }
    //            else
    //            {
    //                itemToAddSplitDatabase.Tamanho = itemToAddFullDatabase.Tamanho;
    //            }
    //            itemToAddFullDatabase.FormaDeArmazenamento = parameterValues[13].text;
    //            itemToAddSplitDatabase.FormaDeArmazenamento = parameterValues[13].text;
    //            if(!int.TryParse(parameterValues[14].text, out itemToAddFullDatabase.CapacidadeEmGB))
    //                {
    //                itemToAddFullDatabase.CapacidadeEmGB = 0;
    //                itemToAddSplitDatabase.CapacidadeEmGB = 0;
    //            }
    //            else
    //            {
    //                itemToAddSplitDatabase.CapacidadeEmGB = itemToAddFullDatabase.CapacidadeEmGB;
    //            }
    //            if(!int.TryParse(parameterValues[15].text, out itemToAddFullDatabase.RPM))
    //            {
    //                itemToAddFullDatabase.RPM = 0;
    //                itemToAddSplitDatabase.RPM = 0;
    //            }
    //            else
    //            {
    //                itemToAddSplitDatabase.RPM = itemToAddFullDatabase.RPM;
    //            }
    //            if(!float.TryParse(parameterValues[16].text, out itemToAddFullDatabase.VelocidadeDeLeitura))
    //            {
    //                itemToAddFullDatabase.VelocidadeDeLeitura = 0f;
    //                itemToAddSplitDatabase.VelocidadeDeLeitura = 0f;
    //            }
    //            else
    //            {
    //                itemToAddSplitDatabase.VelocidadeDeLeitura = itemToAddFullDatabase.VelocidadeDeLeitura;
    //            }
    //            itemToAddFullDatabase.Enterprise = parameterValues[17].text;
    //            itemToAddSplitDatabase.Enterprise = parameterValues[17].text;

    //            InternalDatabase.Instance.splitDatabase[ConstStrings.HD].itens.Add(itemToAddSplitDatabase);
    //            InternalDatabase.hd.itens.Add(itemToAddFullDatabase);
    //            break;
    //        #endregion
    //        #region Memoria
    //        case ConstStrings.Memoria:
    //            itemToAddFullDatabase.Categoria = ConstStrings.Memoria;
    //            InternalDatabase.Instance.splitDatabase[ConstStrings.InventarioSnPro].itens.Add(itemToAddFullDatabase);
    //            itemToAddFullDatabase.Tipo = parameterValues[11].text;
    //            itemToAddSplitDatabase.Tipo = parameterValues[11].text;
    //            if(!int.TryParse(parameterValues[12].text, out itemToAddFullDatabase.CapacidadeEmGB))
    //            {
    //                itemToAddFullDatabase.CapacidadeEmGB = 0;
    //                itemToAddSplitDatabase.CapacidadeEmGB = 0;
    //            }
    //            else
    //            {
    //                itemToAddSplitDatabase.CapacidadeEmGB = itemToAddFullDatabase.CapacidadeEmGB;
    //            }
    //            if(!int.TryParse(parameterValues[13].text, out itemToAddFullDatabase.VelocidadeMHz))
    //            {
    //                itemToAddFullDatabase.VelocidadeMHz = 0;
    //                itemToAddSplitDatabase.VelocidadeMHz = 0;
    //            }
    //            else
    //            {
    //                itemToAddSplitDatabase.VelocidadeMHz = itemToAddFullDatabase.VelocidadeMHz;
    //            }
    //                      itemToAddFullDatabase.LowVoltage = parameterValues[14].text;
    //            itemToAddSplitDatabase.LowVoltage = parameterValues[14].text;
    //            itemToAddFullDatabase.Rank = parameterValues[15].text;
    //            itemToAddSplitDatabase.Rank = parameterValues[15].text;
    //            itemToAddFullDatabase.DIMM = parameterValues[16].text;
    //            itemToAddSplitDatabase.DIMM = parameterValues[16].text;
    //            if(!int.TryParse(parameterValues[17].text, out itemToAddFullDatabase.TaxaDeTransmissao))
    //            {
    //                itemToAddFullDatabase.TaxaDeTransmissao = 0;
    //                itemToAddSplitDatabase.TaxaDeTransmissao = 0;
    //            }
    //            else
    //            {
    //                itemToAddSplitDatabase.TaxaDeTransmissao = itemToAddFullDatabase.TaxaDeTransmissao;
    //            }               
    //            itemToAddFullDatabase.Simbolo = parameterValues[18].text;
    //            itemToAddSplitDatabase.Simbolo = parameterValues[18].text;

    //            InternalDatabase.Instance.splitDatabase[ConstStrings.Memoria].itens.Add(itemToAddSplitDatabase);
    //            InternalDatabase.memoria.itens.Add(itemToAddFullDatabase);
    //            break;
    //        #endregion
    //        #region Placa de rede
    //        case ConstStrings.PlacaDeRede:
    //            itemToAddFullDatabase.Categoria = ConstStrings.PlacaDeRede;
    //            InternalDatabase.Instance.splitDatabase[ConstStrings.InventarioSnPro].itens.Add(itemToAddFullDatabase);
    //            itemToAddFullDatabase.Interface = parameterValues[11].text;
    //            if(!int.TryParse(parameterValues[12].text, out itemToAddFullDatabase.QuantidadeDePortas))
    //            {
    //                itemToAddFullDatabase.QuantidadeDePortas = 0;
    //                itemToAddSplitDatabase.QuantidadeDePortas = 0;
    //            }
    //            else
    //            {
    //                itemToAddSplitDatabase.QuantidadeDePortas = itemToAddFullDatabase.QuantidadeDePortas;
    //            }
    //            itemToAddFullDatabase.QuaisConexoes = parameterValues[13].text;
    //            itemToAddFullDatabase.SuportaFibraOptica = parameterValues[14].text;
    //            itemToAddFullDatabase.Desempenho = parameterValues[15].text;
    //            itemToAddSplitDatabase.Interface = parameterValues[11].text;
    //                            itemToAddSplitDatabase.QuaisConexoes = parameterValues[13].text;
    //            itemToAddSplitDatabase.SuportaFibraOptica = parameterValues[14].text;
    //            itemToAddSplitDatabase.Desempenho = parameterValues[15].text;

    //            InternalDatabase.Instance.splitDatabase[ConstStrings.PlacaDeRede].itens.Add(itemToAddSplitDatabase);
    //            InternalDatabase.placaDeRede.itens.Add(itemToAddFullDatabase);
    //            break;
    //        #endregion
    //        #region iDrac
    //        case ConstStrings.Idrac:
    //            itemToAddFullDatabase.Categoria = ConstStrings.Idrac;
    //            InternalDatabase.Instance.splitDatabase[ConstStrings.InventarioSnPro].itens.Add(itemToAddFullDatabase);
    //            itemToAddFullDatabase.QuaisConexoes = parameterValues[11].text;
    //            if(!float.TryParse(parameterValues[12].text, out itemToAddFullDatabase.VelocidadeGBs))
    //            {
    //                itemToAddFullDatabase.VelocidadeGBs = 0;
    //                itemToAddSplitDatabase.VelocidadeGBs = 0;
    //            }
    //            else
    //            {
    //                itemToAddSplitDatabase.VelocidadeGBs = itemToAddFullDatabase.VelocidadeGBs;
    //            }
    //            itemToAddFullDatabase.EntradaSD = parameterValues[13].text;
    //            itemToAddFullDatabase.ServidoresSuportados = parameterValues[14].text;
    //            itemToAddSplitDatabase.QuaisConexoes = parameterValues[11].text;
    //            itemToAddSplitDatabase.EntradaSD = parameterValues[13].text;
    //            itemToAddSplitDatabase.ServidoresSuportados = parameterValues[14].text;

    //            InternalDatabase.Instance.splitDatabase[ConstStrings.Idrac].itens.Add(itemToAddSplitDatabase);
    //            InternalDatabase.idrac.itens.Add(itemToAddFullDatabase);
    //            break;
    //        #endregion
    //        #region Placa controladora
    //        case ConstStrings.PlacaControladora:
    //            itemToAddFullDatabase.Categoria = ConstStrings.PlacaControladora;
    //            InternalDatabase.Instance.splitDatabase[ConstStrings.InventarioSnPro].itens.Add(itemToAddFullDatabase);
    //            itemToAddFullDatabase.QuaisConexoes = parameterValues[11].text;
    //            if(!int.TryParse(parameterValues[12].text, out itemToAddFullDatabase.QuantidadeDePortas))
    //            {
    //                itemToAddFullDatabase.QuantidadeDePortas = 0;
    //                itemToAddSplitDatabase.QuantidadeDePortas = 0;
    //            }
    //            else
    //            {
    //                itemToAddSplitDatabase.QuantidadeDePortas = itemToAddFullDatabase.QuantidadeDePortas;
    //            }
    //            itemToAddFullDatabase.TipoDeRAID = parameterValues[13].text;
    //            itemToAddFullDatabase.TipoDeHD = parameterValues[14].text;
    //            itemToAddFullDatabase.CapacidadeMaxHD = parameterValues[15].text;
    //            if(!int.TryParse(parameterValues[16].text, out itemToAddFullDatabase.AteQuantosHDs))
    //            {
    //                itemToAddFullDatabase.AteQuantosHDs = 0;
    //                itemToAddSplitDatabase.AteQuantosHDs = 0;
    //            }
    //            else
    //            {
    //                itemToAddSplitDatabase.AteQuantosHDs = itemToAddFullDatabase.AteQuantosHDs;
    //            }
    //            itemToAddFullDatabase.BateriaInclusa = parameterValues[17].text;
    //            itemToAddFullDatabase.Barramento = parameterValues[18].text;
    //            itemToAddSplitDatabase.QuaisConexoes = parameterValues[11].text;
    //            itemToAddSplitDatabase.TipoDeRAID = parameterValues[13].text;
    //            itemToAddSplitDatabase.TipoDeHD = parameterValues[14].text;
    //            itemToAddSplitDatabase.CapacidadeMaxHD = parameterValues[15].text;
    //            itemToAddSplitDatabase.BateriaInclusa = parameterValues[17].text;
    //            itemToAddSplitDatabase.Barramento = parameterValues[18].text;

    //            InternalDatabase.Instance.splitDatabase[ConstStrings.PlacaControladora].itens.Add(itemToAddSplitDatabase);
    //            InternalDatabase.placaControladora.itens.Add(itemToAddFullDatabase);
    //            break;
    //        #endregion
    //        #region Processador
    //        case ConstStrings.Processador:
    //            itemToAddFullDatabase.Categoria = ConstStrings.Processador;
    //            InternalDatabase.Instance.splitDatabase[ConstStrings.InventarioSnPro].itens.Add(itemToAddFullDatabase);
    //            itemToAddFullDatabase.Soquete = parameterValues[11].text;
    //            if(!int.TryParse(parameterValues[12].text, out itemToAddFullDatabase.NucleosFisicos))
    //            {
    //                itemToAddFullDatabase.NucleosFisicos
    //            }
    //            itemToAddFullDatabase.NucleosLogicos = int.TryParse(parameterValues[13].text);
    //            itemToAddFullDatabase.AceitaVirtualizacao = parameterValues[14].text;
    //            itemToAddFullDatabase.TurboBoost = parameterValues[15].text;
    //            itemToAddFullDatabase.HyperThreading = parameterValues[16].text;
    //            itemToAddSplitDatabase.Soquete = parameterValues[11].text;
    //            itemToAddSplitDatabase.NucleosFisicos = int.TryParse(parameterValues[12].text);
    //            itemToAddSplitDatabase.NucleosLogicos = int.TryParse(parameterValues[13].text);
    //            itemToAddSplitDatabase.AceitaVirtualizacao = parameterValues[14].text;
    //            itemToAddSplitDatabase.TurboBoost = parameterValues[15].text;
    //            itemToAddSplitDatabase.HyperThreading = parameterValues[16].text;

    //            InternalDatabase.Instance.splitDatabase[ConstStrings.Processador].itens.Add(itemToAddSplitDatabase);
    //            InternalDatabase.processador.itens.Add(itemToAddFullDatabase);
    //            break;
    //        #endregion
    //        #region Desktop
    //        case ConstStrings.Desktop:
    //            itemToAddFullDatabase.Categoria = ConstStrings.Desktop;
    //            InternalDatabase.Instance.splitDatabase[ConstStrings.InventarioSnPro].itens.Add(itemToAddFullDatabase);
    //            itemToAddFullDatabase.ModeloPlacaMae = parameterValues[11].text;
    //            itemToAddFullDatabase.Fonte = parameterValues[12].text;
    //            itemToAddFullDatabase.Memoria = parameterValues[13].text;
    //            itemToAddFullDatabase.HD = parameterValues[14].text;
    //            itemToAddFullDatabase.PlacaDeVideo = parameterValues[15].text;
    //            itemToAddFullDatabase.PlacaDeRede = parameterValues[16].text;
    //            itemToAddFullDatabase.LeitorDeDVD = parameterValues[17].text;
    //            itemToAddFullDatabase.Processador = parameterValues[18].text;
    //            itemToAddSplitDatabase.Patrimonio = int.TryParse(parameterValues[2].text);
    //            itemToAddSplitDatabase.ModeloPlacaMae = parameterValues[11].text;
    //            itemToAddSplitDatabase.Fonte = parameterValues[12].text;
    //            itemToAddSplitDatabase.Memoria = parameterValues[13].text;
    //            itemToAddSplitDatabase.HD = parameterValues[14].text;
    //            itemToAddSplitDatabase.PlacaDeVideo = parameterValues[15].text;
    //            itemToAddSplitDatabase.PlacaDeRede = parameterValues[16].text;
    //            itemToAddSplitDatabase.LeitorDeDVD = parameterValues[17].text;
    //            itemToAddSplitDatabase.Processador = parameterValues[18].text;

    //            InternalDatabase.Instance.splitDatabase[ConstStrings.Desktop].itens.Add(itemToAddSplitDatabase);
    //            InternalDatabase.desktop.itens.Add(itemToAddFullDatabase);
    //            break;
    //        #endregion
    //        #region Fonte
    //        case ConstStrings.Fonte:
    //            itemToAddFullDatabase.Categoria = ConstStrings.Fonte;
    //            InternalDatabase.Instance.splitDatabase[ConstStrings.InventarioSnPro].itens.Add(itemToAddFullDatabase);
    //            itemToAddFullDatabase.Watts = int.TryParse(parameterValues[11].text);
    //            itemToAddFullDatabase.OndeFunciona = parameterValues[12].text;
    //            itemToAddFullDatabase.Conectores = parameterValues[13].text;
    //            itemToAddSplitDatabase.Watts = int.TryParse(parameterValues[11].text);
    //            itemToAddSplitDatabase.OndeFunciona = parameterValues[12].text;
    //            itemToAddSplitDatabase.Conectores = parameterValues[13].text;

    //            InternalDatabase.Instance.splitDatabase[ConstStrings.Fonte].itens.Add(itemToAddSplitDatabase);
    //            InternalDatabase.fonte.itens.Add(itemToAddFullDatabase);
    //            break;
    //        #endregion
    //        #region Switch
    //        case ConstStrings.Switch:
    //            itemToAddFullDatabase.Categoria = ConstStrings.Switch;
    //            InternalDatabase.Instance.splitDatabase[ConstStrings.InventarioSnPro].itens.Add(itemToAddFullDatabase);
    //            itemToAddFullDatabase.QuantidadeDePortas = int.TryParse(parameterValues[11].text);
    //            itemToAddFullDatabase.Desempenho = parameterValues[12].text;
    //            itemToAddSplitDatabase.QuantidadeDePortas = int.TryParse(parameterValues[11].text);
    //            itemToAddSplitDatabase.Desempenho = parameterValues[12].text;

    //            InternalDatabase.Instance.splitDatabase[ConstStrings.Switch].itens.Add(itemToAddSplitDatabase);
    //            InternalDatabase.Switch.itens.Add(itemToAddFullDatabase);
    //            break;
    //        #endregion
    //        #region Roteador
    //        case ConstStrings.Roteador:
    //            itemToAddFullDatabase.Categoria = ConstStrings.Roteador;
    //            InternalDatabase.Instance.splitDatabase[ConstStrings.InventarioSnPro].itens.Add(itemToAddFullDatabase);
    //            itemToAddFullDatabase.Wireless = parameterValues[11].text;
    //            itemToAddFullDatabase.QuantidadeDePortas = int.TryParse(parameterValues[12].text);
    //            itemToAddFullDatabase.BandaMaxima = int.TryParse(parameterValues[13].text);
    //            itemToAddFullDatabase.VoltagemDeSaida = float.TryParse(parameterValues[14].text);
    //            itemToAddSplitDatabase.Wireless = parameterValues[11].text;
    //            itemToAddSplitDatabase.QuantidadeDePortas = int.TryParse(parameterValues[12].text);
    //            itemToAddSplitDatabase.BandaMaxima = int.TryParse(parameterValues[13].text);
    //            itemToAddSplitDatabase.VoltagemDeSaida = int.TryParse(parameterValues[14].text);

    //            InternalDatabase.Instance.splitDatabase[ConstStrings.Roteador].itens.Add(itemToAddSplitDatabase);
    //            InternalDatabase.roteador.itens.Add(itemToAddFullDatabase);
    //            break;
    //        #endregion
    //        #region Carregador
    //        case ConstStrings.Carregador:
    //            itemToAddFullDatabase.Categoria = ConstStrings.Carregador;
    //            InternalDatabase.Instance.splitDatabase[ConstStrings.InventarioSnPro].itens.Add(itemToAddFullDatabase);
    //            itemToAddFullDatabase.OndeFunciona = parameterValues[11].text;
    //            itemToAddFullDatabase.VoltagemDeSaida = float.TryParse(parameterValues[12].text);
    //            itemToAddFullDatabase.AmperagemDeSaida = float.TryParse(parameterValues[13].text);
    //            itemToAddSplitDatabase.OndeFunciona = parameterValues[11].text;
    //            itemToAddSplitDatabase.VoltagemDeSaida = float.TryParse(parameterValues[12].text);
    //            itemToAddSplitDatabase.AmperagemDeSaida = float.TryParse(parameterValues[13].text);

    //            InternalDatabase.Instance.splitDatabase[ConstStrings.Carregador].itens.Add(itemToAddSplitDatabase);
    //            InternalDatabase.carregador.itens.Add(itemToAddFullDatabase);
    //            break;
    //        #endregion
    //        #region Adaptador AC
    //        case ConstStrings.AdaptadorAC:
    //            itemToAddFullDatabase.Categoria = ConstStrings.AdaptadorAC;
    //            InternalDatabase.Instance.splitDatabase[ConstStrings.InventarioSnPro].itens.Add(itemToAddFullDatabase);
    //            itemToAddFullDatabase.OndeFunciona = parameterValues[11].text;
    //            itemToAddFullDatabase.VoltagemDeSaida = float.TryParse(parameterValues[12].text);
    //            itemToAddFullDatabase.AmperagemDeSaida = float.TryParse(parameterValues[13].text);
    //            itemToAddSplitDatabase.OndeFunciona = parameterValues[11].text;
    //            itemToAddSplitDatabase.VoltagemDeSaida = float.TryParse(parameterValues[12].text);
    //            itemToAddSplitDatabase.AmperagemDeSaida = float.TryParse(parameterValues[13].text);

    //            InternalDatabase.Instance.splitDatabase[ConstStrings.AdaptadorAC].itens.Add(itemToAddSplitDatabase);
    //            InternalDatabase.adaptadorAC.itens.Add(itemToAddFullDatabase);
    //            break;
    //        #endregion
    //        #region Storage NAS
    //        case ConstStrings.StorageNAS:
    //            itemToAddFullDatabase.Categoria = ConstStrings.StorageNAS;
    //            InternalDatabase.Instance.splitDatabase[ConstStrings.InventarioSnPro].itens.Add(itemToAddFullDatabase);
    //            itemToAddFullDatabase.Tamanho = float.TryParse(parameterValues[11].text);
    //            itemToAddFullDatabase.TipoDeRAID = parameterValues[12].text;
    //            itemToAddFullDatabase.TipoDeHD = parameterValues[13].text;
    //            itemToAddFullDatabase.CapacidadeMaxHD = parameterValues[14].text;
    //            itemToAddFullDatabase.AteQuantosHDs = int.TryParse(parameterValues[15].text);
    //            itemToAddSplitDatabase.Tamanho = float.TryParse(parameterValues[11].text);
    //            itemToAddSplitDatabase.TipoDeRAID = parameterValues[12].text;
    //            itemToAddSplitDatabase.TipoDeHD = parameterValues[13].text;
    //            itemToAddSplitDatabase.CapacidadeMaxHD = parameterValues[14].text;
    //            itemToAddSplitDatabase.AteQuantosHDs = int.TryParse(parameterValues[15].text);

    //            InternalDatabase.Instance.splitDatabase[ConstStrings.StorageNAS].itens.Add(itemToAddSplitDatabase);
    //            InternalDatabase.storageNAS.itens.Add(itemToAddFullDatabase);
    //            break;
    //        #endregion
    //        #region GBIC
    //        case ConstStrings.Gbic:
    //            itemToAddFullDatabase.Categoria = ConstStrings.Gbic;
    //            InternalDatabase.Instance.splitDatabase[ConstStrings.InventarioSnPro].itens.Add(itemToAddFullDatabase);
    //            itemToAddFullDatabase.Desempenho = parameterValues[11].text;
    //            itemToAddSplitDatabase.Desempenho = parameterValues[11].text;

    //            InternalDatabase.Instance.splitDatabase[ConstStrings.Gbic].itens.Add(itemToAddSplitDatabase);
    //            InternalDatabase.gbic.itens.Add(itemToAddFullDatabase);
    //            break;
    //        #endregion
    //        #region Placa de Video
    //        case ConstStrings.PlacaDeVideo:
    //            itemToAddFullDatabase.Categoria = ConstStrings.PlacaDeVideo;
    //            InternalDatabase.Instance.splitDatabase[ConstStrings.InventarioSnPro].itens.Add(itemToAddFullDatabase);
    //            itemToAddFullDatabase.QuantidadeDePortas = int.TryParse(parameterValues[11].text);
    //            itemToAddFullDatabase.QuaisConexoes = parameterValues[12].text;
    //            itemToAddSplitDatabase.QuantidadeDePortas = int.TryParse(parameterValues[11].text);
    //            itemToAddSplitDatabase.QuaisConexoes = parameterValues[12].text;

    //            InternalDatabase.Instance.splitDatabase[ConstStrings.PlacaDeVideo].itens.Add(itemToAddSplitDatabase);
    //            InternalDatabase.placaDeVideo.itens.Add(itemToAddFullDatabase);
    //            break;
    //        #endregion
    //        #region Placa de som
    //        case ConstStrings.PlacaDeSom:
    //            itemToAddFullDatabase.Categoria = ConstStrings.PlacaDeSom;
    //            InternalDatabase.Instance.splitDatabase[ConstStrings.InventarioSnPro].itens.Add(itemToAddFullDatabase);
    //            itemToAddFullDatabase.QuantosCanais = int.TryParse(parameterValues[11].text);
    //            itemToAddSplitDatabase.QuantosCanais = int.TryParse(parameterValues[11].text);

    //            InternalDatabase.Instance.splitDatabase[ConstStrings.PlacaDeSom].itens.Add(itemToAddSplitDatabase);
    //            InternalDatabase.placaDeSom.itens.Add(itemToAddFullDatabase);
    //            break;
    //        #endregion
    //        #region Placa de captura de video
    //        case ConstStrings.PlacaDeCapturaDeVideo:
    //            itemToAddFullDatabase.Categoria = ConstStrings.PlacaDeCapturaDeVideo;
    //            InternalDatabase.Instance.splitDatabase[ConstStrings.InventarioSnPro].itens.Add(itemToAddFullDatabase);
    //            itemToAddFullDatabase.QuantidadeDePortas = int.TryParse(parameterValues[11].text);
    //            itemToAddSplitDatabase.QuantidadeDePortas = int.TryParse(parameterValues[11].text);

    //            InternalDatabase.Instance.splitDatabase[ConstStrings.PlacaDeCapturaDeVideo].itens.Add(itemToAddSplitDatabase);
    //            InternalDatabase.placaDeCapturaDeVideo.itens.Add(itemToAddFullDatabase);
    //            break;
    //        #endregion
    //        #region Servidor
    //        case ConstStrings.Servidor:
    //            itemToAddFullDatabase.Categoria = ConstStrings.Servidor;
    //            InternalDatabase.Instance.splitDatabase[ConstStrings.InventarioSnPro].itens.Add(itemToAddFullDatabase);
    //            itemToAddFullDatabase.ModeloPlacaMae = parameterValues[11].text;
    //            itemToAddFullDatabase.Fonte = parameterValues[12].text;
    //            itemToAddFullDatabase.Memoria = parameterValues[13].text;
    //            itemToAddFullDatabase.HD = parameterValues[14].text;
    //            itemToAddFullDatabase.PlacaDeVideo = parameterValues[15].text;
    //            itemToAddFullDatabase.PlacaDeRede = parameterValues[16].text;
    //            itemToAddFullDatabase.Processador = parameterValues[17].text;
    //            itemToAddFullDatabase.MemoriasSuportadas = parameterValues[18].text;
    //            itemToAddFullDatabase.QuantasMemorias = int.TryParse(parameterValues[19].text);
    //            itemToAddFullDatabase.OrdemDasMemorias = parameterValues[20].text;
    //            itemToAddFullDatabase.CapacidadeRAMTotal = int.TryParse(parameterValues[21].text);
    //            itemToAddFullDatabase.Soquete = parameterValues[22].text;
    //            itemToAddFullDatabase.PlacaControladora = parameterValues[23].text;
    //            itemToAddFullDatabase.AteQuantosHDs = int.TryParse(parameterValues[24].text);
    //            itemToAddFullDatabase.TipoDeHD = parameterValues[25].text;
    //            itemToAddFullDatabase.TipoDeRAID = parameterValues[26].text;
    //            itemToAddSplitDatabase.Patrimonio = int.TryParse(parameterValues[2].text);
    //            itemToAddSplitDatabase.Modelo = parameterValues[7].text;
    //            itemToAddSplitDatabase.Fabricante = parameterValues[6].text;
    //            itemToAddSplitDatabase.ModeloPlacaMae = parameterValues[11].text;
    //            itemToAddSplitDatabase.Fonte = parameterValues[12].text;
    //            itemToAddSplitDatabase.Memoria = parameterValues[13].text;
    //            itemToAddSplitDatabase.HD = parameterValues[14].text;
    //            itemToAddSplitDatabase.PlacaDeVideo = parameterValues[15].text;
    //            itemToAddSplitDatabase.PlacaDeRede = parameterValues[16].text;
    //            itemToAddSplitDatabase.Processador = parameterValues[17].text;
    //            itemToAddSplitDatabase.MemoriasSuportadas = parameterValues[18].text;
    //            itemToAddSplitDatabase.QuantasMemorias = int.TryParse(parameterValues[19].text);
    //            itemToAddSplitDatabase.OrdemDasMemorias = parameterValues[20].text;
    //            itemToAddSplitDatabase.CapacidadeRAMTotal = int.TryParse(parameterValues[21].text);
    //            itemToAddSplitDatabase.Soquete = parameterValues[22].text;
    //            itemToAddSplitDatabase.PlacaControladora = parameterValues[23].text;
    //            itemToAddSplitDatabase.AteQuantosHDs = int.TryParse(parameterValues[24].text);
    //            itemToAddSplitDatabase.TipoDeHD = parameterValues[25].text;
    //            itemToAddSplitDatabase.TipoDeRAID = parameterValues[26].text;

    //            InternalDatabase.Instance.splitDatabase[ConstStrings.Servidor].itens.Add(itemToAddSplitDatabase);
    //            InternalDatabase.servidor.itens.Add(itemToAddFullDatabase);
    //            break;
    //        #endregion
    //        #region Notebook
    //        case ConstStrings.Notebook:
    //            itemToAddFullDatabase.Categoria = ConstStrings.Notebook;
    //            InternalDatabase.Instance.splitDatabase[ConstStrings.InventarioSnPro].itens.Add(itemToAddFullDatabase);
    //            itemToAddFullDatabase.HD = parameterValues[11].text;
    //            itemToAddFullDatabase.Memoria = parameterValues[12].text;
    //            itemToAddFullDatabase.EntradaRJ45 = parameterValues[13].text;
    //            itemToAddFullDatabase.BateriaInclusa = parameterValues[14].text;
    //            itemToAddFullDatabase.AdaptadorAC = parameterValues[15].text;
    //            itemToAddFullDatabase.Windows = parameterValues[16].text;

    //            itemToAddSplitDatabase.Patrimonio = int.TryParse(parameterValues[2].text);
    //            itemToAddSplitDatabase.Modelo = parameterValues[7].text;
    //            itemToAddSplitDatabase.Fabricante = parameterValues[6].text;
    //            itemToAddSplitDatabase.HD = parameterValues[11].text;
    //            itemToAddSplitDatabase.Memoria = parameterValues[12].text;
    //            itemToAddSplitDatabase.EntradaRJ45 = parameterValues[13].text;
    //            itemToAddSplitDatabase.BateriaInclusa = parameterValues[14].text;
    //            itemToAddSplitDatabase.AdaptadorAC = parameterValues[15].text;
    //            itemToAddSplitDatabase.Windows = parameterValues[16].text;

    //            InternalDatabase.Instance.splitDatabase[ConstStrings.Notebook].itens.Add(itemToAddSplitDatabase);
    //            InternalDatabase.notebook.itens.Add(itemToAddFullDatabase);
    //            break;
    //        #endregion
    //        #region Monitor
    //        case ConstStrings.Monitor:
    //            itemToAddFullDatabase.Categoria = ConstStrings.Monitor;
    //            InternalDatabase.Instance.splitDatabase[ConstStrings.InventarioSnPro].itens.Add(itemToAddFullDatabase);
    //            itemToAddFullDatabase.Polegadas = float.TryParse(parameterValues[11].text);
    //            itemToAddSplitDatabase.Polegadas = float.TryParse(parameterValues[11].text);
    //            itemToAddFullDatabase.QuaisConexoes = parameterValues[12].text;
    //            itemToAddSplitDatabase.QuaisConexoes = parameterValues[12].text;

    //            InternalDatabase.Instance.splitDatabase[ConstStrings.Monitor].itens.Add(itemToAddSplitDatabase);
    //            InternalDatabase.monitor.itens.Add(itemToAddFullDatabase);
    //            break;
    //        #endregion
    //        #region Outros
    //        default:
    //            itemToAddFullDatabase.Categoria = parameterValues[5].text;
    //            itemToAddSplitDatabase.Categoria = parameterValues[5].text;
    //            InternalDatabase.Instance.splitDatabase[ConstStrings.Outros].itens.Add(itemToAddSplitDatabase);
    //            InternalDatabase.outros.itens.Add(itemToAddFullDatabase);
    //            break;
    //            #endregion
    //    }
    //    InternalDatabase.Instance.fullDatabase.itens.Add(itemToAddFullDatabase);
    //    // ShowMessage();
    //   // EventHandler.CallDatabaseUpdatedEvent(ConstStrings.DataDatabaseSaveFile);
    //}

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
