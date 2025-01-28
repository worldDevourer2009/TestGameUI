using UnityEngine;

namespace ScriptableObjects
{
    [CreateAssetMenu(menuName = "Items Configs/Item Config Draggable", fileName = "Item Config")]
    public class ItemConfig : ScriptableObject
    {
        public Sprite ItemImage
        {
            get => itemImage;
            set => itemImage = value;
        }
        
        public ItemType ItemType => type;
        public float ItemModifierValue => itemModifierValue;
        public int MaxStackCount => maxStackCount;
        public float Weight => weight;
        public bool IsStackable => stackable;
        
        [SerializeField] private Sprite itemImage;
        [SerializeField] private ItemType type;
        [SerializeField] private bool stackable;
        [SerializeField] private float itemModifierValue;
        [SerializeField] private int maxStackCount;
        [SerializeField] private float weight;
    }
}