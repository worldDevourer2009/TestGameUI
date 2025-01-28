using System.Linq;
using Factories;
using UnityEngine;

namespace Components
{
    public class InventoryController : IInventory
    {
        private readonly SlotHandler[] _slots;
        private readonly ICreateItem _createItem;

        public InventoryController(SlotHandler[] slots, ICreateItem createItem)
        {
            _slots = slots;
            _createItem = createItem;
        }

        public void Store(StorableObjectComponent storable, int count = 1)
        {
            var itemType = storable.GetItemConfig().ItemType;
            var maxStack = storable.GetItemConfig().MaxStackCount;
            
            foreach (var slot in _slots)
            {
                if (count <= 0) break;

                var item = slot.GetComponentInChildren<StorableObjectComponent>();
                if (item == null || item.GetItemConfig().ItemType != itemType) continue;

                var availableSpace = maxStack - item.Count;
                if (availableSpace <= 0) continue;

                var addAmount = Mathf.Min(availableSpace, count);
                item.Count += addAmount;
                count -= addAmount;
                item.RefreshCount();
            }
            
            while (count > 0)
            {
                var freeSlot = _slots.FirstOrDefault(s => 
                    s.GetComponentInChildren<StorableObjectComponent>() == null);

                if (freeSlot == null) break;

                var newStack = Mathf.Min(maxStack, count);
                var newItem = CreateNewItemInstance(itemType, freeSlot.transform);
                Debug.Log($"Created item {newItem}");
                newItem.Count = newStack;
                count -= newStack;
                newItem.RefreshCount();
            }
        }
        
        public StorableObjectComponent CreateNewItemInstance(ItemType itemType, Transform parent)
        {
            var newItem = _createItem.CreateItemByType(itemType);
            newItem.transform.SetParent(parent);
            return newItem;
        }
        
        public SlotHandler GetFreeSlot()
        {
            return _slots.FirstOrDefault(s => 
                s.GetComponentInChildren<StorableObjectComponent>() == null);
        }

        public void RemoveItem(StorableObjectComponent storable, int count = 1)
        {
            foreach (var cell in _slots)
            {
                var item = cell.GetComponentInChildren<StorableObjectComponent>();
                if (item == null || item.GetItemConfig().ItemType != storable.GetItemConfig().ItemType) continue;

                if (item.Count > count)
                {
                    item.Count -= count;
                    item.RefreshCount();
                    return;
                }
                
                count -= item.Count; 
                item.Count = 0;
                item.RefreshCount(); 
                if (count <= 0) return;
            }
        }
    }
}