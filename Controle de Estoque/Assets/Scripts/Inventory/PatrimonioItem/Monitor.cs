using Assets.Scripts.Misc;

namespace Assets.Scripts.Inventory.PatrimonioItem
{
    public class Monitor : PatrimonioItemParent
    {
        public Monitor()
        {
            allParameters.Add(ConstStrings.Polegadas, default);
            allParameters.Add(ConstStrings.QuaisEntradas, default);
        }
    }
}