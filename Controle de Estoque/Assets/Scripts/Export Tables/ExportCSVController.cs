using FrostweepGames.Plugins.WebGLFileBrowser;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

[RequireComponent(typeof(ExportCSVs))]
public class ExportCSVController : MonoBehaviour
{
    private ExportCSVs exportCSVs = null;
    private int csvToExportIndex = 0;
    
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
        returnButton.clicked += () => { ReturnToPreviousScreen(); };
        exportButon.clicked += () => { ExportSelectedCSV(); };
        dropdownField.RegisterCallback<ChangeEvent<string>>(HandleInputData);
        WebGLFileBrowser.FileWasSavedEvent += FileWasSaved;
        WebGLFileBrowser.FileSaveFailedEvent += FileFailedSaving;
    }

   

    private void OnDisable()
    {
        returnButton.clicked -= () => { ReturnToPreviousScreen(); };
        exportButon.clicked -= () => { ExportSelectedCSV(); };
        dropdownField.UnregisterCallback<ChangeEvent<string>>(HandleInputData);
    }

    public void ExportSelectedCSV()
    {
        File file = new File()
        {
            fileInfo = new FileInfo()
            {
                fullName = "Inventário.csv"
            },
            data = System.Text.Encoding.UTF8.GetBytes(exportCSVs.CreateInventarioSheet().ToString())
        };
        WebGLFileBrowser.SaveFile(file);
    }

    public void HandleInputData(ChangeEvent<string> evt)
    {
        // maybe close message
    }
    
    /// <summary>
    /// Goes to InitialScene
    /// </summary>
    private void ReturnToPreviousScreen()
    {
        ChangeScreenManager.Instance.OpenScene(Scenes.ExportTablesScene, Scenes.InitialScene);
    }

    private void FileFailedSaving(string error)
    {
        EventHandler.CallIsOneMessageOnlyEvent(true);
        EventHandler.CallOpenMessageEvent("Planilha não exportada");
    }

    private void FileWasSaved(File file)
    {
        EventHandler.CallIsOneMessageOnlyEvent(true);
        EventHandler.CallOpenMessageEvent("Planilha exportada");
    }

}
