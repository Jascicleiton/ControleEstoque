using System.Collections.Generic;

namespace Assets.Scripts.Inventory
{

    /// <summary>
    /// Each instance of this class is a different table from the sql database, with different informations
    /// </summary>
    [System.Serializable]
    public class Sheet
    {
        public List<ItemColumns> itens = new List<ItemColumns>();
    }
}