using SimpleJSON;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.UIElements;


public class GetMovementRecords : MonoBehaviour
{
    [HideInInspector] public List<MovementRecords> regularItemMovementRecords = new List<MovementRecords>();
    [HideInInspector] public List<NoPaNoSeMovementRecords> noPaNoSeMovementRecords = new List<NoPaNoSeMovementRecords>();
    private VisualElement root;
    private VisualTreeAsset movementObjectTemplate;
    private ListView listView;
    private TextField parameterInput;
    private DropdownField searchOptionsDP;
    private DropdownField nameDP;
    private Button returnButton;
    private Button consultButton;
    bool isSearchingPatrimonio = true;

    private void OnEnable()
    {
        GetUIReferences();
        StartCoroutine(WaitATick());        
    }

    private void OnDisable()
    {
        UnsubscribeToEvents();   
    }

    private IEnumerator WaitATick()
    {
        yield return new WaitForSeconds(0.2f);
        FillNameDP();
        FillSearchOptionsDP();
        
    }

    private void GetUIReferences()
    {
        root = GetComponent<UIDocument>().rootVisualElement;
        movementObjectTemplate = Resources.Load<VisualTreeAsset>("Templates/MovementRecord");
        listView = root.Q<ListView>();
        parameterInput = root.Q<TextField>("ParameterTextField");
        searchOptionsDP = root.Q<DropdownField>("ParameterDP");
        nameDP = root.Q<DropdownField>("NameDP");
        returnButton = root.Q<Button>("ReturnButton");
        consultButton = root.Q<Button>("ConsultButton");
        SubscribeToEvents();      
    }

    private void SubscribeToEvents()
    {
        searchOptionsDP.RegisterCallback<ChangeEvent<string>>(HandleSearchOptionsDP);
               returnButton.clicked += () => { ReturnToPreviousScreen(); };
        consultButton.clicked += () => { SearchClicked(); };
    } 

    private void UnsubscribeToEvents()
    {
        searchOptionsDP.UnregisterCallback<ChangeEvent<string>>(HandleSearchOptionsDP);
        returnButton.clicked -= () => { ReturnToPreviousScreen(); };
        consultButton.clicked -= () => { SearchClicked(); };
    }

    private void FillNameDP()
    {
        List<string> names = new List<string>();
        foreach (var item in NoPaNoSeImporter.Instance.itemsList.noPaNoSeItems)
        {
            names.Add(item.ItemName);
        }
        nameDP.choices = names;
        //nameDP.formatListItemCallback = (element) => element.ToString();
        //nameDP.formatSelectedValueCallback = (element) => element.ToString();
        nameDP.style.display = DisplayStyle.None;
    }

    private void FillSearchOptionsDP()
    {
        List<string> choices = new List<string>() { "Patrimônio", "Nome"};
        searchOptionsDP.choices = choices;
        //searchOptionsDP.formatListItemCallback = (element) => element.ToString();
       // searchOptionsDP.formatSelectedValueCallback = (element) => element.ToString();
        searchOptionsDP.value = choices[0];
    }

    /// <summary>
    /// Disables all  movementObjectPrefab from the Pool Manager that were used in the previous search (if it 
    /// actually happened)
    /// </summary>
    private void DeleteOldSearch()
    {
        if (listView.childCount > 0)
        {
            for (int i = 0; i < listView.childCount; i++)
            {
                listView.RemoveAt(i);
            }
        }
        regularItemMovementRecords.Clear();
        noPaNoSeMovementRecords.Clear();
    }

    /// <summary>
    /// Try to get all movements of a specific "Patrimonio" from the Online database
    /// </summary>
    private IEnumerator ImportPatrimonioMovementsRoutine()
    {
        WWWForm movementsForm = CreateForm.GetMovementsForm(ConstStrings.ImportDatabaseKey, parameterInput.text);

        UnityWebRequest createMovementRequest = CreatePostRequest.GetPostRequest(movementsForm, ConstStrings.GetPatrimonioMovements, 3);

        MouseManager.Instance.SetWaitingCursor();
        yield return createMovementRequest.SendWebRequest();

        if (createMovementRequest.result == UnityWebRequest.Result.ConnectionError)
        {
            Debug.LogWarning("StartListRoutine: conectionerror");
        }
        else if (createMovementRequest.result == UnityWebRequest.Result.DataProcessingError)
        {
            Debug.LogWarning("StartListRoutine: data processing error");
        }
        else if (createMovementRequest.result == UnityWebRequest.Result.ProtocolError)
        {
            Debug.LogWarning("StartListRoutine: protocol error");
        }

        if (createMovementRequest.error == null)
        {
            string response = createMovementRequest.downloadHandler.text;
            if (response == "Conection error" || response == "Query failed")
            {
                Debug.LogWarning("StartListRoutine: Server error");
            }
            else if (response == "Wrong app key")
            {
                Debug.LogWarning("StartListRoutine: app key");
            }
            else if (response == "Result came empty")
            {
                EventHandler.CallIsOneMessageOnlyEvent(true);
                EventHandler.CallOpenMessageEvent("No movement found");
            }
            else
            {
                JSONNode inventario = JSON.Parse(createMovementRequest.downloadHandler.text);
                foreach (JSONNode item in inventario)
                {
                    MovementRecords record = new MovementRecords();
                    record.item = ConsultDatabase.Instance.ConsultPatrimonio(item[1], InternalDatabase.Instance.fullDatabase);
                    record.username = item[3];
                    record.date = item[4];
                    record.fromWhere = item[5];
                    record.toWhere = item[6];

                    regularItemMovementRecords.Add(record);
                }
               ShowMovements();
            }
        }
        else
        {
            Debug.LogWarning("StartListRoutine: " + createMovementRequest.error);
            // TODO: send message to user with error and recomendation
        }
        createMovementRequest.Dispose();
        MouseManager.Instance.SetDefaultCursor();
    }

    /// <summary>
    /// Try to get all movements of a specific "NoPaNoSe" item from the Online database
    /// </summary>
    private IEnumerator ImportNoPaNoSeMovementsRoutine(string itemName)
    {
        WWWForm movementsForm = CreateForm.GetMovementsForm(ConstStrings.ImportDatabaseKey, itemName);

        UnityWebRequest createMovementRequest = CreatePostRequest.GetPostRequest(movementsForm, ConstStrings.GetNoPaNoSeMovements, 3);

        MouseManager.Instance.SetWaitingCursor();
        yield return createMovementRequest.SendWebRequest();

        if (createMovementRequest.result == UnityWebRequest.Result.ConnectionError)
        {
            Debug.LogWarning("StartListRoutine: conectionerror");
        }
        else if (createMovementRequest.result == UnityWebRequest.Result.DataProcessingError)
        {
            Debug.LogWarning("StartListRoutine: data processing error");
        }
        else if (createMovementRequest.result == UnityWebRequest.Result.ProtocolError)
        {
            Debug.LogWarning("StartListRoutine: protocol error");
        }

        if (createMovementRequest.error == null)
        {
            string response = createMovementRequest.downloadHandler.text;
            if (response == "Conection error" || response == "Query failed")
            {
                Debug.LogWarning("StartListRoutine: Server error");
            }
            else if (response == "Wrong app key")
            {
                Debug.LogWarning("StartListRoutine: app key");
            }
            else if (response == "Result came empty")
            {
                EventHandler.CallIsOneMessageOnlyEvent(true);
                EventHandler.CallOpenMessageEvent("No movement found");
            }
            else
            {
                JSONNode inventario = JSON.Parse(createMovementRequest.downloadHandler.text);
                foreach (JSONNode item in inventario)
                {
                    NoPaNoSeMovementRecords record = new NoPaNoSeMovementRecords();
                    record.itemName = item[1];
                    record.quantity = item[2];
                    record.username = item[3];
                    record.date = item[4];
                    record.fromWhere = item[5];
                    record.toWhere = item[6];

                    noPaNoSeMovementRecords.Add(record);
                }
                ShowMovements();
            }
        }
        else
        {
            Debug.LogWarning("StartListRoutine: " + createMovementRequest.error);
            // TODO: send message to user with error and recomendation
        }
        createMovementRequest.Dispose();
        MouseManager.Instance.SetDefaultCursor();
    }

    /// <summary>
    /// Show all the movements that were found
    /// </summary>
    private void ShowMovements()
    {
        MouseManager.Instance.SetWaitingCursor();
        if (regularItemMovementRecords.Count > 0)
        {
            regularItemMovementRecords.Sort((x, y) => x.date.CompareTo(y.date));

            for (int i = 0; i < regularItemMovementRecords.Count; i++)
            {
                listView.makeItem = () => movementObjectTemplate.Instantiate();
                listView.bindItem = (element, j) =>
                {
                    Label nameLabel = element.Q<Label>("NameLabel");
                    Label nameParameter = element.Q<Label>("NameParameter");
                    Label quantityLabel = element.Q<Label>("QuantityLabel");
                    Label quantityParameter = element.Q<Label>("QuantityParameter");
                    Label user = element.Q<Label>("User");
                    Label date = element.Q<Label>("Date");
                    Label whereFrom = element.Q<Label>("WhereFrom");
                    Label whereTo = element.Q<Label>("WhereTo");

                    nameLabel.text = "Patrimônio";
                    nameParameter.text = regularItemMovementRecords[i].item.Patrimonio.ToString();
                    quantityLabel.text = "Serial";
                    quantityParameter.text = regularItemMovementRecords[i].item.Serial;
                    user.text = regularItemMovementRecords[i].username;
                    date.text = regularItemMovementRecords[i].date;
                    whereFrom.text = regularItemMovementRecords[i].fromWhere.ToString();
                    whereTo.text = regularItemMovementRecords[i].toWhere.ToString();
                };
            }
            return;
        }
        if (noPaNoSeMovementRecords.Count > 0)
        {
            noPaNoSeMovementRecords.Sort((x, y) => x.date.CompareTo(y.date));

            for (int i = 0; i < noPaNoSeMovementRecords.Count; i++)
            {
                listView.makeItem = () => movementObjectTemplate.Instantiate();
                listView.bindItem = (element, j) =>
                {
                    Label nameLabel = element.Q<Label>("NameLabel");
                    Label nameParameter = element.Q<Label>("NameParameter");
                    Label quantityLabel = element.Q<Label>("QuantityLabel");
                    Label quantityParameter = element.Q<Label>("QuantityParameter");
                    Label user = element.Q<Label>("User");
                    Label date = element.Q<Label>("Date");
                    Label whereFrom = element.Q<Label>("WhereFrom");
                    Label whereTo = element.Q<Label>("WhereTo");

                    nameLabel.text = "Nome";
                    nameParameter.text = noPaNoSeMovementRecords[i].itemName;
                    quantityLabel.text = "Quantidade";
                    quantityParameter.text = noPaNoSeMovementRecords[i].quantity.ToString();
                    user.text = noPaNoSeMovementRecords[i].username;
                    date.text = noPaNoSeMovementRecords[i].date;
                    whereFrom.text = noPaNoSeMovementRecords[i].fromWhere.ToString();
                    whereTo.text = noPaNoSeMovementRecords[i].toWhere.ToString();
                };
            }
            MouseManager.Instance.SetDefaultCursor();            
            return;
        }
    }

    /// <summary>
    /// Search for all the movements of the desired item
    /// </summary>
    public void SearchClicked()
    {
        if(isSearchingPatrimonio)
        { 
            if (parameterInput.text != "")
            {
                DeleteOldSearch();                              
                    StartCoroutine(ImportPatrimonioMovementsRoutine());                             
            }
        }
        else 
        {            
            StartCoroutine(ImportNoPaNoSeMovementsRoutine(nameDP.value));
        }
    }

    /// <summary>
    /// Goes to InitialScene
    /// </summary>
    public void ReturnToPreviousScreen()
    {
        ChangeScreenManager.Instance.OpenScene(Scenes.MovementRecordsScene, Scenes.InitialScene);
    }

    private void HandleSearchOptionsDP(ChangeEvent<string> evt)
    {
        if (evt.newValue == "Patrimônio")
        {
            isSearchingPatrimonio = true;
            nameDP.style.display = DisplayStyle.None;
            parameterInput.style.display = DisplayStyle.Flex;
            parameterInput.value = "";
        }
        else if (evt.newValue == "Nome")
        {
            isSearchingPatrimonio = false;
            parameterInput.style.display = DisplayStyle.None;
            nameDP.style.display = DisplayStyle.Flex;
        }
    }
}
