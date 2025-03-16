using UnityEngine;
using UnityEngine.EventSystems;

public class DraggableItem : Item, IDragHandler, IPointerDownHandler, IPointerUpHandler
{
    private Vector3 offset;
    private bool isDragging = false;

    public void OnPointerDown(PointerEventData data)
    {
        Debug.Log("OnPointerDown 호출됨");

        // 드래그 시작
        isDragging = true;

        // 마우스와 오브젝트의 거리 계산
        Vector3 mousePosition = GetMousePos(data);
        offset = transform.position - mousePosition;
    }

    public void OnDrag(PointerEventData data)
    {
        if (isDragging)
        {
            // 마우스 위치
            Vector3 mousePosition = GetMousePos(data);
            transform.position = mousePosition + offset;
        }
    }

    public void OnPointerUp(PointerEventData data)
    {
        isDragging = false;
    }

    private Vector3 GetMousePos(PointerEventData data)
    {
        return Camera.main.ScreenToWorldPoint(new Vector3(data.position.x, data.position.y, Camera.main.WorldToScreenPoint(transform.position).z));
    }
}
