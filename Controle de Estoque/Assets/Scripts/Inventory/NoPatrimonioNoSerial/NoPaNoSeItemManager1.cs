using System.Collections;
using UnityEngine;
using UnityEngine.Networking;
using System;

public class NoPaNoSeItemManager1
{
    public static bool quantityChanged = false;        
    /// <summary>
    /// Change the quantity of an already existing item
    /// </summary>
    public static IEnumerator ChangeItemQuantityRoutine(NoPaNoSeItem itemToChange, int quantityToMove)
    {
        int itemNewQuantity = itemToChange.Quantity + quantityToMove;

        itemToChange.Quantity = itemNewQuantity;
        WWWForm itemForm = CreateForm.GetNoPaNoSeForm(ConstStrings.UpdateItemKey, itemToChange.ItemName, itemNewQuantity);

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
                Debug.Log("moved");
                quantityChanged = true;
                EventHandler.CallNoPaNoSeItemQuantityChanged(itemToChange.Quantity);
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

      public static bool CanChangeQuantity(NoPaNoSeItem item, int quantityToMove)
    {
        if(quantityToMove == 0)
        {
            //Send message error
            Debug.Log("zero items to move");
            return false;
        }
        if(quantityToMove > 0)
        {
            Debug.Log("positive number to move");
            return true;
        }
        else
        {
            if(item.Quantity + quantityToMove >= 0) 
            {
                Debug.Log("can reduce number");
                return true;
            }
            else
            {
                Debug.Log("moving more than available");
                return false;
            }
        }
    }

   
    /// <summary>
    /// Called to generate a movement record each time an item quantity is changed. It is a different movement record from an item that do
    /// have "serial" and "patrimônio"
    /// </summary>
    public static IEnumerator MoveItem(NoPaNoSeItem itemToChange, int quantityToMove, string whereFrom, string whereTo)
    {
        WWWForm itemForm = new WWWForm();
        itemForm = CreateForm.GetMoveNoPaNoSeItemForm(ConstStrings.MoveItemKey, itemToChange.ItemName, quantityToMove,
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
        quantityChanged = false;
        createUpdateInventarioRequest.Dispose();
        MouseManager.Instance.SetDefaultCursor();
    }    
}
