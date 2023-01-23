using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBulletController : MonoBehaviour
{
    private Rigidbody2D _rd2d = default;

    //[SerializeField, Header("EnemyControllerを格納")]
    //private EnemyController _eC = default;

    [SerializeField, Header("弾の速度")]
    private float _enemyBulletSpeed = default;

    private void Awake()
    {
        _rd2d = GetComponent<Rigidbody2D>();
    }

    /// <summary>
    /// プレイヤーに向かって飛んでいく弾の処理
    /// </summary>
    private void FixedUpdate()
    {
        _rd2d.velocity = new Vector2(0, _enemyBulletSpeed);
    }


}
