using UnityEngine;

[CreateAssetMenu(menuName = "DialogueOrder")]
public class DialogueOrder : DialogueObject
{
    public CocktailDataSO cocktail;
    public DialogueObject next;

    private Customer customer;
    private DialogueEventWaiter waiter;

    public override void Dialogue(Customer customer, DialogueEventWaiter waiter)
    {
        this.customer = customer;
        this.waiter = waiter;
        OrderManager.Instance.onServed += HandleServed;
        OrderManager.Instance.Order(cocktail);
    }

    private void HandleServed(CocktailDataSO wanted, Cocktail served)
    {
        customer.Serve(served);

        waiter.next = next;
        waiter.IsCompleted = true;
    }
}
