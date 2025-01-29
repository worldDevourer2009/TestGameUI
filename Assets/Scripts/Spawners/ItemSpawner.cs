using System.Collections.Generic;
using Components;
using UnityEngine;
using Zenject;

namespace Spawners
{
    public class ItemSpawner : MonoBehaviour, ISpawnItem, ITickable
    {
        [SerializeField] private List<ItemType> itemTypes;
        private IInventory _inventory;

        [Inject]
        public void Construct(IInventory inventory)
        {
            _inventory = inventory;
        }

        public void SpawnItem(ItemType type)
        {
            var freeSlot = _inventory.GetFreeSlot();
            Debug.Log("Spawning item");

            var newItem = _inventory.CreateNewItem(type, freeSlot.transform);
            newItem.Count = newItem.GetItemConfig().MaxStackCount;
            newItem.UpdateCount();
        }

        public void Tick()
        {
            if (Input.GetKeyDown(KeyCode.Alpha1))
                SpawnItem(ItemType.Cap);
            
            if (Input.GetKeyDown(KeyCode.Alpha2))
                SpawnItem(ItemType.BallisticVest);
            
            if (Input.GetKeyDown(KeyCode.Alpha3))
                SpawnItem(ItemType.Rifle);
            
            if (Input.GetKeyDown(KeyCode.Alpha4))
                SpawnItem(ItemType.Jacket);
            
            if (Input.GetKeyDown(KeyCode.Alpha5))
                SpawnItem(ItemType.HealthKit);
            
            if (Input.GetKeyDown(KeyCode.Alpha6))
                SpawnItem(ItemType.Gun);
            
            if (Input.GetKeyDown(KeyCode.Alpha7))
                SpawnItem(ItemType.Helmet);
            
            if (Input.GetKeyDown(KeyCode.Alpha8))
                SpawnItem(ItemType.None);
        }
    }
}