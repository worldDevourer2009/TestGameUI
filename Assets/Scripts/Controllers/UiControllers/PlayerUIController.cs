using System;
using Models;
using Signals;
using UiElements;
using UnityEngine;
using Zenject;

namespace Controllers
{
    public class PlayerUIController : IInitializable, IPlayerUIController, IDisposable
    {
        public event Action<float> TakenDamage;
        public event Action<float> Heal;
        public event Action<float> Armor;

        private readonly SignalBus _signalBus;
        
        private IPlayerModel _playerModel;
        private float _maxHealth;
        private float _currentHealth;

        public PlayerUIController(SignalBus signalBus)
        {
            _signalBus = signalBus;
        }

        public void Initialize()
        {
            _signalBus.Subscribe<ModelServiceSignal>(InitializeController);
            _signalBus.Subscribe<ArmorAddedSignal>(HandleArmor);
        }

        private void HandleArmor(ArmorAddedSignal evt)
        {
            var armorType = evt.ArmorType;
            var value = evt.Armor;
            
            Debug.Log($"Controller recieved {armorType}");
            
            if (armorType == default || _playerModel == null) return;
            
            switch (armorType)
            {
                case ArmorSlotType.Body:
                    _playerModel.SetArmorBody(value);
                    break;
                case ArmorSlotType.Head:
                    _playerModel.SetArmorHead(value);
                    break;
                default:
                    break;
            }
        }

        private void InitializeController(ModelServiceSignal modelSignal)
        {
            var model = modelSignal.PlayerPlayerModel;
            if (model == null) return;
            _playerModel = model;
            
            _maxHealth = _playerModel.GetMaxHealth();
            _currentHealth = _playerModel.GetCurrentHealth();
            
            _signalBus.Subscribe<HealPlayerSignal>(HandleHeal);
            _signalBus.Subscribe<TakeDamagePlayerSignal>(HandleDamage);
            _signalBus.Subscribe<ArmorPlayerSignal>(HandleArmor);
        }
        
        public void DecreasePlayerHealth(float value)
        {
            _playerModel.TakeDamage(value);

            _currentHealth = _playerModel.GetCurrentHealth();
            var clampedValue = CalculateClamp(_currentHealth, _maxHealth);
            TakenDamage?.Invoke(clampedValue);
        }

        public void IncreasePlayerHealth(float value)
        {
            _playerModel.IncreaseHealth(value);
            
            _currentHealth = _playerModel.GetCurrentHealth();
            var clampedValue = CalculateClamp(_currentHealth, _maxHealth);
            TakenDamage?.Invoke(clampedValue);
        }

        public void SetPlayerHeadArmor(float armor)
        {
            _playerModel.SetArmorHead(armor);
            Armor?.Invoke(armor);
        }

        public void SetPlayerBodyArmor(float armor)
        {
            _playerModel.SetArmorBody(armor);
            Armor?.Invoke(armor);
        }

        private void HandleArmor(ArmorPlayerSignal evt)
        {
            var armor = evt.Armor;
            Armor?.Invoke(armor);
        }

        private void HandleHeal(HealPlayerSignal evt)
        {
            _currentHealth = _playerModel.GetCurrentHealth();
            var clampedValue = CalculateClamp(_currentHealth, _maxHealth);
            
            Heal?.Invoke(clampedValue);
        }

        private void HandleDamage(TakeDamagePlayerSignal amount)
        {
            _currentHealth = _playerModel.GetCurrentHealth();
            var clampedValue = CalculateClamp(_currentHealth, _maxHealth);
            
            TakenDamage?.Invoke(clampedValue);
        }

        public void Dispose()
        {
            _signalBus.TryUnsubscribe<HealPlayerSignal>(HandleHeal);
            _signalBus.TryUnsubscribe<TakeDamagePlayerSignal>(HandleDamage);
            _signalBus.TryUnsubscribe<ArmorPlayerSignal>(HandleArmor);
        }

        private float CalculateClamp(float current, float max)
        {
            return Mathf.Clamp(current/max, 0f, 1f);
        }
    }
}