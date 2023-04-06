using System;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using UnityEngine;

public class ChangeEstoque : MonoBehaviour
{
    [SerializeField] private TMP_Dropdown estoqueDP;

    private void Start()
    {
        SetEstoqueDropDownValues();    
    }

    private void SetEstoqueDropDownValues()
    {
        estoqueDP.ClearOptions();
        List<string> options = Enum.GetNames(typeof(CurrentEstoque)).ToList();

        estoqueDP.AddOptions(options);
    }

    public void HandleChangeEstoque(int value)
    {
        InternalDatabase.Instance.currentEstoque = (CurrentEstoque)value;
    }
}
