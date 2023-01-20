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

    [Header("リストの中身の確認")]
    public int[] intArray; 

    [SerializeField, Header("オブジェクトプールのリスト変数")]
    public List<Transform> BulletPos = new List<Transform>();

    /// <summary>
    /// 弾丸のオブジェクトプール
    /// </summary>
    private void Awake()
    {
        for(int i = 0; i < _firstCount; i++)
        {
            //初期生成しつつリストに追加
            BulletPos.Add(Instantiate(_generateObject, Vector3.zero, Quaternion.identity, this.transform).transform);
        }

        //生成されたときに以下の状態になる
        foreach (Transform objectInPool in this.transform)
        {
            objectInPool.gameObject.SetActive(false);
            objectInPool.gameObject.GetComponent<SpriteRenderer>().enabled = false;
        }

    }
    /// <summary>
    /// オブジェクトプールからオブジェクトを見つける
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
