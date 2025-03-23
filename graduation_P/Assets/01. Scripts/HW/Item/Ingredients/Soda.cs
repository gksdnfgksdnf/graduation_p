using UnityEngine;

public class Soda : Item
{
    private SodaSO soda;

    private void Awake()
    {
        soda = itemData as SodaSO;
    }
    public override void Use()
    {
        
    }


}
