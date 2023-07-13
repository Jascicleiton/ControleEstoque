using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assets.Scripts.Inventory.Item
{
    public class Fonte : ItemParent
    {
        public int Watts_I;
        public string OndeFunciona = "";
        public string Conectores = "";

        protected override void Awake()
        {
            base.Awake();
            allValues.Add(Watts_I.ToString());
            allValues.Add(OndeFunciona);
            allValues.Add(Conectores);
        }
    }
}
