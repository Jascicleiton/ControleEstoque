using Assets.Scripts.Inventory.PatrimonioItem;

namespace Assets.Scripts.Inventory.Database
{
    public interface IInternalDatabase
    {
        void AddTemporaryPatrimonioItem(PatrimonioItemParent item);
        void AddPatrimonioItem(PatrimonioItemParent item);
    }
}