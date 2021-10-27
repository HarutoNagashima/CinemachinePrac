using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMove : MonoBehaviour
{
    // 移動速度
    [SerializeField]
    private float moveSpeed = 2.0f;
    // 移動量
    private Vector3 mov = Vector3.zero;
    // ゲームパッドの変数
    private Vector2 gamePad;
    // アニメーターの参照
    [SerializeField]
    private Animator anim;
    // プレイヤーのリジッドボディ参照
    [SerializeField]
    private Rigidbody rig;
    /*// 入力を受け取るスクリプトの参照
    private PlayerInputCatch playerInput;*/

    public void playerMove(InputAction.CallbackContext context)
    {
        // ゲームパッドのNullチェック
        if (Gamepad.current == null) return;
        // 操作取得
        gamePad = context.ReadValue<Vector2>();
        // カメラの方向からX-Z平面の単位ベクトルを取得
        Vector3 cameraForward = Vector3.Scale(Camera.main.transform.forward, new Vector3(1, 0, 1)).normalized;
        // 移動量の計算
        mov = (cameraForward * gamePad.y * moveSpeed + Camera.main.transform.right * gamePad.x * moveSpeed)*Time.deltaTime;

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

    void ApplyAnimatorParameter()
    {
        var speed = Mathf.Abs(gamePad.x);
        Debug.Log(speed);
        anim.SetFloat("Speed", speed, 0.1f, Time.deltaTime);
    }

    void FixedUpdate()
    {
        // 移動させる
        transform.position += mov;
        // アニメーター設定
        ApplyAnimatorParameter();
    }

}
