using UnityEngine;
using System.Collections.Generic;
using System.Linq;
using System;
using Assets.Scripts.Misc;

namespace Assets.Scripts.Inventory.PatrimonioItem
{
    public class PatrimonioItemParent : IPatrimonioItem, IEquatable<PatrimonioItemParent>
    {
        protected Dictionary<string, string> allParameters = new Dictionary<string, string>();

        public PatrimonioItemParent()
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

        /// <summary>
        /// Get a specific parameter value based from it's name
        /// </summary>
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

        /// <summary>
        /// Get all parameters as a list
        /// </summary>
        public List<string> GetAllParametersAslist()
        {
            return allParameters.Values.ToList();
        }

        /// <summary>
        /// Get the dictionary with all parameters
        /// </summary>
        public Dictionary<string, string> GetAllParametersDictionary()
        {
            return allParameters;
        }

        /// <summary>
        /// Set all the item's parameters
        /// </summary>
        public void SetParameters(Dictionary<string, string> parametersDictionary)
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

        /// <summary>
        /// Set a specific parameter value
        /// </summary>
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

        /// <summary>
        /// Return parameter as decimal. If null, the parameter could not be parsed to a decimal
        /// </summary>
        public decimal? GetParameterAsDecimal(string parameterName)
        {
            if (allParameters.ContainsKey(parameterName))
            {

                if (decimal.TryParse(allParameters[parameterName], out decimal parameterValue))
                {
                    return parameterValue;
                }
                Debug.LogError($"Value of {allParameters[parameterName]} is not a decimal");
            }
            Debug.LogError($"Invalid parameter name or parameter not registerd on {nameof(allParameters)} dictionary");
            return null;
        }

        /// <summary>
        /// Return parameter as int. If null, the parameter could not be parsed to an int
        /// </summary>
        public int? GetParameterAsInt(string parameterName)
        {
            if (allParameters.ContainsKey(parameterName))
            {
                if (int.TryParse(allParameters[parameterName], out int parameterValue))
                {
                    return parameterValue;
                }
                Debug.LogError($"Value of {allParameters[parameterName]} is not a integer");
            }
            Debug.LogError($"Invalid parameter name or parameter not registerd on {nameof(allParameters)} dictionary");
            return null;
        }

        #region Equality overloads/overrides
        public bool Equals(PatrimonioItemParent other)
        {
            return GetSpecificParameter(ConstStrings.Patrimonio_I) == other.GetSpecificParameter(ConstStrings.Patrimonio_I);
        }
        public override bool Equals(object obj)
        {
            return obj is PatrimonioItemParent other &&
                GetSpecificParameter(ConstStrings.Patrimonio_I) == other.GetSpecificParameter(ConstStrings.Patrimonio_I);
        }
        public override int GetHashCode()
        {
            return int.Parse(GetSpecificParameter(ConstStrings.Patrimonio_I));
        }
        public static bool operator ==(PatrimonioItemParent item1, PatrimonioItemParent item2) => item1.Equals(item2);
        public static bool operator !=(PatrimonioItemParent item1, PatrimonioItemParent item2) => !item1.Equals(item2);
        #endregion
    }
}