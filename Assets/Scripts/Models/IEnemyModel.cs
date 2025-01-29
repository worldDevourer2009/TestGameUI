namespace Models
{
    public interface IEnemyModel
    {
        void TakeDamage(float damage);
        float GetMaxHealth();
        float GetCurrentHealth();
        void IncreaseHealth(float hp);
    }
}