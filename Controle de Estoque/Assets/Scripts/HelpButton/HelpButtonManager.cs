using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HelpButtonManager : MonoBehaviour
{
    [SerializeField] GameObject helpPanel = null;

    public void ShowHidePanel()
    {
        helpPanel.SetActive(!helpPanel.activeInHierarchy);
    }
}
