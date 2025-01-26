using ScriptableObjects;

namespace Components
{
    public class InventoryController : IInventory
    {
        private readonly IStorable[] _storables;
        private readonly InventoryConfig _inventoryConfig;
        private bool _isFull;

        public InventoryController(InventoryConfig inventoryConfig)
        {
            _inventoryConfig = inventoryConfig;
            _storables = new IStorable[inventoryConfig.InventorySize];
        }

        public void Store(IStorable storable)
        {
            if (_isFull) return;
            
            for (var i = 0; i < _storables.Length; i++)
            {
                if (_storables[i] != null) continue;
                _storables[i] = storable;
            }
        }

        public IStorable Take<T>(IStorable storable)
        {
            for (var i = 0; i < _storables.Length; i++)
            {
                if(_storables[i] != storable || _storables[i] == default) continue;
                var item = _storables[i];
                _storables[i] = null;
                return item;
            }
            
            return default;
        }
    }
}