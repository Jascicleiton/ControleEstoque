namespace Assets.Scripts.Inventory.Item
{
    public class Monitor : ItemParent
    {
        protected override void Awake()
        {
            base.Awake();
            allParameters.Add(ConstStrings.Polegadas, default);
            allParameters.Add(ConstStrings.QuaisEntradas, default);
        }
    }
}