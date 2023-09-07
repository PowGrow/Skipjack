using TMPro;
using UnityEngine;

public class Record : MonoBehaviour
{
    [SerializeField]
    private TextMeshProUGUI Position;
    [SerializeField]
    private TextMeshProUGUI Name;
    [SerializeField]
    private TextMeshProUGUI Score;

    public void SetRecordInfo(int position, string name, int score)
    {
        Position.text = position.ToString();
        Name.text = name.ToString();
        Score.text = score.ToString();
    }
}
