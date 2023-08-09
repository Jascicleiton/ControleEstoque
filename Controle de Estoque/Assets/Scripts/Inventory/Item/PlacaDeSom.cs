namespace Assets.Scripts.Inventory.Item
{
    public class PlacaDeSom : ItemParent
    {
        protected override void Awake()
        {
            base.Awake();
            allParameters.Add(ConstStrings.QuantosCanais_I, default);
        }
    }
}
