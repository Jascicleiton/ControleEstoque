using System.Collections.Generic;
using Newtonsoft.Json.Linq;
using Assets.Scripts.Inventory.PatrimonioItem;

namespace Assets.Scripts.Saving
{
    public class SaveLocallyWithJson : ISaveDatabaseLocally
    {
        public JArray GetJArray(Dictionary<int, PatrimonioItemParent> itemsDictionary)
        {
            JArray state = new JArray();
            IList<JToken> stateList = state;
            foreach (var item in itemsDictionary.Values)
            {
                JObject jObjectToReturn = new JObject();
                IDictionary<string, JToken> stateDict = jObjectToReturn;
                foreach (var parameter in item.GetAllParametersDictionary())
                {
                    stateDict[parameter.Key] = parameter.Value;
                }
            }

            return state;
        }

        public Dictionary<int, PatrimonioItemParent> Load(JToken state)
        {
            return null;
        }
    }
}