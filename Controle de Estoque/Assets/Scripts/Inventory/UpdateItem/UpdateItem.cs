using SimpleJSON;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class UpdateItem : MonoBehaviour
{
    [SerializeField] private TMP_InputField itemToUpdateParameter;
    [SerializeField] private TMP_Dropdown parameterToSearchDP;

    [SerializeField] GameObject[] parameterItems;
    [SerializeField] TMP_InputField[] parameterInputs;
    [SerializeField] TMP_Text[] placeholders;
    [SerializeField] TMP_Text[] parameterNames;

    [SerializeField] private GameObject inputsPanel;
    [SerializeField] private GameObject messagePanel;
    [SerializeField] private TMP_Text messageText;

    [SerializeField] Button resetButton;
    [SerializeField] Button returnButton;

    private bool searchingItem = true;
    private bool inputEnabled = true;

    private List<string> parameters = new List<string>();
    private ItemColumns itemToUpdate;
    private ItemColumns itemToUpdateCategory;
    private int itemToUpdateIndex;
    private int itemToUpdateCategoryIndex;

    private bool updateInventarioSuccess = false;
    private bool updateDetailsSuccess = false;

    void Start()
    {
        ResetInputs();
        itemToUpdate = new ItemColumns();
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
    /// Handles what happens if Enter is pressed
    /// </summary>
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Return) || Input.GetKeyDown(KeyCode.KeypadEnter))
        {
            if (inputEnabled)
            {
                inputEnabled = false;
                if (searchingItem)
                {
                    StartCoroutine(CheckIfItemExists());
                }
                else
                {
                    StartCoroutine(UpdateDatabaseRoutine());
                }
            }
        }
    }

    private void SetInputEnabled(bool inputEnabled)
    {
        for (int i = 0; i < parameterInputs.Length; i++)
        {
            if (parameterInputs[i].gameObject.activeInHierarchy)
            {
                parameterInputs[i].interactable = inputEnabled;
            }
        }
        parameterToSearchDP.interactable = inputEnabled;
        resetButton.interactable = inputEnabled;
        returnButton.interactable = inputEnabled;
        itemToUpdateParameter.interactable = inputEnabled;
        this.inputEnabled = inputEnabled;
    }

    /// <summary>
    /// Check if the item that is going to be updated exists on the fullDatabase
    /// </summary>
    private IEnumerator CheckIfItemExists()
    {
        WWWForm itemForm = new WWWForm();
        UnityWebRequest createItemUpdatePostRequest = new UnityWebRequest();
        if (parameterToSearchDP.value == 0)
        {
            itemForm = CreateForm.GetConsultPatrimonioForm(ConstStrings.ConsultKey, itemToUpdateParameter.text);
            createItemUpdatePostRequest = HelperMethods.GetPostRequest(itemForm, "getitempatrimoniotoupdate.php", 4);
        }
        if (parameterToSearchDP.value == 1)
        {
            itemForm = CreateForm.GetConsultSerialForm(ConstStrings.ConsultKey, itemToUpdateParameter.text);
            createItemUpdatePostRequest = HelperMethods.GetPostRequest(itemForm, "getitemserialtoupdate.php", 4);
        }

        MouseManager.Instance.SetWaitingCursor(this.gameObject);
        EventHandler.CallEnableInput(false);
        yield return createItemUpdatePostRequest.SendWebRequest();

        if (createItemUpdatePostRequest.result == UnityWebRequest.Result.ConnectionError)
        {
            Debug.LogWarning("CheckIfItemExists: conectionerror");
            EventHandler.CallOpenMessageEvent("Server error: 1");
        }
        else if (createItemUpdatePostRequest.result == UnityWebRequest.Result.DataProcessingError)
        {
            Debug.LogWarning("CheckIfItemExists: data processing error");
            EventHandler.CallOpenMessageEvent("Server error: 2");
        }
        else if (createItemUpdatePostRequest.result == UnityWebRequest.Result.ProtocolError)
        {
            Debug.LogWarning("CheckIfItemExists: protocol error");
            EventHandler.CallOpenMessageEvent("Server error: 3");
        }

        if (createItemUpdatePostRequest.error == null)
        {
            string response = createItemUpdatePostRequest.downloadHandler.text;
            if (response == "Database connection error")
            {
                Debug.LogWarning("CheckIfItemExists: conectionerror error");
                EventHandler.CallOpenMessageEvent("Server error: 4");
            }
            else if (response == "Wrong appkey")
            {
                Debug.LogWarning("CheckIfItemExists: app key");
                EventHandler.CallOpenMessageEvent("Server error: 8");
            }
            else if (response == "Check query failed")
            {
                Debug.LogWarning("CheckIfItemExists: query error");
                EventHandler.CallOpenMessageEvent("Server error: 7.5");
            }
            else if (response == "Item not found")
            {
                Debug.LogWarning("CheckIfItemExists: Item does not exist");
                EventHandler.CallOpenMessageEvent("Server error: 11");
            }
            else
            {
                JSONNode inventario = JSON.Parse(createItemUpdatePostRequest.downloadHandler.text);
                foreach (JSONNode item in inventario)
                {
                    itemToUpdate.Entrada = item[0];
                    itemToUpdate.Patrimonio = item[1];
                    itemToUpdate.Status = item[2];
                    itemToUpdate.Serial = item[3];
                    itemToUpdate.Categoria = item[4];
                    itemToUpdate.Fabricante = item[5];
                    itemToUpdate.Modelo = item[6];
                    itemToUpdate.Local = item[7];
                    itemToUpdate.Saida = item[8];
                    itemToUpdate.Observacao = item[9];
                    itemToUpdate.Aquisicao = item[10];
                }
                if (inventario == null)
                {
                    Debug.LogWarning("CheckIfItemExists: " + response);
                    EventHandler.CallOpenMessageEvent("Server error: 12");
                }
            }

        }
        else
        {
            Debug.LogWarning(createItemUpdatePostRequest.error);
            EventHandler.CallOpenMessageEvent("Server error: 10");
        }
        createItemUpdatePostRequest.Dispose();
        MouseManager.Instance.SetDefaultCursor();
        EventHandler.CallEnableInput(true);

        if (itemToUpdate != null)
        {
            searchingItem = false;
            inputsPanel.SetActive(true);
            ShowUpdateItem();
        }
        else
        {
            searchingItem = true;
            //ShowMessage();
        }
    }
    
    private IEnumerator UpdateDatabaseRoutine()
    {
        #region Update inventario
        parameters.Clear();
        parameters.Add(itemToUpdate.Aquisicao);
        parameters.Add(itemToUpdate.Entrada);
        parameters.Add(parameterInputs[1].text);
        parameters.Add(parameterInputs[2].text);
        parameters.Add(parameterInputs[3].text);
        parameters.Add(parameterInputs[4].text);
        parameters.Add(parameterInputs[5].text);
        parameters.Add(parameterInputs[6].text);
        parameters.Add(parameterInputs[7].text);
        parameters.Add(parameterInputs[8].text);
        parameters.Add(parameterInputs[9].text);

        yield return HelperMethods.AddUpdateItem(HelperMethods.GetCategoryInt(itemToUpdate.Categoria), 4, parameters, true);
        if (HelperMethods.GetAddUpdateResponse())
        {
            updateInventarioSuccess = true;
          }
        else
        {
            updateInventarioSuccess = false;
          }

        #endregion
        #region Update the details tables
        switch (itemToUpdate.Categoria)
        {
            #region HD
            case ConstStrings.HD:
                parameters.Clear();
                parameters.Add(parameterInputs[6].text);
                parameters.Add(parameterInputs[5].text);
                parameters.Add(parameterInputs[9].text);
                parameters.Add(parameterInputs[10].text);
                parameters.Add(parameterInputs[11].text);
                parameters.Add(parameterInputs[12].text);
                parameters.Add(parameterInputs[13].text);
                parameters.Add(parameterInputs[14].text);
                parameters.Add(parameterInputs[15].text);                           
                break;
            #endregion
            #region Memória
            case ConstStrings.Memoria:
                parameters.Clear();
                parameters.Add(parameterInputs[6].text);
                parameters.Add(parameterInputs[5].text);
                parameters.Add(parameterInputs[9].text);
                parameters.Add(parameterInputs[10].text);
                parameters.Add(parameterInputs[11].text);
                parameters.Add(parameterInputs[12].text);
                parameters.Add(parameterInputs[13].text);
                parameters.Add(parameterInputs[14].text);
                parameters.Add(parameterInputs[15].text);
                parameters.Add(parameterInputs[16].text);               
                               break;
            #endregion
            #region Placa de rede
            case ConstStrings.PlacaDeRede:
                parameters.Clear();
                parameters.Add(parameterInputs[6].text);
                parameters.Add(parameterInputs[5].text);
                parameters.Add(parameterInputs[9].text);
                parameters.Add(parameterInputs[10].text);
                parameters.Add(parameterInputs[11].text);
                parameters.Add(parameterInputs[12].text);
                parameters.Add(parameterInputs[13].text);
      break;
            #endregion
            #region iDRAC
            case ConstStrings.Idrac:
                parameters.Clear();
                parameters.Add(parameterInputs[6].text);
                parameters.Add(parameterInputs[5].text);
                parameters.Add(parameterInputs[9].text);
                parameters.Add(parameterInputs[10].text);
                parameters.Add(parameterInputs[11].text);
                parameters.Add(parameterInputs[12].text);
           break;
            #endregion
            #region Placa controladora
            case ConstStrings.PlacaControladora:
                parameters.Clear();
                parameters.Add(parameterInputs[6].text);
                parameters.Add(parameterInputs[9].text);
                parameters.Add(parameterInputs[10].text);
                parameters.Add(parameterInputs[11].text);
                parameters.Add(parameterInputs[12].text);
                parameters.Add(parameterInputs[13].text);
                parameters.Add(parameterInputs[14].text);
                parameters.Add(parameterInputs[15].text);
                parameters.Add(parameterInputs[16].text);
               break;
            #endregion
            #region Processador
            case ConstStrings.Processador:
                parameters.Clear();
                parameters.Add(parameterInputs[6].text);
                parameters.Add(parameterInputs[9].text);
                parameters.Add(parameterInputs[10].text);
                parameters.Add(parameterInputs[11].text);
                parameters.Add(parameterInputs[12].text);
                parameters.Add(parameterInputs[13].text);
                parameters.Add(parameterInputs[14].text);
               break;
            #endregion
            #region Desktop
            case ConstStrings.Desktop:
                parameters.Clear();
                parameters.Add(parameterInputs[1].text);
                parameters.Add(parameterInputs[9].text);
                parameters.Add(parameterInputs[10].text);
                parameters.Add(parameterInputs[11].text);
                parameters.Add(parameterInputs[12].text);
                parameters.Add(parameterInputs[13].text);
                parameters.Add(parameterInputs[14].text);
                parameters.Add(parameterInputs[15].text);
                parameters.Add(parameterInputs[16].text);
               break;
            #endregion
            #region Fonte
            case ConstStrings.Fonte:
                parameters.Clear();
                parameters.Add(parameterInputs[6].text);
                parameters.Add(parameterInputs[9].text);
                parameters.Add(parameterInputs[10].text);
                parameters.Add(parameterInputs[11].text);
                break;
            #endregion
            #region Switch
            case ConstStrings.Switch:
                parameters.Clear();
                parameters.Add(parameterInputs[6].text);
                parameters.Add(parameterInputs[9].text);
                parameters.Add(parameterInputs[10].text);
                             break;
            #endregion
            #region Roteador
            case ConstStrings.Roteador:
                parameters.Clear();
                parameters.Add(parameterInputs[6].text);
                parameters.Add(parameterInputs[9].text);
                parameters.Add(parameterInputs[10].text);
                parameters.Add(parameterInputs[11].text);
                parameters.Add(parameterInputs[12].text);
              break;
            #endregion
            #region Carregador
            case ConstStrings.Carregador:
                parameters.Clear();
                parameters.Add(parameterInputs[6].text);
                parameters.Add(parameterInputs[9].text);
                parameters.Add(parameterInputs[10].text);
                parameters.Add(parameterInputs[11].text);
                break;
            #endregion
            #region Adaptador AC
            case ConstStrings.AdaptadorAC:
                parameters.Clear();
                parameters.Add(parameterInputs[6].text);
                                parameters.Add(parameterInputs[9].text);
                parameters.Add(parameterInputs[10].text);
                parameters.Add(parameterInputs[11].text);
               break;
            #endregion
            #region Storage NAS
            case ConstStrings.StorageNAS:
                parameters.Clear();
                parameters.Add(parameterInputs[6].text);
                                parameters.Add(parameterInputs[9].text);
                parameters.Add(parameterInputs[10].text);
                parameters.Add(parameterInputs[11].text);
                parameters.Add(parameterInputs[12].text);
                parameters.Add(parameterInputs[13].text);
              break;
            #endregion
            #region GBIC
            case ConstStrings.Gbic:
                parameters.Clear();
                parameters.Add(parameterInputs[6].text);
                parameters.Add(parameterInputs[5].text);
                parameters.Add(parameterInputs[9].text);
               break;
            #endregion
            #region Placa de vídeo
            case ConstStrings.PlacaDeVideo:
                parameters.Clear();
                parameters.Add(parameterInputs[6].text);
                              parameters.Add(parameterInputs[9].text);
                parameters.Add(parameterInputs[10].text);
                break;
            #endregion
            #region Placa de som
            case ConstStrings.PlacaDeSom:
                parameters.Clear();
                parameters.Add(parameterInputs[6].text);
                parameters.Add(parameterInputs[9].text);
               break;
            #endregion
            #region Placa de captura de vídeo
            case ConstStrings.PlacaDeCapturaDeVideo:
                parameters.Clear();
                parameters.Add(parameterInputs[6].text);
                               parameters.Add(parameterInputs[9].text);
                break;
            #endregion
            #region Servidor
            case ConstStrings.Servidor:
                parameters.Clear();
                parameters.Add(parameterInputs[6].text);
                parameters.Add(parameterInputs[5].text);
               break;
            #endregion
            #region Notebook
            case ConstStrings.Notebook:
                parameters.Clear();
                parameters.Add(parameterInputs[6].text);
                parameters.Add(parameterInputs[5].text);
                               break;
            #endregion
            #region Monitor
            case ConstStrings.Monitor:
                parameters.Clear();
                parameters.Add(parameterInputs[6].text);
                parameters.Add(parameterInputs[5].text);
                parameters.Add(parameterInputs[9].text);
                parameters.Add(parameterInputs[10].text);
                parameters.Add(parameterInputs[11].text);
              break;
            #endregion
            default:
                break;
        }
        #endregion
        yield return HelperMethods.AddUpdateItem(HelperMethods.GetCategoryInt(itemToUpdate.Categoria), 4, parameters, false);
        if (HelperMethods.GetAddUpdateResponse())
        {
            updateInventarioSuccess = true;
        }
        else
        {
            updateInventarioSuccess = false;
        }
        if(updateInventarioSuccess)
        {
            UpdateFullDatabase();
        }
        
        
    }
    /// <summary>
    /// Updates the full database
    /// </summary>
    private void UpdateFullDatabase()
    {
       // itemToUpdate.Entrada = parameterInputs[0].text;
        itemToUpdate.Patrimonio = parameterInputs[1].text;
        itemToUpdate.Status = parameterInputs[2].text;
        itemToUpdate.Serial = parameterInputs[3].text;
        itemToUpdate.Fabricante = parameterInputs[5].text;
        itemToUpdate.Modelo = parameterInputs[6].text;
        itemToUpdate.Local = parameterInputs[7].text;
        itemToUpdate.Saida = parameterInputs[8].text;
        itemToUpdate.Observacao = parameterInputs[9].text;
        switch (itemToUpdate.Categoria)
        {
            case ConstStrings.HD:
                
                itemToUpdate.Interface = parameterInputs[10].text;
                itemToUpdate.Tamanho = parameterInputs[11].text;
                itemToUpdate.FormaDeArmazenamento = parameterInputs[12].text;
                itemToUpdate.CapacidadeEmGB = parameterInputs[13].text;
                itemToUpdate.RPM = parameterInputs[14].text;
                itemToUpdate.VelocidadeDeLeitura = parameterInputs[15].text;
                itemToUpdate.Enterprise = parameterInputs[16].text;
                break;
            case ConstStrings.Memoria:
                itemToUpdate.Tipo = parameterInputs[10].text;
                itemToUpdate.CapacidadeEmGB = parameterInputs[11].text;
                itemToUpdate.VelocidadeMHz = parameterInputs[12].text;
                itemToUpdate.LowVoltage = parameterInputs[13].text;
                itemToUpdate.Rank = parameterInputs[14].text;
                itemToUpdate.DIMM = parameterInputs[15].text;
                itemToUpdate.TaxaDeTransmissao = parameterInputs[16].text;
                itemToUpdate.Simbolo = parameterInputs[17].text;
                break;
            case ConstStrings.PlacaDeRede:
                itemToUpdate.Interface = parameterInputs[10].text;
                itemToUpdate.QuantidadeDePortas = parameterInputs[11].text;
                itemToUpdate.QuaisConexoes = parameterInputs[12].text;
                itemToUpdate.SuportaFibraOptica = parameterInputs[13].text;
                itemToUpdate.Desempenho = parameterInputs[14].text;
                break;
            case ConstStrings.Idrac:
                itemToUpdate.QuaisConexoes = parameterInputs[10].text;
                itemToUpdate.VelocidadeGBs = parameterInputs[11].text;
                itemToUpdate.EntradaSD = parameterInputs[12].text;
                itemToUpdate.ServidoresSuportados = parameterInputs[13].text;
                break;
            case ConstStrings.PlacaControladora:
                itemToUpdate.QuaisConexoes = parameterInputs[10].text;
                itemToUpdate.QuantidadeDePortas = parameterInputs[11].text;
                itemToUpdate.TipoDeRAID = parameterInputs[12].text;
                itemToUpdate.CapacidadeMaxHD = parameterInputs[13].text;
                itemToUpdate.AteQuantosHDs = parameterInputs[14].text;
                itemToUpdate.BateriaInclusa = parameterInputs[15].text;
                itemToUpdate.Barramento = parameterInputs[16].text;
                break;
            case ConstStrings.Processador:
                itemToUpdate.Soquete = parameterInputs[10].text;
                itemToUpdate.NucleosFisicos = parameterInputs[11].text;
                itemToUpdate.NucleosLogicos = parameterInputs[12].text;
                itemToUpdate.AceitaVirtualizacao = parameterInputs[13].text;
                itemToUpdate.TurboBoost = parameterInputs[14].text;
                itemToUpdate.HyperThreading = parameterInputs[15].text;
                break;
            case ConstStrings.Desktop:
                itemToUpdate.ModeloPlacaMae = parameterInputs[10].text;
                itemToUpdate.Fonte = parameterInputs[11].text;
                itemToUpdate.Memoria = parameterInputs[12].text;
                itemToUpdate.HD = parameterInputs[13].text;
                itemToUpdate.PlacaDeVideo = parameterInputs[14].text;
                itemToUpdate.LeitorDeDVD = parameterInputs[15].text;
                break;
            case ConstStrings.Fonte:
                itemToUpdate.Watts = parameterInputs[10].text;
                itemToUpdate.OndeFunciona = parameterInputs[11].text;
                itemToUpdate.Conectores = parameterInputs[12].text;
                break;
            case ConstStrings.Switch:
                itemToUpdate.QuantidadeDePortas = parameterInputs[10].text;
                itemToUpdate.Desempenho = parameterInputs[11].text;
                break;
            case ConstStrings.Roteador:
                itemToUpdate.Wireless = parameterInputs[10].text;
                itemToUpdate.QuantidadeDePortas = parameterInputs[11].text;
                itemToUpdate.BandaMaxima = parameterInputs[12].text;
                break;
            case ConstStrings.Carregador:
                itemToUpdate.OndeFunciona = parameterInputs[10].text;
                itemToUpdate.VoltagemDeSaida = parameterInputs[11].text;
                itemToUpdate.AmperagemDeSaida = parameterInputs[12].text;
                break;
            case ConstStrings.AdaptadorAC:
                itemToUpdate.OndeFunciona = parameterInputs[10].text;
                itemToUpdate.VoltagemDeSaida = parameterInputs[11].text;
                itemToUpdate.AmperagemDeSaida = parameterInputs[12].text;
                break;
            case ConstStrings.StorageNAS:
                itemToUpdate.Tamanho = parameterInputs[10].text;
                itemToUpdate.TipoDeRAID = parameterInputs[11].text;
                itemToUpdate.TipoDeHD = parameterInputs[12].text;
                itemToUpdate.CapacidadeMaxHD = parameterInputs[13].text;
                break;
            case ConstStrings.Gbic:
                itemToUpdate.Desempenho = parameterInputs[10].text;
                break;
            case ConstStrings.PlacaDeVideo:
                itemToUpdate.QuantidadeDePortas = parameterInputs[10].text;
                itemToUpdate.QuaisConexoes = parameterInputs[11].text;
                break;
            case ConstStrings.PlacaDeSom:
                itemToUpdate.QuantosCanais = parameterInputs[10].text;
                break;
            case ConstStrings.PlacaDeCapturaDeVideo:
                itemToUpdate.QuantidadeDePortas = parameterInputs[10].text;
                break;
            case ConstStrings.Servidor:

                break;
            case ConstStrings.Notebook:

                break;
            case ConstStrings.Monitor:
                itemToUpdate.Polegadas = parameterInputs[10].text;
                itemToUpdate.QuaisConexoes = parameterInputs[11].text;
                break;
            default:
                break;
        }
        itemToUpdateIndex = ConsultDatabase.Instance.GetItemIndex();
        InternalDatabase.Instance.fullDatabase.itens[itemToUpdateIndex] = itemToUpdate;
        //EventHandler.CallDatabaseUpdatedEvent(ConstStrings.DataDatabaseSaveFile);
    }

    /// <summary>
    /// Shows the itemToUpdate values on the placeholder of all the inputs
    /// </summary>
    private void ShowUpdateItem()
    {
        ItemColumns tempItem = ConsultDatabase.Instance.ConsultSerial(itemToUpdate.Serial, InternalDatabase.Instance.fullDatabase);
        if (tempItem != null)
        {
            itemToUpdate = tempItem;
        }
        else
        {
            //TODO: update the internal database and try again
        }
        // parameterInputs[0].text = itemToUpdate.Entrada;
        parameterInputs[1].text = itemToUpdate.Patrimonio;
        parameterInputs[2].text = itemToUpdate.Status;
        parameterInputs[3].text = itemToUpdate.Serial;
        parameterInputs[4].text = itemToUpdate.Categoria;
        parameterInputs[5].text = itemToUpdate.Fabricante;
        parameterInputs[6].text = itemToUpdate.Modelo;
        parameterInputs[7].text = itemToUpdate.Local;
        parameterInputs[7].interactable = false;
        parameterInputs[8].text = itemToUpdate.Saida;
        parameterInputs[8].interactable = false;
        parameterInputs[9].text = itemToUpdate.Observacao;

        // parameterNames[0].text = "Entrada no estoque";
        parameterNames[1].text = "Patrimônio";
        parameterNames[2].text = "Status";
        parameterNames[3].text = "Serial";
        parameterNames[4].text = "Categoria";
        parameterNames[5].text = "Fabricante";
        parameterNames[6].text = "Modelo";
        parameterNames[7].text = "Local";
        parameterNames[8].text = "Saída do estoque";
        parameterNames[9].text = "Observação";

        switch (itemToUpdate.Categoria)
        {
            #region HD
            case ConstStrings.HD:
                parameterInputs[10].text = itemToUpdate.Interface;
                parameterInputs[11].text = itemToUpdate.Tamanho;
                parameterInputs[12].text = itemToUpdate.FormaDeArmazenamento;
                parameterInputs[13].text = itemToUpdate.CapacidadeEmGB;
                parameterInputs[14].text = itemToUpdate.RPM;
                parameterInputs[15].text = itemToUpdate.VelocidadeDeLeitura;
                parameterInputs[16].text = itemToUpdate.Enterprise;
                parameterNames[10].text = "Interface";
                parameterNames[11].text = "Tamanho";
                parameterNames[12].text = "Forma de armazenamento";
                parameterNames[13].text = "Capacidade (GB)";
                parameterNames[14].text = "RPM";
                parameterNames[15].text = "Velocidade de Leitura (Gb/s)";
                parameterNames[16].text = "Enterprise";
                parameterNames[17].text = "";
                break;
            #endregion
            #region Memoria
            case ConstStrings.Memoria:
                parameterInputs[10].text = itemToUpdate.Tipo;
                parameterInputs[11].text = itemToUpdate.CapacidadeEmGB;
                parameterInputs[12].text = itemToUpdate.VelocidadeMHz;
                parameterInputs[13].text = itemToUpdate.LowVoltage;
                parameterInputs[14].text = itemToUpdate.Rank;
                parameterInputs[15].text = itemToUpdate.DIMM;
                parameterInputs[16].text = itemToUpdate.TaxaDeTransmissao;
                parameterInputs[17].text = itemToUpdate.Simbolo;
                parameterNames[10].text = "Tipo";
                parameterNames[11].text = "Capacidade (GB)";
                parameterNames[12].text = "Velocidade (MHz)";
                parameterNames[13].text = "É low voltage?";
                parameterNames[14].text = "Rank";
                parameterNames[15].text = "DIMM";
                parameterNames[16].text = "Taxa de transmissão";
                parameterNames[17].text = "Símbolo";
                break;
            #endregion
            #region Placa de rede
            case ConstStrings.PlacaDeRede:
                parameterInputs[10].text = itemToUpdate.Interface;
                parameterInputs[11].text = itemToUpdate.QuantidadeDePortas;
                parameterInputs[12].text = itemToUpdate.QuaisConexoes;
                parameterInputs[13].text = itemToUpdate.SuportaFibraOptica;
                parameterInputs[14].text = itemToUpdate.Desempenho;
                parameterNames[10].text = "Interface";
                parameterNames[11].text = "Quantas portas?";
                parameterNames[12].text = "Quais portas?";
                parameterNames[13].text = "Suporta fibra óptica?";
                parameterNames[14].text = "Desempenho (MB/s)";
                parameterNames[15].text = "";
                parameterNames[16].text = "";
                parameterNames[17].text = "";
                break;
            #endregion
            #region iDrac
            case ConstStrings.Idrac:
                parameterInputs[10].text = itemToUpdate.QuaisConexoes;
                parameterInputs[11].text = itemToUpdate.VelocidadeGBs;
                parameterInputs[12].text = itemToUpdate.EntradaSD;
                parameterInputs[13].text = itemToUpdate.ServidoresSuportados;
                parameterNames[10].text = "Porta";
                parameterNames[11].text = "Velocidade (GB/s)";
                parameterNames[12].text = "Entrada SD";
                parameterNames[13].text = "Servidores suportados";
                parameterNames[14].text = "";
                parameterNames[15].text = "";
                parameterNames[16].text = "";
                parameterNames[17].text = "";
                break;
            #endregion
            #region Placa controladora
            case ConstStrings.PlacaControladora:
                parameterInputs[10].text = itemToUpdate.QuaisConexoes;
                parameterInputs[11].text = itemToUpdate.QuantidadeDePortas;
                parameterInputs[12].text = itemToUpdate.TipoDeRAID;
                parameterInputs[13].text = itemToUpdate.TipoDeHD;
                parameterInputs[14].text = itemToUpdate.CapacidadeMaxHD;
                parameterInputs[15].text = itemToUpdate.AteQuantosHDs;
                parameterInputs[16].text = itemToUpdate.BateriaInclusa;
                parameterInputs[17].text = itemToUpdate.Barramento;
                parameterNames[10].text = "Tipo de conexão";
                parameterNames[11].text = "Quantas portas?";
                parameterNames[12].text = "Tipos de RAID";
                parameterNames[13].text = "Tipo de HD";
                parameterNames[14].text = "Capacidade máx do HD (TB)";
                parameterNames[15].text = "Até quantos HDs";
                parameterNames[16].text = "Bateria inclusa?";
                parameterNames[17].text = "Barramento";
                break;
            #endregion
            #region Processador
            case ConstStrings.Processador:
                parameterInputs[10].text = itemToUpdate.Soquete;
                parameterInputs[11].text = itemToUpdate.NucleosFisicos;
                parameterInputs[12].text = itemToUpdate.NucleosLogicos;
                parameterInputs[13].text = itemToUpdate.AceitaVirtualizacao;
                parameterInputs[14].text = itemToUpdate.TurboBoost;
                parameterInputs[15].text = itemToUpdate.HyperThreading;
                parameterNames[10].text = "Soquete";
                parameterNames[11].text = "Nº núcleos físicos";
                parameterNames[12].text = "Nº núcleos lógicos";
                parameterNames[13].text = "Aceita virtualização?";
                parameterNames[14].text = "Turbo boost?";
                parameterNames[15].text = "Hyper-Threading?";
                parameterNames[16].text = "";
                parameterNames[17].text = "";
                break;
            #endregion
            #region Desktop
            case ConstStrings.Desktop:
                parameterInputs[10].text = itemToUpdate.ModeloPlacaMae;
                parameterInputs[11].text = itemToUpdate.Fonte;
                parameterInputs[12].text = itemToUpdate.Memoria;
                parameterInputs[13].text = itemToUpdate.HD;
                parameterInputs[14].text = itemToUpdate.PlacaDeVideo;
                parameterInputs[15].text = itemToUpdate.PlacaDeRede;
                parameterInputs[16].text = itemToUpdate.LeitorDeDVD;
                parameterInputs[17].text = itemToUpdate.Processador;
                parameterNames[10].text = "Modelo de placa mãe";
                parameterNames[11].text = "Fonte?";
                parameterNames[12].text = "Memória?";
                parameterNames[13].text = "HD?";
                parameterNames[14].text = "Placa de vídeo?";
                parameterNames[15].text = "Placa de rede?";
                parameterNames[16].text = "Leitor de DVD?";
                parameterNames[17].text = "Processador?";
                break;
            #endregion
            #region Fonte
            case ConstStrings.Fonte:
                parameterInputs[10].text = itemToUpdate.Watts;
                parameterInputs[11].text = itemToUpdate.OndeFunciona;
                parameterInputs[12].text = itemToUpdate.Conectores;
                parameterNames[10].text = "Watts de potência";
                parameterNames[11].text = "Onde funciona?";
                parameterNames[12].text = "Conectores";
                parameterNames[13].text = "";
                parameterNames[14].text = "";
                parameterNames[15].text = "";
                parameterNames[16].text = "";
                parameterNames[17].text = "";
                break;
            #endregion
            #region Switch
            case ConstStrings.Switch:
                parameterInputs[10].text = itemToUpdate.QuantidadeDePortas;
                parameterInputs[11].text = itemToUpdate.Desempenho;
                parameterNames[10].text = "Quantas entradas";
                parameterNames[11].text = "Capacidade máx de cada porta (MB/s)";
                parameterNames[12].text = "";
                parameterNames[13].text = "";
                parameterNames[14].text = "";
                parameterNames[15].text = "";
                parameterNames[16].text = "";
                parameterNames[17].text = "";
                break;
            #endregion
            #region Roteador
            case ConstStrings.Roteador:
                parameterInputs[10].text = itemToUpdate.Wireless;
                parameterInputs[11].text = itemToUpdate.QuantidadeDePortas;
                parameterInputs[12].text = itemToUpdate.BandaMaxima;
                parameterInputs[13].text = itemToUpdate.VoltagemDeSaida;
                parameterNames[10].text = "Wireless?";
                parameterNames[11].text = "Quantas entradas?";
                parameterNames[12].text = "Banda máx (MB/s)";
                parameterNames[13].text = "Voltagem";
                parameterNames[14].text = "";
                parameterNames[15].text = "";
                parameterNames[16].text = "";
                parameterNames[17].text = "";
                break;
            #endregion
            #region Carregador
            case ConstStrings.Carregador:
                parameterInputs[10].text = itemToUpdate.OndeFunciona;
                parameterInputs[11].text = itemToUpdate.VoltagemDeSaida;
                parameterInputs[12].text = itemToUpdate.AmperagemDeSaida;
                parameterNames[10].text = "Onde funciona?";
                parameterNames[11].text = "Voltagem de saída";
                parameterNames[12].text = "Amperagem de saída (mA)";
                parameterNames[13].text = "";
                parameterNames[14].text = "";
                parameterNames[15].text = "";
                parameterNames[16].text = "";
                parameterNames[17].text = "";
                break;
            #endregion
            #region Adaptador AC
            case ConstStrings.AdaptadorAC:
                parameterInputs[10].text = itemToUpdate.OndeFunciona;
                parameterInputs[11].text = itemToUpdate.VoltagemDeSaida;
                parameterInputs[12].text = itemToUpdate.AmperagemDeSaida;
                parameterNames[10].text = "Onde funciona?";
                parameterNames[11].text = "Voltagem de saída";
                parameterNames[12].text = "Amperagem de saída (A)";
                parameterNames[13].text = "";
                parameterNames[14].text = "";
                parameterNames[15].text = "";
                parameterNames[16].text = "";
                parameterNames[17].text = "";
                break;
            #endregion
            #region Storage NAS
            case ConstStrings.StorageNAS:
                parameterInputs[10].text = itemToUpdate.Tamanho;
                parameterInputs[11].text = itemToUpdate.TipoDeRAID;
                parameterInputs[12].text = itemToUpdate.TipoDeHD;
                parameterInputs[13].text = itemToUpdate.CapacidadeMaxHD;
                parameterInputs[14].text = itemToUpdate.AteQuantosHDs;
                parameterNames[10].text = "Tamanho dos HDs";
                parameterNames[11].text = "Tipos de RAID";
                parameterNames[12].text = "Tipo de HD";
                parameterNames[13].text = "Capacidade máx do HD";
                parameterNames[14].text = "Até quantos HDs";
                parameterNames[15].text = "";
                parameterNames[16].text = "";
                parameterNames[17].text = "";
                break;
            #endregion
            #region GBIC
            case ConstStrings.Gbic:
                parameterInputs[10].text = itemToUpdate.Desempenho;
                parameterNames[10].text = "Desempenho máx (GB/s)";
                parameterNames[11].text = "";
                parameterNames[12].text = "";
                parameterNames[13].text = "";
                parameterNames[14].text = "";
                parameterNames[15].text = "";
                parameterNames[16].text = "";
                parameterNames[17].text = "";
                break;
            #endregion
            #region Placa de video
            case ConstStrings.PlacaDeVideo:
                parameterInputs[10].text = itemToUpdate.QuantidadeDePortas;
                parameterInputs[11].text = itemToUpdate.QuaisConexoes;
                parameterNames[10].text = "Quantas entradas?";
                parameterNames[11].text = "Quais entradas?";
                parameterNames[12].text = "";
                parameterNames[13].text = "";
                parameterNames[14].text = "";
                parameterNames[15].text = "";
                parameterNames[16].text = "";
                parameterNames[17].text = "";
                break;
            #endregion
            #region Placa de Som
            case ConstStrings.PlacaDeSom:
                parameterInputs[10].text = itemToUpdate.QuantosCanais;
                parameterNames[10].text = "Quantos canais?";
                parameterNames[11].text = "";
                parameterNames[12].text = "";
                parameterNames[13].text = "";
                parameterNames[14].text = "";
                parameterNames[15].text = "";
                parameterNames[16].text = "";
                parameterNames[17].text = "";
                break;
            #endregion
            #region Placa de captura de vídeo
            case ConstStrings.PlacaDeCapturaDeVideo:
                parameterInputs[10].text = itemToUpdate.QuantidadeDePortas;
                parameterNames[10].text = "Quantas entradas?";
                parameterNames[11].text = "";
                parameterNames[12].text = "";
                parameterNames[13].text = "";
                parameterNames[14].text = "";
                parameterNames[15].text = "";
                parameterNames[16].text = "";
                parameterNames[17].text = "";
                break;
            #endregion
            #region Servidor
            case ConstStrings.Servidor:
                parameterNames[10].text = "";
                parameterNames[11].text = "";
                parameterNames[12].text = "";
                parameterNames[13].text = "";
                parameterNames[14].text = "";
                parameterNames[15].text = "";
                parameterNames[16].text = "";
                parameterNames[17].text = "";
                break;
            #endregion
            #region Notebook
            case ConstStrings.Notebook:
                parameterNames[10].text = "";
                parameterNames[11].text = "";
                parameterNames[12].text = "";
                parameterNames[13].text = "";
                parameterNames[14].text = "";
                parameterNames[15].text = "";
                parameterNames[16].text = "";
                parameterNames[17].text = "";
                break;
            #endregion
            #region Monitor
            case ConstStrings.Monitor:
                parameterInputs[10].text = itemToUpdate.Polegadas;
                parameterInputs[11].text = itemToUpdate.QuaisConexoes;
                parameterNames[10].text = "Polegadas";
                parameterNames[11].text = "Tipos de entradas";
                parameterNames[12].text = "";
                parameterNames[13].text = "";
                parameterNames[14].text = "";
                parameterNames[15].text = "";
                parameterNames[16].text = "";
                parameterNames[17].text = "";
                break;
            #endregion

            default:
                parameterNames[10].text = "";
                parameterNames[11].text = "";
                parameterNames[12].text = "";
                parameterNames[13].text = "";
                parameterNames[14].text = "";
                parameterNames[15].text = "";
                parameterNames[16].text = "";
                parameterNames[17].text = "";
                break;
        }

        for (int i = 0; i < parameterItems.Length; i++)
        {
            if (parameterNames[i].text == "")
            {
                parameterItems[i].SetActive(false);
            }
        }
    }

    /// <summary>
    /// Show message if item to update was not found and, if it was found sho message that item was updated
    /// </summary>
    //private void ShowMessage()
    //{
    //    messagePanel.SetActive(true);
    //    if (itemToUpdate == null)
    //    {
    //        messageText.text = "Item não encontrado. Confira se o parâmetro digitado está correto";
    //    }
    //    else
    //    {
    //        messageText.text = "Item atualizado com sucesso";
    //    }
    //    StartCoroutine(CloseShowMessagePanelRoutine());
    //}

    ///// <summary>
    ///// Wait a few seconds to close the MessagePanel
    ///// </summary>
    //private IEnumerator CloseShowMessagePanelRoutine()
    //{
    //    yield return new WaitForSeconds(10f);
    //    CloseMessagePanel();
    //}

    /// <summary>
    /// Resets the texts on all inputs
    /// </summary>
    private void ResetInputs()
    {
        inputEnabled = false;
        inputsPanel.SetActive(true);
        for (int i = 0; i < parameterItems.Length; i++)
        {
            parameterItems[i].SetActive(true);
            parameterInputs[i].text = "";
            parameterNames[i].text = "";
            placeholders[i].text = "Digite o valor";
        }       
        inputsPanel.SetActive(false);
        inputEnabled = true;
    }

    public void MessageClosed()
    {
        ResetInputs();
        searchingItem = true;
        itemToUpdateParameter.text = "";
        MouseManager.Instance.SetDefaultCursor();
    }

    /// <summary>
    /// Returns to InitialScene
    /// </summary>
    public void ReturnToPreviousScreen()
    {
        SceneManager.LoadScene(ConstStrings.SceneInitial);
    }

    public void ResetUpdate()
    {
        ResetInputs();
        searchingItem = true;
    }
}
