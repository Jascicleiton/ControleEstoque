using Newtonsoft.Json.Bson;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CloseProgram : MonoBehaviour
{
    private static bool showWarningMessage = true;
    [SerializeField] private GameObject warningMessagePanel = null;
    
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
