using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManage : MonoBehaviour
{
    public GameObject character;
    CharacterMoveControl script;

    //実験。ハイスコアのテスト。
    [SerializeField] GameObject highscoredisplay;
    // Start is called before the first frame update
    void Start()
    {
        script = character.GetComponent<CharacterMoveControl>();
        Camera.main.GetComponent<ScoreManage>().ScoreReset();

        //実験。ハイスコアのテスト。
        (int highscore, int load_score, int operationMode, int volumeSE, int volumeNoise, int VolumeBGM, int resolution, int effect, int weather) = Camera.main.GetComponent<DataManage>().LoadData();
        highscoredisplay.GetComponent<TextMesh>().text = ("highscore:" + (int)highscore).ToString();
        Application.targetFrameRate = 20;
        //設定読み込み
        //タイトル画面起動
    }

    // Update is called once per frame
    void Update()
    {
        //シーン番号開始
        //ゲーム関連の関数呼び出し
        script.AttitudeControl();

        //実験。ハイスコアのテスト。
        if (Input.GetKeyDown("e"))
        {
            Camera.main.GetComponent<ScoreManage>().UpdateHighScore();
        }
        if (Input.GetKeyDown("r"))
        {
            Camera.main.GetComponent<DataManage>().ResetData();
        }


    }
}
