using System.Collections;
using UnityEngine;
using UnityEngine.Networking;
using System;
using Assets.Scripts.Misc;
using Assets.Scripts.Mouse;
using Assets.Scripts.Users;
using Assets.Scripts.Web;

namespace Assets.Scripts.Inventory.NoPatrimonioNoSerial
{
    public class NoPaNoSeItemManager
    {
        public bool QuantityChanged { get ; private set ; }

        /// <summary>
        /// Change the quantity of an already existing item
        /// </summary>
        public IEnumerator ChangeItemQuantityRoutine(NoPaNoSeItem itemToChange, int quantityToMove, bool isAdding, string whereFrom, string whereTo)
        {
            int itemNewQuantity = 0;
            if (isAdding)
            {
                itemNewQuantity = itemToChange.Quantity + quantityToMove;
            }
            else
            {
                itemNewQuantity = itemToChange.Quantity - quantityToMove;
            }
            itemToChange.Quantity = itemNewQuantity;
            WWWForm itemForm = CreateForm.GetMoveNoPaNoSeItemForm(ConstStrings.UpdateItemKey, itemToChange.ItemName, itemNewQuantity,
                UsersManager.Instance.CurrentUser.GetUsername(), DateTime.Now.ToString("dd/MM/yyyy"), whereFrom, whereTo);

            UnityWebRequest createChangeItemQuantityRequest = CreatePostRequest.GetPostRequest(itemForm, ConstStrings.UpdateNoPaNoSe, 5);
            MouseManager.Instance.SetWaitingCursor();

            yield return createChangeItemQuantityRequest.SendWebRequest();

            if (HandlePostRequestResponse.HandleWebRequest(createChangeItemQuantityRequest))
            {
               
                QuantityChanged = true;
                EventHandler.CallNoPaNoSeItemQuantityChanged(itemToChange.Quantity);
                EventHandler.CallDatabaseUpdatedEvent();
            }

            createChangeItemQuantityRequest.Dispose();
            MouseManager.Instance.SetDefaultCursor();
        }

        public static bool CanChangeQuantity(NoPaNoSeItem item, int quantityToMove)
        {
            if (item.Quantity - quantityToMove >= 0)
            {
                //           Debug.Log("can reduce number");
                return true;
            }
            else
            {
                //         Debug.Log("moving more than available");
                return false;
            }
        }

        /// <summary>
        /// Called to generate a movement record each time an item quantity is changed. It is a different movement record from an item that do
        /// have "serial" and "patrimônio"
        /// </summary>
        public IEnumerator MoveItem(NoPaNoSeItem itemToChange, int quantityToMove, string whereFrom, string whereTo)
        {
            WWWForm itemForm = new WWWForm();
            itemForm = CreateForm.GetMoveNoPaNoSeItemForm(ConstStrings.MoveItemKey, itemToChange.ItemName, quantityToMove,
               UsersManager.Instance.CurrentUser.GetUsername(), DateTime.Now.ToString("dd/MM/yyyy"), whereFrom, whereTo);

            UnityWebRequest createMoveItemRequest = UnityWebRequest.Post(ConstStrings.PhpNoPaNoSeItemsFolder + ConstStrings.MoveNoPaNoSe, itemForm);
            MouseManager.Instance.SetWaitingCursor();

            yield return createMoveItemRequest.SendWebRequest();
            if (HandlePostRequestResponse.HandleWebRequest(createMoveItemRequest))
            {
                EventHandler.CallIsOneMessageOnlyEvent(true);
                EventHandler.CallOpenMessageEvent("Item moved");
            }



            QuantityChanged = false;
            createMoveItemRequest.Dispose();
            MouseManager.Instance.SetDefaultCursor();
        }
    }
}