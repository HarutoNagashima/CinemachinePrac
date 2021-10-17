using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMove : PlayerInput
{
    // �ړ����x
    [SerializeField]
    private float moveSpeed = 2.0f;
    // �ړ���
    private Vector3 mov = Vector3.zero;

    public void playerMove(InputAction.CallbackContext context)
    {
        // �Q�[���p�b�h��Null�`�F�b�N
        if (Gamepad.current == null) return;
        // �J�����̕�������X-Z���ʂ̒P�ʃx�N�g�����擾
        Vector3 cameraForward = Vector3.Scale(Camera.main.transform.forward, new Vector3(1, 0, 1)).normalized;
        // �ړ��ʂ̌v�Z
        mov = cameraForward * gamePad.y * moveSpeed * Time.deltaTime
            + Camera.main.transform.right * gamePad.x * moveSpeed * Time.deltaTime;

        /*switch (context.phase)
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
        }*/
        transform.forward = mov;

    }

    void FixedUpdate()
    {
        // �ړ�������
        this.transform.position += mov;
    }

}
