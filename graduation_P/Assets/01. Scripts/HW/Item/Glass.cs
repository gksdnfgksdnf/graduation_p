using UnityEngine;

public class Glass : Item
{
    private GlassSO glass;

    private void Awake()
    {
        glass = itemData as GlassSO;
    }

    public override void Use()
    {

    }

}
