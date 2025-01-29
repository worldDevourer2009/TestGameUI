using UnityEngine;
using UnityEngine.EventSystems;

namespace Components
{
    public class SlotHandler : MonoBehaviour, IDropHandler
    {
        public void OnDrop(PointerEventData eventData)
        {
            if (transform.childCount > 0) return;
            var obj = eventData.pointerDrag.GetComponent<StorableObjectComponent>();
            obj.dragParent = transform;
        }
    }
}