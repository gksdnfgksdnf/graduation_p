using UnityEngine;
using UnityEngine.UIElements;

[UxmlElement]
public partial class CustomScrollView : ScrollView
{
    private bool isDragging = false;
    private bool isBouncing = false;
    private Vector2 previousPointerPosition;
    private Vector2 velocity = Vector2.zero;
    private Vector2 lastDelta = Vector2.zero;
    private Vector2 wheelVelocity = Vector2.zero;
    private float lastMoveTime;

    private IVisualElementScheduledItem bounceItem;
    private const float decelerationRate = 0.95f;

    public CustomScrollView()
    {
        this.verticalScrollerVisibility = ScrollerVisibility.Hidden;
        this.horizontalScrollerVisibility = ScrollerVisibility.Hidden;

        if (mode == ScrollViewMode.Vertical)
        {
            //contentContainer.style.height = new Length(1000, LengthUnit.Pixel);
        }
        else if (mode == ScrollViewMode.Horizontal)
        {
            contentContainer.style.flexGrow = 0;
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
        lastMoveTime = Time.realtimeSinceStartup;

        if (isBouncing && bounceItem != null)
        {
            bounceItem.Pause();
            isBouncing = false;
        }

        this.CapturePointer(evt.pointerId);
    }

    private void OnPointerMove(PointerMoveEvent evt)
    {
        if (!isDragging) return;

        Vector2 pointerPosition = evt.position;
        Vector2 delta = pointerPosition - previousPointerPosition;

        float currentTime = Time.realtimeSinceStartup;
        float deltaTime = Mathf.Max(0.001f, currentTime - lastMoveTime);
        lastDelta = delta / deltaTime;

        // 축 제한
        if (mode == ScrollViewMode.Vertical)
        {
            delta.x = 0; // 수평 무시
            ApplyDelta(delta);
        }
        else if (mode == ScrollViewMode.Horizontal)
        {
            delta.y = 0; // 수직 무시
            ApplyDelta(delta);
        }

        previousPointerPosition = pointerPosition;
        lastMoveTime = currentTime;
    }


    private void ApplyDelta(Vector2 delta)
    {
        float minScrollY = 0;
        float maxScrollY = contentContainer.resolvedStyle.height - resolvedStyle.height;
        float minScrollX = 0;
        float maxScrollX = contentContainer.resolvedStyle.width - resolvedStyle.width;

        if (mode == ScrollViewMode.Vertical)
        {
            float nextOffsetY = scrollOffset.y - delta.y;
            if (nextOffsetY < minScrollY || nextOffsetY > maxScrollY)
            {
                float overshoot = Mathf.Abs(nextOffsetY - Mathf.Clamp(nextOffsetY, minScrollY, maxScrollY));
                float dampFactor = 1f / (1f + overshoot * 0.05f);
                delta.y *= dampFactor;
            }

            scrollOffset += new Vector2(0, -delta.y);
            velocity = new Vector2(0, -delta.y);

        }
        else if (mode == ScrollViewMode.Horizontal)
        {
            float nextOffsetX = scrollOffset.x - delta.x;
            if (nextOffsetX < minScrollX || nextOffsetX > maxScrollX)
            {
                float overshoot = Mathf.Abs(nextOffsetX - Mathf.Clamp(nextOffsetX, minScrollX, maxScrollX));
                float dampFactor = 1f / (1f + overshoot * 0.05f);
                delta.x *= dampFactor;
            }

            scrollOffset += new Vector2(-delta.x, 0);
            velocity = new Vector2(-delta.x, 0);
        }
    }


    private void OnPointerUp(PointerUpEvent evt)
    {
        isDragging = false;

        // 손 뗐을 때 속도 적용 - 방향에 따라 분리
        if (mode == ScrollViewMode.Vertical)
            velocity = new Vector2(0, -lastDelta.y * 0.35f);
        else if (mode == ScrollViewMode.Horizontal)
            velocity = new Vector2(-lastDelta.x * 0.35f, 0);

        ScheduleTick();
        this.ReleasePointer(evt.pointerId);
    }


    private void OnMouseWheel(WheelEvent evt)
    {
        float minScrollX = 0;
        float minScrollY = 0;
        float maxScrollX = contentContainer.resolvedStyle.width - resolvedStyle.width;
        float maxScrollY = contentContainer.resolvedStyle.height - resolvedStyle.height;

        if (mode == ScrollViewMode.Vertical)
        {
            float newY = scrollOffset.y + evt.delta.y * 10f;
            newY = Mathf.Clamp(newY, minScrollY, maxScrollY);
            scrollOffset = new Vector2(scrollOffset.x, newY);
        }
        else if (mode == ScrollViewMode.Horizontal)
        {
            float newX = scrollOffset.x + evt.delta.y * 10f;
            newX = Mathf.Clamp(newX, minScrollX, maxScrollX);
            scrollOffset = new Vector2(newX, scrollOffset.y);
        }
    }


    private void ScheduleTick()
    {
        schedule.Execute(() =>
        {
            if (isDragging || isBouncing) return;

            bool shouldContinue = false;

            if (mode == ScrollViewMode.Vertical && Mathf.Abs(velocity.y) > 0.05f)
            {
                scrollOffset += new Vector2(0, velocity.y * Time.deltaTime);
                velocity.y *= Mathf.Pow(decelerationRate, Time.deltaTime * 60f);
                shouldContinue = true;
            }
            else if (mode == ScrollViewMode.Horizontal && Mathf.Abs(velocity.x) > 0.05f)
            {
                scrollOffset += new Vector2(velocity.x * Time.deltaTime, 0);
                velocity.x *= Mathf.Pow(decelerationRate, Time.deltaTime * 60f);
                shouldContinue = true;
            }

            if (IsScrollOutOfBounds())
                ApplyBounds();

            if (shouldContinue)
                ScheduleTick();
            else
                velocity = Vector2.zero;
        });
    }


    private void ApplyBounds()
    {
        if (isBouncing) return;

        float minScrollX = 0;
        float minScrollY = 0;
        float maxScrollX = contentContainer.resolvedStyle.width - resolvedStyle.width;
        float maxScrollY = contentContainer.resolvedStyle.height - resolvedStyle.height;

        bool outOfBounds = false;
        Vector2 target = scrollOffset;

        if (mode == ScrollViewMode.Vertical)
        {
            if (scrollOffset.y < minScrollY)
            {
                velocity.y = 0;
                target.y = minScrollY;
                outOfBounds = true;
            }
            else if (scrollOffset.y > maxScrollY)
            {
                velocity.y = 0;
                target.y = maxScrollY;
                outOfBounds = true;
            }
        }
        else if (mode == ScrollViewMode.Horizontal)
        {
            if (scrollOffset.x < minScrollX)
            {
                velocity.x = 0;
                target.x = minScrollX;
                outOfBounds = true;
            }
            else if (scrollOffset.x > maxScrollX)
            {
                velocity.x = 0;
                target.x = maxScrollX;
                outOfBounds = true;
            }
        }

        if (outOfBounds)
        {
            ScheduleBounce(target);
        }
    }


    private void ScheduleBounce(Vector2 targetPosition)
    {
        isBouncing = true;

        bounceItem = schedule.Execute(() =>
        {
            if ((scrollOffset - targetPosition).magnitude > 0.1f)
            {
                scrollOffset = Vector2.Lerp(scrollOffset, targetPosition, 0.05f);
                ScheduleBounce(targetPosition);
            }
            else
            {
                scrollOffset = targetPosition;
                isBouncing = false;
                bounceItem = null;
            }
        });
    }

    private bool IsScrollOutOfBounds()
    {
        float minScrollX = 0;
        float minScrollY = 0;
        float maxScrollX = contentContainer.resolvedStyle.width - resolvedStyle.width;
        float maxScrollY = contentContainer.resolvedStyle.height - resolvedStyle.height;

        return scrollOffset.x < minScrollX || scrollOffset.x > maxScrollX
            || scrollOffset.y < minScrollY || scrollOffset.y > maxScrollY;
    }
}
