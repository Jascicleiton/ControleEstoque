using Assets.Scripts.Misc;

namespace Assets.Scripts.Inventory.PatrimonioItem
{
    public class FonteDeAlimentacao : PatrimonioItemParent
    {
        public FonteDeAlimentacao()
        {
            allParameters.Add(ConstStrings.OndeFunciona, default);
            allParameters.Add(ConstStrings.Voltagem_D, default);
            allParameters.Add(ConstStrings.Amperagem_D, default);
        }
    }
}