using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerInput : MonoBehaviour
{
    // 移動速度
    [SerializeField]
    private float moveSpeed = 2.0f;
    // 移動量
    private Vector3 mov = Vector3.zero;

    void FixedUpdate()
    {
        // 移動させる
        transform.position += mov;
    }

    public void playerMove(InputAction.CallbackContext context)
    {
        // ゲームパッドの入力取得
        var gamePad = context.ReadValue<Vector2>();
        // ゲームパッドのNullチェック
        if (Gamepad.current == null) return;
        // カメラの方向からX-Z平面の単位ベクトルを取得
        Vector3 cameraForward = Vector3.Scale(Camera.main.transform.forward, new Vector3(1, 0, 1)).normalized;
        // 移動量の計算
        mov = cameraForward*gamePad.y*moveSpeed*Time.deltaTime
            +Camera.main.transform.right*gamePad.x*moveSpeed*Time.deltaTime;

        switch (context.phase)
        {
            // 入力開始
            case InputActionPhase.Started:
                transform.forward = mov; // 倒した方向に向く
                break;

            case InputActionPhase.Canceled: // 入力終了
                break;

            default:
                transform.forward = mov;
                break;
        }

    }
}
