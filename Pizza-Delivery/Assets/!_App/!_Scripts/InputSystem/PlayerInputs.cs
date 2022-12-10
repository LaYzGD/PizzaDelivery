using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerInputs : MonoBehaviour
{
    public int HorizontalMovementInput { get; private set; }

    public void HandleMovementInput(InputAction.CallbackContext context)
    {
        var horizontalInput = context.ReadValue<Vector2>();
        horizontalInput.Normalize();

        HorizontalMovementInput = (int)Mathf.Round(horizontalInput.x);
    }
}
