using Components;
using UnityEngine;

namespace ScriptableObjects
{
    [CreateAssetMenu(menuName = "Items Configs/Modifier Config", fileName = "Modifier Config")]
    public class ModifierConfig : ScriptableObject, IConfigInventoryItem
    {
        public Sprite ModifierImage
        {
            get => modifierImage;
            set => modifierImage = value;
        }
        
        public ModifierType ModifierType => type;
        public float StatModifierValue => statModifierValue;
        public int MaxStackCount => maxStackCount;
        public float Weight => weight;
        
        [SerializeField] private Sprite modifierImage;
        [SerializeField] private ModifierType type;
        [SerializeField] private float statModifierValue;
        [SerializeField] private int maxStackCount;
        [SerializeField] private float weight;
    }
}

public interface IConfigInventoryItem {}