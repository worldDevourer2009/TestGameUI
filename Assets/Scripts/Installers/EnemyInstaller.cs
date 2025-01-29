using Components;
using Enemies;
using Models;
using ScriptableObjects;
using UnityEngine;
using Zenject;

namespace Installers
{
    public class EnemyInstaller : MonoInstaller<EnemyInstaller>
    {
        [SerializeField] private EnemyConfig enemyConfig;
        public override void InstallBindings()
        {
            Container
                .Bind<EnemyConfig>()
                .FromInstance(enemyConfig)
                .AsSingle();

            Container
                .BindInterfacesTo<HealthComponent>()
                .AsSingle()
                .WithArguments(enemyConfig.MaxHealth);

            Container
                .BindInterfacesTo<AttackComponent>()
                .AsSingle()
                .WithArguments(enemyConfig.Damage);

            Container
                .BindInterfacesTo<EnemyController>()
                .AsSingle()
                .NonLazy();

            Container
                .BindInterfacesTo<EnemyModel>()
                .AsSingle()
                .NonLazy();
        }
    }
}