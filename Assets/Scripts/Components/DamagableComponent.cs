using System;
using UnityEngine;
using Random = UnityEngine.Random;

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
            var rand = Random.Range(0, 2);
            
            if (rand == 0)
            {
                var entityArmorBody = _armor.GetArmorBody() * 0.01f;
                var afterMathDamage = damage - damage * entityArmorBody / 100;
                
                Debug.Log($"Entity armor and receaving armor {entityArmorBody} " +
                          $"and  damage {afterMathDamage} and recieved damage {damage}");
                
                _health.DecreaseHealthBody(afterMathDamage);
            }
            else
            {
                var entityArmorBody = _armor.GetArmorHead() * 0.01f;
                var afterMathDamage = damage - damage * entityArmorBody / 100;
                
                Debug.Log($"Entity armor and receaving armor {entityArmorBody} " +
                          $"and  damage {afterMathDamage} and recieved damage {damage}");
                
                _health.DecreaseHealthHead(afterMathDamage);
            }
            
            var newHp = _health.GetMaxHealth();
            
            OnTakenDamage?.Invoke(newHp);
        }
    }
}