using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAttack: MonoBehaviour
{
    // コライダーの変数
    [SerializeField]
    private BoxCollider _coll;

    // 効果音
    [SerializeField]
    private AudioClip _attackSE;

    // AudioSourceの参照
    [SerializeField]
    private AudioSource _audioSource;

    void SEPoint()
    {
        Debug.Log("ブン");
        _audioSource.PlayOneShot(_attackSE);
    }

    void AttackStart()
    {
        Debug.Log("弱攻撃はじめ");
        _coll.enabled = true;
    }

    void AttackEnd()
    {
        Debug.Log("弱攻撃終わり");
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
