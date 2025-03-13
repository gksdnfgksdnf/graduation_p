using UnityEngine;

[CreateAssetMenu(menuName = "SO/Customer/Infomation")]
public class CustomerInfomation : ScriptableObject
{
    public float reliance = 0; // 신뢰 수치에 따른 긍정적 효과 획득
    public float relianceMultiplier = 1f;
    public int progress = 0; // 100이 되었을 경우 다른 수치에 따라 엔딩을 결정함
    public int visitCount = 0; // 방문 횟수

    public bool caseClosed = false; // 완료된 손님인지

    public void AddEffect(DialogueDecision.DecisionEffect effect)
    {
        switch (effect.type)
        {
            case CustomerStatType.reliance:
                reliance = Mathf.Clamp(reliance + effect.value, -100f, 100f);
                break;
            case CustomerStatType.progress:
                progress = Mathf.Clamp(progress + (int)effect.value, 0, 100);
                break;
        }
    }

    public void Load()
    {

    }

    public void Save()
    {

    }
}
