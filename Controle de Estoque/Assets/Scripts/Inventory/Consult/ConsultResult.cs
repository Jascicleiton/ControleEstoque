using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ConsultResult : MonoBehaviour
{
    [SerializeField] private TMP_Text itemName; // shows either the item "Serial" or "Patrimônio"
    [SerializeField] private Image[] itemBoxes;
    [SerializeField] private ItemInformationPanelControler itemInformationPanelControler;

    private void Awake()
    {
        if(itemInformationPanelControler == null)
        {
            itemInformationPanelControler = GetComponent<ItemInformationPanelControler>();
        }
    }

    private void Start()
    {
        if (itemInformationPanelControler == null)
        {
            itemInformationPanelControler = GetComponent<ItemInformationPanelControler>();
        }
    }

    /// <summary>
    /// Used to show the result of consulting the database"
    /// itemName = 0: "Patrimônio"; itemName = 1: "Serial"
    /// </summary>
    public void ShowResult(ItemColumns itemToShow, int itemName)
    {
        if (itemInformationPanelControler == null)
        {
            itemInformationPanelControler = GetComponent<ItemInformationPanelControler>();
        }
        if (itemToShow != null)
        {
            if (itemName == 0)
            {
                this.itemName.text = itemToShow.Patrimonio;
            }
            else if (itemName == 1)
            {
                this.itemName.text = itemToShow.Serial;
            }
            itemInformationPanelControler.ShowItemConsult(itemToShow);
        }
        else
        {
            Debug.LogWarning("itemToShow is null");
        }
        ChangeSize();
    }

    private void ChangeSize()
    {
        if(itemBoxes.Length < 10)
        {
            GetComponent<RectTransform>().sizeDelta = new Vector2(1900f, 210f);
        }
        else if( itemBoxes.Length < 20)
        {
            GetComponent<RectTransform>().sizeDelta = new Vector2(1900f, 370f);
        }
        else
        {
            GetComponent<RectTransform>().sizeDelta = new Vector2(1900f, 550f);
        }
    }
}
