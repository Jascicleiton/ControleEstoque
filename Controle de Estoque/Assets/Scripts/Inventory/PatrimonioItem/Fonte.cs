using Assets.Scripts.Misc;

namespace Assets.Scripts.Inventory.PatrimonioItem
{
    public class Fonte : PatrimonioItemParent
    {
        public Fonte()
        {
            allParameters.Add(ConstStrings.Watts_I, default);
            allParameters.Add(ConstStrings.OndeFunciona, default);
            allParameters.Add(ConstStrings.Conectores, default);
        }
    }
}