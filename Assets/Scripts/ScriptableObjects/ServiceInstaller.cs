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
        BindSignalBus();
        BindPlayerSignals();
        BindSaves();
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