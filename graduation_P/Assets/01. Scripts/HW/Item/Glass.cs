using UnityEngine;

public class Glass : DraggableItem
{
    private GlassSO glass;

    private void Awake()
    {
        glass = item as GlassSO;
        itemType = ItemType.Glass;
    }

    public override void Use()
    {
        
    }

}
