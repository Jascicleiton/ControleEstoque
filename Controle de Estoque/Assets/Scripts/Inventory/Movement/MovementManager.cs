using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
using System;
using UnityEngine.SceneManagement;

public class MovementManager : MonoBehaviour
{
    [SerializeField] TMP_Dropdown itemInformationDP;
    [SerializeField] TMP_InputField itemInformationInput;
    [SerializeField] GameObject fromPanel;
    [SerializeField] TMP_InputField fromInput;
    [SerializeField] GameObject toPanel;
    [SerializeField] TMP_InputField toInput;
    [SerializeField] GameObject whoPanel;
    [SerializeField] TMP_InputField whoInput;

    [SerializeField] private GameObject messagePanel;
    [SerializeField] private TMP_Text messageText;

    private int itemToChangeIndex;
    private SheetColumns itemToChange;
    private bool itemFound = false;

    MovementRecords movementToRecord;

    private void Update()
    {
        if(Input.GetKeyDown(KeyCode.Return) || Input.GetKeyDown(KeyCode.KeypadEnter))
        {
            if (itemFound)
            {
                MoveItem();
            }
            else
            {
                CheckIfItemExists();
            }
            
        }
    }

    private void CheckIfItemExists()
    {
        if(itemInformationDP.value == 0)
        {
           itemToChange =  ConsultDatabase.Instance.ConsultPatrimonio(itemInformationInput.text);
        }
        else if(itemInformationDP.value == 1)
        {
            itemToChange = ConsultDatabase.Instance.ConsultSerial(itemInformationInput.text);
        }

        if (itemToChange != null)
        {
            ShouldHidePanels(false);
            fromInput.text = itemToChange.Local;
            whoInput.text = UsersManager.Instance.currentUser.username;
            itemFound = true;
        }
        else
        {
                        itemFound = false;
            ShowMessage(itemFound);
        }
       
    }

    /// <summary>
    /// true if should hide
    /// </summary>
    private void  ShouldHidePanels(bool shouldHide)
    {
        if (shouldHide)
        {
            fromPanel.GetComponent<CanvasGroup>().alpha = 0;
            toPanel.GetComponent<CanvasGroup>().alpha = 0;
            whoPanel.GetComponent<CanvasGroup>().alpha = 0;
        }
        else
        {
            fromPanel.GetComponent<CanvasGroup>().alpha = 1;
            toPanel.GetComponent<CanvasGroup>().alpha = 1;
            whoPanel.GetComponent<CanvasGroup>().alpha = 1;
        }
    }



    /// <summary>
    /// Try to change the item location
    /// </summary>
    private void MoveItem()
    {
        SheetColumns item = new SheetColumns();
        if (itemInformationDP.value == 0)
        {
             item = ConsultDatabase.Instance.ConsultPatrimonio(itemInformationInput.text);
            
        }
        else if (itemInformationDP.value == 1)
        {
           item = ConsultDatabase.Instance.ConsultSerial(itemInformationInput.text);
        }

        if (item != null)
        {
            itemToChangeIndex = ConsultDatabase.Instance.GetItemIndex();
            UpdateItemToChange(item);
            UpdateDatabase();
            ShowMessage(true);
            }
        else
        {
            ShowMessage(false);
        }
    }

    private void UpdateItemToChange(SheetColumns item)
    {
        itemToChange = item;
       
        itemToChange.Local = toInput.text;
        if (itemToChange.Local == "Estoque")
        {
            itemToChange.Entrada = DateTime.Now.ToString("ddMMyyyy");
            itemToChange.Saida = "";
                }
        else
        {
            itemToChange.Entrada = "";
            itemToChange.Saida = DateTime.Now.ToString("dd/MM/yyyy");
        }

        movementToRecord = new MovementRecords();
        movementToRecord.username = UsersManager.Instance.currentUser.username;
        movementToRecord.date = DateTime.Now.ToString("dd/MM/yyyy");
        movementToRecord.item = item;
    }

    private void UpdateDatabase()
    {
        InternalDatabase.fullDatabase.itens[itemToChangeIndex] = itemToChange;
        InternalDatabase.movementRecords.Add(movementToRecord);
        EventHandler.CallDatabaseUpdatedEvent(ConstStrings.DataDatabaseSaveFile);
    }

    private void ShowMessage(bool itemFound)
    {
        messagePanel.SetActive(true);
        if(itemFound)
        {
            messageText.text = "Item movido com sucesso";
            ResetInputs();
            itemFound = false;
        }
        else
        {
            messageText.text = "Item não encontrado";
            ResetInputs();
        }
        StartCoroutine(CloseRoutine());
    }

    private IEnumerator CloseRoutine()
    {
        yield return new WaitForSeconds(5f);
        CloseMessagePanel();
    }

    private void ResetInputs()
    {
        itemInformationInput.text = "";
        fromInput.text = "";
        toInput.text = "";
        whoInput.text = "";
        ShouldHidePanels(true);
    }

    public void CloseMessagePanel()
    {
        itemFound = false;
        messagePanel.SetActive(false);
    }

    /// <summary>
    /// Changes the text shown on itemInformationInput based on the selection of the itemInformationDP
    /// </summary>
    public void HandleInputData(int value)
    {
        if (value == 0)
        {
            itemInformationInput.placeholder.GetComponent<TextMeshProUGUI>().text = "Patrimônio";
        }
        if (value == 1)
        {
            itemInformationInput.placeholder.GetComponent<TextMeshProUGUI>().text = "Serial";
        }
    }

    
    
}
