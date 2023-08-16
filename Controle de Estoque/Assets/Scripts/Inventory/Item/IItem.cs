using System.Collections.Generic;

namespace Assets.Scripts.Inventory.Item
{
    public interface IItem
    {
        public void Setparameters(Dictionary<string, string> parametersDictionary);
        public void SetSpecificParameter(string parameterName, string parameterValue);

        public List<string> GetAllParametersAslist();
        public Dictionary<string, string> GetAllParametersDictionary();
        public string GetSpecificParameter(string parameterName);

        public decimal GetParameterAsDecimal(string parameterName);
        public int GetParameterAsInt(string parameterName);
    }
}