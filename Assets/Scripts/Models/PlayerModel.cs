using System;
using Player;
using Signals;
using UnityEngine;
using Zenject;

namespace Models
{
    public class PlayerModel : IPlayerModel, IInitializable, IDisposable
    {
        private readonly SignalBus _signalBus;
        private readonly IPlayerController _playerController;

        public PlayerModel(SignalBus signalBus, IPlayerController playerController)
        {
            _signalBus = signalBus;
            _playerController = playerController;
            
            _signalBus.Fire(new ModelServiceSignal {PlayerPlayerModel = this});
        }

        public void Initialize()
        {
            _playerController.Damage += HandleDamage;
            _playerController.Heal += HandleHeal;
            _playerController.Armor += HandleArmor;
            
            Debug.Log("Player Model");
        }

        public void IncreaseHealth(float hp)
        {
            _playerController.IncreaseHealth(hp);
        }

        public void TakeDamage(float damage)
        {
            _playerController.TakeDamage(damage);
        }

        public void SetArmorHead(float armor)
        {
            _playerController.SetArmorHead(armor);
        }

        public void SetArmorBody(float armor)
        {
            _playerController.SetArmorBody(armor);
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
            return _playerController.MaxHealth;
        }

        public float GetCurrentHealth()
        {
            return _playerController.CurrentHealth;
        }

        public void Dispose()
        {
            _playerController.Damage -= HandleDamage;
            _playerController.Heal -= HandleHeal;
            _playerController.Armor -= HandleArmor;
        }
    }
}