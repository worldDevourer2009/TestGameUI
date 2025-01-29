using System;
using System.Threading.Tasks;
using ScriptableObjects;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace Components
{
    public abstract class StorableObjectComponent : MonoBehaviour, IPointerDownHandler, IBeginDragHandler, IDragHandler, IEndDragHandler
    {
        protected event Action<bool> OnItemClicked = delegate { };
        public int Count { get; set; }
        
        public Transform dragParent;
        protected Image _itemImage;
        
        private bool _isDragging;
        private bool _isPointerDown;

        protected virtual void Awake()
        {
            _itemImage = GetComponent<Image>();
        }
        
        protected abstract void InitializeImage();
        
        public virtual void OnBeginDrag(PointerEventData eventData)
        {
            _isDragging = true;
            _isPointerDown = false;
            
            _itemImage.raycastTarget = false;
            dragParent = transform.parent;
            transform.SetParent(transform.root);
        }
        
        public async void OnPointerDown(PointerEventData eventData)
        {
            if (_isDragging) return;
            _isPointerDown = true;
            await WaitBeforeClick();
        }

        public virtual ItemConfig GetItemConfig()
        {
            return default;
        }
        
        public virtual void UpdateCount() {}
        
        public virtual void OnDrag(PointerEventData eventData)
        {
            transform.position = Input.mousePosition;
            _isPointerDown = false;
        }

        public virtual void OnEndDrag(PointerEventData eventData)
        {
            _itemImage.raycastTarget = true;
            _isDragging = false;
            transform.SetParent(dragParent);
        }

        private async Task WaitBeforeClick()
        {
            await Task.Delay(200);
            
            if (!_isDragging && _isPointerDown)
                OnItemClicked?.Invoke(_isDragging);
        }
    }
}