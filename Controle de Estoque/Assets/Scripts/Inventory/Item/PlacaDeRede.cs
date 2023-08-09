namespace Assets.Scripts.Inventory.Item
{
    public class PlacaDeRede : ItemParent
    {
        protected override void Awake()
        {
            base.Awake();
            allParameters.Add(ConstStrings.Interface, default);
            allParameters.Add(ConstStrings.QuantasPortas_I, default);
            allParameters.Add(ConstStrings.QuaisPortas, default);
            allParameters.Add(ConstStrings.SuportaFibra, default);
            allParameters.Add(ConstStrings.DesempenhoMax_I, default);
        }
    }
}