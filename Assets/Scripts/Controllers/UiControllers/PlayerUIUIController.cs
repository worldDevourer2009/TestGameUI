using System;
using Models;
using Signals;
using UnityEngine;
using Zenject;

namespace Controllers
{
    public class PlayerUIUIController : IInitializable, IPlayerUIController, IDisposable
    {
        public event Action<float> TakenDamage;
        public event Action<float> Heal;
        public event Action<float> Armor;

        private readonly SignalBus _signalBus;
        
        private IModel _model;
        private float _maxHealth;
        private float _currentHealth;

        public PlayerUIUIController(SignalBus signalBus)
        {
            _signalBus = signalBus;
        }

        public void Initialize()
        {
            _signalBus.Subscribe<ModelServiceSignal>(InitializeController);
        }

        private void InitializeController(ModelServiceSignal modelSignal)
        {
            var model = modelSignal.PlayerModel;
            if (model == null) return;
            
            _model = model;
            _maxHealth = _model.GetMaxHealth();
            _currentHealth = _model.GetCurrentHealth();
            
            _signalBus.Subscribe<HealPlayerSignal>(HandleHeal);
            _signalBus.Subscribe<TakeDamagePlayerSignal>(HandleDamage);
            _signalBus.Subscribe<ArmorPlayerSignal>(HandleArmor);
        }
        
        public void DecreasePlayerHealth(float value)
        {
            _model.TakeDamage(value);

            _currentHealth = _model.GetCurrentHealth();
            var clampedValue = CalculateClamp(_currentHealth, _maxHealth);
            TakenDamage?.Invoke(clampedValue);
        }

        public void IncreasePlayerHealth(float value)
        {
            _model.IncreaseHealth(value);
            
            _currentHealth = _model.GetCurrentHealth();
            var clampedValue = CalculateClamp(_currentHealth, _maxHealth);
            TakenDamage?.Invoke(clampedValue);
        }

        public void SetPlayerHeadArmor(float armor)
        {
            _model.SetArmorHead(armor);
            Armor?.Invoke(armor);
        }

        public void SetPlayerBodyArmor(float armor)
        {
            _model.SetArmorBody(armor);
            Armor?.Invoke(armor);
        }

        private void HandleArmor(ArmorPlayerSignal evt)
        {
            var armor = evt.Armor;
            Armor?.Invoke(armor);
        }

        private void HandleHeal(HealPlayerSignal evt)
        {
            _currentHealth = _model.GetCurrentHealth();
            var clampedValue = CalculateClamp(_currentHealth, _maxHealth);
            
            Heal?.Invoke(clampedValue);
        }

        private void HandleDamage(TakeDamagePlayerSignal amount)
        {
            _currentHealth = _model.GetCurrentHealth();
            var clampedValue = CalculateClamp(_currentHealth, _maxHealth);
            
            TakenDamage?.Invoke(clampedValue);
        }

        public void Dispose()
        {
            _signalBus.Unsubscribe<HealPlayerSignal>(HandleHeal);
            _signalBus.Unsubscribe<TakeDamagePlayerSignal>(HandleDamage);
            _signalBus.Unsubscribe<ArmorPlayerSignal>(HandleArmor);
        }

        private float CalculateClamp(float current, float max)
        {
            return Mathf.Clamp(current/max, 0f, 1f);
        }
    }
}