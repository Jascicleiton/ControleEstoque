using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UIElements;

[RequireComponent(typeof(ExportCSVs))]
public class ExportCSVController : MonoBehaviour
{
    private ExportCSVs exportCSVs = null;
    private int csvToExportIndex = 0;
    private Label messageText;

    private VisualElement root;
    private Button returnButton;
    private Button exportButon;
    private DropdownField dropdownField;

    private void Start()
    {
        exportCSVs = GetComponent<ExportCSVs>();
    }

    private void OnEnable()
    {
        root = GetComponent<UIDocument>().rootVisualElement;
        returnButton = root.Q<Button>("ReturnButton");
        exportButon = root.Q<Button>("ExportButton");
        dropdownField = root.Q<DropdownField>("CategoryDP");
        messageText = root.Q<Label>("MessageLabel");
        returnButton.clicked += () => { ReturnToPreviousScreen(); };
        exportButon.clicked += () => { ExportSelectedCSV(); };
        dropdownField.RegisterCallback<ChangeEvent<string>>(HandleInputData);
    }

    private void OnDisable()
    {
        returnButton.clicked -= () => { ReturnToPreviousScreen(); };
        exportButon.clicked -= () => { ExportSelectedCSV(); };
        dropdownField.UnregisterCallback<ChangeEvent<string>>(HandleInputData);
    }

    public void ExportSelectedCSV()
    {
       // exportCSVs.CreateInventarioSheet();
        ShowMessage("Inventário");
        //switch (csvToExportIndex)
        //{
        //    case 0:
        //        exportCSVs.CreateInventarioSheet2();
        //        ShowMessage("Inventário");
        //        break;
        //    case 1:
        //        exportCSVs.CreateAdaptadorACDetailsSheet();
        //        ShowMessage("Adaptador AC");
        //        break;
        //    case 2:
        //        exportCSVs.CreateDesktopDetailsSheet();
        //        ShowMessage("Desktop");
        //        break;
        //    case 3:
        //        exportCSVs.CreateMonitorDetailsSheet();
        //        ShowMessage("Monitor");
        //        break;
        //    case 4:
        //        exportCSVs.CreateNotebookDetailsSheet();
        //        ShowMessage("Notebook");
        //        break;
        //    case 5:
        //        exportCSVs.CreateRoteadorDetailsSheet();
        //        ShowMessage("Roteador");
        //        break;
        //    case 6:
        //        exportCSVs.CreateServidorDetailsSheet();
        //        ShowMessage("Servidor");
        //        break;
        //    case 7:
        //        exportCSVs.CreateSwitchDetailsSheet();
        //        ShowMessage("Switch");
        //        break;           
        //    default:
        //        break;
        //}
        StartCoroutine(CloseMessageRoutine());
    }

    public void HandleInputData(ChangeEvent<string> evt)
    {
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

    /// <summary>
    /// Goes to InitialScene
    /// </summary>
    public void ReturnToPreviousScreen()
    {
        ChangeScreenManager.Instance.OpenScene(Scenes.ExportTablesScene, Scenes.InitialScene);
    }

}
