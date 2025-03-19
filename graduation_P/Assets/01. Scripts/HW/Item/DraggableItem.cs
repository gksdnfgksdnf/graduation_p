using UnityEngine;

public class DraggableItem : Item
{
    private bool isDragging = false;
    private Vector3 offset;
    private Camera mainCam;
    private float smoothSpeed = 15f;
    Vector3 mousePosition;

    private void Start()
    {
        mainCam = Camera.main;
    }

    private void Update()
    {
        mousePosition = GetMousePos();

        if (Input.GetMouseButtonDown(0))
        {
            if (IsMouseOverItem(mousePosition))
            {
                isDragging = true;
                offset = transform.position - mousePosition;
                gameObject.layer = LayerMask.NameToLayer("DraggingItem");
            }
        }

        if (Input.GetMouseButtonUp(0))
        {
            if (isDragging)
            {
                isDragging = false;
                gameObject.layer = LayerMask.NameToLayer("Item");
            }
        }


        if (isDragging)
        {
            // 부드럽게 따라가기 (Lerp 사용)
            transform.position = Vector3.Lerp(transform.position, mousePosition + offset, smoothSpeed * Time.deltaTime);

            // 속도가 너무 빨라지지 않도록 제한
            //Vector3 direction = (mousePosition - transform.position).normalized;
            //float distance = Vector3.Distance(transform.position, mousePosition);
            //transform.position = transform.position + direction * Mathf.Min(maxSpeed * Time.deltaTime, distance);
        }
    }


    private Vector3 GetMousePos()
    {
        Vector3 mousePosition = mainCam.ScreenToWorldPoint(Input.mousePosition);
        mousePosition.z = 0; // Z 값 고정
        return mousePosition;
    }

    private bool IsMouseOverItem(Vector3 mousePosition)
    {
        Collider2D collider = GetComponent<Collider2D>();
        if (collider != null)
        {
            return collider.OverlapPoint(mousePosition);
        }
        return false;
    }
}
