using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectPool : MonoBehaviour
{
    //生成するオブジェクト
    [SerializeField, Header("生成プレハブ")]
    private GameObject _generateObject = default;

    [SerializeField, Header("生成限界数")]
    private int _maxCount = default;

    [SerializeField, Header("初期生成数")]
    private int _firstCount = default;


    /// <summary>
    /// 生成した弾の個数を格納する関数
    /// </summary>
    /// <returns></returns>
    public int _getfirstCount()
    {
        return _firstCount;
    }

    [SerializeField, Header("生成した弾を格納(_firstCountと同じ数値にする)")]
    public GameObject[] Bullets;

    /// <summary>
    /// 弾丸のオブジェクトプール
    /// </summary>
    private void Awake()
    {
        for(int i = 0; i < _firstCount; i++)
        {
            //GameObject型で生成
            GameObject InstanceObject = Instantiate(_generateObject, Vector3.zero, Quaternion.identity, this.transform);

            //生成したものを配列にいれる
            Bullets[i] = InstanceObject;
        }

        //生成されたときに以下の状態になる
        foreach (Transform objectInPool in this.transform)
        {
            objectInPool.gameObject.SetActive(false);
            objectInPool.gameObject.GetComponent<SpriteRenderer>().enabled = false;
        }
    }

    /// <summary>
    /// オブジェクトプールから非アクティブのオブジェクトを見つける
    /// </summary>
    /// <returns>見つかったオブジェクト</returns>
    public GameObject GetObject()
    {
        GameObject foundObject = null;

        //オブジェクトプールの全体を確認する処理
        foreach (Transform objectInPool in this.transform)
        {
            
            if(objectInPool.gameObject.GetComponent<SpriteRenderer>().enabled)
            {
                continue;
            }

            //見つかった場合
            else
            {
                foundObject = objectInPool.gameObject;
                break;
            }
        }


        return foundObject;
    }


}
