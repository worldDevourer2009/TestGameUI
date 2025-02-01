namespace Components
{
    public interface IArmor
    {
        float GetArmorBody();
        float GetArmorHead();
        void IncreaseArmorHead(float armor);
        void IncreaseArmorBody(float armor);
    }
}