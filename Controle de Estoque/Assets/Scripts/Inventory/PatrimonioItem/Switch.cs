using Assets.Scripts.Misc;

namespace Assets.Scripts.Inventory.PatrimonioItem
{
    public class Switch : PatrimonioItemParent
    {
        public Switch()
        {
            allParameters.Add(ConstStrings.QuantasEQuaisPortas, default);
            allParameters.Add(ConstStrings.CapacidadeMaxCadaPorta, default);
        }
    }
}