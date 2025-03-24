using UnityEngine;

public class Item : MonoBehaviour
{
    public ItemSO itemData;

    [HideInInspector]
    public ItemType itemType;

    #region ItemMove
    protected Vector3 _origin;

    public bool canDrag = true;

    private bool _isDragging = false;
    private Vector3 _offset;
    private Vector3 _mousePosition;
    private Camera _mainCam;

    private Collider2D _col;

    [HideInInspector]
    public float smoothSpeed;
    #endregion

    private void Awake()
    {
        _origin = transform.position;
    }

    private void Start()
    {
        _mainCam = Camera.main;
        smoothSpeed = 15f;
        _col = GetComponent<Collider2D>();
    }

    public void Initialize(ItemType type)
    {
        itemType = type;
    }

    private void Update()
    {
        if (!canDrag) return;

        if (Input.GetMouseButton(0))
        {
            _mousePosition = GetMousePos();
        }

        if (Input.GetMouseButtonDown(0) && IsMouseOverItem(_mousePosition))
        {
            _isDragging = true;
            _offset = transform.position - _mousePosition;
            gameObject.layer = LayerMask.NameToLayer("DraggingItem");
        }

        if (Input.GetMouseButtonUp(0) && _isDragging)
        {
            _isDragging = false;
            gameObject.layer = LayerMask.NameToLayer("Item");
        }
    }

    private void FixedUpdate()
    {
        if (_isDragging)
        {
            transform.position = Vector3.Lerp(transform.position, _mousePosition + _offset, smoothSpeed * Time.fixedDeltaTime);
        }
    }

    private Vector3 GetMousePos()
    {
        _mousePosition = _mainCam.ScreenToWorldPoint(Input.mousePosition);
        _mousePosition.z = 0;
        return _mousePosition;
    }

    private bool IsMouseOverItem(Vector3 mousePosition)
    {
        return _col?.OverlapPoint(mousePosition) ?? false; //if collider is null, it'll be return false.
    }
    public virtual void Use()
    {
        Debug.Log("아이템 사용");
    }


}
