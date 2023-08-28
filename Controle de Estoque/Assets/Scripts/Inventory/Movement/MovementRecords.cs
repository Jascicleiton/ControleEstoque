namespace Assets.Scripts.Inventory.Movement
{
    /// <summary>
    /// Class used to store all the pertinent informations about a specific item that is being moved
    /// </summary>
    [System.Serializable]
    public class MovementRecords
    {
        public ItemColumns item;
        public string username;
        public string date;
        public string fromWhere;
        public string toWhere;
    }
}