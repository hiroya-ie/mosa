using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameEvent : MonoBehaviour
{
    //当たったら跳ね返るタイプの当たり判定
    private void OnCollisionEnter(Collision collision)
    {
        //墜落イベント呼び出し
    }
    //当たったら通り抜けるタイプの当たり判定
    private void OnTriggerEnter(Collider other)
    {
        //ニアミス呼び出し
        //加速リングイベント呼び出し
    }

    public void AccelerateEvent()
    {
        //加速リングに当たったときの処理

        //羽ばたきステータス設定
        //スコア加算
        //加速する音を再生
    }
}
