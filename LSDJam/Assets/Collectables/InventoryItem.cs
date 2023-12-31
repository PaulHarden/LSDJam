using System;

namespace Collectables
{
    [Serializable]
    public class InventoryItem
    {
        public ItemData itemData;
        public int itemQuantity;

        public InventoryItem(ItemData item)
        {
            itemData = item;
            AddQuantity();
        }

        public void AddQuantity() => itemQuantity++;
        public void SubtractQuantity() => itemQuantity--;
    }
}
