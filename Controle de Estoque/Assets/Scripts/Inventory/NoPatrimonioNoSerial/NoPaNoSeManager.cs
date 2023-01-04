using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Networking;
using SimpleJSON;

public class NoPaNoSeManager : Singleton<NoPaNoSeManager>
{
    private NoPaNoSeAll allitems = new NoPaNoSeAll();

    #region New item
    [SerializeField] private GameObject newItemPanel = null;
    [SerializeField] private TMP_InputField newItemNameInput = null;
    [SerializeField] private TMP_InputField newItemQuantityInput = null;
    #endregion

    [SerializeField] private Transform itemParentTransform = null;
    [SerializeField] private GameObject itemPrefab = null;

    [HideInInspector] public bool inputEnabled = true;

    private void Start()
    {
        allitems = new NoPaNoSeAll();
        StartCoroutine(StartListRoutine());
    }

    private void AddNewItem(string itemName, int itemQuantity)
    {
        GameObject newItem = Instantiate(itemPrefab, itemParentTransform);
        NoPaNoSeItemManager newItemManager = newItem.GetComponent<NoPaNoSeItemManager>();
        newItemManager.SetItemInformation(itemName, itemQuantity);
        allitems.noPaNoSeItems.Add(newItemManager.GetItem());
    }

    private IEnumerator StartListRoutine()
    {
        WWWForm itemForm = new WWWForm();
        itemForm.AddField("apppassword", ConstStrings.ImportDatabaseKey);

        UnityWebRequest createUpdateInventarioRequest = UnityWebRequest.Post(ConstStrings.PhpNoPaNoSeItemsFolder + "importallnopanoseitems.php", itemForm);
        MouseManager.Instance.SetWaitingCursor();

        yield return createUpdateInventarioRequest.SendWebRequest();

        if (createUpdateInventarioRequest.result == UnityWebRequest.Result.ConnectionError)
        {
            Debug.LogWarning("StartListRoutine: conectionerror");
        }
        else if (createUpdateInventarioRequest.result == UnityWebRequest.Result.DataProcessingError)
        {
            Debug.LogWarning("StartListRoutine: data processing error");
        }
        else if (createUpdateInventarioRequest.result == UnityWebRequest.Result.ProtocolError)
        {
            Debug.LogWarning("StartListRoutine: protocol error");
        }

        if (createUpdateInventarioRequest.error == null)
        {
            string response = createUpdateInventarioRequest.downloadHandler.text;
            if (response == "Conection error" || response == "Query failed")
            {
                Debug.LogWarning("StartListRoutine: Server error");
            }
            else if (response == "Wrong app key")
            {
                Debug.LogWarning("StartListRoutine: app key");
            }
            else if(response == "Result came empty")
            {
                // do nothing
            }
            else
            {
                JSONNode inventario = JSON.Parse(createUpdateInventarioRequest.downloadHandler.text);
                foreach (JSONNode item in inventario)
                {
                    AddNewItem(item[0], int.Parse(item[1]));
                }
            }
        }
        else
        {
            Debug.LogWarning("StartListRoutine: " + createUpdateInventarioRequest.error);
            // TODO: send message to user with error and recomendation
        }
        createUpdateInventarioRequest.Dispose();
        MouseManager.Instance.SetDefaultCursor();
    }

    private IEnumerator AddNewItemRoutine()
    {     
        WWWForm itemForm = CreateForm.GetNoPaNoSeForm(ConstStrings.AddNewItemKey, newItemNameInput.text, int.Parse(newItemQuantityInput.text));

        UnityWebRequest createUpdateInventarioRequest = UnityWebRequest.Post(ConstStrings.PhpNoPaNoSeItemsFolder + "addnopanoseitem.php", itemForm);
        MouseManager.Instance.SetWaitingCursor();

        yield return createUpdateInventarioRequest.SendWebRequest();

        if (createUpdateInventarioRequest.result == UnityWebRequest.Result.ConnectionError)
        {
            Debug.LogWarning("AddNewItemRoutine: conectionerror");
        }
        else if (createUpdateInventarioRequest.result == UnityWebRequest.Result.DataProcessingError)
        {
            Debug.LogWarning("AddNewItemRoutine: data processing error");
        }
        else if (createUpdateInventarioRequest.result == UnityWebRequest.Result.ProtocolError)
        {
            Debug.LogWarning("AddNewItemRoutine: protocol error");
        }

        if (createUpdateInventarioRequest.error == null)
        {
            string response = createUpdateInventarioRequest.downloadHandler.text;
            if (response == "Conection error" || response == "Inventario update failed")
            {
                Debug.LogWarning("AddNewItemRoutine: Server error");
            }
            else if (response == "Wrong app key")
            {
                Debug.LogWarning("AddNewItemRoutine: app key");
            }
            else if (response == "Item added")
            {
                AddNewItem(newItemNameInput.text, int.Parse(newItemQuantityInput.text));
                Debug.Log("Worked");
            }
            else
            {
                Debug.LogWarning("AddNewItemRoutine: " + response);
                // TODO: send message to user with error and recomendation
            }
        }
        else
        {
            Debug.LogWarning("AddNewItemRoutine: " + createUpdateInventarioRequest.error);
            // TODO: send message to user with error and recomendation
        }
        createUpdateInventarioRequest.Dispose();
        MouseManager.Instance.SetDefaultCursor();
    }

    /// <summary>
    /// Used by AddNewItem_btn inside newItemPanel
    /// </summary>
    public void AddNewItemClicked()
    {
        StartCoroutine(AddNewItemRoutine());
        newItemPanel.SetActive(false);
    }

    /// <summary>
    /// Used by AddNewItem_btn outside newItemPanel
    /// </summary>
    public void OpenAddNewItemPanel()
    {
        newItemPanel.SetActive(true);
        newItemNameInput.text = "";
        newItemQuantityInput.text = "";
    }
}
