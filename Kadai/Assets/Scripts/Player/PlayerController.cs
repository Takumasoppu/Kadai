using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;


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

    //プレイヤーの現在位置
    private Vector2 _playerPos;

    //RigidBody格納
    private Rigidbody2D _rd2b = default;

    /// <summary>
    /// 最初の読み込み
    /// </summary>
    private void Awake()
    {
        //コンポーネント取得
        _rd2b = GetComponent<Rigidbody2D>();
        StartCoroutine("Fire");

    }

    /// <summary>
    /// 入力された値の制御
    /// </summary>
    public void PlayerInput()
    {
        _leftInput = Gamepad.current.leftStick.ReadValue();
    }

    /// <summary>
    /// プレイヤーの挙動(コントローラー)
    /// </summary>
    public void PlayerMove()
    {
        //移動量
        _rd2b.velocity = _leftInput * _playerSpeed;
    }

    /// <summary>
    /// プレイヤーの現在位置
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
}
