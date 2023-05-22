using System;
using UnityEngine;
using UnityEngine.UIElements;

public class HelpController : MonoBehaviour
{
    private Button helpButton;
    private VisualElement helpPanel;
    private VisualElement helpPanel2;
   [SerializeField] private bool isMovementScene;
    private int panelToOpenIndex = 1;

    private void OnEnable()
    {
        VisualElement root = GetComponent<UIDocument>().rootVisualElement;
        helpButton = root.Q<Button>("HelpButton");
        if (!isMovementScene)
        {
            helpPanel = root.Q<VisualElement>("HelpPanel");
        }
        else
        {
            helpPanel = root.Q<VisualElement>("HelpPanel1");
            helpPanel2 = root.Q<VisualElement>("HelpPanel2");
        }
        helpButton.clicked += () => { HelpClicked(); };
        //helpPanel.pickingMode = PickingMode.Ignore;
        EventHandler.ChangeAnimation += ChangePanelToOpen;
    }

    private void OnDisable()
    {
        helpButton.clicked -= () => { HelpClicked(); };
        EventHandler.ChangeAnimation -= ChangePanelToOpen;
    }

    private void HelpClicked()
    {
        if (!isMovementScene)
        {
            if (helpPanel.style.display == DisplayStyle.None)
            {
                helpPanel.style.display = DisplayStyle.Flex;
            }
            else
            {
                helpPanel.style.display = DisplayStyle.None;
            }
        }
        else
        {
            OpenMovementHelpPanel();
        }
    }

    private void ChangePanelToOpen(string panelToOpen)
    {
        if(panelToOpen == "1")
        {
            panelToOpenIndex = 1;
        }
        else if(panelToOpen == "2")
        {
            panelToOpenIndex = 2;
        }
        else
        {
            panelToOpenIndex = 1;
        }
    }

    private void OpenMovementHelpPanel()
    {
        if (panelToOpenIndex == 1)
        {
            if (helpPanel.style.display == DisplayStyle.None)
            {
                helpPanel.style.display = DisplayStyle.Flex;
            }
            else
            {
                helpPanel.style.display = DisplayStyle.None;
            }
        }
        else if(panelToOpenIndex == 2)
        {
            if (helpPanel2.style.display == DisplayStyle.None)
            {
                helpPanel2.style.display = DisplayStyle.Flex;
            }
            else
            {
                helpPanel2.style.display = DisplayStyle.None;
            }
        }
    }
}
