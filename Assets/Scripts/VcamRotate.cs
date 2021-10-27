using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using Cinemachine;

public class VcamRotate : MonoBehaviour
{

    // �Q�[���p�b�h�̕ϐ�
    private Vector2 _gamepad;

    // ���@�[�`�����J�����̃g�����X�|�[�U�[
    private CinemachineOrbitalTransposer _transposer;

    // ���@�[�`�����J�����̎Q��
    [SerializeField]
    private CinemachineVirtualCamera _vCam;

    [SerializeField]
    // �J�����̉�]���x
    private float _rotateSpeed = 3.0f;

    public void camRotate(InputAction.CallbackContext context)
    {
        // ����擾
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
