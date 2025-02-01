using System;

namespace Components
{
    public interface IHealth
    {
        event Action<bool> OnDeath;
        void IncreaseHealth(float hp);
        void DecreaseHealthHead(float hp);
        void DecreaseHealthBody(float hp);
        float GetMaxHealth();
        float GetCurrentHealth();
    }
}