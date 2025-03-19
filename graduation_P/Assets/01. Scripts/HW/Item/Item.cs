using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UIElements;

public class Item : MonoBehaviour
{
    public ItemSO itemData;

    public float smoothSpeed { get; private set; }
    public bool canDrag = true;

    private bool isDragging = false;
    private Vector3 offset;
    private Vector3 mousePosition;
    private Camera mainCam;

    private void Start()
    {
        mainCam = Camera.main;
        smoothSpeed = 15f;
    }

    private void Update()
    {
        if (!canDrag) return;

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
    }

    private void FixedUpdate()
    {
        if (isDragging)
        {
            transform.position = Vector3.Lerp(transform.position, mousePosition + offset, smoothSpeed * Time.fixedDeltaTime);
        }
    }

    public virtual void Use()
    {
        Debug.Log("아이템 사용");
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
