using UiElements;
using UnityEngine;
using Zenject;

namespace Installers
{
    public class PopUpInstaller : MonoInstaller<PopUpInstaller>
    {
        [SerializeField] private PopUpComponent popUpComponent;
        [SerializeField] private BuyButton buyButton;
        [SerializeField] private UseHealthKitButton useHealthKitButton;
        [SerializeField] private DeleteButton deleteButton;

        public override void InstallBindings()
        {
            Container
                .BindInterfacesTo<BuyButton>()
                .FromInstance(buyButton)
                .AsCached();
            
            Container
                .BindInterfacesTo<UseHealthKitButton>()
                .FromInstance(useHealthKitButton)
                .AsCached();
            
            Container
                .BindInterfacesTo<DeleteButton>()
                .FromInstance(deleteButton)
                .AsCached();

            Container.Bind<IClick>()
                .FromComponentsInHierarchy()
                .AsCached();
            
            Container
                .BindInterfacesTo<PopUpComponent>()
                .FromInstance(popUpComponent)
                .AsSingle();
        }
    }
}