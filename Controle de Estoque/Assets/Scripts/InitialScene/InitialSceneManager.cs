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
    [SerializeField] private Button recoverBKPButton;

    [SerializeField] TMP_Text helloMessage;

    void Start()
    {
        helloMessage.text = "Olá " + UsersManager.Instance.currentUser.GetUsername() + ". \nO que você deseja fazer agora?";
        ShowHideButtons();
      
       // InternalDatabase.Instance.FillFullDatabase();
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
                    recoverBKPButton.gameObject.SetActive(false);
                    break;
                case 2:
                    addRemoveButton.gameObject.SetActive(false);
                    updateItemButton.gameObject.SetActive(false);
                    moveButton.gameObject.SetActive(false);
                    recoverBKPButton.gameObject.SetActive(false);
                    break;
                case 3:
                    updateItemButton.gameObject.SetActive(false);
                    exportSheetsButton.gameObject.SetActive(false);
                    noPaNoSeButton.gameObject.SetActive(false);
                    allMovementsButton.gameObject.SetActive(false);
                    recoverBKPButton.gameObject.SetActive(false);
                    break;
                case 5:
                    updateItemButton.gameObject.SetActive(false);
                    recoverBKPButton.gameObject.SetActive(false);
                    break;
                case 4:
                    addRemoveButton.gameObject.SetActive(false);
                    updateItemButton.gameObject.SetActive(false);
                    exportSheetsButton.gameObject.SetActive(false);
                    allMovementsButton.gameObject.SetActive(false);
                    moveButton.gameObject.SetActive(false);
                    recoverBKPButton.gameObject.SetActive(false);
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
                    recoverBKPButton.gameObject.SetActive(false);
                    break;
            }
        }
        else
        {
            Debug.LogWarning("UsersManager not found on InitialScene");
        }    
    }

  
}
