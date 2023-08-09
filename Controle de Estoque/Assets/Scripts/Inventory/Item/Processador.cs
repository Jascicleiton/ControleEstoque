namespace Assets.Scripts.Inventory.Item
{
    public class Processador : ItemParent
    {
        protected override void Awake()
        {
            base.Awake();
            allParameters.Add(ConstStrings.Soquete, default);
            allParameters.Add(ConstStrings.NucleosFisicos_I, default);
            allParameters.Add(ConstStrings.NucleosLogicos_I, default);
        }
    }
}