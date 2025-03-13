using UnityEngine;
using static DialogueDecision;

[CreateAssetMenu(menuName = "SO/Customer/Infomation")]
public class CustomerInfomation : ScriptableObject
{
    public int like; // 호감도 수치에 따른 긍정적인 효과 획득
    public float likeMultiplayer = 1f;
    public int reliance; // 신뢰 수치에 따른 긍정적 효과 획득
    public float relianceMultiplayer = 1f;
    public int progress; // 100이 되었을 경우 다른 수치에 따라 엔딩을 결정함
    public int visitCount; // 방문 횟수

    public bool traumaTrigger = false; // 이거 건드리면 죽을 가능성이 높아짐 + 대화가 바뀜
    public bool caseClosed = false; // 완료된 손님인지

    public void AddEffect(DecisionEffect effect)
    {
        switch (effect.type)
        {
            case CustomerStatType.like:
                like += (int)(effect.value * likeMultiplayer);
                break;
            case CustomerStatType.reliance:
                reliance += (int)(effect.value * relianceMultiplayer);
                break;
            case CustomerStatType.progress:
                progress += effect.value;
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
