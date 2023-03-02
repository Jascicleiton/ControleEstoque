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
                this.itemName.text = "Patrimônio: " + itemToShow.Patrimonio.ToString();
            }
            else if (itemName == 1)
            {
                this.itemName.text = "Serial: " + itemToShow.Serial;
            }
            itemInformationPanelControler.ShowItemConsult(itemToShow);
            ChangeSize(itemInformationPanelControler.GetNumberOfActiveBoxes());
        }
        else
        {
            Debug.LogWarning("itemToShow is null");
        }
    }

    /// <summary>
    /// Changes the panel size according to the number of active "boxes" or parameters inside the panel
    /// </summary>
    public void ChangeSize(int numberOfActiveBoxes)
    {
        if (numberOfActiveBoxes < 10)
        {
            GetComponent<RectTransform>().sizeDelta = new Vector2(1900f, 170);
        }
        else if (numberOfActiveBoxes < 19)
        {
            GetComponent<RectTransform>().sizeDelta = new Vector2(1900f, 280);
        }
        else if (numberOfActiveBoxes <28)
        {
            GetComponent<RectTransform>().sizeDelta = new Vector2(1900f, 380);
        }
        else
        {
            GetComponent<RectTransform>().sizeDelta = new Vector2(1900f, 505);
        }
    }
}
