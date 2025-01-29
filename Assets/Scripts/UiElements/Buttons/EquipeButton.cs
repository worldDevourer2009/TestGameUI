using System;
using System.Collections.Generic;
using System.Linq;
using Components;
using Signals;
using UnityEngine;
using Zenject;

namespace UiElements
{
    public class EquipeButton : MonoBehaviour, IClick, IInitializable
    {
        public event Action OnClick = delegate { };
        private List<IArmorSlot> _armorSlots;
        private SignalBus _signalBus;
        private InventoryItem _passedItem;

        [Inject]
        public void Construct(SignalBus signalBus, List<IArmorSlot> armorSlots)
        {
            _signalBus = signalBus;
            _armorSlots = armorSlots;
        }

        public void Click()
        {
            var itemType = _passedItem.GetItemConfig().ItemType;
            var slotType = ArmorTypeMapper.GetArmorSlotTypeForItem(itemType);
            var targetSlot = _armorSlots.FirstOrDefault(s => s.SlotType == slotType);
            
            if (targetSlot == null) return;
            
            var added = targetSlot.TrySetItem(_passedItem);
            if (!added) return;
            var config = _passedItem.GetItemConfig();
            _signalBus.Fire(new ArmorAddedSignal {Armor = config.ItemModifierValue, ArmorType = slotType});
            OnClick?.Invoke();
        }

        public void Initialize()
        {
            _signalBus.Subscribe<ItemClickedSignal>(HandleItem);
        }

        private void HandleItem(ItemClickedSignal evt)
        {
            var item = evt.Item;
            _passedItem = item;
            Debug.Log($"Passed item armor {item} and current item is {_passedItem}");
        }

        public void OnDestroy()
        {
            _signalBus.Unsubscribe<ItemClickedSignal>(HandleItem);
        }
    }
}