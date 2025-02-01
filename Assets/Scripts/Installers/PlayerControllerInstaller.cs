using Controllers;
using UnityEngine;
using Views;
using Zenject;

namespace Installers
{
    public class PlayerControllerInstaller : MonoInstaller<PlayerControllerInstaller>
    {
        [SerializeField] private PlayerView playerView;
        
        public override void InstallBindings()
        {
            Container
                .BindInterfacesTo<PlayerUIController>()
                .AsSingle()
                .NonLazy();

            Container
                .BindInterfacesAndSelfTo<PlayerView>()
                .FromInstance(playerView)
                .AsSingle()
                .NonLazy();
        }
    }
}