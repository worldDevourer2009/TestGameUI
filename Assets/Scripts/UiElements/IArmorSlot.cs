using Components;

namespace UiElements
{
    public interface IArmorSlot
    {
        ArmorSlotType SlotType { get; }
        bool TrySetItem(InventoryItem newItem);
        void ReturnCurrentItem();
    }
}