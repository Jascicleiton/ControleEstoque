using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Newtonsoft.Json.Linq;
using Assets.Scripts.Inventory.Database;
using Assets.Scripts.Misc;

public class ExportTables : MonoBehaviour
{
   public void ExportInventario()
    {
        JObject state = ExportFunctions.LoadJsonFromFile(ConstStrings.C_Inventario + " - " + InternalDatabase.Instance.currentEstoque.ToString());
        JArray arrayState = new JArray();
        IList<JToken> stateList = arrayState;
        
        if(InternalDatabase.Instance.currentEstoque == CurrentEstoque.Concert)
        {
            foreach (var item in InternalDatabase.Instance.fullDatabase.itens)
            {
                JObject jObject = new JObject();
                IDictionary<string, JToken> stateDict = jObject;
                stateDict["Aquisicao"] = item.Aquisicao;
                stateDict["Entrada"] = item.Entrada;
                stateDict["Patrimonio"] = item.Patrimonio;
                stateDict["Status"] = item.Status;
                stateDict["Serial"] = item.Serial;
                stateDict["Categoria"] = item.Categoria;
                stateDict["Fabricante"] = item.Fabricante;
                stateDict["Modelo"] = item.Modelo;
                stateDict["Local"] = item.Local;
                stateDict["Pessoa"] = item.Pessoa;
                stateDict["CentroDeCusto"] = item.CentroDeCusto;
                stateDict["Saida"] = item.Saida;
                stateList.Add(jObject);
                           }
        }
        else
        {
            foreach (var item in InternalDatabase.Instance.fullDatabase.itens)
            {
                JObject jObject = new JObject();
                IDictionary<string, JToken> stateDict = jObject;
                stateDict["Aquisicao"] = item.Aquisicao;
                stateDict["Entrada"] = item.Entrada;
                stateDict["Patrimonio"] = item.Patrimonio;
                stateDict["Status"] = item.Status;
                stateDict["Serial"] = item.Serial;
                stateDict["Categoria"] = item.Categoria;
                stateDict["Fabricante"] = item.Fabricante;
                stateDict["Modelo"] = item.Modelo;
                stateDict["Local"] = item.Local;
                              stateDict["Saida"] = item.Saida;
                stateDict["Observacao"] = item.Observacao;
                stateList.Add(jObject);
            }
        }
        
    }
}
