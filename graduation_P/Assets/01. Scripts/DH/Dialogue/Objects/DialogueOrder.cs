using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "DialogueOrder")]
public class DialogueOrder : DialogueText
{
    [Header("Order")]
    public CocktailDataSO cocktail;
}
