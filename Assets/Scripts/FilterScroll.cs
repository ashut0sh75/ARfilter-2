using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class FilterScroll : MonoBehaviour, IDragHandler
{
    public ScrollRect scrollRect;
    public float sensitivity = 1f;

    private float scrollPosition;

    public void OnDrag(PointerEventData eventData)
    {
        float delta = eventData.delta.x * sensitivity;
        scrollPosition += delta;

        // Limit the scroll position to the range [0, 1]
        scrollPosition = Mathf.Clamp(scrollPosition, 0f, 1f);

        // Set the scroll position of the ScrollRect component
        scrollRect.horizontalNormalizedPosition = scrollPosition;
    }
}
