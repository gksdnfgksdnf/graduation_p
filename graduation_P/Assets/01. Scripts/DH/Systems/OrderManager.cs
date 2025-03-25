using Cysharp.Threading.Tasks;
using System;
using UnityEngine;

public class OrderManager : MonoBehaviour
{
    public static OrderManager Instance { get; private set; }

    [field: SerializeField] public CocktailDataSO ordered { get; private set; }
    public Action<CocktailDataSO> onOrdered;

    private bool serve = false;
    private Cocktail served;

    private void Awake()
    {
        Instance = this;
    }

    public async UniTask<Cocktail> Order(CocktailDataSO cocktail)
    {
        ordered = cocktail;
        onOrdered?.Invoke(cocktail);
        serve = false;
        await UniTask.WaitUntil(() => serve);
        return served;
    }

    public void Serve(Cocktail cocktail)
    {
        serve = true;
        served = cocktail;
    }
}
