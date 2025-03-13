using UnityEngine;

public abstract class Item : MonoBehaviour
{
    public ItemSO item;
    public float price;
    protected Item_Type itemType;

    public abstract void Use(); //아이템 사용
}
