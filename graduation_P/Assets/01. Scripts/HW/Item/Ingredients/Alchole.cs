using UnityEngine;

public class Alchole : Ingredient
{
    private AlcholeSO alchole;

    private void Awake()
    {
        alchole = itemData as AlcholeSO;
    }

    public override void Use()
    {
        
    }


}
