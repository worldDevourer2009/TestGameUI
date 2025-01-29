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
        }

        private void InitializeController(EnemyModelSignal modelSignal)
        {
            var model = modelSignal.EnemyModel;
            if (model == null) return;

            _enemyModel = model;
            
            _maxHealth = _enemyModel.GetMaxHealth();
            _currentHealth = _enemyModel.GetCurrentHealth();
            
            _signalBus.Subscribe<TakeDamageEnemySignal>(HandleDamage);
            _signalBus.Subscribe<EnemyAttackSignal>(HandleAttack);
        }

        private void HandleDamage(TakeDamageEnemySignal evt)
        {
            _currentHealth = _enemyModel.GetCurrentHealth();
            var clampedValue = CalculateClamp(_currentHealth, _maxHealth);
            TakenDamage?.Invoke(clampedValue);
        }

        private void HandleAttack(EnemyAttackSignal evt)
        {
            //_signalBus.Fire(new EnemyAttackSignal() {Damage =  });
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
            _signalBus.TryUnsubscribe<TakeDamageEnemySignal>(HandleDamage);
            _signalBus.TryUnsubscribe<EnemyAttackSignal>(HandleAttack);
        }

        private float CalculateClamp(float current, float max)
        {
            return Mathf.Clamp(current / max, 0f, 1f);
        }
    }
}