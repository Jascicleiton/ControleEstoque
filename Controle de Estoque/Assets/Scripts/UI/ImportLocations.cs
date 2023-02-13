using SimpleJSON;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class ImportLocations : Singleton<ImportLocations>
{

    // Start is called before the first frame update
    void Start()
    {
        DontDestroyOnLoad(this.gameObject);
        StartCoroutine(ImportLocationsRoutine());
    }

    
    private IEnumerator ImportLocationsRoutine()
    {
        WWWForm locationsForm = new WWWForm();
        locationsForm.AddField("apppassword", "ImportDatabase");

        UnityWebRequest locationRequest = HelperMethods.GetPostRequest(locationsForm, "importlocations.php", 1);
       
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
            }
        }
        locationRequest.Dispose();    
    }

    public void ReImport()
    {
        StartCoroutine(ImportLocationsRoutine());
    }
}

