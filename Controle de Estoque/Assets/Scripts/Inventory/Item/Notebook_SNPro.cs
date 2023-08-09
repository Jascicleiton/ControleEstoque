namespace Assets.Scripts.Inventory.Item
{
    public class Notebook_SNPro : Notebook
    {
        protected override void Awake()
        {
            base.Awake();
            allParameters.Add(ConstStrings.FonteDeAlimentacao, default);
            allParameters.Add(ConstStrings.Bateria, default);
        }
    }
}