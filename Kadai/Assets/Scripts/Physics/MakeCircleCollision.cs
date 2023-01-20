using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MakeCircleCollision : MonoBehaviour
{
    /// <summary>
    /// 円同士の当たり判定
    /// </summary>
    /// <param name="_receiveObj">衝突されるオブジェクトの座標</param>
    /// <param name="_receiveObjRadius">衝突されるオブジェクトの半径</param>
    /// <param name="_hitObj">衝突するオブジェクトの座標</param>
    /// <param name="_hitObjRadius">衝突するオブジェクトの半径</param>
    /// <returns></returns>
    public static bool CircleCollider(Vector2 _receiveObj, float _receiveObjRadius, Vector2 _hitObj, float _hitObjRadius)
    {
        //2つのオブジェクトの距離を計算
        float _sumPosX = _receiveObj.x - _hitObj.x;
        float _sumPosY = _receiveObj.y - _hitObj.y;

        //三平方の定理を使用してx座標とｙ座標の二乗の和を求める
        float _sumSquaredPos = _sumPosX * _sumPosX + _sumPosY * _sumPosY;

        //半径の和を二乗する
        float _sumRadius = _receiveObjRadius + _hitObjRadius;

        // 距離と半径の和を比較して、半径の和の方が大きいなら接触している
        if (_sumSquaredPos < _sumRadius + _sumRadius)
        {
            return true;
        }

        return false;
    }

    

}
