using Cysharp.Threading.Tasks;
using UnityEngine;

[CreateAssetMenu(menuName = "DialogueOrder")]
public class DialogueOrder : DialogueObject
{
    public CocktailDataSO cocktail;
    public DialogueObject next;

    public override async UniTask<DialogueObject> Dialogue(Customer customer)
    {
        await OrderManager.Instance.Order(cocktail);
        return null;
    }
}
