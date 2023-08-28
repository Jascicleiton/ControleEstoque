using Assets.Scripts.Misc;

namespace Assets.Scripts.Inventory.PatrimonioItem
{
    public class Idrac : PatrimonioItemParent
    {
        public Idrac()
        {
            allParameters.Add(ConstStrings.Porta, default);
            allParameters.Add(ConstStrings.Velocidade_I, default);
            allParameters.Add(ConstStrings.EntradaSD, default);
            allParameters.Add(ConstStrings.ServidoresSuportados, default);
        }
    }
}
