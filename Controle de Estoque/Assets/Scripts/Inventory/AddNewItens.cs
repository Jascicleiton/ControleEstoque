using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AddNewItens : MonoBehaviour
{
    public void AddItem(string sheetName)
    {
        switch (sheetName)
        {
            case ConstStrings.InventarioSnPro:
               // AddItem(entrada_txt.text, patrimonio_txt.text, status_txt.text, serial_txt.text, categoria_txt.text, fabricante_txt.text, modelo_txt.text, local_txt.text, saida_txt.text, observacao_txt.text);
                break;
            default:
                break;
        }
    }

    


}
