namespace Models
{
    public interface IPlayerModel
    {
        void IncreaseHealth(float hp);
        void TakeDamage(float damage);
        void SetArmorHead(float armor);
        void SetArmorBody(float armor);
        float GetMaxHealth();
        float GetCurrentHealth();
    }
}