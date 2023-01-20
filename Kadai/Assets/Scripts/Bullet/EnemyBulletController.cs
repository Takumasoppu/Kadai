using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBulletController : MonoBehaviour
{
    private Rigidbody2D _rd2d = default;

    [SerializeField, Header("EnemyControllerを格納")]
    private EnemyController _eC = default;

    [SerializeField, Header("弾の速度")]
    private float _enemyBulletSpeed = default;

    /// <summary>
    /// プレイヤーに向かって飛んでいく弾の処理
    /// </summary>
    public void EnemyBulletMove1()
    {
        float baseDir = _eC.AimFloat();

        _rd2d.velocity = new Vector2(baseDir - _enemyBulletSpeed, baseDir - _enemyBulletSpeed);
        
    }

}
