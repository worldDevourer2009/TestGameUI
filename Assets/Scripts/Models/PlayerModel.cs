using System;
using Components;
using Signals;
using Zenject;

namespace Models
{
    public class PlayerModel : IModel, IInitializable, IDisposable
    {
        private readonly SignalBus _signalBus;
        private readonly IDamagable _damagable;
        private readonly IHealth _health;
        private readonly IArmor _armor;

        public PlayerModel(SignalBus signalBus, IDamagable damagable, IHealth health, IArmor armor)
        {
            _signalBus = signalBus;
            _damagable = damagable;
            _health = health;
            _armor = armor;
        }

        public void Initialize()
        {
            _signalBus.Fire(new ModelServiceSignal {PlayerModel = this});
            
            _damagable.OnTakenDamage += HandleDamage;
            _health.OnHeal += HandleHeal;
            _armor.OnChangeArmor += HandleArmor;
        }

        public void IncreaseHealth(float hp)
        {
            _health.IncreaseHealth(hp);
        }

        public void TakeDamage(float damage)
        {
            _damagable.TakeDamage(damage);
        }

        public void SetArmorHead(float armor)
        {
            _armor.IncreaseArmor(armor);
        }

        public void SetArmorBody(float armor)
        {
            _armor.IncreaseArmor(armor);
        }

        private void HandleArmor(float amount)
        {
            _signalBus.Fire(new ArmorPlayerSignal {Armor = amount});
        }

        private void HandleHeal(float amount)
        {
            _signalBus.Fire(new HealPlayerSignal {Heal = amount});
        }

        private void HandleDamage(float amount)
        {
            _signalBus.Fire(new TakeDamagePlayerSignal {Damage = amount});
        }
        
        public float GetMaxHealth()
        {
            return _health.GetMaxHealth();
        }

        public float GetCurrentHealth()
        {
            return _health.GetCurrentHealth();
        }

        public void Dispose()
        {
            _damagable.OnTakenDamage -= HandleDamage;
            _health.OnHeal -= HandleHeal;
            _armor.OnChangeArmor -= HandleArmor;
        }
    }
}