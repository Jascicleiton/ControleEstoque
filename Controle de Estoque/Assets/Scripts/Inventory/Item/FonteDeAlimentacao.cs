namespace Assets.Scripts.Inventory.Item
{
    public class FonteDeAlimentacao : ItemParent
    {
        protected override void Awake()
        {
            base.Awake();
            allParameters.Add(ConstStrings.OndeFunciona, default);
            allParameters.Add(ConstStrings.Voltagem_D, default);
            allParameters.Add(ConstStrings.Amperagem_D, default);
        }
    }
}