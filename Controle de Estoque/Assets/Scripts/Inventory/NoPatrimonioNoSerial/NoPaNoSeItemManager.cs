using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Networking;
using System;

public class NoPaNoSeItemManager : MonoBehaviour
{
    [SerializeField] private TMP_Text itemName = null;
    [SerializeField] private TMP_Text itemQuantity = null;
    [SerializeField] private TMP_InputField quantityInput = null;
    [SerializeField] private TMP_Dropdown whereToDP = null;
    [SerializeField] private TMP_InputField locationInput = null;
    [SerializeField] private Button minusButton = null;
    [SerializeField] private Button plusButton = null;

    private NoPaNoSeItem item = new NoPaNoSeItem();
    private int tempInt = 0;

    /// <summary>
    /// Update the item informations
    /// </summary>
    private void UpdateText()
    {
        itemName.text = item.ItemName;
        itemQuantity.text = item.Quantity.ToString();
    }

    /// <summary>
    /// Change the quantity of an already existing item
    /// </summary>
    private IEnumerator ChangeItemQuantityRoutine(bool add)
    {
        int itemNewQuantity = 0;
        int tempQuantity = 0;
        if(int.Parse(quantityInput.text) < 0)
        {
            tempQuantity = -(int.Parse(quantityInput.text));
        }
        else
        {
            tempQuantity = int.Parse(quantityInput.text);
        }
        if (add)
        {
            itemNewQuantity = item.Quantity + tempQuantity;
        }
        else
        {
            itemNewQuantity = item.Quantity - tempQuantity;
        }

        item.Quantity = itemNewQuantity;
        WWWForm itemForm = CreateForm.GetNoPaNoSeForm(ConstStrings.UpdateItemKey, item.ItemName, itemNewQuantity);

        UnityWebRequest createUpdateInventarioRequest = UnityWebRequest.Post(ConstStrings.PhpNoPaNoSeItemsFolder + ConstStrings.UpdateNoPaNoSe, itemForm);
        MouseManager.Instance.SetWaitingCursor();

        yield return createUpdateInventarioRequest.SendWebRequest();

        if (createUpdateInventarioRequest.result == UnityWebRequest.Result.ConnectionError)
        {
            Debug.LogWarning("ChangeItemQuantityRoutine: conectionerror");
        }
        else if (createUpdateInventarioRequest.result == UnityWebRequest.Result.DataProcessingError)
        {
            Debug.LogWarning("ChangeItemQuantityRoutine: data processing error");
        }
        else if (createUpdateInventarioRequest.result == UnityWebRequest.Result.ProtocolError)
        {
            Debug.LogWarning("ChangeItemQuantityRoutine: protocol error");
        }

        if (createUpdateInventarioRequest.error == null)
        {
            string response = createUpdateInventarioRequest.downloadHandler.text;
            if (response == "Conection error" || response == "Inventario update failed")
            {
                Debug.LogWarning("ChangeItemQuantityRoutine: Server error");
            }
            else if (response == "Wrong app key")
            {
                Debug.LogWarning("ChangeItemQuantityRoutine: app key");
            }
            else if (response == "Updated")
            {
                SetItemInformation(item.ItemName, itemNewQuantity);
                quantityInput.text = "";
                whereToDP.GetComponent<LocationDropDownHandler>().ResetDropDown();
                EventHandler.CallDatabaseUpdatedEvent();
            }
            else
            {
                Debug.LogWarning("ChangeItemQuantityRoutine: " + response);
                // TODO: send message to user with error and recomendation
            }
        }
        else
        {
            Debug.LogWarning("ChangeItemQuantityRoutine: " + createUpdateInventarioRequest.error);
            // TODO: send message to user with error and recomendation
        }
        createUpdateInventarioRequest.Dispose();
        MouseManager.Instance.SetDefaultCursor();
    }

    /// <summary>
    /// Set the item information as is stored on the online database. Called by NoPaNoSeManager  each time an item is instantiated
    /// </summary>
    public void SetItemInformation(string name, int quantity)
    {
        item.ItemName = name;
        item.Quantity = quantity;
        UpdateText();
        switch (UsersManager.Instance.currentUser.GetAccessLevel())
        {
            case 2:
            case 4:
                minusButton.GetComponent<CanvasGroup>().alpha = 0;
                minusButton.interactable = false;
                plusButton.GetComponent<CanvasGroup>().alpha = 0;
                plusButton.interactable = false;
                whereToDP.GetComponent<CanvasGroup>().alpha = 0;
                whereToDP.interactable = false;
                quantityInput.GetComponent<CanvasGroup>().alpha = 0;
                quantityInput.interactable = false;
                break;
            default:
                break;
        }
    }

    /// <summary>
    /// Trye to change the quantity value of an item. It won't change nor move the item if the quantity drops below 0
    /// </summary>
    public void ChangeItemQuantity(bool add)
    {
        if (add)
        {
            StartCoroutine(ChangeItemQuantityRoutine(add));
            StartCoroutine(MoveItem(add));
        }
        else
        {
            if (int.TryParse(quantityInput.text, out tempInt))
            {
                if (item.Quantity - tempInt < 0)
                {
                    EventHandler.CallIsOneMessageOnlyEvent(true);
                    EventHandler.CallOpenMessageEvent("Negative number");
                }
                else
                {
                    StartCoroutine(ChangeItemQuantityRoutine(add));
                    StartCoroutine(MoveItem(add));
                }
            }
            else
            {
                EventHandler.CallIsOneMessageOnlyEvent(true);
                EventHandler.CallOpenMessageEvent("Invalid number");
            }
        }    
    }

    /// <summary>
    /// Called to generate a movement record each time an item quantity is changed. It is a different movement record from an item that do
    /// have "serial" and "patrimônio"
    /// </summary>
    private IEnumerator MoveItem(bool isAdding)
    {
        WWWForm itemForm = new WWWForm();
        string location = "";
        if(HelperMethods.GetLocationFromDP(whereToDP.value) == "Outros")
        {
            location = locationInput.text;
        }
        else
        {
            location = HelperMethods.GetLocationFromDP(whereToDP.value);
        }
        if (isAdding)
        {
             itemForm = CreateForm.GetMoveNoPaNoSeItemForm(ConstStrings.MoveItemKey, item.ItemName, int.Parse(quantityInput.text),
           UsersManager.Instance.currentUser.GetUsername(), DateTime.Now.ToString("dd/MM/yyyy"), location, "Estoque");
        }
        else
        {
             itemForm = CreateForm.GetMoveNoPaNoSeItemForm(ConstStrings.MoveItemKey, item.ItemName, int.Parse(quantityInput.text),
          UsersManager.Instance.currentUser.GetUsername(), DateTime.Now.ToString("dd/MM/yyyy"), "Estoque", location);
        
        }

        UnityWebRequest createUpdateInventarioRequest = UnityWebRequest.Post(ConstStrings.PhpNoPaNoSeItemsFolder + ConstStrings.MoveNoPaNoSe, itemForm);
        MouseManager.Instance.SetWaitingCursor();

        yield return createUpdateInventarioRequest.SendWebRequest();
    
        if (createUpdateInventarioRequest.result == UnityWebRequest.Result.ConnectionError)
        {
            Debug.LogWarning("MoveItem: conectionerror");
        }
        else if (createUpdateInventarioRequest.result == UnityWebRequest.Result.DataProcessingError)
        {
            Debug.LogWarning("MoveItem: data processing error");
        }
        else if (createUpdateInventarioRequest.result == UnityWebRequest.Result.ProtocolError)
        {
            Debug.LogWarning("MoveItem: protocol error");
        }

        if (createUpdateInventarioRequest.error == null)
        {
            string response = createUpdateInventarioRequest.downloadHandler.text;
            if (response == "Conection error" || response == "Inventario update failed")
            {
                Debug.LogWarning("MoveItem: Server error");
            }
            else if (response == "Wrong app key")
            {
                Debug.LogWarning("MoveItem: app key");
            }
            else if (response == "Moved")
            {
               
            }
            else
            {
                Debug.LogWarning("MoveItem: " + response);
                // TODO: send message to user with error and recomendation
            }
        }
        else
        {
            Debug.LogWarning("MoveItem: " + createUpdateInventarioRequest.error);
            // TODO: send message to user with error and recomendation
        }
        createUpdateInventarioRequest.Dispose();
        MouseManager.Instance.SetDefaultCursor();
    }

    /// <summary>
    /// Get the item that is stored in this instance
    /// </summary>
    public NoPaNoSeItem GetItem()
    {
        return item;
    }

    /// <summary>
    /// Show the locationInput if the location is "Outros" and hide the location input if the location is something
    /// else
    /// </summary>
    public void HandleInputData(int value)
    {
        if (HelperMethods.GetLocationFromDP(value) == "Outros")
        {
            locationInput.gameObject.SetActive(true);
            locationInput.text = "";
        }
        else
        {
            locationInput.gameObject.SetActive(false);
        }
    }
}


