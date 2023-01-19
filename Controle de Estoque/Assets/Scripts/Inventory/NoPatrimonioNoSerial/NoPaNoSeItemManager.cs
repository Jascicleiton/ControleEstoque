using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Networking;

public class NoPaNoSeItemManager : MonoBehaviour
{
    [SerializeField] private TMP_Text itemName = null;
    [SerializeField] private TMP_Text itemQuantity = null;
    [SerializeField] private TMP_InputField quantityInput = null;
    #pragma warning disable CS0219 // Variable is assigned but its value is never used
    [SerializeField] private TMP_InputField whereToInput = null;
    #pragma warning restore CS0219 // Variable is assigned but its value is never used

    private NoPaNoSeItem item = new NoPaNoSeItem();


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
        if(add)
        {
            itemNewQuantity = item.Quantity + int.Parse(quantityInput.text);
        }
        else
        {
            itemNewQuantity = item.Quantity - int.Parse(quantityInput.text);
        }
        WWWForm itemForm = CreateForm.GetNoPaNoSeForm(ConstStrings.UpdateItemKey, item.ItemName, itemNewQuantity);

        UnityWebRequest createUpdateInventarioRequest = UnityWebRequest.Post(ConstStrings.PhpNoPaNoSeItemsFolder + "updatenopanose.php", itemForm);
        MouseManager.Instance.SetWaitingCursor(this.gameObject);
        
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
                Debug.Log("Worked");               
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

    public void SetItemInformation(string name, int quantity)
    {
        item.ItemName = name;
        item.Quantity = quantity;
        UpdateText();
    }

    public void ChangeItemQuantity(bool add)
    {
        if (add)
        {
            StartCoroutine(ChangeItemQuantityRoutine(add));
        }
        else
        {
            if (item.Quantity - int.Parse(quantityInput.text) < 0)
            {
                //TODO: Show message saying that the user is trying to remove more items than there are available
            }
            else
            {
                StartCoroutine(ChangeItemQuantityRoutine(add));
            }
        }
     
    }

    public NoPaNoSeItem GetItem()
    {
        return item;
    }
}
