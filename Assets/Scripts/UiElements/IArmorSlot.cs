using Components;

namespace UiElements
{
    public interface IArmorSlot
    {
        ArmorSlotType SlotType { get; }
        bool TrySetItem(StorableObjectComponent newItem);
        void ReturnCurrentItem();
    }
}