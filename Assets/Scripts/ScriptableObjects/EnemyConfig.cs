using UnityEngine;

namespace ScriptableObjects
{
    [CreateAssetMenu(menuName = "Entities Configs/Enemy Config", fileName = "Enemy Config")]
    public class EnemyConfig : ScriptableObject
    {
        public float MaxHealth => maxHealth;
        public float CurrentHealth => currentHealth;
        
        [SerializeField] private float maxHealth;
        [SerializeField] private float currentHealth;
    }
}