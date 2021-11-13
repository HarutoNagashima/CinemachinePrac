using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAttack: MonoBehaviour
{
    // �R���C�_�[�̕ϐ�
    [SerializeField]
    private BoxCollider _coll;

    // ���ʉ�
    [SerializeField]
    private AudioClip _attackSE;

    // AudioSource�̎Q��
    [SerializeField]
    private AudioSource _audioSource;

    void SEPoint()
    {
        Debug.Log("�u��");
        _audioSource.PlayOneShot(_attackSE);
    }

    void AttackStart()
    {
        Debug.Log("��U���͂���");
        _coll.enabled = true;
    }

    void AttackEnd()
    {
        Debug.Log("��U���I���");
        _coll.enabled = false;
    }

    private void OnCollisionEnter(Collision collision)
    {
        string hitColl= collision.gameObject.name;
        Debug.Log(hitColl);
        Destroy(collision.gameObject);
    }

    void Start()
    {
        _coll.enabled = false;
        Debug.Log(_coll.enabled);
    }
}
