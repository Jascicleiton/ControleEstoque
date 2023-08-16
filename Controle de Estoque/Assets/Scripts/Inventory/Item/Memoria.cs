using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assets.Scripts.Inventory.Item
{
    public class Memoria : ItemParent
    {
        protected override void Awake()
        {
            base.Awake();
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
