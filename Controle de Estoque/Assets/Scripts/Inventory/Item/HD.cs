namespace Assets.Scripts.Inventory.Item
{
    public class HD : ItemParent
    {
        protected override void Awake()
        {
            base.Awake();
            allParameters.Add(ConstStrings.Interface, default);
            allParameters.Add(ConstStrings.Tamanho_D, default);
            allParameters.Add(ConstStrings.FormaDeArmazenamento, default);
            allParameters.Add(ConstStrings.Capacidade_I, default);
            allParameters.Add(ConstStrings.RPM_I, default);
            allParameters.Add(ConstStrings.VelocidadeDeLeitura_I, default);
            allParameters.Add(ConstStrings.Enterprise, default);
        }
    }
}