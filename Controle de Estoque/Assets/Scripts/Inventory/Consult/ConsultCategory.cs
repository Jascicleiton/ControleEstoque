using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class ConsultCategory : MonoBehaviour
{
    /// <summary>
    /// Get the correct sheet base on the category selected, to guarantee the search only happens for the specific category.
    /// </summary>
    private Sheet GetCategorySheet(int value)
    {
        return HelperMethods.GetCategoryDatabaseToConsult(HelperMethods.GetCategoryString(value));
    }
    /// <summary>
    /// Find all itens from a specific category that match the search parameters
    /// </summary>
    public Sheet FindItens(List<int> activeIndexes, TMP_InputField[] categorySearchInputs, Sheet databaseToConsult, List<string> operators)
    {     
        Sheet returnSheet = new Sheet();
        returnSheet.itens.Clear();
        
        switch (activeIndexes.Count)
        {
            case 1:           
                foreach (ItemColumns item in databaseToConsult.itens)
                {        
                        if (HelperMethods.CompareStrings(ConsultCategoryHelperMethods.GetItemValue(item, activeIndexes[0]), categorySearchInputs[activeIndexes[0]].text, operators[0]))
                        {
                            if (!returnSheet.itens.Contains(item))
                            {
                                returnSheet.itens.Add(item);
                            }
                        }
                }
                break;
            case 2:
                foreach (ItemColumns item in databaseToConsult.itens)
                {
                    if (HelperMethods.CompareStrings(ConsultCategoryHelperMethods.GetItemValue(item, activeIndexes[0]), categorySearchInputs[activeIndexes[0]].text, operators[0]) &&
                        HelperMethods.CompareStrings(ConsultCategoryHelperMethods.GetItemValue(item, activeIndexes[1]), categorySearchInputs[activeIndexes[1]].text, operators[1]))
                    {
                        if (!returnSheet.itens.Contains(item))
                        {
                            returnSheet.itens.Add(item);
                        }
                    }
                }
                break;
            case 3:
                foreach (ItemColumns item in databaseToConsult.itens)
                {
                    if (HelperMethods.CompareStrings(ConsultCategoryHelperMethods.GetItemValue(item, activeIndexes[0]), categorySearchInputs[activeIndexes[0]].text, operators[0]) &&
                        HelperMethods.CompareStrings(ConsultCategoryHelperMethods.GetItemValue(item, activeIndexes[1]), categorySearchInputs[activeIndexes[1]].text, operators[1]) &&
                        HelperMethods.CompareStrings(ConsultCategoryHelperMethods.GetItemValue(item, activeIndexes[2]), categorySearchInputs[activeIndexes[2]].text, operators[2]))
                    {
                        if (!returnSheet.itens.Contains(item))
                        {
                            returnSheet.itens.Add(item);
                        }
                    }
                }
                break;
            case 4:
                foreach (ItemColumns item in databaseToConsult.itens)
                {
                    if (HelperMethods.CompareStrings(ConsultCategoryHelperMethods.GetItemValue(item, activeIndexes[0]), categorySearchInputs[activeIndexes[0]].text, operators[0]) &&
                        HelperMethods.CompareStrings(ConsultCategoryHelperMethods.GetItemValue(item, activeIndexes[1]), categorySearchInputs[activeIndexes[1]].text, operators[1]) &&
                        HelperMethods.CompareStrings(ConsultCategoryHelperMethods.GetItemValue(item, activeIndexes[2]), categorySearchInputs[activeIndexes[2]].text, operators[2]) &&
                        HelperMethods.CompareStrings(ConsultCategoryHelperMethods.GetItemValue(item, activeIndexes[3]), categorySearchInputs[activeIndexes[3]].text, operators[3]))
                    {
                        if (!returnSheet.itens.Contains(item))
                        {
                            returnSheet.itens.Add(item);
                        }
                    }
                }
                break;
            case 5:
                foreach (ItemColumns item in databaseToConsult.itens)
                {
                    if (HelperMethods.CompareStrings(ConsultCategoryHelperMethods.GetItemValue(item, activeIndexes[0]), categorySearchInputs[activeIndexes[0]].text, operators[0]) &&
                        HelperMethods.CompareStrings(ConsultCategoryHelperMethods.GetItemValue(item, activeIndexes[1]), categorySearchInputs[activeIndexes[1]].text, operators[1]) &&
                        HelperMethods.CompareStrings(ConsultCategoryHelperMethods.GetItemValue(item, activeIndexes[2]), categorySearchInputs[activeIndexes[2]].text, operators[2]) &&
                        HelperMethods.CompareStrings(ConsultCategoryHelperMethods.GetItemValue(item, activeIndexes[3]), categorySearchInputs[activeIndexes[3]].text, operators[3]) &&
                        HelperMethods.CompareStrings(ConsultCategoryHelperMethods.GetItemValue(item, activeIndexes[4]), categorySearchInputs[activeIndexes[4]].text, operators[4]))
                    {
                        if (!returnSheet.itens.Contains(item))
                        {
                            returnSheet.itens.Add(item);
                        }
                    }
                }
                break;
            case 6:
                foreach (ItemColumns item in databaseToConsult.itens)
                {
                    if (HelperMethods.CompareStrings(ConsultCategoryHelperMethods.GetItemValue(item, activeIndexes[0]), categorySearchInputs[activeIndexes[0]].text, operators[0]) &&
                        HelperMethods.CompareStrings(ConsultCategoryHelperMethods.GetItemValue(item, activeIndexes[1]), categorySearchInputs[activeIndexes[1]].text, operators[1]) &&
                        HelperMethods.CompareStrings(ConsultCategoryHelperMethods.GetItemValue(item, activeIndexes[2]), categorySearchInputs[activeIndexes[2]].text, operators[2]) &&
                        HelperMethods.CompareStrings(ConsultCategoryHelperMethods.GetItemValue(item, activeIndexes[3]), categorySearchInputs[activeIndexes[3]].text, operators[3]) &&
                        HelperMethods.CompareStrings(ConsultCategoryHelperMethods.GetItemValue(item, activeIndexes[4]), categorySearchInputs[activeIndexes[4]].text, operators[4]) &&
                        HelperMethods.CompareStrings(ConsultCategoryHelperMethods.GetItemValue(item, activeIndexes[5]), categorySearchInputs[activeIndexes[5]].text, operators[5]))
                    {
                        if (!returnSheet.itens.Contains(item))
                        {
                            returnSheet.itens.Add(item);
                        }
                    }
                }
                break;
            case 7:
                foreach (ItemColumns item in databaseToConsult.itens)
                {
                    if (HelperMethods.CompareStrings(ConsultCategoryHelperMethods.GetItemValue(item, activeIndexes[0]), categorySearchInputs[activeIndexes[0]].text, operators[0]) &&
                        HelperMethods.CompareStrings(ConsultCategoryHelperMethods.GetItemValue(item, activeIndexes[1]), categorySearchInputs[activeIndexes[1]].text, operators[1]) &&
                        HelperMethods.CompareStrings(ConsultCategoryHelperMethods.GetItemValue(item, activeIndexes[2]), categorySearchInputs[activeIndexes[2]].text, operators[2]) &&
                        HelperMethods.CompareStrings(ConsultCategoryHelperMethods.GetItemValue(item, activeIndexes[3]), categorySearchInputs[activeIndexes[3]].text, operators[3]) &&
                        HelperMethods.CompareStrings(ConsultCategoryHelperMethods.GetItemValue(item, activeIndexes[4]), categorySearchInputs[activeIndexes[4]].text, operators[4]) &&
                        HelperMethods.CompareStrings(ConsultCategoryHelperMethods.GetItemValue(item, activeIndexes[5]), categorySearchInputs[activeIndexes[5]].text, operators[5]) &&
                        HelperMethods.CompareStrings(ConsultCategoryHelperMethods.GetItemValue(item, activeIndexes[6]), categorySearchInputs[activeIndexes[6]].text, operators[6]))
                    {
                        if (!returnSheet.itens.Contains(item))
                        {
                            returnSheet.itens.Add(item);
                        }
                    }
                }
                break;
            case 8:
                foreach (ItemColumns item in databaseToConsult.itens)
                {
                    if (HelperMethods.CompareStrings(ConsultCategoryHelperMethods.GetItemValue(item, activeIndexes[0]), categorySearchInputs[activeIndexes[0]].text, operators[0]) &&
                        HelperMethods.CompareStrings(ConsultCategoryHelperMethods.GetItemValue(item, activeIndexes[1]), categorySearchInputs[activeIndexes[1]].text, operators[1]) &&
                        HelperMethods.CompareStrings(ConsultCategoryHelperMethods.GetItemValue(item, activeIndexes[2]), categorySearchInputs[activeIndexes[2]].text, operators[2]) &&
                        HelperMethods.CompareStrings(ConsultCategoryHelperMethods.GetItemValue(item, activeIndexes[3]), categorySearchInputs[activeIndexes[3]].text, operators[3]) &&
                        HelperMethods.CompareStrings(ConsultCategoryHelperMethods.GetItemValue(item, activeIndexes[4]), categorySearchInputs[activeIndexes[4]].text, operators[4]) &&
                        HelperMethods.CompareStrings(ConsultCategoryHelperMethods.GetItemValue(item, activeIndexes[5]), categorySearchInputs[activeIndexes[5]].text, operators[5]) &&
                        HelperMethods.CompareStrings(ConsultCategoryHelperMethods.GetItemValue(item, activeIndexes[6]), categorySearchInputs[activeIndexes[6]].text, operators[6]) &&
                        HelperMethods.CompareStrings(ConsultCategoryHelperMethods.GetItemValue(item, activeIndexes[7]), categorySearchInputs[activeIndexes[7]].text, operators[7]))
                    {
                        if (!returnSheet.itens.Contains(item))
                        {
                            returnSheet.itens.Add(item);
                        }
                    }
                }
                break;
            case 9:
                foreach (ItemColumns item in databaseToConsult.itens)
                {
                    if (HelperMethods.CompareStrings(ConsultCategoryHelperMethods.GetItemValue(item, activeIndexes[0]), categorySearchInputs[activeIndexes[0]].text, operators[0]) &&
                        HelperMethods.CompareStrings(ConsultCategoryHelperMethods.GetItemValue(item, activeIndexes[1]), categorySearchInputs[activeIndexes[1]].text, operators[1]) &&
                        HelperMethods.CompareStrings(ConsultCategoryHelperMethods.GetItemValue(item, activeIndexes[2]), categorySearchInputs[activeIndexes[2]].text, operators[2]) &&
                        HelperMethods.CompareStrings(ConsultCategoryHelperMethods.GetItemValue(item, activeIndexes[3]), categorySearchInputs[activeIndexes[3]].text, operators[3]) &&
                        HelperMethods.CompareStrings(ConsultCategoryHelperMethods.GetItemValue(item, activeIndexes[4]), categorySearchInputs[activeIndexes[4]].text, operators[4]) &&
                        HelperMethods.CompareStrings(ConsultCategoryHelperMethods.GetItemValue(item, activeIndexes[5]), categorySearchInputs[activeIndexes[5]].text, operators[5]) &&
                        HelperMethods.CompareStrings(ConsultCategoryHelperMethods.GetItemValue(item, activeIndexes[6]), categorySearchInputs[activeIndexes[6]].text, operators[6]) &&
                        HelperMethods.CompareStrings(ConsultCategoryHelperMethods.GetItemValue(item, activeIndexes[7]), categorySearchInputs[activeIndexes[7]].text, operators[7]) &&
                        HelperMethods.CompareStrings(ConsultCategoryHelperMethods.GetItemValue(item, activeIndexes[8]), categorySearchInputs[activeIndexes[8]].text, operators[8]))
                    {
                        if (!returnSheet.itens.Contains(item))
                        {
                            returnSheet.itens.Add(item);
                        }
                    }
                }
                break;
            case 10:
                foreach (ItemColumns item in databaseToConsult.itens)
                {
                    if (HelperMethods.CompareStrings(ConsultCategoryHelperMethods.GetItemValue(item, activeIndexes[0]), categorySearchInputs[activeIndexes[0]].text, operators[0]) &&
                        HelperMethods.CompareStrings(ConsultCategoryHelperMethods.GetItemValue(item, activeIndexes[1]), categorySearchInputs[activeIndexes[1]].text, operators[1]) &&
                        HelperMethods.CompareStrings(ConsultCategoryHelperMethods.GetItemValue(item, activeIndexes[2]), categorySearchInputs[activeIndexes[2]].text, operators[2]) &&
                        HelperMethods.CompareStrings(ConsultCategoryHelperMethods.GetItemValue(item, activeIndexes[3]), categorySearchInputs[activeIndexes[3]].text, operators[3]) &&
                        HelperMethods.CompareStrings(ConsultCategoryHelperMethods.GetItemValue(item, activeIndexes[4]), categorySearchInputs[activeIndexes[4]].text, operators[4]) &&
                        HelperMethods.CompareStrings(ConsultCategoryHelperMethods.GetItemValue(item, activeIndexes[5]), categorySearchInputs[activeIndexes[5]].text, operators[5]) &&
                        HelperMethods.CompareStrings(ConsultCategoryHelperMethods.GetItemValue(item, activeIndexes[6]), categorySearchInputs[activeIndexes[6]].text, operators[6]) &&
                        HelperMethods.CompareStrings(ConsultCategoryHelperMethods.GetItemValue(item, activeIndexes[7]), categorySearchInputs[activeIndexes[7]].text, operators[7]) &&
                        HelperMethods.CompareStrings(ConsultCategoryHelperMethods.GetItemValue(item, activeIndexes[8]), categorySearchInputs[activeIndexes[8]].text, operators[8]) &&
                        HelperMethods.CompareStrings(ConsultCategoryHelperMethods.GetItemValue(item, activeIndexes[9]), categorySearchInputs[activeIndexes[9]].text, operators[9]))
                    {
                        if (!returnSheet.itens.Contains(item))
                        {
                            returnSheet.itens.Add(item);
                        }
                    }
                }
                break;
            case 11:
                foreach (ItemColumns item in databaseToConsult.itens)
                {
                    if (HelperMethods.CompareStrings(ConsultCategoryHelperMethods.GetItemValue(item, activeIndexes[0]), categorySearchInputs[activeIndexes[0]].text, operators[0]) &&
                        HelperMethods.CompareStrings(ConsultCategoryHelperMethods.GetItemValue(item, activeIndexes[1]), categorySearchInputs[activeIndexes[1]].text, operators[1]) &&
                        HelperMethods.CompareStrings(ConsultCategoryHelperMethods.GetItemValue(item, activeIndexes[2]), categorySearchInputs[activeIndexes[2]].text, operators[2]) &&
                        HelperMethods.CompareStrings(ConsultCategoryHelperMethods.GetItemValue(item, activeIndexes[3]), categorySearchInputs[activeIndexes[3]].text, operators[3]) &&
                        HelperMethods.CompareStrings(ConsultCategoryHelperMethods.GetItemValue(item, activeIndexes[4]), categorySearchInputs[activeIndexes[4]].text, operators[4]) &&
                        HelperMethods.CompareStrings(ConsultCategoryHelperMethods.GetItemValue(item, activeIndexes[5]), categorySearchInputs[activeIndexes[5]].text, operators[5]) &&
                        HelperMethods.CompareStrings(ConsultCategoryHelperMethods.GetItemValue(item, activeIndexes[6]), categorySearchInputs[activeIndexes[6]].text, operators[6]) &&
                        HelperMethods.CompareStrings(ConsultCategoryHelperMethods.GetItemValue(item, activeIndexes[7]), categorySearchInputs[activeIndexes[7]].text, operators[7]) &&
                        HelperMethods.CompareStrings(ConsultCategoryHelperMethods.GetItemValue(item, activeIndexes[8]), categorySearchInputs[activeIndexes[8]].text, operators[8]) &&
                        HelperMethods.CompareStrings(ConsultCategoryHelperMethods.GetItemValue(item, activeIndexes[9]), categorySearchInputs[activeIndexes[9]].text, operators[9]) &&
                        HelperMethods.CompareStrings(ConsultCategoryHelperMethods.GetItemValue(item, activeIndexes[10]), categorySearchInputs[activeIndexes[10]].text, operators[10]))
                    {
                        if (!returnSheet.itens.Contains(item))
                        {
                            returnSheet.itens.Add(item);
                        }
                    }
                }
                break;
            case 12:
                foreach (ItemColumns item in databaseToConsult.itens)
                {
                    if (HelperMethods.CompareStrings(ConsultCategoryHelperMethods.GetItemValue(item, activeIndexes[0]), categorySearchInputs[activeIndexes[0]].text, operators[0]) &&
                        HelperMethods.CompareStrings(ConsultCategoryHelperMethods.GetItemValue(item, activeIndexes[1]), categorySearchInputs[activeIndexes[1]].text, operators[1]) &&
                        HelperMethods.CompareStrings(ConsultCategoryHelperMethods.GetItemValue(item, activeIndexes[2]), categorySearchInputs[activeIndexes[2]].text, operators[2]) &&
                        HelperMethods.CompareStrings(ConsultCategoryHelperMethods.GetItemValue(item, activeIndexes[3]), categorySearchInputs[activeIndexes[3]].text, operators[3]) &&
                        HelperMethods.CompareStrings(ConsultCategoryHelperMethods.GetItemValue(item, activeIndexes[4]), categorySearchInputs[activeIndexes[4]].text, operators[4]) &&
                        HelperMethods.CompareStrings(ConsultCategoryHelperMethods.GetItemValue(item, activeIndexes[5]), categorySearchInputs[activeIndexes[5]].text, operators[5]) &&
                        HelperMethods.CompareStrings(ConsultCategoryHelperMethods.GetItemValue(item, activeIndexes[6]), categorySearchInputs[activeIndexes[6]].text, operators[6]) &&
                        HelperMethods.CompareStrings(ConsultCategoryHelperMethods.GetItemValue(item, activeIndexes[7]), categorySearchInputs[activeIndexes[7]].text, operators[7]) &&
                        HelperMethods.CompareStrings(ConsultCategoryHelperMethods.GetItemValue(item, activeIndexes[8]), categorySearchInputs[activeIndexes[8]].text, operators[8]) &&
                        HelperMethods.CompareStrings(ConsultCategoryHelperMethods.GetItemValue(item, activeIndexes[9]), categorySearchInputs[activeIndexes[9]].text, operators[9]) &&
                        HelperMethods.CompareStrings(ConsultCategoryHelperMethods.GetItemValue(item, activeIndexes[10]), categorySearchInputs[activeIndexes[10]].text, operators[10]) &&
                        HelperMethods.CompareStrings(ConsultCategoryHelperMethods.GetItemValue(item, activeIndexes[11]), categorySearchInputs[activeIndexes[11]].text, operators[11]))
                    {
                        if (!returnSheet.itens.Contains(item))
                        {
                            returnSheet.itens.Add(item);
                        }
                    }
                }
                break;
            case 13:
                foreach (ItemColumns item in databaseToConsult.itens)
                {
                    if (HelperMethods.CompareStrings(ConsultCategoryHelperMethods.GetItemValue(item, activeIndexes[0]), categorySearchInputs[activeIndexes[0]].text, operators[0]) &&
                        HelperMethods.CompareStrings(ConsultCategoryHelperMethods.GetItemValue(item, activeIndexes[1]), categorySearchInputs[activeIndexes[1]].text, operators[1]) &&
                        HelperMethods.CompareStrings(ConsultCategoryHelperMethods.GetItemValue(item, activeIndexes[2]), categorySearchInputs[activeIndexes[2]].text, operators[2]) &&
                        HelperMethods.CompareStrings(ConsultCategoryHelperMethods.GetItemValue(item, activeIndexes[3]), categorySearchInputs[activeIndexes[3]].text, operators[3]) &&
                        HelperMethods.CompareStrings(ConsultCategoryHelperMethods.GetItemValue(item, activeIndexes[4]), categorySearchInputs[activeIndexes[4]].text, operators[4]) &&
                        HelperMethods.CompareStrings(ConsultCategoryHelperMethods.GetItemValue(item, activeIndexes[5]), categorySearchInputs[activeIndexes[5]].text, operators[5]) &&
                        HelperMethods.CompareStrings(ConsultCategoryHelperMethods.GetItemValue(item, activeIndexes[6]), categorySearchInputs[activeIndexes[6]].text, operators[6]) &&
                        HelperMethods.CompareStrings(ConsultCategoryHelperMethods.GetItemValue(item, activeIndexes[7]), categorySearchInputs[activeIndexes[7]].text, operators[7]) &&
                        HelperMethods.CompareStrings(ConsultCategoryHelperMethods.GetItemValue(item, activeIndexes[8]), categorySearchInputs[activeIndexes[8]].text, operators[8]) &&
                        HelperMethods.CompareStrings(ConsultCategoryHelperMethods.GetItemValue(item, activeIndexes[9]), categorySearchInputs[activeIndexes[9]].text, operators[9]) &&
                        HelperMethods.CompareStrings(ConsultCategoryHelperMethods.GetItemValue(item, activeIndexes[10]), categorySearchInputs[activeIndexes[10]].text, operators[10]) &&
                        HelperMethods.CompareStrings(ConsultCategoryHelperMethods.GetItemValue(item, activeIndexes[11]), categorySearchInputs[activeIndexes[11]].text, operators[11]) &&
                        HelperMethods.CompareStrings(ConsultCategoryHelperMethods.GetItemValue(item, activeIndexes[12]), categorySearchInputs[activeIndexes[12]].text, operators[12]))
                    {
                        if (!returnSheet.itens.Contains(item))
                        {
                            returnSheet.itens.Add(item);
                        }
                    }
                }
                break;
            case 14:
                foreach (ItemColumns item in databaseToConsult.itens)
                {
                    if (HelperMethods.CompareStrings(ConsultCategoryHelperMethods.GetItemValue(item, activeIndexes[0]), categorySearchInputs[activeIndexes[0]].text, operators[0]) &&
                        HelperMethods.CompareStrings(ConsultCategoryHelperMethods.GetItemValue(item, activeIndexes[1]), categorySearchInputs[activeIndexes[1]].text, operators[1]) &&
                        HelperMethods.CompareStrings(ConsultCategoryHelperMethods.GetItemValue(item, activeIndexes[2]), categorySearchInputs[activeIndexes[2]].text, operators[2]) &&
                        HelperMethods.CompareStrings(ConsultCategoryHelperMethods.GetItemValue(item, activeIndexes[3]), categorySearchInputs[activeIndexes[3]].text, operators[3]) &&
                        HelperMethods.CompareStrings(ConsultCategoryHelperMethods.GetItemValue(item, activeIndexes[4]), categorySearchInputs[activeIndexes[4]].text, operators[4]) &&
                        HelperMethods.CompareStrings(ConsultCategoryHelperMethods.GetItemValue(item, activeIndexes[5]), categorySearchInputs[activeIndexes[5]].text, operators[5]) &&
                        HelperMethods.CompareStrings(ConsultCategoryHelperMethods.GetItemValue(item, activeIndexes[6]), categorySearchInputs[activeIndexes[6]].text, operators[6]) &&
                        HelperMethods.CompareStrings(ConsultCategoryHelperMethods.GetItemValue(item, activeIndexes[7]), categorySearchInputs[activeIndexes[7]].text, operators[7]) &&
                        HelperMethods.CompareStrings(ConsultCategoryHelperMethods.GetItemValue(item, activeIndexes[8]), categorySearchInputs[activeIndexes[8]].text, operators[8]) &&
                        HelperMethods.CompareStrings(ConsultCategoryHelperMethods.GetItemValue(item, activeIndexes[9]), categorySearchInputs[activeIndexes[9]].text, operators[9]) &&
                        HelperMethods.CompareStrings(ConsultCategoryHelperMethods.GetItemValue(item, activeIndexes[10]), categorySearchInputs[activeIndexes[10]].text, operators[10]) &&
                        HelperMethods.CompareStrings(ConsultCategoryHelperMethods.GetItemValue(item, activeIndexes[11]), categorySearchInputs[activeIndexes[11]].text, operators[11]) &&
                        HelperMethods.CompareStrings(ConsultCategoryHelperMethods.GetItemValue(item, activeIndexes[12]), categorySearchInputs[activeIndexes[12]].text, operators[12]) &&
                        HelperMethods.CompareStrings(ConsultCategoryHelperMethods.GetItemValue(item, activeIndexes[13]), categorySearchInputs[activeIndexes[13]].text, operators[13]))
                    {
                        if (!returnSheet.itens.Contains(item))
                        {
                            returnSheet.itens.Add(item);
                        }
                    }
                }
                break;
            case 15:
                foreach (ItemColumns item in databaseToConsult.itens)
                {
                    if (HelperMethods.CompareStrings(ConsultCategoryHelperMethods.GetItemValue(item, activeIndexes[0]), categorySearchInputs[activeIndexes[0]].text, operators[0]) &&
                        HelperMethods.CompareStrings(ConsultCategoryHelperMethods.GetItemValue(item, activeIndexes[1]), categorySearchInputs[activeIndexes[1]].text, operators[1]) &&
                        HelperMethods.CompareStrings(ConsultCategoryHelperMethods.GetItemValue(item, activeIndexes[2]), categorySearchInputs[activeIndexes[2]].text, operators[2]) &&
                        HelperMethods.CompareStrings(ConsultCategoryHelperMethods.GetItemValue(item, activeIndexes[3]), categorySearchInputs[activeIndexes[3]].text, operators[3]) &&
                        HelperMethods.CompareStrings(ConsultCategoryHelperMethods.GetItemValue(item, activeIndexes[4]), categorySearchInputs[activeIndexes[4]].text, operators[4]) &&
                        HelperMethods.CompareStrings(ConsultCategoryHelperMethods.GetItemValue(item, activeIndexes[5]), categorySearchInputs[activeIndexes[5]].text, operators[5]) &&
                        HelperMethods.CompareStrings(ConsultCategoryHelperMethods.GetItemValue(item, activeIndexes[6]), categorySearchInputs[activeIndexes[6]].text, operators[6]) &&
                        HelperMethods.CompareStrings(ConsultCategoryHelperMethods.GetItemValue(item, activeIndexes[7]), categorySearchInputs[activeIndexes[7]].text, operators[7]) &&
                        HelperMethods.CompareStrings(ConsultCategoryHelperMethods.GetItemValue(item, activeIndexes[8]), categorySearchInputs[activeIndexes[8]].text, operators[8]) &&
                        HelperMethods.CompareStrings(ConsultCategoryHelperMethods.GetItemValue(item, activeIndexes[9]), categorySearchInputs[activeIndexes[9]].text, operators[9]) &&
                        HelperMethods.CompareStrings(ConsultCategoryHelperMethods.GetItemValue(item, activeIndexes[10]), categorySearchInputs[activeIndexes[10]].text, operators[10]) &&
                        HelperMethods.CompareStrings(ConsultCategoryHelperMethods.GetItemValue(item, activeIndexes[11]), categorySearchInputs[activeIndexes[11]].text, operators[11]) &&
                        HelperMethods.CompareStrings(ConsultCategoryHelperMethods.GetItemValue(item, activeIndexes[12]), categorySearchInputs[activeIndexes[12]].text, operators[12]) &&
                        HelperMethods.CompareStrings(ConsultCategoryHelperMethods.GetItemValue(item, activeIndexes[13]), categorySearchInputs[activeIndexes[13]].text, operators[13]) &&
                        HelperMethods.CompareStrings(ConsultCategoryHelperMethods.GetItemValue(item, activeIndexes[14]), categorySearchInputs[activeIndexes[14]].text, operators[14]))
                    {
                        if (!returnSheet.itens.Contains(item))
                        {
                            returnSheet.itens.Add(item);
                        }
                    }
                }
                break;
            case 16:
                foreach (ItemColumns item in databaseToConsult.itens)
                {
                    if (HelperMethods.CompareStrings(ConsultCategoryHelperMethods.GetItemValue(item, activeIndexes[0]), categorySearchInputs[activeIndexes[0]].text, operators[0]) &&
                        HelperMethods.CompareStrings(ConsultCategoryHelperMethods.GetItemValue(item, activeIndexes[1]), categorySearchInputs[activeIndexes[1]].text, operators[1]) &&
                        HelperMethods.CompareStrings(ConsultCategoryHelperMethods.GetItemValue(item, activeIndexes[2]), categorySearchInputs[activeIndexes[2]].text, operators[2]) &&
                        HelperMethods.CompareStrings(ConsultCategoryHelperMethods.GetItemValue(item, activeIndexes[3]), categorySearchInputs[activeIndexes[3]].text, operators[3]) &&
                        HelperMethods.CompareStrings(ConsultCategoryHelperMethods.GetItemValue(item, activeIndexes[4]), categorySearchInputs[activeIndexes[4]].text, operators[4]) &&
                        HelperMethods.CompareStrings(ConsultCategoryHelperMethods.GetItemValue(item, activeIndexes[5]), categorySearchInputs[activeIndexes[5]].text, operators[5]) &&
                        HelperMethods.CompareStrings(ConsultCategoryHelperMethods.GetItemValue(item, activeIndexes[6]), categorySearchInputs[activeIndexes[6]].text, operators[6]) &&
                        HelperMethods.CompareStrings(ConsultCategoryHelperMethods.GetItemValue(item, activeIndexes[7]), categorySearchInputs[activeIndexes[7]].text, operators[7]) &&
                        HelperMethods.CompareStrings(ConsultCategoryHelperMethods.GetItemValue(item, activeIndexes[8]), categorySearchInputs[activeIndexes[8]].text, operators[8]) &&
                        HelperMethods.CompareStrings(ConsultCategoryHelperMethods.GetItemValue(item, activeIndexes[9]), categorySearchInputs[activeIndexes[9]].text, operators[9]) &&
                        HelperMethods.CompareStrings(ConsultCategoryHelperMethods.GetItemValue(item, activeIndexes[10]), categorySearchInputs[activeIndexes[10]].text, operators[10]) &&
                        HelperMethods.CompareStrings(ConsultCategoryHelperMethods.GetItemValue(item, activeIndexes[11]), categorySearchInputs[activeIndexes[11]].text, operators[11]) &&
                        HelperMethods.CompareStrings(ConsultCategoryHelperMethods.GetItemValue(item, activeIndexes[12]), categorySearchInputs[activeIndexes[12]].text, operators[12]) &&
                        HelperMethods.CompareStrings(ConsultCategoryHelperMethods.GetItemValue(item, activeIndexes[13]), categorySearchInputs[activeIndexes[13]].text, operators[13]) &&
                        HelperMethods.CompareStrings(ConsultCategoryHelperMethods.GetItemValue(item, activeIndexes[14]), categorySearchInputs[activeIndexes[14]].text, operators[14]) &&
                        HelperMethods.CompareStrings(ConsultCategoryHelperMethods.GetItemValue(item, activeIndexes[15]), categorySearchInputs[activeIndexes[15]].text, operators[15])) 
                    {
                        if (!returnSheet.itens.Contains(item))
                        {
                            returnSheet.itens.Add(item);
                        }
                    }
                }
                break;
            case 17:
                foreach (ItemColumns item in databaseToConsult.itens)
                {
                    if (HelperMethods.CompareStrings(ConsultCategoryHelperMethods.GetItemValue(item, activeIndexes[0]), categorySearchInputs[activeIndexes[0]].text, operators[0]) &&
                         HelperMethods.CompareStrings(ConsultCategoryHelperMethods.GetItemValue(item, activeIndexes[1]), categorySearchInputs[activeIndexes[1]].text, operators[1]) &&
                         HelperMethods.CompareStrings(ConsultCategoryHelperMethods.GetItemValue(item, activeIndexes[2]), categorySearchInputs[activeIndexes[2]].text, operators[2]) &&
                         HelperMethods.CompareStrings(ConsultCategoryHelperMethods.GetItemValue(item, activeIndexes[3]), categorySearchInputs[activeIndexes[3]].text, operators[3]) &&
                         HelperMethods.CompareStrings(ConsultCategoryHelperMethods.GetItemValue(item, activeIndexes[4]), categorySearchInputs[activeIndexes[4]].text, operators[4]) &&
                         HelperMethods.CompareStrings(ConsultCategoryHelperMethods.GetItemValue(item, activeIndexes[5]), categorySearchInputs[activeIndexes[5]].text, operators[5]) &&
                         HelperMethods.CompareStrings(ConsultCategoryHelperMethods.GetItemValue(item, activeIndexes[6]), categorySearchInputs[activeIndexes[6]].text, operators[6]) &&
                         HelperMethods.CompareStrings(ConsultCategoryHelperMethods.GetItemValue(item, activeIndexes[7]), categorySearchInputs[activeIndexes[7]].text, operators[7]) &&
                         HelperMethods.CompareStrings(ConsultCategoryHelperMethods.GetItemValue(item, activeIndexes[8]), categorySearchInputs[activeIndexes[8]].text, operators[8]) &&
                         HelperMethods.CompareStrings(ConsultCategoryHelperMethods.GetItemValue(item, activeIndexes[9]), categorySearchInputs[activeIndexes[9]].text, operators[9]) &&
                         HelperMethods.CompareStrings(ConsultCategoryHelperMethods.GetItemValue(item, activeIndexes[10]), categorySearchInputs[activeIndexes[10]].text, operators[10]) &&
                         HelperMethods.CompareStrings(ConsultCategoryHelperMethods.GetItemValue(item, activeIndexes[11]), categorySearchInputs[activeIndexes[11]].text, operators[11]) &&
                         HelperMethods.CompareStrings(ConsultCategoryHelperMethods.GetItemValue(item, activeIndexes[12]), categorySearchInputs[activeIndexes[12]].text, operators[12]) &&
                         HelperMethods.CompareStrings(ConsultCategoryHelperMethods.GetItemValue(item, activeIndexes[13]), categorySearchInputs[activeIndexes[13]].text, operators[13]) &&
                         HelperMethods.CompareStrings(ConsultCategoryHelperMethods.GetItemValue(item, activeIndexes[14]), categorySearchInputs[activeIndexes[14]].text, operators[14]) &&
                         HelperMethods.CompareStrings(ConsultCategoryHelperMethods.GetItemValue(item, activeIndexes[15]), categorySearchInputs[activeIndexes[15]].text, operators[15]) &&
                         HelperMethods.CompareStrings(ConsultCategoryHelperMethods.GetItemValue(item, activeIndexes[16]), categorySearchInputs[activeIndexes[16]].text, operators[16]))                       
                    {
                        if (!returnSheet.itens.Contains(item))
                        {
                            returnSheet.itens.Add(item);
                        }
                    }
                }
                break;
            case 18:
                foreach (ItemColumns item in databaseToConsult.itens)
                {
                    if (HelperMethods.CompareStrings(ConsultCategoryHelperMethods.GetItemValue(item, activeIndexes[0]), categorySearchInputs[activeIndexes[0]].text, operators[0]) &&
                        HelperMethods.CompareStrings(ConsultCategoryHelperMethods.GetItemValue(item, activeIndexes[1]), categorySearchInputs[activeIndexes[1]].text, operators[1]) &&
                        HelperMethods.CompareStrings(ConsultCategoryHelperMethods.GetItemValue(item, activeIndexes[2]), categorySearchInputs[activeIndexes[2]].text, operators[2]) &&
                        HelperMethods.CompareStrings(ConsultCategoryHelperMethods.GetItemValue(item, activeIndexes[3]), categorySearchInputs[activeIndexes[3]].text, operators[3]) &&
                        HelperMethods.CompareStrings(ConsultCategoryHelperMethods.GetItemValue(item, activeIndexes[4]), categorySearchInputs[activeIndexes[4]].text, operators[4]) &&
                        HelperMethods.CompareStrings(ConsultCategoryHelperMethods.GetItemValue(item, activeIndexes[5]), categorySearchInputs[activeIndexes[5]].text, operators[5]) &&
                        HelperMethods.CompareStrings(ConsultCategoryHelperMethods.GetItemValue(item, activeIndexes[6]), categorySearchInputs[activeIndexes[6]].text, operators[6]) &&
                        HelperMethods.CompareStrings(ConsultCategoryHelperMethods.GetItemValue(item, activeIndexes[7]), categorySearchInputs[activeIndexes[7]].text, operators[7]) &&
                        HelperMethods.CompareStrings(ConsultCategoryHelperMethods.GetItemValue(item, activeIndexes[8]), categorySearchInputs[activeIndexes[8]].text, operators[8]) &&
                        HelperMethods.CompareStrings(ConsultCategoryHelperMethods.GetItemValue(item, activeIndexes[9]), categorySearchInputs[activeIndexes[9]].text, operators[9]) &&
                        HelperMethods.CompareStrings(ConsultCategoryHelperMethods.GetItemValue(item, activeIndexes[10]), categorySearchInputs[activeIndexes[10]].text, operators[10]) &&
                        HelperMethods.CompareStrings(ConsultCategoryHelperMethods.GetItemValue(item, activeIndexes[11]), categorySearchInputs[activeIndexes[11]].text, operators[11]) &&
                        HelperMethods.CompareStrings(ConsultCategoryHelperMethods.GetItemValue(item, activeIndexes[12]), categorySearchInputs[activeIndexes[12]].text, operators[12]) &&
                        HelperMethods.CompareStrings(ConsultCategoryHelperMethods.GetItemValue(item, activeIndexes[13]), categorySearchInputs[activeIndexes[13]].text, operators[13]) &&
                        HelperMethods.CompareStrings(ConsultCategoryHelperMethods.GetItemValue(item, activeIndexes[14]), categorySearchInputs[activeIndexes[14]].text, operators[14]) &&
                        HelperMethods.CompareStrings(ConsultCategoryHelperMethods.GetItemValue(item, activeIndexes[15]), categorySearchInputs[activeIndexes[15]].text, operators[15]) &&
                        HelperMethods.CompareStrings(ConsultCategoryHelperMethods.GetItemValue(item, activeIndexes[16]), categorySearchInputs[activeIndexes[16]].text, operators[16]) &&
                        HelperMethods.CompareStrings(ConsultCategoryHelperMethods.GetItemValue(item, activeIndexes[17]), categorySearchInputs[activeIndexes[17]].text, operators[17]))
                    {
                        if (!returnSheet.itens.Contains(item))
                        {
                            returnSheet.itens.Add(item);
                        }
                    }
                }
                break;
            default:
                break;
        }
        returnSheet.itens.Sort((x, y) => x.Patrimonio.CompareTo(y.Patrimonio));

        return returnSheet; 
    }
 
}