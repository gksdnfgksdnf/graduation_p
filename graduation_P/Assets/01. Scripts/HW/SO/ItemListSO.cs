using System;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "ItemList", menuName = "SO/Item/ItemList")]
public class ItemListSO : ScriptableObject
{
    public List<ItemSO> itemList;

    // 이름
    private Dictionary<string, ItemSO> itemsByName = new Dictionary<string, ItemSO>();
    // 타입
    private Dictionary<Type, List<ItemSO>> itemsByType = new Dictionary<Type, List<ItemSO>>();

    private void OnEnable()
    {
        //foreach (var item in itemList)
        //{
        //    // 이름으로 저장
        //    if (!itemsByName.ContainsKey(item.itemName))
        //        itemsByName[item.itemName] = item;

        //    // 타입으로 저장
        //    var itemType = item.GetType();

        //    if (!itemsByType.ContainsKey(itemType))
        //        itemsByType[itemType] = new List<ItemSO>();

        //    itemsByType[itemType].Add(item);
        //}
    }

    public ItemSO GetItemByName(string name)
    {
        if (itemsByName.ContainsKey(name))
        {
            return itemsByName[name];
        }
        return null;
    }

    public List<T> GetItemsByType<T>() where T : ItemSO
    {
        Type type = typeof(T);

        if (itemsByType.ContainsKey(type))
        {
            List<T> result = new List<T>();
            foreach (var item in itemsByType[type])
            {
                if (item is T typedItem)
                {
                    result.Add(typedItem);
                }
            }
            return result;
        }
        return new List<T>();
    }
}
