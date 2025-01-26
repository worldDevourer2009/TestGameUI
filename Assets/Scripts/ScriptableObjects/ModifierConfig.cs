using Components;
using UnityEngine;

namespace ScriptableObjects
{
    [CreateAssetMenu(menuName = "Items Configs/Modifier Config", fileName = "Modifier Config")]
    public class ModifierConfig : ScriptableObject
    {
        public ModifierType ModifierType => type;
        public float StatModifierValue => statModifierValue;
        public int MaxStackAmount => maxStackAmount;
        public float Weight => weight;
        
        [SerializeField] private ModifierType type;
        [SerializeField] private float statModifierValue;
        [SerializeField] private int maxStackAmount;
        [SerializeField] private float weight;
    }
}