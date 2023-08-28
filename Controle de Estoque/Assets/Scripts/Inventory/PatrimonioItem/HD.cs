using Assets.Scripts.Misc;

namespace Assets.Scripts.Inventory.PatrimonioItem
{
    public class HD : PatrimonioItemParent
    {
        public HD()
        {
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