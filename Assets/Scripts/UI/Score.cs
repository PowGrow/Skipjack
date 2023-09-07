using TMPro;
using UnityEngine;
using Zenject;

public class Score : MonoBehaviour
{
    [SerializeField]
    private TextMeshProUGUI ScoreLabel;
    [SerializeField]
    private GameObject ScoreObject;

    public int CurrentScore { get; private set; }
    [Inject]
    public void Construct(PlayerState playerState, StrufeControl strufeControl)
    {
        strufeControl.OnAllStrufesMoved += UpdateScore;
        playerState.OnGameStart += ShowScore;
        playerState.OnGameOver += HideScore;
    }

    public void UpdateScore()
    {
        CurrentScore++;
        ScoreLabel.text = CurrentScore.ToString();
    }

    public void UpdateScore(int score)
    {
        ScoreLabel.text = score.ToString();
    }

    public void ShowScore()
    {
        CurrentScore = 0;
        UpdateScore(CurrentScore);
        ScoreObject.SetActive(true);
    }

    public void HideScore()
    {
        ScoreObject.SetActive(false);
    }
}
