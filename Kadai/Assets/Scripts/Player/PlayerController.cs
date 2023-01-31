using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;


public class PlayerController : MonoBehaviour
{
    [SerializeField, Header("左スティックの入力値")]
    private Vector2 _leftInput = default;

    [SerializeField, Header("オブジェクトプール")]
    private ObjectPool _oP = default;

    [SerializeField, Header("発射インターバル")]
    private float _shotInterval;

    [SerializeField, Header("プレイヤーの移動速度調整用")]
    private float _playerSpeed = default;

    [SerializeField, Header("弾のダメージ")]
    public int _bulletDamage = default;

    [Header("2つのオブジェクトの中心位置からの距離の差")]
    private Vector3 _v3Delta = default;

    [SerializeField, Header("プレイヤーのHP")]
    private int _playerHp = default;

    [SerializeField, Header("プレイヤーのオーディオソース")]
    private AudioController _audioController = default;

    //プレイヤーの現在位置
    private Vector2 _playerPos;

    //RigidBody格納
    private Rigidbody2D _rb2d = default;

    //プレイヤーが死んだときのフラグ
    private bool _deleteFlag = default;

    /// <summary>
    /// 最初の読み込み
    /// </summary>
    private void Awake()
    {
        //コンポーネント取得
        _rb2d = GetComponent<Rigidbody2D>();
        StartCoroutine("Fire");
        _deleteFlag = false;

    }

    /// <summary>
    /// 入力された値の制御
    /// </summary>
    public void PlayerInput()
    {
        //左スティックから入力された値を読み取る
        _leftInput = Gamepad.current.leftStick.ReadValue();
    }

    /// <summary>
    /// プレイヤーが敵の弾に当たった時の処理
    /// (妥協ポイント：本当は自作したかったが,他の事に時間をかけすぎてしまった)
    /// </summary>

    private void OnTriggerEnter2D(Collider2D obj)
    {
        if(obj.gameObject.tag == "EnemyBullet")
        {
            _playerHp = _playerHp - 1;
        }
       
        //プレイヤーのHPが0になったら倒されたときの処理を呼ぶ
        if(_playerHp < 1)
        {
            _deleteFlag = true;
            DeletePlayer();
        }
    }

    /// <summary>
    /// 敵に倒されてしまった時の処理
    /// </summary>
    public void DeletePlayer()
    {
        _audioController.DeleteSound();

        //コントローラーのYボタンを押したときにリトライできる
        if(Gamepad.current.buttonNorth.isPressed )
        {
            SceneManager.LoadScene("Test");
        }

        //コントローラーのAボタンを押したときにタイトルに戻ることができる
        if(Gamepad.current.buttonSouth.isPressed)
        {
            return;
        }
    }

    /// <summary>
    /// プレイヤーの挙動(コントローラー)
    /// </summary>
    public void PlayerMove()
    {
        //移動量
        _rb2d.velocity = _leftInput * _playerSpeed;
    }

    /// <summary>
    /// プレイヤーの現在位置更新用メソッド
    /// </summary>
    public void NowPlayerPosition()
    {
        _playerPos = this.transform.position;
    }

    /// <summary>
    /// 弾の発射の処理
    /// </summary>
    /// <returns></returns>
    public IEnumerator Fire()
    {
        while (true)
        {
            //コントローラーのBボタンを押したとき
            if (Gamepad.current.buttonEast.isPressed)
            {
                //オブジェクトプールで生成した弾をプレイヤーの少し上に配置
                _oP.GetObject().GetComponent<Transform>().position = new Vector2(_playerPos.x,_playerPos.y + 0.7f);

                //弾が見えるようにする処理
                _oP.GetObject().SetActive(true);
                _oP.GetObject().GetComponent<SpriteRenderer>().enabled = true;

            }
            yield return new WaitForSeconds(_shotInterval);
        }

    }

    /// <summary>
    /// デバッグ用Update
    /// </summary>
    private void Update()
    {
        Debug.Log(_playerHp);
    }
}
