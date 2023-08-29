using Assets.Scripts.Inventory.PatrimonioItem;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assets.Scripts.Misc
{
    public class CompareByModelo : IEqualityComparer<PatrimonioItemParent>
    {
        public bool Equals(PatrimonioItemParent x, PatrimonioItemParent y)
        {
            return x.GetSpecificParameter(ConstStrings.Modelo).ToLower() == y.GetSpecificParameter(ConstStrings.Modelo).ToLower();
        }

        public int GetHashCode(PatrimonioItemParent obj)
        {
            return int.Parse(obj.GetSpecificParameter(ConstStrings.Patrimonio_I));
        }
    }
}
