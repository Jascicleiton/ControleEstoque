using Newtonsoft.Json.Linq;
using Saving;
using SimpleJSON;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class RegularMovementSaver : Singleton<RegularMovementSaver>, IJsonSaveable
{
    [SerializeField] private List<MovementRecords> regularRecords = new List<MovementRecords>();

    // Start is called before the first frame update
    void Start()
    {
        if (Application.platform == RuntimePlatform.WindowsEditor || Application.platform == RuntimePlatform.WindowsPlayer)
        {
            if (!InternalDatabase.Instance.isOfflineProgram)
            {
                StartCoroutine(GetAllRegularMovements());
            }
        }
        else
        {
            Destroy(this.gameObject);
        }
    }

    public void RegisterNewRegularMovement(MovementRecords newMovement)
    {
        if (newMovement != null)
        {
            regularRecords.Add(newMovement);
        }
        else
        {
            EventHandler.CallIsOneMessageOnlyEvent(true);
            EventHandler.CallOpenMessageEvent("Null movement record");
        }
        SavingWrapper.Instance.Save();
    }

    public List<MovementRecords> GetAllRegularRecords()
    {
        return regularRecords;
    }

    private IEnumerator GetAllRegularMovements()
    {
        WWWForm movementsForm = CreateForm.GetAllMovements(ConstStrings.ImportDatabaseKey);
        UnityWebRequest createPostRequest = CreatePostRequest.GetPostRequest(movementsForm, ConstStrings.ImportRegularMovements, 1);
        yield return createPostRequest.SendWebRequest();

        if (createPostRequest.result == UnityWebRequest.Result.ConnectionError)
        {
            Debug.LogWarning("MovementSaver: conectionerror");
        }
        else if (createPostRequest.result == UnityWebRequest.Result.DataProcessingError)
        {
            Debug.LogWarning("MovementSaver: data processing error");
        }
        else if (createPostRequest.result == UnityWebRequest.Result.ProtocolError)
        {
            Debug.LogWarning("MovementSaver: protocol error");
        }

        if (createPostRequest.error == null)
        {
            string response = createPostRequest.downloadHandler.text;
            if (response == "Database connection error")
            {
                Debug.LogWarning("MovementSaver: conection error");
            }
            else if (response == "wrong appkey")
            {
                Debug.LogWarning("MovementSaver: WrongAppKey");
            }
            else if (response == "Query failed")
            {
                Debug.LogWarning("MovementSaver regular: Query failed");
            }
            else
            {
                JSONNode movements = JSON.Parse(createPostRequest.downloadHandler.text);
                foreach (JSONNode item in movements)
                {
                    if (item.Count > 0)
                    {
                        MovementRecords newMovement = new MovementRecords();
                        newMovement.item = new ItemColumns();
                        newMovement.item.Patrimonio = item[1];
                        newMovement.item.Serial = item[2];
                        newMovement.username = item[3];
                        newMovement.date = item[4];
                        newMovement.fromWhere = item[5];
                        newMovement.toWhere = item[6];

                        regularRecords.Add(newMovement);
                    }
                }
            }
        }
        else
        {
            Debug.LogWarning(createPostRequest.error);
        }
        createPostRequest.Dispose();
        SavingWrapper.Instance.Save();
    }

    public JToken CaptureAsJToken()
    {
        JArray state = new JArray();
        IList<JToken> stateList = state;
        foreach (var item in regularRecords)
        {
            JObject jObjectToReturn = new JObject();
            IDictionary<string, JToken> stateDict = jObjectToReturn;
            stateDict["RegPatrimonio"] = item.item.Patrimonio;
            stateDict["RegSerial"] = item.item.Serial;
            stateDict["RegUsername"] = item.username;
            stateDict["RegDate"] = item.date;
            stateDict["RegFromWhere"] = item.fromWhere;
            stateDict["RegToWhere"] = item.toWhere;
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
                    regularMovementToLoad.item = new ItemColumns();
                    IDictionary<string, JToken> itemStateDict = itemState;
                    
                        regularMovementToLoad.item.Patrimonio = itemStateDict["RegPatrimonio"].ToObject<int>();
                        regularMovementToLoad.item.Serial = itemStateDict["RegSerial"].ToObject<string>();
                        regularMovementToLoad.username = itemStateDict["RegUsername"].ToObject<string>();
                        regularMovementToLoad.date = itemStateDict["RegDate"].ToObject<string>();
                        regularMovementToLoad.fromWhere = itemStateDict["RegFromWhere"].ToObject<string>();
                        regularMovementToLoad.toWhere = itemStateDict["RegToWhere"].ToObject<string>();
                        regularRecords.Add(regularMovementToLoad);                                      
                }
            }
        }
    }
}
