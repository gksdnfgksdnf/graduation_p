using UnityEngine;
using UnityEngine.UIElements;

[UxmlElement]
public partial class CustomScrollView : ScrollView
{
    private bool isDragging = false;
    private Vector2 previousPointerPosition;
    private Vector2 velocity = Vector2.zero;

    public CustomScrollView()
    {
        this.verticalScrollerVisibility = ScrollerVisibility.Hidden;
        this.horizontalScrollerVisibility = ScrollerVisibility.Hidden;

        if (mode == ScrollViewMode.Vertical)
        {
            contentContainer.style.height = new Length(1000, LengthUnit.Pixel);
        }
        else if (mode == ScrollViewMode.Horizontal)
        {
            contentContainer.style.flexGrow = 0;
            contentContainer.style.width = new Length(100, LengthUnit.Percent);
            contentContainer.style.height = StyleKeyword.Auto;
            contentContainer.style.flexDirection = FlexDirection.Column;
            contentContainer.style.flexWrap = Wrap.Wrap;
        }

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
        this.CapturePointer(evt.pointerId);
    }

    private void OnPointerMove(PointerMoveEvent evt)
    {
        if (isDragging)
        {
            Vector2 pointerPosition = new Vector2(evt.position.x, evt.position.y);
            Vector2 delta = pointerPosition - previousPointerPosition;

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

            previousPointerPosition = evt.position;
        }
    }

    private void OnPointerUp(PointerUpEvent evt)
    {
        isDragging = false;
        ScheduleTick();
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
                velocity *= scrollDecelerationRate;
                scrollOffset += -velocity;
                ApplyBounds();
                ScheduleTick();
            }
            else
            {
                velocity = Vector2.zero;
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
            Debug.Log("왓");

            velocity.x = 0;
            ScheduleBounce(new Vector2(minScrollX, scrollOffset.y));
        }
        else if (scrollOffset.x > maxScrollX)
        {
            Debug.Log("왓");

            velocity.x = 0;
            ScheduleBounce(new Vector2(maxScrollX, scrollOffset.y));
        }

        if (scrollOffset.y < minScrollY)
        {
            Debug.Log("왓");
            velocity.y = 0;
            ScheduleBounce(new Vector2(scrollOffset.x, minScrollY));
        }
        else if (scrollOffset.y > maxScrollY)
        {
            Debug.Log("왓");
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

