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
                        if (HelperMethods.CompareStrings(item.GetValue(categorySearchInputs[activeIndexes[0]].text), categorySearchInputs[activeIndexes[0]].text, operators[0]))
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
                    if (HelperMethods.CompareStrings(item.GetValue(categorySearchInputs[activeIndexes[0]].text), categorySearchInputs[activeIndexes[0]].text, operators[0]) &&
                        HelperMethods.CompareStrings(item.GetValue(categorySearchInputs[activeIndexes[1]].text), categorySearchInputs[activeIndexes[1]].text, operators[1]))
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
                    if (HelperMethods.CompareStrings(item.GetValue(categorySearchInputs[activeIndexes[0]].text), categorySearchInputs[activeIndexes[0]].text, operators[0]) &&
                        HelperMethods.CompareStrings(item.GetValue(categorySearchInputs[activeIndexes[1]].text), categorySearchInputs[activeIndexes[1]].text, operators[1]) &&
                        HelperMethods.CompareStrings(item.GetValue(categorySearchInputs[activeIndexes[2]].text), categorySearchInputs[activeIndexes[2]].text, operators[2]))
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
                    if (HelperMethods.CompareStrings(item.GetValue(categorySearchInputs[activeIndexes[0]].text), categorySearchInputs[activeIndexes[0]].text, operators[0]) &&
                        HelperMethods.CompareStrings(item.GetValue(categorySearchInputs[activeIndexes[1]].text), categorySearchInputs[activeIndexes[1]].text, operators[1]) &&
                        HelperMethods.CompareStrings(item.GetValue(categorySearchInputs[activeIndexes[2]].text), categorySearchInputs[activeIndexes[2]].text, operators[2]) &&
                        HelperMethods.CompareStrings(item.GetValue(categorySearchInputs[activeIndexes[3]].text), categorySearchInputs[activeIndexes[3]].text, operators[3]))
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
                    if (HelperMethods.CompareStrings(item.GetValue(categorySearchInputs[activeIndexes[0]].text), categorySearchInputs[activeIndexes[0]].text, operators[0]) &&
                        HelperMethods.CompareStrings(item.GetValue(categorySearchInputs[activeIndexes[1]].text), categorySearchInputs[activeIndexes[1]].text, operators[1]) &&
                        HelperMethods.CompareStrings(item.GetValue(categorySearchInputs[activeIndexes[2]].text), categorySearchInputs[activeIndexes[2]].text, operators[2]) &&
                        HelperMethods.CompareStrings(item.GetValue(categorySearchInputs[activeIndexes[3]].text), categorySearchInputs[activeIndexes[3]].text, operators[3]) &&
                        HelperMethods.CompareStrings(item.GetValue(categorySearchInputs[activeIndexes[4]].text), categorySearchInputs[activeIndexes[4]].text, operators[4]))
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
                    if (HelperMethods.CompareStrings(item.GetValue(categorySearchInputs[activeIndexes[0]].text), categorySearchInputs[activeIndexes[0]].text, operators[0]) &&
                        HelperMethods.CompareStrings(item.GetValue(categorySearchInputs[activeIndexes[1]].text), categorySearchInputs[activeIndexes[1]].text, operators[1]) &&
                        HelperMethods.CompareStrings(item.GetValue(categorySearchInputs[activeIndexes[2]].text), categorySearchInputs[activeIndexes[2]].text, operators[2]) &&
                        HelperMethods.CompareStrings(item.GetValue(categorySearchInputs[activeIndexes[3]].text), categorySearchInputs[activeIndexes[3]].text, operators[3]) &&
                        HelperMethods.CompareStrings(item.GetValue(categorySearchInputs[activeIndexes[4]].text), categorySearchInputs[activeIndexes[4]].text, operators[4]) &&
                        HelperMethods.CompareStrings(item.GetValue(categorySearchInputs[activeIndexes[5]].text), categorySearchInputs[activeIndexes[5]].text, operators[5]))
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
                    if (HelperMethods.CompareStrings(item.GetValue(categorySearchInputs[activeIndexes[0]].text), categorySearchInputs[activeIndexes[0]].text, operators[0]) &&
                        HelperMethods.CompareStrings(item.GetValue(categorySearchInputs[activeIndexes[1]].text), categorySearchInputs[activeIndexes[1]].text, operators[1]) &&
                        HelperMethods.CompareStrings(item.GetValue(categorySearchInputs[activeIndexes[2]].text), categorySearchInputs[activeIndexes[2]].text, operators[2]) &&
                        HelperMethods.CompareStrings(item.GetValue(categorySearchInputs[activeIndexes[3]].text), categorySearchInputs[activeIndexes[3]].text, operators[3]) &&
                        HelperMethods.CompareStrings(item.GetValue(categorySearchInputs[activeIndexes[4]].text), categorySearchInputs[activeIndexes[4]].text, operators[4]) &&
                        HelperMethods.CompareStrings(item.GetValue(categorySearchInputs[activeIndexes[5]].text), categorySearchInputs[activeIndexes[5]].text, operators[5]) &&
                        HelperMethods.CompareStrings(item.GetValue(categorySearchInputs[activeIndexes[6]].text), categorySearchInputs[activeIndexes[6]].text, operators[6]))
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
                    if (HelperMethods.CompareStrings(item.GetValue(categorySearchInputs[activeIndexes[0]].text), categorySearchInputs[activeIndexes[0]].text, operators[0]) &&
                        HelperMethods.CompareStrings(item.GetValue(categorySearchInputs[activeIndexes[1]].text), categorySearchInputs[activeIndexes[1]].text, operators[1]) &&
                        HelperMethods.CompareStrings(item.GetValue(categorySearchInputs[activeIndexes[2]].text), categorySearchInputs[activeIndexes[2]].text, operators[2]) &&
                        HelperMethods.CompareStrings(item.GetValue(categorySearchInputs[activeIndexes[3]].text), categorySearchInputs[activeIndexes[3]].text, operators[3]) &&
                        HelperMethods.CompareStrings(item.GetValue(categorySearchInputs[activeIndexes[4]].text), categorySearchInputs[activeIndexes[4]].text, operators[4]) &&
                        HelperMethods.CompareStrings(item.GetValue(categorySearchInputs[activeIndexes[5]].text), categorySearchInputs[activeIndexes[5]].text, operators[5]) &&
                        HelperMethods.CompareStrings(item.GetValue(categorySearchInputs[activeIndexes[6]].text), categorySearchInputs[activeIndexes[6]].text, operators[6]) &&
                        HelperMethods.CompareStrings(item.GetValue(categorySearchInputs[activeIndexes[7]].text), categorySearchInputs[activeIndexes[7]].text, operators[7]))
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
                    if (HelperMethods.CompareStrings(item.GetValue(categorySearchInputs[activeIndexes[0]].text), categorySearchInputs[activeIndexes[0]].text, operators[0]) &&
                        HelperMethods.CompareStrings(item.GetValue(categorySearchInputs[activeIndexes[1]].text), categorySearchInputs[activeIndexes[1]].text, operators[1]) &&
                        HelperMethods.CompareStrings(item.GetValue(categorySearchInputs[activeIndexes[2]].text), categorySearchInputs[activeIndexes[2]].text, operators[2]) &&
                        HelperMethods.CompareStrings(item.GetValue(categorySearchInputs[activeIndexes[3]].text), categorySearchInputs[activeIndexes[3]].text, operators[3]) &&
                        HelperMethods.CompareStrings(item.GetValue(categorySearchInputs[activeIndexes[4]].text), categorySearchInputs[activeIndexes[4]].text, operators[4]) &&
                        HelperMethods.CompareStrings(item.GetValue(categorySearchInputs[activeIndexes[5]].text), categorySearchInputs[activeIndexes[5]].text, operators[5]) &&
                        HelperMethods.CompareStrings(item.GetValue(categorySearchInputs[activeIndexes[6]].text), categorySearchInputs[activeIndexes[6]].text, operators[6]) &&
                        HelperMethods.CompareStrings(item.GetValue(categorySearchInputs[activeIndexes[7]].text), categorySearchInputs[activeIndexes[7]].text, operators[7]) &&
                        HelperMethods.CompareStrings(item.GetValue(categorySearchInputs[activeIndexes[8]].text), categorySearchInputs[activeIndexes[8]].text, operators[8]))
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
                    if (HelperMethods.CompareStrings(item.GetValue(categorySearchInputs[activeIndexes[0]].text), categorySearchInputs[activeIndexes[0]].text, operators[0]) &&
                        HelperMethods.CompareStrings(item.GetValue(categorySearchInputs[activeIndexes[1]].text), categorySearchInputs[activeIndexes[1]].text, operators[1]) &&
                        HelperMethods.CompareStrings(item.GetValue(categorySearchInputs[activeIndexes[2]].text), categorySearchInputs[activeIndexes[2]].text, operators[2]) &&
                        HelperMethods.CompareStrings(item.GetValue(categorySearchInputs[activeIndexes[3]].text), categorySearchInputs[activeIndexes[3]].text, operators[3]) &&
                        HelperMethods.CompareStrings(item.GetValue(categorySearchInputs[activeIndexes[4]].text), categorySearchInputs[activeIndexes[4]].text, operators[4]) &&
                        HelperMethods.CompareStrings(item.GetValue(categorySearchInputs[activeIndexes[5]].text), categorySearchInputs[activeIndexes[5]].text, operators[5]) &&
                        HelperMethods.CompareStrings(item.GetValue(categorySearchInputs[activeIndexes[6]].text), categorySearchInputs[activeIndexes[6]].text, operators[6]) &&
                        HelperMethods.CompareStrings(item.GetValue(categorySearchInputs[activeIndexes[7]].text), categorySearchInputs[activeIndexes[7]].text, operators[7]) &&
                        HelperMethods.CompareStrings(item.GetValue(categorySearchInputs[activeIndexes[8]].text), categorySearchInputs[activeIndexes[8]].text, operators[8]) &&
                        HelperMethods.CompareStrings(item.GetValue(categorySearchInputs[activeIndexes[9]].text), categorySearchInputs[activeIndexes[9]].text, operators[9]))
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
                    if (HelperMethods.CompareStrings(item.GetValue(categorySearchInputs[activeIndexes[0]].text), categorySearchInputs[activeIndexes[0]].text, operators[0]) &&
                        HelperMethods.CompareStrings(item.GetValue(categorySearchInputs[activeIndexes[1]].text), categorySearchInputs[activeIndexes[1]].text, operators[1]) &&
                        HelperMethods.CompareStrings(item.GetValue(categorySearchInputs[activeIndexes[2]].text), categorySearchInputs[activeIndexes[2]].text, operators[2]) &&
                        HelperMethods.CompareStrings(item.GetValue(categorySearchInputs[activeIndexes[3]].text), categorySearchInputs[activeIndexes[3]].text, operators[3]) &&
                        HelperMethods.CompareStrings(item.GetValue(categorySearchInputs[activeIndexes[4]].text), categorySearchInputs[activeIndexes[4]].text, operators[4]) &&
                        HelperMethods.CompareStrings(item.GetValue(categorySearchInputs[activeIndexes[5]].text), categorySearchInputs[activeIndexes[5]].text, operators[5]) &&
                        HelperMethods.CompareStrings(item.GetValue(categorySearchInputs[activeIndexes[6]].text), categorySearchInputs[activeIndexes[6]].text, operators[6]) &&
                        HelperMethods.CompareStrings(item.GetValue(categorySearchInputs[activeIndexes[7]].text), categorySearchInputs[activeIndexes[7]].text, operators[7]) &&
                        HelperMethods.CompareStrings(item.GetValue(categorySearchInputs[activeIndexes[8]].text), categorySearchInputs[activeIndexes[8]].text, operators[8]) &&
                        HelperMethods.CompareStrings(item.GetValue(categorySearchInputs[activeIndexes[9]].text), categorySearchInputs[activeIndexes[9]].text, operators[9]) &&
                        HelperMethods.CompareStrings(item.GetValue(categorySearchInputs[activeIndexes[10]].text), categorySearchInputs[activeIndexes[10]].text, operators[10]))
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
                    if (HelperMethods.CompareStrings(item.GetValue(categorySearchInputs[activeIndexes[0]].text), categorySearchInputs[activeIndexes[0]].text, operators[0]) &&
                        HelperMethods.CompareStrings(item.GetValue(categorySearchInputs[activeIndexes[1]].text), categorySearchInputs[activeIndexes[1]].text, operators[1]) &&
                        HelperMethods.CompareStrings(item.GetValue(categorySearchInputs[activeIndexes[2]].text), categorySearchInputs[activeIndexes[2]].text, operators[2]) &&
                        HelperMethods.CompareStrings(item.GetValue(categorySearchInputs[activeIndexes[3]].text), categorySearchInputs[activeIndexes[3]].text, operators[3]) &&
                        HelperMethods.CompareStrings(item.GetValue(categorySearchInputs[activeIndexes[4]].text), categorySearchInputs[activeIndexes[4]].text, operators[4]) &&
                        HelperMethods.CompareStrings(item.GetValue(categorySearchInputs[activeIndexes[5]].text), categorySearchInputs[activeIndexes[5]].text, operators[5]) &&
                        HelperMethods.CompareStrings(item.GetValue(categorySearchInputs[activeIndexes[6]].text), categorySearchInputs[activeIndexes[6]].text, operators[6]) &&
                        HelperMethods.CompareStrings(item.GetValue(categorySearchInputs[activeIndexes[7]].text), categorySearchInputs[activeIndexes[7]].text, operators[7]) &&
                        HelperMethods.CompareStrings(item.GetValue(categorySearchInputs[activeIndexes[8]].text), categorySearchInputs[activeIndexes[8]].text, operators[8]) &&
                        HelperMethods.CompareStrings(item.GetValue(categorySearchInputs[activeIndexes[9]].text), categorySearchInputs[activeIndexes[9]].text, operators[9]) &&
                        HelperMethods.CompareStrings(item.GetValue(categorySearchInputs[activeIndexes[10]].text), categorySearchInputs[activeIndexes[10]].text, operators[10]) &&
                        HelperMethods.CompareStrings(item.GetValue(categorySearchInputs[activeIndexes[11]].text), categorySearchInputs[activeIndexes[11]].text, operators[11]))
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
                    if (HelperMethods.CompareStrings(item.GetValue(categorySearchInputs[activeIndexes[0]].text), categorySearchInputs[activeIndexes[0]].text, operators[0]) &&
                        HelperMethods.CompareStrings(item.GetValue(categorySearchInputs[activeIndexes[1]].text), categorySearchInputs[activeIndexes[1]].text, operators[1]) &&
                        HelperMethods.CompareStrings(item.GetValue(categorySearchInputs[activeIndexes[2]].text), categorySearchInputs[activeIndexes[2]].text, operators[2]) &&
                        HelperMethods.CompareStrings(item.GetValue(categorySearchInputs[activeIndexes[3]].text), categorySearchInputs[activeIndexes[3]].text, operators[3]) &&
                        HelperMethods.CompareStrings(item.GetValue(categorySearchInputs[activeIndexes[4]].text), categorySearchInputs[activeIndexes[4]].text, operators[4]) &&
                        HelperMethods.CompareStrings(item.GetValue(categorySearchInputs[activeIndexes[5]].text), categorySearchInputs[activeIndexes[5]].text, operators[5]) &&
                        HelperMethods.CompareStrings(item.GetValue(categorySearchInputs[activeIndexes[6]].text), categorySearchInputs[activeIndexes[6]].text, operators[6]) &&
                        HelperMethods.CompareStrings(item.GetValue(categorySearchInputs[activeIndexes[7]].text), categorySearchInputs[activeIndexes[7]].text, operators[7]) &&
                        HelperMethods.CompareStrings(item.GetValue(categorySearchInputs[activeIndexes[8]].text), categorySearchInputs[activeIndexes[8]].text, operators[8]) &&
                        HelperMethods.CompareStrings(item.GetValue(categorySearchInputs[activeIndexes[9]].text), categorySearchInputs[activeIndexes[9]].text, operators[9]) &&
                        HelperMethods.CompareStrings(item.GetValue(categorySearchInputs[activeIndexes[10]].text), categorySearchInputs[activeIndexes[10]].text, operators[10]) &&
                        HelperMethods.CompareStrings(item.GetValue(categorySearchInputs[activeIndexes[11]].text), categorySearchInputs[activeIndexes[11]].text, operators[11]) &&
                        HelperMethods.CompareStrings(item.GetValue(categorySearchInputs[activeIndexes[12]].text), categorySearchInputs[activeIndexes[12]].text, operators[12]))
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
                    if (HelperMethods.CompareStrings(item.GetValue(categorySearchInputs[activeIndexes[0]].text), categorySearchInputs[activeIndexes[0]].text, operators[0]) &&
                        HelperMethods.CompareStrings(item.GetValue(categorySearchInputs[activeIndexes[1]].text), categorySearchInputs[activeIndexes[1]].text, operators[1]) &&
                        HelperMethods.CompareStrings(item.GetValue(categorySearchInputs[activeIndexes[2]].text), categorySearchInputs[activeIndexes[2]].text, operators[2]) &&
                        HelperMethods.CompareStrings(item.GetValue(categorySearchInputs[activeIndexes[3]].text), categorySearchInputs[activeIndexes[3]].text, operators[3]) &&
                        HelperMethods.CompareStrings(item.GetValue(categorySearchInputs[activeIndexes[4]].text), categorySearchInputs[activeIndexes[4]].text, operators[4]) &&
                        HelperMethods.CompareStrings(item.GetValue(categorySearchInputs[activeIndexes[5]].text), categorySearchInputs[activeIndexes[5]].text, operators[5]) &&
                        HelperMethods.CompareStrings(item.GetValue(categorySearchInputs[activeIndexes[6]].text), categorySearchInputs[activeIndexes[6]].text, operators[6]) &&
                        HelperMethods.CompareStrings(item.GetValue(categorySearchInputs[activeIndexes[7]].text), categorySearchInputs[activeIndexes[7]].text, operators[7]) &&
                        HelperMethods.CompareStrings(item.GetValue(categorySearchInputs[activeIndexes[8]].text), categorySearchInputs[activeIndexes[8]].text, operators[8]) &&
                        HelperMethods.CompareStrings(item.GetValue(categorySearchInputs[activeIndexes[9]].text), categorySearchInputs[activeIndexes[9]].text, operators[9]) &&
                        HelperMethods.CompareStrings(item.GetValue(categorySearchInputs[activeIndexes[10]].text), categorySearchInputs[activeIndexes[10]].text, operators[10]) &&
                        HelperMethods.CompareStrings(item.GetValue(categorySearchInputs[activeIndexes[11]].text), categorySearchInputs[activeIndexes[11]].text, operators[11]) &&
                        HelperMethods.CompareStrings(item.GetValue(categorySearchInputs[activeIndexes[12]].text), categorySearchInputs[activeIndexes[12]].text, operators[12]) &&
                        HelperMethods.CompareStrings(item.GetValue(categorySearchInputs[activeIndexes[13]].text), categorySearchInputs[activeIndexes[13]].text, operators[13]))
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
                    if (HelperMethods.CompareStrings(item.GetValue(categorySearchInputs[activeIndexes[0]].text), categorySearchInputs[activeIndexes[0]].text, operators[0]) &&
                        HelperMethods.CompareStrings(item.GetValue(categorySearchInputs[activeIndexes[1]].text), categorySearchInputs[activeIndexes[1]].text, operators[1]) &&                        HelperMethods.CompareStrings(item.GetValue(categorySearchInputs[activeIndexes[2]].text), categorySearchInputs[activeIndexes[2]].text, operators[0]) &&
                        HelperMethods.CompareStrings(item.GetValue(categorySearchInputs[activeIndexes[2]].text), categorySearchInputs[activeIndexes[2]].text, operators[2]) &&
                        HelperMethods.CompareStrings(item.GetValue(categorySearchInputs[activeIndexes[3]].text), categorySearchInputs[activeIndexes[3]].text, operators[3]) &&
                        HelperMethods.CompareStrings(item.GetValue(categorySearchInputs[activeIndexes[4]].text), categorySearchInputs[activeIndexes[4]].text, operators[4]) &&
                        HelperMethods.CompareStrings(item.GetValue(categorySearchInputs[activeIndexes[5]].text), categorySearchInputs[activeIndexes[5]].text, operators[5]) &&
                        HelperMethods.CompareStrings(item.GetValue(categorySearchInputs[activeIndexes[6]].text), categorySearchInputs[activeIndexes[6]].text, operators[6]) &&
                        HelperMethods.CompareStrings(item.GetValue(categorySearchInputs[activeIndexes[7]].text), categorySearchInputs[activeIndexes[7]].text, operators[7]) &&
                        HelperMethods.CompareStrings(item.GetValue(categorySearchInputs[activeIndexes[8]].text), categorySearchInputs[activeIndexes[8]].text, operators[8]) &&
                        HelperMethods.CompareStrings(item.GetValue(categorySearchInputs[activeIndexes[9]].text), categorySearchInputs[activeIndexes[9]].text, operators[9]) &&
                        HelperMethods.CompareStrings(item.GetValue(categorySearchInputs[activeIndexes[10]].text), categorySearchInputs[activeIndexes[10]].text, operators[10]) &&
                        HelperMethods.CompareStrings(item.GetValue(categorySearchInputs[activeIndexes[11]].text), categorySearchInputs[activeIndexes[11]].text, operators[11]) &&
                        HelperMethods.CompareStrings(item.GetValue(categorySearchInputs[activeIndexes[12]].text), categorySearchInputs[activeIndexes[12]].text, operators[12]) &&
                        HelperMethods.CompareStrings(item.GetValue(categorySearchInputs[activeIndexes[13]].text), categorySearchInputs[activeIndexes[13]].text, operators[13]) &&
                        HelperMethods.CompareStrings(item.GetValue(categorySearchInputs[activeIndexes[14]].text), categorySearchInputs[activeIndexes[14]].text, operators[14]))
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
                    if (HelperMethods.CompareStrings(item.GetValue(categorySearchInputs[activeIndexes[0]].text), categorySearchInputs[activeIndexes[0]].text, operators[0]) &&
                        HelperMethods.CompareStrings(item.GetValue(categorySearchInputs[activeIndexes[1]].text), categorySearchInputs[activeIndexes[1]].text, operators[1]) &&
                        HelperMethods.CompareStrings(item.GetValue(categorySearchInputs[activeIndexes[2]].text), categorySearchInputs[activeIndexes[2]].text, operators[2]) &&
                        HelperMethods.CompareStrings(item.GetValue(categorySearchInputs[activeIndexes[3]].text), categorySearchInputs[activeIndexes[3]].text, operators[3]) &&
                        HelperMethods.CompareStrings(item.GetValue(categorySearchInputs[activeIndexes[4]].text), categorySearchInputs[activeIndexes[4]].text, operators[4]) &&
                        HelperMethods.CompareStrings(item.GetValue(categorySearchInputs[activeIndexes[5]].text), categorySearchInputs[activeIndexes[5]].text, operators[5]) &&
                        HelperMethods.CompareStrings(item.GetValue(categorySearchInputs[activeIndexes[6]].text), categorySearchInputs[activeIndexes[6]].text, operators[6]) &&
                        HelperMethods.CompareStrings(item.GetValue(categorySearchInputs[activeIndexes[7]].text), categorySearchInputs[activeIndexes[7]].text, operators[7]) &&
                        HelperMethods.CompareStrings(item.GetValue(categorySearchInputs[activeIndexes[8]].text), categorySearchInputs[activeIndexes[8]].text, operators[8]) &&
                        HelperMethods.CompareStrings(item.GetValue(categorySearchInputs[activeIndexes[9]].text), categorySearchInputs[activeIndexes[9]].text, operators[9]) &&
                        HelperMethods.CompareStrings(item.GetValue(categorySearchInputs[activeIndexes[10]].text), categorySearchInputs[activeIndexes[10]].text, operators[10]) &&
                        HelperMethods.CompareStrings(item.GetValue(categorySearchInputs[activeIndexes[11]].text), categorySearchInputs[activeIndexes[11]].text, operators[11]) &&
                        HelperMethods.CompareStrings(item.GetValue(categorySearchInputs[activeIndexes[12]].text), categorySearchInputs[activeIndexes[12]].text, operators[12]) &&
                        HelperMethods.CompareStrings(item.GetValue(categorySearchInputs[activeIndexes[13]].text), categorySearchInputs[activeIndexes[13]].text, operators[13]) &&
                        HelperMethods.CompareStrings(item.GetValue(categorySearchInputs[activeIndexes[14]].text), categorySearchInputs[activeIndexes[14]].text, operators[14]) &&
                        HelperMethods.CompareStrings(item.GetValue(categorySearchInputs[activeIndexes[15]].text), categorySearchInputs[activeIndexes[15]].text, operators[15]))
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
                    if (HelperMethods.CompareStrings(item.GetValue(categorySearchInputs[activeIndexes[0]].text), categorySearchInputs[activeIndexes[0]].text, operators[0]) &&
                        HelperMethods.CompareStrings(item.GetValue(categorySearchInputs[activeIndexes[1]].text), categorySearchInputs[activeIndexes[1]].text, operators[1]) &&
                        HelperMethods.CompareStrings(item.GetValue(categorySearchInputs[activeIndexes[2]].text), categorySearchInputs[activeIndexes[2]].text, operators[2]) &&
                        HelperMethods.CompareStrings(item.GetValue(categorySearchInputs[activeIndexes[3]].text), categorySearchInputs[activeIndexes[3]].text, operators[3]) &&
                        HelperMethods.CompareStrings(item.GetValue(categorySearchInputs[activeIndexes[4]].text), categorySearchInputs[activeIndexes[4]].text, operators[4]) &&
                        HelperMethods.CompareStrings(item.GetValue(categorySearchInputs[activeIndexes[5]].text), categorySearchInputs[activeIndexes[5]].text, operators[5]) &&
                        HelperMethods.CompareStrings(item.GetValue(categorySearchInputs[activeIndexes[6]].text), categorySearchInputs[activeIndexes[6]].text, operators[6]) &&
                        HelperMethods.CompareStrings(item.GetValue(categorySearchInputs[activeIndexes[7]].text), categorySearchInputs[activeIndexes[7]].text, operators[7]) &&
                        HelperMethods.CompareStrings(item.GetValue(categorySearchInputs[activeIndexes[8]].text), categorySearchInputs[activeIndexes[8]].text, operators[8]) &&
                        HelperMethods.CompareStrings(item.GetValue(categorySearchInputs[activeIndexes[9]].text), categorySearchInputs[activeIndexes[9]].text, operators[9]) &&
                        HelperMethods.CompareStrings(item.GetValue(categorySearchInputs[activeIndexes[10]].text), categorySearchInputs[activeIndexes[10]].text, operators[10]) &&
                        HelperMethods.CompareStrings(item.GetValue(categorySearchInputs[activeIndexes[11]].text), categorySearchInputs[activeIndexes[11]].text, operators[11]) &&
                        HelperMethods.CompareStrings(item.GetValue(categorySearchInputs[activeIndexes[12]].text), categorySearchInputs[activeIndexes[12]].text, operators[12]) &&
                        HelperMethods.CompareStrings(item.GetValue(categorySearchInputs[activeIndexes[13]].text), categorySearchInputs[activeIndexes[13]].text, operators[13]) &&
                        HelperMethods.CompareStrings(item.GetValue(categorySearchInputs[activeIndexes[14]].text), categorySearchInputs[activeIndexes[14]].text, operators[14]) &&
                        HelperMethods.CompareStrings(item.GetValue(categorySearchInputs[activeIndexes[15]].text), categorySearchInputs[activeIndexes[15]].text, operators[15]) &&
                        HelperMethods.CompareStrings(item.GetValue(categorySearchInputs[activeIndexes[16]].text), categorySearchInputs[activeIndexes[16]].text, operators[16]))
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
                    if (HelperMethods.CompareStrings(item.GetValue(categorySearchInputs[activeIndexes[0]].text), categorySearchInputs[activeIndexes[0]].text, operators[0]) &&
                        HelperMethods.CompareStrings(item.GetValue(categorySearchInputs[activeIndexes[1]].text), categorySearchInputs[activeIndexes[1]].text, operators[1]) &&
                        HelperMethods.CompareStrings(item.GetValue(categorySearchInputs[activeIndexes[2]].text), categorySearchInputs[activeIndexes[2]].text, operators[2]) &&
                        HelperMethods.CompareStrings(item.GetValue(categorySearchInputs[activeIndexes[3]].text), categorySearchInputs[activeIndexes[3]].text, operators[3]) &&
                        HelperMethods.CompareStrings(item.GetValue(categorySearchInputs[activeIndexes[4]].text), categorySearchInputs[activeIndexes[4]].text, operators[4]) &&
                        HelperMethods.CompareStrings(item.GetValue(categorySearchInputs[activeIndexes[5]].text), categorySearchInputs[activeIndexes[5]].text, operators[5]) &&
                        HelperMethods.CompareStrings(item.GetValue(categorySearchInputs[activeIndexes[6]].text), categorySearchInputs[activeIndexes[6]].text, operators[6]) &&
                        HelperMethods.CompareStrings(item.GetValue(categorySearchInputs[activeIndexes[7]].text), categorySearchInputs[activeIndexes[7]].text, operators[7]) &&
                        HelperMethods.CompareStrings(item.GetValue(categorySearchInputs[activeIndexes[8]].text), categorySearchInputs[activeIndexes[8]].text, operators[8]) &&
                        HelperMethods.CompareStrings(item.GetValue(categorySearchInputs[activeIndexes[9]].text), categorySearchInputs[activeIndexes[9]].text, operators[9]) &&
                        HelperMethods.CompareStrings(item.GetValue(categorySearchInputs[activeIndexes[10]].text), categorySearchInputs[activeIndexes[10]].text, operators[10]) &&
                        HelperMethods.CompareStrings(item.GetValue(categorySearchInputs[activeIndexes[11]].text), categorySearchInputs[activeIndexes[11]].text, operators[11]) &&
                        HelperMethods.CompareStrings(item.GetValue(categorySearchInputs[activeIndexes[12]].text), categorySearchInputs[activeIndexes[12]].text, operators[12]) &&
                        HelperMethods.CompareStrings(item.GetValue(categorySearchInputs[activeIndexes[13]].text), categorySearchInputs[activeIndexes[13]].text, operators[13]) &&
                        HelperMethods.CompareStrings(item.GetValue(categorySearchInputs[activeIndexes[14]].text), categorySearchInputs[activeIndexes[14]].text, operators[14]) &&
                        HelperMethods.CompareStrings(item.GetValue(categorySearchInputs[activeIndexes[15]].text), categorySearchInputs[activeIndexes[15]].text, operators[15]) &&
                        HelperMethods.CompareStrings(item.GetValue(categorySearchInputs[activeIndexes[16]].text), categorySearchInputs[activeIndexes[16]].text, operators[16]) &&
                        HelperMethods.CompareStrings(item.GetValue(categorySearchInputs[activeIndexes[17]].text), categorySearchInputs[activeIndexes[17]].text, operators[17]))
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
