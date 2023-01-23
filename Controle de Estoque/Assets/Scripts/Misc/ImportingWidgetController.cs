using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ImportingWidgetController : MonoBehaviour
{
    [SerializeField] private GameObject image;
    [SerializeField] private TMP_Text percentageText;
    private float percentage;


    private void Start()
    {
        DontDestroyOnLoad(this.gameObject);
    }

    private void OnEnable()
    {
        EventHandler.ImportFinished += UpdateInformations;
    }

    private void OnDisable()
    {
        EventHandler.ImportFinished -= UpdateInformations;
    }

    private void UpdateInformations(bool isInventory)
    {
        if (isInventory)
        {
            percentage += 0.048f;
        }
        else
        {
            percentage += 0.0476f;
        }
        image.transform.localScale = new Vector3(percentage, 1f, 1f);
        percentageText.text = (percentage * 100).ToString() + "%";
        if (percentage > 0.99f)
        {
            Destroy(this.gameObject);
        }
    }
}
