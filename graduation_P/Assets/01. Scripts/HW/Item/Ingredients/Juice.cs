using UnityEngine;

public class Juice : Ingredient
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
