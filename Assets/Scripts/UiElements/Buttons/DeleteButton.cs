using System;
using Components;
using Signals;
using UnityEngine;
using Zenject;

namespace UiElements
{
    public class DeleteButton : MonoBehaviour, IClick, IInitializable
    {
        public event Action OnClick;
        
        private SignalBus _signalBus;
        private IInventory _inventory;
        
        private InventoryItem _currentItem;

        [Inject]
        public void Construct(SignalBus signalBus, IInventory inventory)
        {
            _signalBus = signalBus;
            _inventory = inventory;
        }

        public void Initialize()
        {
            _signalBus.Subscribe<ItemClickedSignal>(HandleRecievedItem);
        }
        
        public void Click()
        {
            Debug.Log("Deleted");
            _inventory.RemoveItem(_currentItem, _currentItem.Count); 
            OnClick?.Invoke();
        }


        private void HandleRecievedItem(ItemClickedSignal evt)
        {
            var item = evt.Item;
            _currentItem = item;
        }
    }
}