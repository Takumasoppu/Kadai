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
    private EnemyController[] _thirdWaveEnemy = default;

    [SerializeField, Header("4Wave目の敵格納")]
    private EnemyController[] _fourthWaveEnemy = default;

    [SerializeField, Header("オブジェクトプールの格納")]
    private ObjectPool _objPoolScript = default;

    [SerializeField, Header("オブジェクトプールの格納2")]
    private GameObject _objPool = default;

    [Header("1Wave管理フラグ")]
    private bool _firstWave = true;

    [Header("2Wave管理フラグ")]
    private bool _secondWave = false;

    [Header("3Wave管理フラグ")]
    private bool _thirdWave = false;

    [Header("4Wave管理フラグ")]
    private bool _fourthWave = false;

    [Header("FinalWave管理フラグ")]
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

            _secondWaveEnemy[2].ActiveObj();

        }
        //配列内の敵がすべて見えなくなったら(倒されたら) 2Wave目を終了して3Wave目をスタートする
        if (_secondWaveEnemy[0].GetComponent<SpriteRenderer>().enabled == false &&
           _secondWaveEnemy[1].GetComponent<SpriteRenderer>().enabled == false &&
           _secondWaveEnemy[2].GetComponent<SpriteRenderer>().enabled == false &&
           _secondWave == true)
        {
            //2Wave目終了
            _secondWave = false;

            //3Wave目スタート
            _thirdWave = true;
        }


        //##########[ 3Wave目の敵の処理 ]##############

        //3Wave開始
        if(_thirdWave == true)
        {
            _thirdWaveEnemy[0].ActiveObj();
            _thirdWaveEnemy[1].ActiveObj();
            _thirdWaveEnemy[2].ActiveObj();
            _thirdWaveEnemy[3].ActiveObj();
        }

        //配列内の敵がすべて見えなくなったら(倒されたら) 3Wave目を終了して4Wave目をスタートする
        if (_thirdWaveEnemy[0].GetComponent<SpriteRenderer>().enabled == false &&
           _thirdWaveEnemy[1].GetComponent<SpriteRenderer>().enabled == false &&
           _thirdWaveEnemy[2].GetComponent<SpriteRenderer>().enabled == false &&
           _thirdWaveEnemy[3].GetComponent<SpriteRenderer>().enabled == false &&
           _thirdWave == true)
        {
            _thirdWave = false;

            _fourthWave = true;
        }


        //##########[ 4Wave目の敵の処理 ]##############

        if(_fourthWave == true)
        {
            _fourthWaveEnemy[0].ActiveObj();

            _fourthWaveEnemy[1].ActiveObj();

            _fourthWaveEnemy[2].ActiveObj();

            _fourthWaveEnemy[3].ActiveObj();
        }

        //配列内の敵がすべて見えなくなったら(倒されたら) 4Wave目を終了してfinalWaveをスタートする
        if (_fourthWaveEnemy[0].GetComponent<SpriteRenderer>().enabled == false &&
            _fourthWaveEnemy[1].GetComponent<SpriteRenderer>().enabled == false &&
            _fourthWaveEnemy[2].GetComponent<SpriteRenderer>().enabled == false &&
            _fourthWaveEnemy[3].GetComponent<SpriteRenderer>().enabled == false &&
            _fourthWave == true)
        {
            _fourthWave = false;

            _finalWave = true;
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

            _secondWaveEnemy[2].EnemyMove();


            //2Wave目の敵のポジションの更新
            _secondWaveEnemy[0].NowEnemyPosition();

            _secondWaveEnemy[1].NowEnemyPosition();

            _secondWaveEnemy[2].NowEnemyPosition();


            //2Wave目の敵が攻撃する際の弾の配置
            _secondWaveEnemy[0].EnemyShot();

            _secondWaveEnemy[1].EnemyShot();

            _secondWaveEnemy[2].EnemyShot();


            //2Wave目の敵が倒されたときの処理
            _secondWaveEnemy[0].EnemyOut();

            _secondWaveEnemy[1].EnemyOut();

            _secondWaveEnemy[2].EnemyOut();

        }

        //##########[  3Wave目の敵の処理  ]##############
        if(_thirdWave == true)
        {
            _thirdWaveEnemy[0].EnemyMove();

            _thirdWaveEnemy[1].EnemyMove();

            _thirdWaveEnemy[2].EnemyMove();

            _thirdWaveEnemy[3].EnemyMove();


            _thirdWaveEnemy[0].NowEnemyPosition();

            _thirdWaveEnemy[1].NowEnemyPosition();

            _thirdWaveEnemy[2].NowEnemyPosition();

            _thirdWaveEnemy[3].NowEnemyPosition();


            _thirdWaveEnemy[0].EnemyShot();

            _thirdWaveEnemy[1].EnemyShot();

            _thirdWaveEnemy[2].EnemyShot();

            _thirdWaveEnemy[3].EnemyShot();


            _thirdWaveEnemy[0].EnemyOut();

            _thirdWaveEnemy[1].EnemyOut();

            _thirdWaveEnemy[2].EnemyOut();

            _thirdWaveEnemy[3].EnemyOut();
        }

        //##########[  3Wave目の敵の処理  ]##############
        if(_fourthWave == true)
        {
            //2Wave目の敵の挙動
            _fourthWaveEnemy[0].EnemyMove();

            _fourthWaveEnemy[1].EnemyMove();

            _fourthWaveEnemy[2].EnemyMove();

            _fourthWaveEnemy[3].EnemyMove();


            //2Wave目の敵のポジションの更新
            _fourthWaveEnemy[0].NowEnemyPosition();

            _fourthWaveEnemy[1].NowEnemyPosition();

            _fourthWaveEnemy[2].NowEnemyPosition();

            _fourthWaveEnemy[3].NowEnemyPosition();


            //2Wave目の敵が攻撃する際の弾の配置
            _fourthWaveEnemy[0].EnemyShot();

            _fourthWaveEnemy[1].EnemyShot();

            _fourthWaveEnemy[2].EnemyShot();

            _fourthWaveEnemy[3].EnemyShot();


            //2Wave目の敵が倒されたときの処理
            _fourthWaveEnemy[0].EnemyOut();

            _fourthWaveEnemy[1].EnemyOut();

            _fourthWaveEnemy[2].EnemyOut();

            _fourthWaveEnemy[3].EnemyOut();
        }

        //##########[  FinalWaveの敵の処理  ]##############
        if (_finalWave == true)
        {

        }
    }
}
