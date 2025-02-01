using System;
using SavesManagement;

namespace Components
{
    public class ArmorComponent : IArmor, ISaveable
    {
        public string SaveId => "PlayerArmor";
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

        public float GetArmorBody()
        {
            return _currentArmorBody;
        }

        public float GetArmorHead()
        {
            return _currentArmorHead;
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
        
        public void SaveData(GameData gameData)
        {
            gameData.playerStatsData.armorHead = _currentArmorHead;
            gameData.playerStatsData.armorBody = _currentArmorBody;
        }

        public void LoadData(GameData gameData)
        {
            SetArmorHead(gameData.playerStatsData.armorHead);
            SetArmorBody(gameData.playerStatsData.armorBody);
        }
    }
}