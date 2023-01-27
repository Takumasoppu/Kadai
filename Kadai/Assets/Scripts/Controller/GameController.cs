using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameController : MonoBehaviour
{
    [SerializeField, Header("PlayerControllerの格納")]
    private PlayerController _pC = default;

    [SerializeField, Header("1Wave目の敵格納")]
    private  EnemyController[] _oneWaveEnemy = default;
    
    //[SerializeField, Header("EnemyBulletControllerの格納")]
    //private EnemyBulletController _eBc = default;

    [SerializeField, Header("オブジェクトプールの格納")]
    private ObjectPool _objPoolScript = default;

    [SerializeField, Header("オブジェクトプールの格納2")]
    private GameObject _objPool = default;

    [SerializeField, Header("1Wave終了フラグ")]
    private bool _oneWaveEnd = default;

    /// <summary>
    /// ここで他のスクリプトの挙動を管理
    /// </summary>
    private void Update()
    {
        //プレイヤーの移動のメソッド呼び出し
        _pC.PlayerInput();

        _oneWaveEnemy[0].ActiveObj();

        _oneWaveEnemy[1].ActiveObj();

        //敵を倒した際の処理
        _oneWaveEnemy[0].EnemyOut();

        _oneWaveEnemy[1].EnemyOut();

    }

    /// <summary>
    /// ここで他のスクリプトの物理計算を管理
    /// </summary>
    private void FixedUpdate()
    {
        //プレイヤーの物理挙動
        _pC.PlayerMove();

        //攻撃のメソッド呼び出し
        _pC.Fire();

        //現在のプレイヤーのポジション
        _pC.NowPlayerPosition();


        //##########[ 1Wave目の敵の処理 ]##############

        //敵の物理挙動
        _oneWaveEnemy[0].EnemyMove();

        _oneWaveEnemy[1].EnemyMove();

        //現在の敵のポジション
        _oneWaveEnemy[0].NowEnemyPosition();

        _oneWaveEnemy[1].NowEnemyPosition();

        //敵が攻撃する際の弾の配置
        _oneWaveEnemy[0].Enemyshot();

        _oneWaveEnemy[1].Enemyshot();

        



    }
}
