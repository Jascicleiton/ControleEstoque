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
                StartCoroutine(UpdateDatabaseRoutine());
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
            itemForm = CreateForm.GetConsultPatrimonioForm(ConstStrings.ConsultKey, itemToUpdateParameter.text);
            createItemUpdatePostRequest = UnityWebRequest.Post(ConstStrings.PhpUpdateItemsFolder + "getitempatrimoniotoupdate.php", itemForm);
        }
        if (parameterToSearchDP.value == 1)
        {
            itemForm = CreateForm.GetConsultSerialForm(ConstStrings.ConsultKey, itemToUpdateParameter.text);
            createItemUpdatePostRequest = UnityWebRequest.Post(ConstStrings.PhpUpdateItemsFolder + "getitemserialtoupdate.php", itemForm);
        }

        MouseManager.Instance.SetWaitingCursor(); 
        yield return createItemUpdatePostRequest.SendWebRequest();

        if (createItemUpdatePostRequest.result == UnityWebRequest.Result.ConnectionError)
        {
            Debug.LogWarning("CheckIfItemExists: conectionerror");
        }
        else if (createItemUpdatePostRequest.result == UnityWebRequest.Result.DataProcessingError)
        {
            Debug.LogWarning("CheckIfItemExists: data processing error");
        }
        else if (createItemUpdatePostRequest.result == UnityWebRequest.Result.ProtocolError)
        {
            Debug.LogWarning("CheckIfItemExists: protocol error");
        }

        if (createItemUpdatePostRequest.error == null)
        {
            string response = createItemUpdatePostRequest.downloadHandler.text;
            if (response == "1")
            {
                Debug.LogWarning("CheckIfItemExists: conectionerror error");
            }
            else if  (response == "2")
            {
                Debug.LogWarning("CheckIfItemExists: query error");

            }
            else if (response == "3")
            {
                Debug.LogWarning("CheckIfItemExists: Item does not exist");
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
        MouseManager.Instance.SetDefaultCursor();

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
        parameterInputs[4].text = itemToUpdate.Categoria;
        parameterInputs[5].text = itemToUpdate.Fabricante;
        parameterInputs[6].text = itemToUpdate.Modelo;
        parameterInputs[7].text = itemToUpdate.Local;
        parameterInputs[8].text = itemToUpdate.Saida;
        parameterInputs[9].text = itemToUpdate.Observacao;

        parameterNames[0].text = "Entrada no estoque";
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

    private IEnumerator UpdateDatabaseRoutine()
    {
        #region Update inventario
        WWWForm itemForm = CreateForm.GetInventarioForm(ConstStrings.UpdateItemKey, parameterInputs[0].text, parameterInputs[1].text,
        parameterInputs[2].text, parameterInputs[3].text, parameterInputs[4].text, parameterInputs[5].text,
        parameterInputs[6].text, parameterInputs[7].text, parameterInputs[8].text, parameterInputs[9].text);

        UnityWebRequest createUpdateInventarioRequest = UnityWebRequest.Post(ConstStrings.PhpUpdateItemsFolder + "updateinventario.php", itemForm);
        MouseManager.Instance.SetWaitingCursor();
        yield return createUpdateInventarioRequest.SendWebRequest();

        if (createUpdateInventarioRequest.result == UnityWebRequest.Result.ConnectionError)
        {
            Debug.LogWarning("UpdateDatabaseRoutine: conectionerror");
        }
        else if (createUpdateInventarioRequest.result == UnityWebRequest.Result.DataProcessingError)
        {
            Debug.LogWarning("UpdateDatabaseRoutine: data processing error");
        }
        else if (createUpdateInventarioRequest.result == UnityWebRequest.Result.ProtocolError)
        {
            Debug.LogWarning("UpdateDatabaseRoutine: protocol error");
        }

        if (createUpdateInventarioRequest.error == null)
        {
            string response = createUpdateInventarioRequest.downloadHandler.text;
            if (response == "Conection error" || response == "Inventario update failed")
            {
                Debug.LogWarning("UpdateDatabaseRoutine: Server error");
            }
            else if (response == "Wrong app key")
            {
                Debug.LogWarning("UpdateDatabaseRoutine: app key");
            }
            else
            {
                Debug.Log("Worked");
                UpdateFullDatabase();
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
     //   switch (itemToUpdate.Categoria)
     //   {
     //       #region HD
     //       case ConstStrings.HD:

     //           WWWForm hdForm = CreateAddItemForm.GetHDForm(parameterInputs[6].text, parameterInputs[5].text,
     //           parameterInputs[9].text, parameterInputs[10].text, parameterInputs[11].text, parameterInputs[12].text,
     //           parameterInputs[13].text, parameterInputs[14].text, parameterInputs[15].text);

     //           UnityWebRequest createUpdateHDRequest = UnityWebRequest.Post(ConstStrings.PhpUpdateItemsFolder + "updatehd.php", hdForm);
     //           yield return createUpdateHDRequest.SendWebRequest();

     //           if (createUpdateHDRequest.result == UnityWebRequest.Result.ConnectionError)
     //           {
     //               Debug.LogWarning("conectionerror");
     //           }
     //           else if (createUpdateHDRequest.result == UnityWebRequest.Result.DataProcessingError)
     //           {
     //               Debug.LogWarning("data processing error");
     //           }
     //           else if (createUpdateHDRequest.result == UnityWebRequest.Result.ProtocolError)
     //           {
     //               Debug.LogWarning("protocol error");
     //           }

     //           if (createUpdateHDRequest.error == null)
     //           {
     //               string response = createUpdateHDRequest.downloadHandler.text;
     //               if (response == "1" || response == "2")
     //               {
     //                   Debug.LogWarning("Server error");
     //                   // TODO send message to user with error and recomendation
     //               }
     //               else if (response == "3")
     //               {
     //                   Debug.LogWarning("Item does not exist");
     //                   // TODO send message to user with error and recomendation
     //               }
     //               else
     //               {
     //                   //TODO show success message
     //               }

     //           }
     //           else
     //           {
     //               Debug.LogWarning(createUpdateHDRequest.error);
     //               // TODO send message to user with error and recomendation
     //           }
     //           createUpdateHDRequest.Dispose();
     //           break;
     //       #endregion
     //       #region Memória
     //       case ConstStrings.Memoria:
     //           WWWForm memoriaForm = CreateAddItemForm.GetMemoriaForm(parameterInputs[6].text, parameterInputs[5].text,
     //           parameterInputs[9].text, parameterInputs[10].text, parameterInputs[11].text, parameterInputs[12].text,
     //           parameterInputs[13].text, parameterInputs[14].text, parameterInputs[15].text, parameterInputs[16].text);
     //           UnityWebRequest createMemoriaPostRequest = UnityWebRequest.Post(ConstStrings.PhpAdditemsFolder + "updatememoria.php", memoriaForm);
     //           yield return createMemoriaPostRequest.SendWebRequest();

     //           if (createMemoriaPostRequest.result == UnityWebRequest.Result.ConnectionError)
     //           {
     //               Debug.LogWarning("conectionerror");
     //           }
     //           else if (createMemoriaPostRequest.result == UnityWebRequest.Result.DataProcessingError)
     //           {
     //               Debug.LogWarning("data processing error");
     //           }
     //           else if (createMemoriaPostRequest.result == UnityWebRequest.Result.ProtocolError)
     //           {
     //               Debug.LogWarning("protocol error");
     //           }

     //           if (createMemoriaPostRequest.error == null)
     //           {

     //               string response = createMemoriaPostRequest.downloadHandler.text;
     //               if (response == "1" || response == "2" || response == "5")
     //               {

     //               }
     //               else if (response == "3")
     //               {

     //               }
     //               else if (response == "4")
     //               {

     //               }
     //               else
     //               {

     //               }

     //           }
     //           else
     //           {

     //           }
     //           createMemoriaPostRequest.Dispose();
     //           break;
     //       #endregion
     //       #region Placa de rede
     //       case ConstStrings.PlacaDeRede:

     //           WWWForm placaDeRedeForm = CreateAddItemForm.GetPlacaDeRedeForm(parameterInputs[6].text, parameterInputs[5].text,
     //           parameterInputs[9].text, parameterInputs[10].text, parameterInputs[11].text, parameterInputs[11].text,
     //           parameterInputs[13].text);

     //           UnityWebRequest createUpdatePlacaDeRedeRequest = UnityWebRequest.Post(ConstStrings.PhpUpdateItemsFolder + "updateplacaderede.php", placaDeRedeForm);
     //           yield return createUpdatePlacaDeRedeRequest.SendWebRequest();

     //           if (createUpdatePlacaDeRedeRequest.result == UnityWebRequest.Result.ConnectionError)
     //           {
     //               Debug.LogWarning("conectionerror");
     //           }
     //           else if (createUpdatePlacaDeRedeRequest.result == UnityWebRequest.Result.DataProcessingError)
     //           {
     //               Debug.LogWarning("data processing error");
     //           }
     //           else if (createUpdatePlacaDeRedeRequest.result == UnityWebRequest.Result.ProtocolError)
     //           {
     //               Debug.LogWarning("protocol error");
     //           }

     //           if (createUpdatePlacaDeRedeRequest.error == null)
     //           {
     //               string response = createUpdatePlacaDeRedeRequest.downloadHandler.text;
     //               if (response == "1" || response == "2")
     //               {
     //                   Debug.LogWarning("Server error");
     //                   // TODO send message to user with error and recomendation
     //               }
     //               else if (response == "3")
     //               {
     //                   Debug.LogWarning("Item does not exist");
     //                   // TODO send message to user with error and recomendation
     //               }
     //               else
     //               {
     //                   //TODO show success message
     //               }

     //           }
     //           else
     //           {
     //               Debug.LogWarning(createUpdatePlacaDeRedeRequest.error);
     //               // TODO send message to user with error and recomendation
     //           }
     //           createUpdatePlacaDeRedeRequest.Dispose();
     //           break;
     //       #endregion
     //       #region iDRAC
     //       case ConstStrings.Idrac:
     //           WWWForm idracForm = CreateAddItemForm.GetiDracForm(parameterInputs[6].text, parameterInputs[5].text,
     //          parameterInputs[9].text, parameterInputs[10].text, parameterInputs[11].text, parameterInputs[12].text);
     //           UnityWebRequest createiDracPostRequest = UnityWebRequest.Post(ConstStrings.PhpAdditemsFolder + "updateidrac.php", idracForm);
     //           yield return createiDracPostRequest.SendWebRequest();

     //           if (createiDracPostRequest.result == UnityWebRequest.Result.ConnectionError)
     //           {
     //               Debug.LogWarning("conectionerror");
     //           }
     //           else if (createiDracPostRequest.result == UnityWebRequest.Result.DataProcessingError)
     //           {
     //               Debug.LogWarning("data processing error");
     //           }
     //           else if (createiDracPostRequest.result == UnityWebRequest.Result.ProtocolError)
     //           {
     //               Debug.LogWarning("protocol error");
     //           }

     //           if (createiDracPostRequest.error == null)
     //           {

     //               string response = createiDracPostRequest.downloadHandler.text;
     //               if (response == "1" || response == "2" || response == "5")
     //               {

     //               }
     //               else if (response == "3")
     //               {

     //               }
     //               else if (response == "4")
     //               {

     //               }
     //               else
     //               {

     //               }

     //           }
     //           else
     //           {

     //           }
     //           createiDracPostRequest.Dispose();
     //           break;
     //       #endregion
     //       #region Placa controladora
     //       case ConstStrings.PlacaControladora:
     //           WWWForm placaControladoraForm = CreateAddItemForm.GetPlacaControladoraForm(parameterInputs[6].text,
     //           parameterInputs[9].text, parameterInputs[10].text, parameterInputs[11].text, parameterInputs[12].text,
     //           parameterInputs[13].text, parameterInputs[14].text, parameterInputs[15].text, parameterInputs[16].text);

     //           UnityWebRequest createUpdatePlacaControladoraRequest = UnityWebRequest.Post(ConstStrings.PhpUpdateItemsFolder + "updateplacacontroladora.php", placaControladoraForm);
     //           yield return createUpdatePlacaControladoraRequest.SendWebRequest();

     //           if (createUpdatePlacaControladoraRequest.result == UnityWebRequest.Result.ConnectionError)
     //           {
     //               Debug.LogWarning("conectionerror");
     //           }
     //           else if (createUpdatePlacaControladoraRequest.result == UnityWebRequest.Result.DataProcessingError)
     //           {
     //               Debug.LogWarning("data processing error");
     //           }
     //           else if (createUpdatePlacaControladoraRequest.result == UnityWebRequest.Result.ProtocolError)
     //           {
     //               Debug.LogWarning("protocol error");
     //           }

     //           if (createUpdatePlacaControladoraRequest.error == null)
     //           {
     //               string response = createUpdatePlacaControladoraRequest.downloadHandler.text;
     //               if (response == "1" || response == "2")
     //               {
     //                   Debug.LogWarning("Server error");
     //                   // TODO send message to user with error and recomendation
     //               }
     //               else if (response == "3")
     //               {
     //                   Debug.LogWarning("Item does not exist");
     //                   // TODO send message to user with error and recomendation
     //               }
     //               else
     //               {
     //                   //TODO show success message
     //               }

     //           }
     //           else
     //           {
     //               Debug.LogWarning(createUpdatePlacaControladoraRequest.error);
     //               // TODO send message to user with error and recomendation
     //           }
     //           createUpdatePlacaControladoraRequest.Dispose();
     //           break;
     //       #endregion
     //       #region Processador
     //       case ConstStrings.Processador:
     //           WWWForm processadorForm = CreateAddItemForm.GetProcessadorForm(parameterInputs[6].text,
     //           parameterInputs[9].text, parameterInputs[10].text, parameterInputs[11].text, parameterInputs[12].text,
     //           parameterInputs[13].text, parameterInputs[14].text);

     //           UnityWebRequest createProcessadorRequest = UnityWebRequest.Post(ConstStrings.PhpUpdateItemsFolder + "updateprocesador.php", processadorForm);
     //           yield return createProcessadorRequest.SendWebRequest();

     //           if (createProcessadorRequest.result == UnityWebRequest.Result.ConnectionError)
     //           {
     //               Debug.LogWarning("conectionerror");
     //           }
     //           else if (createProcessadorRequest.result == UnityWebRequest.Result.DataProcessingError)
     //           {
     //               Debug.LogWarning("data processing error");
     //           }
     //           else if (createProcessadorRequest.result == UnityWebRequest.Result.ProtocolError)
     //           {
     //               Debug.LogWarning("protocol error");
     //           }

     //           if (createProcessadorRequest.error == null)
     //           {
     //               string response = createProcessadorRequest.downloadHandler.text;
     //               if (response == "1" || response == "2")
     //               {
     //                   Debug.LogWarning("Server error");
     //                   // TODO send message to user with error and recomendation
     //               }
     //               else if (response == "3")
     //               {
     //                   Debug.LogWarning("Item does not exist");
     //                   // TODO send message to user with error and recomendation
     //               }
     //               else
     //               {
     //                   //TODO show success message
     //               }

     //           }
     //           else
     //           {
     //               Debug.LogWarning(createProcessadorRequest.error);
     //               // TODO send message to user with error and recomendation
     //           }
     //           createProcessadorRequest.Dispose();
     //           break;
     //       #endregion
     //       #region Desktop
     //       case ConstStrings.Desktop:
     //           WWWForm desktopForm = CreateAddItemForm.GetDesktopForm(parameterInputs[1].text,
     //            parameterInputs[9].text, parameterInputs[10].text, parameterInputs[11].text, parameterInputs[12].text,
     //            parameterInputs[13].text, parameterInputs[14].text, parameterInputs[15].text, parameterInputs[16].text);

     //           UnityWebRequest createUpdatedesktopRequest = UnityWebRequest.Post(ConstStrings.PhpUpdateItemsFolder + "updatedesktop.php", desktopForm);
     //           yield return createUpdatedesktopRequest.SendWebRequest();

     //           if (createUpdatedesktopRequest.result == UnityWebRequest.Result.ConnectionError)
     //           {
     //               Debug.LogWarning("conectionerror");
     //           }
     //           else if (createUpdatedesktopRequest.result == UnityWebRequest.Result.DataProcessingError)
     //           {
     //               Debug.LogWarning("data processing error");
     //           }
     //           else if (createUpdatedesktopRequest.result == UnityWebRequest.Result.ProtocolError)
     //           {
     //               Debug.LogWarning("protocol error");
     //           }

     //           if (createUpdatedesktopRequest.error == null)
     //           {
     //               string response = createUpdatedesktopRequest.downloadHandler.text;
     //               if (response == "1" || response == "2")
     //               {
     //                   Debug.LogWarning("Server error");
     //                   // TODO send message to user with error and recomendation
     //               }
     //               else if (response == "3")
     //               {
     //                   Debug.LogWarning("Item does not exist");
     //                   // TODO send message to user with error and recomendation
     //               }
     //               else
     //               {
     //                   //TODO show success message
     //               }

     //           }
     //           else
     //           {
     //               Debug.LogWarning(createUpdatedesktopRequest.error);
     //               // TODO send message to user with error and recomendation
     //           }
     //           createUpdatedesktopRequest.Dispose();
     //           break;
     //       #endregion
     //       #region Fonte
     //       case ConstStrings.Fonte:
     //           WWWForm fonteForm = CreateAddItemForm.GetFonteForm(parameterInputs[6].text, parameterInputs[9].text,
     //           parameterInputs[10].text, parameterInputs[11].text);

     //           UnityWebRequest createUpdatefonteRequest = UnityWebRequest.Post(ConstStrings.PhpUpdateItemsFolder + "updatefonte.php", fonteForm);
     //           yield return createUpdatefonteRequest.SendWebRequest();

     //           if (createUpdatefonteRequest.result == UnityWebRequest.Result.ConnectionError)
     //           {
     //               Debug.LogWarning("conectionerror");
     //           }
     //           else if (createUpdatefonteRequest.result == UnityWebRequest.Result.DataProcessingError)
     //           {
     //               Debug.LogWarning("data processing error");
     //           }
     //           else if (createUpdatefonteRequest.result == UnityWebRequest.Result.ProtocolError)
     //           {
     //               Debug.LogWarning("protocol error");
     //           }

     //           if (createUpdatefonteRequest.error == null)
     //           {
     //               string response = createUpdatefonteRequest.downloadHandler.text;
     //               if (response == "1" || response == "2")
     //               {
     //                   Debug.LogWarning("Server error");
     //                   // TODO send message to user with error and recomendation
     //               }
     //               else if (response == "3")
     //               {
     //                   Debug.LogWarning("Item does not exist");
     //                   // TODO send message to user with error and recomendation
     //               }
     //               else
     //               {
     //                   //TODO show success message
     //               }

     //           }
     //           else
     //           {
     //               Debug.LogWarning(createUpdatefonteRequest.error);
     //               // TODO send message to user with error and recomendation
     //           }
     //           createUpdatefonteRequest.Dispose();
     //           break;
     //       #endregion
     //       #region Switch
     //       case ConstStrings.Switch:
     //           WWWForm switchForm = CreateAddItemForm.GetSwitchForm(parameterInputs[6].text, parameterInputs[9].text,
     //           parameterInputs[10].text);

     //           UnityWebRequest createUpdateSwitchRequest = UnityWebRequest.Post(ConstStrings.PhpUpdateItemsFolder + "updateswitch.php", switchForm);
     //           yield return createUpdateSwitchRequest.SendWebRequest();

     //           if (createUpdateSwitchRequest.result == UnityWebRequest.Result.ConnectionError)
     //           {
     //               Debug.LogWarning("conectionerror");
     //           }
     //           else if (createUpdateSwitchRequest.result == UnityWebRequest.Result.DataProcessingError)
     //           {
     //               Debug.LogWarning("data processing error");
     //           }
     //           else if (createUpdateSwitchRequest.result == UnityWebRequest.Result.ProtocolError)
     //           {
     //               Debug.LogWarning("protocol error");
     //           }

     //           if (createUpdateSwitchRequest.error == null)
     //           {
     //               string response = createUpdateSwitchRequest.downloadHandler.text;
     //               if (response == "1" || response == "2")
     //               {
     //                   Debug.LogWarning("Server error");
     //                   // TODO send message to user with error and recomendation
     //               }
     //               else if (response == "3")
     //               {
     //                   Debug.LogWarning("Item does not exist");
     //                   // TODO send message to user with error and recomendation
     //               }
     //               else
     //               {
     //                   //TODO show success message
     //               }

     //           }
     //           else
     //           {
     //               Debug.LogWarning(createUpdateSwitchRequest.error);
     //               // TODO send message to user with error and recomendation
     //           }
     //           createUpdateSwitchRequest.Dispose();
     //           break;
     //       #endregion
     //       #region Roteador
     //       case ConstStrings.Roteador:
     //           WWWForm roteadorForm = CreateAddItemForm.GetRoteadorForm(parameterInputs[6].text, parameterInputs[9].text,
     //            parameterInputs[10].text, parameterInputs[11].text, parameterInputs[12].text);

     //           UnityWebRequest createUpdateRoteadorRequest = UnityWebRequest.Post(ConstStrings.PhpUpdateItemsFolder + "updateroteador.php", roteadorForm);
     //           yield return createUpdateRoteadorRequest.SendWebRequest();

     //           if (createUpdateRoteadorRequest.result == UnityWebRequest.Result.ConnectionError)
     //           {
     //               Debug.LogWarning("conectionerror");
     //           }
     //           else if (createUpdateRoteadorRequest.result == UnityWebRequest.Result.DataProcessingError)
     //           {
     //               Debug.LogWarning("data processing error");
     //           }
     //           else if (createUpdateRoteadorRequest.result == UnityWebRequest.Result.ProtocolError)
     //           {
     //               Debug.LogWarning("protocol error");
     //           }

     //           if (createUpdateRoteadorRequest.error == null)
     //           {
     //               string response = createUpdateRoteadorRequest.downloadHandler.text;
     //               if (response == "1" || response == "2")
     //               {
     //                   Debug.LogWarning("Server error");
     //                   // TODO send message to user with error and recomendation
     //               }
     //               else if (response == "3")
     //               {
     //                   Debug.LogWarning("Item does not exist");
     //                   // TODO send message to user with error and recomendation
     //               }
     //               else
     //               {
     //                   //TODO show success message
     //               }

     //           }
     //           else
     //           {
     //               Debug.LogWarning(createUpdateRoteadorRequest.error);
     //               // TODO send message to user with error and recomendation
     //           }
     //           createUpdateRoteadorRequest.Dispose();
     //           break;
     //       #endregion
     //       #region Carregador
     //       case ConstStrings.Carregador:
     //           WWWForm carregadorForm = CreateAddItemForm.GetCarregadorForm(parameterInputs[6].text, parameterInputs[9].text,
     //           parameterInputs[10].text, parameterInputs[11].text);

     //           UnityWebRequest createUpdateCarregadorRequest = UnityWebRequest.Post(ConstStrings.PhpUpdateItemsFolder + "updatecarregador.php", carregadorForm);
     //           yield return createUpdateCarregadorRequest.SendWebRequest();

     //           if (createUpdateCarregadorRequest.result == UnityWebRequest.Result.ConnectionError)
     //           {
     //               Debug.LogWarning("conectionerror");
     //           }
     //           else if (createUpdateCarregadorRequest.result == UnityWebRequest.Result.DataProcessingError)
     //           {
     //               Debug.LogWarning("data processing error");
     //           }
     //           else if (createUpdateCarregadorRequest.result == UnityWebRequest.Result.ProtocolError)
     //           {
     //               Debug.LogWarning("protocol error");
     //           }

     //           if (createUpdateCarregadorRequest.error == null)
     //           {
     //               string response = createUpdateCarregadorRequest.downloadHandler.text;
     //               if (response == "1" || response == "2")
     //               {
     //                   Debug.LogWarning("Server error");
     //                   // TODO send message to user with error and recomendation
     //               }
     //               else if (response == "3")
     //               {
     //                   Debug.LogWarning("Item does not exist");
     //                   // TODO send message to user with error and recomendation
     //               }
     //               else
     //               {
     //                   //TODO show success message
     //               }

     //           }
     //           else
     //           {
     //               Debug.LogWarning(createUpdateCarregadorRequest.error);
     //               // TODO send message to user with error and recomendation
     //           }
     //           createUpdateCarregadorRequest.Dispose();
     //           break;
     //       #endregion
     //       #region Adaptador AC
     //       case ConstStrings.AdaptadorAC:
     //           WWWForm adaptadorACForm = CreateAddItemForm.GetAdaptadorACForm(parameterInputs[6].text, parameterInputs[9].text,
     //           parameterInputs[10].text, parameterInputs[11].text);

     //           UnityWebRequest createAdaptadorACRequest = UnityWebRequest.Post(ConstStrings.PhpUpdateItemsFolder + "updateadaptadorac.php", adaptadorACForm);
     //           yield return createAdaptadorACRequest.SendWebRequest();

     //           if (createAdaptadorACRequest.result == UnityWebRequest.Result.ConnectionError)
     //           {
     //               Debug.LogWarning("conectionerror");
     //           }
     //           else if (createAdaptadorACRequest.result == UnityWebRequest.Result.DataProcessingError)
     //           {
     //               Debug.LogWarning("data processing error");
     //           }
     //           else if (createAdaptadorACRequest.result == UnityWebRequest.Result.ProtocolError)
     //           {
     //               Debug.LogWarning("protocol error");
     //           }

     //           if (createAdaptadorACRequest.error == null)
     //           {
     //               string response = createAdaptadorACRequest.downloadHandler.text;
     //               if (response == "1" || response == "2")
     //               {
     //                   Debug.LogWarning("Server error");
     //                   // TODO send message to user with error and recomendation
     //               }
     //               else if (response == "3")
     //               {
     //                   Debug.LogWarning("Item does not exist");
     //                   // TODO send message to user with error and recomendation
     //               }
     //               else
     //               {
     //                   //TODO show success message
     //               }

     //           }
     //           else
     //           {
     //               Debug.LogWarning(createAdaptadorACRequest.error);
     //               // TODO send message to user with error and recomendation
     //           }
     //           createAdaptadorACRequest.Dispose();
     //           break;
     //       #endregion
     //       #region Storage NAS
     //       case ConstStrings.StorageNAS:
     //           WWWForm storageNasForm = CreateAddItemForm.GetStorageNASForm(parameterInputs[6].text, parameterInputs[9].text,
     //           parameterInputs[10].text, parameterInputs[11].text, parameterInputs[12].text, parameterInputs[13].text);

     //           UnityWebRequest createStorageNasRequest = UnityWebRequest.Post(ConstStrings.PhpUpdateItemsFolder + "updatestoragenas.php", storageNasForm);
     //           yield return createStorageNasRequest.SendWebRequest();

     //           if (createStorageNasRequest.result == UnityWebRequest.Result.ConnectionError)
     //           {
     //               Debug.LogWarning("conectionerror");
     //           }
     //           else if (createStorageNasRequest.result == UnityWebRequest.Result.DataProcessingError)
     //           {
     //               Debug.LogWarning("data processing error");
     //           }
     //           else if (createStorageNasRequest.result == UnityWebRequest.Result.ProtocolError)
     //           {
     //               Debug.LogWarning("protocol error");
     //           }

     //           if (createStorageNasRequest.error == null)
     //           {
     //               string response = createStorageNasRequest.downloadHandler.text;
     //               if (response == "1" || response == "2")
     //               {
     //                   Debug.LogWarning("Server error");
     //                   // TODO send message to user with error and recomendation
     //               }
     //               else if (response == "3")
     //               {
     //                   Debug.LogWarning("Item does not exist");
     //                   // TODO send message to user with error and recomendation
     //               }
     //               else
     //               {
     //                   //TODO show success message
     //               }

     //           }
     //           else
     //           {
     //               Debug.LogWarning(createStorageNasRequest.error);
     //               // TODO send message to user with error and recomendation
     //           }
     //           createStorageNasRequest.Dispose();
     //           break;
     //       #endregion
     //       #region GBIC
     //       case ConstStrings.Gbic:
     //           WWWForm gbicForm = CreateAddItemForm.GetGBICForm(parameterInputs[6].text, parameterInputs[7].text,
     //      parameterInputs[9].text);

     //           UnityWebRequest createGbicRequest = UnityWebRequest.Post(ConstStrings.PhpUpdateItemsFolder + "updategbic.php", gbicForm);
     //           yield return createGbicRequest.SendWebRequest();

     //           if (createGbicRequest.result == UnityWebRequest.Result.ConnectionError)
     //           {
     //               Debug.LogWarning("conectionerror");
     //           }
     //           else if (createGbicRequest.result == UnityWebRequest.Result.DataProcessingError)
     //           {
     //               Debug.LogWarning("data processing error");
     //           }
     //           else if (createGbicRequest.result == UnityWebRequest.Result.ProtocolError)
     //           {
     //               Debug.LogWarning("protocol error");
     //           }

     //           if (createGbicRequest.error == null)
     //           {
     //               string response = createGbicRequest.downloadHandler.text;
     //               if (response == "1" || response == "2")
     //               {
     //                   Debug.LogWarning("Server error");
     //                   // TODO send message to user with error and recomendation
     //               }
     //               else if (response == "3")
     //               {
     //                   Debug.LogWarning("Item does not exist");
     //                   // TODO send message to user with error and recomendation
     //               }
     //               else
     //               {
     //                   //TODO show success message
     //               }

     //           }
     //           else
     //           {
     //               Debug.LogWarning(createGbicRequest.error);
     //               // TODO send message to user with error and recomendation
     //           }
     //           createGbicRequest.Dispose();
     //           break;
     //       #endregion
     //       #region Placa de vídeo
     //       case ConstStrings.PlacaDeVideo:
     //           WWWForm placaDeVideoForm = CreateAddItemForm.GetPlacaVideoForm(parameterInputs[6].text, parameterInputs[9].text,
     //           parameterInputs[10].text);

     //           UnityWebRequest createPlacaDeVideoRequest = UnityWebRequest.Post(ConstStrings.PhpUpdateItemsFolder + "updateplacadevideo.php", placaDeVideoForm);
     //           yield return createPlacaDeVideoRequest.SendWebRequest();

     //           if (createPlacaDeVideoRequest.result == UnityWebRequest.Result.ConnectionError)
     //           {
     //               Debug.LogWarning("conectionerror");
     //           }
     //           else if (createPlacaDeVideoRequest.result == UnityWebRequest.Result.DataProcessingError)
     //           {
     //               Debug.LogWarning("data processing error");
     //           }
     //           else if (createPlacaDeVideoRequest.result == UnityWebRequest.Result.ProtocolError)
     //           {
     //               Debug.LogWarning("protocol error");
     //           }

     //           if (createPlacaDeVideoRequest.error == null)
     //           {
     //               string response = createPlacaDeVideoRequest.downloadHandler.text;
     //               if (response == "1" || response == "2")
     //               {
     //                   Debug.LogWarning("Server error");
     //                   // TODO send message to user with error and recomendation
     //               }
     //               else if (response == "3")
     //               {
     //                   Debug.LogWarning("Item does not exist");
     //                   // TODO send message to user with error and recomendation
     //               }
     //               else
     //               {
     //                   //TODO show success message
     //               }

     //           }
     //           else
     //           {
     //               Debug.LogWarning(createPlacaDeVideoRequest.error);
     //               // TODO send message to user with error and recomendation
     //           }
     //           createPlacaDeVideoRequest.Dispose();
     //           break;
     //       #endregion
     //       #region Placa de som
     //       case ConstStrings.PlacaDeSom:
     //           WWWForm placaDeSomForm = CreateAddItemForm.GetPlacaSomForm(parameterInputs[6].text, parameterInputs[9].text);

     //           UnityWebRequest createPlacaDeSomRequest = UnityWebRequest.Post(ConstStrings.PhpUpdateItemsFolder + "updateplacadesom.php", placaDeSomForm);
     //           yield return createPlacaDeSomRequest.SendWebRequest();

     //           if (createPlacaDeSomRequest.result == UnityWebRequest.Result.ConnectionError)
     //           {
     //               Debug.LogWarning("conectionerror");
     //           }
     //           else if (createPlacaDeSomRequest.result == UnityWebRequest.Result.DataProcessingError)
     //           {
     //               Debug.LogWarning("data processing error");
     //           }
     //           else if (createPlacaDeSomRequest.result == UnityWebRequest.Result.ProtocolError)
     //           {
     //               Debug.LogWarning("protocol error");
     //           }

     //           if (createPlacaDeSomRequest.error == null)
     //           {
     //               string response = createPlacaDeSomRequest.downloadHandler.text;
     //               if (response == "1" || response == "2")
     //               {
     //                   Debug.LogWarning("Server error");
     //                   // TODO send message to user with error and recomendation
     //               }
     //               else if (response == "3")
     //               {
     //                   Debug.LogWarning("Item does not exist");
     //                   // TODO send message to user with error and recomendation
     //               }
     //               else
     //               {
     //                   //TODO show success message
     //               }

     //           }
     //           else
     //           {
     //               Debug.LogWarning(createPlacaDeSomRequest.error);
     //               // TODO send message to user with error and recomendation
     //           }
     //           createPlacaDeSomRequest.Dispose();
     //           break;
     //       #endregion
     //       #region Placa de captura de vídeo
     //       case ConstStrings.PlacaDeCapturaDeVideo:
     //           WWWForm placaDeCapturaDeVideoForm = CreateAddItemForm.GetPlacaCapturaVideoForm(parameterInputs[6].text, 
     //           parameterInputs[9].text);

     //           UnityWebRequest createPlacaDeCapturaDeVideoRequest = UnityWebRequest.Post(ConstStrings.PhpUpdateItemsFolder + "updateplacadecapturadevideo.php", placaDeCapturaDeVideoForm);
     //           yield return createPlacaDeCapturaDeVideoRequest.SendWebRequest();

     //           if (createPlacaDeCapturaDeVideoRequest.result == UnityWebRequest.Result.ConnectionError)
     //           {
     //               Debug.LogWarning("conectionerror");
     //           }
     //           else if (createPlacaDeCapturaDeVideoRequest.result == UnityWebRequest.Result.DataProcessingError)
     //           {
     //               Debug.LogWarning("data processing error");
     //           }
     //           else if (createPlacaDeCapturaDeVideoRequest.result == UnityWebRequest.Result.ProtocolError)
     //           {
     //               Debug.LogWarning("protocol error");
     //           }

     //           if (createPlacaDeCapturaDeVideoRequest.error == null)
     //           {
     //               string response = createPlacaDeCapturaDeVideoRequest.downloadHandler.text;
     //               if (response == "1" || response == "2")
     //               {
     //                   Debug.LogWarning("Server error");
     //                   // TODO send message to user with error and recomendation
     //               }
     //               else if (response == "3")
     //               {
     //                   Debug.LogWarning("Item does not exist");
     //                   // TODO send message to user with error and recomendation
     //               }
     //               else
     //               {
     //                   //TODO show success message
     //               }

     //           }
     //           else
     //           {
     //               Debug.LogWarning(createPlacaDeCapturaDeVideoRequest.error);
     //               // TODO send message to user with error and recomendation
     //           }
     //           createPlacaDeCapturaDeVideoRequest.Dispose();
     //           break;
     //       #endregion
     //       #region Servidor
     //       case ConstStrings.Servidor:
     //           WWWForm servidorForm = CreateAddItemForm.GetServidorForm(parameterInputs[6].text,
     //parameterInputs[7].text);

     //           UnityWebRequest createServidorRequest = UnityWebRequest.Post(ConstStrings.PhpUpdateItemsFolder + "updateservidor.php", servidorForm);
     //           yield return createServidorRequest.SendWebRequest();

     //           if (createServidorRequest.result == UnityWebRequest.Result.ConnectionError)
     //           {
     //               Debug.LogWarning("conectionerror");
     //           }
     //           else if (createServidorRequest.result == UnityWebRequest.Result.DataProcessingError)
     //           {
     //               Debug.LogWarning("data processing error");
     //           }
     //           else if (createServidorRequest.result == UnityWebRequest.Result.ProtocolError)
     //           {
     //               Debug.LogWarning("protocol error");
     //           }

     //           if (createServidorRequest.error == null)
     //           {
     //               string response = createServidorRequest.downloadHandler.text;
     //               if (response == "1" || response == "2")
     //               {
     //                   Debug.LogWarning("Server error");
     //                   // TODO send message to user with error and recomendation
     //               }
     //               else if (response == "3")
     //               {
     //                   Debug.LogWarning("Item does not exist");
     //                   // TODO send message to user with error and recomendation
     //               }
     //               else
     //               {
     //                   //TODO show success message
     //               }

     //           }
     //           else
     //           {
     //               Debug.LogWarning(createServidorRequest.error);
     //               // TODO send message to user with error and recomendation
     //           }
     //           createServidorRequest.Dispose();
     //           break;
     //       #endregion
     //       #region Notebook
     //       case ConstStrings.Notebook:
     //           WWWForm notebookForm = CreateAddItemForm.GetNotebookForm(parameterInputs[6].text,
     //              parameterInputs[7].text);

     //           UnityWebRequest createNotebookRequest = UnityWebRequest.Post(ConstStrings.PhpUpdateItemsFolder + "updatenotebook.php", notebookForm);
     //           yield return createNotebookRequest.SendWebRequest();

     //           if (createNotebookRequest.result == UnityWebRequest.Result.ConnectionError)
     //           {
     //               Debug.LogWarning("conectionerror");
     //           }
     //           else if (createNotebookRequest.result == UnityWebRequest.Result.DataProcessingError)
     //           {
     //               Debug.LogWarning("data processing error");
     //           }
     //           else if (createNotebookRequest.result == UnityWebRequest.Result.ProtocolError)
     //           {
     //               Debug.LogWarning("protocol error");
     //           }

     //           if (createNotebookRequest.error == null)
     //           {
     //               string response = createNotebookRequest.downloadHandler.text;
     //               if (response == "1" || response == "2")
     //               {
     //                   Debug.LogWarning("Server error");
     //                   // TODO send message to user with error and recomendation
     //               }
     //               else if (response == "3")
     //               {
     //                   Debug.LogWarning("Item does not exist");
     //                   // TODO send message to user with error and recomendation
     //               }
     //               else
     //               {
     //                   //TODO show success message
     //               }

     //           }
     //           else
     //           {
     //               Debug.LogWarning(createNotebookRequest.error);
     //               // TODO send message to user with error and recomendation
     //           }
     //           createNotebookRequest.Dispose();
     //           break;
     //       #endregion
     //       #region Monitor
     //       case ConstStrings.Monitor:
     //           WWWForm monitorForm = CreateAddItemForm.GetMonitorForm(parameterInputs[6].text,
     //             parameterInputs[7].text, parameterInputs[9].text, parameterInputs[10].text);

     //           UnityWebRequest createMonitorRequest = UnityWebRequest.Post(ConstStrings.PhpUpdateItemsFolder + "updatemonitor.php", monitorForm);
     //           yield return createMonitorRequest.SendWebRequest();

     //           if (createMonitorRequest.result == UnityWebRequest.Result.ConnectionError)
     //           {
     //               Debug.LogWarning("conectionerror");
     //           }
     //           else if (createMonitorRequest.result == UnityWebRequest.Result.DataProcessingError)
     //           {
     //               Debug.LogWarning("data processing error");
     //           }
     //           else if (createMonitorRequest.result == UnityWebRequest.Result.ProtocolError)
     //           {
     //               Debug.LogWarning("protocol error");
     //           }

     //           if (createMonitorRequest.error == null)
     //           {
     //               string response = createMonitorRequest.downloadHandler.text;
     //               if (response == "1" || response == "2")
     //               {
     //                   Debug.LogWarning("Server error");
     //                   // TODO send message to user with error and recomendation
     //               }
     //               else if (response == "3")
     //               {
     //                   Debug.LogWarning("Item does not exist");
     //                   // TODO send message to user with error and recomendation
     //               }
     //               else
     //               {
     //                   //TODO show success message
     //               }

     //           }
     //           else
     //           {
     //               Debug.LogWarning(createMonitorRequest.error);
     //               // TODO send message to user with error and recomendation
     //           }
     //           createMonitorRequest.Dispose();
     //           break;
     //       #endregion

     //       default:
     //           break;
     //   }
        MouseManager.Instance.SetDefaultCursor();
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
        inputsPanel.SetActive(true);
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
        StopAllCoroutines();
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
