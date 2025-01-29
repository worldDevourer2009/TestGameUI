using System;
using System.Collections.Generic;
using System.Linq;
using ScriptableObjects;
using Signals;
using UiElements;
using UnityEngine;
using Zenject;

namespace Components
{
    public class ShootButtonComponent : MonoBehaviour, IClick
    {
        [SerializeField] private List<GunConfig> gunConfig;
        public event Action OnClick;
        
        private SignalBus _signalBus;
        private IInventory _inventory;
        private ItemConfig _currentConfig;
        private GunConfig _currentGunConfig;

        [Inject]
        public void Construct(SignalBus signalBus, IInventory inventory)
        {
            _signalBus = signalBus;
            _inventory = inventory;
        }

        private void Start()
        {
            _signalBus.Subscribe<SelectedGunSignal>(HandleGunDamage);
        }

        private void HandleGunDamage(SelectedGunSignal evt)
        {
            var gun = evt.Type;
            _currentConfig = gun;
            SetGunConfig(_currentConfig.ItemType);
        }

        public void Click()
        {
            if (_currentConfig == null && _currentGunConfig == null) return;

            //_inventory.RemoveItem(_currentConfig., _currentGunConfig.Consumable);
            
            _signalBus.Fire(new PlayerFiredSignal());
        }

        private void SetGunConfig(ItemType type)
        {
            if (type == ItemType.Gun)
                _currentGunConfig = gunConfig.FirstOrDefault(x => x.GunType == GunType.Gun);
            
            if (type == ItemType.Rifle)
                _currentGunConfig = gunConfig.FirstOrDefault(x => x.GunType == GunType.Rifle);
        }
    }
}