using UnityEngine;
using UnityEngine.Networking;

public class HandlePostRequestResponse
{
    /// <summary>
    /// Handles what happens after a web request is sent
    /// </summary>
    public static bool HandleWebRequest(UnityWebRequest requestToHandle)
    {
        bool requestWorked = false;
        if (requestToHandle.result == UnityWebRequest.Result.ConnectionError)
        {
            Debug.LogWarning("AddUpdate: conectionerror");
            EventHandler.CallOpenMessageEvent("Server error: 1");
        }
        else if (requestToHandle.result == UnityWebRequest.Result.DataProcessingError)
        {
            Debug.LogWarning("AddUpdate: data processing error");
            EventHandler.CallOpenMessageEvent("Server error: 2");
        }
        else if (requestToHandle.result == UnityWebRequest.Result.ProtocolError)
        {
            Debug.LogWarning("AddUpdate: protocol error");
            EventHandler.CallOpenMessageEvent("Server error: 3");
        }

        if (requestToHandle.error == null)
        {
            string response = requestToHandle.downloadHandler.text;
            if (response == "Conection error")
            {
                Debug.LogWarning("AddUpdate: Server error");
                EventHandler.CallOpenMessageEvent("Server error: 4");
            }
            else if (response == "Update failed")
            {
                Debug.LogWarning("Update: UpdateQuery failed");
                EventHandler.CallOpenMessageEvent("Server error: 5");
            }
            else if (response == "insert item failed")
            {
                Debug.LogWarning("Insert: InsertQuery failed");
                EventHandler.CallOpenMessageEvent("Server error: 6");
            }
            else if (response == "Patrimônio found")
            {
                Debug.Log("Patrimônio duplicado");
                EventHandler.CallIsOneMessageOnlyEvent(true);
                EventHandler.CallOpenMessageEvent("Patrimônio já existe");
            }
            else if (response == "Serial found")
            {
                EventHandler.CallIsOneMessageOnlyEvent(true);
                EventHandler.CallOpenMessageEvent("Serial já existe");
            }
            else if (response == "Modelo Found")
            {
                EventHandler.CallOpenMessageEvent("Modelo já está detalhado");
            }
            #region Check if already exists Queries
            else if (response == "Patrimônio query failed")
            {
                Debug.LogWarning("Insert inventario: Patrimonio query failed");
                EventHandler.CallOpenMessageEvent("Server error: 7.1");
            }
            else if (response == "Serial query failed")
            {
                Debug.LogWarning("Insert Inventario: Serial query failed");
                EventHandler.CallOpenMessageEvent("Server error: 7.2");
            }
            else if (response == "Desktop Patrimonio query failed")
            {
                Debug.LogWarning("Insert Desktop: Patrimonio query failed");
                EventHandler.CallOpenMessageEvent("Server error: 7.3");
            }
            else if (response == "Modelo query failed")
            {
                Debug.LogWarning("Insert: Modelo query failed");
                EventHandler.CallOpenMessageEvent("Server error: 7.4");
            }
            #endregion
            else if (response == "Wrong app key")
            {
                Debug.LogWarning("AddUpdate: app key");
                EventHandler.CallOpenMessageEvent("Server error: 8");
            }
            else if (response == "Worked")
            {
                // Debug.Log("Worked");
                requestWorked = true;
                EventHandler.CallOpenMessageEvent("Worked");
                EventHandler.CallPostRequestResponse(response);
            }
            else if (response == "Updated")
            {
                //  Debug.Log("Updated");
                requestWorked = true;
                EventHandler.CallOpenMessageEvent("Updated");
                EventHandler.CallPostRequestResponse(response);
            }
            else if(response == "1" || response == "2" || response == "3" || response == "5" || response == "10")
            {
                requestWorked = true;
                EventHandler.CallPostRequestResponse(response);
            }
            else
            {
                Debug.LogWarning("AddUpdate: " + response);
                EventHandler.CallOpenMessageEvent("Server error: 9");
            }
        }
        else
        {
            Debug.LogWarning("AddUpdate: " + requestToHandle.error);
            EventHandler.CallOpenMessageEvent("Server error: 10");
        }
        requestToHandle.Dispose();
        return requestWorked;
    }
}
