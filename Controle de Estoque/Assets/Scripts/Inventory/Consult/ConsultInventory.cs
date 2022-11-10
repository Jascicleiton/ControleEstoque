using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ConsultInventory : MonoBehaviour
{
    [SerializeField] TMP_Dropdown searchOptionDP; // drop down used to choose search option
    [SerializeField] TMP_Dropdown categoryDP; // drop down used to search for an item category
    [SerializeField] TMP_InputField inputField; // field use to type the item "Patrimônio" or the item "Serial"
    [SerializeField] TMP_Text numberOfItensFoundText; // show how many itens were found on the search

    [SerializeField] private GameObject consultResult;
    [SerializeField] private Transform consultResultTransform;

    [SerializeField] private GameObject caterySearchParametersPanel;
    [SerializeField] private TMP_InputField[] categorySearchInputs;

    private ConsultCategory consultCategory = null;

    private void Start()
    {
        consultCategory = GetComponent<ConsultCategory>();
    }

    private void Update()
    {
        if(inputField.IsActive())
        {
            if(Input.GetKeyDown(KeyCode.Return) || Input.GetKeyDown(KeyCode.KeypadEnter))
            {               
                if (searchOptionDP.value == 1)
                {
                   if(ConsultDatabase.Instance.ConsultPatrimonio(inputField.text) != null)
                    {
                        RemoveOldSearch();
                        GameObject result =  Instantiate(consultResult, consultResultTransform);
                        result.GetComponent<ConsultResult>().ShowResult(ConsultDatabase.Instance.ConsultPatrimonio(inputField.text), 0);
                    }
                    else
                    {
                        SetItensFoundText(false);
                    }
                }
                else if (searchOptionDP.value == 2)
                {
                    if (ConsultDatabase.Instance.ConsultSerial(inputField.text) != null)
                    {
                        RemoveOldSearch();
                        GameObject result = Instantiate(consultResult, consultResultTransform);
                        result.GetComponent<ConsultResult>().ShowResult(ConsultDatabase.Instance.ConsultSerial(inputField.text), 1);
                    }
                    else
                    {
                        SetItensFoundText(false);
                    }
                }
            }
        }
        if (categoryDP.IsActive())
        {
            if (Input.GetKeyDown(KeyCode.Return) || Input.GetKeyDown(KeyCode.KeypadEnter))
            {
                ConsultWithCategory();
            }
        }
    }

    private void RemoveOldSearch()
    {
        SetItensFoundText(false);
        if (consultResultTransform.childCount > 0)
        {
            for (int i = 0; i < consultResultTransform.childCount; i++)
            {
                consultResultTransform.GetChild(i).gameObject.SetActive(false);
            }
        }
    }

    private void SetItensFoundText(bool isInvisible)
    {
        if (isInvisible)
        {
            numberOfItensFoundText.color = new Color32(255, 255, 255, 0);
        }
        else
        {
            numberOfItensFoundText.color = new Color32(255, 255, 255, 255);
            switch (searchOptionDP.value)
            {
                case 0:

                        break;
                case 1:
                    if(ConsultDatabase.Instance.ConsultPatrimonio(inputField.text) == null)
                    {
                        numberOfItensFoundText.text = "Patrimônio não encontrado";
                    }
                
                    break;
                case 2:
                    if (ConsultDatabase.Instance.ConsultPatrimonio(inputField.text) == null)
                    {
                        numberOfItensFoundText.text = "Serial não encontrado";
                    }
                    
                    break;
                default:
                    break;
            }
        }
    }

    /// <summary>
    /// Consult the inventory using the parameters chosen from each category
    /// </summary>
    private void ConsultWithCategory()
    {
        RemoveOldSearch();
        Sheet foundItens = new Sheet();
        List<int> activeIndexes = new List<int>();
        
        for (int i = 0; i < categorySearchInputs.Length; i++)
        {
            if (categorySearchInputs[i].IsActive())
            {
                if (categorySearchInputs[i].text != "")
                {
                    activeIndexes.Add(i);
                }
            }
        }

        if (activeIndexes.Count > 0)
        {
            
            foundItens = consultCategory.FindItens(activeIndexes, categorySearchInputs, GetCategory(categoryDP.value));
        }

        if (foundItens != null)
        {
            if (foundItens.itens.Count > 0)
            {
                foreach (SheetColumns foundItem in foundItens.itens)
                {
                    GameObject result = PoolManager.Instance.ReuseObject(consultResult);
                    result.SetActive(true);
                    result.GetComponent<ConsultResult>().ShowResult(foundItem, 0);
                }
                numberOfItensFoundText.color = new Color32(255, 255, 255, 255);
                numberOfItensFoundText.text = foundItens.itens.Count.ToString() + " itens encontrados";
            }
            else
            {
                numberOfItensFoundText.color = new Color32(255, 255, 255, 255);
                numberOfItensFoundText.text = foundItens.itens.Count.ToString() + " itens encontrados";
            }
        }
        else
        {
                numberOfItensFoundText.color = new Color32(255, 255, 255, 255);
                numberOfItensFoundText.text = foundItens.itens.Count.ToString() + " itens encontrados";
        }

    }

    /// <summary>
    /// Get the correct sheet base on the category selected, to guarantee the search only happens for the specific category.
    /// </summary>
    private Sheet GetCategory(int value)
    {
        switch (value)
        {
            case 0:
                return InternalDatabase.hd;
            case 1:
                return InternalDatabase.memoria;
            case 2:
                return InternalDatabase.placaDeRede;
            case 3:
                return InternalDatabase.idrac;
            case 4:
                return InternalDatabase.placaControladora;
            case 5:
                return InternalDatabase.processador;
            case 6:
                return InternalDatabase.desktop;
            case 7:
                return InternalDatabase.fonte;
            case 8:
                return InternalDatabase.Switch;
            case 9:
                return InternalDatabase.roteador;
            case 10:
                return InternalDatabase.carregador;
            case 11:
                return InternalDatabase.adaptadorAC;
            case 12:
                return InternalDatabase.storageNAS;
            case 13:
                return InternalDatabase.gbic;
            case 14:
                return InternalDatabase.placaDeVideo;
            case 15:
                return InternalDatabase.placaDeSom;
            case 16:
                return InternalDatabase.placaControladora;
            case 17:
            case 18:
            case 19:
            case 20:
            default:
                return null;              
        }
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
                caterySearchParametersPanel.SetActive(true);
                inputField.gameObject.SetActive(false);
                break;
            case 1:
                categoryDP.gameObject.SetActive(false);
                caterySearchParametersPanel.SetActive(false);
                inputField.gameObject.SetActive(true);
                inputField.placeholder.GetComponent<TextMeshProUGUI>().text = "Patrimônio";
                numberOfItensFoundText.color = new Color32(255, 255, 255, 0);
                break;
            case 2:
                categoryDP.gameObject.SetActive(false);
                caterySearchParametersPanel.SetActive(false);
                inputField.gameObject.SetActive(true);
                inputField.placeholder.GetComponent<TextMeshProUGUI>().text = "Serial";
                numberOfItensFoundText.color = new Color32(255, 255, 255, 0);
                break;
            default:
                break;
        }
    }
}