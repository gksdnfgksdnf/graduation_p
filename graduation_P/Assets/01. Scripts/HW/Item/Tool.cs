using UnityEngine;

public class Tool : Item
{
    private ToolSO tool;
    private ToolType toolType;

    private void Awake()
    {
        tool = itemData as ToolSO;

        Initialize(ItemType.Tool);
    }

    public override void Use()
    {
        Debug.Log("도구사용!!");
    }

}
