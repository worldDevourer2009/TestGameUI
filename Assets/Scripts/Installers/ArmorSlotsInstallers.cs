using UiElements;
using UnityEngine;
using Zenject;

namespace Installers
{
    public class ArmorSlotsInstallers : MonoInstaller<ArmorSlotsInstallers>
    {
        [SerializeField] private EquipeButton equipeButton;
        
        public override void InstallBindings()
        {
            Container
                .BindInterfacesTo<EquipeButton>()
                .FromInstance(equipeButton)
                .AsSingle();
            
            Container
                .Bind<IArmorSlot>()
                .To<ArmorSlot>()
                .FromComponentsInHierarchy()
                .AsCached();
        }
    }
}