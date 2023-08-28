using Assets.Scripts.ScreenManager;
using Assets.Scripts.Users;
using UnityEngine;
using UnityEngine.UIElements;

namespace Assets.Scripts.InitialScene
{
    public class InitialSceneManager : MonoBehaviour
    {
        private Button _consultButton;
        private Button _moveButton;
        private Button _addRemoveButton;
        private Button _updateItemButton;
        private Button _exportSheetsButton;
        private Button _logoutButton;
        private Button _noPaNoSeButton;
        private Button _showMovementRecordsButton;
        private Button _recoverBKPButton;

        private Label _helloMessage;

        void Start()
        {
            _helloMessage.text = "Olá " + UsersManager.Instance.CurrentUser.GetUsername() + ". \nO que você deseja fazer agora?";
            ShowHideButtons();
        }

        private void OnEnable()
        {
            GetUiElementsReferences();
        }

        private void OnDisable()
        {
            UnsubscribeUIElementsToEvents();
        }

        private void GetUiElementsReferences()
        {
            VisualElement root = GetComponent<UIDocument>().rootVisualElement;
            _consultButton = root.Q<Button>("ConsultButton");
            _moveButton = root.Q<Button>("MoveButton");
            _addRemoveButton = root.Q<Button>("AddButton");
            _updateItemButton = root.Q<Button>("UpdateButton");
            _exportSheetsButton = root.Q<Button>("ExportSheetsButton");
            _logoutButton = root.Q<Button>("LogoutButton");
            _noPaNoSeButton = root.Q<Button>("NoPaNoSeButton");
            _showMovementRecordsButton = root.Q<Button>("ShowMovementsRecordsButton");
            _recoverBKPButton = root.Q<Button>("RecoberBKPButton");
            _helloMessage = root.Q<Label>("GrettingLabel");
            SubscribeUIElementsToEvents();
        }

        private void SubscribeUIElementsToEvents()
        {
            _consultButton.clicked += () => { ConsultClicked(); };
            _moveButton.clicked += () => { MoveClicked(); };
            _addRemoveButton.clicked += () => { AddClicked(); };
            _updateItemButton.clicked += () => { UpdateClicked(); };
            _exportSheetsButton.clicked += () => { ExportClicked(); };
            _logoutButton.clicked += () => { LogoutClicked(); };
            _noPaNoSeButton.clicked += () => { NoPaNoSeClicked(); };
            _showMovementRecordsButton.clicked += () => { MovementRecordsClicked(); };
            _recoverBKPButton.clicked += () => { RecoverBKPClicked(); };
        }

        private void UnsubscribeUIElementsToEvents()
        {
            _consultButton.clicked -= () => { ConsultClicked(); };
            _moveButton.clicked -= () => { MoveClicked(); };
            _addRemoveButton.clicked -= () => { AddClicked(); };
            _updateItemButton.clicked -= () => { UpdateClicked(); };
            _exportSheetsButton.clicked -= () => { ExportClicked(); };
            _logoutButton.clicked -= () => { LogoutClicked(); };
            _noPaNoSeButton.clicked -= () => { NoPaNoSeClicked(); };
            _showMovementRecordsButton.clicked -= () => { MovementRecordsClicked(); };
            _recoverBKPButton.clicked -= () => { RecoverBKPClicked(); };
        }

        /// <summary>
        /// Hides the buttons that the user is not allowed to use
        /// </summary>
        private void ShowHideButtons()
        {
            if (UsersManager.Instance != null)
            {
                switch (UsersManager.Instance.CurrentUser.GetAccessLevel())
                {
                    case 1:
                        _addRemoveButton.style.display = DisplayStyle.None;
                        _updateItemButton.style.display = DisplayStyle.None;
                        _exportSheetsButton.style.display = DisplayStyle.None;
                        _showMovementRecordsButton.style.display = DisplayStyle.None;
                        _recoverBKPButton.style.display = DisplayStyle.None;
                        break;
                    case 2:
                        _addRemoveButton.style.display = DisplayStyle.None;
                        _updateItemButton.style.display = DisplayStyle.None;
                        _moveButton.style.display = DisplayStyle.None;
                        _recoverBKPButton.style.display = DisplayStyle.None;
                        break;
                    case 3:
                        _updateItemButton.style.display = DisplayStyle.None;
                        _exportSheetsButton.style.display = DisplayStyle.None;
                        _noPaNoSeButton.style.display = DisplayStyle.None;
                        _showMovementRecordsButton.style.display = DisplayStyle.None;
                        _recoverBKPButton.style.display = DisplayStyle.None;
                        break;
                    case 5:
                        _updateItemButton.style.display = DisplayStyle.None;
                        _recoverBKPButton.style.display = DisplayStyle.None;
                        break;
                    case 4:
                        _addRemoveButton.style.display = DisplayStyle.None;
                        _updateItemButton.style.display = DisplayStyle.None;
                        _exportSheetsButton.style.display = DisplayStyle.None;
                        _showMovementRecordsButton.style.display = DisplayStyle.None;
                        _moveButton.style.display = DisplayStyle.None;
                        _recoverBKPButton.style.display = DisplayStyle.None;
                        break;
                    case 10:

                        break;
                    default:
                        _consultButton.style.display = DisplayStyle.None;
                        _moveButton.style.display = DisplayStyle.None;
                        _addRemoveButton.style.display = DisplayStyle.None;
                        _updateItemButton.style.display = DisplayStyle.None;
                        _exportSheetsButton.style.display = DisplayStyle.None;
                        _noPaNoSeButton.style.display = DisplayStyle.None;
                        _showMovementRecordsButton.style.display = DisplayStyle.None;
                        _recoverBKPButton.style.display = DisplayStyle.None;
                        break;
                }
            }
            else
            {
                Debug.LogWarning("UsersManager not found on InitialScene");
            }
        }

        private void ConsultClicked()
        {
            ChangeScreenManager.Instance.OpenScene(Scenes.InitialScene, Scenes.ConsultScene);
        }

        private void MoveClicked()
        {
            ChangeScreenManager.Instance.OpenScene(Scenes.InitialScene, Scenes.MovementScene);
        }

        private void AddClicked()
        {
            ChangeScreenManager.Instance.OpenScene(Scenes.InitialScene, Scenes.AddItemScene);
        }

        private void UpdateClicked()
        {
            ChangeScreenManager.Instance.OpenScene(Scenes.InitialScene, Scenes.UpdateItemScene);
        }

        private void ExportClicked()
        {
            ChangeScreenManager.Instance.OpenScene(Scenes.InitialScene, Scenes.ExportTablesScene);
        }

        private void MovementRecordsClicked()
        {
            ChangeScreenManager.Instance.OpenScene(Scenes.InitialScene, Scenes.MovementRecordsScene);
        }

        private void NoPaNoSeClicked()
        {
            ChangeScreenManager.Instance.OpenScene(Scenes.InitialScene, Scenes.NoPaNoSeScene);
        }

        private void RecoverBKPClicked()
        {
            ChangeScreenManager.Instance.OpenScene(Scenes.InitialScene, Scenes.RecoverBKP);
        }

        private void LogoutClicked()
        {
            UsersManager.Instance.CurrentUser = new User("pessoa", "");
            ChangeScreenManager.Instance.OpenScene(Scenes.InitialScene, Scenes.MainMenu);
        }
    }
}