namespace Assets.Scripts.Inventory.Item
{
    public class Roteador : ItemParent
    {
        protected override void Awake()
        {
            base.Awake();
            allParameters.Add(ConstStrings.Wireless, default);
            allParameters.Add(ConstStrings.QuantasEntradas_I, default);
            allParameters.Add(ConstStrings.BandaMax_I, default);
        }
    }
}
