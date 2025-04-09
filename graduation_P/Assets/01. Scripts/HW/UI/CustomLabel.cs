using System;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UIElements;

[UxmlElement]

public partial class CustomLabel : Label
{
    public CustomLabel()
    {
        RegisterCallback<PointerDownEvent>(OnPointerDown);
        RegisterCallback<PointerUpEvent>(OnPointerUp);
        RegisterCallback<PointerLeaveEvent>(OnPointerLeave);
    }

    private void OnPointerLeave(PointerLeaveEvent evt)
    {
        if (this.ClassListContains("clicked"))
            this.RemoveFromClassList("clicked");
    }

    private void OnPointerDown(PointerDownEvent evt)
    {
        this.AddToClassList("clicked");
    }

    private void OnPointerUp(PointerUpEvent evt)
    {
        Debug.Log("지웠냐");
        this.RemoveFromClassList("clicked");
    }
}
