using UnityEngine;
using Zenject;

public class Menu : MonoBehaviour
{
    [SerializeField]
    private GameObject ParentContainer;
    [SerializeField]
    private GameObject ScoreViewPrefab;

    private PlayerState _playerState;
    private GameObject _scoreView;
    [Inject]
    public void Contruct (DiContainer container, PlayerState playerState, Restart restart)
    {
        _playerState = playerState;
        restart.FromRestartToMenu += ShowMenu;
        _scoreView = container.InstantiatePrefabForComponent<ScoreLoader>(ScoreViewPrefab, transform).gameObject;
    }

    public void OnPlayButtonClicked()
    {
        _playerState.Set(State.Idle);
        HideMenu();
    }

    public void OnScoreButtonClicked()
    {
        HideMenu();
        ShowScore();
    }

    public void OnBackButtonClickedd()
    {
        HideScore();
        ShowMenu();
    }

    private void ShowMenu()
    {
        ParentContainer.SetActive(true);
    }

    private void HideMenu()
    {
        ParentContainer.SetActive(false);
    }

    private void ShowScore()
    {
        _scoreView.SetActive(true);
    }

    private void HideScore()
    {
        _scoreView.SetActive(false);
    }
}
