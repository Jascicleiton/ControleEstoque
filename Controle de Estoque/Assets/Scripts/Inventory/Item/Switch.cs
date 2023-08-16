namespace Assets.Scripts.Inventory.Item
{
    public class Switch : ItemParent
    {
        protected override void Awake()
        {
            base.Awake();
            allParameters.Add(ConstStrings.QuantasEQuaisPortas, default);
            allParameters.Add(ConstStrings.CapacidadeMaxCadaPorta, default);
        }
    }
}