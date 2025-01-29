using Components;
using Models;
using Player;
using ScriptableObjects;
using UnityEngine;
using Zenject;

namespace Installers
{
    public class PlayerInstaller : MonoInstaller<PlayerInstaller>
    {
        [SerializeField] private PlayerConfig playerConfig;

        public override void InstallBindings()
        {
            Container
                .Bind<PlayerConfig>()
                .FromInstance(playerConfig)
                .AsSingle();
            
            Container
                .BindInterfacesTo<ArmorComponent>()
                .AsSingle();

            Container
                .BindInterfacesTo<HealthComponent>()
                .AsSingle()
                .WithArguments(playerConfig.MaxPlayerHealth);

            Container
                .BindInterfacesTo<DamagableComponent>()
                .AsSingle();

            Container
                .BindInterfacesTo<PlayerController>()
                .AsSingle()
                .NonLazy();

            Container
                .BindInterfacesTo<PlayerModel>()
                .AsSingle()
                .NonLazy();
        }
    }
}