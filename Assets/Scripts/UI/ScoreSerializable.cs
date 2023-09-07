using System;
using System.Collections.Generic;

[Serializable]
public class ScoreSerializable 
{
    public List<RecordSerializable> Records;
    public ScoreSerializable(List<RecordSerializable> records)
    {
        Records = records;
    }
}
