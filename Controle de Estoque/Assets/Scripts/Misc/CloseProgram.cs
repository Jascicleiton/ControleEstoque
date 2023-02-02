using UnityEngine;
using UnityEngine.UI;

public class CloseProgram : MonoBehaviour
{
    private static bool showWarningMessage = true;
    [SerializeField] private GameObject warningMessagePanel = null;
    [SerializeField] private Button closeButton;
    
    private void Awake()
    {
        if (PlayerPrefs.HasKey(ConstStrings.showWarningMessage))
        {
            showWarningMessage = PlayerPrefs.GetInt(ConstStrings.showWarningMessage) == 1;
        }
        else
        {
            showWarningMessage = true;
        }
       
    }

    private void Start()
    {
        if(closeButton == null)
        {
            closeButton = GetComponentInChildren<Button>();
        }
    }

    private void OnEnable()
    {
        EventHandler.EnableInput += EnableInput; 
    }

    private void OnDisable()
    {
        EventHandler.EnableInput -= EnableInput;
    }

    private void EnableInput(bool enableInput)
    {
        closeButton.enabled = enableInput;
    }

    private void SaveWarningMessageValue()
    {
        PlayerPrefs.SetInt(ConstStrings.showWarningMessage, showWarningMessage ? 1 : 0);
        PlayerPrefs.Save();
    }

    private void ShowWarningMessage()
    {
        warningMessagePanel.SetActive(true);
    }

    public void ButtonClicked()
    {
        if(showWarningMessage)
        {
            ShowWarningMessage();
        }
        else
        {
            Application.Quit();
        }
    }

    public void YesButtonClicked()
    {
        Application.Quit();
    }

    public void NoButtonClicked()
    {
        warningMessagePanel.SetActive(false);
    }

    public void HandleInputData(bool value)
    {
        showWarningMessage = !value;       

            SaveWarningMessageValue();
    }
}
