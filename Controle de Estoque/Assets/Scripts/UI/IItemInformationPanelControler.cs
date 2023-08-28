using Assets.Scripts.Inventory;
using System.Collections.Generic;

namespace Assets.Scripts.UI
{
    public interface IItemInformationPanelControler
    {
        void DisableItemsForAdd(string category);
        List<string> GetCategoryValues(string category);
        List<string> GetInventoryValues();
        int GetNumberOfActiveBoxes();
        void ResetItems();
        void ResetValues();
        void ShowCategoryItemTemplate(string category);
        void ShowItem(ItemColumns itemToShow);
        void ShowItemConsult(ItemColumns itemToShow);
    }
}