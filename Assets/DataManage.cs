using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DataManage : MonoBehaviour
{
    /*
     * データの管理を担う。
     */
    //https://gametukurikata.com/program/savedata
    public void SaveData(int highscore=-1,int score = -1, int operationMode = -1, int volumeSE = -1, int volumeNoise = -1, int volumeBGM = -1, int resolution = -1, int effect = -1, int weather = -1)
     {
        /*
         * データをセーブする。
         * 各引数が未指定（＝-1）でなければ渡された値を保存
         */
        if (highscore != -1) { PlayerPrefs.SetInt("highscore", highscore);}
        if (score != -1) { PlayerPrefs.SetInt("score", score);}
        if (operationMode != -1) { PlayerPrefs.SetInt("operationMode", operationMode);}
        if (volumeSE != -1) { PlayerPrefs.SetInt("volumeSE", volumeSE);}
        if (volumeNoise != -1) { PlayerPrefs.SetInt("volumeNoise", volumeNoise);}
        if (volumeBGM != -1) { PlayerPrefs.SetInt("volumeBGM", volumeBGM);}
        if (resolution != -1) { PlayerPrefs.SetInt("resolution", resolution);}
        if (effect != -1) { PlayerPrefs.SetInt("effect", effect);}
        if (weather != -1) { PlayerPrefs.SetInt("weather", weather);}
    }

    public (int highscore,int score,int operationMode,int volumeSE,int volumeNoise,int VolumeBGM,int resolution,int effect,int weather) LoadData()
    {
        /*
         * データをロードする
         * 各保存データを返す。何も保存されていなければデフォルト値を返す。
         */
        int highscore = PlayerPrefs.GetInt("highscore", 0);
        int score = PlayerPrefs.GetInt("score", 0);
        int operationMode = PlayerPrefs.GetInt("operationMode", 1);
        int volumeSE = PlayerPrefs.GetInt("volumeSE", 50);
        int volumeNoise = PlayerPrefs.GetInt("volumeNoise", 50);
        int volumeBGM = PlayerPrefs.GetInt("volumeBGM", 50);
        int resolution = PlayerPrefs.GetInt("resolution", 1);
        int effect = PlayerPrefs.GetInt("effect", 1);
        int weather = PlayerPrefs.GetInt("weather", 1);
        return (highscore,score,operationMode,volumeSE,volumeNoise,volumeBGM,resolution,effect,weather);
    }

    public void ResetData()
    {
        PlayerPrefs.DeleteKey("highscore");
        PlayerPrefs.DeleteKey("score");
        PlayerPrefs.DeleteKey("operationMode");
        PlayerPrefs.DeleteKey("volumeSE");
        PlayerPrefs.DeleteKey("volumeNoise");
        PlayerPrefs.DeleteKey("volumeBGM");
        PlayerPrefs.DeleteKey("resolution");
        PlayerPrefs.DeleteKey("effect");
        PlayerPrefs.DeleteKey("weather");
    }
}
