using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ConsultDatabase : Singleton<ConsultDatabase>
{
    /// <summary>
    /// Consult if the item exists on the database using the "Serial"
    /// </summary>
    public SheetColumns ConsultSerial(string serialToConsult)
    {
        foreach (SheetColumns item in InternalDatabase.fullDatabase.itens)
        {
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
        foreach (SheetColumns item in InternalDatabase.fullDatabase.itens)
        {
            if (item.Patrimonio == patrimonioToConsult)
            {
                return item;
            }
        }
        
        return null;
    }

}
