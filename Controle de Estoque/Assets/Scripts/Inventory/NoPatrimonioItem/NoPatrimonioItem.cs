using Assets.Scripts.Inventory.PatrimonioItem;
using System;

namespace Assets.Scripts.Inventory.NoPatrimonioItem
{
    public class NoPatrimonioItem : IEquatable<NoPatrimonioItem>
    {
        public string Name { get; private set; }
        public int Quantity { get; private set; }
        public NoPatrimonioItem(string name, int quantity)
        {
            Name = name;
            Quantity = quantity;
        }

        public void AddQuantity(int quantityToAdd)
        {
            Quantity += quantityToAdd;
        }

        /// <summary>
        /// If true, quantity is decreased
        /// </summary>
        public bool DecreaseQuantity(int quantityToRemove)
        {
            if (Quantity < quantityToRemove)
            {
                return false;
            }
            else
            {
                Quantity -= quantityToRemove;
                return true;
            }
        }

        public bool Equals(NoPatrimonioItem other)
        {
            if (Name.ToUpper().Equals(other.Name.ToUpper()))
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        public override bool Equals(object obj)
        {
            return obj is NoPatrimonioItem other && Equals(other);
                
        }

        public override int GetHashCode()
        {
            return HashCode.Combine(Name, Quantity);
        }

        public static bool operator ==(NoPatrimonioItem item1, NoPatrimonioItem item2)
        {
            return item1.Equals(item2);
        }

        public static bool operator !=(NoPatrimonioItem item1, NoPatrimonioItem item2)
        {
            return !(item1 == item2);
        }
    }
}