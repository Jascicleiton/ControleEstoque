using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ConsultInventoryAll : MonoBehaviour
{
    [SerializeField] ConsultResult[] allresults;

    // Start is called before the first frame update
    void Start()
    {
       if(InternalDatabase.Instance != null)
        {
            if (InternalDatabase.Instance.splitDatabase[ConstStrings.InventarioSnPro].itens.Count > 0)
            {
                for (int i = 0; i < InternalDatabase.Instance.splitDatabase[ConstStrings.InventarioSnPro].itens.Count; i++)
                {
                    if (i < allresults.Length)
                    {
                        allresults[i].ShowResult(InternalDatabase.Instance.splitDatabase[ConstStrings.InventarioSnPro].itens[i], 0);
                    }
                }
            }
        }
        for (int i = 0; i < allresults.Length; i++)
        {
            if (i >= InternalDatabase.Instance.splitDatabase[ConstStrings.InventarioSnPro].itens.Count)
            {
                allresults[i].gameObject.SetActive(false);
            }
        }
    }

}
