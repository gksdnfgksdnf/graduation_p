using UnityEngine;

[CreateAssetMenu(menuName = "SO/Dialogue/Order")]
public class DialogueOrder : DialogueText
{
    [Header("Order")]
    public CocktailDataSO cocktail;

    public override void ExitDialogue(Customer customer, DialogueDisplayer displayer)
    {
        base.ExitDialogue(customer, displayer);
        customer.Order(cocktail);
    }
}
