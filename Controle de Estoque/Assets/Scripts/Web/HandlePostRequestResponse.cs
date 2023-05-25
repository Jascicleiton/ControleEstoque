using UnityEngine;
using UnityEngine.Networking;

public class HandlePostRequestResponse
{
    /*
    Error codes:
    Server error: 1 - Erro de conexão ao banco de dados
    Server error: 2 - DataProcessing error
    Server error: 3 - Erro de protocolo
    Server error: 4 - Erro de conexão ao banco de dados
    Server error: 5 - Erro na atualização de um item
    Server error: 6 - Erro ao adicionar um item
    Server error: 7.1 - Patrimonio query error
    Server error: 7.2 - Serial query error
    Server error: 7.3 - Modelo query error
    Server error: 7.4 - Username query error
    Server error: 8 - Wrong appkey
    Server error: 9 - Error not handled
    Server error: 10 - Request received an error
    Server error: 11 - Insert new user failed
    */

    /// <summary>
    /// Handles what happens after a web request is sent
    /// </summary>
    public static bool HandleWebRequest(UnityWebRequest requestToHandle)
    {
        bool requestWorked = false;
        if (requestToHandle.result == UnityWebRequest.Result.ConnectionError)
        {
            Debug.LogWarning("Conectionerror");
            EventHandler.CallIsOneMessageOnlyEvent(true);
            EventHandler.CallOpenMessageEvent("Server error: 1");
        }
        else if (requestToHandle.result == UnityWebRequest.Result.DataProcessingError)
        {
            Debug.LogWarning("Data processing error");
            EventHandler.CallIsOneMessageOnlyEvent(true);
            EventHandler.CallOpenMessageEvent("Server error: 2");
        }
        else if (requestToHandle.result == UnityWebRequest.Result.ProtocolError)
        {
            Debug.LogWarning("Protocol error");
            EventHandler.CallIsOneMessageOnlyEvent(true);
            EventHandler.CallOpenMessageEvent("Server error: 3");
        }

        if (requestToHandle.error == null)
        {
            string response = requestToHandle.downloadHandler.text;
            if (response == "Database conection error")
            {
                Debug.LogWarning("AddUpdate: Server error");
                EventHandler.CallIsOneMessageOnlyEvent(true);
                EventHandler.CallOpenMessageEvent("Server error: 4");
            }
            else if (response == "Update failed")
            {
                Debug.LogWarning("Update: UpdateQuery failed");
                EventHandler.CallIsOneMessageOnlyEvent(true);
                EventHandler.CallOpenMessageEvent("Server error: 5");
            }
            else if (response == "insert item failed")
            {
                Debug.LogWarning("Insert: InsertQuery failed");
                EventHandler.CallIsOneMessageOnlyEvent(true);
                EventHandler.CallOpenMessageEvent("Server error: 6");
            }
            else if (response == "Patrimônio found")
            {
                Debug.Log("Patrimônio duplicado");
                EventHandler.CallIsOneMessageOnlyEvent(true);
                EventHandler.CallIsOneMessageOnlyEvent(true);
                EventHandler.CallOpenMessageEvent("Patrimônio já existe");
            }
            else if (response == "Serial found")
            {
                EventHandler.CallIsOneMessageOnlyEvent(true);
                EventHandler.CallIsOneMessageOnlyEvent(true);
                EventHandler.CallOpenMessageEvent("Serial já existe");
            }
            else if (response == "Modelo found")
            {
                EventHandler.CallOpenMessageEvent("Modelo já está detalhado");
            }
            #region Queries errors
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
            else if (response == "Modelo query failed")
            {
                Debug.LogWarning("Insert: Modelo query failed");
                EventHandler.CallOpenMessageEvent("Server error: 7.3");
            }
            else if (response == "Username query ran into an error")
            {
                EventHandler.CallIsOneMessageOnlyEvent(true);
                EventHandler.CallOpenMessageEvent("Server error: 7.4");
            }
            #endregion
            else if (response == "wrong appkey")
            {
                Debug.LogWarning("Wrong app key");
                EventHandler.CallOpenMessageEvent("Server error: 8");
            }
            else if (response == "Worked")
            {
                Debug.Log("Worked");
                requestWorked = true;
                EventHandler.CallOpenMessageEvent("Worked");
                EventHandler.CallPostRequestResponse(response);
            }
            else if (response == "Updated")
            {
                Debug.Log("Updated");
                requestWorked = true;
                EventHandler.CallOpenMessageEvent("Updated");
                EventHandler.CallPostRequestResponse(response);
            }
            else if(response == "1" || response == "2" || response == "3" || response == "4" || response == "5" || response == "10")
            {
                requestWorked = true;
                EventHandler.CallPostRequestResponse(response);
            }
            else if(response == "Username does not exist")
            {
                EventHandler.CallIsOneMessageOnlyEvent(true);
                EventHandler.CallOpenMessageEvent("Username null");
            }
            else if(response == "Duplicate username")
            {
                EventHandler.CallIsOneMessageOnlyEvent(true);
                EventHandler.CallOpenMessageEvent("Duplicate username");
            }
            else if(response == "Password was not able to be verified")
            {
                EventHandler.CallIsOneMessageOnlyEvent(true);
                EventHandler.CallOpenMessageEvent("Wrong password");
            }
            else if(response == "Can create new user")
            {
                requestWorked = true;
            }
            else if(response == "Changed")
            {
                Debug.Log(response);
                requestWorked = true;
                EventHandler.CallOpenMessageEvent("Updated");
            }
            else if (response == "ChangedMoved")
            {
                Debug.Log(response);
                requestWorked = true;
                EventHandler.CallIsOneMessageOnlyEvent(true);
                EventHandler.CallOpenMessageEvent("Item moved");
            }
            else if(response == "Moved")
            {
                Debug.Log(response);
                requestWorked = true;
                EventHandler.CallOpenMessageEvent("Worked");
            }
            else if(response == "Username already exist")
            {
                EventHandler.CallIsOneMessageOnlyEvent(true);
                EventHandler.CallOpenMessageEvent("Username already exist");
            }
            else if(response == "Insert user failed")
            {
                EventHandler.CallIsOneMessageOnlyEvent(true);
                EventHandler.CallOpenMessageEvent("Server error: 11");
            }
            else if(response == "User added")
            {
                requestWorked = true;
                EventHandler.CallIsOneMessageOnlyEvent(true);
                EventHandler.CallOpenMessageEvent("User added");
            }
            else
            {
                Debug.LogWarning("Not recorded response: " + response);
                EventHandler.CallOpenMessageEvent("Server error: 9");
                EventHandler.CallPostRequestResponse(response);
            }
        }
        else
        {
            Debug.LogWarning("Error: " + requestToHandle.error);
            EventHandler.CallOpenMessageEvent("Server error: 10");
            EventHandler.CallPostRequestResponse(requestToHandle.error);
        }
        requestToHandle.Dispose();
        return requestWorked;
    }
}
