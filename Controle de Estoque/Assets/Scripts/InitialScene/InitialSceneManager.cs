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

    [SerializeField] TMP_Text helloMessage;

    // Start is called before the first frame update
    void Start()
    {
        helloMessage.text = "Olá " + UsersManager.Instance.currentUser.username + ". \nO que você deseja fazer agora?";
        ShowHideButtons();
    }

    /// <summary>
    /// hides the buttons that the user is not allowed to use
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
            }
            else
            {
                consultButton.gameObject.SetActive(true);
                moveButton.gameObject.SetActive(true);
                addRemoveButton.gameObject.SetActive(false);
                updateItemButton.gameObject.SetActive(false);
                exportSheetsButton.gameObject.SetActive(false);
                logoutButton.gameObject.SetActive(true);
            }
        }
        else
        {
            Debug.LogWarning("UsersManager not found on InitialScene");
        }
    }

    /// <summary>
    /// Goes to ConsultScene
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
    /// Goes to AddRemoveItemScene
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
    /// Goes to ExportSheetsScene
    /// </summary>
    public void ExportSheetsClicked()
    {
        SceneManager.LoadScene(ConstStrings.SceneExportSheets);
    }

    /// <summary>
    /// logout the current user and goes to MainMenu
    /// </summary>
    public void LogoutClicked()
    {
        UsersManager.Instance.currentUser = null;
        SceneManager.LoadScene(ConstStrings.SceneMainMenu);
    }
}
