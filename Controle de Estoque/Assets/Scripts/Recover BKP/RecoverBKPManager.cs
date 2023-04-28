using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class RecoverBKPManager : MonoBehaviour
{
    public void RecoverBKP()
    {
        StartCoroutine(RecoverBKPRoutine());
    }

    private IEnumerator RecoverBKPRoutine()
    {
        print("Starting Routine");
        int index = 0;
        foreach (var item in InternalDatabase.Instance.fullDatabase.itens)
        {
            print("item: " + index);
            index++;
            List<string> parameters = new List<string>() { item.Aquisicao, item.Entrada, item.Patrimonio.ToString(),
            item.Status, item.Serial, item.Categoria, item.Fabricante, item.Modelo, item.Local, item.Saida, item.Observacao};
            WWWForm itemForm = CreateForm.GetInventarioForm(ConstStrings.RecoverBKPKey, parameters);

            UnityWebRequest createRecoverBKPRequest = CreatePostRequest.GetPostRequest(itemForm, ConstStrings.RecoverBKPPHP, 6);
            MouseManager.Instance.SetWaitingCursor();

            yield return createRecoverBKPRequest.SendWebRequest();
            if (createRecoverBKPRequest.result == UnityWebRequest.Result.ConnectionError)
            {
                Debug.LogWarning("RecoveryBKP: conectionerror");
                EventHandler.CallOpenMessageEvent("Server error: 1");
            }
            else if (createRecoverBKPRequest.result == UnityWebRequest.Result.DataProcessingError)
            {
                Debug.LogWarning("RecoveryBKP: data processing error");
                EventHandler.CallOpenMessageEvent("Server error: 2");
            }
            else if (createRecoverBKPRequest.result == UnityWebRequest.Result.ProtocolError)
            {
                Debug.LogWarning("RecoveryBKP: protocol error");
                EventHandler.CallOpenMessageEvent("Server error: 3");
            }
            if(createRecoverBKPRequest.error == null)
            {
                string response = createRecoverBKPRequest.downloadHandler.text;
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
            }
           
        }
        MouseManager.Instance.SetDefaultCursor();
    }
}