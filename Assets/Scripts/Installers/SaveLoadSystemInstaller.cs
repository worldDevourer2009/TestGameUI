using SavesManagement;
using Zenject;

namespace Installers
{
    public class SaveLoadSystemInstaller : MonoInstaller<SaveLoadSystemInstaller>
    {
        public override void InstallBindings()
        {
            Container
                .BindInterfacesAndSelfTo<SaveLoadSystem>()
                .AsSingle();
        }
    }
}