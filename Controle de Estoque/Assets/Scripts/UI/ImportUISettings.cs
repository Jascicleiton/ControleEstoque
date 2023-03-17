using SimpleJSON;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class ImportUISettings : Singleton<ImportUISettings>
{

    // Start is called before the first frame update
    void Start()
    {
        DontDestroyOnLoad(this.gameObject);
        StartCoroutine(ImportLocationsRoutine());
        StartCoroutine(ImportCategoriesRoutine());
    }
   
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
                InternalDatabase.locations.Clear();
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

    public void ReImport()
    {
        StartCoroutine(ImportLocationsRoutine());
        StartCoroutine(ImportCategoriesRoutine());
    }
}

