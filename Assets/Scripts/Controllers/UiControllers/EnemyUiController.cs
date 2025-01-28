using System;
using Zenject;

namespace Controllers
{
    public class EnemyUiController : IInitializable, IEnemyUIController, IDisposable
    {
        public event Action<float> TakenDamage;
        
        public void Initialize()
        {
            throw new NotImplementedException();
        }

        public void DecreaseEnemyHealth(float value)
        {
            throw new NotImplementedException();
        }

        public void IncreaseEnemyHealth(float value)
        {
            throw new NotImplementedException();
        }

        public void Dispose()
        {
            throw new NotImplementedException();
        }
    }
}