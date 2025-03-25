using Cysharp.Threading.Tasks;
using System;
using UnityEngine;

public delegate void CocktailServedEvent(CocktailDataSO wanted, Cocktail served);

public class OrderManager : MonoBehaviour
{
    public static OrderManager Instance { get; private set; }

    [field: SerializeField] public CocktailDataSO wanted { get; private set; }
    public Action<CocktailDataSO> onWanted;
    public CocktailServedEvent onServed;

    [SerializeField] private Cocktail testCocktail;

    private void Awake()
    {
        Instance = this;
    }

    public void Order(CocktailDataSO cocktail)
    {
        wanted = cocktail;
        onWanted?.Invoke(cocktail);
    }

    public void Serve(Cocktail served)
    {
        onServed?.Invoke(wanted, served);
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.K))
        {
            Serve(testCocktail);
        }
    }
}
