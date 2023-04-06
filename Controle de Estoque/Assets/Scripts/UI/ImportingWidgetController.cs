using System.Collections;
using TMPro;
using UnityEngine;

public class ImportingWidgetController : MonoBehaviour
{
    [SerializeField] private GameObject image;
    [SerializeField] private TMP_Text percentageText;
    [SerializeField] private Canvas canvas;
    private float totalPercentageLoaded;
    private float percentageToLoad;
    private bool coroutineCalled = false;

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
        coroutineCalled = false;
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
        if(!coroutineCalled)
        {
            StartCoroutine(WaitAMinute());
            coroutineCalled = true;
        }
        if (isInventory)
        {
            switch (InternalDatabase.Instance.currentEstoque)
            {
                case CurrentEstoque.SnPro:
                    percentageToLoad = 0.0480f;
                    break;
                case CurrentEstoque.Fumsoft:
                case CurrentEstoque.Concert:
                    percentageToLoad = 0.125f;
                    break;
                case CurrentEstoque.ESF:
                    percentageToLoad = 0.16f;
                    break;
                case CurrentEstoque.Testing:
                    break;
                default:
                    percentageToLoad = 0.1f;
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
                case CurrentEstoque.Fumsoft:
                case CurrentEstoque.Concert:
                    percentageToLoad = 0.125f;
                    break;
                case CurrentEstoque.ESF:
                    percentageToLoad = 0.25f;
                    break;
                case CurrentEstoque.Testing:
                    break;
                default:
                    percentageToLoad = 0.1f;
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

    private IEnumerator WaitAMinute()
    {
        yield return new WaitForSecondsRealtime(60f);
        Destroy(this.gameObject);
    }
}
