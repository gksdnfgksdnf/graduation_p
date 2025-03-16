using UnityEngine;

public class Alchole : DraggableItem
{

    private AlcholeSO alchole;

    private void Awake()
    {
        alchole = item as AlcholeSO;
        itemType = Item_Type.Ingredient;

    }

    public override void Use()
    {
        
    }


}
