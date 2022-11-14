using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ConsultDatabase : Singleton<ConsultDatabase>
{
    private int itemIndex = 0;
    /// <summary>
    /// Consult if the item exists on the database using the "Serial"
    /// </summary>
    public ItemColumns ConsultSerial(string serialToConsult, Sheet databaseToConsult)
    {
        itemIndex = 0;
        foreach (ItemColumns item in databaseToConsult.itens)
        {
            itemIndex++;
            if (item.Serial == serialToConsult)
            {
                                return item;
            }
        }
       
        return null;
    }

    /// <summary>
    /// Consult if the item exists on the database using the "Patrimônio"
    /// </summary>
    public ItemColumns ConsultPatrimonio(string patrimonioToConsult, Sheet databaseToConsult)
    {
        itemIndex = 0;
        foreach (ItemColumns item in databaseToConsult.itens)
        {
            itemIndex++;
            if (item.Patrimonio == patrimonioToConsult)
            {

                return item;
            }
        }
        
        return null;
    }

    /// <summary>
    /// Get the index of the item found during a consult
    /// </summary>
    public int GetItemIndex()
    {
        return itemIndex;
    }

}
