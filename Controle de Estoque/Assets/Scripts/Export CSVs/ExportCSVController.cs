using Mono.Cecil;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

[RequireComponent(typeof(ExportCSVs))]
public class ExportCSVController : MonoBehaviour
{
    private ExportCSVs exportCSVs = null;
    private int csvToExportIndex;
    [SerializeField] private TMP_Text messageText;

    private void Start()
    {
        exportCSVs = GetComponent<ExportCSVs>();
        csvToExportIndex = 0;
    }

    public void ExportSelectedCSV()
    {
        switch (csvToExportIndex)
        {
            case 0:
                exportCSVs.CreateInventarioSheet();
                ShowMessage("Inventário");
                break;
            case 1:
                exportCSVs.CreateMovimentacaoSheet();
                ShowMessage("Movementação");
                break;
            case 2:
                exportCSVs.CreateHDDetailsSheet();
                ShowMessage("HD");
                break;
            case 3:
                exportCSVs.CreateMemoriaDetailsSheet();
                ShowMessage("Memória");
                break;
            case 4:
                exportCSVs.CreatePlacaDeRedeDetailsSheet();
                ShowMessage("Placa de rede");
                break;
            case 5:
                exportCSVs.CreateiDracDetailsSheet();
                ShowMessage("iDrac");
                break;
            case 6:
                exportCSVs.CreatePlacaControladoraDetailsSheet();
                ShowMessage("Placa Controladora");
                break;
            case 7:
                exportCSVs.CreateProcessadorDetailsSheet();
                ShowMessage("Processador");
                break;
            case 8:
                exportCSVs.CreateDesktopDetailsSheet();
                ShowMessage("Desktop");
                break;
            case 9:
                exportCSVs.CreateFonteDetailsSheet();
                ShowMessage("Fonte");
                break;
            case 10:
                exportCSVs.CreateSwitchDetailsSheet();
                ShowMessage("Switch");
                break;
            case 11:
                exportCSVs.CreateRoteadorDetailsSheet();
                ShowMessage("Roteador");
                break;
            case 12:
                exportCSVs.CreateCarregadorDetailsSheet();
                ShowMessage("Carregador");
                break;
            case 13:
                exportCSVs.CreateAdaptadorACDetailsSheet();
                ShowMessage("Adaptador AC");
                break;
            case 14:
                exportCSVs.CreateStorageNASDetailsSheet();
                ShowMessage("Storage NAS");
                break;
            case 15:
                exportCSVs.CreateGbicDetailsSheet();
                ShowMessage("GBIC");
                break;
            case 16:
                exportCSVs.CreatePlacaDeVideoDetailsSheet();
                ShowMessage("Placa de vídeo");
                break;
            case 17:
                exportCSVs.CreatePlacaDeSomDetailsSheet();
                ShowMessage("Placa de som");
                break;
            case 18:
                exportCSVs.CreatePlacaDeCapturaDeVideoDetailsSheet();
                ShowMessage("Placa de captura de vídeo");
                break;
            case 19:
                exportCSVs.CreateServidorDetailsSheet();
                ShowMessage("Servidor");
                break;
            case 20:
                exportCSVs.CreateNotebookDetailsSheet();
                ShowMessage("Notebook");
                break;
            case 21:
                exportCSVs.CreateMonitorDetailsSheet();
                ShowMessage("Monitor");
                break;
            default:
                break;
        }
        StartCoroutine(CloseMessageRoutine());
    }

    public void HandleInputData(int value)
    {
        csvToExportIndex = value;
        CloseMessage();
        StopCoroutine(CloseMessageRoutine());
    }

    private void ShowMessage(string sheetName)
    {
        messageText.text = "Planilha " + sheetName + " exportada com sucesso";
    }

    private void CloseMessage()
    {
        messageText.text = "";
    }

    private IEnumerator CloseMessageRoutine()
    {
        yield return new WaitForSeconds(5f);
        CloseMessage();
    }

}
