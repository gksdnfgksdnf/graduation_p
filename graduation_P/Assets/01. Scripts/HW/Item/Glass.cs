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
        Debug.Log("뮉싱쿨라스 솨용!!!");
    }

}
