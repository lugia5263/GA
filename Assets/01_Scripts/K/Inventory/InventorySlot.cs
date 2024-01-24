using UnityEngine;
using UnityEngine.EventSystems;

public class InventorySlot : MonoBehaviour
{
    private RectTransform rectTransform;
    private Vector2 offset;

    void Start()
    {
        rectTransform = GetComponent<RectTransform>();
    }

    public void OnPointerDown(BaseEventData eventData)
    {
        PointerEventData pointerEventData = (PointerEventData)eventData;
        if (pointerEventData != null)
        {
            offset = rectTransform.position - (Vector3)pointerEventData.position;
        }
    }

    public void OnDrag(BaseEventData eventData)
    {
        PointerEventData pointerEventData = (PointerEventData)eventData;
        if (pointerEventData != null)
        {
            rectTransform.position = pointerEventData.position + offset;
        }
    }
}