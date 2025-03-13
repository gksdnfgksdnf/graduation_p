using UnityEngine;

public class Glass : Item
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
