using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScoreManage : MonoBehaviour
{
    float score = 50;
    //スコアの管理を行う
    public int GetScore()
    { 
        //スコアを返す
        return (int)score;
    }

    public void ScoreCalc(float add)
    {
        //スコアを加算する。距離、加速、ニアミスで加算予定
        score += add;
        Debug.Log("SCORE:" + score);
    }

    public void UpdateHighScore()
    {
        //ハイスコアと今のスコアを比較してハイスコア更新
        int highscore = 0;
        //LoadDataを呼び出してハイスコア確認
        //ハイスコアと今のスコアを比較
        if (score > highscore)
        {
            highscore = (int)score;
        }
        //SaveDataを呼び出してデータ更新
    }

    public void ScoreReset()
    {
        //スコアをゼロにリセットする
        score = 0;
    }
}
