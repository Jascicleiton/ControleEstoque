using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class ImportingManager : MonoBehaviour
{
    [SerializeField] TMP_Text inventario;
    [SerializeField] TMP_Text hd;
    [SerializeField] TMP_Text memoria;
    [SerializeField] TMP_Text placaderede;
    [SerializeField] TMP_Text idrac;
    [SerializeField] TMP_Text placacontroladora;
    [SerializeField] TMP_Text processador;
    [SerializeField] TMP_Text desktop;
    [SerializeField] TMP_Text fonte;
        [SerializeField] TMP_Text Switch;
    [SerializeField] TMP_Text roteador;
    [SerializeField] TMP_Text carregador;
    [SerializeField] TMP_Text adaptadorac;
    [SerializeField] TMP_Text storagenas;
    [SerializeField] TMP_Text gbic;

    private void Awake()
    {
        DontDestroyOnLoad(this);
    }

    public void InventarioLoaded()
    {
        inventario.text = "Carregado";
    }

    public void HDLoaded()
    {
        hd.text = "Carregado";
    }

    public void MemoriaLoaded()
    {
        memoria.text = "Carregado";
    }

    public void PlacaDeRedeLoaded()
    {
        placaderede.text = "Carregado";
    }

    public void iDracLoaded()
    {
        idrac.text = "Carregado";
    }

    

}
