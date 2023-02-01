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
        if (!isWithItemInformationPanelController)
        {
            GetActiveInputs();
            CheckIfInputIsActiveAndEnabled();
        }
    }


    // Update is called once per frame
    void Update()
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

    public void GetActiveInputs()
    {
        activeInputsCount = 0;
        for (int i = 0; i < inputFields.Length; i++)
        {
            if (inputFields[i] != null && inputFields[i].IsActive())
            {
                activeInputsCount++;
                            }
        }
            }

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
