using Newtonsoft.Json.Linq;
using SimpleJSON;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
using Saving;

public class ImportUISettings : Singleton<ImportUISettings>, IJsonSaveable
{
    [SerializeField] List<string> categories = new List<string>();
    [SerializeField] List<string> locations = new List<string>();
    // Start is called before the first frame update
    void Start()
    {
      //  DontDestroyOnLoad(this.gameObject);
        if (Application.platform != RuntimePlatform.WindowsEditor && Application.platform != RuntimePlatform.WindowsPlayer)
        {
            StartCoroutine(ImportLocationsRoutine());
            StartCoroutine(ImportCategoriesRoutine());
        }
        else if (Application.isEditor)
        {
            StartCoroutine(ImportLocationsRoutine());
            StartCoroutine(ImportCategoriesRoutine());
        }
    }

    /// <summary>
    /// Import all locations from the online database to InternalDatabase
    /// </summary>
    private IEnumerator ImportLocationsRoutine()
    {
        WWWForm locationsForm = new WWWForm();
        locationsForm.AddField("apppassword", "ImportDatabase");

        UnityWebRequest locationRequest = CreatePostRequest.GetPostRequest(locationsForm, "importlocations.php", 1);

        yield return locationRequest.SendWebRequest();

        Sheet tempSheet = new Sheet();
        tempSheet.itens = new List<ItemColumns>();

        if (locationRequest.result == UnityWebRequest.Result.ConnectionError)
        {
            Debug.LogWarning("ImportLocations: conection error");
        }
        else if (locationRequest.result == UnityWebRequest.Result.DataProcessingError)
        {
            Debug.LogWarning("ImportLocations: data processing error");
        }
        else if (locationRequest.result == UnityWebRequest.Result.ProtocolError)
        {
            Debug.LogWarning("ImportLocations: protocol error");
        }
        if (locationRequest.error == null)
        {
            string response = locationRequest.downloadHandler.text;
            if (response == "Database connection error" || response == "wrong appkey" || response == "Query failed")
            {
                Debug.LogWarning("ImportLocations: Server error");
            }
            else if (response == "Result came empty")
            {
                Debug.LogWarning("ImportLocations: Result came empty");
            }
            else
            {
                InternalDatabase.locations.Clear();
                JSONNode inventario = JSON.Parse(locationRequest.downloadHandler.text);
                foreach (JSONNode item in inventario)
                {
                    InternalDatabase.locations.Add(item[0]);
                }
                InternalDatabase.locations.Sort();
            }
        }
        locationRequest.Dispose();
    }

    /// <summary>
    /// Import all categories from the online database to InternalDatabase
    /// </summary>
    private IEnumerator ImportCategoriesRoutine()
    {
        WWWForm categoriesForm = new WWWForm();
        categoriesForm.AddField("apppassword", "ImportDatabase");

        UnityWebRequest categoriesRequest = CreatePostRequest.GetPostRequest(categoriesForm, "importcategories.php", 1);

        yield return categoriesRequest.SendWebRequest();

        Sheet tempSheet = new Sheet();
        tempSheet.itens = new List<ItemColumns>();

        if (categoriesRequest.result == UnityWebRequest.Result.ConnectionError)
        {
            Debug.LogWarning("ImportLocations: conection error");
        }
        else if (categoriesRequest.result == UnityWebRequest.Result.DataProcessingError)
        {
            Debug.LogWarning("ImportLocations: data processing error");
        }
        else if (categoriesRequest.result == UnityWebRequest.Result.ProtocolError)
        {
            Debug.LogWarning("ImportLocations: protocol error");
        }
        if (categoriesRequest.error == null)
        {
            string response = categoriesRequest.downloadHandler.text;
            if (response == "Database connection error" || response == "wrong appkey" || response == "Query failed")
            {
                Debug.LogWarning("ImportLocations: Server error");
            }
            else if (response == "Result came empty")
            {
                Debug.LogWarning("ImportLocations: Result came empty");
            }
            else
            {
                InternalDatabase.categories.Clear();
                JSONNode inventario = JSON.Parse(categoriesRequest.downloadHandler.text);
                foreach (JSONNode item in inventario)
                {
                    InternalDatabase.categories.Add(item[0]);
                }
                InternalDatabase.categories.Sort();
            }
        }
        categoriesRequest.Dispose();
    }

   
    /// <summary>
    /// Save all categories and locations for offline version
    /// </summary>
    public JToken CaptureAsJToken()
    {
        JArray state = new JArray();
        IList<JToken> stateList = state;
        int cont = 0;
        foreach (var item in InternalDatabase.locations)
        {
            JObject jObjectToReturn = new JObject();
            IDictionary<string, JToken> stateDict = jObjectToReturn;
            stateDict["Location"] = item;
            stateList.Add(jObjectToReturn);
        }
        foreach (var item in InternalDatabase.categories)
        {
            JObject jObjectToReturn = new JObject();
            IDictionary<string, JToken> stateDict = jObjectToReturn;
            stateDict["Category"] = item;
            stateList.Add(jObjectToReturn);
        }

        return state;
    }

    /// <summary>
    /// Load all categories and locations for offline version
    /// </summary>
    public void RestoreFromJToken(JToken state)
    {
        print("hi");
            if (state is JArray stateArray)
        {
            print("statearray");
            IList<JToken> stateList = stateArray;
            foreach (var item in stateList)
            {
                if (item is JObject itemState)
                {
                    string locationToLoad = "";
                    string categoryToLoad = "";
                    IDictionary<string, JToken> itemStateDict = itemState;
                    if (itemStateDict["Location"] != null)
                    {
                        print("locations");
                        locationToLoad = itemStateDict["Location"].ToObject<string>();
                        InternalDatabase.locations.Add(locationToLoad);
                    }
                    if (itemStateDict["Category"] != null)
                    {
                        print("category");
                        categoryToLoad = itemStateDict["Category"].ToObject<string>();
                        InternalDatabase.categories.Add(categoryToLoad);
                    }
                }
            }
            InternalDatabase.locations.Sort();
            InternalDatabase.categories.Sort();
        }
        categories = InternalDatabase.categories;
        locations = InternalDatabase.locations;
    }
}

