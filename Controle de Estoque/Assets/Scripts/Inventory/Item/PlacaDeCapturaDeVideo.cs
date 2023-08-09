namespace Assets.Scripts.Inventory.Item
{
    public class PlacaDeCapturaDeVideo : ItemParent
    {
        protected override void Awake()
        {
            base.Awake();
            allParameters.Add(ConstStrings.QuantasEntradas_I, default);
        }
    }
}