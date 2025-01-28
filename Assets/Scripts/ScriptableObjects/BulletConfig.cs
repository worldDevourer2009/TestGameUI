using Components;
using UnityEngine;

namespace ScriptableObjects
{
    [CreateAssetMenu(menuName = "Items Configs/Bullet Config", fileName = "Bullet Config")]
    public class BulletConfig : ScriptableObject
    {
        public Sprite BulletImage
        {
            get => bulletImage;
            set => bulletImage = value;
        }
        
        public BulletType BulletType => bulletType;
        public float BulletWeight => weight;
        public int MaxStuckCount => maxStackCount;

        [SerializeField] private Sprite bulletImage;
        [SerializeField] private BulletType bulletType;
        [SerializeField] private int maxStackCount;
        [SerializeField] private float weight;
    }
}