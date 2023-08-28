using Assets.Scripts.Inventory.Database;
using Assets.Scripts.Misc;
using Assets.Scripts.Mouse;
using Assets.Scripts.ScreenManager;
using Assets.Scripts.UI;
using Assets.Scripts.Users;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

namespace Assets.Scripts.Inventory.AddItem
{
    public class AddItem : MonoBehaviour
    {
        private List<TextField> _parameterValues;
        private DropdownField _categoryDP;
        private Button _resetButton;
        private Button _returnButton;
        private Button _addButton;
        private Button _addDetailsButton;
        private string _currentCategory = "";

        IItemInformationPanelControler _itemInformationPanelController;

        private AddItemLocal _addItemLocalReference;



        private void Awake()
        {
            _itemInformationPanelController = GetComponent<ItemInformationPanelControler>();
        }

        private void Start()
        {
            _addItemLocalReference = new AddItemLocal();
            UpdateNames();
            if (UsersManager.Instance.CurrentUser.GetAccessLevel() < 10)
            {
                _addDetailsButton.style.display = DisplayStyle.None;
            }
        }

        private void OnEnable()
        {
            GetUIReferences();
            SubscribeToEvents();
            // print("Number of parameters found by additem: " + parameterValues.Count);
        }

        private void OnDisable()
        {
            UnsubscribeToEvents();
        }

        #region Get UI references and subscribe to events, if any needed
        private void GetUIReferences()
        {
            VisualElement root = GetComponent<UIDocument>().rootVisualElement;
            _categoryDP = root.Q<DropdownField>("CategoryDP");
            _resetButton = root.Q<Button>("ResetButton");
            _returnButton = root.Q<Button>("ReturnButton");
            _addButton = root.Q<Button>("AddButton");
            _addDetailsButton = root.Q<Button>("AddDetailsButton");
            _parameterValues = root.Query<VisualElement>(name: "ParametersContainer").Descendents<TextField>().ToList();
        }

        private void SubscribeToEvents()
        {
            EventHandler.MessageClosed += MessageClosed;
            EventHandler.EnableInput += SetInputEnabled;
            _categoryDP.RegisterCallback<ChangeEvent<string>>(HandleInputData);
            _resetButton.clicked += () => { ResetAddItem(); };
            _returnButton.clicked += () => { ReturnToPreviousScreen(); };
            _addButton.clicked += () => { AddItemClicked(); };
            _addDetailsButton.clicked += () => { AddDetailsItem(); };
        }

        private void UnsubscribeToEvents()
        {
            EventHandler.MessageClosed -= MessageClosed;
            EventHandler.EnableInput -= SetInputEnabled;
            _categoryDP.UnregisterCallback<ChangeEvent<string>>(HandleInputData);
            _resetButton.clicked -= () => { ResetAddItem(); };
            _returnButton.clicked -= () => { ReturnToPreviousScreen(); };
            _addButton.clicked -= () => { AddItemClicked(); };
            _addDetailsButton.clicked -= () => { AddDetailsItem(); };
        }
        #endregion

        /// <summary>
        /// Set the input enabled or disabled. Called using the Event EnableInput
        /// </summary>
        private void SetInputEnabled(bool inputEnabled)
        {
            foreach (var item in _parameterValues)
            {
                if (item.enabledInHierarchy)
                {
                    item.isReadOnly = !inputEnabled;
                }
            }
        }

        /// <summary>
        /// Updates all the placeholder text according to the category selected
        /// </summary>
        private void UpdateNames()
        {
            _currentCategory = _categoryDP.value;
            _itemInformationPanelController.ShowCategoryItemTemplate(_currentCategory);
            _itemInformationPanelController.DisableItemsForAdd(_currentCategory);
            //  EventHandler.CallUpdateTabInputs();
        }

        /// <summary>
        /// Routine used to add a new item to the online database
        /// </summary>
        private IEnumerator AddNewItemRoutine(bool addInventario)
        {
            bool addInventarioSuccess;
            List<string> parameters = new List<string>();

            SetInputEnabled(false);

            #region Add to inventory
            if (addInventario)
            {
                #region Add new item to Inventario
                parameters.Clear();
                parameters = _itemInformationPanelController.GetInventoryValues();

                if (_categoryDP.value != ConstStrings.C_Outros)
                {
                    parameters[5] = _categoryDP.value;
                }

                if (InternalDatabase.Instance.currentEstoque == CurrentEstoque.Concert)
                {
                    parameters.Add("");
                }

                yield return HelperMethods.AddUpdateItem(HelperMethods.GetCategoryInt(_categoryDP.value), 2, parameters, true);

                if (HelperMethods.GetAddUpdateResponse())
                {
                    addInventarioSuccess = true;
                    // success
                }
                else
                {
                    addInventarioSuccess = false;
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
                parameters = _itemInformationPanelController.GetCategoryValues(_categoryDP.value);

                yield return HelperMethods.AddUpdateItem(HelperMethods.GetCategoryInt(_categoryDP.value), 2, parameters, false);
                if (addInventario)
                {
                    _addItemLocalReference.AddItem(_parameterValues.ToArray(), _categoryDP.value);
                }

            }
            #endregion
            // Used if is only adding to the details database, to notify the MessageManager that the "adition to the inventory" was a success
            if (!addInventario)
            {
                EventHandler.CallOpenMessageEvent("Worked");
            }
            SetInputEnabled(true);
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

        /// <summary>
        /// Called when the AddItem Button is clicked
        /// </summary>
        private void AddItemClicked()
        {
            if (InternalDatabase.Instance.isOfflineProgram)
            {
                SetInputEnabled(false);
                _addItemLocalReference.AddItem(_parameterValues.ToArray(), _categoryDP.value);
            }
            else
            {
                StartCoroutine(AddNewItemRoutine(true));
            }
        }

        /// <summary>
        /// Call UpdateNames whenever a new category is selected
        /// </summary>
        private void HandleInputData(ChangeEvent<string> evt)
        {
            _currentCategory = evt.newValue;
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
        /// Resets all inputs to default values
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
            if (InternalDatabase.Instance.isOfflineProgram)
            {
                _addItemLocalReference.AddItem(_parameterValues.ToArray(), _categoryDP.value);
            }
            else
            {
                StartCoroutine(AddNewItemRoutine(false));
            }
        }

    }
}