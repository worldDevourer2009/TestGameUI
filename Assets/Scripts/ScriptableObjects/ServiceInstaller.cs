using Signals;
using UnityEngine;
using Zenject;

[CreateAssetMenu(menuName = "Installer Zenject/ProjectInstaller", fileName = "ProjectInstaller")]
public class ServiceInstaller : ScriptableObjectInstaller<ServiceInstaller>
{
    public override void InstallBindings()
    {
        BindSignalBus();
        BindPlayerSignals();
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
    }

    private void BindSignalBus()
    {
        SignalBusInstaller
            .Install(Container);
    }
}