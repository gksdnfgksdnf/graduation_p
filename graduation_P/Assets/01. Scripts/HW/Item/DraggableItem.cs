using UnityEngine;
using UnityEngine.EventSystems;


public class DraggableItem : Item
{
    private Vector3 offset;
    private bool isDragging = false;
    private Camera mainCamera;
    private void Start()
    {
        mainCamera = Camera.main;
        if (mainCamera == null)
        {
            Debug.LogError("Main Camera can't be find!");
        }
    }

    private void OnMouseDown()
    {
        gameObject.layer = LayerMask.NameToLayer("DraggingItem");
        isDragging = true;

        Vector3 mousePosition = GetMousePos();
        offset = transform.position - mousePosition;
        Debug.Log("잡았냐");
    }

    private void OnMouseDrag()
    {
        if (isDragging)
        {
            Vector3 mousePosition = GetMousePos();
            transform.position = mousePosition + offset;
            Debug.Log("움직이냐");

        }
    }

    private void OnMouseUp()
    {
        isDragging = false;
        gameObject.layer = LayerMask.NameToLayer("Item");
        Debug.Log("땠냐");


    }

    private Vector3 GetMousePos()
    {
        Vector3 mousePosition = mainCamera.ScreenToWorldPoint(Input.mousePosition);
        mousePosition.z = 0f; 
        return mousePosition;
    }
}
