using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerInput : MonoBehaviour
{
    protected Vector2 gamePad;
    public void CatchInput(InputAction.CallbackContext context)
    {
        // ゲームパッドの入力取得
        gamePad = context.ReadValue<Vector2>();
        Debug.Log(gamePad);
    }
}
