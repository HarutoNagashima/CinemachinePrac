using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using Cinemachine;

public class VcamRotate : MonoBehaviour
{

    // ゲームパッドの変数
    private Vector2 _gamepad;

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
        _gamepad = context.ReadValue<Vector2>();
    }

    void Start()
    {
        _transposer=_vCam.GetCinemachineComponent<CinemachineOrbitalTransposer>();
    }

    void FixedUpdate()
    {
        _transposer.m_Heading.m_Bias += _gamepad.x;
        _transposer.m_FollowOffset.y += _gamepad.y *_rotateSpeed *Time.deltaTime;
    }
}
