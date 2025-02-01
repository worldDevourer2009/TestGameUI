using System;

namespace Controllers
{
    public interface IEnemyUIController
    {
        event Action<float> TakenDamage;
        event Action<float> Heal;
        
        //To scale project
        void DecreaseEnemyHealth(float value);
        void IncreaseEnemyHealth(float value);
        float GetMaxHealth();
        float GetCurrentHealth();
    }
}