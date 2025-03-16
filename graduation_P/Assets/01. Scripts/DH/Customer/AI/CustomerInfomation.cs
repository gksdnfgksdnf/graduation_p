using Donet;
using System;
using System.IO;
using System.Threading.Tasks;
using UnityEngine;

[CreateAssetMenu(menuName = "SO/Customer/Infomation")]
public class CustomerInfomation : ScriptableObject, INetworkSerializable
{
    public string customerName;
    public float reliance = 0; // 신뢰 수치에 따른 긍정적 효과 획득
    public float relianceMultiplier = 1f;
    public int progress = 0; // 100이 되었을 경우 다른 수치에 따라 엔딩을 결정함
    public int visitCount = 0; // 방문 횟수

    public int firstAppearDay = 0;
    public bool caseClosed = false; // 완료된 손님인지

    private string path => $"{Application.persistentDataPath}\\Customer\\{customerName}.txt";

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

    public async Task Load() // 나중에 암호화
    {
        if (File.Exists(path))
        {
            ArraySegment<byte> buffer;
            byte[] data;
            using (FileStream stream = File.OpenRead(path))
            {
                data = new byte[stream.Length];
                int count = await stream.ReadAsync(data, 0, data.Length);
            }
            buffer = new ArraySegment<byte>(data, 0, data.Length);

            Serializer serializer = LocalSerializer.Serializer;
            serializer.Open(NetworkSerializeMode.Deserialize, buffer);
            serializer.SerializeObject(this);
            serializer.Close();
        }
    }

    public async Task Save() // 나중에 암호화
    {
        if (!File.Exists(path))
            File.Create(path);

        byte[] data = new byte[1024];
        ArraySegment<byte> buffer = new ArraySegment<byte>(data, 0, data.Length);

        Serializer serializer = LocalSerializer.Serializer;
        serializer.Open(NetworkSerializeMode.Serialize, buffer);
        serializer.SerializeObject(this);
        int count = serializer.Close();

        using (FileStream stream = File.OpenWrite(path))
        {
            await stream.WriteAsync(data, 0, count);
        }
    }

    public bool Serialize(Serializer serializer)
    {
        try
        {
            serializer.Serialize(ref reliance);
            serializer.Serialize(ref relianceMultiplier);
            serializer.Serialize(ref progress);
            serializer.Serialize(ref visitCount);
            serializer.Serialize(ref firstAppearDay);
            serializer.Serialize(ref caseClosed);
        }
        catch (Exception ex)
        {
            Debug.LogError($"Serialize Error: {ex}");
            return false;
        }
        return true;
    }
}
