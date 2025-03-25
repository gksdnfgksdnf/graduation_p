using UnityEngine;

public class Alchole : Ingredient
{
    private AlcholeSO alchole;

    protected override void Awake()
    {
        base.Awake();
        alchole = itemData as AlcholeSO;
    }

    public override void Use()
    {
        
    }


}
