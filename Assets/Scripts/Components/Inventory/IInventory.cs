namespace Components
{
    public interface IInventory
    {
        void Store(IStorable storable);
        IStorable Take<T>(IStorable storable);
    }
}