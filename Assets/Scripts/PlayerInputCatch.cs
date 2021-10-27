using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerInputCatch : MonoBehaviour
{
    private Vector2 _gamePad;
    public void CatchInput(InputAction.CallbackContext context)
    {
        // ゲームパッドの入力取得
        _gamePad = context.ReadValue<Vector2>();
    }

    public Vector2 GetGamePad => _gamePad;
}