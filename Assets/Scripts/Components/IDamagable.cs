using System;

namespace Components
{
    public interface IDamagable
    {
        event Action<float> OnTakenDamage;
        void TakeDamage(float damage);
    }
}