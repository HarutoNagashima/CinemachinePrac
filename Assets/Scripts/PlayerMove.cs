using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMove : MonoBehaviour
{
    // �ړ����x
    [SerializeField]
    private float moveSpeed = 2.0f;
    // �ړ���
    private Vector3 mov = Vector3.zero;
    // �Q�[���p�b�h�̕ϐ�
    private Vector2 gamePad;
    /*// ���͂��󂯎��X�N���v�g�̎Q��
    private PlayerInputCatch playerInput;*/

    public void playerMove(InputAction.CallbackContext context)
    {
        // �Q�[���p�b�h��Null�`�F�b�N
        if (Gamepad.current == null) return;
        // ����擾
        gamePad = context.ReadValue<Vector2>();
        // �J�����̕�������X-Z���ʂ̒P�ʃx�N�g�����擾
        Vector3 cameraForward = Vector3.Scale(Camera.main.transform.forward, new Vector3(1, 0, 1)).normalized;
        // �ړ��ʂ̌v�Z
        mov = cameraForward * gamePad.y * moveSpeed * Time.deltaTime
            + Camera.main.transform.right * gamePad.x * moveSpeed * Time.deltaTime;

        switch (context.phase)
        {
            // ���͊J�n
            case InputActionPhase.Started:
                transform.forward = mov; // �|���������Ɍ���
                break;

            case InputActionPhase.Canceled: // ���͏I��
                break;

            default:
                transform.forward = mov;
                break;
        }
    }

    void FixedUpdate()
    {
        // �ړ�������
        transform.position += mov;
    }

}
