using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BaseZone : MonoBehaviour
{
    public ItemType baseType;
    public Item baseItem;

    protected CocktailMaker cocktailMaker;

    private void Start()
    {
        cocktailMaker = FindFirstObjectByType<CocktailMaker>();
    }

    protected Item GetItem(Collider2D other)
    {
        Item item = other.transform.GetComponent<Item>();
        return item;
    }

    protected virtual bool IsValidItemType(Item item)
    {
        return baseType == item.itemType;
    }

    protected virtual IEnumerator PlaceItem(Item item)
    {
        while (Vector3.Distance(item.transform.position, transform.position) > 0.01f)
        {
            item.transform.position = Vector3.Lerp(
                item.transform.position,
                transform.position,
                item.smoothSpeed * Time.deltaTime
            );
            yield return null;
        }

        item.transform.position = transform.position;
    }
}
