using Assets.Scripts.Inventory.Database;
using Assets.Scripts.Misc;
using Assets.Scripts.Saving;
using Assets.Scripts.Web;
using Newtonsoft.Json.Linq;
using SimpleJSON;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

namespace Assets.Scripts.Inventory.Movement
{
    public class NoPaNoSeMovementSaver : Singleton<NoPaNoSeMovementSaver>, IJsonSaveable
    {
        private List<NoPaNoSeMovementRecords> _noPaNoSeRecords = new List<NoPaNoSeMovementRecords>();

        // Start is called before the first frame update
        void Start()
        {
            if (Application.platform == RuntimePlatform.WindowsEditor || Application.platform == RuntimePlatform.WindowsPlayer)
            {
                if (!InternalDatabase.Instance.isOfflineProgram)
                {
                    StartCoroutine(GetAllNoPaNoSeMovements());
                }
            }
            else
            {
                Destroy(gameObject);
            }
        }

        private IEnumerator GetAllNoPaNoSeMovements()
        {
            WWWForm movementsForm = CreateForm.GetAllMovements(ConstStrings.ImportDatabaseKey);
            UnityWebRequest createPostRequest = CreatePostRequest.GetPostRequest(movementsForm, ConstStrings.ImportNoPaNoSeMovements, 1);
            yield return createPostRequest.SendWebRequest();

            if (createPostRequest.result == UnityWebRequest.Result.ConnectionError)
            {
                Debug.LogWarning("RegularMovementSaver: conectionerror");
            }
            else if (createPostRequest.result == UnityWebRequest.Result.DataProcessingError)
            {
                Debug.LogWarning("RegularMovementSaver: data processing error");
            }
            else if (createPostRequest.result == UnityWebRequest.Result.ProtocolError)
            {
                Debug.LogWarning("RegularMovementSaver: protocol error");
            }

            if (createPostRequest.error == null)
            {
                string response = createPostRequest.downloadHandler.text;
                if (response == "Database connection error")
                {
                    Debug.LogWarning("RegularMovementSaver: conection error");
                }
                else if (response == "wrong appkey")
                {
                    Debug.LogWarning("RegularMovementSaver: WrongAppKey");
                }
                else if (response == "Query failed")
                {
                    Debug.LogWarning("RegularMovementSaver: Query failed");
                }
                else
                {
                    JSONNode movements = JSON.Parse(createPostRequest.downloadHandler.text);
                    foreach (JSONNode item in movements)
                    {
                        NoPaNoSeMovementRecords record = new NoPaNoSeMovementRecords();
                        record.itemName = item[1];
                        record.quantity = item[2];
                        record.username = item[3];
                        record.date = item[4];
                        record.fromWhere = item[5];
                        record.toWhere = item[6];

                        _noPaNoSeRecords.Add(record);
                    }
                }
            }
            else
            {
                Debug.LogWarning(createPostRequest.error);
            }
            createPostRequest.Dispose();
            if (InternalDatabase.Instance.isOfflineProgram)
            {
                SavingWrapper.Instance.Save();
            }
        }

        public void RegisterNewNoPaNoSeMovement(NoPaNoSeMovementRecords newMovement)
        {
            if (newMovement != null)
            {
                _noPaNoSeRecords.Add(newMovement);
            }
            else
            {
                EventHandler.CallIsOneMessageOnlyEvent(true);
                EventHandler.CallOpenMessageEvent("Null movement record");
            }
            SavingWrapper.Instance.Save();
        }

        public List<NoPaNoSeMovementRecords> GetAllNoPaNoSeRecords()
        {
            return _noPaNoSeRecords;
        }

        public JToken CaptureAsJToken()
        {
            JArray state = new JArray();
            IList<JToken> stateList = state;
            foreach (var item in _noPaNoSeRecords)
            {
                JObject jObjectToReturn = new JObject();
                IDictionary<string, JToken> stateDict = jObjectToReturn;
                stateDict["NoPaName"] = item.itemName;
                stateDict["NoPaQuantity"] = item.quantity;
                stateDict["NoPaUsername"] = item.username;
                stateDict["NoPaDate"] = item.date;
                stateDict["NoPaFromWhere"] = item.fromWhere;
                stateDict["NoPaToWhere"] = item.toWhere;
                stateList.Add(jObjectToReturn);
            }
            return state;
        }

        public void RestoreFromJToken(JToken state)
        {
            if (state is JArray stateArray)
            {
                IList<JToken> stateList = stateArray;
                foreach (var item in stateList)
                {
                    if (item is JObject itemState)
                    {
                        MovementRecords regularMovementToLoad = new MovementRecords();
                        NoPaNoSeMovementRecords noPaNoSeMovementToLoad = new NoPaNoSeMovementRecords();
                        IDictionary<string, JToken> itemStateDict = itemState;
                        noPaNoSeMovementToLoad.itemName = itemStateDict["NoPaName"].ToObject<string>();
                        noPaNoSeMovementToLoad.quantity = itemStateDict["NoPaQuantity"].ToObject<string>();
                        noPaNoSeMovementToLoad.username = itemStateDict["NoPaUsername"].ToObject<string>();
                        noPaNoSeMovementToLoad.date = itemStateDict["NoPaDate"].ToObject<string>();
                        noPaNoSeMovementToLoad.fromWhere = itemStateDict["NoPaFromWhere"].ToObject<string>();
                        noPaNoSeMovementToLoad.toWhere = itemStateDict["NoPaToWhere"].ToObject<string>();
                        _noPaNoSeRecords.Add(noPaNoSeMovementToLoad);
                    }
                }
            }
        }
    }
}