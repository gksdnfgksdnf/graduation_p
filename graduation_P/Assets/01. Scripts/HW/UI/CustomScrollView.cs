using UnityEngine;
using UnityEngine.UIElements;

[UxmlElement]
public partial class CustomScrollView : ScrollView
{
    private bool isDragging = false;
    private Vector2 previousPointerPosition;
    private Vector2 velocity = Vector2.zero;
    private float decelerationRate = 0.95f; // 감속률

    public CustomScrollView()
    {
        this.verticalScrollerVisibility = ScrollerVisibility.Hidden;
        this.horizontalScrollerVisibility = ScrollerVisibility.Hidden;

        RegisterCallback<PointerDownEvent>(OnPointerDown);
        RegisterCallback<PointerMoveEvent>(OnPointerMove);
        RegisterCallback<PointerUpEvent>(OnPointerUp);
        RegisterCallback<WheelEvent>(OnMouseWheel);
    }

    private void OnPointerDown(PointerDownEvent evt)
    {
        isDragging = true;
        velocity = Vector2.zero;
        previousPointerPosition = evt.position;

        // ✅ PointerCapture 적용 → 스크롤 영역 밖에서도 이벤트 감지
        this.CapturePointer(evt.pointerId);
    }

    private void OnPointerMove(PointerMoveEvent evt)
    {
        if (isDragging)
        {
            Vector2 pointerPosition = new Vector2(evt.position.x, evt.position.y); // ✅ 변환 추가
            Vector2 delta = pointerPosition - previousPointerPosition;

            Vector2 newScroll = scrollOffset - delta;

            float minScrollX = 0;
            float minScrollY = 0;
            float maxScrollX = Mathf.Max(0, contentContainer.resolvedStyle.width - resolvedStyle.width);
            float maxScrollY = Mathf.Max(0, contentContainer.resolvedStyle.height - resolvedStyle.height);

            Vector2 outOfBoundsDistance = Vector2.zero;

            if (newScroll.x < minScrollX) outOfBoundsDistance.x = minScrollX - newScroll.x;
            else if (newScroll.x > maxScrollX) outOfBoundsDistance.x = newScroll.x - maxScrollX;

            if (newScroll.y < minScrollY) outOfBoundsDistance.y = minScrollY - newScroll.y;
            else if (newScroll.y > maxScrollY) outOfBoundsDistance.y = newScroll.y - maxScrollY;

            // 감속 비율 (범위를 넘을수록 더 느려짐)
            Vector2 slowdownFactor = new Vector2(
                1f / (1f + outOfBoundsDistance.x * 0.15f),
                1f / (1f + outOfBoundsDistance.y * 0.15f)
            );

            delta *= slowdownFactor;

            // 최종 스크롤 업데이트 (수직, 수평 방향 적용)
            if (mode == ScrollViewMode.Vertical)
            {
                scrollOffset += new Vector2(0, -delta.y);
                velocity = new Vector2(0, -delta.y);
            }
            else if (mode == ScrollViewMode.Horizontal)
            {
                scrollOffset += new Vector2(-delta.x, 0);
                velocity = new Vector2(-delta.x, 0);
            }
            else // ScrollViewMode.Both
            {
                scrollOffset += -delta;
                velocity = -delta;
            }

            previousPointerPosition = evt.position;
        }
    }

    private void OnPointerUp(PointerUpEvent evt)
    {
        isDragging = false;
        ScheduleTick();

        // ✅ PointerCapture 해제
        this.ReleasePointer(evt.pointerId);
    }

    private void OnMouseWheel(WheelEvent evt)
    {
        if (mode == ScrollViewMode.Vertical)
        {
            scrollOffset += new Vector2(0, evt.delta.y * 10f);
        }
        else if (mode == ScrollViewMode.Horizontal)
        {
            scrollOffset += new Vector2(evt.delta.y * 10f, 0); // 가로 스크롤 휠 적용
        }
        else // ScrollViewMode.Both
        {
            scrollOffset += new Vector2(evt.delta.x * 10f, evt.delta.y * 10f);
        }
    }

    private void ScheduleTick()
    {
        schedule.Execute(() =>
        {
            if (velocity.magnitude > 0.1f)
            {
                velocity *= decelerationRate;
                scrollOffset += -velocity;
                ApplyBounds();
                ScheduleTick();
            }
        });
    }

    private void ApplyBounds()
    {
        float minScrollX = 0;
        float minScrollY = 0;
        float maxScrollX = contentContainer.resolvedStyle.width - resolvedStyle.width;
        float maxScrollY = contentContainer.resolvedStyle.height - resolvedStyle.height;

        if (scrollOffset.x < minScrollX)
        {
            velocity.x = 0;
            ScheduleBounce(new Vector2(minScrollX, scrollOffset.y));
        }
        else if (scrollOffset.x > maxScrollX)
        {
            velocity.x = 0;
            ScheduleBounce(new Vector2(maxScrollX, scrollOffset.y));
        }

        if (scrollOffset.y < minScrollY)
        {
            velocity.y = 0;
            ScheduleBounce(new Vector2(scrollOffset.x, minScrollY));
        }
        else if (scrollOffset.y > maxScrollY)
        {
            velocity.y = 0;
            ScheduleBounce(new Vector2(scrollOffset.x, maxScrollY));
        }
    }

    private void ScheduleBounce(Vector2 targetPosition)
    {
        schedule.Execute(() =>
        {
            if ((scrollOffset - targetPosition).magnitude > 0.1f)
            {
                scrollOffset = Vector2.Lerp(scrollOffset, targetPosition, 0.15f);
                ScheduleBounce(targetPosition);
            }
            else
            {
                scrollOffset = targetPosition;
            }
        });
    }
}
