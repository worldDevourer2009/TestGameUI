using System;
using System.Collections.Generic;
using Components;
using ScriptableObjects;
using Signals;
using UnityEngine;
using Zenject;

namespace UiElements
{
    public class SelectGunButton : MonoBehaviour, IClick
    {
        [SerializeField] private List<ItemConfig> gunConfigs;
        [SerializeField] private GunType gunType;
        public event Action OnClick = delegate { };
        
        private SignalBus _signalBus;

        [Inject]
        public void Construct(SignalBus signalBus)
        {
            _signalBus = signalBus;
        }

        public void Click()
        {
            var type = GetConfigByType();
            Debug.Log($"Clicked type {type}");
            _signalBus.Fire(new SelectedGunSignal {Type = type});
        }

        private ItemConfig GetConfigByType()
        {
            foreach (var config in gunConfigs)
            {
                switch (gunType)
                {
                    case GunType.Gun when config.ItemType == ItemType.Gun:
                        return config;
                    case GunType.Rifle when config.ItemType == ItemType.Rifle:
                        return config;
                    default:
                        continue;
                }
            }

            return null;
        }
    }
}