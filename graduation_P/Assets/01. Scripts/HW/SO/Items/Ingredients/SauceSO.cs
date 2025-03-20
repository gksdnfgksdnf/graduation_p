using UnityEngine;

public enum SauceType
{
    None,
    Tabasco,
    Worcestershire, //우스터
}
[CreateAssetMenu(fileName = "NewSauce", menuName = "SO/Item/AdditionalIngredient/Sauce")]
public class SauceSO : IngredientSO
{
    public SauceType sauceType;

    public bool isSpicy;
    public bool isSweet;
    public bool isTangy;
}
