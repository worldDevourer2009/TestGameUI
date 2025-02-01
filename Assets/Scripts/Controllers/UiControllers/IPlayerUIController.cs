using System;

namespace Controllers
{
    public interface IPlayerUIController
    {
        event Action<float> TakenDamage;
        event Action<float> Heal;
        event Action<float> Armor;

        
        //to Scale project
        void DecreasePlayerHealth(float value);
        void IncreasePlayerHealth(float value);
        void SetPlayerHeadArmor(float value);
        void SetPlayerBodyArmor(float value);
    }
}