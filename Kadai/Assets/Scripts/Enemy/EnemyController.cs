using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// これは全方位発射雑魚敵のスクリプトです。
/// </summary>
public class EnemyController : MonoBehaviour
{

    [Header("敵のRigidbodyの変数名")]
    private Rigidbody2D _enemyRd2b = default;

    [SerializeField, Header("オブジェクトプール")]
    private ObjectPool _enemyOp = default;

    [SerializeField, Header("敵が動く際のX方向のスピード")]
    private float _enemySpeedx = default;

    [SerializeField, Header("敵が動く際のY方向のスピード")]
    private float _enemySpeedy = default;

    [SerializeField, Header("敵が緩やかに下に移動する際のスピード")]
    private float _enemySlowlySpeed = default;

    [SerializeField, Header("敵の現在位置")]
    private Vector3 _enemyPosition;

    [SerializeField, Header("プレイヤーのゲームオブジェクト格納")]
    private GameObject _player = default;

    [SerializeField, Header("敵のHP")]
    private int _enemyHp = default;

    [SerializeField, Header("停止までの時間")]
    private float _stopTime = default;

    [SerializeField, Header("動ける時間")]
    private float _activeTime = default;

    [SerializeField, Header("死ぬまでの時間")]
    private float _deleteTime = default;

    [SerializeField, Header("発射インターバル")]
    private float _shotInterval;

    //[SerializeField, Header("死亡演出用の数字")]
    //private Vector2 _deathSpeed = default;

    [SerializeField, Header("オブジェクトプールのスクリプトの中のリストを取得する")]
    private List<Transform> _objPoolList = default;

    [SerializeField, Header("攻撃される弾のオブジェクト格納")]
    private GameObject _receiveBullet;

    //[Header("敵の半径")]
    //public float _enemyRadius;

    [Header("当たり判定調整用変数")]
    public float _hitArea;

    //[Header("当たり判定フラグ")]
    //private bool _hitFlag = default;

    [Header("中心位置からの距離の差")]
    public Vector3 _v3Delta;

    [Header("中心座標同士の距離の二乗")]
    public float _fDistanceSq;

    /// <summary>
    /// 最初に読み込み
    /// </summary>
    private void Awake()
    {
        _enemyRd2b = this.GetComponent<Rigidbody2D>();
        _enemyPosition = this.gameObject.transform.localPosition;
    }

    /// <summary>
    /// 敵が出てくるときの動き
    /// </summary>
    public void EnemyMove()
    {
        _activeTime += Time.deltaTime;

        _enemyRd2b.velocity = new Vector2(_enemySpeedx, _enemySpeedy);

        //動ける時間を過ぎたら緩やかに下に移動する
        if (_activeTime > _stopTime)
        {
            _enemyRd2b.velocity = new Vector2(_enemySpeedx * 0, _enemySlowlySpeed);
        }
    }

    /// <summary>
    /// 敵を倒した時の演出
    /// </summary>
    public void EnemyOut()
    {
        //_receiveBullet.transform.position = _objPoolList.

        //ここから下で当たり判定の処理
        _v3Delta = _enemyPosition - _receiveBullet.transform.position;

        _fDistanceSq = _v3Delta.x * _v3Delta.x +
                       _v3Delta.y * _v3Delta.y;

        if (_fDistanceSq < (_hitArea * _hitArea))
        {
            Debug.Log("当たっているよ");
            this.gameObject.SetActive(false);
        }
        else
        {
            Debug.Log("当たっていないか、弾が発射されていないか、エラーだよ");
        }
    }

    /// <summary>
    /// 敵とプレイヤーの角度を求める
    /// </summary>
    /// <returns></returns>
    public float AimFloat()
    {
        float _xKyori = _player.transform.position.x - _enemyPosition.x ;
        float _yKyori = _player.transform.position.y - _enemyPosition.y ;
        return Mathf.Atan2(_xKyori, _yKyori);
    }

    /// <summary>
    /// 敵が攻撃してくる時の処理
    /// </summary>
    public IEnumerator Enemyshot()
    {
        while (true)
        {
            //オブジェクトプールで生成した弾を敵の少し下に配置
            _enemyOp.GetObject().GetComponent<Transform>().position = new Vector2(_enemyPosition.x,_enemyPosition.y - 0.7f);

            //弾が見えるようにする処理
            _enemyOp.GetObject().SetActive(true);
            _enemyOp.GetObject().GetComponent<SpriteRenderer>().enabled = true;

            yield return new WaitForSeconds(_shotInterval);
        }
    }

    //デバッグ用update
    public void Update()
    {
        
        Debug.Log(_receiveBullet.transform.position);
        Debug.Log(_enemyPosition);
        Debug.Log(_fDistanceSq);
    }




}
