using System;
using Zenject;

namespace Models
{
    public class EnemyModel : IEnemyModel, IInitializable, IDisposable
    {
        private readonly SignalBus _signalBus;

        public EnemyModel(SignalBus signalBus)
        {
            _signalBus = signalBus;
        }

        public void TakeDamage(float damage)
        {
        }

        public float GetMaxHealth()
        {
            return default;
        }

        public float GetCurrentHealth()
        {
            return default;
        }

        public void Initialize()
        {
        }

        public void Dispose()
        {
        }
    }
}