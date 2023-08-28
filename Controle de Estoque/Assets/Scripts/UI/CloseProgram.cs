using Assets.Scripts.Misc;
using System.Collections;
using UnityEditor;
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
        if (Application.platform != RuntimePlatform.WindowsEditor && Application.platform != RuntimePlatform.WindowsPlayer)
        {
            Destroy(this.gameObject);
        }
        else
        {
            if (closeButton == null)
            {
                closeButton = GetComponentInChildren<Button>();
            }
        }
        
    }

    /// <summary>
    /// Save the setting of showWarningMessage on the user computer
    /// </summary>
    private void SaveWarningMessageValue()
    {
        PlayerPrefs.SetInt(ConstStrings.showWarningMessage, showWarningMessage ? 1 : 0);
        PlayerPrefs.Save();
    }

    /// <summary>
    /// Show warning message when button is clicked if showWarningMessage is true
    /// </summary>
    private void ShowWarningMessage()
    {
        warningMessagePanel.SetActive(true);
    }

    /// <summary>
    /// Waits half a second to allow the database to be saved
    /// </summary>
    private IEnumerator WaitATick()
    {
        yield return new WaitForSeconds(0.5f);
        Application.Quit();
    }

    /// <summary>
    /// If showWarningMessage is true, open the warning message panel. If it is false, close the program
    /// </summary>
    public void ButtonClicked()
    {
        if(showWarningMessage)
        {
            ShowWarningMessage();
        }
        else
        {
            EventHandler.CallDatabaseUpdatedEvent();
            StartCoroutine(WaitATick());
        }
    }

    /// <summary>
    /// Close the program if yes button from the Warning message panel is clicked
    /// </summary>
    public void YesButtonClicked()
    {
        EventHandler.CallDatabaseUpdatedEvent();
        StartCoroutine(WaitATick());
    }

    /// <summary>
    /// Close warningMessagePanel
    /// </summary>
    public void NoButtonClicked()
    {
        warningMessagePanel.SetActive(false);
    }

    /// <summary>
    /// Save the showWarningMessage value if the check box is clicked
    /// </summary>
    public void HandleInputData(bool value)
    {
        showWarningMessage = !value;       

            SaveWarningMessageValue();
    }
}
