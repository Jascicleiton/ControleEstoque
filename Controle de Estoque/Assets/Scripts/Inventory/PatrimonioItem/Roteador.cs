using Assets.Scripts.Misc;

namespace Assets.Scripts.Inventory.PatrimonioItem
{
    public class Roteador : PatrimonioItemParent
    {
        public Roteador()
        {
            allParameters.Add(ConstStrings.Wireless, default);
            allParameters.Add(ConstStrings.QuantasEntradas_I, default);
            allParameters.Add(ConstStrings.BandaMax_I, default);
        }
    }
}
