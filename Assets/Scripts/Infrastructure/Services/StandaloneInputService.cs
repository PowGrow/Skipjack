using System;

public class StandaloneInputService : IInputService
{
    private InputControls _inputControls = new InputControls();

    public event Action OnJumpPressed;

    public float HorizontalAxis => Horizontal();
    public void Activate()
    {
        _inputControls.Enable();
        _inputControls.Control.Jump.performed += (context) => OnJumpPressed?.Invoke();
    }

    private float Horizontal()
    {
        return _inputControls.Control.Axis.ReadValue<float>();
    }

}
