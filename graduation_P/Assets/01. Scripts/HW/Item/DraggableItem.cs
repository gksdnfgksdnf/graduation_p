using UnityEngine;
using UnityEngine.EventSystems;

public class DraggableItem : Item
{
    private Vector3 offset;
    private bool isDragging = false;
    private Camera mainCamera;

    private void Start()
    {
        // 메인 카메라 가져오기
        mainCamera = Camera.main;
        if (mainCamera == null)
        {
            Debug.LogError("Main Camera를 찾을 수 없습니다!");
        }
    }

    private void OnMouseDown()
    {
        Debug.Log("OnMouseDown 호출됨");

        // 드래그 시작
        isDragging = true;

        // 마우스와 오브젝트의 거리 계산
        Vector3 mousePosition = GetMousePos();
        offset = transform.position - mousePosition;
    }

    private void OnMouseDrag()
    {
        if (isDragging)
        {
            // 마우스 위치 업데이트
            Vector3 mousePosition = GetMousePos();
            transform.position = mousePosition + offset;
        }
    }

    private void OnMouseUp()
    {
        Debug.Log("OnMouseUp 호출됨");

        // 드래그 종료
        isDragging = false;
    }

    private Vector3 GetMousePos()
    {
        Vector3 mousePosition = mainCamera.ScreenToWorldPoint(Input.mousePosition);
        mousePosition.z = 0f; 
        return mousePosition;
    }
}
