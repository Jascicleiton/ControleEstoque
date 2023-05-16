using UnityEngine;
using UnityEngine.UIElements;

namespace InitialScene
{
    public class InitialSceneManager : MonoBehaviour
    {
        private VisualElement root;

        private Button consultButton;
        private Button moveButton;
        private Button addRemoveButton;
        private Button updateItemButton;
        private Button exportSheetsButton;
        private Button logoutButton;
        private Button noPaNoSeButton;
        private Button showMovementRecordsButton;
        private Button recoverBKPButton;

        private Label helloMessage;

        void Start()
        {
            helloMessage.text = "Olá " + UsersManager.Instance.currentUser.GetUsername() + ". \nO que você deseja fazer agora?";
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
            root = GetComponent<UIDocument>().rootVisualElement;
            consultButton = root.Q<Button>("ConsultButton");
            moveButton = root.Q<Button>("MoveButton");
            addRemoveButton = root.Q<Button>("AddButton");
            updateItemButton = root.Q<Button>("UpdateButton");
            exportSheetsButton = root.Q<Button>("ExportSheetsButton");
            logoutButton = root.Q<Button>("LogoutButton");
            noPaNoSeButton = root.Q<Button>("NoPaNoSeButton");
            showMovementRecordsButton = root.Q<Button>("ShowMovementsRecordsButton");
            recoverBKPButton = root.Q<Button>("RecoberBKPButton");
            helloMessage = root.Q<Label>("GrettingLabel");
            SubscribeUIElementsToEvents();
        }

        private void SubscribeUIElementsToEvents()
        {
            consultButton.clicked += () => { ConsultClicked(); };
            moveButton.clicked += () => { MoveClicked(); };
            addRemoveButton.clicked += () => { AddClicked(); };
            updateItemButton.clicked += () => { UpdateClicked(); };
            exportSheetsButton.clicked += () => { ExportClicked(); };
            logoutButton.clicked += () => { LogoutClicked(); };
            noPaNoSeButton.clicked += () => { NoPaNoSeClicked(); };
            showMovementRecordsButton.clicked += () => { MovementRecordsClicked(); };
            recoverBKPButton.clicked += () => { RecoverBKPClicked(); };
        }

        private void UnsubscribeUIElementsToEvents()
        {
            consultButton.clicked -= () => { ConsultClicked(); };
            moveButton.clicked -= () => { MoveClicked(); };
            addRemoveButton.clicked -= () => { AddClicked(); };
            updateItemButton.clicked -= () => { UpdateClicked(); };
            exportSheetsButton.clicked -= () => { ExportClicked(); };
            logoutButton.clicked -= () => { LogoutClicked(); };
            noPaNoSeButton.clicked -= () => { NoPaNoSeClicked(); };
            showMovementRecordsButton.clicked -= () => { MovementRecordsClicked(); };
            recoverBKPButton.clicked -= () => { RecoverBKPClicked(); };
        }

        /// <summary>
        /// Hides the buttons that the user is not allowed to use
        /// </summary>
        private void ShowHideButtons()
        {
            if (UsersManager.Instance != null)
            {
                switch (UsersManager.Instance.currentUser.GetAccessLevel())
                {
                    case 1:
                        addRemoveButton.style.display = DisplayStyle.None;
                        updateItemButton.style.display = DisplayStyle.None;
                        exportSheetsButton.style.display = DisplayStyle.None;
                        showMovementRecordsButton.style.display = DisplayStyle.None;
                        recoverBKPButton.style.display = DisplayStyle.None;
                        break;
                    case 2:
                        addRemoveButton.style.display = DisplayStyle.None;
                        updateItemButton.style.display = DisplayStyle.None;
                        moveButton.style.display = DisplayStyle.None;
                        recoverBKPButton.style.display = DisplayStyle.None;
                        break;
                    case 3:
                        updateItemButton.style.display = DisplayStyle.None;
                        exportSheetsButton.style.display = DisplayStyle.None;
                        noPaNoSeButton.style.display = DisplayStyle.None;
                        showMovementRecordsButton.style.display = DisplayStyle.None;
                        recoverBKPButton.style.display = DisplayStyle.None;
                        break;
                    case 5:
                        updateItemButton.style.display = DisplayStyle.None;
                        recoverBKPButton.style.display = DisplayStyle.None;
                        break;
                    case 4:
                        addRemoveButton.style.display = DisplayStyle.None;
                        updateItemButton.style.display = DisplayStyle.None;
                        exportSheetsButton.style.display = DisplayStyle.None;
                        showMovementRecordsButton.style.display = DisplayStyle.None;
                        moveButton.style.display = DisplayStyle.None;
                        recoverBKPButton.style.display = DisplayStyle.None;
                        break;
                    case 10:

                        break;
                    default:
                        consultButton.style.display = DisplayStyle.None;
                        moveButton.style.display = DisplayStyle.None;
                        addRemoveButton.style.display = DisplayStyle.None;
                        updateItemButton.style.display = DisplayStyle.None;
                        exportSheetsButton.style.display = DisplayStyle.None;
                        noPaNoSeButton.style.display = DisplayStyle.None;
                        showMovementRecordsButton.style.display = DisplayStyle.None;
                        recoverBKPButton.style.display = DisplayStyle.None;
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
            ChangeScreenManager.Instance.OpenScene(Scenes.InitialScene, Scenes.TestScene);
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
            UsersManager.Instance.currentUser = new User("pessoa", "");
            ChangeScreenManager.Instance.OpenScene(Scenes.InitialScene, Scenes.MainMenu);
        }
    }
}