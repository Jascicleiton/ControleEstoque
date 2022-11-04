using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ConsultInventory : MonoBehaviour
{
    [SerializeField] Image[] informationItens;
    [SerializeField] TMP_Text[] itensTexts;
    [SerializeField] TMP_Dropdown categoryDP; // drop down used to search for an item category
    [SerializeField] TMP_InputField inputField; // field use to type the item "Patrimônio" or the item "Serial"

    private Sheet SearchDetailsSheet(string categoryName)
    {
        Sheet sheetToReturn = new Sheet();
        switch (categoryName)
        {
            case ConstStrings.HD:
                InternalDatabase.splitDatabase.TryGetValue(ConstStrings.HD, out sheetToReturn);
                break;
            case ConstStrings.Memoria:
                InternalDatabase.splitDatabase.TryGetValue(ConstStrings.Memoria, out sheetToReturn);
                break;
            case ConstStrings.PlacaDeRede:
                InternalDatabase.splitDatabase.TryGetValue(ConstStrings.PlacaDeRede, out sheetToReturn);
                break;
            case ConstStrings.Idrac:
                InternalDatabase.splitDatabase.TryGetValue(ConstStrings.Idrac, out sheetToReturn);
                break;
            case ConstStrings.PlacaControladora:
                InternalDatabase.splitDatabase.TryGetValue(ConstStrings.PlacaControladora, out sheetToReturn);
                break;
            case ConstStrings.Processador:
                InternalDatabase.splitDatabase.TryGetValue(ConstStrings.Processador, out sheetToReturn);
                break;
            case ConstStrings.Desktop:
                InternalDatabase.splitDatabase.TryGetValue(ConstStrings.Desktop, out sheetToReturn);
                break;
            case ConstStrings.Fonte:
                InternalDatabase.splitDatabase.TryGetValue(ConstStrings.Fonte, out sheetToReturn);
                break;
            case ConstStrings.Switch:
                InternalDatabase.splitDatabase.TryGetValue(ConstStrings.Switch, out sheetToReturn);
                break;
            case ConstStrings.Roteador:
                InternalDatabase.splitDatabase.TryGetValue(ConstStrings.Roteador, out sheetToReturn);
                break;
            case ConstStrings.Carregador:
                InternalDatabase.splitDatabase.TryGetValue(ConstStrings.Carregador, out sheetToReturn);
                break;
            case ConstStrings.AdaptadorAC:
                InternalDatabase.splitDatabase.TryGetValue(ConstStrings.AdaptadorAC, out sheetToReturn);
                break;
            case ConstStrings.StorageNAS:
                InternalDatabase.splitDatabase.TryGetValue(ConstStrings.StorageNAS, out sheetToReturn);
                break;
            case ConstStrings.Gbic:
                InternalDatabase.splitDatabase.TryGetValue(ConstStrings.Gbic, out sheetToReturn);
                break;
            case ConstStrings.PlacaDeVideo:
                InternalDatabase.splitDatabase.TryGetValue(ConstStrings.PlacaDeVideo, out sheetToReturn);
                break;
            case ConstStrings.PlacaDeSom:
                InternalDatabase.splitDatabase.TryGetValue(ConstStrings.PlacaDeSom, out sheetToReturn);
                break;
            case ConstStrings.PlacaDeCapturaDeVideo:
                InternalDatabase.splitDatabase.TryGetValue(ConstStrings.PlacaDeCapturaDeVideo, out sheetToReturn);
                break;
            default:
                break;
        }
        return sheetToReturn;
    }

    private List<string[]> ListToReturn(SheetColumns item, Sheet detailsShet)
    {
        List<string>[] tempList = new List<string>[2];
        List<string> columnNames = new List<string>();
        List<string> columnValues = new List<string>();



        return null;
    }
    /// <summary>
    /// Consult the inventory using the "Serial"
    /// </summary>
    public List<string[]> Consult(string serialToConsult)
    {
        // search for the serial on the "Inventário SnPro" sheet/database
        Sheet sheetToConsult = new Sheet();
        InternalDatabase.splitDatabase.TryGetValue(ConstStrings.InventarioSnPro, out sheetToConsult);
        SheetColumns itemFound = new SheetColumns();
        foreach (SheetColumns itemToConsult in sheetToConsult.itens)
        {
            if (itemToConsult.Serial == serialToConsult)
            {
                itemFound = itemToConsult;
            }
        }

        // search for the details sheet based on the "Categoria"
        Sheet detailsheetToConsult = SearchDetailsSheet(itemFound.Categoria);

        // return the value as a list of string arrays

        return ListToReturn(itemFound, detailsheetToConsult);
    }

    /// <summary>
    /// Consult the inventory using the "Patrimônio"
    /// </summary>
    public void Consult(int patrimonioToConsult)
    {

    }

    /// <summary>
    /// Consult the inventory using the parameters chosen from each category
    /// </summary>
    public void Consult(string[] paramentersNames, string[] parametersValues)
    {

    }
}