using Controllers;
using Views;
using Zenject;

namespace Installers
{
    public class EnemyUiInstaller : MonoInstaller<EnemyUiInstaller>
    {
        public override void InstallBindings()
        {
            Container
                .BindInterfacesTo<EnemyUiController>()
                .AsSingle()
                .NonLazy();

            Container
                .BindInterfacesTo<EnemyView>()
                .FromComponentInHierarchy()
                .AsSingle();
        }
    }
}