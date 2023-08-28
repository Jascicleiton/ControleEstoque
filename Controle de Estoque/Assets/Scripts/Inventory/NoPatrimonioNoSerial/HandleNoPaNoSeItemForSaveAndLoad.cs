using System.Collections.Generic;
using Newtonsoft.Json.Linq;

namespace Assets.Scripts.Inventory.NoPatrimonioNoSerial
{
    public class HandleNoPaNoSeItemForSaveAndLoad
    {
        /// <summary>
        /// Save all the NoPaNoSeItem for offline use of the program
        /// </summary>
        public static JArray SaveObject(NoPaNoSeAll allItems)
        {
            JArray state = new JArray();
            IList<JToken> stateList = state;
            foreach (var item in allItems.noPaNoSeItems)
            {
                JObject jObjectToReturn = new JObject();
                IDictionary<string, JToken> stateDict = jObjectToReturn;
                stateDict["ItemName"] = item.ItemName;
                stateDict["Quantity"] = item.Quantity;
                stateList.Add(jObjectToReturn);
            }

            return state;
        }

        /// <summary>
        /// Load all the NoPaNoSeItem for offline use of the program
        /// </summary>
        public static void LoadJObject(JToken state, out NoPaNoSeAll itemsToLoad)
        {
            itemsToLoad = new NoPaNoSeAll();
            if (state is JArray stateArray)
            {
                IList<JToken> stateList = stateArray;
                foreach (var item in stateList)
                {
                    if (item is JObject itemState)
                    {
                        NoPaNoSeItem itemToLoad = new NoPaNoSeItem();
                        IDictionary<string, JToken> itemStateDict = itemState;
                        itemToLoad.ItemName = itemStateDict["ItemName"].ToObject<string>();
                        itemToLoad.Quantity = itemStateDict["Quantity"].ToObject<int>();
                        itemsToLoad.noPaNoSeItems.Add(itemToLoad);
                    }
                }
            }

        }
    }
}