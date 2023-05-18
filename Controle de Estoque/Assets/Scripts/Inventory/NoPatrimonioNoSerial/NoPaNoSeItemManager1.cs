using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Networking;
using System;

public class NoPaNoSeItemManager1 : MonoBehaviour
{
    private NoPaNoSeItem item = new NoPaNoSeItem();
        
    /// <summary>
    /// Change the quantity of an already existing item
    /// </summary>
    private IEnumerator ChangeItemQuantityRoutine(int quantityToMove)
    {
        int itemNewQuantity = item.Quantity + quantityToMove;
        
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
                Debug.Log("moved");
                EventHandler.CallNoPaNoSeItemQuantityChanged(quantityToMove);
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
       
    }

    /// <summary>
    /// Trye to change the quantity value of an item. It won't change nor move the item if the quantity drops below 0
    /// </summary>
    public void ChangeItemQuantity(NoPaNoSeItem itemToChange, int quantityToMove, string whereFrom, string whereTo)
    {
        item = itemToChange;

        if (quantityToMove < 0)
        {
            if (itemToChange.Quantity + quantityToMove < 0)
            {
                EventHandler.CallIsOneMessageOnlyEvent(true);
                EventHandler.CallOpenMessageEvent("Negative number");
                return;
            }
        }
        StartCoroutine(ChangeItemQuantityRoutine(quantityToMove));
        StartCoroutine(MoveItem(quantityToMove, whereFrom, whereTo));
    }

    /// <summary>
    /// Called to generate a movement record each time an item quantity is changed. It is a different movement record from an item that do
    /// have "serial" and "patrimônio"
    /// </summary>
    private IEnumerator MoveItem(int quantityToMove, string whereFrom, string whereTo)
    {
        WWWForm itemForm = new WWWForm();
        itemForm = CreateForm.GetMoveNoPaNoSeItemForm(ConstStrings.MoveItemKey, item.ItemName, quantityToMove,
           UsersManager.Instance.currentUser.GetUsername(), DateTime.Now.ToString("dd/MM/yyyy"), whereFrom, whereTo);

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
    
}
