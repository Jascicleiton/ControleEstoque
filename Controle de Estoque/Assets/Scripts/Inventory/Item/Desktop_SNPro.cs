namespace Assets.Scripts.Inventory.Item
{
    public class Desktop_SNPro : Desktop
    {
        protected override void Awake()
        {
            base.Awake();
            allParameters.Add(ConstStrings.ModeloPlacaMae, default);
            allParameters.Add(ConstStrings.Fonte, default);
            allParameters.Add(ConstStrings.PlacaDeVideo, default);
            allParameters.Add(ConstStrings.LeitorDeDvd, default);
        }
    }
}