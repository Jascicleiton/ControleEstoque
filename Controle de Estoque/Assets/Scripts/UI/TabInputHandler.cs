using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class TabInputHandler : MonoBehaviour
{
    [SerializeField] TMP_InputField[] inputFields;

    private int inputIndex = 0;
    private int activeInputsCount;
    public bool isWithItemInformationPanelController = false;

    private void Start()
    {      
            GetActiveInputs();
       //     CheckIfInputIsActiveAndEnabled();
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
                    inputIndex = activeInputsCount - 1;
                }
                CheckIfInputIsActiveAndEnabled();
            }
        }
        else
        {
            if (Input.GetKeyDown(KeyCode.Tab))
            {
                inputIndex++;
                if (inputIndex >= activeInputsCount)
                {
                    inputIndex = 0;
                }
                CheckIfInputIsActiveAndEnabled();
            }
        }
    }

    /// <summary>
    /// Loops through all inputs and and selects the first one that is active, enabled and interactable
    /// </summary>
    public void CheckIfInputIsActiveAndEnabled()
    {
        if (inputFields[inputIndex] != null && inputFields[inputIndex].isActiveAndEnabled && inputFields[inputIndex].interactable)
        {
            inputFields[inputIndex].Select();
        }
        else
        {
            inputIndex++;
            if (inputIndex < activeInputsCount)
            {
                CheckIfInputIsActiveAndEnabled();
            }
            else
            {
                inputIndex = 0;
                CheckIfInputIsActiveAndEnabled();
            }
        }
    }

    /// <summary>
    /// Loops through all inputs, get the ones that are Active and select the first active one. It is called using the Event 'UpdateTabInputs'
    /// each time the number of inputs changes.
    /// </summary>
    public void GetActiveInputs()
    {
        inputIndex = 0;
        activeInputsCount = 0;
        for (int i = 0; i < inputFields.Length; i++)
        {
            if (inputFields[i] != null && inputFields[i].IsActive())
            {
                activeInputsCount++;
            }
        }
        CheckIfInputIsActiveAndEnabled();
    }

    /// <summary>
    /// Called each time an input is selected to set the inputIndex so it circles through the inputs in the correct order
    /// </summary>
    public void InputSelected(TMP_InputField inputSelected)
    {
        for (int i = 0; i < inputFields.Length; i++)
        {
            if (inputSelected == inputFields[i])
            {
                inputIndex = i;
            }
        }
    }
}
