using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ImportingWidgetController : MonoBehaviour
{
    [SerializeField] private GameObject image;
    [SerializeField] private TMP_Text percentageText;
    [SerializeField] private Canvas canvas;
    private float totalPercentageLoaded;
    private float percentageToLoad;
   

    private void Start()
    {
        DontDestroyOnLoad(this.gameObject);
        if (canvas == null)
        {
            canvas = GetComponent<Canvas>();
        }
        canvas.worldCamera = FindObjectOfType<Camera>();
    }

    private void OnEnable()
    {
        EventHandler.ImportFinished += UpdateInformations;
    }

    private void OnDisable()
    {
        EventHandler.ImportFinished -= UpdateInformations;
    }

    /// <summary>
    ///  Update the text and image each time a sheet is imported. Called by the event ImportFinished
    /// </summary>
    private void UpdateInformations(bool isInventory)
    {
        if (isInventory)
        {
            switch (InternalDatabase.Instance.currentEstoque)
            {
                case CurrentEstoque.SnPro:
                    percentageToLoad = 0.0480f;
                    break;
                case CurrentEstoque.Funsoft:
                    break;
                case CurrentEstoque.ESF:
                    percentageToLoad = 0.16f;
                    break;
                case CurrentEstoque.Testing:
                    break;
                default:
                    break;
            }

        }
        else
        {
            switch (InternalDatabase.Instance.currentEstoque)
            {
                case CurrentEstoque.SnPro:
                    percentageToLoad = 0.0476f;
                    break;
                case CurrentEstoque.Funsoft:
                    break;
                case CurrentEstoque.ESF:
                    percentageToLoad = 0.14f;
                    break;
                case CurrentEstoque.Testing:
                    break;
                default:
                    break;
            }
        }
        totalPercentageLoaded += percentageToLoad;
        image.transform.localScale = new Vector3(totalPercentageLoaded, 1f, 1f);
        percentageText.text = (totalPercentageLoaded * 100).ToString("0.00") + "%";
        if (totalPercentageLoaded > 0.99f)
        {
            Destroy(this.gameObject);
        }
    }
}
