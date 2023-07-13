using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assets.Scripts.Inventory.Item
{
    public class Idrac : ItemParent
    {
        public string Porta = "";
        public int Velocidade_I;
        public string EntradaSD = "";
        public string ServidoresSuportados = "";

        protected override void Awake()
        {
            base.Awake();
            allValues.Add(Porta);
            allValues.Add(Velocidade_I.ToString());
            allValues.Add(EntradaSD);
            allValues.Add(ServidoresSuportados);
        }
    }
}
