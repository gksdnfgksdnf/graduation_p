using System;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "NewCocktail", menuName = "SO/Cocktail/CocktailData")]
public class CocktailDataSO : ItemSO
{
    public CocktailInfo cocktailInfo;
    public List<IngredientSO> ingredients;
    public List<ToolSO> tools;
    public GlassSO glass;
}


[Serializable]
public class CocktailInfo
{
    public Flavors flavors;
    public Aromas aromas;
    public BodyType body;
    public FinishType finish;
    public bool fizziness;
}

[Serializable]
public class Flavors
{
    public int sweet;
    public int salty;
    public int sour;
    public int bitter;
    public int abv;
}

[Serializable]
public class Aromas
{
    public bool flower;
    public bool herbal;
    public bool fruity;
    public bool spicy;
    public bool smoky;
}

public enum BodyType
{
    Light,
    Medium,
    Full
}

public enum FinishType
{
    Short,
    Medium,
    Long,
    Smooth,
    Sharp
}
