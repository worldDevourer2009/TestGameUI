using System;
using UnityEngine;

namespace Components
{
    public class HealthComponent : IHealth
    {
        public event Action<bool> OnDeath = delegate  { };
        public event Action<float> OnHeal = delegate { };
        private readonly float _maxHealth;
        
        private float _currentHealth;
        private bool _isDead;

        public HealthComponent(float maxHealth)
        {
            _maxHealth = maxHealth;
        }

        public void SetHealth(float hp)
        {
            _currentHealth = hp;
            Debug.Log($"Setting health to {_currentHealth}");
            
            _isDead = false;
            OnDeath?.Invoke(false);
        }

        public void IncreaseHealth(float hp)
        {
            _currentHealth += hp;
            Debug.Log($"Increasing health to {_currentHealth}");
            
            if (_currentHealth <= _maxHealth)
            {
                OnHeal?.Invoke(_currentHealth);
                return;
            }
            
            _currentHealth = _maxHealth;
            OnHeal?.Invoke(_currentHealth);
        }

        public void DecreaseHealth(float hp)
        {
            _currentHealth -= hp;
            
            Debug.Log($"Decreasing health to {_currentHealth}");
            if (_currentHealth > 0) return; 
            _currentHealth = 0;
            
            _isDead = true;
            OnDeath?.Invoke(_isDead);
        }
        
        public float GetMaxHealth()
        {
            return _maxHealth;
        }

        public float GetCurrentHealth()
        {
            return _currentHealth;
        }
    }
}