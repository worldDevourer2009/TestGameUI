using System;
using Components;
using Signals;
using UnityEngine;
using Zenject;

namespace Player
{
    public class PlayerController : IPlayerController, IInitializable
    {
        public float MaxHealth => _health.GetMaxHealth();
        public float CurrentHealth => _health.GetCurrentHealth();
        
        public event Action<float> Damage = delegate { };
        public event Action<float> Heal = delegate { };
        public event Action<float> Armor = delegate { };

        private readonly SignalBus _signalBus;
        private readonly IDamagable _damagable;
        private readonly IHealth _health;
        private readonly IArmor _armor;

        public PlayerController(SignalBus signalBus, IDamagable damagable, IHealth health, IArmor armor)
        {
            _signalBus = signalBus;
            _damagable = damagable;
            _health = health;
            _armor = armor;
        }
        
        public void Initialize()
        {
            _health.OnDeath += HandleDeath;
        }

        private void HandleDeath(bool isDead)
        {
            _signalBus.Fire(new PlayerDeathSignal {IsDead = isDead});
        }

        public void IncreaseHealth(float hp)
        {
            Debug.Log($"Increasing player healh to {hp}");
            _health.IncreaseHealth(hp);
        }

        public void TakeDamage(float damage)
        {
            Debug.Log($"Decreasing player healh up to {damage}");
            _damagable.TakeDamage(damage);
        }

        public void SetArmorHead(float armor)
        {
            Debug.Log($"Setting player armor head up to {armor}");
            _armor.IncreaseArmorHead(armor);
            Debug.Log($"Current armor is {_armor.GetArmor()}");
        }

        public void SetArmorBody(float armor)
        {
            Debug.Log($"Setting player armor body up to {armor}");
            _armor.IncreaseArmorBody(armor);
            Debug.Log($"Current armor is {_armor.GetArmor()}");
        }
    }
}