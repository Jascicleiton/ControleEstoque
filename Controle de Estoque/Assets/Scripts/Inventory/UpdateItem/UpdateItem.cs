using SimpleJSON;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.SceneManagement;

public class UpdateItem : MonoBehaviour
{
    [SerializeField] private TMP_InputField itemToUpdateParameter;
    [SerializeField] private TMP_Dropdown parameterToSearchDP;

    [SerializeField] TMP_InputField[] inputs;
    [SerializeField] TMP_Text[] placeholders;

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
        ShowHideInputs();
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
                CheckIfItemExists();
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
    /// Updates all the inputs according to the category selected
    /// </summary>
    private void ShowHideInputs()
    {
        foreach (var item in inputs)
        {
            item.gameObject.SetActive(true);
        }

        switch (inputs.Length)
        {
            case 0:
                placeholders[0].text = "Interface";
                placeholders[1].text = "Tamanho";
                placeholders[2].text = "Forma de armazenamento";
                placeholders[3].text = "Capacidade (GB)";
                placeholders[4].text = "RPM";
                placeholders[5].text = "Velocidade de Leitura (Gb/s)";
                placeholders[6].text = "Enterprise";
                placeholders[7].text = "";
                break;
            case 1:
                placeholders[0].text = "Tipo";
                placeholders[1].text = "Capacidade (GB)";
                placeholders[2].text = "Velocidade (MHz)";
                placeholders[3].text = "É low voltage?";
                placeholders[4].text = "Rank";
                placeholders[5].text = "DIMM";
                placeholders[6].text = "Taxa de transmissão";
                placeholders[7].text = "Símbolo";
                break;
            case 2:
                placeholders[0].text = "Interface";
                placeholders[1].text = "Quantas portas?";
                placeholders[2].text = "Quais portas?";
                placeholders[3].text = "Suporta fibra óptica?";
                placeholders[4].text = "Desempenho (MB/s)";
                placeholders[5].text = "";
                placeholders[6].text = "";
                placeholders[7].text = "";
                break;
            case 3:
                placeholders[0].text = "Porta";
                placeholders[1].text = "Velocidade (GB/s)";
                placeholders[2].text = "Entrada SD";
                placeholders[3].text = "Servidores suportados";
                placeholders[4].text = "";
                placeholders[5].text = "";
                placeholders[6].text = "";
                placeholders[7].text = "";
                break;
            case 4:
                placeholders[0].text = "Tipo de conexão";
                placeholders[1].text = "Quantas portas?";
                placeholders[2].text = "Tipos de RAID";
                placeholders[3].text = "Capacidade máx do HD (TB)";
                placeholders[4].text = "Até quantos HDs";
                placeholders[5].text = "Bateria inclusa?";
                placeholders[6].text = "Barramento";
                placeholders[7].text = "";
                break;
            case 5:
                placeholders[0].text = "Soquete";
                placeholders[1].text = "Nº núcleos físicos";
                placeholders[2].text = "Nº núcleos lógicos";
                placeholders[3].text = "Aceita virtualização?";
                placeholders[4].text = "Turbo boost?";
                placeholders[5].text = "Hyper-Threading?";
                placeholders[6].text = "";
                placeholders[7].text = "";
                break;
            case 6:
                placeholders[0].text = "Modelo de placa mãe";
                placeholders[1].text = "Fonte?";
                placeholders[2].text = "Memória?";
                placeholders[3].text = "HD?";
                placeholders[4].text = "Placa de vídeo?";
                placeholders[5].text = "Leitor de DVD?";
                placeholders[6].text = "";
                placeholders[7].text = "";
                break;
            case 7:
                placeholders[0].text = "Watts de potência";
                placeholders[1].text = "Onde funciona?";
                placeholders[2].text = "Conectores";
                placeholders[3].text = "";
                placeholders[4].text = "";
                placeholders[5].text = "";
                placeholders[6].text = "";
                placeholders[7].text = "";
                break;
            case 8:
                placeholders[0].text = "Quantas entradas";
                placeholders[1].text = "Capacidade máx de cada porta (MB/s)";
                placeholders[2].text = "";
                placeholders[3].text = "";
                placeholders[4].text = "";
                placeholders[5].text = "";
                placeholders[6].text = "";
                placeholders[7].text = "";
                break;
            case 9:
                placeholders[0].text = "Wireless?";
                placeholders[1].text = "Quantas entradas?";
                placeholders[2].text = "Banda máx (MB/s)";
                placeholders[3].text = "";
                placeholders[4].text = "";
                placeholders[5].text = "";
                placeholders[6].text = "";
                placeholders[7].text = "";
                break;
            case 10:
                placeholders[0].text = "Onde funciona?";
                placeholders[1].text = "Voltagem de saída";
                placeholders[2].text = "Amperagem de saída (mA)";
                placeholders[3].text = "";
                placeholders[4].text = "";
                placeholders[5].text = "";
                placeholders[6].text = "";
                placeholders[7].text = "";
                break;
            case 11:
                placeholders[0].text = "Onde funciona?";
                placeholders[1].text = "Voltagem de saída";
                placeholders[2].text = "Amperagem de saída (A)";
                placeholders[3].text = "";
                placeholders[4].text = "";
                placeholders[5].text = "";
                placeholders[6].text = "";
                placeholders[7].text = "";
                break;
            case 12:
                placeholders[0].text = "Tamanho dos HDs";
                placeholders[1].text = "Tipos de RAID";
                placeholders[2].text = "Tipo de HD";
                placeholders[3].text = "Capacidade máx do HD";
                placeholders[4].text = "Até quantos HDs";
                placeholders[5].text = "";
                placeholders[6].text = "";
                placeholders[7].text = "";
                break;
            case 13:
                placeholders[0].text = "Desempenho máx (GB/s)";
                placeholders[1].text = "";
                placeholders[2].text = "";
                placeholders[3].text = "";
                placeholders[4].text = "";
                placeholders[5].text = "";
                placeholders[6].text = "";
                placeholders[7].text = "";
                break;
            case 14:
                placeholders[0].text = "Quantas entradas?";
                placeholders[1].text = "Quais entradas?";
                placeholders[2].text = "";
                placeholders[3].text = "";
                placeholders[4].text = "";
                placeholders[5].text = "";
                placeholders[6].text = "";
                placeholders[7].text = "";
                break;
            case 15:
                placeholders[0].text = "Quantos canais?";
                placeholders[1].text = "";
                placeholders[2].text = "";
                placeholders[3].text = "";
                placeholders[4].text = "";
                placeholders[5].text = "";
                placeholders[6].text = "";
                placeholders[7].text = "";
                break;
            case 16:
                placeholders[0].text = "Quantas entradas?";
                placeholders[1].text = "";
                placeholders[2].text = "";
                placeholders[3].text = "";
                placeholders[4].text = "";
                placeholders[5].text = "";
                placeholders[6].text = "";
                placeholders[7].text = "";
                break;

            default:
                break;
        }

        for (int i = 0; i < placeholders.Length; i++)
        {
            if (placeholders[i] != null && placeholders[i].text == "")
            {
                inputs[i + 9].gameObject.SetActive(false);
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
            itemForm = CreateForm.GetConsultPatrimonioForm(itemToUpdateParameter.text);
            createItemUpdatePostRequest = UnityWebRequest.Post(ConstStrings.PhpUpdateItemsFolder + "getitempatrimoniotoupdate.php", itemForm);
        }
        if (parameterToSearchDP.value == 1)
        {
            itemForm = CreateForm.GetConsultPatrimonioForm(itemToUpdateParameter.text);
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
        inputs[0].text = itemToUpdate.Entrada;
        inputs[1].text = itemToUpdate.Patrimonio;
        inputs[2].text = itemToUpdate.Status;
        inputs[3].text = itemToUpdate.Serial;
        inputs[4].text = itemToUpdate.Fabricante;
        inputs[5].text = itemToUpdate.Modelo;
        inputs[6].text = itemToUpdate.Local;
        inputs[7].text = itemToUpdate.Saida;
        inputs[17].text = itemToUpdate.Categoria;
        switch (itemToUpdate.Categoria)
        {
            case ConstStrings.HD:
                inputs[9].text = itemToUpdate.Interface;
                inputs[10].text = itemToUpdate.Tamanho;
                inputs[11].text = itemToUpdate.FormaDeArmazenamento;
                inputs[12].text = itemToUpdate.CapacidadeEmGB;
                inputs[13].text = itemToUpdate.RPM;
                inputs[14].text = itemToUpdate.VelocidadeDeLeitura;
                inputs[15].text = itemToUpdate.Enterprise;
                break;
            case ConstStrings.Memoria:
                inputs[9].text = itemToUpdate.Tipo;
                inputs[10].text = itemToUpdate.CapacidadeEmGB;
                inputs[11].text = itemToUpdate.VelocidadeMHz;
                inputs[12].text = itemToUpdate.LowVoltage;
                inputs[13].text = itemToUpdate.Rank;
                inputs[14].text = itemToUpdate.DIMM;
                inputs[15].text = itemToUpdate.TaxaDeTransmissao;
                inputs[16].text = itemToUpdate.Simbolo;
                break;
            case ConstStrings.PlacaDeRede:
                inputs[9].text = itemToUpdate.Interface;
                inputs[10].text = itemToUpdate.QuantidadeDePortas;
                inputs[11].text = itemToUpdate.QuaisConexoes;
                inputs[12].text = itemToUpdate.SuportaFibraOptica;
                inputs[13].text = itemToUpdate.Desempenho;
                break;
            case ConstStrings.Idrac:
                inputs[9].text = itemToUpdate.QuaisConexoes;
                inputs[10].text = itemToUpdate.VelocidadeGBs;
                inputs[11].text = itemToUpdate.EntradaSD;
                inputs[12].text = itemToUpdate.ServidoresSuportados;
                break;
            case ConstStrings.PlacaControladora:
                inputs[9].text = itemToUpdate.QuaisConexoes;
                inputs[10].text = itemToUpdate.QuantidadeDePortas;
                inputs[11].text = itemToUpdate.TipoDeRAID;
                inputs[12].text = itemToUpdate.CapacidadeMaxHD;
                inputs[13].text = itemToUpdate.AteQuantosHDs;
                inputs[14].text = itemToUpdate.BateriaInclusa;
                inputs[15].text = itemToUpdate.Barramento;
                break;
            case ConstStrings.Processador:
                inputs[9].text = itemToUpdate.Soquete;
                inputs[10].text = itemToUpdate.NucleosFisicos;
                inputs[11].text = itemToUpdate.NucleosLogicos;
                inputs[12].text = itemToUpdate.AceitaVirtualizacao;
                inputs[13].text = itemToUpdate.TurboBoost;
                inputs[14].text = itemToUpdate.HyperThreading;
                break;
            case ConstStrings.Desktop:
                inputs[9].text = itemToUpdate.ModeloPlacaMae;
                inputs[10].text = itemToUpdate.Fonte;
                inputs[11].text = itemToUpdate.Memoria;
                inputs[12].text = itemToUpdate.HD;
                inputs[13].text = itemToUpdate.PlacaDeVideo;
                inputs[14].text = itemToUpdate.LeitorDeDVD;
                break;
            case ConstStrings.Fonte:
                inputs[9].text = itemToUpdate.Watts;
                inputs[10].text = itemToUpdate.OndeFunciona;
                inputs[11].text = itemToUpdate.Conectores;
                break;
            case ConstStrings.Switch:
                inputs[9].text = itemToUpdate.QuantidadeDePortas;
                inputs[10].text = itemToUpdate.Desempenho;
                break;
            case ConstStrings.Roteador:
                inputs[9].text = itemToUpdate.Wireless;
                inputs[10].text = itemToUpdate.QuantidadeDePortas;
                inputs[11].text = itemToUpdate.BandaMaxima;
                break;
            case ConstStrings.Carregador:
                inputs[9].text = itemToUpdate.OndeFunciona;
                inputs[10].text = itemToUpdate.VoltagemDeSaida;
                inputs[11].text = itemToUpdate.AmperagemDeSaida;
                break;
            case ConstStrings.AdaptadorAC:
                inputs[9].text = itemToUpdate.OndeFunciona;
                inputs[10].text = itemToUpdate.VoltagemDeSaida;
                inputs[11].text = itemToUpdate.AmperagemDeSaida;
                break;
            case ConstStrings.StorageNAS:
                inputs[9].text = itemToUpdate.Tamanho;
                inputs[10].text = itemToUpdate.TipoDeRAID;
                inputs[11].text = itemToUpdate.TipoDeHD;
                inputs[12].text = itemToUpdate.CapacidadeMaxHD;
                break;
            case ConstStrings.Gbic:
                inputs[9].text = itemToUpdate.Desempenho;
                break;
            case ConstStrings.PlacaDeVideo:
                inputs[9].text = itemToUpdate.QuantidadeDePortas;
                inputs[10].text = itemToUpdate.QuaisConexoes;
                break;
            case ConstStrings.PlacaDeSom:
                inputs[9].text = itemToUpdate.QuantosCanais;
                break;
            case ConstStrings.PlacaDeCapturaDeVideo:
                inputs[9].text = itemToUpdate.QuantidadeDePortas;
                break;

            default:
                break;
        }

    }

    private IEnumerator UpdateDatabaseRoutine()
    {
        #region Update inventario
        WWWForm itemForm = CreateForm.GetInventarioForm(inputs[0].text, inputs[1].text, inputs[2].text, 
        inputs[3].text, inputs[4].text, inputs[5].text, inputs[6].text, inputs[7].text, inputs[8].text, 
        inputs[9].text);
       
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

                WWWForm hdForm = CreateForm.GetHDForm(inputs[6].text, inputs[11].text, inputs[12].text,
                inputs[13].text, inputs[14].text, inputs[15].text, inputs[16].text, inputs[17].text, inputs[18].text);

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
                    Debug.LogWarning(createUpdateHDRequest.error);
                    // TODO send message to user with error and recomendation
                }
                createUpdateHDRequest.Dispose();
                break;
            #endregion
            case ConstStrings.Memoria:
                itemToUpdate.Tipo = inputs[9].text;
                itemToUpdate.CapacidadeEmGB = inputs[10].text;
                itemToUpdate.VelocidadeMHz = inputs[11].text;
                itemToUpdate.LowVoltage = inputs[12].text;
                itemToUpdate.Rank = inputs[13].text;
                itemToUpdate.DIMM = inputs[14].text;
                itemToUpdate.TaxaDeTransmissao = inputs[15].text;
                itemToUpdate.Simbolo = inputs[16].text;
                break;
            case ConstStrings.PlacaDeRede:
                itemToUpdate.Interface = inputs[9].text;
                itemToUpdate.QuantidadeDePortas = inputs[10].text;
                itemToUpdate.QuaisConexoes = inputs[11].text;
                itemToUpdate.SuportaFibraOptica = inputs[12].text;
                itemToUpdate.Desempenho = inputs[13].text;
                break;
            case ConstStrings.Idrac:
                itemToUpdate.QuaisConexoes = inputs[9].text;
                itemToUpdate.VelocidadeGBs = inputs[10].text;
                itemToUpdate.EntradaSD = inputs[11].text;
                itemToUpdate.ServidoresSuportados = inputs[12].text;
                break;
            case ConstStrings.PlacaControladora:
                itemToUpdate.QuaisConexoes = inputs[9].text;
                itemToUpdate.QuantidadeDePortas = inputs[10].text;
                itemToUpdate.TipoDeRAID = inputs[11].text;
                itemToUpdate.CapacidadeMaxHD = inputs[12].text;
                itemToUpdate.AteQuantosHDs = inputs[13].text;
                itemToUpdate.BateriaInclusa = inputs[14].text;
                itemToUpdate.Barramento = inputs[15].text;
                break;
            case ConstStrings.Processador:
                itemToUpdate.Soquete = inputs[9].text;
                itemToUpdate.NucleosFisicos = inputs[10].text;
                itemToUpdate.NucleosLogicos = inputs[11].text;
                itemToUpdate.AceitaVirtualizacao = inputs[12].text;
                itemToUpdate.TurboBoost = inputs[13].text;
                itemToUpdate.HyperThreading = inputs[14].text;
                break;
            case ConstStrings.Desktop:
                itemToUpdate.ModeloPlacaMae = inputs[9].text;
                itemToUpdate.Fonte = inputs[10].text;
                itemToUpdate.Memoria = inputs[11].text;
                itemToUpdate.HD = inputs[12].text;
                itemToUpdate.PlacaDeVideo = inputs[13].text;
                itemToUpdate.LeitorDeDVD = inputs[14].text;
                break;
            case ConstStrings.Fonte:
                itemToUpdate.Watts = inputs[9].text;
                itemToUpdate.OndeFunciona = inputs[10].text;
                itemToUpdate.Conectores = inputs[11].text;
                break;
            case ConstStrings.Switch:
                itemToUpdate.QuantidadeDePortas = inputs[9].text;
                itemToUpdate.Desempenho = inputs[10].text;
                break;
            case ConstStrings.Roteador:
                itemToUpdate.Wireless = inputs[9].text;
                itemToUpdate.QuantidadeDePortas = inputs[10].text;
                itemToUpdate.BandaMaxima = inputs[11].text;
                break;
            case ConstStrings.Carregador:
                itemToUpdate.OndeFunciona = inputs[9].text;
                itemToUpdate.VoltagemDeSaida = inputs[10].text;
                itemToUpdate.AmperagemDeSaida = inputs[11].text;
                break;
            case ConstStrings.AdaptadorAC:
                itemToUpdate.OndeFunciona = inputs[9].text;
                itemToUpdate.VoltagemDeSaida = inputs[10].text;
                itemToUpdate.AmperagemDeSaida = inputs[11].text;
                break;
            case ConstStrings.StorageNAS:
                itemToUpdate.Tamanho = inputs[9].text;
                itemToUpdate.TipoDeRAID = inputs[10].text;
                itemToUpdate.TipoDeHD = inputs[11].text;
                itemToUpdate.CapacidadeMaxHD = inputs[12].text;
                break;
            case ConstStrings.Gbic:
                itemToUpdate.Desempenho = inputs[9].text;
                break;
            case ConstStrings.PlacaDeVideo:
                itemToUpdate.QuantidadeDePortas = inputs[9].text;
                itemToUpdate.QuaisConexoes = inputs[10].text;
                break;
            case ConstStrings.PlacaDeSom:
                itemToUpdate.QuantosCanais = inputs[9].text;
                break;
            case ConstStrings.PlacaDeCapturaDeVideo:
                itemToUpdate.QuantidadeDePortas = inputs[9].text;
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
        itemToUpdate.Entrada = inputs[0].text;
        itemToUpdate.Patrimonio = inputs[1].text;
        itemToUpdate.Status = inputs[2].text;
        itemToUpdate.Serial = inputs[3].text;
        itemToUpdate.Fabricante = inputs[4].text;
        itemToUpdate.Modelo = inputs[5].text;
        itemToUpdate.Local = inputs[6].text;
        itemToUpdate.Saida = inputs[7].text;
        itemToUpdate.Saida = inputs[17].text;
        switch (itemToUpdate.Categoria)
        {
            case ConstStrings.HD:
                
                itemToUpdate.Interface = inputs[9].text;
                itemToUpdate.Tamanho = inputs[10].text;
                itemToUpdate.FormaDeArmazenamento = inputs[11].text;
                itemToUpdate.CapacidadeEmGB = inputs[12].text;
                itemToUpdate.RPM = inputs[13].text;
                itemToUpdate.VelocidadeDeLeitura = inputs[14].text;
                itemToUpdate.Enterprise = inputs[15].text;
                break;
            case ConstStrings.Memoria:
                itemToUpdate.Tipo = inputs[9].text;
                itemToUpdate.CapacidadeEmGB = inputs[10].text;
                itemToUpdate.VelocidadeMHz = inputs[11].text;
                itemToUpdate.LowVoltage = inputs[12].text;
                itemToUpdate.Rank = inputs[13].text;
                itemToUpdate.DIMM = inputs[14].text;
                itemToUpdate.TaxaDeTransmissao = inputs[15].text;
                itemToUpdate.Simbolo = inputs[16].text;
                break;
            case ConstStrings.PlacaDeRede:
                itemToUpdate.Interface = inputs[9].text;
                itemToUpdate.QuantidadeDePortas = inputs[10].text;
                itemToUpdate.QuaisConexoes = inputs[11].text;
                itemToUpdate.SuportaFibraOptica = inputs[12].text;
                itemToUpdate.Desempenho = inputs[13].text;
                break;
            case ConstStrings.Idrac:
                itemToUpdate.QuaisConexoes = inputs[9].text;
                itemToUpdate.VelocidadeGBs = inputs[10].text;
                itemToUpdate.EntradaSD = inputs[11].text;
                itemToUpdate.ServidoresSuportados = inputs[12].text;
                break;
            case ConstStrings.PlacaControladora:
                itemToUpdate.QuaisConexoes = inputs[9].text;
                itemToUpdate.QuantidadeDePortas = inputs[10].text;
                itemToUpdate.TipoDeRAID = inputs[11].text;
                itemToUpdate.CapacidadeMaxHD = inputs[12].text;
                itemToUpdate.AteQuantosHDs = inputs[13].text;
                itemToUpdate.BateriaInclusa = inputs[14].text;
                itemToUpdate.Barramento = inputs[15].text;
                break;
            case ConstStrings.Processador:
                itemToUpdate.Soquete = inputs[9].text;
                itemToUpdate.NucleosFisicos = inputs[10].text;
                itemToUpdate.NucleosLogicos = inputs[11].text;
                itemToUpdate.AceitaVirtualizacao = inputs[12].text;
                itemToUpdate.TurboBoost = inputs[13].text;
                itemToUpdate.HyperThreading = inputs[14].text;
                break;
            case ConstStrings.Desktop:
                itemToUpdate.ModeloPlacaMae = inputs[9].text;
                itemToUpdate.Fonte = inputs[10].text;
                itemToUpdate.Memoria = inputs[11].text;
                itemToUpdate.HD = inputs[12].text;
                itemToUpdate.PlacaDeVideo = inputs[13].text;
                itemToUpdate.LeitorDeDVD = inputs[14].text;
                break;
            case ConstStrings.Fonte:
                itemToUpdate.Watts = inputs[9].text;
                itemToUpdate.OndeFunciona = inputs[10].text;
                itemToUpdate.Conectores = inputs[11].text;
                break;
            case ConstStrings.Switch:
                itemToUpdate.QuantidadeDePortas = inputs[9].text;
                itemToUpdate.Desempenho = inputs[10].text;
                break;
            case ConstStrings.Roteador:
                itemToUpdate.Wireless = inputs[9].text;
                itemToUpdate.QuantidadeDePortas = inputs[10].text;
                itemToUpdate.BandaMaxima = inputs[11].text;
                break;
            case ConstStrings.Carregador:
                itemToUpdate.OndeFunciona = inputs[9].text;
                itemToUpdate.VoltagemDeSaida = inputs[10].text;
                itemToUpdate.AmperagemDeSaida = inputs[11].text;
                break;
            case ConstStrings.AdaptadorAC:
                itemToUpdate.OndeFunciona = inputs[9].text;
                itemToUpdate.VoltagemDeSaida = inputs[10].text;
                itemToUpdate.AmperagemDeSaida = inputs[11].text;
                break;
            case ConstStrings.StorageNAS:
                itemToUpdate.Tamanho = inputs[9].text;
                itemToUpdate.TipoDeRAID = inputs[10].text;
                itemToUpdate.TipoDeHD = inputs[11].text;
                itemToUpdate.CapacidadeMaxHD = inputs[12].text;
                break;
            case ConstStrings.Gbic:
                itemToUpdate.Desempenho = inputs[9].text;
                break;
            case ConstStrings.PlacaDeVideo:
                itemToUpdate.QuantidadeDePortas = inputs[9].text;
                itemToUpdate.QuaisConexoes = inputs[10].text;
                break;
            case ConstStrings.PlacaDeSom:
                itemToUpdate.QuantosCanais = inputs[9].text;
                break;
            case ConstStrings.PlacaDeCapturaDeVideo:
                itemToUpdate.QuantidadeDePortas = inputs[9].text;
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
        foreach (var item in inputs)
        {
            item.gameObject.SetActive(true);
            item.text = "";
        }
    }

    /// <summary>
    /// Close  message panel
    /// </summary>
    public void CloseMessagePanel()
    {
        messagePanel.SetActive(false);
        StopCoroutine(CloseShowMessagePanelRoutine());
        ResetInputs();
        ShowHideInputs();
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
