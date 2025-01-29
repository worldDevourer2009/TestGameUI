using System.Collections.Generic;
using Components;
using Factories;
using Spawners;
using UnityEngine;
using Zenject;

namespace Installers
{
    public class InventoryInstaller : MonoInstaller<InventoryInstaller>
    {
        [SerializeField] private SlotHandler[] slots;
        [SerializeField] private ItemSpawner itemSpawner;
        [SerializeField] private List<Item> itemsPrefabs;

        public override void InstallBindings()
        {
            Container
                .Bind<SlotHandler[]>()
                .FromInstance(slots)
                .AsSingle();
            
            Container
                .Bind<List<Item>>()
                .FromInstance(itemsPrefabs)
                .AsSingle();
            
            Container
                .BindInterfacesTo<ItemFactory>()
                .AsSingle();
            
            Container
                .BindInterfacesTo<InventoryController>()
                .AsSingle()
                .WithArguments(slots);

            Container
                .BindInterfacesTo<InventoryItem>()
                .FromComponentsInHierarchy()
                .AsCached();

            Container
                .BindInterfacesTo<ItemSpawner>()
                .FromInstance(itemSpawner)
                .AsSingle();
        }
    }
}