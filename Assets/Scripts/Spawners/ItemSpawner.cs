using System.Collections.Generic;
using System.Linq;
using Components;
using Factories;
using UnityEngine;
using Zenject;

namespace Spawners
{
    public class ItemSpawner : MonoBehaviour, IInitializable
    {
        [SerializeField] private List<ItemType> itemTypes;
        private IInventory _inventory;
        private ICreateItem _createItem;

        [Inject]
        public void Construct(IInventory inventory, ICreateItem createItem)
        {
            _inventory = inventory;
            _createItem = createItem;
        }
        
        public void Initialize()
        {
            foreach (var item in itemTypes.Select(type => _createItem.CreateItemByType(type)))
            {
                _inventory.Store(item, item.GetItemConfig().MaxStackCount);
            }
        }
    }
}