using System;

namespace Components
{
    public class ArmorComponent : IArmor
    {
        public event Action<float> OnChangeArmor = delegate { };
        private float _currentArmorHead;
        private float _currentArmorBody;

        public ArmorComponent()
        {
        }

        public void SetArmorHead(float armor)
        {
            _currentArmorHead = armor;
            OnChangeArmor?.Invoke(_currentArmorHead);
        }

        public void SetArmorBody(float armor)
        {
            _currentArmorBody = armor;
            OnChangeArmor?.Invoke(_currentArmorBody);
        }
        
        public float GetArmor()
        {
            return _currentArmorBody + _currentArmorHead;
        }

        public void IncreaseArmorHead(float armor)
        {
            _currentArmorHead = armor;
        }

        public void DecreaseArmorHead(float armor)
        {
            _currentArmorHead = armor;
        }

        public void IncreaseArmorBody(float armor)
        {
            _currentArmorBody = armor;
        }

        public void DecreaseArmorBody(float armor)
        {
            _currentArmorBody = armor;
        }
    }
}