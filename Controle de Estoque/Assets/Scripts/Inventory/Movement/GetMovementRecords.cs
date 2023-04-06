using SimpleJSON;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.UI;
using TMPro;

public class GetMovementRecords : MonoBehaviour
{
    public List<MovementRecords> regularItemMovementRecords = new List<MovementRecords>();
    public List<NoPaNoSeMovementRecords> noPaNoSeMovementRecords = new List<NoPaNoSeMovementRecords>();
    [SerializeField] private GameObject movementObjectPrefab;
    [SerializeField] private Transform instantiateTransform;
     [SerializeField] private TMP_InputField parameterInput;
    bool isSearchingPatrimonio = true;

    /// <summary>
    /// Disables all  movementObjectPrefab from the Pool Manager that were used in the previous search (if it 
    /// actually happened)
    /// </summary>
    private void DeleteOldSearch()
    {
        if (instantiateTransform.childCount > 0)
        {
            for (int i = 0; i < instantiateTransform.childCount; i++)
            {
                instantiateTransform.GetChild(i).gameObject.SetActive(false);
            }
        }
        regularItemMovementRecords.Clear();
        noPaNoSeMovementRecords.Clear();
    }

    /// <summary>
    /// Try to get all movements of a specific "Patrimonio" from the Online database
    /// </summary>
    private IEnumerator ImportPatrimonioMovementsRoutine()
    {
        WWWForm movementsForm = CreateForm.GetMovementsForm(ConstStrings.ImportDatabaseKey, parameterInput.text);

        UnityWebRequest createMovementRequest = CreatePostRequest.GetPostRequest(movementsForm, "importpatrimoniomovements.php", 3);

        MouseManager.Instance.SetWaitingCursor();
        yield return createMovementRequest.SendWebRequest();

        if (createMovementRequest.result == UnityWebRequest.Result.ConnectionError)
        {
            Debug.LogWarning("StartListRoutine: conectionerror");
        }
        else if (createMovementRequest.result == UnityWebRequest.Result.DataProcessingError)
        {
            Debug.LogWarning("StartListRoutine: data processing error");
        }
        else if (createMovementRequest.result == UnityWebRequest.Result.ProtocolError)
        {
            Debug.LogWarning("StartListRoutine: protocol error");
        }

        if (createMovementRequest.error == null)
        {
            string response = createMovementRequest.downloadHandler.text;
            if (response == "Conection error" || response == "Query failed")
            {
                Debug.LogWarning("StartListRoutine: Server error");
            }
            else if (response == "Wrong app key")
            {
                Debug.LogWarning("StartListRoutine: app key");
            }
            else if (response == "Result came empty")
            {
                EventHandler.CallIsOneMessageOnlyEvent(true);
                EventHandler.CallOpenMessageEvent("No movement found");
            }
            else
            {
                JSONNode inventario = JSON.Parse(createMovementRequest.downloadHandler.text);
                foreach (JSONNode item in inventario)
                {
                    MovementRecords record = new MovementRecords();
                    record.item = ConsultDatabase.Instance.ConsultPatrimonio(item[1], InternalDatabase.Instance.fullDatabase);
                    record.username = item[3];
                    record.date = item[4];
                    record.fromWhere = item[5];
                    record.toWhere = item[6];

                    regularItemMovementRecords.Add(record);
                }
               ShowMovements();
            }
        }
        else
        {
            Debug.LogWarning("StartListRoutine: " + createMovementRequest.error);
            // TODO: send message to user with error and recomendation
        }
        createMovementRequest.Dispose();
        MouseManager.Instance.SetDefaultCursor();
    }

    /// <summary>
    /// Try to get all movements of a specific "NoPaNoSe" item from the Online database
    /// </summary>
    private IEnumerator ImportNoPaNoSeMovementsRoutine()
    {
        WWWForm movementsForm = CreateForm.GetMovementsForm(ConstStrings.ImportDatabaseKey, parameterInput.text);

        UnityWebRequest createMovementRequest = CreatePostRequest.GetPostRequest(movementsForm, "importnopanosemovements.php", 3);

        MouseManager.Instance.SetWaitingCursor();
        yield return createMovementRequest.SendWebRequest();

        if (createMovementRequest.result == UnityWebRequest.Result.ConnectionError)
        {
            Debug.LogWarning("StartListRoutine: conectionerror");
        }
        else if (createMovementRequest.result == UnityWebRequest.Result.DataProcessingError)
        {
            Debug.LogWarning("StartListRoutine: data processing error");
        }
        else if (createMovementRequest.result == UnityWebRequest.Result.ProtocolError)
        {
            Debug.LogWarning("StartListRoutine: protocol error");
        }

        if (createMovementRequest.error == null)
        {
            string response = createMovementRequest.downloadHandler.text;
            if (response == "Conection error" || response == "Query failed")
            {
                Debug.LogWarning("StartListRoutine: Server error");
            }
            else if (response == "Wrong app key")
            {
                Debug.LogWarning("StartListRoutine: app key");
            }
            else if (response == "Result came empty")
            {
                EventHandler.CallIsOneMessageOnlyEvent(true);
                EventHandler.CallOpenMessageEvent("No movement found");
            }
            else
            {
                JSONNode inventario = JSON.Parse(createMovementRequest.downloadHandler.text);
                foreach (JSONNode item in inventario)
                {
                    NoPaNoSeMovementRecords record = new NoPaNoSeMovementRecords();
                    record.itemName = item[1];
                    record.quantity = item[2];
                    record.username = item[3];
                    record.date = item[4];
                    record.fromWhere = item[5];
                    record.toWhere = item[6];

                    noPaNoSeMovementRecords.Add(record);
                }
                ShowMovements();
            }
        }
        else
        {
            Debug.LogWarning("StartListRoutine: " + createMovementRequest.error);
            // TODO: send message to user with error and recomendation
        }
        createMovementRequest.Dispose();
        MouseManager.Instance.SetDefaultCursor();
    }

    /// <summary>
    /// Show all the movements that were found
    /// </summary>
    private void ShowMovements()
    {
        MouseManager.Instance.SetWaitingCursor();
        if (regularItemMovementRecords.Count > 0)
        {
            regularItemMovementRecords.Sort((x, y) => x.date.CompareTo(y.date));
            
                for (int i = 0; i < regularItemMovementRecords.Count; i++)
                {
                    GameObject instance = PoolManager.Instance.ReuseObject(movementObjectPrefab);
                    instance.gameObject.SetActive(true);
                    instance.GetComponent<MovementUIController>().SetMovementInfo(regularItemMovementRecords[i]);               
                }              
            
          
            MouseManager.Instance.SetDefaultCursor();
            parameterInput.text = "";
            return;
        }
        if (noPaNoSeMovementRecords.Count > 0)
        {
            noPaNoSeMovementRecords.Sort((x, y) => x.date.CompareTo(y.date));

            for (int i = 0; i < noPaNoSeMovementRecords.Count; i++)
            {
                GameObject instance = PoolManager.Instance.ReuseObject(movementObjectPrefab);
                instance.gameObject.SetActive(true);
                instance.GetComponent<MovementUIController>().SetMovementInfo(noPaNoSeMovementRecords[i]);
            }
            MouseManager.Instance.SetDefaultCursor();
            parameterInput.text = "";
            return;
        }
       
    }

    /// <summary>
    /// Set the isSearchingPatriomonio variable and the placeholder text of the parameterInput according to what
    /// the user wants to search. 0 = search an item based on it's "patrimônio"; 1 = search an item based on it's 
    /// name - no patrimônio item
    /// </summary>
    public void HandleDP(int value)
    {
        if (value == 0)
        {
            isSearchingPatrimonio = true;
            parameterInput.placeholder.GetComponent<TMP_Text>().text = "Digite patrimônio para busca....";
        }
        if(value == 1)
        {
            isSearchingPatrimonio = false;
            parameterInput.placeholder.GetComponent<TMP_Text>().text = "Digite nome do item para busca....";
        }
        parameterInput.text = "";
    }

    /// <summary>
    /// Search for all the movements of the desired item
    /// </summary>
    public void SearchClicked()
    {
        if (parameterInput.text != "")
        {
            DeleteOldSearch();
            if (isSearchingPatrimonio)
            {
                StartCoroutine(ImportPatrimonioMovementsRoutine());
            }
            else
            {
                StartCoroutine(ImportNoPaNoSeMovementsRoutine());
            }
        }
    }
}
