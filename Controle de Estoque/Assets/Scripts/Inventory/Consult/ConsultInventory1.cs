using System.Collections;
using System.Collections.Generic;
using UnityEngine.UIElements;
using UnityEngine;
using System.Linq;
using System.Drawing;

namespace Assets.Scripts.Inventory.Consult
{
    public class ConsultInventory1 : MonoBehaviour
    {
        
        private VisualElement root;
        private DropdownField searchOptionDP; // drop down used to choose search option
        private DropdownField categoryDP; // drop down used to search for an item category
                                          //[SerializeField] TMP_Dropdown locationDP;
        private TextField patrimonioSearchInputField; // field use to type the item "Patrimônio" or the item "Serial"
        private Label numberOfItensFoundText; // show how many itens were found on the search

        private VisualTreeAsset consultResult;
        private ListView listView;
        private List<VisualTreeAsset> results = new List<VisualTreeAsset>();

        private VisualElement categorySearchParametersPanel;
        private List<TextField> categorySearchInputs;
        private List<DropdownField> operators;

        private ConsultCategory consultCategory = null;
        private bool inputEnabled = true;
        private int tempInt = 0; // used for all int.TryParse
        private Sheet foundItems = new Sheet();
        string placeholderClass = TextField.ussClassName + "__placeholder";

        private Button returnButton;
        [SerializeField] SearchCategoryDropDownHandler searchCategoryDPHandler;
       
        /// <summary>
        /// get the ConsultCategory component
        /// </summary>
        private void Start()
        {
            consultCategory = GetComponent<ConsultCategory>();
            if(searchCategoryDPHandler == null)
            {
                searchCategoryDPHandler = GetComponent<SearchCategoryDropDownHandler>();
            }
            foundItems.itens = new List<ItemColumns>();
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
            if (inputEnabled)
            {
                if (patrimonioSearchInputField.style.display == DisplayStyle.Flex)
                {
                    if (Input.GetKeyDown(KeyCode.Return) || Input.GetKeyDown(KeyCode.KeypadEnter))
                    {
                        if (searchOptionDP.value == "Patrimônio")
                        {
                            if (int.TryParse(patrimonioSearchInputField.text, out tempInt))
                            {
                                RemoveOldSearch();
                                if (ConsultDatabase.Instance.ConsultPatrimonio(tempInt, InternalDatabase.Instance.fullDatabase) != null)
                                {
                                    RemoveOldSearch();
                                    foundItems.itens.Add(ConsultDatabase.Instance.ConsultPatrimonio(tempInt, InternalDatabase.Instance.fullDatabase));
                                 
                                    listView.itemsSource = foundItems.itens;
                                    foreach (var item in listView.Children())
                                    {
                                        item.style.height = StyleKeyword.Auto;
                                    }
                                    listView.Rebuild();
                                }
                            }
                        }
                    }
                }
                if (categoryDP.style.display == DisplayStyle.Flex)
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
            root = GetComponent<UIDocument>().rootVisualElement;
            searchOptionDP = root.Q<DropdownField>("SearchOptionsDP");
            categoryDP = root.Q<DropdownField>("CategoryDP");
            patrimonioSearchInputField = root.Q<TextField>("PatrimonioTextField");
            numberOfItensFoundText = root.Q<Label>("NumberOfItemsFound");
            categorySearchParametersPanel = root.Q<VisualElement>("SearchParametersContainer");
            categorySearchInputs = root.Query(name: "SearchParametersContainer").Descendents<TextField>().ToList();
            operators = root.Query(name: "SearchParametersContainer").Descendents<DropdownField>().ToList();
            listView = root.Q<ListView>();
            returnButton = root.Q<Button>("ReturnButton");
            consultResult = Resources.Load<VisualTreeAsset>("Templates/ResultPanel");
            
            FillListView();
        }

        private void RegisterToEvents()
        {
            searchOptionDP.RegisterCallback<ChangeEvent<string>>(HandleSearchOptionDP);
            returnButton.clicked += () => { ReturnToPreviousScreen(); };
            EventHandler.EnableInput += SetInputEnabled;
            FillDropdowns();
        }

        private void UnregisterToEvents()
        {
            EventHandler.EnableInput -= SetInputEnabled;
        }

      
        /// <summary>
        /// Enables or disables input. Called by Event EnableInput
        /// </summary>
        private void SetInputEnabled(bool enableInput)
        {
            inputEnabled = enableInput;
        }

        /// <summary>
        /// Hides ItensFoundText and disables all instances of ConsultResult
        /// </summary>
        private void RemoveOldSearch()
        {
            SetItensFoundText(false);
            foundItems.itens.Clear();
            listView.Rebuild();
        }

        /// <summary>
        /// If true, sets the text invisible, if false set it to be visible. For consults using either "Patrimônio" or 
        /// "Serial", also set the text of the ItensFound text box if it is to be visible
        /// </summary>
        private void SetItensFoundText(bool isInvisible)
        {
            if (isInvisible)
            {
                numberOfItensFoundText.style.visibility = Visibility.Hidden;
            }
            else
            {
                numberOfItensFoundText.style.visibility = Visibility.Visible;
                switch (searchOptionDP.value)
                {
                    case "Patrimônio":
                        if (int.TryParse(patrimonioSearchInputField.text, out tempInt))
                        {
                            if (ConsultDatabase.Instance.ConsultPatrimonio(tempInt, InternalDatabase.Instance.fullDatabase) == null)
                            {
                                numberOfItensFoundText.text = "Patrimônio não encontrado";
                            }
                            else
                            {
                                numberOfItensFoundText.text = "1 item encontrado";
                            }
                        }
                        break;
                    case "Serial":
                        if (ConsultDatabase.Instance.ConsultSerial(patrimonioSearchInputField.text, InternalDatabase.Instance.fullDatabase) == null)
                        {
                            numberOfItensFoundText.text = "Serial não encontrado";
                        }
                        else
                        {
                            numberOfItensFoundText.text = "1 item encontrado";
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

            for (int i = 0; i < categorySearchInputs.Count; i++)
            {
                if (categorySearchInputs[i].style.display == DisplayStyle.Flex)
                {
                   if (!categorySearchInputs[i].ClassListContains(placeholderClass))
                    {
                        activeIndexes.Add(i);
                        activeOperators.Add(GetOperatorFromDP(i));
                    }
                }
            }

            if (activeIndexes.Count > 0)
            {
                foundItems = consultCategory.FindItens(activeIndexes, categorySearchInputs.ToArray(), HelperMethods.GetCategoryDatabaseToConsult(categoryDP.value), activeOperators);
            }
           
            if (foundItems != null)
            {
                if (foundItems.itens.Count > 0)
                {
                    for (int i = 0; i < foundItems.itens.Count; i++)
                    {
                        if (InternalDatabase.Instance.currentEstoque != CurrentEstoque.ESF)
                        {
                            if (foundItems.itens[i].Status == "DEFEITO")
                            {
                                foundItems.itens.RemoveAt(i);
                            }
                        }
                    }
                    listView.itemsSource = foundItems.itens;
                    foreach (var item in listView.Children())
                    {
                        item.style.height = StyleKeyword.Auto;
                    }
                    listView.Rebuild();
                    numberOfItensFoundText.style.visibility = Visibility.Visible;
                    numberOfItensFoundText.text = foundItems.itens.Count.ToString() + " itens encontrados";
                }
                else
                {
                    numberOfItensFoundText.style.visibility = Visibility.Visible;
                    numberOfItensFoundText.text = foundItems.itens.Count.ToString() + " itens encontrados";
                }
            }
            else
            {
                numberOfItensFoundText.style.visibility = Visibility.Visible;
                numberOfItensFoundText.text = "Nenhum item encontrado";
            }
        }

        /// <summary>
        /// Get the string operator from the array of all operators to determine how the search for the parameter(s)
        /// should be
        /// </summary>
        private string GetOperatorFromDP(int index)
        {
            print(operators[index].value);
            return operators[index].value;
        }
 
        /// <summary>
        /// Handles what happens when the "procurar por" dropdown changes the value
        /// </summary>
        private void HandleSearchOptionDP(ChangeEvent<string> evt)
        {
            switch (evt.newValue)
            {
                case "Categoria":
                    categoryDP.style.display = DisplayStyle.Flex;
                    categorySearchParametersPanel.style.display = DisplayStyle.Flex;
                    categoryDP.value = InternalDatabase.categories[0];
                    patrimonioSearchInputField.style.display = DisplayStyle.None;
                    EventHandler.CallUpdateConsultInputs();
                    break;
                case "Patrimônio":
                    categoryDP.style.display = DisplayStyle.None;
                    categorySearchParametersPanel.style.display = DisplayStyle.None;
                    patrimonioSearchInputField.style.display = DisplayStyle.Flex;
                    //    searchParameterInputField.placeholder.GetComponent<TextMeshProUGUI>().text = "Patrimônio";
                    numberOfItensFoundText.style.visibility = Visibility.Hidden;

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
            listView.makeItem = () => consultResult.Instantiate();
            listView.bindItem = (element, i) =>
            {
                VisualElement[] itemBoxes = element.Q<VisualElement>("Results").Children().ToArray();
                List<Label> parameterNames = element.Query(name: "Results").Descendents<Label>(name: "ParameterName").ToList();
                List<Label> parameterValues = element.Query(name: "Results").Descendents<Label>(name: "ParameterValue").ToList();
                Label patrimonioLabel = element.Q<Label>("PatrimonioLabel");

                ShowResult(foundItems.itens[i], itemBoxes.ToList(), parameterNames, parameterValues, patrimonioLabel);
               // element.style.minHeight = 170;
             //   element.style.maxHeight = 500;
                element.style.height = StyleKeyword.Auto;
            };
            listView.fixedItemHeight = 100;
            listView.itemsSource = foundItems.itens;
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
            searchOptionDP.choices = searchOptions;
            searchOptionDP.value = searchOptions[0];
            searchOptionDP.formatListItemCallback = (element) => element.ToString();

            List<string> operatorsValues = new List<string>() { "=", "≠" };
            foreach (var item in operators)
            {
                item.choices = operatorsValues;
                item.value = operatorsValues[0];
                item.formatListItemCallback = (element) => element.ToString();
            }
        }
    }
}