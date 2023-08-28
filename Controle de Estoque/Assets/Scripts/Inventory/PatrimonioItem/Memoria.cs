using Assets.Scripts.Misc;

namespace Assets.Scripts.Inventory.PatrimonioItem
{
    public class Memoria : PatrimonioItemParent
    {
        public Memoria()
        {
            allParameters.Add(ConstStrings.Tipo, default);
            allParameters.Add(ConstStrings.Capacidade_I, default);
            allParameters.Add(ConstStrings.Velocidade_I, default);
            allParameters.Add(ConstStrings.LowVoltage, default);
            allParameters.Add(ConstStrings.Rank, default);
            allParameters.Add(ConstStrings.DIMM, default);
            allParameters.Add(ConstStrings.TaxaDeTransmissao_I, default);
            allParameters.Add(ConstStrings.Simbolo, default);
        }
    }
}
