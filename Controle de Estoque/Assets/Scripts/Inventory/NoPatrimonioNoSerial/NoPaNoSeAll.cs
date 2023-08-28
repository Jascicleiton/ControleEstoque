using System.Collections.Generic;

namespace Assets.Scripts.Inventory.NoPatrimonioNoSerial
{
    /// <summary>
    /// Class to hold all the items that do not have "serial" nor "patrim�nio"
    /// </summary>
    [System.Serializable]
    public class NoPaNoSeAll
    {
        public List<NoPaNoSeItem> noPaNoSeItems = new List<NoPaNoSeItem>();
    }
}