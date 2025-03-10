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
            var amountToAdd = _itemConfig.MaxStackCount;
            if (amountToAdd == 0)
            {
                OnClick?.Invoke();
                return;
            }
            
            OnClick?.Invoke();
            _inventory.Store(_currentItem, amountToAdd);
        }

        private void OnDestroy()
        {
            _signalBus.TryUnsubscribe<ItemClickedSignal>(HandleRecievedItem);
        }
    }
}