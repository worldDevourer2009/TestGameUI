using Main;
using Zenject;

namespace Installers
{
    public class GameManagerInstaller :MonoInstaller
    {
        public override void InstallBindings()
        {
            Container
                .BindInterfacesAndSelfTo<GameManager>()
                .FromComponentsInHierarchy()
                .AsSingle();
        }
    }
}