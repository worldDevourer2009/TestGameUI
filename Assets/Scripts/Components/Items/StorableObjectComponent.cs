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
        protected Image ItemImage;
        private Transform _originalParent;
        private bool _isDragging;
        private bool _isPointerDown;

        protected virtual void Awake()
        {
            ItemImage = GetComponent<Image>();
        }
        
        protected abstract void InitializeImage();
        
        public virtual void OnBeginDrag(PointerEventData eventData)
        {
            Debug.Log("On begin drag");
            _isDragging = true;
            ItemImage.raycastTarget = false;
            _isPointerDown = false;
            dragParent = transform.parent;
            transform.SetParent(dragParent);
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
        
        public virtual void RefreshCount() {}
        
        public virtual void OnDrag(PointerEventData eventData)
        {
            transform.position = Input.mousePosition;
            _isPointerDown = false;
        }

        public virtual void OnEndDrag(PointerEventData eventData)
        {
            ItemImage.raycastTarget = true;
            transform.SetParent(dragParent);
            _isDragging = true;
        }

        private async Task WaitBeforeClick()
        {
            await Task.Delay(200);
            
            if (!_isDragging && _isPointerDown)
                OnItemClicked?.Invoke(_isDragging);
        }
    }
}