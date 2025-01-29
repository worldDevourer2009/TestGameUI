using System.Collections.Generic;
using System.Linq;
using Components;
using SavesManagement;
using TMPro;
using UnityEngine;
using Zenject;

namespace UiElements
{
    public class ArmorSlot : MonoBehaviour, IArmorSlot, ISaveable
    {
        public string SaveId => $"ArmorSlot_{armorSlotType}";
        public ArmorSlotType SlotType => armorSlotType;
        [SerializeField] private ArmorSlotType armorSlotType;
        [SerializeField] private TextMeshProUGUI armorCount;
        private IInventory _inventory;
        private StorableObjectComponent _currentItem;

        [Inject]
        public void Construct(IInventory inventory)
        {
            _inventory = inventory;
        }

        public bool TrySetItem(StorableObjectComponent newItem)
        {
            Debug.Log($"Passed item {newItem.GetItemConfig().ItemType}");
            var itemSlotType = ArmorTypeMapper.GetArmorSlotTypeForItem(newItem.GetItemConfig().ItemType);
            Debug.Log($"Item slot type {itemSlotType}");

            if (itemSlotType != armorSlotType || itemSlotType == ArmorSlotType.None)
            {
                Debug.Log($"What type ? {armorSlotType}");
                return false;
            }
            
            if (_currentItem != null)
            {
                Debug.Log("Returning type...");
                ReturnCurrentItem();
            }

            _currentItem = newItem;
            var config = _currentItem.GetItemConfig();
            Debug.Log($"Transfrom is {transform.gameObject.name}");
            _currentItem.transform.SetParent(transform);
            armorCount.text = _currentItem.GetItemConfig().ItemModifierValue.ToString();
            Debug.Log($"Transfrom set is {_currentItem.transform.parent.name}");
            return true;
        }

        public void ReturnCurrentItem()
        {
            Debug.Log($"Hello {_currentItem} and {_currentItem.Count}");
            _inventory.Store(_currentItem, _currentItem.Count);
            _currentItem = null;
        }
        
        public void SaveData(GameData gameData)
        {
            if (_currentItem != null)
            {
                gameData.inventoryData.equippedItems.Add(new EquippedItemData
                {
                    itemType = _currentItem.GetItemConfig().ItemType,
                    slotType = armorSlotType
                });
            }
        }

        public void LoadData(GameData gameData)
        {
            var equippedItem = gameData.inventoryData.equippedItems
                .FirstOrDefault(e => e.slotType == armorSlotType);
            
            if (equippedItem != null)
            {
                var item = _inventory.CreateNewItemInstance(equippedItem.itemType, transform);
                TrySetItem(item);
            }
        }
    }
    
    public static class ArmorTypeMapper
    {
        private static readonly Dictionary<ItemType, ArmorSlotType> ArmorTypeMapping = 
            new()
            {
                { ItemType.Helmet, ArmorSlotType.Head },
                { ItemType.Jacket, ArmorSlotType.Body },
                { ItemType.BallisticVest, ArmorSlotType.Body },
                { ItemType.Cap, ArmorSlotType.Head }
            };

        public static ArmorSlotType GetArmorSlotTypeForItem(ItemType itemType)
        {
            return ArmorTypeMapping.GetValueOrDefault(itemType, ArmorSlotType.None);
        }
    }
}