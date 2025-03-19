using UnityEngine;

public class Alchole : DraggableItem
{

    private AlcholeSO alchole;

    private void Awake()
    {
        alchole = item as AlcholeSO;
        itemType = ItemType.Ingredient;

    }

    public override void Use()
    {
        
    }


}
