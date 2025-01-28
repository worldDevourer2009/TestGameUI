using System;

namespace Controllers
{
    public interface IEnemyUIController
    {
        event Action<float> TakenDamage;
        void DecreaseEnemyHealth(float value);
        void IncreaseEnemyHealth(float value);
    }
}