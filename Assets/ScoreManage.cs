using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScoreManage : MonoBehaviour
{
    //実験。スコア表示用
    [SerializeField] GameObject scoreDisplay;
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
        //Debug.Log("SCORE:" + score);
        //実験。スコア表示用
        scoreDisplay.GetComponent<TextMesh>().text = ((int)score).ToString();

    }

    public void UpdateHighScore()
    {
        //ハイスコアと今のスコアを比較してハイスコア更新
        DataManage dataManage = Camera.main.GetComponent<DataManage>();
        //LoadDataを呼び出してハイスコア確認
        (int highscore, int load_score, int operationMode, float volumeSE, float volumeNoise, float VolumeBGM, int resolution, int effect, int weather, float XSensitivity, float YSensitivity)  = dataManage.LoadData();
        //ハイスコアと今のスコアを比較
        if (score > highscore)
        {
            highscore = (int)score;
        }
        //SaveDataを呼び出してデータ更新
        dataManage.SaveData(highscore: highscore, score: (int)score);
    }

    public void ScoreReset()
    {
        //スコアをゼロにリセットする
        score = 0;
    }
}
