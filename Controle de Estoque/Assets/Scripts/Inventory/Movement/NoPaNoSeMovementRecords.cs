namespace Assets.Scripts.Inventory.Movement
{
    /// <summary>
    /// Class used to store all the pertinent informations about a specific item, that does not have a "patrimônio",
    /// that is being moved
    /// </summary>
    [System.Serializable]
    public class NoPaNoSeMovementRecords
    {
        public string itemName;
        public string quantity;
        public string username;
        public string date;
        public string fromWhere;
        public string toWhere;
    }
}