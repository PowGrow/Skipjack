using System;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using Zenject;

public class Restart : MonoBehaviour
{
    [SerializeField]
    private GameObject RestartButton;
    [SerializeField]
    private GameObject MenuButton;
    [SerializeField]
    private GameObject EndScoreContainer;
    [SerializeField]
    private TextMeshProUGUI ScoreLabel;
    [SerializeField]
    private TMP_InputField InputFieldScore;

    public event Action FromRestartToMenu;

    private Data _data;
    private Score _score;
    private LoadingCurtain _loadingCurtain;
    private PlayerState _playerState;

    [Inject]
    public void Construct(Data data, Score score, LoadingCurtain loadingCurtain, PlayerState playerState)
    {
        playerState.OnGameOver += ShowRestartMenu;
        _data = data;
        _score = score;
        _loadingCurtain = loadingCurtain;
        _playerState = playerState;
    }

    public void OnRestartButtonClick()
    {
        _loadingCurtain.Show();

        _loadingCurtain.Hide();
        HideRestartMenu();
        _playerState.Set(State.Idle);
    }

    public void OnMenuButtonClick()
    {
        HideRestartMenu();
        FromRestartToMenu?.Invoke();
    }

    private void ShowRestartMenu()
    {
        ScoreLabel.text = _score.CurrentScore.ToString();
        EndScoreContainer.SetActive(true);
        RestartButton.SetActive(true);
        MenuButton.SetActive(true);
    }

    private void HideRestartMenu()
    {
        TryToSaveScore();
        EndScoreContainer.SetActive(false);
        RestartButton.SetActive(false);
        MenuButton.SetActive(false);
    }

    private void TryToSaveScore()
    {
        if (InputFieldScore.text == "")
            return;
        var record = new RecordSerializable(0, InputFieldScore.text, Convert.ToInt32(ScoreLabel.text));
        _data.SaveData(record);
    }
}
