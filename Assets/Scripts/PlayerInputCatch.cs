using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerInputCatch : MonoBehaviour
{
    private Vector2 gamePad;
    public void CatchInput(InputAction.CallbackContext context)
    {
        // �Q�[���p�b�h�̓��͎擾
        gamePad = context.ReadValue<Vector2>();
    }

    public Vector2 GetGamePad => gamePad;
}