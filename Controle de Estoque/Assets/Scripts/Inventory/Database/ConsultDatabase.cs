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
    /// Consult if the item exists on the database using the "Patrimônio"
    /// </summary>
    public ItemColumns ConsultPatrimonio(int patrimonioToConsult, Sheet databaseToConsult)
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
    /// Get the index of the item found during a consult on the "Inventário" sheet
    /// </summary>
    public int GetItemIndex()
    {
        return itemIndexFullDatabase;
    }

    /// <summary>
    /// Get the index of the item found during a consult on it's respective category sheet
    /// </summary>
    public int GetCategoryItemIndex(Sheet categoryToConsult, int patrimonio)
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
