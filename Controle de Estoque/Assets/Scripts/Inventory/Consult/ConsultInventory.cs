using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ConsultInventory : MonoBehaviour
{
    [SerializeField] TMP_Dropdown searchOption; // drop down used to choose search option
    [SerializeField] TMP_Dropdown categoryDP; // drop down used to search for an item category
    [SerializeField] TMP_InputField inputField; // field use to type the item "Patrim�nio" or the item "Serial"

   [SerializeField] private GameObject consultResult;
    [SerializeField] private Transform consultResultTransform;

    private void Start()
    {
       
    }

    private void Update()
    {
        if(inputField.IsActive())
        {
            if(Input.GetKeyDown(KeyCode.Return) || Input.GetKeyDown(KeyCode.KeypadEnter))
            {
                
                if (searchOption.value == 1)
                {
                   if(ConsultPatrimonio(inputField.text) != null)
                    {
                        GameObject result =  Instantiate(consultResult, consultResultTransform);
                        result.GetComponent<ConsultResult>().ShowResult(ConsultPatrimonio(inputField.text));
                    }
                    else
                    {
                        print("Patrim�nio n�o existente");
                    }
                }
                else if (searchOption.value == 2)
                {
                    if (ConsultSerial(inputField.text) != null)
                    {
                        GameObject result = Instantiate(consultResult, consultResultTransform);
                        result.GetComponent<ConsultResult>().ShowResult(ConsultSerial(inputField.text));
                    }
                    else
                    {
                        print("Serial n�o existente");
                    }
                }
            }
        }
    }

    /// <summary>
    /// 0 = Categoria, 1 = Patrim�nio, 2 = Serial
    /// </summary>
    public void HandleInputData(int value)
    {
        switch (value)
        {
            case 0:
                categoryDP.gameObject.SetActive(true);
                inputField.gameObject.SetActive(false);
                break;
            case 1:
                categoryDP.gameObject.SetActive(false);
                inputField.gameObject.SetActive(true);
                inputField.placeholder.GetComponent<TextMeshProUGUI>().text = "Patrim�nio";
                break;
            case 2:
                categoryDP.gameObject.SetActive(false);
                inputField.gameObject.SetActive(true);
                inputField.placeholder.GetComponent<TextMeshProUGUI>().text = "Serial";
                break;
            default:
                break;
        }
    }

    /// <summary>
    /// Consult the inventory using the "Serial"
    /// </summary>
    public SheetColumns ConsultSerial(string serialToConsult)
    {
        foreach (SheetColumns item in InternalDatabase.fullDatabase.itens)
        {
            if(item.Serial == serialToConsult)
            {
                return item;
            }
        }
        return null;
    }

    /// <summary>
    /// Consult the inventory using the "Patrim�nio"
    /// </summary>
    public SheetColumns ConsultPatrimonio(string patrimonioToConsult)
    {
        foreach (SheetColumns item in InternalDatabase.fullDatabase.itens)
        {
            if (item.Patrimonio == patrimonioToConsult)
            {
                return item;
            }
        }
        return null;
    }

    /// <summary>
    /// Consult the inventory using the parameters chosen from each category
    /// </summary>
    public void Consult(string[] paramentersNames, string[] parametersValues)
    {

    }
}