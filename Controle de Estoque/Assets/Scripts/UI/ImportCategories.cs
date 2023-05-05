using Newtonsoft.Json.Linq;
using SimpleJSON;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class ImportCategories : MonoBehaviour, IJsonSaveable
{
    public List<string> categories = new List<string>();
    // Start is called before the first frame update
    void Start()
    {
        if (Application.platform != RuntimePlatform.WindowsEditor && Application.platform != RuntimePlatform.WindowsPlayer)
        {
            StartCoroutine(ImportCategoriesRoutine());
        }
        else if (Application.isEditor)
        {
            StartCoroutine(ImportCategoriesRoutine());
        }
      //  GambiarraParaCarregarCategorias();
    }

    /// <summary>
    /// Import all categories from the online database to InternalDatabase
    /// </summary>
    private IEnumerator ImportCategoriesRoutine()
    {
        WWWForm categoriesForm = new WWWForm();
        categoriesForm.AddField("apppassword", "ImportDatabase");

        UnityWebRequest categoriesRequest = CreatePostRequest.GetPostRequest(categoriesForm, "importcategories.php", 1);

        yield return categoriesRequest.SendWebRequest();

        Sheet tempSheet = new Sheet();
        tempSheet.itens = new List<ItemColumns>();

        if (categoriesRequest.result == UnityWebRequest.Result.ConnectionError)
        {
            Debug.LogWarning("ImportLocations: conection error");
        }
        else if (categoriesRequest.result == UnityWebRequest.Result.DataProcessingError)
        {
            Debug.LogWarning("ImportLocations: data processing error");
        }
        else if (categoriesRequest.result == UnityWebRequest.Result.ProtocolError)
        {
            Debug.LogWarning("ImportLocations: protocol error");
        }
        if (categoriesRequest.error == null)
        {
            string response = categoriesRequest.downloadHandler.text;
            if (response == "Database connection error" || response == "wrong appkey" || response == "Query failed")
            {
                Debug.LogWarning("ImportLocations: Server error");
            }
            else if (response == "Result came empty")
            {
                Debug.LogWarning("ImportLocations: Result came empty");
            }
            else
            {
                InternalDatabase.categories.Clear();
                JSONNode inventario = JSON.Parse(categoriesRequest.downloadHandler.text);
                foreach (JSONNode item in inventario)
                {
                    InternalDatabase.categories.Add(item[0]);
                }
                InternalDatabase.categories.Sort();
            }
        }
        categoriesRequest.Dispose();
    }
   
    /// <summary>
    /// Used only as a temporary solution to have Categories on the program
    /// </summary>
    private void GambiarraParaCarregarCategorias()
    {
        InternalDatabase.categories.Clear();
        InternalDatabase.categories.AddRange(new List<string> {"Adaptador AC", "Carregador", "Desktop", "Fonte", "Gbic", "HD", "iDrac", "Memória",
            "Monitor", "Mouse", "No break", "Notebook", "Outros", "Placa controladora", "Placa de captura de vídeo", "Placa de rede", "Placa de som",
            "Placa de vídeo", "Processador", "Roteador", "Servidor", "Storage NAS", "Switch","Teclado", "USB"});
    }


    public JToken CaptureAsJToken()
    {
        JArray state = new JArray();
        IList<JToken> stateList = state;
        foreach (var item in InternalDatabase.categories)
        {
            JObject jObjectToReturn = new JObject();
            IDictionary<string, JToken> stateDict = jObjectToReturn;
            stateDict["Category"] = item;
            stateList.Add(jObjectToReturn);
        }
        return state;
    }

    public void RestoreFromJToken(JToken state)
    {
        if (state is JArray stateArray)
        {
            IList<JToken> stateList = stateArray;
            foreach (var item in stateList)
            {
                if (item is JObject itemState)
                {
                    string categoryToLoad = "";
                    IDictionary<string, JToken> itemStateDict = itemState;

                    if (itemStateDict["Category"] != null)
                    {                       
                        categoryToLoad = itemStateDict["Category"].ToObject<string>();
                        InternalDatabase.categories.Add(categoryToLoad);
                    }
                }
            }
            InternalDatabase.categories.Sort();
        }
       
    }
}