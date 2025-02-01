using System.Linq;
using Factories;
using ModestTree;
using SavesManagement;
using UnityEngine;
using Zenject;
using Object = UnityEngine.Object;

namespace Components
{
    [ZenjectAllowDuringValidation]
    public class InventoryController : IInventory, ISaveable
    {
        public bool IsEmpty => _isEmpty;
        public string SaveId => "Inventory";
        
        private readonly SlotHandler[] _slots;
        private readonly ICreateItem _createItem;
        private bool _isEmpty = true;

        public InventoryController(SlotHandler[] slots, ICreateItem createItem)
        {
            _slots = slots;
            _createItem = createItem;
            Debug.Log($"Constrcut inventory and {_createItem == null}");
        }

        public void GetItemFromInventoryByType(ItemType type, int amount = 1)
        {
            var targetItem = _slots
                .Select(x => x.GetComponentInChildren<StorableObjectComponent>())
                .FirstOrDefault(x => x != null && x.GetItemConfig().ItemType == type);

            if (targetItem == null) return;
            
            RemoveItem(targetItem, amount);
        }

        public void Store(StorableObjectComponent storable, int amount = 1)
        {
            var itemType = storable.GetItemConfig().ItemType;
            var maxStack = storable.GetItemConfig().MaxStackCount;
            
            foreach (var slot in _slots)
            {
                if (amount <= 0) break;

                var item = slot.GetComponentInChildren<StorableObjectComponent>();
                if (item == null || item.GetItemConfig().ItemType != itemType) continue;

                var availableSpace = maxStack - item.Count;
                if (availableSpace <= 0) continue;

                var addAmount = Mathf.Min(availableSpace, amount);
                item.Count += addAmount;
                amount -= addAmount;
                item.UpdateCount();
            }
            
            while (amount > 0)
            {
                var freeSlot = _slots.FirstOrDefault(x => 
                    x.GetComponentInChildren<StorableObjectComponent>() == null);

                if (freeSlot == null) break;

                var newStack = Mathf.Min(maxStack, amount);
                var newItem = CreateNewItem(itemType, freeSlot.transform);
                Debug.Log($"Created item {newItem}");
                newItem.Count = newStack;
                amount -= newStack;
                newItem.UpdateCount();
            }
        }
        
        public StorableObjectComponent CreateNewItem(ItemType itemType, Transform parent)
        {
            var newItem = _createItem.CreateItemByType(itemType);
            newItem.transform.SetParent(parent);
            return newItem;
        }
        
        private void UpdateEmptyStatus()
        {
            _isEmpty = _slots.All(slot => 
                slot.GetComponentInChildren<StorableObjectComponent>() == null);
        }
        
        public SlotHandler GetFreeSlot()
        {
            return _slots.FirstOrDefault(x => 
                x.GetComponentInChildren<StorableObjectComponent>() == null);
        }

        public void RemoveItem(StorableObjectComponent storable, int amount = 1)
        {
            if (storable == null || amount <= 0) return;

            foreach (var slot in _slots)
            {
                var item = slot.GetComponentInChildren<StorableObjectComponent>();
                if (item == null || item != storable) continue;

                if (item.Count > amount)
                {
                    item.Count -= amount;
                    item.UpdateCount();
                    return;
                }
                
                amount -= item.Count;
                item.Count = 0;
                
                item.UpdateCount();
                
                if (item == null) return;
                Object.Destroy(item.gameObject);
        
                if (amount <= 0) break;
            }
        }
        
        private void ClearAllSlots()
        {
            foreach (var slot in _slots)
            {
                var item = GetItemFromSlot(slot);
                if (item == null) continue;
                Object.Destroy(item);
            }
        }
        
        private StorableObjectComponent GetItemFromSlot(SlotHandler slot)
        {
            return slot.GetComponentInChildren<StorableObjectComponent>();
        }
        
        public void SaveData(GameData gameData)
        {
            if (gameData?.inventoryData == null) return;
            
            gameData.inventoryData.slots.Clear();
            
            foreach (var slot in _slots)
            {
                var item = slot.GetComponentInChildren<StorableObjectComponent>();
                
                if (item == null) continue;
                
                Debug.Log($"Item to save {item.GetItemConfig().ItemType}");
                gameData.inventoryData.slots.Add(new InventorySlotData
                {
                    itemType = item.GetItemConfig().ItemType,
                    amount = item.Count,
                    slotIndex = _slots.IndexOf(slot)
                });
            }
        }

        public void LoadData(GameData gameData)
        {
            if (gameData?.inventoryData == null) return;
            
            ClearAllSlots();
            _isEmpty = true;
            
            foreach (var slot in _slots)
            {
                var item = slot.GetComponentInChildren<StorableObjectComponent>();
                if (item == null) continue;
                Object.Destroy(item.gameObject);
            }
            
            foreach (var slotData in gameData.inventoryData.slots)
            {
                if (slotData.slotIndex < 0 || slotData.slotIndex >= _slots.Length) continue;
                
                var slot = _slots[slotData.slotIndex];
                var item = CreateNewItem(slotData.itemType, slot.transform);
                item.Count = slotData.amount;
                item.UpdateCount();
            }
            
            UpdateEmptyStatus();
        }
    }
}