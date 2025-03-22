using UnityEngine;

public class Juice : Item
{
    private JuiceSO juice;

    private void Awake()
    {
        juice = itemData as JuiceSO;
    }

    public override void Use()
    {
        
    }

}
