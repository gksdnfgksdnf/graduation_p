using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "DialogueOrder")]
public class DialogueOrder : DialogueText
{
    [Header("Order")]
    public bool format = false;
    public CocktailDataSO cocktail;

    [Header("Random")]
    public bool reroll = false;
    public List<CocktailDataSO> randomCocktails;

    public override void EnterDialogue(Customer customer, DialogueDisplayer displayer)
    {
        if (cocktail == null || reroll)
            cocktail = randomCocktails[Random.Range(0, randomCocktails.Count)];
        if (format)
            texts[^1] = string.Format(texts[^1], cocktail.itemName);
        base.EnterDialogue(customer, displayer);
    }

    public override void ExitDialogue(Customer customer, DialogueDisplayer displayer)
    {
        customer.Order(cocktail);
        base.ExitDialogue(customer, displayer);
    }
}
