using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMove : MonoBehaviour
{
    // �ړ����x
    [SerializeField]
    private float _moveSpeed = 2.0f;
    // �v���C���[����]���鑬�x
    [SerializeField]
    private float _rotateSpeed = 600;

    // �ړ���
    private Vector3 mov = Vector3.zero;

    // �Q�[���p�b�h�̕ϐ�
    private Vector2 _gamePad;

    // �A�j���[�^�[�̎Q��
    [SerializeField]
    private Animator _anim;

    public void playerMove(InputAction.CallbackContext context)
    {
        // ����擾
        _gamePad = context.ReadValue<Vector2>().normalized;
        // �J�����̕�������X-Z���ʂ̒P�ʃx�N�g�����擾
        Vector3 cameraForward = Vector3.Scale(Camera.main.transform.forward, new Vector3(1, 0, 1)).normalized;
        // �ړ��ʂ̌v�Z
        mov = (cameraForward * _gamePad.y * _moveSpeed + Camera.main.transform.right * _gamePad.x * _moveSpeed) * Time.deltaTime;

        switch (context.phase)
        {
            // ���͊J�n
            case InputActionPhase.Started:
                transform.forward = mov*(_rotateSpeed*Time.deltaTime); // �|���������Ɍ���
                break;

            case InputActionPhase.Canceled: // ���͏I��
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
        // �ړ�������
        transform.position += mov;
        // �A�j���[�^�[�ݒ�
        ApplyAnimatorParameter();
    }

}
