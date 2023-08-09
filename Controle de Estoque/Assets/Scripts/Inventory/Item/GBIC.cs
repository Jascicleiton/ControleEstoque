namespace Assets.Scripts.Inventory.Item
{
    public class GBIC : ItemParent
    {
        protected override void Awake()
        {
            base.Awake();
            allParameters.Add(ConstStrings.DesempenhoMax_I, default);
        }
    }
}