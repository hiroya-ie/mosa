using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameEvent : MonoBehaviour
{
    //衝突パーティクル
    [SerializeField] GameObject crashParticle;
    //当たったら跳ね返るタイプの当たり判定
    private void OnCollisionEnter(Collision collision)
    {
        //墜落イベント呼び出し
        if (collision.gameObject.tag == "crash")
        {
            CrashEvent();
        }
    }
    //当たったら通り抜けるタイプの当たり判定
    private void OnTriggerEnter(Collider other)
    {
        //ニアミス呼び出し
        //加速リングイベント呼び出し
        if (other.tag == "ring")
        {
            AccelerateEvent();
        }
    }

    public void AccelerateEvent()
    {
        //加速リングに当たったときの処理
        //羽ばたきステータス設定
        this.gameObject.GetComponent<CharacterMoveControl>().AccelSet();
        //スコア加算
        Camera.main.GetComponent<ScoreManage>().ScoreCalc(200);
        //加速する音を再生
        Camera.main.GetComponent<SoundManage>().AccelerateSound();
    }

    public void CrashEvent()
    {
        //墜落したときの処理を行う。
        //衝突エフェクトの発生
        GameObject crashParticleObj = Instantiate(crashParticle) as GameObject;
        crashParticleObj.transform.position = this.gameObject.transform.position;
        //衝突音の発生
        //setDeadを呼び出す。
        this.gameObject.GetComponent<CharacterMoveControl>().setDead();
    }
}
