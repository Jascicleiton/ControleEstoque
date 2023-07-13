using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assets.Scripts.Inventory.Item
{
    public class Memoria : ItemParent
    {
        public string Tipo = "";
        public int Capacidade_I;
        public int Velocidade_I;
        public string LowVoltage = "";
        public string Rank = "";
        public string DIMM = "";
        public int TaxaDeTransmissao_I;
        public string Simbolo = "";

        protected override void Awake()
        {
            base.Awake();
            allValues.Add(Tipo);
            allValues.Add(Capacidade_I.ToString());
            allValues.Add(Velocidade_I.ToString());
            allValues.Add(LowVoltage.ToString());
            allValues.Add(Rank.ToString());
            allValues.Add(DIMM.ToString());
            allValues.Add(TaxaDeTransmissao_I.ToString());
            allValues.Add(Simbolo);
        }
    }
}
