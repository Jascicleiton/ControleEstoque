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

    private bool searchingItem = true;

    private ItemColumns itemToUpdate;
    private ItemColumns itemToUpdateCategory;
    private int itemToUpdateIndex;
    private int itemToUpdateCategoryIndex;

    void Start()
    {
        ResetInputs();
        itemToUpdate = new ItemColumns();
    }

    /// <summary>
    /// Handles what happens if Enter is pressed
    /// </summary>
    private void Update()
    {
        if (searchingItem)
        {
            if (Input.GetKeyDown(KeyCode.Return) || Input.GetKeyDown(KeyCode.KeypadEnter))
            {
               StartCoroutine( CheckIfItemExists());
            }
        }
        else
        {
            if (Input.GetKeyDown(KeyCode.Return) || Input.GetKeyDown(KeyCode.KeypadEnter))
            {
                UpdateFullDatabase();
            }
          }
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
            itemForm = CreateAddItemForm.GetConsultPatrimonioForm(itemToUpdateParameter.text);
            createItemUpdatePostRequest = UnityWebRequest.Post(ConstStrings.PhpUpdateItemsFolder + "getitempatrimoniotoupdate.php", itemForm);
        }
        if (parameterToSearchDP.value == 1)
        {
            itemForm = CreateAddItemForm.GetConsultPatrimonioForm(itemToUpdateParameter.text);
            createItemUpdatePostRequest = UnityWebRequest.Post(ConstStrings.PhpUpdateItemsFolder + "getitemserialtoupdate.php", itemForm);
        }    
         
        yield return createItemUpdatePostRequest.SendWebRequest();

        if (createItemUpdatePostRequest.result == UnityWebRequest.Result.ConnectionError)
        {
            Debug.LogWarning("conectionerror");
        }
        else if (createItemUpdatePostRequest.result == UnityWebRequest.Result.DataProcessingError)
        {
            Debug.LogWarning("data processing error");
        }
        else if (createItemUpdatePostRequest.result == UnityWebRequest.Result.ProtocolError)
        {
            Debug.LogWarning("protocol error");
        }

        if (createItemUpdatePostRequest.error == null)
        {
            string response = createItemUpdatePostRequest.downloadHandler.text;
            if (response == "1" || response == "2")
            {
                Debug.LogWarning("Server error");
            }
            else if (response == "3")
            {
                Debug.LogWarning("Item does not exist");
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
                }
            }

        }
        else
        {
            Debug.LogWarning(createItemUpdatePostRequest.error);
        }
        createItemUpdatePostRequest.Dispose();
        

        if (itemToUpdate != null)
        {
            searchingItem = false;
            inputsPanel.SetActive(true);
            ShowUpdateItem();
        }
        else
        {
            searchingItem = true;
            ShowMessage();
        }
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
        parameterInputs[0].text = itemToUpdate.Entrada;
        parameterInputs[1].text = itemToUpdate.Patrimonio;
        parameterInputs[2].text = itemToUpdate.Status;
        parameterInputs[3].text = itemToUpdate.Serial;
        parameterInputs[4].text = itemToUpdate.Fabricante;
        parameterInputs[5].text = itemToUpdate.Modelo;
        parameterInputs[6].text = itemToUpdate.Local;
        parameterInputs[7].text = itemToUpdate.Saida;
        parameterInputs[8].text = itemToUpdate.Observacao;

        parameterNames[0].text = "Entrada no estoque";
        parameterNames[1].text = "Patrimônio";
        parameterNames[2].text = "Status";
        parameterNames[3].text = "Serial";
        parameterNames[4].text = "Fabricante";
        parameterNames[5].text = "Modelo";
        parameterNames[6].text = "Local";
        parameterNames[7].text = "Saída do estoque";
        parameterNames[8].text = "Observação";

        switch (itemToUpdate.Categoria)
        {
            #region HD
            case ConstStrings.HD:
                parameterInputs[9].text = itemToUpdate.Interface;
                parameterInputs[10].text = itemToUpdate.Tamanho;
                parameterInputs[11].text = itemToUpdate.FormaDeArmazenamento;
                parameterInputs[12].text = itemToUpdate.CapacidadeEmGB;
                parameterInputs[13].text = itemToUpdate.RPM;
                parameterInputs[14].text = itemToUpdate.VelocidadeDeLeitura;
                parameterInputs[15].text = itemToUpdate.Enterprise;
                parameterNames[9].text = "Interface";
                parameterNames[10].text = "Tamanho";
                parameterNames[11].text = "Forma de armazenamento";
                parameterNames[12].text = "Capacidade (GB)";
                parameterNames[13].text = "RPM";
                parameterNames[14].text = "Velocidade de Leitura (Gb/s)";
                parameterNames[15].text = "Enterprise";
                parameterNames[16].text = "";
                parameterNames[17].text = "";
                break;
            #endregion
            #region Memoria
            case ConstStrings.Memoria:
                parameterInputs[9].text = itemToUpdate.Tipo;
                parameterInputs[10].text = itemToUpdate.CapacidadeEmGB;
                parameterInputs[11].text = itemToUpdate.VelocidadeMHz;
                parameterInputs[12].text = itemToUpdate.LowVoltage;
                parameterInputs[13].text = itemToUpdate.Rank;
                parameterInputs[14].text = itemToUpdate.DIMM;
                parameterInputs[15].text = itemToUpdate.TaxaDeTransmissao;
                parameterInputs[16].text = itemToUpdate.Simbolo;
                parameterNames[9].text = "Tipo";
                parameterNames[10].text = "Capacidade (GB)";
                parameterNames[11].text = "Velocidade (MHz)";
                parameterNames[12].text = "É low voltage?";
                parameterNames[13].text = "Rank";
                parameterNames[14].text = "DIMM";
                parameterNames[15].text = "Taxa de transmissão";
                parameterNames[16].text = "Símbolo";
                parameterNames[17].text = "";
                break;
            #endregion
            #region Placa de rede
            case ConstStrings.PlacaDeRede:
                parameterInputs[9].text = itemToUpdate.Interface;
                parameterInputs[10].text = itemToUpdate.QuantidadeDePortas;
                parameterInputs[11].text = itemToUpdate.QuaisConexoes;
                parameterInputs[12].text = itemToUpdate.SuportaFibraOptica;
                parameterInputs[13].text = itemToUpdate.Desempenho;
                parameterNames[9].text = "Interface";
                parameterNames[10].text = "Quantas portas?";
                parameterNames[11].text = "Quais portas?";
                parameterNames[12].text = "Suporta fibra óptica?";
                parameterNames[13].text = "Desempenho (MB/s)";
                parameterNames[14].text = "";
                parameterNames[15].text = "";
                parameterNames[16].text = "";
                parameterNames[17].text = "";
                break;
            #endregion
            #region iDrac
            case ConstStrings.Idrac:
                parameterInputs[9].text = itemToUpdate.QuaisConexoes;
                parameterInputs[10].text = itemToUpdate.VelocidadeGBs;
                parameterInputs[11].text = itemToUpdate.EntradaSD;
                parameterInputs[12].text = itemToUpdate.ServidoresSuportados;
                parameterNames[9].text = "Porta";
                parameterNames[10].text = "Velocidade (GB/s)";
                parameterNames[11].text = "Entrada SD";
                parameterNames[12].text = "Servidores suportados";
                parameterNames[13].text = "";
                parameterNames[14].text = "";
                parameterNames[15].text = "";
                parameterNames[16].text = "";
                parameterNames[17].text = "";
                break;
            #endregion
            #region Placa controladora
            case ConstStrings.PlacaControladora:
                parameterInputs[9].text = itemToUpdate.QuaisConexoes;
                parameterInputs[10].text = itemToUpdate.QuantidadeDePortas;
                parameterInputs[11].text = itemToUpdate.TipoDeRAID;
                parameterInputs[12].text = itemToUpdate.TipoDeHD;
                parameterInputs[13].text = itemToUpdate.CapacidadeMaxHD;
                parameterInputs[14].text = itemToUpdate.AteQuantosHDs;
                parameterInputs[15].text = itemToUpdate.BateriaInclusa;
                parameterInputs[16].text = itemToUpdate.Barramento;
                parameterNames[9].text = "Tipo de conexão";
                parameterNames[10].text = "Quantas portas?";
                parameterNames[11].text = "Tipos de RAID";
                parameterNames[12].text = "Tipo de HD";
                parameterNames[13].text = "Capacidade máx do HD (TB)";
                parameterNames[14].text = "Até quantos HDs";
                parameterNames[15].text = "Bateria inclusa?";
                parameterNames[16].text = "Barramento";
                parameterNames[17].text = "";
                break;
            #endregion
            #region Processador
            case ConstStrings.Processador:
                parameterInputs[9].text = itemToUpdate.Soquete;
                parameterInputs[10].text = itemToUpdate.NucleosFisicos;
                parameterInputs[11].text = itemToUpdate.NucleosLogicos;
                parameterInputs[12].text = itemToUpdate.AceitaVirtualizacao;
                parameterInputs[13].text = itemToUpdate.TurboBoost;
                parameterInputs[14].text = itemToUpdate.HyperThreading;
                parameterNames[9].text = "Soquete";
                parameterNames[10].text = "Nº núcleos físicos";
                parameterNames[11].text = "Nº núcleos lógicos";
                parameterNames[12].text = "Aceita virtualização?";
                parameterNames[13].text = "Turbo boost?";
                parameterNames[14].text = "Hyper-Threading?";
                parameterNames[15].text = "";
                parameterNames[16].text = "";
                parameterNames[17].text = "";
                break;
            #endregion
            #region Desktop
            case ConstStrings.Desktop:
                parameterInputs[9].text = itemToUpdate.ModeloPlacaMae;
                parameterInputs[10].text = itemToUpdate.Fonte;
                parameterInputs[11].text = itemToUpdate.Memoria;
                parameterInputs[12].text = itemToUpdate.HD;
                parameterInputs[13].text = itemToUpdate.PlacaDeVideo;
                parameterInputs[14].text = itemToUpdate.PlacaDeRede;
                parameterInputs[15].text = itemToUpdate.LeitorDeDVD;
                parameterInputs[16].text = itemToUpdate.Processador;
                parameterNames[9].text = "Modelo de placa mãe";
                parameterNames[10].text = "Fonte?";
                parameterNames[11].text = "Memória?";
                parameterNames[12].text = "HD?";
                parameterNames[13].text = "Placa de vídeo?";
                parameterNames[14].text = "Placa de rede?";
                parameterNames[15].text = "Leitor de DVD?";
                parameterNames[16].text = "Processador?";
                break;
            #endregion
            #region Fonte
            case ConstStrings.Fonte:
                parameterInputs[9].text = itemToUpdate.Watts;
                parameterInputs[10].text = itemToUpdate.OndeFunciona;
                parameterInputs[11].text = itemToUpdate.Conectores;
                parameterNames[9].text = "Watts de potência";
                parameterNames[10].text = "Onde funciona?";
                parameterNames[11].text = "Conectores";
                parameterNames[12].text = "";
                parameterNames[13].text = "";
                parameterNames[14].text = "";
                parameterNames[15].text = "";
                parameterNames[16].text = "";
                parameterNames[17].text = "";
                break;
            #endregion
            #region Switch
            case ConstStrings.Switch:
                parameterInputs[9].text = itemToUpdate.QuantidadeDePortas;
                parameterInputs[10].text = itemToUpdate.Desempenho;
                parameterNames[9].text = "Quantas entradas";
                parameterNames[10].text = "Capacidade máx de cada porta (MB/s)";
                parameterNames[11].text = "";
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
                parameterInputs[9].text = itemToUpdate.Wireless;
                parameterInputs[10].text = itemToUpdate.QuantidadeDePortas;
                parameterInputs[11].text = itemToUpdate.BandaMaxima;
                parameterNames[9].text = "Wireless?";
                parameterNames[10].text = "Quantas entradas?";
                parameterNames[11].text = "Banda máx (MB/s)";
                parameterNames[12].text = "Voltagem";
                parameterNames[13].text = "";
                parameterNames[14].text = "";
                parameterNames[15].text = "";
                parameterNames[16].text = "";
                parameterNames[17].text = "";
                break;
            #endregion
            #region Carregador
            case ConstStrings.Carregador:
                parameterInputs[9].text = itemToUpdate.OndeFunciona;
                parameterInputs[10].text = itemToUpdate.VoltagemDeSaida;
                parameterInputs[11].text = itemToUpdate.AmperagemDeSaida;
                parameterNames[9].text = "Onde funciona?";
                parameterNames[10].text = "Voltagem de saída";
                parameterNames[11].text = "Amperagem de saída (mA)";
                parameterNames[12].text = "";
                parameterNames[13].text = "";
                parameterNames[14].text = "";
                parameterNames[15].text = "";
                parameterNames[16].text = "";
                parameterNames[17].text = "";
                break;
            #endregion
            #region Adaptador AC
            case ConstStrings.AdaptadorAC:
                parameterInputs[9].text = itemToUpdate.OndeFunciona;
                parameterInputs[10].text = itemToUpdate.VoltagemDeSaida;
                parameterInputs[11].text = itemToUpdate.AmperagemDeSaida;
                parameterNames[9].text = "Onde funciona?";
                parameterNames[10].text = "Voltagem de saída";
                parameterNames[11].text = "Amperagem de saída (A)";
                parameterNames[12].text = "";
                parameterNames[13].text = "";
                parameterNames[14].text = "";
                parameterNames[15].text = "";
                parameterNames[16].text = "";
                parameterNames[17].text = "";
                break;
            #endregion
            #region Storage NAS
            case ConstStrings.StorageNAS:
                parameterInputs[9].text = itemToUpdate.Tamanho;
                parameterInputs[10].text = itemToUpdate.TipoDeRAID;
                parameterInputs[11].text = itemToUpdate.TipoDeHD;
                parameterInputs[12].text = itemToUpdate.CapacidadeMaxHD;
                parameterNames[9].text = "Tamanho dos HDs";
                parameterNames[10].text = "Tipos de RAID";
                parameterNames[11].text = "Tipo de HD";
                parameterNames[12].text = "Capacidade máx do HD";
                parameterNames[13].text = "Até quantos HDs";
                parameterNames[14].text = "";
                parameterNames[15].text = "";
                parameterNames[16].text = "";
                parameterNames[17].text = "";
                break;
            #endregion
            #region GBIC
            case ConstStrings.Gbic:
                parameterInputs[9].text = itemToUpdate.Desempenho;
                parameterNames[9].text = "Desempenho máx (GB/s)";
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
            #region Placa de video
            case ConstStrings.PlacaDeVideo:
                parameterInputs[9].text = itemToUpdate.QuantidadeDePortas;
                parameterInputs[10].text = itemToUpdate.QuaisConexoes;
                parameterNames[9].text = "Quantas entradas?";
                parameterNames[10].text = "Quais entradas?";
                parameterNames[11].text = "";
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
                parameterInputs[9].text = itemToUpdate.QuantosCanais;
                parameterNames[9].text = "Quantos canais?";
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
            #region Placa de captura de vídeo
            case ConstStrings.PlacaDeCapturaDeVideo:
                parameterInputs[9].text = itemToUpdate.QuantidadeDePortas;
                parameterNames[9].text = "Quantas entradas?";
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
            #region Servidor
            case ConstStrings.Servidor:
                parameterNames[9].text = "";
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
                parameterNames[9].text = "";
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
                parameterNames[9].text = "Polegadas";
                parameterNames[10].text = "Tipos de entradas";
                parameterNames[11].text = "";
                parameterNames[12].text = "";
                parameterNames[13].text = "";
                parameterNames[14].text = "";
                parameterNames[15].text = "";
                parameterNames[16].text = "";
                parameterNames[17].text = "";
                break;
            #endregion

            default:
                parameterNames[9].text = "";
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

    private IEnumerator UpdateDatabaseRoutine()
    {
        #region Update inventario
        WWWForm itemForm = CreateAddItemForm.GetInventarioForm(parameterInputs[0].text, parameterInputs[1].text, parameterInputs[2].text, 
        parameterInputs[3].text, parameterInputs[17].text, parameterInputs[4].text, parameterInputs[5].text, parameterInputs[6].text, parameterInputs[7].text, 
        parameterInputs[8].text);
       
        UnityWebRequest createUpdateInventarioRequest = UnityWebRequest.Post(ConstStrings.PhpUpdateItemsFolder + "updateinventario.php", itemForm);
        yield return createUpdateInventarioRequest.SendWebRequest();

        if (createUpdateInventarioRequest.result == UnityWebRequest.Result.ConnectionError)
        {
            Debug.LogWarning("conectionerror");
        }
        else if (createUpdateInventarioRequest.result == UnityWebRequest.Result.DataProcessingError)
        {
            Debug.LogWarning("data processing error");
        }
        else if (createUpdateInventarioRequest.result == UnityWebRequest.Result.ProtocolError)
        {
            Debug.LogWarning("protocol error");
        }

        if (createUpdateInventarioRequest.error == null)
        {
            string response = createUpdateInventarioRequest.downloadHandler.text;
            if (response == "1" || response == "2")
            {
                Debug.LogWarning("Server error");
            }
            else if (response == "3")
            {
                Debug.LogWarning("Item does not exist");
            }
            else
            {
                //TODO show success message
            }

        }
        else
        {
            Debug.LogWarning(createUpdateInventarioRequest.error);
            // TODO send message to user with error and recomendation
        }
        createUpdateInventarioRequest.Dispose();
        #endregion
        #region Update the details tables
        switch (itemToUpdate.Categoria)
        {
            #region HD
            case ConstStrings.HD:

                WWWForm hdForm = CreateAddItemForm.GetHDForm(parameterInputs[6].text, parameterInputs[11].text, parameterInputs[12].text,
                parameterInputs[13].text, parameterInputs[14].text, parameterInputs[15].text, parameterInputs[16].text, parameterInputs[17].text, parameterInputs[18].text);

                UnityWebRequest createUpdateHDRequest = UnityWebRequest.Post(ConstStrings.PhpUpdateItemsFolder + "updatehd.php", hdForm);
                yield return createUpdateHDRequest.SendWebRequest();

                if (createUpdateHDRequest.result == UnityWebRequest.Result.ConnectionError)
                {
                    Debug.LogWarning("conectionerror");
                }
                else if (createUpdateHDRequest.result == UnityWebRequest.Result.DataProcessingError)
                {
                    Debug.LogWarning("data processing error");
                }
                else if (createUpdateHDRequest.result == UnityWebRequest.Result.ProtocolError)
                {
                    Debug.LogWarning("protocol error");
                }

                if (createUpdateHDRequest.error == null)
                {
                    string response = createUpdateHDRequest.downloadHandler.text;
                    if (response == "1" || response == "2")
                    {
                        Debug.LogWarning("Server error");
                        // TODO send message to user with error and recomendation
                    }
                    else if (response == "3")
                    {
                        Debug.LogWarning("Item does not exist");
                        // TODO send message to user with error and recomendation
                    }
                    else
                    {
                        //TODO show success message
                    }

                }
                else
                {
                    Debug.LogWarning(createUpdateHDRequest.error);
                    // TODO send message to user with error and recomendation
                }
                createUpdateHDRequest.Dispose();
                break;
            #endregion
            #region Memíria
            case ConstStrings.Memoria:
                WWWForm memoriaForm = CreateAddItemForm.GetMemoriaForm(parameterInputs[6].text, parameterInputs[7].text,
                parameterInputs[9].text, parameterInputs[10].text, parameterInputs[11].text, parameterInputs[12].text,
                parameterInputs[13].text, parameterInputs[14].text, parameterInputs[15].text, parameterInputs[16].text);
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
            case ConstStrings.PlacaDeRede:
                itemToUpdate.Interface = parameterInputs[9].text;
                itemToUpdate.QuantidadeDePortas = parameterInputs[10].text;
                itemToUpdate.QuaisConexoes = parameterInputs[11].text;
                itemToUpdate.SuportaFibraOptica = parameterInputs[12].text;
                itemToUpdate.Desempenho = parameterInputs[13].text;
                break;
            #endregion
            #region iDRAC
            case ConstStrings.Idrac:
                itemToUpdate.QuaisConexoes = parameterInputs[9].text;
                itemToUpdate.VelocidadeGBs = parameterInputs[10].text;
                itemToUpdate.EntradaSD = parameterInputs[11].text;
                itemToUpdate.ServidoresSuportados = parameterInputs[12].text;
                break;
            #endregion
            #region Placa controladora
            case ConstStrings.PlacaControladora:
                itemToUpdate.QuaisConexoes = parameterInputs[9].text;
                itemToUpdate.QuantidadeDePortas = parameterInputs[10].text;
                itemToUpdate.TipoDeRAID = parameterInputs[11].text;
                itemToUpdate.CapacidadeMaxHD = parameterInputs[12].text;
                itemToUpdate.AteQuantosHDs = parameterInputs[13].text;
                itemToUpdate.BateriaInclusa = parameterInputs[14].text;
                itemToUpdate.Barramento = parameterInputs[15].text;
                break;
            #endregion
            #region Processador
            case ConstStrings.Processador:
                itemToUpdate.Soquete = parameterInputs[9].text;
                itemToUpdate.NucleosFisicos = parameterInputs[10].text;
                itemToUpdate.NucleosLogicos = parameterInputs[11].text;
                itemToUpdate.AceitaVirtualizacao = parameterInputs[12].text;
                itemToUpdate.TurboBoost = parameterInputs[13].text;
                itemToUpdate.HyperThreading = parameterInputs[14].text;
                break;
            #endregion
            #region Desktop
            case ConstStrings.Desktop:
                itemToUpdate.ModeloPlacaMae = parameterInputs[9].text;
                itemToUpdate.Fonte = parameterInputs[10].text;
                itemToUpdate.Memoria = parameterInputs[11].text;
                itemToUpdate.HD = parameterInputs[12].text;
                itemToUpdate.PlacaDeVideo = parameterInputs[13].text;
                itemToUpdate.LeitorDeDVD = parameterInputs[14].text;
                break;
            #endregion
            #region Fonte
            case ConstStrings.Fonte:
                itemToUpdate.Watts = parameterInputs[9].text;
                itemToUpdate.OndeFunciona = parameterInputs[10].text;
                itemToUpdate.Conectores = parameterInputs[11].text;
                break;
            #endregion
            #region Switch
            case ConstStrings.Switch:
                itemToUpdate.QuantidadeDePortas = parameterInputs[9].text;
                itemToUpdate.Desempenho = parameterInputs[10].text;
                break;
            #endregion
            #region Roteador
            case ConstStrings.Roteador:
                itemToUpdate.Wireless = parameterInputs[9].text;
                itemToUpdate.QuantidadeDePortas = parameterInputs[10].text;
                itemToUpdate.BandaMaxima = parameterInputs[11].text;
                break;
            #endregion
            case ConstStrings.Carregador:
                itemToUpdate.OndeFunciona = parameterInputs[9].text;
                itemToUpdate.VoltagemDeSaida = parameterInputs[10].text;
                itemToUpdate.AmperagemDeSaida = parameterInputs[11].text;
                break;
            case ConstStrings.AdaptadorAC:
                itemToUpdate.OndeFunciona = parameterInputs[9].text;
                itemToUpdate.VoltagemDeSaida = parameterInputs[10].text;
                itemToUpdate.AmperagemDeSaida = parameterInputs[11].text;
                break;
            case ConstStrings.StorageNAS:
                itemToUpdate.Tamanho = parameterInputs[9].text;
                itemToUpdate.TipoDeRAID = parameterInputs[10].text;
                itemToUpdate.TipoDeHD = parameterInputs[11].text;
                itemToUpdate.CapacidadeMaxHD = parameterInputs[12].text;
                break;
            case ConstStrings.Gbic:
                itemToUpdate.Desempenho = parameterInputs[9].text;
                break;
            case ConstStrings.PlacaDeVideo:
                itemToUpdate.QuantidadeDePortas = parameterInputs[9].text;
                itemToUpdate.QuaisConexoes = parameterInputs[10].text;
                break;
            case ConstStrings.PlacaDeSom:
                itemToUpdate.QuantosCanais = parameterInputs[9].text;
                break;
            case ConstStrings.PlacaDeCapturaDeVideo:
                itemToUpdate.QuantidadeDePortas = parameterInputs[9].text;
                break;

            default:
                break;
        }
        #endregion
    }
    /// <summary>
    /// Updates the full database
    /// </summary>
    private void UpdateFullDatabase()
    {
        itemToUpdate.Entrada = parameterInputs[0].text;
        itemToUpdate.Patrimonio = parameterInputs[1].text;
        itemToUpdate.Status = parameterInputs[2].text;
        itemToUpdate.Serial = parameterInputs[3].text;
        itemToUpdate.Fabricante = parameterInputs[4].text;
        itemToUpdate.Modelo = parameterInputs[5].text;
        itemToUpdate.Local = parameterInputs[6].text;
        itemToUpdate.Saida = parameterInputs[7].text;
        itemToUpdate.Saida = parameterInputs[17].text;
        switch (itemToUpdate.Categoria)
        {
            case ConstStrings.HD:
                
                itemToUpdate.Interface = parameterInputs[9].text;
                itemToUpdate.Tamanho = parameterInputs[10].text;
                itemToUpdate.FormaDeArmazenamento = parameterInputs[11].text;
                itemToUpdate.CapacidadeEmGB = parameterInputs[12].text;
                itemToUpdate.RPM = parameterInputs[13].text;
                itemToUpdate.VelocidadeDeLeitura = parameterInputs[14].text;
                itemToUpdate.Enterprise = parameterInputs[15].text;
                break;
            case ConstStrings.Memoria:
                itemToUpdate.Tipo = parameterInputs[9].text;
                itemToUpdate.CapacidadeEmGB = parameterInputs[10].text;
                itemToUpdate.VelocidadeMHz = parameterInputs[11].text;
                itemToUpdate.LowVoltage = parameterInputs[12].text;
                itemToUpdate.Rank = parameterInputs[13].text;
                itemToUpdate.DIMM = parameterInputs[14].text;
                itemToUpdate.TaxaDeTransmissao = parameterInputs[15].text;
                itemToUpdate.Simbolo = parameterInputs[16].text;
                break;
            case ConstStrings.PlacaDeRede:
                itemToUpdate.Interface = parameterInputs[9].text;
                itemToUpdate.QuantidadeDePortas = parameterInputs[10].text;
                itemToUpdate.QuaisConexoes = parameterInputs[11].text;
                itemToUpdate.SuportaFibraOptica = parameterInputs[12].text;
                itemToUpdate.Desempenho = parameterInputs[13].text;
                break;
            case ConstStrings.Idrac:
                itemToUpdate.QuaisConexoes = parameterInputs[9].text;
                itemToUpdate.VelocidadeGBs = parameterInputs[10].text;
                itemToUpdate.EntradaSD = parameterInputs[11].text;
                itemToUpdate.ServidoresSuportados = parameterInputs[12].text;
                break;
            case ConstStrings.PlacaControladora:
                itemToUpdate.QuaisConexoes = parameterInputs[9].text;
                itemToUpdate.QuantidadeDePortas = parameterInputs[10].text;
                itemToUpdate.TipoDeRAID = parameterInputs[11].text;
                itemToUpdate.CapacidadeMaxHD = parameterInputs[12].text;
                itemToUpdate.AteQuantosHDs = parameterInputs[13].text;
                itemToUpdate.BateriaInclusa = parameterInputs[14].text;
                itemToUpdate.Barramento = parameterInputs[15].text;
                break;
            case ConstStrings.Processador:
                itemToUpdate.Soquete = parameterInputs[9].text;
                itemToUpdate.NucleosFisicos = parameterInputs[10].text;
                itemToUpdate.NucleosLogicos = parameterInputs[11].text;
                itemToUpdate.AceitaVirtualizacao = parameterInputs[12].text;
                itemToUpdate.TurboBoost = parameterInputs[13].text;
                itemToUpdate.HyperThreading = parameterInputs[14].text;
                break;
            case ConstStrings.Desktop:
                itemToUpdate.ModeloPlacaMae = parameterInputs[9].text;
                itemToUpdate.Fonte = parameterInputs[10].text;
                itemToUpdate.Memoria = parameterInputs[11].text;
                itemToUpdate.HD = parameterInputs[12].text;
                itemToUpdate.PlacaDeVideo = parameterInputs[13].text;
                itemToUpdate.LeitorDeDVD = parameterInputs[14].text;
                break;
            case ConstStrings.Fonte:
                itemToUpdate.Watts = parameterInputs[9].text;
                itemToUpdate.OndeFunciona = parameterInputs[10].text;
                itemToUpdate.Conectores = parameterInputs[11].text;
                break;
            case ConstStrings.Switch:
                itemToUpdate.QuantidadeDePortas = parameterInputs[9].text;
                itemToUpdate.Desempenho = parameterInputs[10].text;
                break;
            case ConstStrings.Roteador:
                itemToUpdate.Wireless = parameterInputs[9].text;
                itemToUpdate.QuantidadeDePortas = parameterInputs[10].text;
                itemToUpdate.BandaMaxima = parameterInputs[11].text;
                break;
            case ConstStrings.Carregador:
                itemToUpdate.OndeFunciona = parameterInputs[9].text;
                itemToUpdate.VoltagemDeSaida = parameterInputs[10].text;
                itemToUpdate.AmperagemDeSaida = parameterInputs[11].text;
                break;
            case ConstStrings.AdaptadorAC:
                itemToUpdate.OndeFunciona = parameterInputs[9].text;
                itemToUpdate.VoltagemDeSaida = parameterInputs[10].text;
                itemToUpdate.AmperagemDeSaida = parameterInputs[11].text;
                break;
            case ConstStrings.StorageNAS:
                itemToUpdate.Tamanho = parameterInputs[9].text;
                itemToUpdate.TipoDeRAID = parameterInputs[10].text;
                itemToUpdate.TipoDeHD = parameterInputs[11].text;
                itemToUpdate.CapacidadeMaxHD = parameterInputs[12].text;
                break;
            case ConstStrings.Gbic:
                itemToUpdate.Desempenho = parameterInputs[9].text;
                break;
            case ConstStrings.PlacaDeVideo:
                itemToUpdate.QuantidadeDePortas = parameterInputs[9].text;
                itemToUpdate.QuaisConexoes = parameterInputs[10].text;
                break;
            case ConstStrings.PlacaDeSom:
                itemToUpdate.QuantosCanais = parameterInputs[9].text;
                break;
            case ConstStrings.PlacaDeCapturaDeVideo:
                itemToUpdate.QuantidadeDePortas = parameterInputs[9].text;
                break;

            default:
                break;
        }
        itemToUpdateIndex = ConsultDatabase.Instance.GetItemIndex();
        InternalDatabase.Instance.fullDatabase.itens[itemToUpdateIndex] = itemToUpdate;
        ShowMessage();
        EventHandler.CallDatabaseUpdatedEvent(ConstStrings.DataDatabaseSaveFile);
    }

    /// <summary>
    /// Show message if item to update was not found and, if it was found sho message that item was updated
    /// </summary>
    private void ShowMessage()
    {
        messagePanel.SetActive(true);
        if (itemToUpdate == null)
        {
            messageText.text = "Item não encontrado. Confira se o parâmetro digitado está correto";
        }
        else
        {
            messageText.text = "Item atualizado com sucesso";
        }
        StartCoroutine(CloseShowMessagePanelRoutine());
    }

    /// <summary>
    /// Wait a few seconds to close the MessagePanel
    /// </summary>
    private IEnumerator CloseShowMessagePanelRoutine()
    {
        yield return new WaitForSeconds(10f);
        CloseMessagePanel();
    }

    /// <summary>
    /// Resets the texts on all inputs
    /// </summary>
    private void ResetInputs()
    {
        for (int i = 0; i < parameterItems.Length; i++)
        {
            parameterItems[i].SetActive(true);
            parameterInputs[i].text = "";
            parameterNames[i].text = "";
            placeholders[i].text = "Digite o valor";
        }
        
        inputsPanel.SetActive(false);
    }

    /// <summary>
    /// Close  message panel
    /// </summary>
    public void CloseMessagePanel()
    {
        messagePanel.SetActive(false);
        StopCoroutine(CloseShowMessagePanelRoutine());
        ResetInputs();
        searchingItem = true;
        itemToUpdateParameter.text = "";
    }

    /// <summary>
    /// Returns to InitialScene
    /// </summary>
    public void ReturnToPreviousScreen()
    {
        SceneManager.LoadScene(ConstStrings.SceneInitial);
    }
}
