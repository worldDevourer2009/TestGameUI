using Components;
using Models;
using Player;
using ScriptableObjects;
using Signals;
using UnityEngine;
using Zenject;

[CreateAssetMenu(menuName = "Installer Zenject/ProjectInstaller", fileName = "ProjectInstaller")]
public class ServiceInstaller : ScriptableObjectInstaller<ServiceInstaller>
{
    [SerializeField] private PlayerConfig playerConfig;
    
    public override void InstallBindings()
    {
        BindSignalBus();
        BindPlayerSignals();
        
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

    private void BindPlayerSignals()
    {
        Container
            .DeclareSignal<TakeDamagePlayerSignal>();

        Container
            .DeclareSignal<HealPlayerSignal>();

        Container
            .DeclareSignal<ArmorPlayerSignal>();

        Container
            .DeclareSignal<ModelServiceSignal>();

        Container
            .DeclareSignal<ItemClickedSignal>();

        Container
            .DeclareSignal<ArmorAddedSignal>();
    }

    private void BindSignalBus()
    {
        SignalBusInstaller
            .Install(Container);
    }
}