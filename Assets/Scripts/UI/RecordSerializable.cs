using System;
using TMPro;
using UnityEngine;

[Serializable]
public class RecordSerializable
{
    public int Position;
    public string Name;
    public int Score;
    public RecordSerializable(int position, string name, int score)
    {
        Position = position;
        Name = name;
        Score = score;
    }
}
