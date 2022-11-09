using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class ConsultCategory : MonoBehaviour
{
    string parameter1;
    string parameter2;
    string parameter3;
    string parameter4;
    string parameter5;
    string parameter6;
    string parameter7;
    string parameter8;
    string parameter9;
    string parameter10;
    string parameter11;
    string parameter12;
    string parameter13;
    string parameter14;

    public Sheet FindItens(List<int> activeIndexes, TMP_InputField[] categorySearchInputs)
    {
        Sheet returnSheet = new Sheet();
        /// FOr each category do:
        /// for each number of ative indexes do:
        /// check if there is one or more itens that match the search and if there is, add it to the returnSheet.itens
        switch (activeIndexes.Count)
        {
            case 1:
                
                foreach (SheetColumns item in InternalDatabase.fullDatabase.itens)
                {
                    if (item.GetValue(categorySearchInputs[activeIndexes[0]].text) == categorySearchInputs[activeIndexes[0]].text)
                    {
                        returnSheet.itens.Add(item);
                    }
                }
                break;
            case 2:      
                foreach (SheetColumns item in InternalDatabase.fullDatabase.itens)
                {
                    if (item.GetValue(categorySearchInputs[activeIndexes[0]].text) == categorySearchInputs[activeIndexes[0]].text &&
                        item.GetValue(categorySearchInputs[activeIndexes[1]].text) == categorySearchInputs[activeIndexes[1]].text)
                    {
                        returnSheet.itens.Add(item);
                    }
                }
                break;
            case 3:
                parameter1 = ConstStrings.HDSearchParameters[activeIndexes[0]];
                parameter2 = ConstStrings.HDSearchParameters[activeIndexes[1]];
                parameter3 = ConstStrings.HDSearchParameters[activeIndexes[2]];
                foreach (SheetColumns item in InternalDatabase.fullDatabase.itens)
                {

                    if (item.GetValue(parameter1) == categorySearchInputs[activeIndexes[0]].text &&
                        item.GetValue(parameter2) == categorySearchInputs[activeIndexes[1]].text &&
                        item.GetValue(parameter3) == categorySearchInputs[activeIndexes[2]].text)
                    {
                        returnSheet.itens.Add(item);
                    }
                }
                break;
            case 4:
                parameter1 = ConstStrings.HDSearchParameters[activeIndexes[0]];
                parameter2 = ConstStrings.HDSearchParameters[activeIndexes[1]];
                parameter3 = ConstStrings.HDSearchParameters[activeIndexes[2]];
                parameter4 = ConstStrings.HDSearchParameters[activeIndexes[3]];
                foreach (SheetColumns item in InternalDatabase.fullDatabase.itens)
                {
                    if (item.GetValue(parameter1) == categorySearchInputs[activeIndexes[0]].text &&
                        item.GetValue(parameter2) == categorySearchInputs[activeIndexes[1]].text &&
                        item.GetValue(parameter3) == categorySearchInputs[activeIndexes[2]].text &&
                        item.GetValue(parameter4) == categorySearchInputs[activeIndexes[3]].text)
                    {
                        returnSheet.itens.Add(item);
                    }
                }
                break;
            case 5:
                parameter1 = ConstStrings.HDSearchParameters[activeIndexes[0]];
                parameter2 = ConstStrings.HDSearchParameters[activeIndexes[1]];
                parameter3 = ConstStrings.HDSearchParameters[activeIndexes[2]];
                parameter4 = ConstStrings.HDSearchParameters[activeIndexes[3]];
                parameter5 = ConstStrings.HDSearchParameters[activeIndexes[4]];
                foreach (SheetColumns item in InternalDatabase.fullDatabase.itens)
                {
                    if (item.GetValue(parameter1) == categorySearchInputs[activeIndexes[0]].text &&
                        item.GetValue(parameter2) == categorySearchInputs[activeIndexes[1]].text &&
                        item.GetValue(parameter3) == categorySearchInputs[activeIndexes[2]].text &&
                        item.GetValue(parameter4) == categorySearchInputs[activeIndexes[3]].text &&
                        item.GetValue(parameter5) == categorySearchInputs[activeIndexes[4]].text)
                    {
                        returnSheet.itens.Add(item);
                    }
                }
                break;
            case 6:
                parameter1 = ConstStrings.HDSearchParameters[activeIndexes[0]];
                parameter2 = ConstStrings.HDSearchParameters[activeIndexes[1]];
                parameter3 = ConstStrings.HDSearchParameters[activeIndexes[2]];
                parameter4 = ConstStrings.HDSearchParameters[activeIndexes[3]];
                parameter5 = ConstStrings.HDSearchParameters[activeIndexes[4]];
                parameter6 = ConstStrings.HDSearchParameters[activeIndexes[5]];
                foreach (SheetColumns item in InternalDatabase.fullDatabase.itens)
                {
                    if (item.GetValue(parameter1) == categorySearchInputs[activeIndexes[0]].text &&
                        item.GetValue(parameter2) == categorySearchInputs[activeIndexes[1]].text &&
                        item.GetValue(parameter3) == categorySearchInputs[activeIndexes[2]].text &&
                        item.GetValue(parameter4) == categorySearchInputs[activeIndexes[3]].text &&
                        item.GetValue(parameter5) == categorySearchInputs[activeIndexes[4]].text &&
                        item.GetValue(parameter6) == categorySearchInputs[activeIndexes[5]].text)
                    {
                        returnSheet.itens.Add(item);
                    }
                }
                break;
            case 7:
                parameter1 = ConstStrings.HDSearchParameters[activeIndexes[0]];
                parameter2 = ConstStrings.HDSearchParameters[activeIndexes[1]];
                parameter3 = ConstStrings.HDSearchParameters[activeIndexes[2]];
                parameter4 = ConstStrings.HDSearchParameters[activeIndexes[3]];
                parameter5 = ConstStrings.HDSearchParameters[activeIndexes[4]];
                parameter6 = ConstStrings.HDSearchParameters[activeIndexes[5]];
                parameter7 = ConstStrings.HDSearchParameters[activeIndexes[6]];
                foreach (SheetColumns item in InternalDatabase.fullDatabase.itens)
                {
                    if (item.GetValue(parameter1) == categorySearchInputs[activeIndexes[0]].text &&
                        item.GetValue(parameter2) == categorySearchInputs[activeIndexes[1]].text &&
                        item.GetValue(parameter3) == categorySearchInputs[activeIndexes[2]].text &&
                        item.GetValue(parameter4) == categorySearchInputs[activeIndexes[3]].text &&
                        item.GetValue(parameter5) == categorySearchInputs[activeIndexes[4]].text &&
                        item.GetValue(parameter6) == categorySearchInputs[activeIndexes[5]].text &&
                        item.GetValue(parameter7) == categorySearchInputs[activeIndexes[6]].text)
                    {
                        returnSheet.itens.Add(item);
                    }
                }
                break;
            case 8:
                parameter1 = ConstStrings.HDSearchParameters[activeIndexes[0]];
                parameter2 = ConstStrings.HDSearchParameters[activeIndexes[1]];
                parameter3 = ConstStrings.HDSearchParameters[activeIndexes[2]];
                parameter4 = ConstStrings.HDSearchParameters[activeIndexes[3]];
                parameter5 = ConstStrings.HDSearchParameters[activeIndexes[4]];
                parameter6 = ConstStrings.HDSearchParameters[activeIndexes[5]];
                parameter7 = ConstStrings.HDSearchParameters[activeIndexes[6]];
                parameter8 = ConstStrings.HDSearchParameters[activeIndexes[7]];
                foreach (SheetColumns item in InternalDatabase.fullDatabase.itens)
                {
                    if (item.GetValue(parameter1) == categorySearchInputs[activeIndexes[0]].text &&
                        item.GetValue(parameter2) == categorySearchInputs[activeIndexes[1]].text &&
                        item.GetValue(parameter3) == categorySearchInputs[activeIndexes[2]].text &&
                        item.GetValue(parameter4) == categorySearchInputs[activeIndexes[3]].text &&
                        item.GetValue(parameter5) == categorySearchInputs[activeIndexes[4]].text &&
                        item.GetValue(parameter6) == categorySearchInputs[activeIndexes[5]].text &&
                        item.GetValue(parameter7) == categorySearchInputs[activeIndexes[6]].text &&
                        item.GetValue(parameter8) == categorySearchInputs[activeIndexes[7]].text)
                    {
                        returnSheet.itens.Add(item);
                    }
                }
                break;
            case 9:
                parameter1 = ConstStrings.HDSearchParameters[activeIndexes[0]];
                parameter2 = ConstStrings.HDSearchParameters[activeIndexes[1]];
                parameter3 = ConstStrings.HDSearchParameters[activeIndexes[2]];
                parameter4 = ConstStrings.HDSearchParameters[activeIndexes[3]];
                parameter5 = ConstStrings.HDSearchParameters[activeIndexes[4]];
                parameter6 = ConstStrings.HDSearchParameters[activeIndexes[5]];
                parameter7 = ConstStrings.HDSearchParameters[activeIndexes[6]];
                parameter8 = ConstStrings.HDSearchParameters[activeIndexes[7]];
                parameter9 = ConstStrings.HDSearchParameters[activeIndexes[8]];
                foreach (SheetColumns item in InternalDatabase.fullDatabase.itens)
                {
                    if (item.GetValue(parameter1) == categorySearchInputs[activeIndexes[0]].text &&
                        item.GetValue(parameter2) == categorySearchInputs[activeIndexes[1]].text &&
                        item.GetValue(parameter3) == categorySearchInputs[activeIndexes[2]].text &&
                        item.GetValue(parameter4) == categorySearchInputs[activeIndexes[3]].text &&
                        item.GetValue(parameter5) == categorySearchInputs[activeIndexes[4]].text &&
                        item.GetValue(parameter6) == categorySearchInputs[activeIndexes[5]].text &&
                        item.GetValue(parameter7) == categorySearchInputs[activeIndexes[6]].text &&
                        item.GetValue(parameter8) == categorySearchInputs[activeIndexes[7]].text &&
                        item.GetValue(parameter9) == categorySearchInputs[activeIndexes[8]].text)
                    {
                        returnSheet.itens.Add(item);
                    }
                }
                break;
            case 10:
                parameter1 = ConstStrings.HDSearchParameters[activeIndexes[0]];
                parameter2 = ConstStrings.HDSearchParameters[activeIndexes[1]];
                parameter3 = ConstStrings.HDSearchParameters[activeIndexes[2]];
                parameter4 = ConstStrings.HDSearchParameters[activeIndexes[3]];
                parameter5 = ConstStrings.HDSearchParameters[activeIndexes[4]];
                parameter6 = ConstStrings.HDSearchParameters[activeIndexes[5]];
                parameter7 = ConstStrings.HDSearchParameters[activeIndexes[6]];
                parameter8 = ConstStrings.HDSearchParameters[activeIndexes[7]];
                parameter9 = ConstStrings.HDSearchParameters[activeIndexes[8]];
                parameter10 = ConstStrings.HDSearchParameters[activeIndexes[9]];
                foreach (SheetColumns item in InternalDatabase.fullDatabase.itens)
                {
                    if (item.GetValue(parameter1) == categorySearchInputs[activeIndexes[0]].text &&
                        item.GetValue(parameter2) == categorySearchInputs[activeIndexes[1]].text &&
                        item.GetValue(parameter3) == categorySearchInputs[activeIndexes[2]].text &&
                        item.GetValue(parameter4) == categorySearchInputs[activeIndexes[3]].text &&
                        item.GetValue(parameter5) == categorySearchInputs[activeIndexes[4]].text &&
                        item.GetValue(parameter6) == categorySearchInputs[activeIndexes[5]].text &&
                        item.GetValue(parameter7) == categorySearchInputs[activeIndexes[6]].text &&
                        item.GetValue(parameter8) == categorySearchInputs[activeIndexes[7]].text &&
                        item.GetValue(parameter9) == categorySearchInputs[activeIndexes[8]].text &&
                        item.GetValue(parameter10) == categorySearchInputs[activeIndexes[9]].text)
                    {
                        returnSheet.itens.Add(item);
                    }
                }
                break;
            case 11:
                parameter1 = ConstStrings.HDSearchParameters[activeIndexes[0]];
                parameter2 = ConstStrings.HDSearchParameters[activeIndexes[1]];
                parameter3 = ConstStrings.HDSearchParameters[activeIndexes[2]];
                parameter4 = ConstStrings.HDSearchParameters[activeIndexes[3]];
                parameter5 = ConstStrings.HDSearchParameters[activeIndexes[4]];
                parameter6 = ConstStrings.HDSearchParameters[activeIndexes[5]];
                parameter7 = ConstStrings.HDSearchParameters[activeIndexes[6]];
                parameter8 = ConstStrings.HDSearchParameters[activeIndexes[7]];
                parameter9 = ConstStrings.HDSearchParameters[activeIndexes[8]];
                parameter10 = ConstStrings.HDSearchParameters[activeIndexes[9]];
                parameter11 = ConstStrings.HDSearchParameters[activeIndexes[10]];
                foreach (SheetColumns item in InternalDatabase.fullDatabase.itens)
                {
                    if (item.GetValue(parameter1) == categorySearchInputs[activeIndexes[0]].text &&
                        item.GetValue(parameter2) == categorySearchInputs[activeIndexes[1]].text &&
                        item.GetValue(parameter3) == categorySearchInputs[activeIndexes[2]].text &&
                        item.GetValue(parameter4) == categorySearchInputs[activeIndexes[3]].text &&
                        item.GetValue(parameter5) == categorySearchInputs[activeIndexes[4]].text &&
                        item.GetValue(parameter6) == categorySearchInputs[activeIndexes[5]].text &&
                        item.GetValue(parameter7) == categorySearchInputs[activeIndexes[6]].text &&
                        item.GetValue(parameter8) == categorySearchInputs[activeIndexes[7]].text &&
                        item.GetValue(parameter9) == categorySearchInputs[activeIndexes[8]].text &&
                        item.GetValue(parameter10) == categorySearchInputs[activeIndexes[9]].text &&
                        item.GetValue(parameter11) == categorySearchInputs[activeIndexes[10]].text)
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
