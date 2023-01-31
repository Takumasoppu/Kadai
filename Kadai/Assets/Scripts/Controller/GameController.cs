using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameController : MonoBehaviour
{
    [SerializeField, Header("PlayerControllerの格納")]
    private PlayerController _pC = default;

    [SerializeField, Header("1Wave目の敵格納")]
    private EnemyController[] _firstWaveEnemy = default;

    [SerializeField, Header("2Wave目の敵格納")]
    private EnemyController[] _secondWaveEnemy = default;

    [SerializeField, Header("3Wave目の敵格納")]
    private EnemyController _finalWaveEnemy = default;

    [SerializeField, Header("オブジェクトプールの格納")]
    private ObjectPool _objPoolScript = default;

    [SerializeField, Header("オブジェクトプールの格納2")]
    private GameObject _objPool = default;

    [Header("1Wave管理フラグ")]
    private bool _firstWave = true;

    [Header("2Wave管理フラグ")]
    private bool _secondWave = false;

    [Header("3Wave管理フラグ")]
    private bool _finalWave = false;

    /// <summary>
    /// ここで他のスクリプトの挙動を管理
    /// </summary>
    private void Update()
    {
        Debug.Log(_secondWave);

        //プレイヤーの移動のメソッド呼び出し
        _pC.PlayerInput();


        //##########[ 1Wave目の敵の処理 ]##############

        //1Wave目の終了フラグがオンの間続く
        if (_firstWave == true)
        {
            _firstWaveEnemy[0].ActiveObj();

            _firstWaveEnemy[1].ActiveObj();
        }

        //配列内の敵がすべて見えなくなったら(倒されたら) 1Wave目を終了して2Wave目をスタートする
        if (_firstWaveEnemy[0].GetComponent<SpriteRenderer>().enabled == false &&
            _firstWaveEnemy[1].GetComponent<SpriteRenderer>().enabled == false &&
            _firstWave == true)
        {
            //1Wave目終了
            _firstWave = false;

            //2Wave目スタート
            _secondWave = true;

        }


        //##########[ 2Wave目の敵の処理 ]##############

        //2Wave開始
        if (_secondWave == true)
        {
            _secondWaveEnemy[0].ActiveObj();

            _secondWaveEnemy[1].ActiveObj();

        }

        //配列内の敵がすべて見えなくなったら(倒されたら) 2Wave目を終了して3Wave目をスタートする
        if (_secondWaveEnemy[0].GetComponent<SpriteRenderer>().enabled == false &&
            _secondWaveEnemy[1].GetComponent<SpriteRenderer>().enabled == false &&
            _secondWave == true)
        {
            //2Wave目終了
            _secondWave = false;

            //3Wave目スタート
            _finalWave = true;
        }


        //##########[ 3Wave目の敵の処理 ]##############

        //finalWave開始
        if(_finalWave == true)
        {
            _finalWaveEnemy.ActiveObj();
        }

        //配列内の敵がすべて見えなくなったら(倒されたら) 3Wave目を終了して4Wave目をスタートする
        if (_finalWaveEnemy.GetComponent<SpriteRenderer>().enabled == false)
        {
            _finalWave = false;
        }
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


        //##########[  1Wave目の敵の処理  ]##############

        //1Wave目の終了フラグがオンの間続く
        if (_firstWave == true)
        {
            //1Wave目の敵の物理挙動
            _firstWaveEnemy[0].EnemyMove();

            _firstWaveEnemy[1].EnemyMove();

            //1Wave目の現在の敵のポジションの更新
            _firstWaveEnemy[0].NowEnemyPosition();

            _firstWaveEnemy[1].NowEnemyPosition();

            //1Wave目の敵が攻撃する際の弾の配置
            _firstWaveEnemy[0].EnemyShot();

            _firstWaveEnemy[1].EnemyShot();

            //1Wave目の敵を倒した際の処理
            _firstWaveEnemy[0].EnemyOut();

            _firstWaveEnemy[1].EnemyOut();

        }

        //##########[  2Wave目の敵の処理  ]##############

        if (_secondWave == true)
        {
            //2Wave目の敵の挙動
            _secondWaveEnemy[0].EnemyMove();

            _secondWaveEnemy[1].EnemyMove();


            //2Wave目の敵のポジションの更新
            _secondWaveEnemy[0].NowEnemyPosition();

            _secondWaveEnemy[1].NowEnemyPosition();


            //2Wave目の敵が攻撃する際の弾の配置
            _secondWaveEnemy[0].EnemyShot();

            _secondWaveEnemy[1].EnemyShot();

            
            //2Wave目の敵が倒されたときの処理
            _secondWaveEnemy[0].EnemyOut();

            _secondWaveEnemy[1].EnemyOut();
           
        }

        //##########[  finalWave目の敵の処理  ]##############
        if(_finalWave == true)
        {
            _finalWaveEnemy.EnemyMove();

            _finalWaveEnemy.NowEnemyPosition();

            _finalWaveEnemy.EnemyShot();

            _finalWaveEnemy.EnemyOut();

        }  
    }
}
