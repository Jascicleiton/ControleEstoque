using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Networking;
using SimpleJSON;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System.Linq;
using Saving;
using Newtonsoft.Json.Linq;

public class NoPaNoSeManager : Singleton<NoPaNoSeManager>, IJsonSaveable
{
    private NoPaNoSeAll allitems = new NoPaNoSeAll();

    #region New item
    [SerializeField] private GameObject newItemPanel = null;
    [SerializeField] private TMP_InputField newItemNameInput = null;
    [SerializeField] private TMP_InputField newItemQuantityInput = null;
    #endregion

    [SerializeField] private Transform itemParentTransform = null;
    [SerializeField] private GameObject itemPrefab = null;
    [SerializeField] private ScrollRect scrollRect;
    [HideInInspector] public bool inputEnabled = true;
    [SerializeField] private Button addNewItemButton = null;

    private int tempInt = 0; // Used for all int.TryParse

    /// <summary>
    /// Show all the available items on the online database, if any, and hide the option to add a new item if the
    /// access level is equal to 1, 2 or 4 - no adding allowed
    /// </summary>
    private void Start()
    {
        allitems = new NoPaNoSeAll();
        StartCoroutine(StartListRoutine());
        switch (UsersManager.Instance.currentUser.GetAccessLevel())
        {
            case 1:
            case 2:
            case 4:
                addNewItemButton.gameObject.SetActive(false);
                break;
            default:
                break;
        }
    }

    /// <summary>
    /// Add a new item to the screen and scrolls to the bottom of the list
    /// </summary>
    private void AddNewItem(string itemName, int itemQuantity)
    {
        GameObject newItem = Instantiate(itemPrefab, itemParentTransform);
        NoPaNoSeItemManager newItemManager = newItem.GetComponent<NoPaNoSeItemManager>();
        newItemManager.SetItemInformation(itemName, itemQuantity);
        allitems.noPaNoSeItems.Add(newItemManager.GetItem());
        StartCoroutine(ScrollToBottom());
    }

    /// <summary>
    /// Sort Alphabetically the imported list of items and show it
    /// </summary>
        private void ShowItems(List<NoPaNoSeItem> itemsToShow)
    {
        List<NoPaNoSeItem> sortedItemsToShow = itemsToShow.OrderBy(x => x.ItemName).ToList();
        for (int i = 0; i < itemsToShow.Count; i++)
        {
            AddNewItem(sortedItemsToShow[i].ItemName, sortedItemsToShow[i].Quantity);
        }
    }

    /// <summary>
    /// Import all items stored on the online database
    /// </summary>
    private IEnumerator StartListRoutine()
    {
        WWWForm itemForm = new WWWForm();
        itemForm.AddField("apppassword", ConstStrings.ImportDatabaseKey);

        UnityWebRequest createUpdateInventarioRequest = CreatePostRequest.GetPostRequest(itemForm, "importallnopanoseitems.php", 5);
        MouseManager.Instance.SetWaitingCursor();

        yield return createUpdateInventarioRequest.SendWebRequest();

        if (createUpdateInventarioRequest.result == UnityWebRequest.Result.ConnectionError)
        {
            EventHandler.CallDisconectedFromInternet();
            Debug.LogWarning("StartListRoutine: conectionerror");
        }
        else if (createUpdateInventarioRequest.result == UnityWebRequest.Result.DataProcessingError)
        {
            EventHandler.CallDisconectedFromInternet();
            Debug.LogWarning("StartListRoutine: data processing error");
        }
        else if (createUpdateInventarioRequest.result == UnityWebRequest.Result.ProtocolError)
        {
            EventHandler.CallDisconectedFromInternet();
            Debug.LogWarning("StartListRoutine: protocol error");
        }

        if (createUpdateInventarioRequest.error == null)
        {
            string response = createUpdateInventarioRequest.downloadHandler.text;
            if (response == "Conection error" || response == "Query failed")
            {
                EventHandler.CallDisconectedFromInternet();
                Debug.LogWarning("StartListRoutine: Server error");
            }
            else if (response == "Wrong app key")
            {
                EventHandler.CallDisconectedFromInternet();
                Debug.LogWarning("StartListRoutine: app key");
            }
            else if(response == "Result came empty")
            {
                EventHandler.CallDisconectedFromInternet();
            }
            else
            {
                JSONNode inventario = JSON.Parse(createUpdateInventarioRequest.downloadHandler.text);
                List<NoPaNoSeItem> tempItems = new List<NoPaNoSeItem>();
                foreach (JSONNode item in inventario)
                {
                    tempItems.Add(new NoPaNoSeItem(item[0], int.Parse(item[1])));
                }
                                ShowItems(tempItems);
            }
        }
        else
        {
            EventHandler.CallDisconectedFromInternet();
            Debug.LogWarning("StartListRoutine: " + createUpdateInventarioRequest.error);
            // TODO: send message to user with error and recomendation
        }
        createUpdateInventarioRequest.Dispose();
        MouseManager.Instance.SetDefaultCursor();
    }

    /// <summary>
    /// Try to add a new item to the online database and shows it on the screen
    /// </summary>
    private IEnumerator AddNewItemRoutine()
    {
        if (!int.TryParse(newItemQuantityInput.text, out tempInt))
        {
            EventHandler.CallIsOneMessageOnlyEvent(true);
            EventHandler.CallOpenMessageEvent("Invalid number format");
            yield break;
        }

        WWWForm itemForm = CreateForm.GetNoPaNoSeForm(ConstStrings.AddNewItemKey, newItemNameInput.text, tempInt);

        UnityWebRequest createUpdateInventarioRequest = CreatePostRequest.GetPostRequest(itemForm, "addnopanoseitem.php", 5);
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
                EventHandler.CallDatabaseUpdatedEvent();
                newItemPanel.SetActive(false);
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
    /// Scroll down to the bottom of the list to show the last added item
    /// </summary>
    private IEnumerator ScrollToBottom()
    {
        yield return new WaitForEndOfFrame();
        Canvas.ForceUpdateCanvases();
        scrollRect.verticalNormalizedPosition = 0f;
        Canvas.ForceUpdateCanvases();
    }

    /// <summary>
    /// Used by AddNewItem_btn inside newItemPanel
    /// </summary>
    public void AddNewItemClicked()
    {
        StartCoroutine(AddNewItemRoutine());
        
    }

    /// <summary>
    /// Used by AddNewItem_btn outside newItemPanel
    /// </summary>
    public void OpenAddNewItemPanel()
    {
        newItemPanel.SetActive(true);
        newItemNameInput.text = "";
        newItemQuantityInput.text = "";
      //  EventHandler.CallUpdateTabInputs();
    }

    /// <summary>
    /// Returns to InitialScene
    /// </summary>
    public void ReturnToInitialScene()
    {
        SceneManager.LoadScene(ConstStrings.SceneInitial);
    }

    /// <summary>
    /// Save all the NoPaNoSe items for the use of the offline program
    /// </summary>
    public JToken CaptureAsJToken()
    {
        JArray state = HandleNoPaNoSeItemForSaveAndLoad.SaveObject(allitems);
        return state;
    }

    /// <summary>
    /// Load all the NoPaNoSe items for the use of the offline program
    /// </summary>
    public void RestoreFromJToken(JToken state)
    {
        HandleNoPaNoSeItemForSaveAndLoad.LoadJObject(state, out allitems);
        ShowItems(allitems.noPaNoSeItems);
    }
}
