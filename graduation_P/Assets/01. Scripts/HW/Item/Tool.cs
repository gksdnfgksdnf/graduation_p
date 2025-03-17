using UnityEngine;

public class Tool : DraggableItem
{
    private ToolSO tool;

    private void Awake()
    {
        tool = item as ToolSO;
        itemType = ItemType.Tool;
    }

    public override void Use()
    {
        
    }

}
