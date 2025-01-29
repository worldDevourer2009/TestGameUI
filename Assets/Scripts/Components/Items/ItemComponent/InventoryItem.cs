using Components;
using ScriptableObjects;
using Signals;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using Zenject;


    public class InventoryItem : StorableObjectComponent
    {
        public ItemType ItemType => itemConfig.ItemType;
        [SerializeField] private ItemConfig itemConfig;

        private SignalBus _signalBus;
        private TextMeshProUGUI _countText;
        
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

        public override void UpdateCount()
        {
            if (Count <= 0)
            {
                Destroy(gameObject);
                return;
            }

            _countText.text = Count switch
            {
                1 => "",
                _ => Count.ToString()
            };
        }

        protected override void InitializeImage()
        {
            var image = itemConfig.ItemImage;
            _itemImage.sprite = image;
            Count = 1;
            UpdateCount();

            OnItemClicked += HandleClicked;
        }

        private void HandleClicked(bool draging)
        {
            if (draging) return;
            _signalBus.Fire(new ItemClickedSignal {Item = this});
        }

        private void OnDestroy()
        {
            OnItemClicked -= HandleClicked;
        }
    }