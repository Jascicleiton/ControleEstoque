namespace Assets.Scripts.Inventory.Item
{
    public class PlacaControladora : ItemParent
    {
        protected override void Awake()
        {
            base.Awake();
            allParameters.Add(ConstStrings.TipoDeConexao, default);
            allParameters.Add(ConstStrings.QuantasPortas_I, default);
            allParameters.Add(ConstStrings.TiposDeRaid, default);
            allParameters.Add(ConstStrings.CapacidadeMaxHD_I, default);
            allParameters.Add(ConstStrings.AteQuantosHds_I, default);
            allParameters.Add(ConstStrings.BateriaInclusa, default);
        }
    }
}