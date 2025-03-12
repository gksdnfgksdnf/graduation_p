public enum Ing_Type
{
    Alchole,
    Juice,
    Drink,
    Ganish,
    Ice,
    None
}

[System.Serializable]
public class Ingredient
{
    public string name;
    public Ing_Type type;
}
