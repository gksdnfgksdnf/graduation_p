using UnityEngine;

public enum DialogueType
{
    Major,
    Minor,
    SmallTalk,
    ETC
}

[CreateAssetMenu(menuName = "SO/Dialogue/Header")]
public class DialogueHeader : ScriptableObject
{
    public DialogueType Type;
    public BehaviourType behaviour = BehaviourType.Enter;
    public CustomerFeel feel = CustomerFeel.Fine;
    public float drunk = 0;
    public float reliance = 0;
    public DialogueObject header;
    public int count = 0;
    public int maxCount = 1;
}
