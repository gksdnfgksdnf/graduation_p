using UnityEngine;

public enum GlassType
{
    Shot,
    Rock,
    Flute,
    HighBall,
    Collins,
    Zombie,
    StemlessWine,
    Martini,//CocktailGlass
    Margarita,
    Hurricane,
    Snifter,
    Sour,
    Pilsner,
    Sherry,
    Coupe,
    ChampagneFlute
}
[CreateAssetMenu(fileName = "NewGlass", menuName = "SO/Item/Glass")]
public class GlassSO : ItemSO
{
    public GlassType glassType;
}

