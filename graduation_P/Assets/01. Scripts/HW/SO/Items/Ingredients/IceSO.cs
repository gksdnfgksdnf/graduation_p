using UnityEngine;

public enum IceType
{
    None,
    Cubed, // 정육면체 모양의 일반적인 얼음
    Lump, //온더락 잔에 들어가는 조금 큰 얼음
    Sphere, //원형 얼음
    Cuboid, //직육면체 얼음
    Cracked, //부순 얼음
    Crushed, // 조금 더 부순 얼음
    Shaved //빙수 느낌

}

[CreateAssetMenu(fileName = "NewIce", menuName = "SO/Item/AdditionalIngredient/Ice")]
public class IceSO : IngredientSO
{
    public IceType iceType;
}
