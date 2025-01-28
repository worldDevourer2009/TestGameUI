using Components;
using Player;
using ScriptableObjects;
using Signals;
using System;
using UnityEngine;
using Zenject;

namespace UiElements
{
    public class UseHealthKitButton : MonoBehaviour, IClick, IInitializable
    {
        public event Action OnClick = delegate { };
        private ItemConfig _itemConfig;
        private InventoryItem _currentItem;
        
        private SignalBus _signalBus;
        private IPlayerController _playerController;
        private IInventory _inventory;

        [Inject]
        public void Construct(SignalBus signalBus, IInventory inventory, IPlayerController playerController)
        {
            _signalBus = signalBus;
            _inventory = inventory;
            _playerController = playerController;
        }
        
        public void Initialize()
        {
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
            if (_itemConfig.ItemType != ItemType.HealthKit) return;
            _playerController.IncreaseHealth(_itemConfig.ItemModifierValue);
            _inventory.RemoveItem(_currentItem);
            OnClick?.Invoke();
        }
    }
}