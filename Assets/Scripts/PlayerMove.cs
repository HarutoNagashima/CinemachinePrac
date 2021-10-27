using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMove : MonoBehaviour
{
    // 移動速度
    [SerializeField]
    private float _moveSpeed = 2.0f;
    // プレイヤーが回転する速度
    [SerializeField]
    private float _rotateSpeed = 600;

    // 移動量
    private Vector3 mov = Vector3.zero;

    // ゲームパッドの変数
    private Vector2 _gamePad;

    // アニメーターの参照
    [SerializeField]
    private Animator _anim;

    public void playerMove(InputAction.CallbackContext context)
    {
        // 操作取得
        _gamePad = context.ReadValue<Vector2>().normalized;
        // カメラの方向からX-Z平面の単位ベクトルを取得
        Vector3 cameraForward = Vector3.Scale(Camera.main.transform.forward, new Vector3(1, 0, 1)).normalized;
        // 移動量の計算
        mov = (cameraForward * _gamePad.y * _moveSpeed + Camera.main.transform.right * _gamePad.x * _moveSpeed) * Time.deltaTime;

        switch (context.phase)
        {
            // 入力開始
            case InputActionPhase.Started:
                transform.forward = mov*(_rotateSpeed*Time.deltaTime); // 倒した方向に向く
                break;

            case InputActionPhase.Canceled: // 入力終了
                break;

            default:
                transform.forward = mov * (_rotateSpeed * Time.deltaTime);
                break;
        }
    }

    void ApplyAnimatorParameter()
    {
        float speed = Mathf.Abs(_gamePad.magnitude);
        _anim.SetFloat("Speed", speed, 0.1f, Time.deltaTime);
    }

    void FixedUpdate()
    {
        // 移動させる
        transform.position += mov;
        // アニメーター設定
        ApplyAnimatorParameter();
    }

}
