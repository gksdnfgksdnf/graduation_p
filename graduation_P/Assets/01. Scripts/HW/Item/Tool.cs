using UnityEngine;

public class Tool : Item
{
    private ToolSO tool;

    private void Awake()
    {
        tool = item as ToolSO;
        itemType = Item_Type.Tool;
    }

    public override void Use()
    {
        
    }

}
