using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "DialogueHeader")]
public class DialogueHeader : ScriptableObject
{
    public DialogueObject header;

    [Header("Requirements")]
    public BehaviourType behaviour = BehaviourType.Enter;
    [Range(0f, 100f)]
    public float minDrunk = 0f;
    [Range(0f, 100f)]
    public float maxDrunk = 100f;
    [Range(-100f, 100f)]
    public float minReliance = -100f;
    [Range(-100f, 100f)]
    public float maxReliance = 100f;

    [Header("Reuse")]
    public int count = 0;
    public int maxCount = 1;
    public bool infiniteCount = false;
}
