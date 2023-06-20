using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Saving;
using SimpleJSON;
using UnityEngine.Networking;
using Newtonsoft.Json.Linq;

public class UsersManager : Singleton<UsersManager>, IJsonSaveable
{
    public List<User> usersDatabase;
    public User admin;

    public bool adminLogged = false;
    public User currentUser = new User("pessoa","");

    protected  override void Awake()
    {
        base.Awake();
       
    }

    private void Start()
    {
        usersDatabase = new List<User>();
        admin = new User("admin", "admin", 10);
        if (!usersDatabase.Contains(admin) && InternalDatabase.Instance.isOfflineProgram)
        {
            usersDatabase.Add(admin);
        }
        if (Application.platform == RuntimePlatform.WindowsEditor || Application.platform == RuntimePlatform.WindowsPlayer)
        {
            StartCoroutine(GetAllUsers());
        }
   
    }

    private IEnumerator GetAllUsers()
    {
        WWWForm usersForm = CreateForm.GetUsersForm(ConstStrings.ImportDatabaseKey);
        UnityWebRequest createPostRequest = CreatePostRequest.GetPostRequest(usersForm, ConstStrings.ImportUsers, 1);
        yield return createPostRequest.SendWebRequest();

        if (createPostRequest.result == UnityWebRequest.Result.ConnectionError)
        {
            Debug.LogWarning("UsersManager: conectionerror");
        }
        else if (createPostRequest.result == UnityWebRequest.Result.DataProcessingError)
        {
            Debug.LogWarning("UsersManager: data processing error");
        }
        else if (createPostRequest.result == UnityWebRequest.Result.ProtocolError)
        {
            Debug.LogWarning("UsersManager: protocol error");
        }

        if (createPostRequest.error == null)
        {
            string response = createPostRequest.downloadHandler.text;
            if (response == "Database connection error")
            {
                Debug.LogWarning("UsersManager: conection error");
            }
            else if (response == "wrong appkey")
            {
                Debug.LogWarning("UsersManager: WrongAppKey");
            }
            else if (response == "Query failed")
            {
                Debug.LogWarning("UsersManager: Query failed");
            }
            else
            {
                JSONNode users = JSON.Parse(createPostRequest.downloadHandler.text);
                foreach (JSONNode item in users)
                {
                    User user = new User(item[1], item[2], item[3]);
                    usersDatabase.Add(user);
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
        foreach (var item in usersDatabase)
        {
            JObject jObjectToReturn = new JObject();
            IDictionary<string, JToken> stateDict = jObjectToReturn;
            stateDict["username"] = item.GetUsername();
            stateDict["password"] = item.GetPassword();
            stateDict["accesslevel"] = item.GetAccessLevel();
            stateList.Add(jObjectToReturn);
        }
        return state;
    }

    public void RestoreFromJToken(JToken state)
    {
        //if (state is JArray stateArray)
        //{
        //    IList<JToken> stateList = stateArray;
        //    foreach (var item in stateList)
        //    {
        //        if (item is JObject itemState)
        //        {
                  
        //            IDictionary<string, JToken> itemStateDict = itemState;
        //            User userToLoad = new User(itemStateDict["username"].ToObject<string>(), itemStateDict["password"].ToObject<string>(),
        //                itemStateDict["accesslevel"].ToObject<int>());  
        //            usersDatabase.Add(userToLoad);
        //        }
        //    }
        //}
    }
}
