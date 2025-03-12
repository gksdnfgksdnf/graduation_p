using UnityEngine;

public enum CustomerStatType
{
    Drunk,
    Reliance,
    Like
}

[CreateAssetMenu(menuName = "SO/Customer/Stat")]
public class CustomerStat : ScriptableObject
{
    public int drunk; // 취기
    public int reliance; // 신뢰
    public int like; // 호감도
}
