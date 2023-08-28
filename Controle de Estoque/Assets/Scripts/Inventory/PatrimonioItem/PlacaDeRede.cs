using Assets.Scripts.Misc;

namespace Assets.Scripts.Inventory.PatrimonioItem
{
    public class PlacaDeRede : PatrimonioItemParent
    {
        public PlacaDeRede()
        {
            allParameters.Add(ConstStrings.Interface, default);
            allParameters.Add(ConstStrings.QuantasPortas_I, default);
            allParameters.Add(ConstStrings.QuaisPortas, default);
            allParameters.Add(ConstStrings.SuportaFibra, default);
            allParameters.Add(ConstStrings.DesempenhoMax_I, default);
        }
    }
}