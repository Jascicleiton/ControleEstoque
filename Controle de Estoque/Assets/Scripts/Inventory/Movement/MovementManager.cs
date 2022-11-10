using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class MovementManager : MonoBehaviour
{
    [SerializeField] TMP_Dropdown itemInformationDP;
    [SerializeField] TMP_InputField itemInformationInput;
    [SerializeField] TMP_InputField fromInput;
    [SerializeField] TMP_InputField toInput;
    [SerializeField] TMP_InputField whoInput;

    [SerializeField] private GameObject messagePanel;

    private MovementMessage messageController;

    private void Start()
    {
        messageController = FindObjectOfType<MovementMessage>();
    }

    private void Update()
    {
        if(Input.GetKeyDown(KeyCode.Return) || Input.GetKeyDown(KeyCode.KeypadEnter))
        {
            MoveItem();
        }
    }
    /// <summary>
    /// Try to change the item location
    /// </summary>
    private void MoveItem()
    {
        if(itemInformationDP.value == 0)
        {
            SheetColumns item = ConsultDatabase.Instance.ConsultPatrimonio(itemInformationInput.text);
            if (item != null)
            {
                
            }
            else
            {
                messageController.ShowMessage(0, item);
            }
        }
        if (itemInformationDP.value == 1)
        {
            SheetColumns item = ConsultDatabase.Instance.ConsultSerial(itemInformationInput.text);
        }
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
