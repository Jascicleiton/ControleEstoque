namespace Assets.Scripts.Inventory.Database
{
    public class ConsultDatabase
    {
        private int _itemIndexFullDatabase = 0;
        private int _categoryItemIndex = 0;

        /// <summary>
        /// Consult if the item exists on the database using the "Serial"
        /// </summary>
        public ItemColumns ConsultSerial(string serialToConsult, Sheet databaseToConsult)
        {
            _itemIndexFullDatabase = 0;
            foreach (ItemColumns item in databaseToConsult.itens)
            {
                if (item.Serial == serialToConsult)
                {
                    return item;
                }
                else
                {
                    _itemIndexFullDatabase++;
                }
            }

            return null;
        }

        /// <summary>
        /// Consult if the item exists on the database using the "Patrimônio"
        /// </summary>
        public ItemColumns ConsultPatrimonio(int patrimonioToConsult, Sheet databaseToConsult)
        {
            _itemIndexFullDatabase = 0;
            foreach (ItemColumns item in databaseToConsult.itens)
            {
                if (item.Patrimonio == patrimonioToConsult)
                {
                    return item;
                }
                else
                {
                    _itemIndexFullDatabase++;
                }
            }
            return null;
        }

        /// <summary>
        /// Get the index of the item found during a consult on the "Inventário" sheet
        /// </summary>
        public int GetItemIndex()
        {
            return _itemIndexFullDatabase;
        }

        /// <summary>
        /// Get the index of the item found during a consult on it's respective category sheet
        /// </summary>
        public int GetCategoryItemIndex(Sheet categoryToConsult, int patrimonio)
        {
            _categoryItemIndex = 0;
            for (int i = 0; i < categoryToConsult.itens.Count; i++)
            {
                if (categoryToConsult.itens[i].Patrimonio == patrimonio)
                {
                    _categoryItemIndex = i;
                    break;
                }
            }
            return _categoryItemIndex;
        }

    }
}