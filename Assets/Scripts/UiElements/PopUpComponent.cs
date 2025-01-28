using System;
using System.Collections.Generic;
using Components;
using ScriptableObjects;
using Signals;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using Zenject;

namespace UiElements
{
    public class PopUpComponent : MonoBehaviour, IInitializable
    {
        [SerializeField] private GameObject mainObject;
        
        [SerializeField] private Image itemImage;
        [SerializeField] private Image amountImage;
        [SerializeField] private Image healImage;
        [SerializeField] private Image protectionImage;
        
        [SerializeField] private TextMeshProUGUI itemWeight;
        [SerializeField] private TextMeshProUGUI itemVariable;
        [SerializeField] private List<ActionButtons> buttons;

        private SignalBus _signalBus;
        private List<IClick> _clicks;
        private ItemConfig _currentItemConfig;
        private InventoryItem _item;
        
        [Inject]
        public void Construct(SignalBus signalBus, List<IClick> clicks)
        {
            _signalBus = signalBus;
            _clicks = clicks;
        }
        
        public void Initialize()
        {
            mainObject.gameObject.SetActive(false);
            
            amountImage.gameObject.SetActive(false);
            healImage.gameObject.SetActive(false);
            protectionImage.gameObject.SetActive(false);

            foreach (var click in _clicks)
            {
                click.OnClick += HandleClick;
            }
            
            _signalBus.Subscribe<ItemClickedSignal>(InitRecievedItem);
        }

        private void HandleClick()
        {
            this.gameObject.SetActive(false);
        }

        private void InitRecievedItem(ItemClickedSignal evt)
        {
            _item = evt.Item;
            if (_item == null)
            {
                Debug.Log("Item recieved is null");
                return;
            }
            
            mainObject.gameObject.SetActive(true);
            _currentItemConfig = _item.GetItemConfig();
            ProcessSignalStats();
            ProcessSignalButtons();
        }

        private void ProcessSignalButtons()
        {
            var type = _currentItemConfig.ItemType;
            DisableAllButtons();
            
            switch (type)
            {
                case ItemType.Gun:
                case ItemType.Rifle:
                    var buyButton = GetButtonByType(ButtonType.AmmoButton);
                    buyButton.gameObject.SetActive(true);
                    break;
                case ItemType.Helmet:
                case ItemType.Jacket:
                case ItemType.BallisticVest:
                case ItemType.Cap:
                    var equipeButton = GetButtonByType(ButtonType.EquipmentButton);
                    equipeButton.gameObject.SetActive(true);
                    break;
                case ItemType.HealthKit:
                    var healButton = GetButtonByType(ButtonType.HealButton);
                    healButton.gameObject.SetActive(true);
                    break;
                case ItemType.None:
                default:
                    throw new ArgumentOutOfRangeException();
            }
        }

        private void DisableAllButtons()
        {
            foreach (var button in buttons)
            {
                if (button.type == ButtonType.DeleteButton) continue;
                button.button.gameObject.SetActive(false);
            }
        }

        private void ProcessSignalStats()
        {
            DisableAllImages();
            var type = _currentItemConfig.ItemType;
            
            switch (type)
            {
                case ItemType.Gun:
                    itemImage.sprite = _currentItemConfig.ItemImage;
                    amountImage.gameObject.SetActive(true);
                    
                    itemWeight.text = _currentItemConfig.Weight.ToString();
                    itemVariable.text = _item.Count.ToString();
                    break;
                case ItemType.Rifle:
                    itemImage.sprite = _currentItemConfig.ItemImage;
                    amountImage.gameObject.SetActive(true);
                    itemWeight.text = _currentItemConfig.Weight.ToString();
                    itemVariable.text = _item.Count.ToString();
                    break;
                case ItemType.Helmet:
                    itemImage.sprite = _currentItemConfig.ItemImage;
                    protectionImage.gameObject.SetActive(true);
                    itemWeight.text = _currentItemConfig.Weight.ToString();
                    itemVariable.text = _currentItemConfig.ItemModifierValue.ToString();
                    break;
                case ItemType.Jacket:
                    itemImage.sprite = _currentItemConfig.ItemImage;
                    protectionImage.gameObject.SetActive(true);
                    itemWeight.text = _currentItemConfig.Weight.ToString();
                    itemVariable.text = _currentItemConfig.ItemModifierValue.ToString();
                    break;
                case ItemType.BallisticVest:
                    itemImage.sprite = _currentItemConfig.ItemImage;
                    protectionImage.gameObject.SetActive(true);
                    itemWeight.text = _currentItemConfig.Weight.ToString();
                    itemVariable.text = _currentItemConfig.ItemModifierValue.ToString();
                    break;
                case ItemType.Cap:
                    itemImage.sprite = _currentItemConfig.ItemImage;
                    protectionImage.gameObject.SetActive(true);
                    itemWeight.text = _currentItemConfig.Weight.ToString();
                    itemVariable.text = _currentItemConfig.ItemModifierValue.ToString();
                    break;
                case ItemType.HealthKit:
                    itemImage.sprite = _currentItemConfig.ItemImage;
                    healImage.gameObject.SetActive(true);
                    itemWeight.text = _currentItemConfig.Weight.ToString();
                    itemVariable.text = _currentItemConfig.ItemModifierValue.ToString();
                    break;
                case ItemType.None:
                default:
                    throw new ArgumentOutOfRangeException();
            }
        }

        private void DisableAllImages()
        {
            amountImage.gameObject.SetActive(false);
            healImage.gameObject.SetActive(false);
            protectionImage.gameObject.SetActive(false);
        }

        private Button GetButtonByType(ButtonType type)
        {
            foreach (var button in buttons)
            {
                if (button.type != type) continue;
                return button.button;
            }

            return null;
        }

        private void OnDestroy()
        {
            _signalBus.Unsubscribe<ItemClickedSignal>(InitRecievedItem);
        }
    }
}