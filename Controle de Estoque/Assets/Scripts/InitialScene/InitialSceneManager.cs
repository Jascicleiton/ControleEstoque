using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using TMPro;

public class InitialSceneManager : MonoBehaviour
{
    [SerializeField] private Button consultButton;
    [SerializeField] private Button moveButton;
    [SerializeField] private Button addRemoveButton;
    [SerializeField] private Button updateItemButton;
    [SerializeField] private Button exportSheetsButton;
    [SerializeField] private Button logoutButton;
    [SerializeField] private Button noPaNoSeButton;
    [SerializeField] private Button fullInventoryButton;
    [SerializeField] private Button fullDetailsButton;
    [SerializeField] private Button allMovementsButton;

    [SerializeField] TMP_Text helloMessage;

    // Start is called before the first frame update
    void Start()
    {
        helloMessage.text = "Olá " + UsersManager.Instance.currentUser.username + ". \nO que você deseja fazer agora?";
        ShowHideButtons();
        InternalDatabase.Instance.FillFullDatabase();
    }

    /// <summary>
    /// Hides the buttons that the user is not allowed to use
    /// </summary>
    private void ShowHideButtons()
    {
        if(UsersManager.Instance != null)
        {
            if(UsersManager.Instance.adminLogged)
            {
                consultButton.gameObject.SetActive(true);
                moveButton.gameObject.SetActive(true);
                addRemoveButton.gameObject.SetActive(true);
                updateItemButton.gameObject.SetActive(true);
                exportSheetsButton.gameObject.SetActive(true);
                logoutButton.gameObject.SetActive(true);
                noPaNoSeButton.gameObject.SetActive(true);
                fullInventoryButton.gameObject.SetActive(false);
                fullDetailsButton.gameObject.SetActive(false);
                allMovementsButton.gameObject.SetActive(true);
            }
            else
            {
                consultButton.gameObject.SetActive(true);
                moveButton.gameObject.SetActive(true);
                logoutButton.gameObject.SetActive(true);
                noPaNoSeButton.gameObject.SetActive(true);
                addRemoveButton.gameObject.SetActive(false);
                updateItemButton.gameObject.SetActive(false);
                exportSheetsButton.gameObject.SetActive(false);        
                fullInventoryButton.gameObject.SetActive(false);
                fullDetailsButton.gameObject.SetActive(false);
                allMovementsButton.gameObject.SetActive(false);
            }
        }
        else
        {
            Debug.LogWarning("UsersManager not found on InitialScene");
        }
        if (InternalDatabase.Instance.currentEstoque == CurrentEstoque.Concert)
        {
            consultButton.gameObject.SetActive(true);
            moveButton.gameObject.SetActive(true);
            addRemoveButton.gameObject.SetActive(true);
            updateItemButton.gameObject.SetActive(true);
            exportSheetsButton.gameObject.SetActive(true);
            logoutButton.gameObject.SetActive(true);
            noPaNoSeButton.gameObject.SetActive(false);
            fullInventoryButton.gameObject.SetActive(false);
            fullDetailsButton.gameObject.SetActive(false);
            allMovementsButton.gameObject.SetActive(true);
        }
      
    }

    /// <summary>
    /// Go to ConsultScene
    /// </summary>
    public void ConsultClicked()
    {
        SceneManager.LoadScene(ConstStrings.SceneConsult);
    }

    /// <summary>
    /// Goes to MovementScene
    /// </summary>
    public void MoveClicked()
    {
        SceneManager.LoadScene(ConstStrings.SceneMovement);
    }

    /// <summary>
    /// Go to AddRemoveItemScene
    /// </summary>
    public void AddClicked()
    {
        SceneManager.LoadScene(ConstStrings.SceneAddRemoveItem);
    }

    /// <summary>
    /// Goes to UpdateItemScene
    /// </summary>
    public void UpdateItemClicked()
    {
        SceneManager.LoadScene(ConstStrings.SceneUpdateItem);
    }

    /// <summary>
    /// Go to ExportSheetsScene
    /// </summary>
    public void ExportSheetsClicked()
    {
        SceneManager.LoadScene(ConstStrings.SceneExportSheets);
    }

    /// <summary>
    /// Go to NoPaNoSeScene
    /// </summary>
    public void NoPaNoSeClicked()
    {
        SceneManager.LoadScene(ConstStrings.SceneNoPaNoSe);
    }

    /// <summary>
    /// Go to ConsultSceneAllinventory
    /// </summary>
    public void ShowAllInventoryClicked()
    {
        SceneManager.LoadScene(ConstStrings.SceneConsultInventoryAll);
    }

    /// <summary>
    /// Go to ConsultSceneAllDetails
    /// </summary>
    public void ShowAllDetailsClicked()
    {
        SceneManager.LoadScene(ConstStrings.SceneConsultDetailsAll);
    }

    /// <summary>
    /// logout the current user and go to MainMenu
    /// </summary>
    public void LogoutClicked()
    {
        UsersManager.Instance.currentUser = new User("pessoa", "");
        SceneManager.LoadScene(ConstStrings.SceneMainMenu);
    }
}
