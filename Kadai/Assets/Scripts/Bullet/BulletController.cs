using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletController : MonoBehaviour
{
    Rigidbody2D _rd2b = default;

    [SerializeField, Header("弾速(20以上が好ましい)")]
    private float _bulletSpeed = default;


    /// <summary>
    /// 弾のRigidBodyを取得
    /// </summary>
    private void Awake()
    {
        _rd2b = GetComponent<Rigidbody2D>();
         
    }

    /// <summary>
    /// 弾速管理
    /// </summary>
    private void FixedUpdate()
    {
        _rd2b.velocity = new Vector2(0,_bulletSpeed);

    }


}
