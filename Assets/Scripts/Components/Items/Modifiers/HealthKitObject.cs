using ScriptableObjects;
using UnityEngine;
using UnityEngine.UI;
using Zenject;

namespace Components.Modifiers
{
    public class HealthKitObject : StorableObjectComponent, IInitializable
    {
        public ModifierConfig Config => modifierConfig;
        [SerializeField] private ModifierConfig modifierConfig;
        public ModifierType BulletType => modifierConfig.ModifierType;
        private Image _pistolAmmoImage;
        
        [Inject]
        public void Construct()
        {
            _pistolAmmoImage = GetComponent<Image>();
        }

        public void Initialize()
        {
            InitializeImage();
        }
        
        protected override void InitializeImage()
        {
            var image = modifierConfig.ModifierImage;
            _pistolAmmoImage.sprite = image;
        }
    }
}