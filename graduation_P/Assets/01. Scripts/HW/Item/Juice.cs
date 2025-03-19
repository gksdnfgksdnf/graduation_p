using UnityEngine;

public class Juice : DraggableItem
{
    private JuiceSO juice;

    private void Awake()
    {
        juice = item as JuiceSO;
        itemType = ItemType.Ingredient; 
    }

    public override void Use()
    {
        
    }

}
