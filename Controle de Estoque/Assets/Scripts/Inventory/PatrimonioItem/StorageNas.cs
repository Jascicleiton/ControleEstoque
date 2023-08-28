using Assets.Scripts.Misc;

namespace Assets.Scripts.Inventory.PatrimonioItem
{
    public class StorageNas : PatrimonioItemParent
    {
        public StorageNas()
        {
            allParameters.Add(ConstStrings.TamanhoDosHds, default);
            allParameters.Add(ConstStrings.TiposDeRaid, default);
            allParameters.Add(ConstStrings.TiposDeHd, default);
            allParameters.Add(ConstStrings.CapacidadeMaxHD_I, default);
            allParameters.Add(ConstStrings.AteQuantosHds_I, default);
        }
    }
}