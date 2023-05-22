using Assets.Scripts.UI;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

namespace Inventory.AddItem
{
    public class AddItem : MonoBehaviour
    {
        private VisualElement root;
        private TextField[] parameterValues;
        private DropdownField categoryDP;
        private Button resetButton;
        private Button returnButton;
        private Button addButton;
        private Button addDetailsButton;
        private string currentCategory = "";

        ItemInformationPanelControler1 itemInformationPanelController;
        CategoryDropDownHandler1 categoryDropDownHandler;
        private List<string> parameters = new List<string>();

        bool addDetalheSuccess = false;
        bool addInventarioSuccess = false;

        private void Start()
        {
           
                itemInformationPanelController = GetComponent<ItemInformationPanelControler1>();
           
            UpdateNames();
            if (UsersManager.Instance.currentUser.GetAccessLevel() < 10)
            {
                addDetailsButton.style.display = DisplayStyle.None;
            }
        }

        private void OnEnable()
        {
            GetUIReferences();
            SubscribeToEvents();            
        }

        private void OnDisable()
        {
            UnsubscribeToEvents();
        }

        /// <summary>
        /// Set the input enabled or disabled. Called using the Event EnableInput
        /// </summary>
        private void SetInputEnabled(bool inputEnabled)
        {
            foreach (var item in parameterValues)
            {
                if (item.enabledInHierarchy)
                {
                    item.isReadOnly = inputEnabled;
                }
            }
        }

        /// <summary>
        /// Updates all the placeholder text according to the category selected
        /// </summary>
        private void UpdateNames()
        {
            itemInformationPanelController.ShowCategoryItemTemplate(currentCategory);
            itemInformationPanelController.DisableItemsForAdd(currentCategory);
            EventHandler.CallUpdateTabInputs();
        }

        /// <summary>
        /// Routine used to add a new item to the online database
        /// </summary>
        private IEnumerator AddNewItemRoutine(bool addInventario)
        {
            SetInputEnabled(false);
            #region Add to inventory
            if (addInventario)
            {
                #region Add new item to Inventario
                parameters.Clear();
                parameters = itemInformationPanelController.GetInventoryValues();
                if (categoryDP.value == ConstStrings.Outros)
                {
                    parameters.Insert(5, parameterValues[5].text);
                }
                else
                {
                    parameters.Insert(5, categoryDP.value);
                }

                if (InternalDatabase.Instance.currentEstoque == CurrentEstoque.Concert)
                {
                    parameters.Add("");
                }
                else
                {
                    parameters.Insert(9, "");
                }
                
                  yield return HelperMethods.AddUpdateItem(HelperMethods.GetCategoryInt(categoryDP.value), 2, parameters, true);

                if (HelperMethods.GetAddUpdateResponse())
                {
                    addInventarioSuccess = true;
                    // success
                }
                else
                {
                    addInventarioSuccess = false;
                    addDetalheSuccess = false;
                    // fail
                }
                SetInputEnabled(true);
                EventHandler.CallIsOneMessageOnlyEvent(false);
                #endregion
            }
            else
            {
                EventHandler.CallIsOneMessageOnlyEvent(true);
                addInventarioSuccess = true;
            }
            #endregion
            #region Add to details
            if (addInventarioSuccess)
            {
                parameters.Clear();
                parameters = itemInformationPanelController.GetCategoryValues(categoryDP.value);

                  yield return HelperMethods.AddUpdateItem(HelperMethods.GetCategoryInt(categoryDP.value), 2, parameters, false);
                if (HelperMethods.GetAddUpdateResponse())
                {
                    addDetalheSuccess = true;
                }
                else
                {
                    addDetalheSuccess = false;
                }
                if (addInventario)
                {
                          AddItemLocal.AddItem(parameterValues, categoryDP.value);
                }
                SetInputEnabled(true);
            }
            #endregion
            // Used if is only adding to the details database, to notify the MessageManager that the "adition to the inventory" was a success
            if (!addInventario)
            {
                EventHandler.CallOpenMessageEvent("Worked");
                addInventarioSuccess = true;
            }
            
        }

        /// <summary>
        /// Close the message. Called by the Event MessageClosed
        /// </summary>
        private void MessageClosed()
        {
            UpdateNames();
            MouseManager.Instance.SetDefaultCursor();
            SetInputEnabled(true);
        }

        private void GetUIReferences()
        {
            root = GetComponent<UIDocument>().rootVisualElement;
            categoryDP = root.Q<DropdownField>("CategoryDP");
            resetButton = root.Q<Button>("ResetButton");
            returnButton = root.Q<Button>("ReturnButton");
            addButton = root.Q<Button>("AddButton");
            addDetailsButton = root.Q<Button>("AddDetailsButton");
        }

        private void SubscribeToEvents()
        {
            EventHandler.MessageClosed += MessageClosed;
            EventHandler.EnableInput += SetInputEnabled;
            categoryDP.RegisterCallback<ChangeEvent<string>>(HandleInputData);
            resetButton.clicked += () => { ResetAddItem(); };
            returnButton.clicked += () => { ReturnToPreviousScreen(); };
            addButton.clicked += () => { AddItemClicked(); };
            addDetailsButton.clicked += () => { AddDetailsItem(); };
        }

        private void UnsubscribeToEvents()
        {
            EventHandler.MessageClosed -= MessageClosed;
            EventHandler.EnableInput -= SetInputEnabled;
            categoryDP.UnregisterCallback<ChangeEvent<string>>(HandleInputData);
            resetButton.clicked -= () => { ResetAddItem(); };
            returnButton.clicked -= () => { ReturnToPreviousScreen(); };
            addButton.clicked -= () => { AddItemClicked(); };
            addDetailsButton.clicked -= () => { AddDetailsItem(); };
        }

        /// <summary>
        /// Called when the AddItem Button is clicked
        /// </summary>
        private void AddItemClicked()
        {
            StartCoroutine(AddNewItemRoutine(true));
        }

        /// <summary>
        /// Call UpdateNames whenever a new category is selected
        /// </summary>
        private void HandleInputData(ChangeEvent<string> evt)
        {
            currentCategory = evt.newValue;
            UpdateNames();
        }

        /// <summary>
        /// Goes to InitialScene
        /// </summary>
        private void ReturnToPreviousScreen()
        {
            ChangeScreenManager.Instance.OpenScene(Scenes.AddItemScene, Scenes.InitialScene);
        }

        /// <summary>
        /// Resets all inputs to default balues
        /// </summary>
        private void ResetAddItem()
        {
            UpdateNames();
        }

        /// <summary>
        /// Add an item only to the detaisl table. Only users with access level greater or equal to 10 can use this
        /// </summary>
        private void AddDetailsItem()
        {
            StartCoroutine(AddNewItemRoutine(false));
        }

    }
}