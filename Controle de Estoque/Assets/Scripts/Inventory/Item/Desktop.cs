namespace Assets.Scripts.Inventory.Item
{
    public class Desktop : ItemParent
    {
        protected override void Awake()
        {
            base.Awake();
            allParameters.Add(ConstStrings.Processador, default);
            allParameters.Add(ConstStrings.Hd, default);
            allParameters.Add(ConstStrings.Memoria, default);
            allParameters.Add(ConstStrings.QualWindows, default);
        }
    }
}