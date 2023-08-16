using UnityEngine;
using System.Collections.Generic;
using System.Linq;
using System;

namespace Assets.Scripts.Inventory.Item
{
    public class ItemParent : IItem, IEquatable<ItemParent>
    {
        protected Dictionary<string, string> allParameters = new Dictionary<string, string>();

        protected virtual void Awake()
        {
            allParameters.Add(ConstStrings.Aquisicao, default);
            allParameters.Add(ConstStrings.Entrada, default);
            allParameters.Add(ConstStrings.Patrimonio_I, default);
            allParameters.Add(ConstStrings.Status, default);
            allParameters.Add(ConstStrings.Serial, default);
            allParameters.Add(ConstStrings.Categoria, default);
            allParameters.Add(ConstStrings.Fabricante, default);
            allParameters.Add(ConstStrings.Modelo, default);
            allParameters.Add(ConstStrings.Local, default);
            allParameters.Add(ConstStrings.Saida, default);
            allParameters.Add(ConstStrings.Observacao, default);
        }

        public string GetSpecificParameter(string parameterNameToGet)
        {
            if (allParameters.ContainsKey(parameterNameToGet))
            {
                return allParameters[parameterNameToGet];
            }
            else
            {
                Debug.LogWarning($"Invalid parameter name or parameter not registerd on {nameof(allParameters)} dictionary");
                return null;
            }
        }

        public List<string> GetAllParametersAslist()
        {
            return allParameters.Values.ToList();
        }

        public Dictionary<string, string> GetAllParametersDictionary()
        {
            return allParameters;
        }

        public void Setparameters(Dictionary<string, string> parametersDictionary)
        {
            foreach (var item in parametersDictionary.Keys)
            {
                if (allParameters.ContainsKey(item))
                {
                    allParameters[item] = parametersDictionary[item];
                }
                else
                {
                    Debug.LogWarning($"Invalid parameter name or parameter not registerd on {nameof(allParameters)} dictionary");
                }
            }
        }

        public void SetSpecificParameter(string parameterName, string parameterValue)
        {
            if (allParameters.ContainsKey(parameterName))
            {
                allParameters[parameterName] = parameterValue;
            }
            else
            {
                Debug.LogWarning($"Invalid parameter name or parameter not registerd on {nameof(allParameters)} dictionary");
            }
        }

        public decimal GetParameterAsDecimal(string parameterName)
        {
            if (allParameters.ContainsKey(parameterName))
            {
                decimal parameterValue;
                if (decimal.TryParse(allParameters[parameterName], out parameterValue))
                {
                    return parameterValue;
                }
                Debug.LogError($"Value of {allParameters[parameterName]} is not a decimal");
            }
            Debug.LogError($"Invalid parameter name or parameter not registerd on {nameof(allParameters)} dictionary");
            return -1000;
        }

        public int GetParameterAsInt(string parameterName)
        {
            if (allParameters.ContainsKey(parameterName))
            {
                int parameterValue;
                if (int.TryParse(allParameters[parameterName], out parameterValue))
                {
                    return parameterValue;
                }
                Debug.LogError($"Value of {allParameters[parameterName]} is not a integer");
            }
            Debug.LogError($"Invalid parameter name or parameter not registerd on {nameof(allParameters)} dictionary");
            return -1000;
        }

        #region Equality overloads/overrides
        public bool Equals(ItemParent other)
        {
            return GetSpecificParameter(ConstStrings.Patrimonio_I) == other.GetSpecificParameter(ConstStrings.Patrimonio_I);
        }
        public override bool Equals(object obj)
        {
            return obj is ItemParent other &&
                GetSpecificParameter(ConstStrings.Patrimonio_I) == other.GetSpecificParameter(ConstStrings.Patrimonio_I);
        }
        public override int GetHashCode()
        {
            return int.Parse(GetSpecificParameter(ConstStrings.Patrimonio_I));
        }
        public static bool operator ==(ItemParent item1, ItemParent item2) => item1.Equals(item2);
        public static bool operator !=(ItemParent item1, ItemParent item2) => !item1.Equals(item2);
        #endregion
    }
}