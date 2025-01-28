using ScriptableObjects;
using Signals;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using Zenject;

namespace Components
{
    public class InventoryItem : StorableObjectComponent
    {
        public ItemType ItemType => itemConfig.ItemType;
        [SerializeField] private ItemConfig itemConfig;

        private SignalBus _signalBus;
        private TextMeshProUGUI _countText;
        private Image _itemImage;
        
        [Inject]
        public void Construct(SignalBus signalBus)
        {
            _itemImage = GetComponent<Image>();
            _countText = GetComponentInChildren<TextMeshProUGUI>();
            _signalBus = signalBus;
            InitializeImage();
        }

        public override ItemConfig GetItemConfig()
        {
            return itemConfig;
        }

        public override void RefreshCount()
        {
            switch (Count)
            {
                case 1:
                    return;
                case 0:
                    break;
            }
            Debug.Log($"Current count for {this.gameObject.name} is {Count}");
            _countText.text = Count.ToString();
        }

        protected override void InitializeImage()
        {
            var image = itemConfig.ItemImage;
            _itemImage.sprite = image;
            
            Debug.Log($"Image is {_itemImage.name} and sprite name is {itemConfig.ItemImage.name}");
            
            Count = 1;
            RefreshCount();

            OnItemClicked += HandleClicked;
        }

        private void HandleClicked(bool draging)
        {
            if (draging) return;
            Debug.Log("Hello");
            _signalBus.Fire(new ItemClickedSignal {Item = this});
        }

        private void OnDestroy()
        {
            OnItemClicked -= HandleClicked;
        }
    }
}