using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class TabInputHandler : MonoBehaviour
{
    [SerializeField] TMP_InputField[] inputFields;

    [SerializeField] private int inputIndex;

    private void Start()
    {
        if (inputFields[inputIndex] != null && inputFields[inputIndex].isActiveAndEnabled)
        {
            inputFields[inputIndex].Select();
        }
    }


    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Tab))
        {
            inputIndex++;
            print(inputIndex);
            if (inputIndex >= inputFields.Length)
            {
                inputIndex = 0;
            }
            if (inputFields[inputIndex] != null && inputFields[inputIndex].isActiveAndEnabled && inputIndex < inputFields.Length)
            {
                inputFields[inputIndex].Select();
            }
            else if ((inputFields[inputIndex] != null && !inputFields[inputIndex].interactable) || inputFields[inputIndex] == null)
            {
                while ((inputFields[inputIndex] != null && !inputFields[inputIndex].interactable) || inputFields[inputIndex] == null)
                {
                    inputIndex++;
                    //    print(inputIndex);
                }
            }
            print(inputIndex);
            if (inputIndex < inputFields.Length)
            {
                inputFields[inputIndex].Select();
            }
            else
            {
                inputIndex = 0;
                inputFields[inputIndex].Select();
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
