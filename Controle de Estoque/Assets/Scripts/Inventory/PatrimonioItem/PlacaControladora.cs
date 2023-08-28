using Assets.Scripts.Misc;

namespace Assets.Scripts.Inventory.PatrimonioItem
{
    public class PlacaControladora : PatrimonioItemParent
    {
        public PlacaControladora()
        {
            allParameters.Add(ConstStrings.TipoDeConexao, default);
            allParameters.Add(ConstStrings.QuantasPortas_I, default);
            allParameters.Add(ConstStrings.TiposDeRaid, default);
            allParameters.Add(ConstStrings.CapacidadeMaxHD_I, default);
            allParameters.Add(ConstStrings.AteQuantosHds_I, default);
            allParameters.Add(ConstStrings.BateriaInclusa, default);
        }
    }
}