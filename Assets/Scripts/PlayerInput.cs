using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerInput : MonoBehaviour
{
    protected Vector2 gamePad;
    public void CatchInput(InputAction.CallbackContext context)
    {
        // �Q�[���p�b�h�̓��͎擾
        gamePad = context.ReadValue<Vector2>();
        Debug.Log(gamePad);
    }
}
