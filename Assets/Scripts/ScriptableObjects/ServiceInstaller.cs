using Main;
using SavesManagement;
using ScriptableObjects;
using Services.SavesManagement.SavePersistentDataManager;
using Signals;
using UnityEngine;
using Zenject;

[CreateAssetMenu(menuName = "Installer Zenject/ProjectInstaller", fileName = "ProjectInstaller")]
public class ServiceInstaller : ScriptableObjectInstaller<ServiceInstaller>
{
    [SerializeField] private PlayerConfig playerConfig;
    
    public override void InstallBindings()
    {
        BindSceneManagement();
        BindSignalBus();
        DeclareSignals();
        BindSaves();
    }

    private void BindSceneManagement()
    {
        Container
            .BindInterfacesTo<SceneLoadManager>()
            .AsSingle()
            .NonLazy();
    }

    private void DeclareSignals()
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

        Container
            .DeclareSignal<SelectedGunSignal>();

        Container
            .DeclareSignal<PlayerFiredSignal>();

        Container
            .DeclareSignal<EnemyModelSignal>();

        Container
            .DeclareSignal<TakeDamageEnemySignal>();

        Container
            .DeclareSignal<EnemyAttackSignal>();

        Container
            .DeclareSignal<PlayerDeathSignal>();

        Container
            .DeclareSignal<EnemyDeathSignal>();

        Container
            .DeclareSignal<LoadingCanvasEnableSignal>();
    }

    private void BindSignalBus()
    {
        SignalBusInstaller
            .Install(Container);
    }

    private void BindSaves()
    {
        Container.BindInterfacesTo<JsonSerializer>()
            .AsSingle()
            .NonLazy();
        
        Container
            .BindInterfacesTo<SaveManager>()
            .AsSingle()
            .NonLazy();
    }
}