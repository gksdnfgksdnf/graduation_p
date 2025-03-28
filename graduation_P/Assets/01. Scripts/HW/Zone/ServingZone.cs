using UnityEngine;

public class ServingZone : BaseZone
{
    
    private void OnTriggerEnter2D(Collider2D other)
    {
        Item item = GetItem(other);

        if (item == null)
            return;

        if (baseItem == null)
        {
            if (IsValidItemType(item))
            {
                baseItem = item;
                StartCoroutine(PlaceItem(item));
                Debug.Log($"칵테일 서빙 준비 완료 : {baseItem.gameObject.name}");
                cocktailMaker.FinishMakeCocktail();
            }
            else
            {
                Debug.Log($"이 아이템은 {baseType} 타입이어야 합니다. {item.itemType}");
            }
        }
    }
}
