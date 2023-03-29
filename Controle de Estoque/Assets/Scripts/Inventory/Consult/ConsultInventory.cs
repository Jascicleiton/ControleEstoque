using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

[RequireComponent(typeof(ConsultCategory))]
public class ConsultInventory : MonoBehaviour
{
    [SerializeField] TMP_Dropdown searchOptionDP; // drop down used to choose search option
    [SerializeField] TMP_Dropdown categoryDP; // drop down used to search for an item category
    //[SerializeField] TMP_Dropdown locationDP;
    [SerializeField] TMP_InputField inputField; // field use to type the item "Patrimônio" or the item "Serial"
    [SerializeField] CanvasGroup numberOfItemsImage;
    [SerializeField] TMP_Text numberOfItensFoundText; // show how many itens were found on the search

    [SerializeField] private GameObject consultResult;
    [SerializeField] private Transform consultResultTransform;

    [SerializeField] private GameObject categorySearchParametersPanel;
    [SerializeField] private List<TMP_InputField> categorySearchInputs;
    [SerializeField] private TMP_Dropdown[] operators;

    private ConsultCategory consultCategory = null;
    private bool inputEnabled = true;
    private TMP_InputField locationInput;
    private int tempInt = 0; // used for all int.TryParse

    /// <summary>
    /// get the ConsultCategory component
    /// </summary>
    private void Start()
    {
        consultCategory = GetComponent<ConsultCategory>();
        // InventarioManager.Instance.ImportSheets();
        //locationInput = inputField;
        //locationDP.value = HelperMethods.GetLocationDPValue("Estoque");
    }

    private void OnEnable()
    {
        EventHandler.EnableInput += SetInputEnabled;
    }

    private void OnDisable()
    {
        EventHandler.EnableInput -= SetInputEnabled;
    }

    /// <summary>
    /// Handles what happens if Enter is pressed
    /// </summary>
    private void Update()
    {
        if (inputEnabled)
        {
            if (inputField.IsActive())
            {
                if (Input.GetKeyDown(KeyCode.Return) || Input.GetKeyDown(KeyCode.KeypadEnter))
                {
                    if (searchOptionDP.value == 1)
                    {
                        if (int.TryParse(inputField.text, out tempInt))
                        {
                            if (ConsultDatabase.Instance.ConsultPatrimonio(tempInt, InternalDatabase.Instance.fullDatabase) != null)
                            {
                                RemoveOldSearch();
                                GameObject result = Instantiate(consultResult, consultResultTransform);
                                result.GetComponent<ConsultResult>().ShowResult(ConsultDatabase.Instance.ConsultPatrimonio(tempInt, InternalDatabase.Instance.fullDatabase), 0);
                            }
                            else
                            {
                                RemoveOldSearch();
                            }
                        }
                    }
                    else if (searchOptionDP.value == 2)
                    {
                        if (ConsultDatabase.Instance.ConsultSerial(inputField.text, InternalDatabase.Instance.fullDatabase) != null)
                        {
                            RemoveOldSearch();
                            GameObject result = Instantiate(consultResult, consultResultTransform);
                            result.GetComponent<ConsultResult>().ShowResult(ConsultDatabase.Instance.ConsultSerial(inputField.text, InternalDatabase.Instance.fullDatabase), 1);
                        }
                        else
                        {
                            RemoveOldSearch();
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
    }

    /// <summary>
    /// Enables or disables input. Called by Event EnableInput
    /// </summary>
    private void SetInputEnabled(bool enableInput)
    {
        inputEnabled = enableInput;
    }

    /// <summary>
    /// Hides ItensFoundText and disables all instances of ConsultResult
    /// </summary>
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
        for (int i = 0; i < categorySearchInputs.Count; i++)
        {
            if (categorySearchInputs[i].IsActive())
            {
                categorySearchInputs[i].text = "";
            }
        }
    }

    /// <summary>
    /// If true, sets the text invisible, if false set it to be visible
    /// </summary>
    private void SetItensFoundText(bool isInvisible)
    {
        if (isInvisible)
        {
            numberOfItemsImage.alpha = 0f;
        }
        else
        {
            numberOfItemsImage.alpha = 1f;
            switch (searchOptionDP.value)
            {
                case 1:
                    if (int.TryParse(inputField.text, out tempInt))
                        {
                        if (ConsultDatabase.Instance.ConsultPatrimonio(tempInt, InternalDatabase.Instance.fullDatabase) == null)
                        {
                            numberOfItensFoundText.text = "Patrimônio não encontrado";
                        }
                        else
                        {
                            numberOfItensFoundText.text = "1 item encontrado";
                        }
                    }
                    break;
                case 2:
                    if (ConsultDatabase.Instance.ConsultSerial(inputField.text, InternalDatabase.Instance.fullDatabase) == null)
                    {
                        numberOfItensFoundText.text = "Serial não encontrado";
                    }
                    else
                    {
                        numberOfItensFoundText.text = "1 item encontrado";
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
        Sheet foundItens = new Sheet();
        List<int> activeIndexes = new List<int>();
        List<string> activeOperators = new List<string>();

        for (int i = 0; i < categorySearchInputs.Count; i++)
        {
            if (categorySearchInputs[i].IsActive())
            {
                if (categorySearchInputs[i].text != "")
                {
                    activeIndexes.Add(i);
                    activeOperators.Add(GetOperatorFromDP(i));
                }
            }
        }
        // GetLocation(activeIndexes, activeOperators);

        if (activeIndexes.Count > 0)
        {
            //print(activeIndexes.Count);          
            foundItens = consultCategory.FindItens(activeIndexes, categorySearchInputs.ToArray(), GetCategorySheet(categoryDP.value), activeOperators);
        }
        RemoveOldSearch();
        if (foundItens != null)
        {
            if (foundItens.itens.Count > 0)
            {
                for (int i = 0; i < foundItens.itens.Count; i++)
                {
                    if (foundItens.itens[i].Status != "DEFEITO")
                    {
                        GameObject result = PoolManager.Instance.ReuseObject(consultResult);
                        result.SetActive(true);
                        result.GetComponent<ConsultResult>().ShowResult(foundItens.itens[i], 0);
                    }
                    else
                    {
                        foundItens.itens.Remove(foundItens.itens[i]);
                    }
                }
                         numberOfItemsImage.alpha = 1f;
                numberOfItensFoundText.text = foundItens.itens.Count.ToString() + " itens encontrados";
            }
            else
            {
                numberOfItemsImage.alpha = 1f;
                numberOfItensFoundText.text = foundItens.itens.Count.ToString() + " itens encontrados";
            }
        }
        else
        {
            numberOfItemsImage.alpha = 1f;
            numberOfItensFoundText.text = foundItens.itens.Count.ToString() + " itens encontrados";
        }

    }

    /// <summary>
    /// Add a location string to the search parameters based on the Location dropdown value
    /// </summary>
    private void GetLocation(List<int> activeIndexes, List<string> activeOperators)
    {
        // locationInput.text = HelperMethods.GetLocationFromDP(locationDP.value);
        if (categorySearchInputs[0].text != "" && categorySearchInputs[1].text != "")
        {
            categorySearchInputs.Insert(2, locationInput);
            activeIndexes.Insert(2, 2);
            activeOperators.Insert(2, "=");
        }
        else if ((categorySearchInputs[0].text != "" && categorySearchInputs[1].text == "") || (categorySearchInputs[0].text == "" && categorySearchInputs[1].text != ""))
        {
            categorySearchInputs.Insert(1, locationInput);
            activeIndexes.Insert(1, 1);
            activeOperators.Insert(1, "=");
        }
        else
        {
            categorySearchInputs.Insert(0, locationInput);
            activeIndexes.Insert(0, 0);
            activeOperators.Insert(0, "=");
        }


    }

    /// <summary>
    /// Get the string operator from the array of all operators
    /// </summary>
    private string GetOperatorFromDP(int index)
    {
        return operators[index].options[operators[index].value].text;
    }

    /// <summary>
    /// Get the correct sheet base on the category selected, to guarantee the search only happens for the specific category.
    /// </summary>
    private Sheet GetCategorySheet(int value)
    {
        return InternalDatabase.allFullDetailsSheets[value];
    }

    /// <summary>
    /// Handles what happens when the "procurar por" dropdown changes the value
    /// 0 = Categoria, 1 = Patrimônio, 2 = Serial
    /// </summary>
    public void HandleInputData(int value)
    {
        switch (value)
        {
            case 0:
                categoryDP.gameObject.SetActive(true);
                categorySearchParametersPanel.SetActive(true);
                //locationDP.value = HelperMethods.GetLocationDPValue("Estoque");
                inputField.gameObject.SetActive(false);
                break;
            case 1:
                categoryDP.gameObject.SetActive(false);
                categorySearchParametersPanel.SetActive(false);
                inputField.gameObject.SetActive(true);
                inputField.placeholder.GetComponent<TextMeshProUGUI>().text = "Patrimônio";
                numberOfItemsImage.alpha = 0f;

                break;
            case 2:
                categoryDP.gameObject.SetActive(false);
                categorySearchParametersPanel.SetActive(false);
                inputField.gameObject.SetActive(true);
                inputField.placeholder.GetComponent<TextMeshProUGUI>().text = "Serial";
                numberOfItemsImage.alpha = 0f;

                break;
            default:
                break;
        }
    }

    /// <summary>
    /// Returns to InitialScene
    /// </summary>
    public void ReturnToPreviousScreen()
    {
        SceneManager.LoadScene(ConstStrings.SceneInitial);
    }
}