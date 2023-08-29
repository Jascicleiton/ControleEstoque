using System.Collections.Generic;
using Newtonsoft.Json.Linq;
using Assets.Scripts.Inventory.PatrimonioItem;

namespace Assets.Scripts.Saving
{
    public interface ISaveDatabaseLocally
    {
        JArray GetJArray(Dictionary<int, PatrimonioItemParent> itemsDictionary);
        Dictionary<int, PatrimonioItemParent> Load(JToken state);
    }
}