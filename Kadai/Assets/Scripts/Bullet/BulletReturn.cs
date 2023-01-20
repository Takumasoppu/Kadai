using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletReturn : MonoBehaviour
{
    SpriteRenderer _sPr = default;


    /// <summary>
    /// 弾のSpriteRenderer取得
    /// </summary>
    private void Awake()
    {
        _sPr = this.GetComponent<SpriteRenderer>();   
    }

    /// <summary>
    /// 弾が画面外に出たときに弾を見えなくする処理
    /// </summary>
    private void OnBecameInvisible()
    {
        _sPr.enabled = false;
        this.gameObject.SetActive(false);
    }

}
