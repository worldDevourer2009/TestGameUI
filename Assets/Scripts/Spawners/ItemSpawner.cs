using System.Collections.Generic;
using Components;
using Signals;
using UnityEngine;
using Zenject;

namespace Spawners
{
    public class ItemSpawner : MonoBehaviour, ISpawnItem, IInitializable
    {
        [SerializeField] private List<ItemType> itemTypes;
        [SerializeField] private List<ItemType> standardItems;
        private IInventory _inventory;
        private SignalBus _signalBus;

        [Inject]
        public void Construct(IInventory inventory, SignalBus signalBus)
        {
            _inventory = inventory;
            _signalBus = signalBus;
        }

        public void SpawnItem(ItemType type)
        {
            var freeSlot = _inventory.GetFreeSlot();
            var newItem = _inventory.CreateNewItem(type, freeSlot.transform);
            newItem.Count = newItem.GetItemConfig().MaxStackCount;
            newItem.UpdateCount();
        }
        
        public void Initialize()
        {
            _signalBus.Subscribe<EnemyDeathSignal>(HanldeLoot);
            if (!_inventory.IsEmpty) return;
            Debug.Log($"Invnetory empty is {_inventory.IsEmpty}");
            HandleAddingStandardLoot();
        }

        private void HandleAddingStandardLoot()
        {
            foreach (var item in standardItems)
            {
                SpawnItem(item);
            }
        }

        private void HanldeLoot(EnemyDeathSignal evt)
        {
            var loot = evt.Loot;
            if (loot == default) return;
            SpawnItem(loot);
        }
    }
}