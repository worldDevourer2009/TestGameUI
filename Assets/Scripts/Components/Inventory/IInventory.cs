using UnityEngine;

namespace Components
{
    public interface IInventory
    {
        void GetItemFromInventoryByType(ItemType type, int amount = 1);
        void Store(StorableObjectComponent storable, int amount = 1);
        public void RemoveItem(StorableObjectComponent storable, int amount = 1);
        StorableObjectComponent CreateNewItem(ItemType itemType, Transform parent = null);
        SlotHandler GetFreeSlot();
    }
}