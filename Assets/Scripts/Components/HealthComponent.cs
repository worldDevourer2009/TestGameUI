using System;
using SavesManagement;
using UnityEngine;
using Zenject;

namespace Components
{
    [ZenjectAllowDuringValidation]
    public class HealthComponent : IHealth, ISaveable
    {
        public string SaveId => "PlayerHealth";
        public event Action<bool> OnDeath = delegate  { };
        public event Action<float> OnHeal = delegate { };
        private readonly float _maxHealth;
        
        private float _currentHealth;
        private bool _isDead;

        public HealthComponent(float maxHealth)
        {
            _maxHealth = maxHealth;
            _currentHealth = maxHealth;
        }

        public void SetHealth(float hp)
        {
            _currentHealth = hp;
            Debug.Log($"set health to {_currentHealth}");
            
            _isDead = false;
            OnDeath?.Invoke(false);
        }

        public void IncreaseHealth(float hp)
        {
            _currentHealth += hp;
            Debug.Log($"increasing health to {_currentHealth}");
            
            if (_currentHealth <= _maxHealth)
            {
                OnHeal?.Invoke(_currentHealth);
                return;
            }
            
            _currentHealth = _maxHealth;
            OnHeal?.Invoke(_currentHealth);
        }

        public void DecreaseHealthHead(float hp)
        {
            _currentHealth -= hp * (1 + 40f / 100f);
            
            Debug.Log($"decreasing health head to {_currentHealth}");
            if (_currentHealth > 0) return; 
            _currentHealth = 0;
            
            _isDead = true;
            OnDeath?.Invoke(_isDead);
        }

        public void DecreaseHealthBody(float hp)
        {
            _currentHealth -= hp;
            
            Debug.Log($"decreasing health body to {_currentHealth}");
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
        
        public void SaveData(GameData gameData)
        {
            if (gameData == null) return;
            
            gameData.playerStatsData.health = _currentHealth;
        }

        public void LoadData(GameData gameData)
        {
            if (gameData == null) return;
            
            if (gameData.playerStatsData.health > 0)
            {
                SetHealth(gameData.playerStatsData.health);
            }
        }
    }
}