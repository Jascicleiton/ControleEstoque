namespace Assets.Scripts.Inventory.Item
{
    public class StorageNas : ItemParent
    {
        protected override void Awake()
        {
            base.Awake();
            allParameters.Add(ConstStrings.TamanhoDosHds, default);
            allParameters.Add(ConstStrings.TiposDeRaid, default);
            allParameters.Add(ConstStrings.TiposDeHd, default);
            allParameters.Add(ConstStrings.CapacidadeMaxHD_I, default);
            allParameters.Add(ConstStrings.AteQuantosHds_I, default);
        }
    }
}