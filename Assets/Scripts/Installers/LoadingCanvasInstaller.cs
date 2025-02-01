using UiElements;
using Zenject;

namespace Installers
{
    public class LoadingCanvasInstaller : MonoInstaller<LoadingCanvasInstaller>
    {
        public override void InstallBindings()
        {
            Container
                .BindInterfacesTo<LoadingCanvas>()
                .FromComponentInHierarchy()
                .AsSingle();
        }
    }
}