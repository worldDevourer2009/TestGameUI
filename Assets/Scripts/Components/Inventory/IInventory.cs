using UnityEngine;

namespace Components
{
    public interface IInventory
    {
        void Store(StorableObjectComponent storable, int count = 0);
        public void RemoveItem(StorableObjectComponent storable, int count = 1);
        StorableObjectComponent CreateNewItemInstance(ItemType itemType, Transform parent = null);
        SlotHandler GetFreeSlot();
    }
}