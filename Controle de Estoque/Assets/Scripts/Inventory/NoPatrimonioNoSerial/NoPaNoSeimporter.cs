using Newtonsoft.Json.Linq;
using Saving;
using SimpleJSON;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class NoPaNoSeImporter : Singleton<NoPaNoSeImporter>, IJsonSaveable
{
    [HideInInspector]
    public NoPaNoSeAll itemsList = new NoPaNoSeAll();
   
    // Start is called before the first frame update
    void Start()
    {
        if (!InternalDatabase.Instance.offlineProgram)
        {
            StartCoroutine(StartListRoutine());
            itemsList.noPaNoSeItems.Clear();
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
                    itemsList.noPaNoSeItems.Add(new NoPaNoSeItem(item[0], int.Parse(item[1])));
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
        itemsList.noPaNoSeItems.Add(newItem);
       
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
        JArray state = HandleNoPaNoSeItemForSaveAndLoad.SaveObject(itemsList);
        return state;
    }

    /// <summary>
    /// Load all the NoPaNoSe items for the use of the offline program
    /// </summary>
    public void RestoreFromJToken(JToken state)
    {
        HandleNoPaNoSeItemForSaveAndLoad.LoadJObject(state, out itemsList);       
    }

}
