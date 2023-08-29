using Assets.Scripts.Misc;

namespace Assets.Scripts.Inventory.PatrimonioItem
{
    public class Servidor : PatrimonioItemParent
    {
        public Servidor()
        {
            allParameters.Add(ConstStrings.Hd, default);
            allParameters.Add(ConstStrings.Memoria, default);
            allParameters.Add(ConstStrings.Processador, default);
            allParameters.Add(ConstStrings.QualSistemaOperacional, default);
        }
    }
}