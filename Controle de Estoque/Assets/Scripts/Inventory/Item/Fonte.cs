namespace Assets.Scripts.Inventory.Item
{
    public class Fonte : ItemParent
    {
        protected override void Awake()
        {
            base.Awake();
            allParameters.Add(ConstStrings.Watts_I, default);
            allParameters.Add(ConstStrings.OndeFunciona, default);
            allParameters.Add(ConstStrings.Conectores, default);
        }
    }
}