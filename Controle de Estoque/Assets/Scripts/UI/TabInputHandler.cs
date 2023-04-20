using System.Collections;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using UnityEngine;

public class TabInputHandler : MonoBehaviour
{
    List<TMP_InputField> inputFields = new List<TMP_InputField>();

    private int inputIndex = 0;
       public bool isWithItemInformationPanelController = false;
    
    private void Start()
    {      
            GetActiveInputs();
    }

    private void OnEnable()
    {
        EventHandler.UpdateTabInputs += GetActiveInputs;
    }

    private void OnDisable()
    {
        EventHandler.UpdateTabInputs -= GetActiveInputs;
    }

    // Check if Shift + Tab or Tab is being pressed
    void Update()
    {
        if (Input.GetKey(KeyCode.LeftShift))
        {        
            if (Input.GetKeyDown(KeyCode.Tab))
            {
                inputIndex--;
                if (inputIndex < 0)
                {
                    inputIndex = inputFields.Count -1;
                }
                inputFields[inputIndex].Select();
            }
        }
        else
        {
            if (Input.GetKeyDown(KeyCode.Tab))
            {
                inputIndex++;
                if (inputIndex >= inputFields.Count)
                {
                    inputIndex = 0;
                }
                inputFields[inputIndex].Select();
            }
        }
    }

    /// <summary>
    /// Loops through all inputs, get the ones that are Active and select the first active one. It is called using the Event 'UpdateTabInputs'
    /// each time the number of inputs changes.
    /// </summary>
    public void GetActiveInputs()
    {
        inputFields.Clear();
        inputFields = FindObjectsOfType<TMP_InputField>().ToList();
        for (int i = 0; i < inputFields.Count; i++)
        {
            if (!inputFields[i].interactable || inputFields[i].tag != ConstStrings.TabTarget || inputFields[i].GetComponent<InputNumber>() == null)
            {
                inputFields.RemoveAt(i);
                continue;
            }
        }
       // print(inputFields.Count);
        if (inputFields != null && inputFields.Count > 2)
        {
            inputFields.Sort((x, y) => x.GetComponent<InputNumber>().number.CompareTo(y.GetComponent<InputNumber>().number));
            inputFields[0].Select();
        }
    }

    /// <summary>
    /// Called each time an input is selected to set the inputIndex so it circles through the inputs in the correct order
    /// </summary>
    public void InputSelected(TMP_InputField inputSelected)
    {
        for (int i = 0; i < inputFields.Count; i++)
        {
            if (inputSelected == inputFields[i])
            {
                inputIndex = i;
            }
        }
    }
}
