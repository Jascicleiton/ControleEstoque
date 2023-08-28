using Assets.Scripts.Inventory.Database;
using Assets.Scripts.Inventory.NoPatrimonioNoSerial;
using Assets.Scripts.Misc;
using Assets.Scripts.Mouse;
using Assets.Scripts.ScreenManager;
using Assets.Scripts.Web;
using SimpleJSON;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.UIElements;

namespace Assets.Scripts.Inventory.Movement
{
    public class GetMovementRecords : MonoBehaviour
    {
        [HideInInspector] public List<MovementRecords> RegularItemMovementRecords = new List<MovementRecords>();
        [HideInInspector] public List<NoPaNoSeMovementRecords> NoPaNoSeMovementRecords = new List<NoPaNoSeMovementRecords>();
        
        private VisualTreeAsset _movementObjectTemplate;
        private ListView _listView;
        private TextField _parameterInput;
        private DropdownField _searchOptionsDP;
        private DropdownField _nameDP;
        private Button _returnButton;
        private Button _consultButton;
        bool _isSearchingPatrimonio = true;
        
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
            VisualElement root = GetComponent<UIDocument>().rootVisualElement;
            _movementObjectTemplate = Resources.Load<VisualTreeAsset>("Templates/MovementRecord");
            _listView = root.Q<ListView>("Results");
            _parameterInput = root.Q<TextField>("ParameterTextField");
            _searchOptionsDP = root.Q<DropdownField>("ParameterDP");
            _nameDP = root.Q<DropdownField>("NameDP");
            _returnButton = root.Q<Button>("ReturnButton");
            _consultButton = root.Q<Button>("ConsultButton");
            SubscribeToEvents();
            _listView.makeItem = () => _movementObjectTemplate.Instantiate();
        }

        private void SubscribeToEvents()
        {
            _searchOptionsDP.RegisterCallback<ChangeEvent<string>>(HandleSearchOptionsDP);
            _returnButton.clicked += () => { ReturnToPreviousScreen(); };
            _consultButton.clicked += () => { SearchClicked(); };
        }

        private void UnsubscribeToEvents()
        {
            _searchOptionsDP.UnregisterCallback<ChangeEvent<string>>(HandleSearchOptionsDP);
            _returnButton.clicked -= () => { ReturnToPreviousScreen(); };
            _consultButton.clicked -= () => { SearchClicked(); };
        }

        private void FillNameDP()
        {
            List<string> names = new List<string>();
            foreach (var item in NoPaNoSeImporter.Instance.ItemsList.noPaNoSeItems)
            {
                names.Add(item.ItemName);
            }
            if (names.Count > 0)
            {
                _nameDP.choices = names;
                //nameDP.formatListItemCallback = (element) => element.ToString();
                //nameDP.formatSelectedValueCallback = (element) => element.ToString();
                _nameDP.value = names[0];
                _nameDP.style.display = DisplayStyle.None;
            }
        }

        private void FillSearchOptionsDP()
        {
            List<string> choices = new List<string>() { "Patrimônio", "Nome" };
            _searchOptionsDP.choices = choices;
            //searchOptionsDP.formatListItemCallback = (element) => element.ToString();
            // searchOptionsDP.formatSelectedValueCallback = (element) => element.ToString();
            _searchOptionsDP.value = choices[0];
        }

        /// <summary>
        /// Disables all  movementObjectPrefab from the Pool Manager that were used in the previous search (if it 
        /// actually happened)
        /// </summary>
        private void DeleteOldSearch()
        {
            RegularItemMovementRecords.Clear();
            NoPaNoSeMovementRecords.Clear();
            _listView.Rebuild();
        }

        /// <summary>
        /// Try to get all movements of a specific "Patrimonio" from the Online database
        /// </summary>
        private IEnumerator ImportPatrimonioMovementsRoutine()
        {
            WWWForm movementsForm = CreateForm.GetMovementsForm(ConstStrings.ImportDatabaseKey, _parameterInput.text);

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
                    ConsultDatabase consultDatabase = new ConsultDatabase();
                    foreach (JSONNode item in inventario)
                    {
                        MovementRecords record = new MovementRecords();
                        record.item = consultDatabase.ConsultPatrimonio(item[1], InternalDatabase.Instance.fullDatabase);
                        record.username = item[3];
                        record.date = item[4];
                        record.fromWhere = item[5];
                        record.toWhere = item[6];

                        RegularItemMovementRecords.Add(record);
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

                        NoPaNoSeMovementRecords.Add(record);
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

            if (RegularItemMovementRecords.Count > 0)
            {
                RegularItemMovementRecords.Sort((x, y) => x.date.CompareTo(y.date));
                _listView.bindItem = (element, i) =>
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
                    nameParameter.text = RegularItemMovementRecords[i].item.Patrimonio.ToString();
                    quantityLabel.text = "Serial";
                    quantityParameter.text = RegularItemMovementRecords[i].item.Serial;
                    user.text = RegularItemMovementRecords[i].username;
                    date.text = RegularItemMovementRecords[i].date;
                    whereFrom.text = RegularItemMovementRecords[i].fromWhere.ToString();
                    whereTo.text = RegularItemMovementRecords[i].toWhere.ToString();
                    element.style.height = StyleKeyword.Auto;
                };
                _listView.itemsSource = RegularItemMovementRecords;
            }
            else if (NoPaNoSeMovementRecords.Count > 0)
            {
                NoPaNoSeMovementRecords.Sort((x, y) => x.date.CompareTo(y.date));
                _listView.bindItem = (element, i) =>
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
                    nameParameter.text = NoPaNoSeMovementRecords[i].itemName;
                    quantityLabel.text = "Quantidade";
                    quantityParameter.text = NoPaNoSeMovementRecords[i].quantity.ToString();
                    user.text = NoPaNoSeMovementRecords[i].username;
                    date.text = NoPaNoSeMovementRecords[i].date;
                    whereFrom.text = NoPaNoSeMovementRecords[i].fromWhere.ToString();
                    whereTo.text = NoPaNoSeMovementRecords[i].toWhere.ToString();
                };
                _listView.itemsSource = NoPaNoSeMovementRecords;
            }
            else
            {
                EventHandler.CallIsOneMessageOnlyEvent(true);
                EventHandler.CallOpenMessageEvent("No movement found");
            }
            _listView.fixedItemHeight = 100;
            MouseManager.Instance.SetDefaultCursor();
        }

        /// <summary>
        /// Search for all the movements of the desired item
        /// </summary>
        public void SearchClicked()
        {
            if (_isSearchingPatrimonio)
            {
                if (_parameterInput.text != "")
                {
                    DeleteOldSearch();
                    if (!InternalDatabase.Instance.isOfflineProgram)
                    {
                        StartCoroutine(ImportPatrimonioMovementsRoutine());
                    }
                    else
                    {
                        FindRegularMovementsOffline();
                    }
                }
            }
            else
            {
                DeleteOldSearch();
                if (!InternalDatabase.Instance.isOfflineProgram)
                {
                    StartCoroutine(ImportNoPaNoSeMovementsRoutine(_nameDP.value));
                }
                else
                {
                    FindNoPaNoSeMovementsOffline();
                }
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
                _isSearchingPatrimonio = true;
                _nameDP.style.display = DisplayStyle.None;
                _parameterInput.style.display = DisplayStyle.Flex;
                _parameterInput.value = "";
                EventHandler.CallChangeAnimation("1");
            }
            else if (evt.newValue == "Nome")
            {
                _isSearchingPatrimonio = false;
                _parameterInput.style.display = DisplayStyle.None;
                _nameDP.style.display = DisplayStyle.Flex;
                EventHandler.CallChangeAnimation("2");
            }
        }

        private void FindRegularMovementsOffline()
        {
            List<MovementRecords> allMovements = RegularMovementSaver.Instance.GetAllRegularRecords();
            if (int.TryParse(_parameterInput.text, out int tryParseint))
            {
                foreach (var movementRecord in allMovements)
                {
                    if (movementRecord.item.Patrimonio == tryParseint)
                    {
                        RegularItemMovementRecords.Add(movementRecord);
                    }
                }
                ShowMovements();
            }
            else
            {
                EventHandler.CallIsOneMessageOnlyEvent(true);
                EventHandler.CallOpenMessageEvent("Invalid patrimonio format");
            }

        }

        private void FindNoPaNoSeMovementsOffline()
        {
            List<NoPaNoSeMovementRecords> allMovements = NoPaNoSeMovementSaver.Instance.GetAllNoPaNoSeRecords();
            foreach (var movementRecord in allMovements)
            {
                if (movementRecord.itemName == _nameDP.value)
                {
                    NoPaNoSeMovementRecords.Add(movementRecord);
                }
            }
            ShowMovements();
        }

    }
}