using UnityEngine;
using UnityEngine.EventSystems;

namespace Components
{
    public class SlotHandler : MonoBehaviour, IDropHandler
    {
        private int _currentCount;
        
        public void OnDrop(PointerEventData eventData)
        {
            if (transform.childCount > 0) return;
            var obj = eventData.pointerDrag.GetComponent<StorableObjectComponent>();
            obj.dragParent = transform;
        }
    }
}