using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using SimpleJSON;
using UnityEngine.Networking;
using Newtonsoft.Json.Linq;
using Assets.Scripts.Inventory.Database;
using Assets.Scripts.Misc;
using Assets.Scripts.Saving;
using static UnityEditor.PlayerSettings;
using Assets.Scripts.Web;

namespace Assets.Scripts.Users
{
    public class UsersManager : Singleton<UsersManager>, IJsonSaveable
    {
        private List<User> _usersDatabase;
        public User Admin { get; private set; }
        public bool AdminLogged { get; set; }
        public User CurrentUser { get; set; }              

        private void Start()
        {
            CurrentUser = new User("pessoa", "password", -1);
            _usersDatabase = new List<User>();
            Admin = new User("admin", "admin", 10);
            if (!_usersDatabase.Contains(Admin) && InternalDatabase.Instance.isOfflineProgram)
            {
                _usersDatabase.Add(Admin);
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
                        _usersDatabase.Add(user);
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

        public JToken CaptureAsJToken()
        {
            JArray state = new JArray();
            IList<JToken> stateList = state;
            foreach (var item in _usersDatabase)
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
            if (state is JArray stateArray)
            {
                IList<JToken> stateList = stateArray;
                foreach (var item in stateList)
                {
                    if (item is JObject itemState)
                    {

                        IDictionary<string, JToken> itemStateDict = itemState;
                        User userToLoad = new User(itemStateDict["username"].ToObject<string>(), itemStateDict["password"].ToObject<string>(),
                            itemStateDict["accesslevel"].ToObject<int>());
                        _usersDatabase.Add(userToLoad);
                    }
                }
            }
        }
    }
}