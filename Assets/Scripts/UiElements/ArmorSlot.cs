using System.Collections.Generic;
using Components;
using TMPro;
using UnityEngine;
using Zenject;

namespace UiElements
{
    public class ArmorSlot : MonoBehaviour, IArmorSlot
    {
        public ArmorSlotType SlotType => armorSlotType;
        [SerializeField] private ArmorSlotType armorSlotType;
        [SerializeField] private TextMeshProUGUI armorCount;
        private IInventory _inventory;
        private InventoryItem _currentItem;

        [Inject]
        public void Construct(IInventory inventory)
        {
            _inventory = inventory;
        }

        public bool TrySetItem(InventoryItem newItem)
        {
            Debug.Log($"Passed item {newItem.ItemType}");
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