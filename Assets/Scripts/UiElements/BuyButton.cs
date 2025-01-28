using System;
using Components;
using ScriptableObjects;
using Signals;
using UnityEngine;
using Zenject;

namespace UiElements
{
    public class BuyButton : MonoBehaviour, IClick, IInitializable
    {
        public event Action OnClick = delegate { };
        private InventoryItem _currentItem;
        private ItemConfig _itemConfig;
        
        private IInventory _inventory;
        private SignalBus _signalBus;

        [Inject]
        public void Construct(IInventory inventory, SignalBus signalBus)
        {
            _inventory = inventory;
            _signalBus = signalBus;
            Debug.Log("Hello");
        }
        
        public void Initialize()
        {
            Debug.Log("World");
             _signalBus.Subscribe<ItemClickedSignal>(HandleRecievedItem);
        }

        private void HandleRecievedItem(ItemClickedSignal evt)
        {
            var item = evt.Item;
            var itemConfig = item.GetItemConfig();
            _currentItem = item;
            _itemConfig = itemConfig;
            
        }

        public void Click()
        {
            if (_itemConfig.ItemType != ItemType.Rifle && _itemConfig.ItemType != ItemType.Gun) return;
            var amountToAdd = CalculateAmountToAdd(_itemConfig);
            if (amountToAdd == 0) return;
            _inventory.Store(_currentItem, amountToAdd);
            OnClick?.Invoke();
        }

        private int CalculateAmountToAdd(ItemConfig config)
        {
            var currentCount = _inventory.GetItemCount(config.ItemType);
            return Mathf.Max(0, config.MaxStackCount - currentCount);
        }

        private void OnDestroy()
        {
            _signalBus.Unsubscribe<ItemClickedSignal>(HandleRecievedItem);
        }
    }
}