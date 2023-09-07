using System;
using UnityEngine.InputSystem;

public class TouchScreenInputService : IInputService
{
    private InputControls _inputControls = new InputControls();

    public event Action OnJumpPressed;
    public float HorizontalAxis => Swipe();
    public void Activate()
    {
        _inputControls.Enable();
        _inputControls.Control.Touch.performed += (context) => OnJumpPressed?.Invoke();
    }

    private float Swipe()
    {
        return _inputControls.Control.Axis.ReadValue<float>();
    }
}
