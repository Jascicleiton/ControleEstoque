using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class ConsultDetailsAll : MonoBehaviour
{
    [SerializeField] Transform instantiateTransform;
    [SerializeField] GameObject itemResultPrefab;
    [SerializeField] List<GameObject> allResults = new List<GameObject>();
    [SerializeField] TMP_Dropdown dropdown;

    public void HandleInputData()
    {
        ShowResult(dropdown.value);
    }

    /// <summary>
    /// Show all items from a specific categorys
    /// </summary>
    private void ShowResult(int value)
    {
        if (allResults.Count > 0)
        {
            foreach (GameObject item in allResults)
            {
                Destroy(item);
            }
        }
        if (InternalDatabase.Instance != null)
        {
            if (InternalDatabase.Instance.splitDatabase[HelperMethods.GetCategoryString(value)].itens.Count > 0)
            {
                foreach (var item in InternalDatabase.Instance.splitDatabase[HelperMethods.GetCategoryString(value)].itens)
                {
                    GameObject itemResult = Instantiate(itemResultPrefab, instantiateTransform);
                    allResults.Add(itemResult);
                    itemResult.GetComponent<ConsultResult>().ShowResult(item, 0);
                }
            }
        }
    }

}
