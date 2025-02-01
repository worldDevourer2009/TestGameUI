namespace Factories
{
    public interface ICreateItem
    {
        InventoryItem CreateItemByType(ItemType type);
    }
}