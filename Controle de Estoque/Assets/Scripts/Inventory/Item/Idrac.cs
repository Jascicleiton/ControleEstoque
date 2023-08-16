using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assets.Scripts.Inventory.Item
{
    public class Idrac : ItemParent
    {
        protected override void Awake()
        {
            base.Awake();
            allParameters.Add(ConstStrings.Porta, default);
            allParameters.Add(ConstStrings.Velocidade_I, default);
            allParameters.Add(ConstStrings.EntradaSD, default);
            allParameters.Add(ConstStrings.ServidoresSuportados, default);
        }
    }
}
