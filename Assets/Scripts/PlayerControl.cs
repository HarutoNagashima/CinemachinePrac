using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerControl : MonoBehaviour
{
    // 移動速度
    [SerializeField]
    private float _moveSpeed = 2.0f;
    // プレイヤーが回転する速度
    [SerializeField]
    private float _rotateSpeed = 600;

    // 移動量
    private Vector3 mov = Vector3.zero;

    // ゲームパッド/左スティックの変数
    private Vector2 _leftStick;
    // キーボードの変数
    private Vector2 _keyBooard;

    // ゲームパッド/ボタンの変数
    private bool _padButton;

    // キャラクターの変数
    private GameObject _player;

    // アニメーターの参照
    [SerializeField]
    private Animator _anim;

    // 移動
    public void playerMove(InputAction.CallbackContext context)
    {
        // 操作取得
        _leftStick = context.ReadValue<Vector2>();
        Debug.Log(_leftStick);
        // カメラの方向からX-Z平面の単位ベクトルを取得
        Vector3 cameraForward = Vector3.Scale(Camera.main.transform.forward, new Vector3(1, 0, 1));
        // 移動量の計算
        mov = (cameraForward * _leftStick.y * _moveSpeed + Camera.main.transform.right * _leftStick.x * _moveSpeed) * Time.deltaTime;

        switch (context.phase)
        {
            // 入力開始
            case InputActionPhase.Started:
                _player.transform.forward = mov * (_rotateSpeed * Time.deltaTime); // 倒した方向に向く
                break;

            case InputActionPhase.Canceled: // 入力終了
                break;

            default:
                _player.transform.forward = mov * (_rotateSpeed * Time.deltaTime);
                break;
        }
    }
    // 回避
    public void dodge(InputAction.CallbackContext context)
    {
        if (context.phase == InputActionPhase.Performed)
        {
            _anim.SetTrigger("ButtonEast");
        }
    }
    // 弱攻撃
    public void LightAtttack(InputAction.CallbackContext context)
    {
        if (context.phase == InputActionPhase.Performed && !_anim.GetCurrentAnimatorStateInfo(0).IsName("Attack01") && !_anim.IsInTransition(0))
        {
            _anim.SetTrigger("LightAttack");
        }
    }

    // アニメーターに値を設定
    void ApplyAnimatorParameter()
    {
        float speed = Mathf.Abs(_leftStick.magnitude);
        _anim.SetFloat("Speed", speed, 0.1f, Time.deltaTime);
    }

    void Start()
    {
        // プレイヤーオブジェクトをキャッシュ
        _player = GameObject.Find("Player");
    }

    void FixedUpdate()
    {
        // 移動させる
        _player.transform.position += mov;
        // アニメーター設定
        ApplyAnimatorParameter();
    }

}
