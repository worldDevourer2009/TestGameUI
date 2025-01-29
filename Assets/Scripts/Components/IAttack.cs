using System;

namespace Components
{
    public interface IAttack
    {
        float GetDamage();
    }
}


namespace Components
{
    public class AttackComponent : IAttack
    {
        private readonly float _attackDamage;

        public AttackComponent(float attackDamage)
        {
            _attackDamage = attackDamage;
        }

        public float GetDamage()
        {
            return _attackDamage;
        }
    }
}