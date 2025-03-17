using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UIElements;

public class Item : MonoBehaviour
{
    public ItemSO item;
    protected Item_Type itemType;

    public virtual void Use()
    {

    }
}
