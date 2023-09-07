using UnityEngine;
using Zenject;

public class EnemyCreator : MonoBehaviour
{
    [SerializeField]
    private float Delay;
    private IEnemyFactory _enemyFactory;
    private PlayerState _playerState;

    private float _currentDelay = 0f;
    [Inject]
    public void Construct(IEnemyFactory enemyFactory, PlayerState playerState)
    {
        _enemyFactory = enemyFactory;
        _playerState = playerState;
        _playerState.OnGameOver += enemyFactory.RemoveAllEnemies;
    }

    private void Update()
    {
        if (_playerState.Is(State.GameOver))
            return;

        if(_currentDelay <= 0f)
        {
            _enemyFactory.CreateEnemy();
            _currentDelay = Delay;
        }
        else
        {
            _currentDelay -= Time.deltaTime;
        }
    }
}
