using System;

public interface IInputService
{
    public event Action OnJumpPressed;
    public float HorizontalAxis { get; }
    public void Activate();
}
