using System;
using Enemies;
using Signals;
using Zenject;

namespace Models
{
    public class EnemyModel : IEnemyModel, IInitializable, IDisposable
    {
        private readonly SignalBus _signalBus;
        private readonly IEnemyController _enemyController;

        public EnemyModel(SignalBus signalBus, IEnemyController enemyController)
        {
            _signalBus = signalBus;
            _enemyController = enemyController;
            
            _signalBus.Fire(new EnemyModelSignal { EnemyModel = this });
        }

        public void Initialize()
        {
            _enemyController.OnTakeDamage += TakeDamage;
            _enemyController.OnAttack += HandleAttack;
        }

        private void HandleAttack(float damage)
        {
            _signalBus.Fire(new EnemyAttackSignal { Damage = damage });
        }

        public void TakeDamage(float damage)
        {
            _signalBus.Fire(new TakeDamageEnemySignal { Damage = damage });
        }

        public float GetMaxHealth()
        {
            return _enemyController.MaxHealth;
        }

        public float GetCurrentHealth()
        {
            return _enemyController.CurrentHealth;
        }

        public void IncreaseHealth(float hp)
        {
            _enemyController.IncreaseHealth(hp);
        }

        public void Dispose()
        {
            _enemyController.OnTakeDamage -= TakeDamage;
            _enemyController.OnAttack -= HandleAttack;
        }
    }
}