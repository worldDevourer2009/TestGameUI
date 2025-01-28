namespace Components
{
    public interface IArmor
    {
        void SetArmorHead(float armor);
        void SetArmorBody(float armor);
        float GetArmor();
        void IncreaseArmorHead(float armor);
        void DecreaseArmorHead(float armor);
        void IncreaseArmorBody(float armor);
        void DecreaseArmorBody(float armor);
    }
}