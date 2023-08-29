using System.Collections.Generic;
using UnityEngine;
using Newtonsoft.Json.Linq;
using Assets.Scripts.Inventory.NoPatrimonioItem;
using Assets.Scripts.Inventory.Movement;
using Assets.Scripts.Misc;
using Assets.Scripts.Saving;
using Assets.Scripts.Inventory.PatrimonioItem;
using System.Linq;

namespace Assets.Scripts.Inventory.Database
{
    public class NewInternalDatabase : Singleton<InternalDatabase>, IJsonSaveable, IInternalDatabase
    {
        [SerializeField] GameObject exportManagerPrefab;

        public string currentVersion = "1.0"; // current program version - it is here for lazy reasons
               
        public static List<MovementRecords> movementRecords;
        public static List<string> locations = new List<string>();
        public static List<string> categories = new List<string>();

        public bool isOfflineProgram = false;

        public CurrentEstoque currentEstoque = CurrentEstoque.SnPro;

        public Dictionary<int, PatrimonioItemParent> itemsDictionary = new Dictionary<int, PatrimonioItemParent>();
        public List<PatrimonioItemParent> tempItemsList = new List<PatrimonioItemParent>(500);

        private void Start()
        {
            if (isOfflineProgram)
            {
                SavingWrapper.Instance.Load();
            }
        }

        private void OnEnable()
        {
            EventHandler.FillInternalDatabase += FillFullDatabase;
        }

        private void OnDisable()
        {
            EventHandler.FillInternalDatabase -= FillFullDatabase;
        }       

        /// <summary>
        /// Get all Sheet classes saved on splitDatabase and join them into a single Sheet class
        /// </summary>
        private void FillFullDatabase()
        {
            foreach (var item in tempItemsList)
            {
               var existingItem = itemsDictionary.FirstOrDefault(target => target.Value.Equals(item, ConstStrings.Patrimonio_I));
                if(existingItem.Value is not null)
                {
                    itemsDictionary[existingItem.Key].SetParameters(item.GetAllParametersDictionary());
                }
            }            
        }

        /// <summary>
        /// Used by the save system to save the pertinent information from this class
        /// </summary>
        public JToken CaptureAsJToken()
        {
           // JArray state = HandleSheetsForSaveAndLoad.GetJObject(fullDatabase);

            //return state;
            return null;
        }

        /// <summary>
        /// Used by the save system to load the pertinent information from this class
        /// </summary>
        public void RestoreFromJToken(JToken state)
        {
            //HandleSheetsForSaveAndLoad.LoadJObject(state, out sheetToLoad);
            //fullDatabase = sheetToLoad;            
        }

        public void AddTemporaryPatrimonioItem(PatrimonioItemParent item)
        {
            if(!tempItemsList.Contains(item))
            {
                tempItemsList.Add(item);
            }            
        }

        public void AddPatrimonioItem(PatrimonioItemParent item)
        {
            if (!itemsDictionary.ContainsKey(item.GetPatrimonio()))
            {
                itemsDictionary.Add(item.GetPatrimonio(), item);
            }
        }
    }
}
