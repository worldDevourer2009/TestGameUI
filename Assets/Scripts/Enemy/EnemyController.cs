using System;
using Components;
using Signals;
using Zenject;

namespace Enemies
{
    public class EnemyController : IEnemyController, IInitializable
    {
        public event Action<float> OnTakeDamage = delegate { };
        public event Action<float> OnAttack = delegate { };
        
        public float MaxHealth => _health.GetMaxHealth();
        public float CurrentHealth => _health.GetCurrentHealth();

        private readonly SignalBus _signalBus;
        private readonly IHealth _health;
        private readonly IAttack _attack;

        public EnemyController(SignalBus signalBus, IHealth health, IAttack attack)
        {
            _signalBus = signalBus;
            _health = health;
            _attack = attack;
        }
        
        public void Initialize()
        {
            _health.OnDeath += HandleDeath;
        }

        private void HandleDeath(bool isDead)
        {
            _signalBus.Fire(new EnemyDeathSignal {IsDead = isDead});
        }

        public void IncreaseHealth(float hp)
        {
            _health.IncreaseHealth(hp);
        }

        public void TakeDamage(float damage)
        {
            _health.DecreaseHealth(damage);
            OnTakeDamage?.Invoke(CurrentHealth);
        }

        public void Attack()
        {
            var dmg = _attack.GetDamage();
            OnAttack?.Invoke(dmg);
        }
    }
}

namespace Enemies
{
    public interface IEnemyController
    {
        public float MaxHealth { get; }
        public float CurrentHealth { get; }
        
        event Action<float> OnTakeDamage;
        event Action<float> OnAttack;
        
        void IncreaseHealth(float hp);
        void TakeDamage(float damage);
        void Attack();
    }
}