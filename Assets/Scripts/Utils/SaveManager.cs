using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using Newtonsoft.Json;
using UnityEngine;

public static class SaveManager
{
    private static string FilePath => Path.Combine(Application.persistentDataPath, "save.json");

    public static void Save(SaveData saveData)
    {
        if (saveData == null) return;
        saveData.scores?.Sort((a,b) => b.score.CompareTo(a.score));
        string json = JsonConvert.SerializeObject(saveData, Formatting.Indented);
        File.WriteAllText(FilePath, json);
#if UNITY_EDITOR
        Debug.Log("File saved:" + FilePath);
#endif
    }
    
    public static SaveData Load()
    {
        if (!File.Exists(FilePath))
        {
            return new SaveData();
        }

        string json = File.ReadAllText(FilePath);

        SaveData data = null;
        try
        {
            data = JsonConvert.DeserializeObject<SaveData>(json);
        }
        catch (Exception e)
        {
            return new SaveData();
        }

        if (data == null) data = new SaveData();
        if (data.scores == null) data.scores = new List<ScoreEntity>();

        // 按整数从大到小排序
        data.scores.Sort((a, b) => b.score.CompareTo(a.score));

        return data;
    }
}

[Serializable]
public class ScoreEntity
{
    public int score;
    public string name;

    public ScoreEntity(int score, string name)
    {
        this.score = score;
        this.name = name;
    }
}

[Serializable]
public class SaveData
{
    public List<ScoreEntity> scores;

    public SaveData(List<ScoreEntity> scores)
    {
        this.scores = scores;
    }

    public SaveData()
    {
        var scores = new List<ScoreEntity>();
        this.scores = scores;
        
    }
}