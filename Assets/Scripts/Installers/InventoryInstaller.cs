using Components;
using ScriptableObjects;
using UnityEngine;
using Zenject;

namespace Installers
{
    public class InventoryInstaller : MonoInstaller<InventoryInstaller>
    {
        [SerializeField] private InventoryConfig inventoryConfig;

        public override void InstallBindings()
        {
            Container
                .Bind<InventoryConfig>()
                .FromInstance(inventoryConfig)
                .AsSingle();
            
            Container
                .Bind<IInventory>()
                .To<InventoryController>()
                .AsSingle()
                .WithArguments(inventoryConfig);
        }
    }
}