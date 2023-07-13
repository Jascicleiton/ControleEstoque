using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assets.Scripts.Inventory.Item
{
    public class GBIC : ItemParent
    {
        public int DesempenhoMax_I;

        protected override void Awake()
        {
            base.Awake();
            allValues.Add(DesempenhoMax_I.ToString());
        }
    }
}
