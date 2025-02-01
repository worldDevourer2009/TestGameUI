using System;
using System.Collections.Generic;
using Components;
using Signals;
using UnityEngine;
using Zenject;
using Random = System.Random;

namespace Enemies
{
    public class EnemyController : IEnemyController, ITickable, IInitializable
    {
        public event Action<float> OnTakeDamage = delegate { };
        public event Action<float> OnAttack = delegate { };
        
        public float MaxHealth => _health.GetMaxHealth();
        public float CurrentHealth => _health.GetCurrentHealth();

        private readonly SignalBus _signalBus;
        private readonly IHealth _health;
        private readonly IAttack _attack;
        private readonly List<ItemType> _loot;

        public EnemyController(SignalBus signalBus, IHealth health, IAttack attack, List<ItemType> loot)
        {
            _signalBus = signalBus;
            _health = health;
            _attack = attack;
            _loot = loot;
        }
        
        public void Initialize()
        {
            _health.OnDeath += HandleDeath;
            _signalBus.Subscribe<PlayerFiredSignal>(OnDamage);
        }
        
        public void Tick()
        {
            if (Input.GetKeyDown(KeyCode.L))
                Attack();
        }

        private void OnDamage(PlayerFiredSignal evt)
        {
            var dmg = evt.Damage;
            if (dmg == 0)
            {
                Debug.Log($"Damage is {evt.Damage}");
                return;
            }
            
            Debug.Log($"Damage is {evt.Damage}");
            TakeDamage(dmg);
            Attack();
        }

        private void HandleDeath(bool isDead)
        {
            var randomLoot = GetRandomItemSafe();
            Debug.Log($"RandomLoot is {randomLoot}");
            _signalBus.Fire(new EnemyDeathSignal {IsDead = isDead, Loot = randomLoot});
        }
        
        private ItemType GetRandomItemSafe()
        {
            if (_loot.Count == 0) return default;

            var random = new Random();
            var randomIndex = random.Next(0, _loot.Count);
            return _loot[randomIndex];
        }


        public void IncreaseHealth(float hp)
        {
            _health.IncreaseHealth(hp);
        }

        public void TakeDamage(float damage)
        {
            _health.DecreaseHealthBody(damage);
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