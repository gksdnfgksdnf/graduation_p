using UnityEngine;

public class Glass : DraggableItem
{
    private GlassSO glass;

    private void Awake()
    {
        glass = item as GlassSO;
        itemType = Item_Type.Glass;
    }

    public override void Use()
    {
        
    }

}
