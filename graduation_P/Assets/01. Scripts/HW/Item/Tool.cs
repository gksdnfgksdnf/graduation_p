using UnityEngine;

public class Tool : Item
{
    private ToolSO tool;

    private void Awake()
    {
        tool = itemData as ToolSO;
    }

    public override void Use()
    {
        Debug.Log("도구사용!!");

    }

}
