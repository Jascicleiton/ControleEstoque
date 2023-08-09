namespace Assets.Scripts.Inventory.Item
{
    public class PlacaSas : ItemParent
    {
        protected override void Awake()
        {
            base.Awake();
            allParameters.Add(ConstStrings.QuantasEntradas_I, default);
            allParameters.Add(ConstStrings.Velocidade_I, default);
        }
    }
}
