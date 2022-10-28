using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using System.IO;
using System.Linq;
using TMPro;
using Unity.VisualScripting;

public class InventarioManager : MonoBehaviour
{
    public TextAsset inventarioCSV;
    public InventarioListSO inventario;
    string fileName = "";
    [SerializeField] GameObject addPanel;
    [SerializeField] GameObject removePanel;

    [SerializeField] TMP_InputField entrada_txt;
    [SerializeField] TMP_InputField patrimonio_txt;
    [SerializeField] TMP_InputField status_txt;
    [SerializeField] TMP_InputField serial_txt;
    [SerializeField] TMP_InputField categoria_txt;
    [SerializeField] TMP_InputField fabricante_txt;
    [SerializeField] TMP_InputField modelo_txt;
    [SerializeField] TMP_InputField local_txt;
    [SerializeField] TMP_InputField saida_txt;
    [SerializeField] TMP_InputField observacao_txt;
    [SerializeField] TMP_Text txt;
    [SerializeField] TMP_InputField rem_patrimonio_txt;
    [SerializeField] TMP_InputField rem_serial_txt;

    // Start is called before the first frame update
    void Start()
    {
        ImportCSVToSO();
        fileName = Application.dataPath + "/Inventario_Sysnetpro.csv";
        txt.text = inventario.item.Count.ToString();
        CreateSheet();
    }

    private void CreateSheet()
    {
        TextWriter textWriter = new StreamWriter(fileName, false);
        textWriter.WriteLine("Entrada, Patrim�nio, Status, Serial, Categoria, " +
                        "Fabricante, Modelo, Local, Sa�da, Observa��o");
        textWriter.Close();
        foreach (InventarioColumns item in inventario.item)
        {
            textWriter = new StreamWriter(fileName, true);
            textWriter.WriteLine(item.Entrada + "," + item.Patrim�nio + "," + item.Status + "," +
                item.Serial + "," + item.Categoria + "," + item.Fabricante + "," +
                item.Modelo + "," + item.Local + "," + item.Sa�da + "," + item.Observa��o);
            
        }
        textWriter.Close();
    }
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.F5))
        {
            addPanel.SetActive(true);
            removePanel.SetActive(false);
        }
        if(Input.GetKeyDown(KeyCode.F6))
        {
            addPanel.SetActive(false);
            removePanel.SetActive(true);
        }
    }

    public void ImportCSVToSO()
    {        
            string[] data = inventarioCSV.text.Split(new string[] { ",", "\n" }, StringSplitOptions.None);

            int tableSize = data.Length / 10 - 1;
            InventarioColumns[] temp = new InventarioColumns[tableSize];
            for (int i = 0; i < tableSize; i++)
            {
                temp[i] = new InventarioColumns();
                temp[i].Entrada = data[10 * (i + 1)];
                temp[i].Patrim�nio = data[10 * (i + 1) +1];
                temp[i].Status = data[10 * (i + 1) + 2];
                temp[i].Serial = data[10 * (i + 1) + 3];
                temp[i].Categoria = data[10 * (i + 1) + 4];
                temp[i].Fabricante = data[10 * (i + 1) + 5];
                temp[i].Modelo = data[10 * (i + 1) + 6];
                temp[i].Local = data[10 * (i + 1) + 7];
                temp[i].Sa�da = data[10 * (i + 1) + 8];
                temp[i].Observa��o = data[10 * (i + 1) + 9];
            }
            inventario.item = temp.ToList();   
    }
    public void AddNewItem()
    {
        
        AddNewItem(entrada_txt.text, patrimonio_txt.text, status_txt.text, serial_txt.text, categoria_txt.text, fabricante_txt.text, modelo_txt.text, local_txt.text, saida_txt.text, observacao_txt.text);
    }

    public void AddNewItem(string Entrada, string Patrim�nio, string Status, string Serial, string Categoria, string Fabricante, string Modelo, string Local, string Sa�da, string Observa��o)
    {
        if(inventario != null && inventario.item.Count > 0)
        {
            InventarioColumns temp = new InventarioColumns();
            temp.Entrada = Entrada;
            temp.Patrim�nio = Patrim�nio;
            temp.Status = Status;
            temp.Serial = Serial;
            temp.Categoria = Categoria;
            temp.Fabricante = Fabricante;
            temp.Modelo = Modelo;
            temp.Local = Local;
            temp.Sa�da = Sa�da;
            temp.Observa��o = Observa��o;
            inventario.item.Add(temp);
            
            TextWriter textWriter = new StreamWriter(fileName, true);
            textWriter.WriteLine(temp.Entrada + "," + temp.Patrim�nio + "," + temp.Status + "," +
                temp.Serial + "," + temp.Categoria + "," + temp.Fabricante + "," +
                temp.Modelo + "," + temp.Local + "," + temp.Sa�da + "," + temp.Observa��o);
            textWriter.Close();
            txt.text = inventario.item.Count.ToString();
            
        }
    }

    public void RemoveItem()
    {
        foreach (InventarioColumns item in inventario.item)
        {
            if (item.Patrim�nio == rem_patrimonio_txt.text)
            {
                if(item.Serial == rem_serial_txt.text)
                {
                    inventario.item.Remove(item);
                }
            }
        }
        CreateSheet();
    }
}
