using Assets.Scripts.Misc;

namespace Assets.Scripts.Inventory.PatrimonioItem
{
    public class Processador : PatrimonioItemParent
    {
        public Processador()
        {
            allParameters.Add(ConstStrings.Soquete, default);
            allParameters.Add(ConstStrings.NucleosFisicos_I, default);
            allParameters.Add(ConstStrings.NucleosLogicos_I, default);
        }
    }
}