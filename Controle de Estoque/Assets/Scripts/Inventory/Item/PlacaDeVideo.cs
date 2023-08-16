namespace Assets.Scripts.Inventory.Item
{
    public class PlacaDeVideo : ItemParent
    {
        protected override void Awake()
        {
            base.Awake();
            allParameters.Add(ConstStrings.QuantasEntradas_I, default);
            allParameters.Add(ConstStrings.QuaisEntradas, default);
        }
    }
}