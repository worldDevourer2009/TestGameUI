using System;

namespace Components
{
    public class ArmorComponent : IArmor
    {
        public event Action<float> OnChangeArmor = delegate { };
        private float _currentArmor;

        public ArmorComponent()
        {
        }

        public void SetArmor(float armor)
        {
            _currentArmor = armor;
            OnChangeArmor?.Invoke(_currentArmor);
        }

        public void IncreaseArmor(float armor)
        {
            _currentArmor += armor;
            OnChangeArmor?.Invoke(_currentArmor);
        }

        public void DecreaseArmor(float armor)
        {
            _currentArmor -= armor;
            if (_currentArmor < 0)
            {
                _currentArmor = 0;
                OnChangeArmor?.Invoke(_currentArmor);
                return;
            }
            
            OnChangeArmor?.Invoke(_currentArmor);
        }
        
        public float GetArmor()
        {
            return _currentArmor;
        }
    }
}