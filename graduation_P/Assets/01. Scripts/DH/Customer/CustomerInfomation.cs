using UnityEngine;

public enum EffectType
{
    Drunk,
    Reliance,
    Progress,
}

[CreateAssetMenu(menuName = "SO/Customer/Infomation")]
public class CustomerInfomation : ScriptableObject
{
    public string customerName;
    public float drunk = 0; // 취기에 따른 대화 해금
    public float drunkMultiplier = 1f;
    public float reliance = 0; // 신뢰 수치에 따른 긍정적 효과 획득
    public float relianceMultiplier = 1f;
    public int progress = 0; // 100이 되었을 경우 다른 수치에 따라 엔딩을 결정함
    public int visitCount = 0; // 방문 횟수

    public int firstAppearDay = 0;
    public bool caseClosed = false; // 완료된 손님인지

    public void AddEffect(EffectType type, int value)
    {
        switch (type)
        {
            case EffectType.Drunk:
                drunk = Mathf.Clamp(drunk + (value * drunkMultiplier), 0f, 100f);
                break;
            case EffectType.Reliance:
                reliance = Mathf.Clamp(reliance + (value * relianceMultiplier), -100f, 100f);
                break;
            case EffectType.Progress:
                progress = Mathf.Clamp(progress + (int)value, 0, 100);
                break;
        }
    }
}
