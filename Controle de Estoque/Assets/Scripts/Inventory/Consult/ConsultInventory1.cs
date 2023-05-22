using System.Collections;
using System.Collections.Generic;
using UnityEngine.UIElements;
using UnityEngine;
using System.Linq;

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

        private Button returnButton;
       
        /// <summary>
        /// get the ConsultCategory component
        /// </summary>
        private void Start()
        {
            consultCategory = GetComponent<ConsultCategory>();
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
                                if (ConsultDatabase.Instance.ConsultPatrimonio(tempInt, InternalDatabase.Instance.fullDatabase) != null)
                                {
                                    RemoveOldSearch();
                                  //  GameObject result = Instantiate(consultResult, consultResultTransform);
                                  //  result.GetComponent<ConsultResult>().ShowResult(ConsultDatabase.Instance.ConsultPatrimonio(tempInt, InternalDatabase.Instance.fullDatabase), 0);
                                }
                                else
                                {
                                    RemoveOldSearch();
                                }
                            }
                        }
                        else if (searchOptionDP.value == "Serial")
                        {
                            if (ConsultDatabase.Instance.ConsultSerial(patrimonioSearchInputField.text, InternalDatabase.Instance.fullDatabase) != null)
                            {
                                RemoveOldSearch();
                                //GameObject result = Instantiate(consultResult, consultResultTransform);
                                //result.GetComponent<ConsultResult>().ShowResult(ConsultDatabase.Instance.ConsultSerial(searchParameterInputField.text, InternalDatabase.Instance.fullDatabase), 1);
                            }
                            else
                            {
                                RemoveOldSearch();
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
            foreach (var item in categorySearchParametersPanel.Children())
            {
                categorySearchInputs.Add(item as TextField);
            }
            operators = root.Query(name: "SearchParametersContainer").Descendents<DropdownField>().ToList();
            listView = root.Q<ListView>();
            returnButton = root.Q<Button>("ReturnButton");
        }

        private void RegisterToEvents()
        {
            searchOptionDP.RegisterCallback<ChangeEvent<string>>(HandleSearchOptionDP);
            returnButton.clicked += () => { ReturnToPreviousScreen(); };
            EventHandler.EnableInput += SetInputEnabled;
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
            if (listView.childCount > 0)
            {
                foreach (var item in listView.Children())
                {
                    item.style.display = DisplayStyle.None;
                }
                    
                    //consultResultTransform.GetChild(i).gameObject.SetActive(false);
               
            }
            for (int i = 0; i < categorySearchInputs.Count; i++)
            {
                if (categorySearchInputs[i].style.display == DisplayStyle.Flex)
                {
                    categorySearchInputs[i].value = "";
                }
            }
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
            Sheet foundItens = new Sheet();
            List<int> activeIndexes = new List<int>();
            List<string> activeOperators = new List<string>();

            for (int i = 0; i < categorySearchInputs.Count; i++)
            {
                if (categorySearchInputs[i].style.display == DisplayStyle.Flex)
                {
                    if (categorySearchInputs[i].text != "")
                    {
                        activeIndexes.Add(i);
                        activeOperators.Add(GetOperatorFromDP(i));
                    }
                }
            }

            if (activeIndexes.Count > 0)
            {
                //print(activeIndexes.Count);          
                foundItens = consultCategory.FindItens(activeIndexes, categorySearchInputs.ToArray(), HelperMethods.GetCategoryDatabaseToConsult(categoryDP.value), activeOperators);
            }
            RemoveOldSearch();
            if (foundItens != null)
            {
                if (foundItens.itens.Count > 0)
                {
                    for (int i = 0; i < foundItens.itens.Count; i++)
                    {
                        if (InternalDatabase.Instance.currentEstoque != CurrentEstoque.ESF)
                        {
                            if (foundItens.itens[i].Status == "DEFEITO")
                            {
                                foundItens.itens.RemoveAt(i);
                            }
                        }
                    }
                    FillListView(foundItens);
                    numberOfItensFoundText.style.visibility = Visibility.Visible;
                    numberOfItensFoundText.text = foundItens.itens.Count.ToString() + " itens encontrados";
                }
                else
                {
                    numberOfItensFoundText.style.visibility = Visibility.Visible;
                    numberOfItensFoundText.text = foundItens.itens.Count.ToString() + " itens encontrados";
                }
            }
            else
            {
                numberOfItensFoundText.style.visibility = Visibility.Visible;
                numberOfItensFoundText.text = foundItens.itens.Count.ToString() + " itens encontrados";
            }
        }

        /// <summary>
        /// Get the string operator from the array of all operators to determine how the search for the parameter(s)
        /// should be
        /// </summary>
        private string GetOperatorFromDP(int index)
        {
            return operators[index].value;
        }

        /// <summary>
        /// Get the correct sheet base on the category selected, to guarantee the search only happens for the specific category.
        /// </summary>
        private Sheet GetCategorySheet(int value)
        {
            return HelperMethods.GetCategoryDatabaseToConsult(HelperMethods.GetCategoryString(value));
        }

  
        /// <summary>
        /// Handles what happens when the "procurar por" dropdown changes the value
        /// </summary>
        private void HandleSearchOptionDP(ChangeEvent<string> evt)
        {
            switch (evt.newValue)
            {
                case "Categoria":
                    categoryDP.style.display = DisplayStyle.None;
                    categorySearchParametersPanel.style.display = DisplayStyle.Flex;
                    //locationDP.value = HelperMethods.GetLocationDPValue("Estoque");
                    patrimonioSearchInputField.style.display = DisplayStyle.None;
                    break;
                case "Patrimônio":
                    categoryDP.style.display = DisplayStyle.None;
                    categorySearchParametersPanel.style.display = DisplayStyle.None;
                    patrimonioSearchInputField.style.display = DisplayStyle.Flex;
                    //    searchParameterInputField.placeholder.GetComponent<TextMeshProUGUI>().text = "Patrimônio";
                    numberOfItensFoundText.style.visibility = Visibility.Hidden;

                    break;
                case "Serial":
                    categoryDP.style.display = DisplayStyle.None;
                    categorySearchParametersPanel.style.display = DisplayStyle.None;
                    patrimonioSearchInputField.style.display = DisplayStyle.Flex;
                    //   searchParameterInputField.placeholder.GetComponent<TextMeshProUGUI>().text = "Serial";
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


        private void FillListView(Sheet foundItems)
        {
            if (listView.itemsSource != null)
            {
                listView.itemsSource.Clear();
            }

            listView.makeItem = () => consultResult.Instantiate();
            listView.bindItem = (element, i) =>
            {
                VisualElement[] itemBoxes = element.Q<VisualElement>("Results").Children().ToArray();
                List<Label> parameterNames = element.Query(name: "Results").Descendents<Label>(name: "ParameterName").ToList();
                List<Label> parameterValues = element.Query(name: "Results").Descendents<Label>(name: "ParameterValue").ToList();
                Label patrimonioLabel = element.Q<Label>("PatrimonioLabel");
                
                ShowResult(foundItems.itens[i], itemBoxes.ToList(), parameterNames, parameterValues);
            };
            listView.itemsSource = foundItems.itens;
        }

        /// <summary>
        /// Used to show the result of consulting the database"
        /// </summary>
        public void ShowResult(ItemColumns itemToShow, List<VisualElement> itemBoxes, List<Label> parameterNames, List<Label> parameterValues)
        {
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
    }
}