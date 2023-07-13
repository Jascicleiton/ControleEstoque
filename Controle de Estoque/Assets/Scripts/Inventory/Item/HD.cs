using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assets.Scripts.Inventory.Item
{
    public class HD : ItemParent
    {
        public string Interface = "";
        public float Tamanho_F;
        public string FormaDeArmazenamento = "";
        public int Capacidade_I;
        public int RPM_I;
        public int VelocidadeDeLeitura_I;
        public string Enterprise;

        protected override void Awake()
        {
            base.Awake();
            allValues.Add(Interface);
            allValues.Add(Tamanho_F.ToString());
            allValues.Add(FormaDeArmazenamento.ToString());
            allValues.Add(Capacidade_I.ToString());
            allValues.Add( RPM_I.ToString());
            allValues.Add(VelocidadeDeLeitura_I.ToString());
            allValues.Add(Enterprise);
        }
    }
}
