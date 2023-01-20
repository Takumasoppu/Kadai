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
    private Vector2 _enemyPosition = default;

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

    [SerializeField, Header("死亡演出用の数字")]
    private Vector2 _deathSpeed = default;

    [SerializeField, Header("攻撃される弾のオブジェクト格納")]
    private GameObject _receiveBullet = default;

    [Header("敵の半径")]
    public float _enemyRadius;

    [Header("攻撃される弾の半径")]
    public float _bulletRadius = default;

    [Header("当たり判定フラグ")]
    private bool _hitFlag = default;

    /// <summary>
    /// 最初に読み込み
    /// </summary>
    private void Start()
    {
        _enemyRd2b = this.GetComponent<Rigidbody2D>();
        _enemyRadius = this.gameObject.transform.localScale.x / 2 ;
        _bulletRadius = _receiveBullet.transform.localScale.x / 2 ;

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
        //trueが帰ってきたら実行
        if (MakeCircleCollision.CircleCollider(_enemyPosition, _enemyRadius, _receiveBullet.transform.position, _bulletRadius))
        {
            _hitFlag = true;
        } 

        if(_hitFlag == true)
        {
            Debug.Log("当たっているよ！");
            _deleteTime += Time.deltaTime;
            this.transform.localScale = -_deathSpeed * _deleteTime;
        }
        else
        {
            Debug.Log("この表示がされたらエラー");
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

    public void Update()
    {
        Debug.Log(_enemyRadius);
        Debug.Log(_bulletRadius);

        Debug.Log(MakeCircleCollision.CircleCollider(_enemyPosition, _enemyRadius, _receiveBullet.transform.position, _bulletRadius));
    }




}
