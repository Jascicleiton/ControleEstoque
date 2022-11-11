using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ConsultDatabase : Singleton<ConsultDatabase>
{
    private int itemIndex = 0;
    /// <summary>
    /// Consult if the item exists on the database using the "Serial"
    /// </summary>
    public SheetColumns ConsultSerial(string serialToConsult)
    {
        itemIndex = 0;
        foreach (SheetColumns item in InternalDatabase.fullDatabase.itens)
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
    public SheetColumns ConsultPatrimonio(string patrimonioToConsult)
    {
        itemIndex = 0;
        foreach (SheetColumns item in InternalDatabase.fullDatabase.itens)
        {
            itemIndex++;
            if (item.Patrimonio == patrimonioToConsult)
            {

                return item;
            }
        }
        
        return null;
    }

    public int GetItemIndex()
    {
        return itemIndex;
    }

}
