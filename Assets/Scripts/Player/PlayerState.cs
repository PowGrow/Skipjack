using System;

public class PlayerState
{
    public event Action OnGameOver;
    public event Action OnGameStart;
    public State CurrentState { get; private set; } = State.GameOver;
    private bool _isGameStart = true;
    public bool Is(State state)
    {
        if (CurrentState == state)
            return true;
        return false;
    }

    public void Set(State state)
    {
        CurrentState = state;
        if (CurrentState == State.GameOver)
        {
            _isGameStart = true;
            OnGameOver?.Invoke();
        }
        if(CurrentState == State.Idle && _isGameStart == true)
        {
            _isGameStart = false;
            OnGameStart?.Invoke();
        }
    }

    public void CheckGameOver()
    {
        if (!Is(State.GameOver))
            Set(State.Idle);
    }
}
