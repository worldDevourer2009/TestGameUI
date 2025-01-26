using System;
using UnityEngine;

namespace Components
{
    public class DamagableComponent : IDamagable
    {
        public event Action<float> OnTakenDamage = delegate { };
        
        private readonly IArmor _armor;
        private readonly IHealth _health;

        public DamagableComponent(IArmor armor, IHealth health)
        {
            _armor = armor;
            _health = health;
        }

        public void TakeDamage(float damage)
        {
            var entityArmor = _armor.GetArmor() * 0.01f;
            var afterMathDamage = damage - damage * entityArmor / 100;
            
            Debug.Log($"Entity armor and receaving armor {entityArmor} " +
                      $"and  damage {afterMathDamage} and recieved damage {damage}");
            
            _health.DecreaseHealth(afterMathDamage);
            var newHp = _health.GetMaxHealth();
            
            OnTakenDamage?.Invoke(newHp);
        }
    }
}