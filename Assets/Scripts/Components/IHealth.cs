using System;

namespace Components
{
    public interface IHealth
    {
        event Action<bool> OnDeath;
        event Action<float> OnHeal;

        void SetHealth(float hp);
        void IncreaseHealth(float hp);
        void DecreaseHealth(float hp);
        float GetMaxHealth();
        float GetCurrentHealth();
    }
}