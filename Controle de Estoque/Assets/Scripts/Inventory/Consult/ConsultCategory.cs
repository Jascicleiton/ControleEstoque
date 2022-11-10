using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class ConsultCategory : MonoBehaviour
{
    public Sheet FindItens(List<int> activeIndexes, TMP_InputField[] categorySearchInputs, Sheet databaseToConsult)
    {
        Sheet returnSheet = new Sheet();
        
        switch (activeIndexes.Count)
        {
            case 1:
                foreach (SheetColumns item in databaseToConsult.itens)
                {
                    if (item.GetValue(categorySearchInputs[activeIndexes[0]].text) == categorySearchInputs[activeIndexes[0]].text)
                    {
                        returnSheet.itens.Add(item);
                    }
                }
                break;
            case 2:
                foreach (SheetColumns item in databaseToConsult.itens)
                {
                    if (item.GetValue(categorySearchInputs[activeIndexes[0]].text) == categorySearchInputs[activeIndexes[0]].text &&
                        item.GetValue(categorySearchInputs[activeIndexes[1]].text) == categorySearchInputs[activeIndexes[1]].text)
                    {
                        returnSheet.itens.Add(item);
                    }
                }
                break;
            case 3:
                foreach (SheetColumns item in databaseToConsult.itens)
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
                foreach (SheetColumns item in databaseToConsult.itens)
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
                foreach (SheetColumns item in databaseToConsult.itens)
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
                foreach (SheetColumns item in databaseToConsult.itens)
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
                foreach (SheetColumns item in databaseToConsult.itens)
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
                foreach (SheetColumns item in databaseToConsult.itens)
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
                foreach (SheetColumns item in databaseToConsult.itens)
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
                foreach (SheetColumns item in databaseToConsult.itens)
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
                foreach (SheetColumns item in databaseToConsult.itens)
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
            default:
                break;
        }
        return returnSheet;
    }
}
