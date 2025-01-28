using System;
using System.Collections.Generic;
using System.Linq;
using Components;
using Zenject;

namespace Factories
{
    public class ItemFactory : ICreateItem
    {
        private readonly DiContainer _diContainer;
        private readonly List<Item> _itemsPrefabs;

        public ItemFactory(DiContainer diContainer, List<Item> itemsPrefabs)
        {
            _diContainer = diContainer;
            _itemsPrefabs = itemsPrefabs;
        }

        public InventoryItem CreateItemByType(ItemType type)
        {
            switch (type)
            {
                case ItemType.Cap:
                    var cap = GetPrefabByType(type);
                    if (cap == null) break;
                    return _diContainer.InstantiatePrefabForComponent<InventoryItem>(cap);
                case ItemType.Gun:
                    var gunBull = GetPrefabByType(type);
                    if (gunBull == null) break;
                    return _diContainer.InstantiatePrefabForComponent<InventoryItem>(gunBull);
                case ItemType.Rifle:
                    var rifleBull = GetPrefabByType(type);
                    if (rifleBull == null) break;
                    return _diContainer.InstantiatePrefabForComponent<InventoryItem>(rifleBull);
                case ItemType.Helmet:
                    var helmet = GetPrefabByType(type);
                    if (helmet == null) break;
                    return _diContainer.InstantiatePrefabForComponent<InventoryItem>(helmet);
                case ItemType.Jacket:
                    var jacket = GetPrefabByType(type);
                    if (jacket == null) break;
                    return _diContainer.InstantiatePrefabForComponent<InventoryItem>(jacket);
                case ItemType.BallisticVest:
                    var vest = GetPrefabByType(type);
                    if (vest == null) break;
                    return _diContainer.InstantiatePrefabForComponent<InventoryItem>(vest);
                case ItemType.HealthKit:
                    var hpKit = GetPrefabByType(type);
                    if (hpKit == null) break;
                    return _diContainer.InstantiatePrefabForComponent<InventoryItem>(hpKit);
                case ItemType.None:
                    break;
                default:
                    throw new ArgumentOutOfRangeException(nameof(type), type, null);
            }

            return null;
        }

        private StorableObjectComponent GetPrefabByType(ItemType type)
        {
            return _itemsPrefabs.Where(x => x.type == type).Select(item => item.item).FirstOrDefault();
        }
    }
}