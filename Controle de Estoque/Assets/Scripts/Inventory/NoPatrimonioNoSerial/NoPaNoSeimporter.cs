using Assets.Scripts.Inventory.Database;
using Assets.Scripts.Misc;
using Assets.Scripts.Mouse;
using Assets.Scripts.Saving;
using Assets.Scripts.Web;
using Newtonsoft.Json.Linq;
using SimpleJSON;
using System.Collections;
using UnityEngine;
using UnityEngine.Networking;

namespace Assets.Scripts.Inventory.NoPatrimonioNoSerial
{
    public class NoPaNoSeImporter : Singleton<NoPaNoSeImporter>, IJsonSaveable
    {
        [HideInInspector]
        public NoPaNoSeAll ItemsList = new NoPaNoSeAll();

        // Start is called before the first frame update
        void Start()
        {
            if (!InternalDatabase.Instance.isOfflineProgram)
            {
                ItemsList.noPaNoSeItems.Clear();
                StartCoroutine(StartListRoutine());
            }
        }

        /// <summary>
        /// Import all items stored on the online database
        /// </summary>
        private IEnumerator StartListRoutine()
        {
            WWWForm itemForm = new WWWForm();
            itemForm.AddField("apppassword", ConstStrings.ImportDatabaseKey);

            UnityWebRequest createUpdateInventarioRequest = CreatePostRequest.GetPostRequest(itemForm, ConstStrings.ImportAllNoPaNoSeItems, 1);
            MouseManager.Instance.SetWaitingCursor();

            yield return createUpdateInventarioRequest.SendWebRequest();

            if (createUpdateInventarioRequest.result == UnityWebRequest.Result.ConnectionError)
            {
                EventHandler.CallDisconectedFromInternet();
                Debug.LogWarning("NoPaNoSeImporter: conectionerror");
            }
            else if (createUpdateInventarioRequest.result == UnityWebRequest.Result.DataProcessingError)
            {
                EventHandler.CallDisconectedFromInternet();
                Debug.LogWarning("NoPaNoSeImporter: data processing error");
            }
            else if (createUpdateInventarioRequest.result == UnityWebRequest.Result.ProtocolError)
            {
                EventHandler.CallDisconectedFromInternet();
                Debug.LogWarning("NoPaNoSeImporter: protocol error");
            }

            if (createUpdateInventarioRequest.error == null)
            {
                string response = createUpdateInventarioRequest.downloadHandler.text;
                if (response == "Conection error" || response == "Query failed")
                {
                    EventHandler.CallDisconectedFromInternet();
                    Debug.LogWarning("NoPaNoSeImporter: Server error");
                }
                else if (response == "Wrong app key")
                {
                    EventHandler.CallDisconectedFromInternet();
                    Debug.LogWarning("NoPaNoSeImporter: app key");
                }
                else if (response == "Result came empty")
                {
                    EventHandler.CallDisconectedFromInternet();
                }
                else
                {
                    JSONNode inventario = JSON.Parse(createUpdateInventarioRequest.downloadHandler.text);

                    foreach (JSONNode item in inventario)
                    {
                        ItemsList.noPaNoSeItems.Add(new NoPaNoSeItem(item[0], int.Parse(item[1])));
                    }

                }
            }
            else
            {
                EventHandler.CallDisconectedFromInternet();
                Debug.LogWarning("NoPaNoSeImporter: " + createUpdateInventarioRequest.error);
                // TODO: send message to user with error and recomendation
            }
            createUpdateInventarioRequest.Dispose();
            MouseManager.Instance.SetDefaultCursor();
        }

        public void AddNewItem(NoPaNoSeItem newItem)
        {
            ItemsList.noPaNoSeItems.Add(newItem);

        }

        public void SaveItems()
        {
            if (Application.platform == RuntimePlatform.WindowsEditor || Application.platform == RuntimePlatform.WindowsPlayer)
            {
                SavingWrapper.Instance.Save();
            }
        }

        /// <summary>
        /// Save all the NoPaNoSe items for the use of the offline program
        /// </summary>
        public JToken CaptureAsJToken()
        {
            JArray state = HandleNoPaNoSeItemForSaveAndLoad.SaveObject(ItemsList);
            return state;
        }

        /// <summary>
        /// Load all the NoPaNoSe items for the use of the offline program
        /// </summary>
        public void RestoreFromJToken(JToken state)
        {
            HandleNoPaNoSeItemForSaveAndLoad.LoadJObject(state, out ItemsList);
        }

    }
}