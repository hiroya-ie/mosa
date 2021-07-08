using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManage : MonoBehaviour
{
    public GameObject character;
    CharacterMoveControl characterMoveControl;
    SceneManage sceneManage;

    //実験。ハイスコアのテスト。
    [SerializeField] GameObject highscoredisplay;
    // Start is called before the first frame update
    void Start()
    {
        //Application.targetFrameRate = 10;
        characterMoveControl = character.GetComponent<CharacterMoveControl>();
        Camera.main.GetComponent<ScoreManage>().ScoreReset();
        sceneManage = Camera.main.GetComponent<SceneManage>();

        //実験。ハイスコアのテスト。
        (int highscore, int load_score, int operationMode, float volumeSE, float volumeNoise, float VolumeBGM, int resolution, int effect, int weather) = Camera.main.GetComponent<DataManage>().LoadData();
        highscoredisplay.GetComponent<TextMesh>().text = ("highscore:" + (int)highscore).ToString();
        //設定読み込み
        //タイトル画面起動
    }

    // Update is called once per frame
    void Update()
    {
        //シーン番号開始
        //ゲーム関連の関数呼び出し
        switch (sceneManage.GetScene())
        {
            case 0:
                break;
            case 1:
                break;
            case 2:
                characterMoveControl.AttitudeControl();
                break;
            case 3:
                break;
        }

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