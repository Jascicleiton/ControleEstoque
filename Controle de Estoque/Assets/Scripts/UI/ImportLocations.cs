using Newtonsoft.Json.Linq;
using SimpleJSON;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
using static UnityEditor.FilePathAttribute;

public class ImportLocations : MonoBehaviour, IJsonSaveable
{
    // Start is called before the first frame update
    void Start()
    {
        if (Application.platform != RuntimePlatform.WindowsEditor && Application.platform != RuntimePlatform.WindowsPlayer)
        {
            StartCoroutine(ImportLocationsRoutine());
        }
        else if (Application.isEditor)
        {
            StartCoroutine(ImportLocationsRoutine());
        }
      // GambiarraParaCarregarLocais();
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
    /// Used only as a temporary solution to have locations on the program
    /// </summary>
    private void GambiarraParaCarregarLocais()
    {
        InternalDatabase.locations.Clear();
        InternalDatabase.locations.AddRange(new List<string> { "Bancada de testes", "BRT", "Concert", "Descarte", "Drogaria Cristina",
            "Encontre sua franquia", "Escritório", "Estoque", "Fumsoft", "IOCT", "Master truck", "Netlite","Oficina da roupa","Outros","SNP01","SNP02",
            "SNP03","SNP04","SNP05","SNP06", "SNP07","SNP08","Top cintos","Trop 1","Trop 2","Uni Santos","Zaplus Car"});
    }

    public JToken CaptureAsJToken()
    {
        JArray state = new JArray();
        IList<JToken> stateList = state;
        foreach (var item in InternalDatabase.locations)
        {
            JObject jObjectToReturn = new JObject();
            IDictionary<string, JToken> stateDict = jObjectToReturn;
            stateDict["Location"] = item;
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
                    string locationToLoad = "";
                    IDictionary<string, JToken> itemStateDict = itemState;
                    if (itemStateDict["Location"] != null)
                    {
                        locationToLoad = itemStateDict["Location"].ToObject<string>();
                        InternalDatabase.locations.Add(locationToLoad);
                    }
                }
            }
            InternalDatabase.locations.Sort();
                  }
        
    }
}
