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
        Debug.Log("뮉싱쿨라스 솨용!!!");
    }

}
