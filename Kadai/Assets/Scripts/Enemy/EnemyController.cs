using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

/// <summary>
/// 雑魚敵のスクリプト
/// </summary>
public class EnemyController : MonoBehaviour
{
    //#############[ RigidBody ]###############

    [Header("敵のRigidbody")]
    private Rigidbody2D _enemyRd2b = default;

    [Header("敵の弾のRigidbody")]
    private Rigidbody2D _bulletRd2b = default;

    //#############[ 他 ]###############

    [SerializeField, Header("オブジェクトプール")]
    private ObjectPool _enemyOp = default;

    [SerializeField, Header("攻撃されるオブジェクトプールを格納")]
    private ObjectPool _receiveBulletsPooling = default;

    [SerializeField, Header("PlayerControllerの格納")]
    private PlayerController _playerController = default;

    [SerializeField, Header("GameControllerの格納")]
    private GameController _gameController = default;


    //#############[ GameObject系 ]###############

    [SerializeField, Header("攻撃される弾の親オブジェクトを格納")]
    private GameObject _receiveBullet = default;

    [SerializeField, Header("プレイヤーのゲームオブジェクト格納")]
    private GameObject _player = default;

    [SerializeField, Header("敵を倒したときに敵が場外に行くようにするポジション")]
    private GameObject _deathPoint = default;

    [SerializeField, Header("敵の弾のオブジェクトプール")]
    private GameObject _enemyOpjPooling = default;

    [SerializeField, Header("オブジェクトプール")]
    private GameObject[] _EnemyBullets = new GameObject[0];

    [SerializeField, Header("現在アクティブになっている弾を格納")]
    private GameObject[] _activeBullets = new GameObject[0];


    //#############[ 数値系 ]###############

    //中心座標同士の距離の二乗
    private float _fDistanceSq = default;

    //現在の時間経過
    private float _nowTime = default;

    //弾のカウント
    private int _bulletCount = default;

    //2つのオブジェクトの中心位置からの距離の差
    private Vector3 _v3Delta = default;

    //敵の現在位置
    private Transform _enemyTranform;

    //発射角度
    private float _enemyBulletRad = default;

    [SerializeField, Header("敵のHP")]
    public int _enemyHp = default;

    [SerializeField, Header("停止までの時間")]
    private float _stopTime = default;

    [SerializeField, Header("動ける時間")]
    private float _activeTime = default;

    [SerializeField, Header("発射インターバル")]
    private float _shotInterval = default;

    [SerializeField, Header("敵が動く際のX方向のスピード")]
    private float _enemySpeedx = default;

    [SerializeField, Header("敵が動く際のY方向のスピード")]
    private float _enemySpeedy = default;

    [SerializeField, Header("敵が緩やかに下に移動する際のスピード")]
    private float _enemySlowlySpeed = default;

    [Header("当たり判定調整用変数")]
    public float _hitArea = default;


    //#############[ フラグ系 ]###############

    //初動停止確認フラグ
    private bool _stopFlag = false;

    //########################################

    /// <summary>
    /// 最初の読み込み（主に弾の格納）
    /// </summary>
    private void Start()
    {
        _enemyRd2b = this.GetComponent<Rigidbody2D>();

        _bulletRd2b = _enemyOpjPooling.GetComponent<Rigidbody2D>();

        //オブジェクトプールで生成された分配列に格納
        for (int i = 0; i < _receiveBulletsPooling._getfirstCount(); i++)
        {
            _EnemyBullets[i] = _receiveBulletsPooling.Bullets[i];
        }

    }

    /// <summary>
    /// 現在アクティブになっているプレイヤーの弾を格納
    /// </summary>
    public void ActiveObj()
    {
        for (_bulletCount = 0; _bulletCount < _EnemyBullets.Length; _bulletCount++)
        {
            //弾がアクティブだったら格納
            if (_EnemyBullets[_bulletCount].activeSelf)
            {
                _activeBullets[_bulletCount] = _EnemyBullets[_bulletCount];
                return;
            }
        }
    }

    /// <summary>
    /// 敵が出てくるときの動き
    /// </summary>
    public void EnemyMove()
    {
        //動ける時間を更新
        _activeTime += Time.deltaTime;

        //敵の移動
        _enemyRd2b.velocity = new Vector2(_enemySpeedx, _enemySpeedy);

        //動ける時間を過ぎたら緩やかに下に移動する処理
        if (_activeTime > _stopTime)
        {
            _enemyRd2b.velocity = new Vector2(_enemySpeedx * 0, _enemySlowlySpeed);
            _stopFlag = true;
        }
    }


    /// <summary>
    /// 敵を倒した時の処理
    /// </summary>
    public void EnemyOut()
    {
        //敵とプレイヤーが撃った弾の距離
        _v3Delta = _enemyTranform.position - _activeBullets[_bulletCount].transform.position;

        //それぞれの距離を二乗
        _fDistanceSq = _v3Delta.x * _v3Delta.x +
                       _v3Delta.y * _v3Delta.y;

        ///距離の二乗よりヒットエリア(自分で指定したエリア)の二乗の方が大きかったら当たっている
        if (_fDistanceSq < (_hitArea * _hitArea))
        {
            //ダメージ計算
            _enemyHp = _enemyHp - _playerController._bulletDamage;

            //プレイヤーのオブジェクトプールの子オブジェクトのスプライトレンダラーを非アクティブにする
            _activeBullets[_bulletCount].GetComponent<SpriteRenderer>().enabled = false;

            //非アクティブになったら画面外にあるオブジェクトプールに戻る
            _activeBullets[_bulletCount].transform.position = _receiveBullet.transform.position;

        }

        //プレイヤーの攻撃ダメージが敵のHPを超えたら
        if (_enemyHp < _playerController._bulletDamage)
        {
            //敵を見えなくする
            this.gameObject.GetComponent<SpriteRenderer>().enabled = false;

            //倒されたときに対象のポイントに移動する
            this.gameObject.transform.position = _deathPoint.transform.position;
        }
    }


    /// <summary>
    /// 現在の敵のポジション更新用
    /// </summary>
    public void NowEnemyPosition()
    {
        _enemyTranform = this.gameObject.transform;
    }

    /// <summary>
    /// 敵が攻撃してくる時の処理
    /// </summary>
    public void EnemyShot()
    {
        //フラグがオンになってから開始
        if (_stopFlag == true)
        {
            _nowTime += Time.deltaTime;

            if (_nowTime > _shotInterval)
            {
                //オブジェクトプールで生成した弾を敵の少し下に配置
                _enemyOp.GetObject().GetComponent<Transform>().position = new Vector2(_enemyTranform.position.x, _enemyTranform.position.y);

                //弾が見えるようにする処理
                _enemyOp.GetObject().SetActive(true);
                _enemyOp.GetObject().GetComponent<SpriteRenderer>().enabled = true;

                _nowTime = 0;
            }
        }
        else
        {
            return;
        }
    }

    /// <summary>
    /// 敵の攻撃(拡散弾)
    /// </summary>
    public void EnemyShot2()
    {
        return;
    }

    /// <summary>
    /// 敵とプレイヤーの角度を求める
    /// </summary>
    /// <returns></returns>
    /*public float AimFloat()
    {
        float _xKyori = _player.transform.position.x - _enemyTranform.position.x ;
        float _yKyori = _player.transform.position.y - _enemyTranform.position.y ;
        return Mathf.Atan2(_xKyori, _yKyori);
    }*/

    //デバッグ用update
    public void Update()
    {
        //Debug.Log(_receiveBulletPool.transform.position) ;
        //Debug.Log(_enemyTranform);
        //Debug.Log(_fDistanceSq);
        //Debug.Log(_nowTime);
        //Debug.Log(_stopFlag);
        //Debug.Log(_enemyHp);
        //Debug.Log(_bulletCount);
        //Debug.Log(_bullets.Length);
    }

}
