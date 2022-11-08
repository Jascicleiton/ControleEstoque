using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ConsultInventory : MonoBehaviour
{
    [SerializeField] TMP_Dropdown searchOption; // drop down used to choose search option
    [SerializeField] TMP_Dropdown categoryDP; // drop down used to search for an item category
    [SerializeField] TMP_InputField inputField; // field use to type the item "Patrimônio" or the item "Serial"

    [SerializeField] private GameObject consultResult;
    [SerializeField] private Transform consultResultTransform;

    [SerializeField] private TMP_InputField[] categorySearchInputs;
    [SerializeField] private TMP_Text[] categorySearchNames;

    List<string> searchNameList = new List<string>();
    List<string> searchInputsList = new List<string>();

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
                        RemoveOldSearch();
                        GameObject result =  Instantiate(consultResult, consultResultTransform);
                        result.GetComponent<ConsultResult>().ShowResult(ConsultPatrimonio(inputField.text), 0);
                    }
                    else
                    {
                        print("Patrimônio não existente");
                    }
                }
                else if (searchOption.value == 2)
                {
                    if (ConsultSerial(inputField.text) != null)
                    {
                        RemoveOldSearch();
                        GameObject result = Instantiate(consultResult, consultResultTransform);
                        result.GetComponent<ConsultResult>().ShowResult(ConsultSerial(inputField.text), 1);
                    }
                    else
                    {
                        print("Serial não existente");
                    }
                }
            }
        }
        if (categoryDP.IsActive())
        {
            if (Input.GetKeyDown(KeyCode.Return) || Input.GetKeyDown(KeyCode.KeypadEnter))
            {

            }
        }
    }

    private void RemoveOldSearch()
    {
        if(consultResultTransform.childCount > 0)
        {
            for (int i = 0; i < consultResultTransform.childCount; i++)
            {
                Destroy(consultResultTransform.GetChild(i).gameObject);
            }
        }
    }

    /// <summary>
    /// Consult if the item exists on the database using the "Serial"
    /// </summary>
    private SheetColumns ConsultSerial(string serialToConsult)
    {
        foreach (SheetColumns item in InternalDatabase.fullDatabase.itens)
        {
            if (item.Serial == serialToConsult)
            {
                return item;
            }
        }
        return null;
    }

    /// <summary>
    /// Consult if the item exists on the database using the "Patrimônio"
    /// </summary>
    private SheetColumns ConsultPatrimonio(string patrimonioToConsult)
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
    private void Consult(string[] searchParamentersNames, string[] searchParametersValues)
    {

    }

    /// <summary>
    /// 0 = Categoria, 1 = Patrimônio, 2 = Serial
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
                inputField.placeholder.GetComponent<TextMeshProUGUI>().text = "Patrimônio";
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
}