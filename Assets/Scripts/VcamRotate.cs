using Cinemachine;
using UnityEngine;
using UnityEngine.InputSystem;

public class VcamRotate : MonoBehaviour
{

    // �Q�[���p�b�h�̕ϐ�
    private Vector2 _rightStick;

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
