using System;
using Models;
using Signals;
using UnityEngine;
using Zenject;

namespace Controllers
{
    public class EnemyUiController : IInitializable, IEnemyUIController, IDisposable
    {
        public event Action<float> TakenDamage;
        public event Action<float> Heal;

        private readonly SignalBus _signalBus;
        private IEnemyModel _enemyModel;
        
        private float _maxHealth;
        private float _currentHealth;

        public EnemyUiController(SignalBus signalBus)
        {
            _signalBus = signalBus;
        }

        public void Initialize()
        {
            _signalBus.Subscribe<EnemyModelSignal>(InitializeController);
            _signalBus.Subscribe<TakeDamageEnemySignal>(HandleDamage);
        }

        private void InitializeController(EnemyModelSignal modelSignal)
        {
            var model = modelSignal.EnemyModel;
            _enemyModel = model;
            
            _maxHealth = _enemyModel.GetMaxHealth();
            _currentHealth = _enemyModel.GetCurrentHealth();
            Heal?.Invoke(_currentHealth);
            Debug.Log("Init controller");
        }

        private void HandleDamage(TakeDamageEnemySignal evt)
        {
            _currentHealth = _enemyModel.GetCurrentHealth();
            var clampedValue = CalculateClamp(_currentHealth, _maxHealth);
            TakenDamage?.Invoke(clampedValue);
        }

        public void DecreaseEnemyHealth(float value)
        {
            _enemyModel.TakeDamage(value);
            TakenDamage?.Invoke(value);
        }

        public void IncreaseEnemyHealth(float value)
        {
            _enemyModel.IncreaseHealth(value);
            Heal?.Invoke(value);
        }

        public float GetMaxHealth()
        {
            return _enemyModel.GetMaxHealth();
        }

        public float GetCurrentHealth()
        {
            return _enemyModel.GetCurrentHealth();
        }

        public void Dispose()
        {
            _signalBus.TryUnsubscribe<EnemyModelSignal>(InitializeController);
            _signalBus.TryUnsubscribe<TakeDamageEnemySignal>(HandleDamage);
        }

        private float CalculateClamp(float current, float max)
        {
            return Mathf.Clamp(current / max, 0f, 1f);
        }
    }
}