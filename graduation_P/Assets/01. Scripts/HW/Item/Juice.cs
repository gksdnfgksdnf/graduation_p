using UnityEngine;

public class Juice : Item
{
    private JuiceSO juice;

    private void Awake()
    {
        juice = item as JuiceSO;
        itemType = Item_Type.Ingredient;
    }

    public override void Use()
    {
        
    }

}
