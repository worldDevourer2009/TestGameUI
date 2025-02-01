namespace Components
{
    public interface IArmor
    {
        void SetArmorHead(float armor);
        void SetArmorBody(float armor);
        float GetArmorBody();
        float GetArmorHead();
        void IncreaseArmorHead(float armor);
        void DecreaseArmorHead(float armor);
        void IncreaseArmorBody(float armor);
        void DecreaseArmorBody(float armor);
    }
}