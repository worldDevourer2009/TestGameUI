using System;

namespace Components
{
    public interface IArmor
    {
        event Action<float> OnChangeArmor;
        void SetArmor(float armor);
        float GetArmor();
        void IncreaseArmor(float armor);
        void DecreaseArmor(float armor);
    }
}