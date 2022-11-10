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
    [SerializeField] private Button updateDatabaseButton;

    [SerializeField] TMP_Text helloMessage;

    // Start is called before the first frame update
    void Start()
    {
        helloMessage.text = "Olá " + UsersManager.Instance.currentUser.username + ". \nO que você deseja fazer agora?";
        ShowHideButtons();
    }

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
                updateDatabaseButton.gameObject.SetActive(true);
            }
            else
            {
                consultButton.gameObject.SetActive(true);
                moveButton.gameObject.SetActive(true);
                addRemoveButton.gameObject.SetActive(false);
                updateItemButton.gameObject.SetActive(false);
                updateDatabaseButton.gameObject.SetActive(false);
            }
        }
        else
        {
            Debug.LogWarning("UsersManager not found on InitialScene");
        }
    }

    public void ConsultClicked()
    {
        SceneManager.LoadScene("ConsultScene");
    }

    public void MoveClicked()
    {
        SceneManager.LoadScene("MovementScene");
    }

    public void AddClicked()
    {
        SceneManager.LoadScene("ConsultScene");
    }

    public void UpdateItemClicked()
    {
        SceneManager.LoadScene("ConsultScene");
    }

    public void UpdateDatabaseClicked()
    {
        SceneManager.LoadScene("ConsultScene");
    }
}
