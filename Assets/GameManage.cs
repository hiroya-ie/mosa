using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManage : MonoBehaviour
{
    public GameObject character;
    CharacterMoveControl characterMoveControl;
    SceneManage sceneManage;

    // Start is called before the first frame update
    void Start()
    {
        //Application.targetFrameRate = 10;
        characterMoveControl = character.GetComponent<CharacterMoveControl>();
        Camera.main.GetComponent<ScoreManage>().ScoreReset();
        sceneManage = Camera.main.GetComponent<SceneManage>();

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
        if (Input.GetKeyDown("r"))
        {
            Camera.main.GetComponent<DataManage>().ResetData();
        }


    }
}
