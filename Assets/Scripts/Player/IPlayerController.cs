using System;

namespace Player
{
    public interface IPlayerController
    {
        public float MaxHealth { get; }
        public float CurrentHealth { get; }
        
        event Action<float> Damage;
        event Action<float> Heal;
        event Action<float> Armor;
        
        void IncreaseHealth(float hp);
        void TakeDamage(float damage);
        void SetArmorHead(float armor);
        void SetArmorBody(float armor);
    }
}