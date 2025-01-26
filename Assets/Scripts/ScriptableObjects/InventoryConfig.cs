using UnityEngine;

namespace ScriptableObjects
{
    [CreateAssetMenu(menuName = "Inventory Config", fileName = "Inventory Config")]
    public class InventoryConfig : ScriptableObject
    {
        public int InventorySize => inventorySize;
        public float MaxCaryWeight => maxCaryWeight;
        
        [SerializeField] private float maxCaryWeight;
        [SerializeField] private int inventorySize = 30;
    }
}