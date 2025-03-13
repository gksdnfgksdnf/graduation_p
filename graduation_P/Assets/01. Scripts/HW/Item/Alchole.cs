using UnityEngine;

public class Alchole : Item
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
