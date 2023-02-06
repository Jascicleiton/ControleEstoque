using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class ConsultCategory : MonoBehaviour
{
    /// <summary>
    /// Find all itens from a specific category that match the search parameters
    /// </summary>
    public Sheet FindItens(List<int> activeIndexes, TMP_InputField[] categorySearchInputs, Sheet databaseToConsult)
    {
        Sheet returnSheet = new Sheet();
        
        switch (activeIndexes.Count)
        {
            case 1:
                foreach (ItemColumns item in databaseToConsult.itens)
                {
                    if (item.GetValue(categorySearchInputs[activeIndexes[0]].text) == categorySearchInputs[activeIndexes[0]].text)
                    {
                        returnSheet.itens.Add(item);
                    }
                }
                break;
            case 2:
                foreach (ItemColumns item in databaseToConsult.itens)
                {
                    if (item.GetValue(categorySearchInputs[activeIndexes[0]].text) == categorySearchInputs[activeIndexes[0]].text &&
                        item.GetValue(categorySearchInputs[activeIndexes[1]].text) == categorySearchInputs[activeIndexes[1]].text)
                    {
                        returnSheet.itens.Add(item);
                    }
                }
                break;
            case 3:
                foreach (ItemColumns item in databaseToConsult.itens)
                {

                    if (item.GetValue(categorySearchInputs[activeIndexes[0]].text) == categorySearchInputs[activeIndexes[0]].text &&
                        item.GetValue(categorySearchInputs[activeIndexes[1]].text) == categorySearchInputs[activeIndexes[1]].text &&
                        item.GetValue(categorySearchInputs[activeIndexes[2]].text) == categorySearchInputs[activeIndexes[2]].text)
                    {
                        returnSheet.itens.Add(item);
                    }
                }
                break;
            case 4:
                foreach (ItemColumns item in databaseToConsult.itens)
                {
                    if (item.GetValue(categorySearchInputs[activeIndexes[0]].text) == categorySearchInputs[activeIndexes[0]].text &&
                        item.GetValue(categorySearchInputs[activeIndexes[1]].text) == categorySearchInputs[activeIndexes[1]].text &&
                        item.GetValue(categorySearchInputs[activeIndexes[2]].text) == categorySearchInputs[activeIndexes[2]].text &&
                        item.GetValue(categorySearchInputs[activeIndexes[3]].text) == categorySearchInputs[activeIndexes[3]].text)
                    {
                        returnSheet.itens.Add(item);
                    }
                }
                break;
            case 5:
                foreach (ItemColumns item in databaseToConsult.itens)
                {
                    if (item.GetValue(categorySearchInputs[activeIndexes[0]].text) == categorySearchInputs[activeIndexes[0]].text &&
                        item.GetValue(categorySearchInputs[activeIndexes[1]].text) == categorySearchInputs[activeIndexes[1]].text &&
                        item.GetValue(categorySearchInputs[activeIndexes[2]].text) == categorySearchInputs[activeIndexes[2]].text &&
                        item.GetValue(categorySearchInputs[activeIndexes[3]].text) == categorySearchInputs[activeIndexes[3]].text &&
                        item.GetValue(categorySearchInputs[activeIndexes[4]].text) == categorySearchInputs[activeIndexes[4]].text)
                    {
                        returnSheet.itens.Add(item);
                    }
                }
                break;
            case 6:
                foreach (ItemColumns item in databaseToConsult.itens)
                {
                    if (item.GetValue(categorySearchInputs[activeIndexes[0]].text) == categorySearchInputs[activeIndexes[0]].text &&
                        item.GetValue(categorySearchInputs[activeIndexes[1]].text) == categorySearchInputs[activeIndexes[1]].text &&
                        item.GetValue(categorySearchInputs[activeIndexes[2]].text) == categorySearchInputs[activeIndexes[2]].text &&
                        item.GetValue(categorySearchInputs[activeIndexes[3]].text) == categorySearchInputs[activeIndexes[3]].text &&
                        item.GetValue(categorySearchInputs[activeIndexes[4]].text) == categorySearchInputs[activeIndexes[4]].text &&
                        item.GetValue(categorySearchInputs[activeIndexes[5]].text) == categorySearchInputs[activeIndexes[5]].text)
                    {
                        returnSheet.itens.Add(item);
                    }
                }
                break;
            case 7:
                foreach (ItemColumns item in databaseToConsult.itens)
                {
                    if (item.GetValue(categorySearchInputs[activeIndexes[0]].text) == categorySearchInputs[activeIndexes[0]].text &&
                        item.GetValue(categorySearchInputs[activeIndexes[1]].text) == categorySearchInputs[activeIndexes[1]].text &&
                        item.GetValue(categorySearchInputs[activeIndexes[2]].text) == categorySearchInputs[activeIndexes[2]].text &&
                        item.GetValue(categorySearchInputs[activeIndexes[3]].text) == categorySearchInputs[activeIndexes[3]].text &&
                        item.GetValue(categorySearchInputs[activeIndexes[4]].text) == categorySearchInputs[activeIndexes[4]].text &&
                        item.GetValue(categorySearchInputs[activeIndexes[5]].text) == categorySearchInputs[activeIndexes[5]].text &&
                        item.GetValue(categorySearchInputs[activeIndexes[6]].text) == categorySearchInputs[activeIndexes[6]].text)
                    {
                        returnSheet.itens.Add(item);
                    }
                }
                break;
            case 8:
                foreach (ItemColumns item in databaseToConsult.itens)
                {
                    if (item.GetValue(categorySearchInputs[activeIndexes[0]].text) == categorySearchInputs[activeIndexes[0]].text &&
                        item.GetValue(categorySearchInputs[activeIndexes[1]].text) == categorySearchInputs[activeIndexes[1]].text &&
                        item.GetValue(categorySearchInputs[activeIndexes[2]].text) == categorySearchInputs[activeIndexes[2]].text &&
                        item.GetValue(categorySearchInputs[activeIndexes[3]].text) == categorySearchInputs[activeIndexes[3]].text &&
                        item.GetValue(categorySearchInputs[activeIndexes[4]].text) == categorySearchInputs[activeIndexes[4]].text &&
                        item.GetValue(categorySearchInputs[activeIndexes[5]].text) == categorySearchInputs[activeIndexes[5]].text &&
                        item.GetValue(categorySearchInputs[activeIndexes[6]].text) == categorySearchInputs[activeIndexes[6]].text &&
                        item.GetValue(categorySearchInputs[activeIndexes[7]].text) == categorySearchInputs[activeIndexes[7]].text)
                    {
                        returnSheet.itens.Add(item);
                    }
                }
                break;
            case 9:
                foreach (ItemColumns item in databaseToConsult.itens)
                {
                    if (item.GetValue(categorySearchInputs[activeIndexes[0]].text) == categorySearchInputs[activeIndexes[0]].text &&
                        item.GetValue(categorySearchInputs[activeIndexes[1]].text) == categorySearchInputs[activeIndexes[1]].text &&
                        item.GetValue(categorySearchInputs[activeIndexes[2]].text) == categorySearchInputs[activeIndexes[2]].text &&
                        item.GetValue(categorySearchInputs[activeIndexes[3]].text) == categorySearchInputs[activeIndexes[3]].text &&
                        item.GetValue(categorySearchInputs[activeIndexes[4]].text) == categorySearchInputs[activeIndexes[4]].text &&
                        item.GetValue(categorySearchInputs[activeIndexes[5]].text) == categorySearchInputs[activeIndexes[5]].text &&
                        item.GetValue(categorySearchInputs[activeIndexes[6]].text) == categorySearchInputs[activeIndexes[6]].text &&
                        item.GetValue(categorySearchInputs[activeIndexes[7]].text) == categorySearchInputs[activeIndexes[7]].text &&
                        item.GetValue(categorySearchInputs[activeIndexes[8]].text) == categorySearchInputs[activeIndexes[8]].text)
                    {
                        returnSheet.itens.Add(item);
                    }
                }
                break;
            case 10:
                foreach (ItemColumns item in databaseToConsult.itens)
                {
                    if (item.GetValue(categorySearchInputs[activeIndexes[0]].text) == categorySearchInputs[activeIndexes[0]].text &&
                        item.GetValue(categorySearchInputs[activeIndexes[1]].text) == categorySearchInputs[activeIndexes[1]].text &&
                        item.GetValue(categorySearchInputs[activeIndexes[2]].text) == categorySearchInputs[activeIndexes[2]].text &&
                        item.GetValue(categorySearchInputs[activeIndexes[3]].text) == categorySearchInputs[activeIndexes[3]].text &&
                        item.GetValue(categorySearchInputs[activeIndexes[4]].text) == categorySearchInputs[activeIndexes[4]].text &&
                        item.GetValue(categorySearchInputs[activeIndexes[5]].text) == categorySearchInputs[activeIndexes[5]].text &&
                        item.GetValue(categorySearchInputs[activeIndexes[6]].text) == categorySearchInputs[activeIndexes[6]].text &&
                        item.GetValue(categorySearchInputs[activeIndexes[7]].text) == categorySearchInputs[activeIndexes[7]].text &&
                        item.GetValue(categorySearchInputs[activeIndexes[8]].text) == categorySearchInputs[activeIndexes[8]].text &&
                        item.GetValue(categorySearchInputs[activeIndexes[9]].text) == categorySearchInputs[activeIndexes[9]].text)
                    {
                        returnSheet.itens.Add(item);
                    }
                }
                break;
            case 11:
                foreach (ItemColumns item in databaseToConsult.itens)
                {
                    if (item.GetValue(categorySearchInputs[activeIndexes[0]].text) == categorySearchInputs[activeIndexes[0]].text &&
                        item.GetValue(categorySearchInputs[activeIndexes[1]].text) == categorySearchInputs[activeIndexes[1]].text &&
                        item.GetValue(categorySearchInputs[activeIndexes[2]].text) == categorySearchInputs[activeIndexes[2]].text &&
                        item.GetValue(categorySearchInputs[activeIndexes[3]].text) == categorySearchInputs[activeIndexes[3]].text &&
                        item.GetValue(categorySearchInputs[activeIndexes[4]].text) == categorySearchInputs[activeIndexes[4]].text &&
                        item.GetValue(categorySearchInputs[activeIndexes[5]].text) == categorySearchInputs[activeIndexes[5]].text &&
                        item.GetValue(categorySearchInputs[activeIndexes[6]].text) == categorySearchInputs[activeIndexes[6]].text &&
                        item.GetValue(categorySearchInputs[activeIndexes[7]].text) == categorySearchInputs[activeIndexes[7]].text &&
                        item.GetValue(categorySearchInputs[activeIndexes[8]].text) == categorySearchInputs[activeIndexes[8]].text &&
                        item.GetValue(categorySearchInputs[activeIndexes[9]].text) == categorySearchInputs[activeIndexes[9]].text &&
                        item.GetValue(categorySearchInputs[activeIndexes[10]].text) == categorySearchInputs[activeIndexes[10]].text)
                    {
                        returnSheet.itens.Add(item);
                    }
                }
                break;
            case 12:
                foreach (ItemColumns item in databaseToConsult.itens)
                {
                    if (item.GetValue(categorySearchInputs[activeIndexes[0]].text) == categorySearchInputs[activeIndexes[0]].text &&
                        item.GetValue(categorySearchInputs[activeIndexes[1]].text) == categorySearchInputs[activeIndexes[1]].text &&
                        item.GetValue(categorySearchInputs[activeIndexes[2]].text) == categorySearchInputs[activeIndexes[2]].text &&
                        item.GetValue(categorySearchInputs[activeIndexes[3]].text) == categorySearchInputs[activeIndexes[3]].text &&
                        item.GetValue(categorySearchInputs[activeIndexes[4]].text) == categorySearchInputs[activeIndexes[4]].text &&
                        item.GetValue(categorySearchInputs[activeIndexes[5]].text) == categorySearchInputs[activeIndexes[5]].text &&
                        item.GetValue(categorySearchInputs[activeIndexes[6]].text) == categorySearchInputs[activeIndexes[6]].text &&
                        item.GetValue(categorySearchInputs[activeIndexes[7]].text) == categorySearchInputs[activeIndexes[7]].text &&
                        item.GetValue(categorySearchInputs[activeIndexes[8]].text) == categorySearchInputs[activeIndexes[8]].text &&
                        item.GetValue(categorySearchInputs[activeIndexes[9]].text) == categorySearchInputs[activeIndexes[9]].text &&
                        item.GetValue(categorySearchInputs[activeIndexes[10]].text) == categorySearchInputs[activeIndexes[10]].text &&
                        item.GetValue(categorySearchInputs[activeIndexes[11]].text) == categorySearchInputs[activeIndexes[11]].text)
                    {
                        returnSheet.itens.Add(item);
                    }
                }
                break;
            case 13:
                foreach (ItemColumns item in databaseToConsult.itens)
                {
                    if (item.GetValue(categorySearchInputs[activeIndexes[0]].text) == categorySearchInputs[activeIndexes[0]].text &&
                        item.GetValue(categorySearchInputs[activeIndexes[1]].text) == categorySearchInputs[activeIndexes[1]].text &&
                        item.GetValue(categorySearchInputs[activeIndexes[2]].text) == categorySearchInputs[activeIndexes[2]].text &&
                        item.GetValue(categorySearchInputs[activeIndexes[3]].text) == categorySearchInputs[activeIndexes[3]].text &&
                        item.GetValue(categorySearchInputs[activeIndexes[4]].text) == categorySearchInputs[activeIndexes[4]].text &&
                        item.GetValue(categorySearchInputs[activeIndexes[5]].text) == categorySearchInputs[activeIndexes[5]].text &&
                        item.GetValue(categorySearchInputs[activeIndexes[6]].text) == categorySearchInputs[activeIndexes[6]].text &&
                        item.GetValue(categorySearchInputs[activeIndexes[7]].text) == categorySearchInputs[activeIndexes[7]].text &&
                        item.GetValue(categorySearchInputs[activeIndexes[8]].text) == categorySearchInputs[activeIndexes[8]].text &&
                        item.GetValue(categorySearchInputs[activeIndexes[9]].text) == categorySearchInputs[activeIndexes[9]].text &&
                        item.GetValue(categorySearchInputs[activeIndexes[10]].text) == categorySearchInputs[activeIndexes[10]].text &&
                        item.GetValue(categorySearchInputs[activeIndexes[11]].text) == categorySearchInputs[activeIndexes[11]].text &&
                        item.GetValue(categorySearchInputs[activeIndexes[12]].text) == categorySearchInputs[activeIndexes[12]].text)
                    {
                        returnSheet.itens.Add(item);
                    }
                }
                break;
            case 14:
                foreach (ItemColumns item in databaseToConsult.itens)
                {
                    if (item.GetValue(categorySearchInputs[activeIndexes[0]].text) == categorySearchInputs[activeIndexes[0]].text &&
                        item.GetValue(categorySearchInputs[activeIndexes[1]].text) == categorySearchInputs[activeIndexes[1]].text &&
                        item.GetValue(categorySearchInputs[activeIndexes[2]].text) == categorySearchInputs[activeIndexes[2]].text &&
                        item.GetValue(categorySearchInputs[activeIndexes[3]].text) == categorySearchInputs[activeIndexes[3]].text &&
                        item.GetValue(categorySearchInputs[activeIndexes[4]].text) == categorySearchInputs[activeIndexes[4]].text &&
                        item.GetValue(categorySearchInputs[activeIndexes[5]].text) == categorySearchInputs[activeIndexes[5]].text &&
                        item.GetValue(categorySearchInputs[activeIndexes[6]].text) == categorySearchInputs[activeIndexes[6]].text &&
                        item.GetValue(categorySearchInputs[activeIndexes[7]].text) == categorySearchInputs[activeIndexes[7]].text &&
                        item.GetValue(categorySearchInputs[activeIndexes[8]].text) == categorySearchInputs[activeIndexes[8]].text &&
                        item.GetValue(categorySearchInputs[activeIndexes[9]].text) == categorySearchInputs[activeIndexes[9]].text &&
                        item.GetValue(categorySearchInputs[activeIndexes[10]].text) == categorySearchInputs[activeIndexes[10]].text &&
                        item.GetValue(categorySearchInputs[activeIndexes[11]].text) == categorySearchInputs[activeIndexes[11]].text &&
                        item.GetValue(categorySearchInputs[activeIndexes[12]].text) == categorySearchInputs[activeIndexes[12]].text &&
                        item.GetValue(categorySearchInputs[activeIndexes[13]].text) == categorySearchInputs[activeIndexes[13]].text )
                    {
                        returnSheet.itens.Add(item);
                    }
                }
                break;
            case 15:
                foreach (ItemColumns item in databaseToConsult.itens)
                {
                    if (item.GetValue(categorySearchInputs[activeIndexes[0]].text) == categorySearchInputs[activeIndexes[0]].text &&
                        item.GetValue(categorySearchInputs[activeIndexes[1]].text) == categorySearchInputs[activeIndexes[1]].text &&
                        item.GetValue(categorySearchInputs[activeIndexes[2]].text) == categorySearchInputs[activeIndexes[2]].text &&
                        item.GetValue(categorySearchInputs[activeIndexes[3]].text) == categorySearchInputs[activeIndexes[3]].text &&
                        item.GetValue(categorySearchInputs[activeIndexes[4]].text) == categorySearchInputs[activeIndexes[4]].text &&
                        item.GetValue(categorySearchInputs[activeIndexes[5]].text) == categorySearchInputs[activeIndexes[5]].text &&
                        item.GetValue(categorySearchInputs[activeIndexes[6]].text) == categorySearchInputs[activeIndexes[6]].text &&
                        item.GetValue(categorySearchInputs[activeIndexes[7]].text) == categorySearchInputs[activeIndexes[7]].text &&
                        item.GetValue(categorySearchInputs[activeIndexes[8]].text) == categorySearchInputs[activeIndexes[8]].text &&
                        item.GetValue(categorySearchInputs[activeIndexes[9]].text) == categorySearchInputs[activeIndexes[9]].text &&
                        item.GetValue(categorySearchInputs[activeIndexes[10]].text) == categorySearchInputs[activeIndexes[10]].text &&
                        item.GetValue(categorySearchInputs[activeIndexes[11]].text) == categorySearchInputs[activeIndexes[11]].text &&
                        item.GetValue(categorySearchInputs[activeIndexes[12]].text) == categorySearchInputs[activeIndexes[12]].text &&
                        item.GetValue(categorySearchInputs[activeIndexes[13]].text) == categorySearchInputs[activeIndexes[13]].text &&
                        item.GetValue(categorySearchInputs[activeIndexes[14]].text) == categorySearchInputs[activeIndexes[14]].text)
                    {
                        returnSheet.itens.Add(item);
                    }
                }
                break;
            case 16:
                foreach (ItemColumns item in databaseToConsult.itens)
                {
                    if (item.GetValue(categorySearchInputs[activeIndexes[0]].text) == categorySearchInputs[activeIndexes[0]].text &&
                        item.GetValue(categorySearchInputs[activeIndexes[1]].text) == categorySearchInputs[activeIndexes[1]].text &&
                        item.GetValue(categorySearchInputs[activeIndexes[2]].text) == categorySearchInputs[activeIndexes[2]].text &&
                        item.GetValue(categorySearchInputs[activeIndexes[3]].text) == categorySearchInputs[activeIndexes[3]].text &&
                        item.GetValue(categorySearchInputs[activeIndexes[4]].text) == categorySearchInputs[activeIndexes[4]].text &&
                        item.GetValue(categorySearchInputs[activeIndexes[5]].text) == categorySearchInputs[activeIndexes[5]].text &&
                        item.GetValue(categorySearchInputs[activeIndexes[6]].text) == categorySearchInputs[activeIndexes[6]].text &&
                        item.GetValue(categorySearchInputs[activeIndexes[7]].text) == categorySearchInputs[activeIndexes[7]].text &&
                        item.GetValue(categorySearchInputs[activeIndexes[8]].text) == categorySearchInputs[activeIndexes[8]].text &&
                        item.GetValue(categorySearchInputs[activeIndexes[9]].text) == categorySearchInputs[activeIndexes[9]].text &&
                        item.GetValue(categorySearchInputs[activeIndexes[10]].text) == categorySearchInputs[activeIndexes[10]].text &&
                        item.GetValue(categorySearchInputs[activeIndexes[11]].text) == categorySearchInputs[activeIndexes[11]].text &&
                        item.GetValue(categorySearchInputs[activeIndexes[12]].text) == categorySearchInputs[activeIndexes[12]].text &&
                        item.GetValue(categorySearchInputs[activeIndexes[13]].text) == categorySearchInputs[activeIndexes[13]].text &&
                        item.GetValue(categorySearchInputs[activeIndexes[14]].text) == categorySearchInputs[activeIndexes[14]].text &&
                        item.GetValue(categorySearchInputs[activeIndexes[15]].text) == categorySearchInputs[activeIndexes[15]].text)
                    {
                        returnSheet.itens.Add(item);
                    }
                }
                break;
            case 17:
                foreach (ItemColumns item in databaseToConsult.itens)
                {
                    if (item.GetValue(categorySearchInputs[activeIndexes[0]].text) == categorySearchInputs[activeIndexes[0]].text &&
                        item.GetValue(categorySearchInputs[activeIndexes[1]].text) == categorySearchInputs[activeIndexes[1]].text &&
                        item.GetValue(categorySearchInputs[activeIndexes[2]].text) == categorySearchInputs[activeIndexes[2]].text &&
                        item.GetValue(categorySearchInputs[activeIndexes[3]].text) == categorySearchInputs[activeIndexes[3]].text &&
                        item.GetValue(categorySearchInputs[activeIndexes[4]].text) == categorySearchInputs[activeIndexes[4]].text &&
                        item.GetValue(categorySearchInputs[activeIndexes[5]].text) == categorySearchInputs[activeIndexes[5]].text &&
                        item.GetValue(categorySearchInputs[activeIndexes[6]].text) == categorySearchInputs[activeIndexes[6]].text &&
                        item.GetValue(categorySearchInputs[activeIndexes[7]].text) == categorySearchInputs[activeIndexes[7]].text &&
                        item.GetValue(categorySearchInputs[activeIndexes[8]].text) == categorySearchInputs[activeIndexes[8]].text &&
                        item.GetValue(categorySearchInputs[activeIndexes[9]].text) == categorySearchInputs[activeIndexes[9]].text &&
                        item.GetValue(categorySearchInputs[activeIndexes[10]].text) == categorySearchInputs[activeIndexes[10]].text &&
                        item.GetValue(categorySearchInputs[activeIndexes[11]].text) == categorySearchInputs[activeIndexes[11]].text &&
                        item.GetValue(categorySearchInputs[activeIndexes[12]].text) == categorySearchInputs[activeIndexes[12]].text &&
                        item.GetValue(categorySearchInputs[activeIndexes[13]].text) == categorySearchInputs[activeIndexes[13]].text &&
                        item.GetValue(categorySearchInputs[activeIndexes[14]].text) == categorySearchInputs[activeIndexes[14]].text &&
                        item.GetValue(categorySearchInputs[activeIndexes[15]].text) == categorySearchInputs[activeIndexes[15]].text &&
                        item.GetValue(categorySearchInputs[activeIndexes[16]].text) == categorySearchInputs[activeIndexes[16]].text)
                    {
                        returnSheet.itens.Add(item);
                    }
                }
                break;
            case 18:
                foreach (ItemColumns item in databaseToConsult.itens)
                {
                    if (item.GetValue(categorySearchInputs[activeIndexes[0]].text) == categorySearchInputs[activeIndexes[0]].text &&
                        item.GetValue(categorySearchInputs[activeIndexes[1]].text) == categorySearchInputs[activeIndexes[1]].text &&
                        item.GetValue(categorySearchInputs[activeIndexes[2]].text) == categorySearchInputs[activeIndexes[2]].text &&
                        item.GetValue(categorySearchInputs[activeIndexes[3]].text) == categorySearchInputs[activeIndexes[3]].text &&
                        item.GetValue(categorySearchInputs[activeIndexes[4]].text) == categorySearchInputs[activeIndexes[4]].text &&
                        item.GetValue(categorySearchInputs[activeIndexes[5]].text) == categorySearchInputs[activeIndexes[5]].text &&
                        item.GetValue(categorySearchInputs[activeIndexes[6]].text) == categorySearchInputs[activeIndexes[6]].text &&
                        item.GetValue(categorySearchInputs[activeIndexes[7]].text) == categorySearchInputs[activeIndexes[7]].text &&
                        item.GetValue(categorySearchInputs[activeIndexes[8]].text) == categorySearchInputs[activeIndexes[8]].text &&
                        item.GetValue(categorySearchInputs[activeIndexes[9]].text) == categorySearchInputs[activeIndexes[9]].text &&
                        item.GetValue(categorySearchInputs[activeIndexes[10]].text) == categorySearchInputs[activeIndexes[10]].text &&
                        item.GetValue(categorySearchInputs[activeIndexes[11]].text) == categorySearchInputs[activeIndexes[11]].text &&
                        item.GetValue(categorySearchInputs[activeIndexes[12]].text) == categorySearchInputs[activeIndexes[12]].text &&
                        item.GetValue(categorySearchInputs[activeIndexes[13]].text) == categorySearchInputs[activeIndexes[13]].text &&
                        item.GetValue(categorySearchInputs[activeIndexes[14]].text) == categorySearchInputs[activeIndexes[14]].text &&
                        item.GetValue(categorySearchInputs[activeIndexes[15]].text) == categorySearchInputs[activeIndexes[15]].text &&
                        item.GetValue(categorySearchInputs[activeIndexes[16]].text) == categorySearchInputs[activeIndexes[16]].text &&
                        item.GetValue(categorySearchInputs[activeIndexes[17]].text) == categorySearchInputs[activeIndexes[17]].text)
                    {
                        returnSheet.itens.Add(item);
                    }
                }
                break;
            default:
                break;
        }
        return returnSheet;
    }
}
