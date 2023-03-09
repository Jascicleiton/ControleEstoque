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
                exportCSVs.CreateAdaptadorACDetailsSheet();
                ShowMessage("Adaptador AC");
                break;
            case 2:
                exportCSVs.CreateDesktopDetailsSheet();
                ShowMessage("Desktop");
                break;
            case 3:
                exportCSVs.CreateMonitorDetailsSheet();
                ShowMessage("Monitor");
                break;
            case 4:
                exportCSVs.CreateNotebookDetailsSheet();
                ShowMessage("Notebook");
                break;
            case 5:
                exportCSVs.CreateRoteadorDetailsSheet();
                ShowMessage("Roteador");
                break;
            case 6:
                exportCSVs.CreateServidorDetailsSheet();
                ShowMessage("Servidor");
                break;
            case 7:
                exportCSVs.CreateSwitchDetailsSheet();
                ShowMessage("Switch");
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
