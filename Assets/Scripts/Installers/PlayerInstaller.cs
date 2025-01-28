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
                .Bind(typeof(IArmor))
                .To<ArmorComponent>()
                .AsSingle();

            Container
                .Bind(typeof(IHealth))
                .To<HealthComponent>()
                .AsSingle()
                .WithArguments(playerConfig.MaxPlayerHealth);

            Container
                .Bind(typeof(IDamagable))
                .To<DamagableComponent>()
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