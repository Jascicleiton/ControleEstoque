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
    [SerializeField] private Button allMovementsButton;

    [SerializeField] TMP_Text helloMessage;

    // Start is called before the first frame update
    void Start()
    {
        helloMessage.text = "Olá " + UsersManager.Instance.currentUser.GetUsername() + ". \nO que você deseja fazer agora?";
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
            switch (UsersManager.Instance.currentUser.GetAccessLevel())
            {
                case 1:
                    addRemoveButton.gameObject.SetActive(false);
                    updateItemButton.gameObject.SetActive(false);
                    exportSheetsButton.gameObject.SetActive(false);
                    allMovementsButton.gameObject.SetActive(false);
                    break;
                case 2:
                    addRemoveButton.gameObject.SetActive(false);
                    updateItemButton.gameObject.SetActive(false);
                    moveButton.gameObject.SetActive(false);
                    break;
                case 3:
                    updateItemButton.gameObject.SetActive(false);
                    exportSheetsButton.gameObject.SetActive(false);
                    noPaNoSeButton.gameObject.SetActive(false);
                    allMovementsButton.gameObject.SetActive(false);
                    break;
                case 5:
                    updateItemButton.gameObject.SetActive(false);
                    break;
                case 10:

                    break;
                default:
                    consultButton.gameObject.SetActive(true);
                    moveButton.gameObject.SetActive(false);
                    addRemoveButton.gameObject.SetActive(false);
                    updateItemButton.gameObject.SetActive(false);
                    exportSheetsButton.gameObject.SetActive(false);
                    noPaNoSeButton.gameObject.SetActive(false);
                    allMovementsButton.gameObject.SetActive(false);
                    break;
            }
        }
        else
        {
            Debug.LogWarning("UsersManager not found on InitialScene");
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
        UsersManager.Instance.currentUser = new User("pessoa", "", 1);
        SceneManager.LoadScene(ConstStrings.SceneMainMenu);
    }
}
