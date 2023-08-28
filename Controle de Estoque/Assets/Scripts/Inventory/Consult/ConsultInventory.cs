using System.Collections;
using System.Collections.Generic;
using UnityEngine.UIElements;
using UnityEngine;
using System.Linq;
using Assets.Scripts.Inventory.Database;
using Assets.Scripts.Misc;
using Assets.Scripts.ScreenManager;

namespace Assets.Scripts.Inventory.Consult
{
    public class ConsultInventory : MonoBehaviour
    {
        
        private DropdownField _searchOptionDP; // drop down used to choose search option
        private DropdownField _categoryDP; // drop down used to search for an item category
                                          //[SerializeField] TMP_Dropdown locationDP;
        private TextField _patrimonioSearchInputField; // field use to type the item "Patrimônio" or the item "Serial"
        private Label _numberOfItensFoundText; // show how many itens were found on the search

        private VisualTreeAsset _consultResult;
        private ListView _listView;
        private List<VisualTreeAsset> _results = new List<VisualTreeAsset>();

        private VisualElement _categorySearchParametersPanel;
        private List<TextField> _categorySearchInputs;
        private List<DropdownField> _operators;

        private ConsultCategory _consultCategory = null;
        private ConsultDatabase _consultDatabase = null;

        private bool _inputEnabled = true;
        private int _tempInt = 0; // used for all int.TryParse
        private Sheet _foundItems = new Sheet();
        private string _placeholderClass = TextField.ussClassName + "__placeholder";

        private Button _returnButton;
        [SerializeField] private SearchCategoryDropDownHandler _searchCategoryDPHandler;
       
        /// <summary>
        /// get the ConsultCategory component
        /// </summary>
        private void Start()
        {
            _consultCategory = new ConsultCategory();
            _consultDatabase = new ConsultDatabase();
            if(_searchCategoryDPHandler == null)
            {
                _searchCategoryDPHandler = GetComponent<SearchCategoryDropDownHandler>();
            }
            _foundItems.itens = new List<ItemColumns>();
        }

        private void OnEnable()
        {
            GetUIElementsReferences();
            RegisterToEvents();
        }

        private void OnDisable()
        {
            UnregisterToEvents();
        }

        /// <summary>
        /// Handles what happens if Enter is pressed
        /// </summary>
        private void Update()
        {
            if (_inputEnabled)
            {
                if (_patrimonioSearchInputField.style.display == DisplayStyle.Flex)
                {
                    if (Input.GetKeyDown(KeyCode.Return) || Input.GetKeyDown(KeyCode.KeypadEnter))
                    {
                        if (_searchOptionDP.value == "Patrimônio")
                        {
                            if (int.TryParse(_patrimonioSearchInputField.text, out _tempInt))
                            {
                                RemoveOldSearch();
                                if (_consultDatabase.ConsultPatrimonio(_tempInt, InternalDatabase.Instance.fullDatabase) != null)
                                {
                                    RemoveOldSearch();
                                    _foundItems.itens.Add(_consultDatabase.ConsultPatrimonio(_tempInt, InternalDatabase.Instance.fullDatabase));
                                 
                                    _listView.itemsSource = _foundItems.itens;
                                    foreach (var item in _listView.Children())
                                    {
                                        item.style.height = StyleKeyword.Auto;
                                    }
                                    _listView.Rebuild();
                                }
                            }
                            else
                            {
                                EventHandler.CallIsOneMessageOnlyEvent(true);
                                EventHandler.CallOpenMessageEvent("Invalid patrimonio format");
                            }
                        }
                    }
                }
                if (_categoryDP.style.display == DisplayStyle.Flex)
                {
                    if (Input.GetKeyDown(KeyCode.Return) || Input.GetKeyDown(KeyCode.KeypadEnter))
                    {
                        ConsultWithCategory();
                    }
                }
            }
        }

        private void GetUIElementsReferences()
        {
            VisualElement root = GetComponent<UIDocument>().rootVisualElement;
            _searchOptionDP = root.Q<DropdownField>("SearchOptionsDP");
            _categoryDP = root.Q<DropdownField>("CategoryDP");
            _patrimonioSearchInputField = root.Q<TextField>("PatrimonioTextField");
            _numberOfItensFoundText = root.Q<Label>("NumberOfItemsFound");
            _categorySearchParametersPanel = root.Q<VisualElement>("SearchParametersContainer");
            _categorySearchInputs = root.Query(name: "SearchParametersContainer").Descendents<TextField>().ToList();
            _operators = root.Query(name: "SearchParametersContainer").Descendents<DropdownField>().ToList();
            _listView = root.Q<ListView>();
            _returnButton = root.Q<Button>("ReturnButton");
            _consultResult = Resources.Load<VisualTreeAsset>("Templates/ResultPanel");
            
            FillListView();
        }

        private void RegisterToEvents()
        {
            _searchOptionDP.RegisterCallback<ChangeEvent<string>>(HandleSearchOptionDP);
            _returnButton.clicked += () => { ReturnToPreviousScreen(); };
            EventHandler.EnableInput += SetInputEnabled;
            FillDropdowns();
        }

        private void UnregisterToEvents()
        {
            _searchOptionDP.UnregisterCallback<ChangeEvent<string>>(HandleSearchOptionDP);
            _returnButton.clicked -= () => {  ReturnToPreviousScreen(); };
            EventHandler.EnableInput -= SetInputEnabled;
        }

      
        /// <summary>
        /// Enables or disables input. Called by Event EnableInput
        /// </summary>
        private void SetInputEnabled(bool enableInput)
        {
            _inputEnabled = enableInput;
        }

        /// <summary>
        /// Hides ItensFoundText and disables all instances of ConsultResult
        /// </summary>
        private void RemoveOldSearch()
        {
            SetItensFoundText(false);
            _foundItems.itens.Clear();
            _listView.Rebuild();
        }

        /// <summary>
        /// If true, sets the text invisible, if false set it to be visible. For consults using either "Patrimônio" or 
        /// "Serial", also set the text of the ItensFound text box if it is to be visible
        /// </summary>
        private void SetItensFoundText(bool isInvisible)
        {
            if (isInvisible)
            {
                _numberOfItensFoundText.style.visibility = Visibility.Hidden;
            }
            else
            {
                _numberOfItensFoundText.style.visibility = Visibility.Visible;
                switch (_searchOptionDP.value)
                {
                    case "Patrimônio":
                        if (int.TryParse(_patrimonioSearchInputField.text, out _tempInt))
                        {
                            if (_consultDatabase.ConsultPatrimonio(_tempInt, InternalDatabase.Instance.fullDatabase) == null)
                            {
                                _numberOfItensFoundText.text = "Patrimônio não encontrado";
                            }
                            else
                            {
                                _numberOfItensFoundText.text = "1 item encontrado";
                            }
                        }
                        break;
                    case "Serial":
                        if (_consultDatabase.ConsultSerial(_patrimonioSearchInputField.text, InternalDatabase.Instance.fullDatabase) == null)
                        {
                            _numberOfItensFoundText.text = "Serial não encontrado";
                        }
                        else
                        {
                            _numberOfItensFoundText.text = "1 item encontrado";
                        }
                        break;
                    default:
                        break;
                }
            }
        }
      
        /// <summary>
        /// Consult the inventory using the parameters chosen from each category
        /// </summary>
        private void ConsultWithCategory()
        {
            RemoveOldSearch();
            List<int> activeIndexes = new List<int>();
            List<string> activeOperators = new List<string>();

            for (int i = 0; i < _categorySearchInputs.Count; i++)
            {
                if (_categorySearchInputs[i].style.display == DisplayStyle.Flex)
                {
                   if (!_categorySearchInputs[i].ClassListContains(_placeholderClass))
                    {
                        activeIndexes.Add(i);
                        activeOperators.Add(GetOperatorFromDP(i));
                    }
                }
            }

            if (activeIndexes.Count > 0)
            {
                _foundItems = _consultCategory.FindItens(activeIndexes, _categorySearchInputs.ToArray(), HelperMethods.GetCategoryDatabaseToConsult(_categoryDP.value), activeOperators);
            }
           
            if (_foundItems != null)
            {
                if (_foundItems.itens.Count > 0)
                {
                    for (int i = 0; i < _foundItems.itens.Count; i++)
                    {
                        if (InternalDatabase.Instance.currentEstoque != CurrentEstoque.ESF)
                        {
                            if (_foundItems.itens[i].Status == "DEFEITO")
                            {
                                _foundItems.itens.RemoveAt(i);
                            }
                        }
                    }
                    _listView.itemsSource = _foundItems.itens;
                    foreach (var item in _listView.Children())
                    {
                        item.style.height = StyleKeyword.Auto;
                    }
                    _listView.Rebuild();
                    _numberOfItensFoundText.style.visibility = Visibility.Visible;
                    _numberOfItensFoundText.text = _foundItems.itens.Count.ToString() + " itens encontrados";
                }
                else
                {
                    _numberOfItensFoundText.style.visibility = Visibility.Visible;
                    _numberOfItensFoundText.text = _foundItems.itens.Count.ToString() + " itens encontrados";
                }
            }
            else
            {
                _numberOfItensFoundText.style.visibility = Visibility.Visible;
                _numberOfItensFoundText.text = "Nenhum item encontrado";
            }
        }

        /// <summary>
        /// Get the string operator from the array of all operators to determine how the search for the parameter(s)
        /// should be
        /// </summary>
        private string GetOperatorFromDP(int index)
        {
            print(_operators[index].value);
            return _operators[index].value;
        }
 
        /// <summary>
        /// Handles what happens when the "procurar por" dropdown changes the value
        /// </summary>
        private void HandleSearchOptionDP(ChangeEvent<string> evt)
        {
            switch (evt.newValue)
            {
                case "Categoria":
                    _categoryDP.style.display = DisplayStyle.Flex;
                    _categorySearchParametersPanel.style.display = DisplayStyle.Flex;
                    _categoryDP.value = InternalDatabase.categories[0];
                    _patrimonioSearchInputField.style.display = DisplayStyle.None;
                    EventHandler.CallUpdateConsultInputs();
                    break;
                case "Patrimônio":
                    _categoryDP.style.display = DisplayStyle.None;
                    _categorySearchParametersPanel.style.display = DisplayStyle.None;
                    _patrimonioSearchInputField.style.display = DisplayStyle.Flex;
                    //    searchParameterInputField.placeholder.GetComponent<TextMeshProUGUI>().text = "Patrimônio";
                    _numberOfItensFoundText.style.visibility = Visibility.Hidden;

                    break;
               default:
                    break;
            }
        }

        /// <summary>
        /// Goes to InitialScene
        /// </summary>
        private void ReturnToPreviousScreen()
        {
            ChangeScreenManager.Instance.OpenScene(Scenes.ConsultScene, Scenes.InitialScene);
        }

        private void FillListView()
        {
            _listView.makeItem = () => _consultResult.Instantiate();
            _listView.bindItem = (element, i) =>
            {
                VisualElement[] itemBoxes = element.Q<VisualElement>("Results").Children().ToArray();
                List<Label> parameterNames = element.Query(name: "Results").Descendents<Label>(name: "ParameterName").ToList();
                List<Label> parameterValues = element.Query(name: "Results").Descendents<Label>(name: "ParameterValue").ToList();
                Label patrimonioLabel = element.Q<Label>("PatrimonioLabel");

                ShowResult(_foundItems.itens[i], itemBoxes.ToList(), parameterNames, parameterValues, patrimonioLabel);
               // element.style.minHeight = 170;
             //   element.style.maxHeight = 500;
                element.style.height = StyleKeyword.Auto;
            };
            _listView.fixedItemHeight = 100;
            _listView.itemsSource = _foundItems.itens;
        }            

        /// <summary>
        /// Used to show the result of consulting the database"
        /// </summary>
        public void ShowResult(ItemColumns itemToShow, List<VisualElement> itemBoxes, List<Label> parameterNames, List<Label> parameterValues, Label patrimonioLabel)
        {
            patrimonioLabel.text = itemToShow.Patrimonio.ToString();
            ActivateAllItemBoxes(itemBoxes);
            if (itemToShow != null)
            {
                Dictionary<string, List<string>> dictionary = new Dictionary<string, List<string>>();
                dictionary = HelperMethods.GetParameterValuesNamesPlaceholders(itemToShow, itemToShow.Categoria);
                List<string> names = new List<string>();
                List<string> values = new List<string>();
                dictionary.TryGetValue("Names", out names);
                dictionary.TryGetValue("Values", out values);
                FillNames(names, parameterNames);
                FillValues(values, parameterValues);
            }
            else
            {
                print("Item to show is null");
            }
            HideEmptyItemBox(itemBoxes, parameterNames);
        }

        /// <summary>
        /// Activate all item boxes
        /// </summary>
        private void ActivateAllItemBoxes(List<VisualElement> itemBoxes)
        {
            for (int i = 0; i < itemBoxes.Count; i++)
            {
                itemBoxes[i].style.display = DisplayStyle.Flex;
            }
        }

        /// <summary>
        /// Fill the names of all item boxes that should get a name
        /// </summary>
        private void FillNames(List<string> names, List<Label> parameterNames)
        {
            for (int i = 0; i < parameterNames.Count; i++)
            {
                if (i < names.Count)
                {
                    parameterNames[i].text = names[i];
                }
                else
                {
                    break;
                }
            }
        }

        /// <summary>
        /// Fill the values of all item boxes that should get a value. Used when the item box have an inputField object
        /// </summary>
        private void FillValues(List<string> values, List<Label> parameterValues)
        {
            for (int i = 0; i < parameterValues.Count; i++)
            {
                if (i < values.Count)
                {
                    parameterValues[i].text = values[i];
                }
                else
                {
                    break;
                }
            }
        }

        /// <summary>
        /// Hide all item boxes that have a empty name
        /// </summary>
        private void HideEmptyItemBox(List<VisualElement> itemBoxes, List<Label> parameterNames)
        {
            for (int i = 0; i < parameterNames.Count; i++)
            {
                if (parameterNames[i] != null && parameterNames[i].text == "")
                {
                    itemBoxes[i].style.display = DisplayStyle.None;
                }
            }
        }

        private void FillDropdowns()
        {
            List<string> searchOptions = new List<string>(){ "Patrimônio", "Categoria"};
            _searchOptionDP.choices = searchOptions;
            _searchOptionDP.value = searchOptions[0];
            _searchOptionDP.formatListItemCallback = (element) => element.ToString();

            List<string> operatorsValues = new List<string>() { "=", "≠" };
            foreach (var item in _operators)
            {
                item.choices = operatorsValues;
                item.value = operatorsValues[0];
                item.formatListItemCallback = (element) => element.ToString();
            }
        }
    }
}