using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ConsultDatabase : Singleton<ConsultDatabase>
{
    private int itemIndexFullDatabase = 0;
    private int categoryItemIndex = 0;
    /// <summary>
    /// Consult if the item exists on the database using the "Serial"
    /// </summary>
    public ItemColumns ConsultSerial(string serialToConsult, Sheet databaseToConsult)
    {
        itemIndexFullDatabase = 0;
        foreach (ItemColumns item in databaseToConsult.itens)
        {
            if (item.Serial == serialToConsult)
            {
                return item;
            }
            else
            {
                itemIndexFullDatabase++;
            }
        }

        return null;
    }

    /// <summary>
    /// Consult if the item exists on the database using the "Patrim�nio"
    /// </summary>
    public ItemColumns ConsultPatrimonio(string patrimonioToConsult, Sheet databaseToConsult)
    {
        itemIndexFullDatabase = 0;
        foreach (ItemColumns item in databaseToConsult.itens)
        {
            if (item.Patrimonio == patrimonioToConsult)
            {
                return item;
            }
            else
            {
                itemIndexFullDatabase++;
            }
        }

        return null;
    }

    /// <summary>
    /// Get the index of the item found during a consult
    /// </summary>
    public int GetItemIndex()
    {
        return itemIndexFullDatabase;
    }

    public int GetCategoryItemIndex(Sheet categoryToConsult, string patrimonio)
    {
        categoryItemIndex = 0;
        for (int i = 0; i < categoryToConsult.itens.Count; i++)
        {
            if (categoryToConsult.itens[i].Patrimonio == patrimonio)
            {
                categoryItemIndex = i;
                break;
            }
        }
        return categoryItemIndex;
    }

}
