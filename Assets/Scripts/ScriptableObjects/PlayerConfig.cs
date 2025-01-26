using UnityEngine;

namespace ScriptableObjects
{
    [CreateAssetMenu(menuName = "Entities Configs/Player Config", fileName = "Player Config")]
    public class PlayerConfig : ScriptableObject
    {
        public float MaxPlayerHealth => maxHealth;
        public float CurrentPlayerHealth => currentHealth;
        
        [SerializeField] private float maxHealth;
        [SerializeField] private float currentHealth;
    }
}