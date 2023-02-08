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
            if (HelperMethods.GetCategoryString(categoryDP.value) == ConstStrings.Outros)
            {
                parameters.Add(parameterValues[5].text);
            }
            else
            {
                parameters.Add(HelperMethods.GetCategoryString(categoryDP.value));
            }
            parameters.Add(parameterValues[6].text);
            parameters.Add(parameterValues[7].text);
            parameters.Add(parameterValues[8].text);
            parameters.Add("");
            parameters.Add(parameterValues[10].text);

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

            AddItem();
        }
        if (!addInventario)
        {
            EventHandler.CallOpenMessageEvent("Worked");
            addInventarioSuccess = true;
        }
    }

    /// <summary>
    /// Close the message. It is public to be used on the button too
    /// </summary>
    public void MessageClosed()
    {
        UpdateNames();
        MouseManager.Instance.SetDefaultCursor();
        SetInputEnabled(true);
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
       // EventHandler.CallDatabaseUpdatedEvent(ConstStrings.DataDatabaseSaveFile);
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
