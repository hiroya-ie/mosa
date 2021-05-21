using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManage : MonoBehaviour
{
    public GameObject character;
    CharacterMoveControl script;
    // Start is called before the first frame update
    void Start()
    {
        script = character.GetComponent<CharacterMoveControl>();
        Camera.main.GetComponent<ScoreManage>().ScoreReset();
        //設定読み込み
        //タイトル画面起動
    }

    // Update is called once per frame
    void Update()
    {
        //シーン番号開始
        //ゲーム関連の関数呼び出し
        script.AttitudeControl();
        
    }
}
