using System;
using UnityEngine;
using UnityEngine.UIElements;

namespace Assets.Scripts.HelpButton
{
    public class HelpController : MonoBehaviour
    {
        private Button _helpButton;
        private VisualElement _helpPanel;
        private VisualElement _helpPanel2;
        [SerializeField] private bool _isMovementScene;
        private int _panelToOpenIndex = 1;

        private void OnEnable()
        {
            VisualElement root = GetComponent<UIDocument>().rootVisualElement;
            _helpButton = root.Q<Button>("HelpButton");
            if (!_isMovementScene)
            {
                _helpPanel = root.Q<VisualElement>("HelpPanel");
            }
            else
            {
                _helpPanel = root.Q<VisualElement>("HelpPanel1");
                _helpPanel2 = root.Q<VisualElement>("HelpPanel2");
            }
            _helpButton.clicked += () => { HelpClicked(); };
            //helpPanel.pickingMode = PickingMode.Ignore;
            EventHandler.ChangeAnimation += ChangePanelToOpen;
        }

        private void OnDisable()
        {
            _helpButton.clicked -= () => { HelpClicked(); };
            EventHandler.ChangeAnimation -= ChangePanelToOpen;
        }

        private void HelpClicked()
        {
            if (!_isMovementScene)
            {
                if (_helpPanel.style.display == DisplayStyle.None)
                {
                    _helpPanel.style.display = DisplayStyle.Flex;
                }
                else
                {
                    _helpPanel.style.display = DisplayStyle.None;
                }
            }
            else
            {
                OpenMovementHelpPanel();
            }
        }

        private void ChangePanelToOpen(string panelToOpen)
        {
            if (panelToOpen == "1")
            {
                _panelToOpenIndex = 1;
            }
            else if (panelToOpen == "2")
            {
                _panelToOpenIndex = 2;
            }
            else
            {
                _panelToOpenIndex = 1;
            }
        }

        private void OpenMovementHelpPanel()
        {
            if (_panelToOpenIndex == 1)
            {
                if (_helpPanel.style.display == DisplayStyle.None)
                {
                    _helpPanel.style.display = DisplayStyle.Flex;
                }
                else
                {
                    _helpPanel.style.display = DisplayStyle.None;
                }
            }
            else if (_panelToOpenIndex == 2)
            {
                if (_helpPanel2.style.display == DisplayStyle.None)
                {
                    _helpPanel2.style.display = DisplayStyle.Flex;
                }
                else
                {
                    _helpPanel2.style.display = DisplayStyle.None;
                }
            }
        }
    }
}