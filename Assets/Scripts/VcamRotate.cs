using Cinemachine;
using UnityEngine;
using UnityEngine.InputSystem;

public class VcamRotate : MonoBehaviour
{

    // ゲームパッドの変数
    private Vector2 _rightStick;

    // ヴァーチャルカメラのトランスポーザー
    private CinemachineOrbitalTransposer _transposer;

    // ヴァーチャルカメラの参照
    [SerializeField]
    private CinemachineVirtualCamera _vCam;

    [SerializeField]
    // カメラの回転速度
    private float _rotateSpeed = 3.0f;

    public void camRotate(InputAction.CallbackContext context)
    {
        // 操作取得
        _rightStick = context.ReadValue<Vector2>();
    }

    void Start()
    {
        _transposer = _vCam.GetCinemachineComponent<CinemachineOrbitalTransposer>();
    }

    void FixedUpdate()
    {
        _transposer.m_Heading.m_Bias += _rightStick.x;
        _transposer.m_FollowOffset.y += _rightStick.y * _rotateSpeed * Time.deltaTime;
    }
}
