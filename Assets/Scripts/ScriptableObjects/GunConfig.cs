using Components;
using UnityEngine;

namespace ScriptableObjects
{
    [CreateAssetMenu(menuName = "Items Configs/Gun Config", fileName = "Gun Config")]
    public class GunConfig : ScriptableObject
    {
        public GunType GunType => gunType;
        public float GunDamage => gunDamage;
        public int Consumable => consumable;
        
        [SerializeField] private GunType gunType;
        [SerializeField] private float gunDamage;
        [SerializeField] private int consumable;
        
    }
}