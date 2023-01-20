﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameController : MonoBehaviour
{
    [SerializeField, Header("PlayerControllerの格納")]
    private PlayerController _pC = default;

    [SerializeField, Header("EnemyControllerの格納")]
    private EnemyController _eC = default;

    [SerializeField, Header("EnemyBulletControllerの格納")]
    private EnemyBulletController _eBc = default;

    /// <summary>
    /// ここで他のスクリプトの挙動を管理
    /// </summary>
    private void Update()
    {
        //プレイヤーの移動のメソッド呼び出し
        _pC.PlayerInput();

        _eC.EnemyOut();
    }

    /// <summary>
    /// ここで他のスクリプトの物理計算を管理
    /// </summary>
    private void FixedUpdate()
    {
        //プレイヤーの物理挙動
        _pC.PlayerMove();

        //敵の物理挙動
        _eC.EnemyMove();

        //攻撃のメソッド呼び出し
        _pC.Fire();

        //現在のプレイヤーのポジション
        _pC.NowPlayerPosition();

        //敵の攻撃
        _eBc.EnemyBulletMove1();

        //敵が攻撃する際の弾の配置
        _eC.Enemyshot();       

    }
}
