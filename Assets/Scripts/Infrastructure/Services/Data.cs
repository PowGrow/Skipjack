using System.Collections.Generic;
using System.IO;
using UnityEngine;

public class Data 
{
    private const string filename = "Score.json";
    public ScoreSerializable ScoreTable { get; private set; }

    private string path;
    public Data()
    {
        path = Path.Combine(Application.persistentDataPath, filename);
        ScoreTable = LoadData();
    }
    public void SaveData(RecordSerializable recordSerializable)
    {
        if (recordSerializable.Score == 0)
            return;
        if(ScoreTable.Records.Count != 0)
            InsertRecord(recordSerializable);
        else
        {
            recordSerializable.Position = 1;
            ScoreTable.Records.Add(recordSerializable);
        }
        var json = JsonUtility.ToJson(ScoreTable);
        File.WriteAllText(path, json);
    }

    private void InsertRecord(RecordSerializable recordSerializable)
    {
        var isScoreInserted = false;
        for (int i = 0; i < ScoreTable.Records.Count; i++)
        {
            if (ScoreTable.Records[i].Score < recordSerializable.Score)
            {
                if (isScoreInserted == false)
                {
                    recordSerializable.Position = i + 1;
                    ScoreTable.Records.Insert(i, recordSerializable);
                    isScoreInserted = true;
                }
                else
                {
                    ScoreTable.Records[i].Position += 1;
                }
            }
        }
    }

    public ScoreSerializable LoadData()
    {
        if (!File.Exists(path))
            return new ScoreSerializable(new List<RecordSerializable>());
        ScoreTable =  JsonUtility.FromJson<ScoreSerializable>(File.ReadAllText(path));
        return ScoreTable;
    }
}
