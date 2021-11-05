using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerControl : MonoBehaviour
{
    // �ړ����x
    [SerializeField]
    private float _moveSpeed = 2.0f;
    // �v���C���[����]���鑬�x
    [SerializeField]
    private float _rotateSpeed = 600;

    // �ړ���
    private Vector3 mov = Vector3.zero;

    // �Q�[���p�b�h/���X�e�B�b�N�̕ϐ�
    private Vector2 _leftStick;

    // �Q�[���p�b�h/�{�^���̕ϐ�
    private bool _padButton;

    // �L�����N�^�[�̕ϐ�
    private GameObject _player;

    // �A�j���[�^�[�̎Q��
    [SerializeField]
    private Animator _anim;

    public void playerMove(InputAction.CallbackContext context)
    {
        // ����擾
        _leftStick = context.ReadValue<Vector2>();
        // �J�����̕�������X-Z���ʂ̒P�ʃx�N�g�����擾
        Vector3 cameraForward = Vector3.Scale(Camera.main.transform.forward, new Vector3(1, 0, 1));
        // �ړ��ʂ̌v�Z
        mov = (cameraForward * _leftStick.y * _moveSpeed + Camera.main.transform.right * _leftStick.x * _moveSpeed) * Time.deltaTime;

        switch (context.phase)
        {
            // ���͊J�n
            case InputActionPhase.Started:
                _player.transform.forward = mov * (_rotateSpeed * Time.deltaTime); // �|���������Ɍ���
                break;

            case InputActionPhase.Canceled: // ���͏I��
                break;

            default:
                _player.transform.forward = mov * (_rotateSpeed * Time.deltaTime);
                break;
        }
    }
    public void dodge(InputAction.CallbackContext context)
    {
        if (context.phase == InputActionPhase.Performed)
        {
            Debug.Log("���");
            _anim.SetTrigger("ButtonEast");
        }
    }
    public void LightAtttack(InputAction.CallbackContext context)
    {
        if (context.phase == InputActionPhase.Performed && !_anim.GetCurrentAnimatorStateInfo(0).IsName("Attack01") && !_anim.IsInTransition(0))
        {
            Debug.Log("��U��");
            _anim.SetTrigger("LightAttack");
        }
    }


    void ApplyAnimatorParameter()
    {
        float speed = Mathf.Abs(_leftStick.magnitude);
        _anim.SetFloat("Speed", speed, 0.1f, Time.deltaTime);
    }

    void Start()
    {
        // �v���C���[�I�u�W�F�N�g���L���b�V��
        _player = GameObject.Find("Player");
    }

    void FixedUpdate()
    {
        // �ړ�������
        _player.transform.position += mov;
        // �A�j���[�^�[�ݒ�
        ApplyAnimatorParameter();
    }

}
