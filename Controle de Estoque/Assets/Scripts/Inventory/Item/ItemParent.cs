using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Assets.Scripts.Inventory.Item
{
    public class ItemParent : MonoBehaviour
    {
        public string Aquisicao = "";
        public string Entrada = "";
        public int Patrimonio;
        public string Status = "";
        public string Serial = "";
        public string Categoria = "";
        public string Fabricante = "";
        public string Modelo = "";
        public string Local = "";
        public string Saida = "";
        public string Observacao = "";

        protected List<string> allValues = new List<string>();

        protected virtual void Awake()
        {
            allValues.Add(Aquisicao);
            allValues.Add(Entrada);
            allValues.Add(Patrimonio.ToString());
            allValues.Add(Status);
            allValues.Add(Serial);
            allValues.Add(Categoria);
            allValues.Add(Fabricante);
            allValues.Add(Modelo);
            allValues.Add(Local);
            allValues.Add(Saida);
            allValues.Add(Observacao);
        }

        public string GetValue(string variableNameToGet)
        {
            foreach (var item in allValues)
            {
                if (nameof(item) == variableNameToGet)
                {
                    return item;
                }
            }
            return null;
        }

        public List<string> GetAllValues()
        {
            return allValues;
        }
    }
}