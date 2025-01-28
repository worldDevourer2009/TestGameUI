namespace Components
{
    public interface IInventory
    {
        void Store(StorableObjectComponent storable, int count = 0);
        StorableObjectComponent CanTake(SlotHandler slot);
        public void RemoveItem(StorableObjectComponent storable, int count = 1);
        bool HasEnoughSpace();
        public int GetItemCount(ItemType itemType);
    }
}