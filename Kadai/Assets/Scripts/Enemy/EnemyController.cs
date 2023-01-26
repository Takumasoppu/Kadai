using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;


/// <summary>
/// これは雑魚敵のスクリプトです。
/// </summary>
public class EnemyController : MonoBehaviour
{
    //#############[ RigidBody ]###############

    [Header("敵のRigidbodyの変数名")]
    private Rigidbody2D _enemyRd2b = default;


    //#############[ 他 ]###############

    [SerializeField, Header("オブジェクトプール")]
    private ObjectPool _enemyOp = default;

    [SerializeField, Header("攻撃されるオブジェクトプールを格納")]
    private ObjectPool _receiveBulletsPooling = default;

    [SerializeField, Header("PlayerControllerの格納")]
    private PlayerController _playerController = default;


    //#############[ GameObject系 ]###############

    [SerializeField, Header("攻撃される弾の親オブジェクトを格納")]
    private GameObject _receiveBullet = default;

    [SerializeField, Header("プレイヤーのゲームオブジェクト格納")]
    private GameObject _player = default;

    [SerializeField, Header("敵を倒したときに敵が場外に行くようにするポジション")]
    private GameObject _deathPoint = default;

    [SerializeField, Header("オブジェクトプール")]
    private GameObject[] _bullets = new GameObject[0];

    [SerializeField, Header("現在アクティブになっている弾を格納")]
    private GameObject[] _activeBullets = new GameObject[0];


    //#############[ 数値系 ]###############

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

    [Header("中心座標同士の距離の二乗")]
    public float _fDistanceSq = default;

    [SerializeField,Header("現在の時間経過")]
    float _nowTime = default;

    [SerializeField, Header("敵のHP")]
    public int _enemyHp = default;

    [SerializeField, Header("弾のカウント")]
    private int _bulletCount = default;

    [Header("2つのオブジェクトの中心位置からの距離の差")]
    public Vector3 _v3Delta = default;

    [SerializeField, Header("敵の現在位置")]
    public Transform _enemyTranform;


    //#############[ フラグ系 ]###############

    [Header("初動停止確認フラグ")]
    private bool _stopFlag = default;

    [Header("敵死亡フラグ")]
    private bool _deleteFlag = default;


    /// <summary>
    /// 最初に読み込み
    /// </summary>
    private void Start()
    {
        _enemyRd2b = this.GetComponent<Rigidbody2D>();

        for (int i = 0; i < _receiveBulletsPooling._getfirstCount(); i++)
        {
            _bullets[i] = _receiveBulletsPooling.Bullets[i];
        }
    }

    /// <summary>
    /// 現在アクティブになっている弾を格納
    /// </summary>
    public void ActiveObj()
    {
        for (_bulletCount = 0; _bulletCount < _bullets.Length; _bulletCount++)
        {
            //弾がアクティブだったら格納
            if (_bullets[_bulletCount].activeSelf)
            {
                _activeBullets[_bulletCount] = _bullets[_bulletCount];
                return;
            }
            
        }
    }

    /// <summary>
    /// 敵が出てくるときの動き
    /// </summary>
    public void EnemyMove()
    {
        _activeTime += Time.deltaTime;

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

        if (_fDistanceSq < (_hitArea * _hitArea))
        {
             //ダメージ計算
            _enemyHp = _enemyHp - _playerController._bulletDamage;

            //プレイヤーのオブジェクトプールの子オブジェクトを非アクティブにする
            _activeBullets[_bulletCount].gameObject.SetActive(false);

            //非アクティブになったら画面外にあるオブジェクトプールに戻る
            _activeBullets[_bulletCount].transform.position = _receiveBullet.transform.position;
        }

        if (_enemyHp < 1)
        {
            //敵を見えなくする
            this.gameObject.SetActive(false);

            //倒されたときに対象のポイントに移動する
            this.gameObject.transform.position = _deathPoint.transform.position;
        }

    }

    /// <summary>
    /// 現在の敵のポジション
    /// </summary>
    public void NowEnemyPosition()
    {
        _enemyTranform = this.gameObject.transform;
    }

    /// <summary>
    /// 敵とプレイヤーの角度を求める
    /// </summary>
    /// <returns></returns>
    public float AimFloat()
    {
        float _xKyori = _player.transform.position.x - _enemyTranform.position.x ;
        float _yKyori = _player.transform.position.y - _enemyTranform.position.y ;
        return Mathf.Atan2(_xKyori, _yKyori);
    }

    /// <summary>
    /// 敵が攻撃してくる時の処理
    /// </summary>
    public void Enemyshot()
    {
        //フラグがオンになってから開始
        if (_stopFlag == true && _deleteFlag == false)
        {
            _nowTime += Time.deltaTime;

            if (_nowTime > _shotInterval)
            {
                //オブジェクトプールで生成した弾を敵の少し下に配置
                _enemyOp.GetObject().GetComponent<Transform>().position = new Vector2(_enemyTranform.position.x, _enemyTranform.position.y - 0.7f);

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
