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

    private Collider2D col;

    private void Start()
    {
        mainCam = Camera.main;
        smoothSpeed = 15f;
        col = GetComponent<Collider2D>();
    }

    private void Update()
    {
        if (!canDrag) return;

        if (Input.GetMouseButton(0))
        {
            mousePosition = GetMousePos();
        }

        if (Input.GetMouseButtonDown(0) && IsMouseOverItem(mousePosition))
        {
            isDragging = true;
            offset = transform.position - mousePosition;
            gameObject.layer = LayerMask.NameToLayer("DraggingItem");
        }

        if (Input.GetMouseButtonUp(0) && isDragging)
        {
            isDragging = false;
            gameObject.layer = LayerMask.NameToLayer("Item");
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
        mousePosition = mainCam.ScreenToWorldPoint(Input.mousePosition);
        mousePosition.z = 0;
        return mousePosition;
    }

    private bool IsMouseOverItem(Vector3 mousePosition)
    {
        return col?.OverlapPoint(mousePosition) ?? false; //if collider is null, it'll be return false.
    }


}
