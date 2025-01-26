namespace Models
{
    public interface IModel
    {
        void IncreaseHealth(float hp);
        void TakeDamage(float damage);
        void SetArmorHead(float armor);
        void SetArmorBody(float armor);
        float GetMaxHealth();
        float GetCurrentHealth();
    }
}