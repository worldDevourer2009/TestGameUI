using System.Linq;

namespace Components
{
    public class InventoryController : IInventory
    {
        private readonly SlotHandler[] _slots;

        public InventoryController(SlotHandler[] slots)
        {
            _slots = slots;
        }

        public void Store(StorableObjectComponent storable, int count = 1)
        {
            if (!HasEnoughSpace()) return;
            
            foreach (var cell in _slots)
            {
                var item = cell.GetComponentInChildren<StorableObjectComponent>();
                if (item == null || item.GetItemConfig().ItemType != storable.GetItemConfig().ItemType) continue;
                
                var maxStack = item.GetItemConfig().MaxStackCount;
                item.Count += count;
                    
                if (item.Count > maxStack)
                    item.Count = maxStack;
                    
                item.RefreshCount();
                return;
            }
            
            foreach (var cell in _slots)
            {
                var item = cell.GetComponentInChildren<StorableObjectComponent>();
                if (item != null) continue;
                
                storable.transform.SetParent(cell.transform); 
                storable.Count = count; 
                storable.RefreshCount();
                return;
            }
        }

        public bool HasEnoughSpace()
        {
            return _slots
                .Any(item => item
                    .GetComponentInChildren(typeof(StorableObjectComponent)) == null);
        }

        public StorableObjectComponent CanTake(SlotHandler slot)
        {
            var item = slot.GetComponentInChildren<StorableObjectComponent>();
            if (item == null)
                return null;
            
            item.Count--;
            return item;
        }
        
        public int GetItemCount(ItemType itemType)
        {
            int count = 0;

            foreach (var slot in _slots)
            {
                var storedItem = slot.GetComponentInChildren<StorableObjectComponent>();
                if (storedItem != null && storedItem.GetItemConfig().ItemType == itemType)
                {
                    count += storedItem.Count;
                }
            }

            return count;
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
            }
        }
    }
}