using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UIElements;

public class Item : MonoBehaviour
{
    public ItemSO item;
    protected ItemType itemType;
    
    public virtual void Use()
    {
        Debug.Log("아이템 사용");
    }
}
