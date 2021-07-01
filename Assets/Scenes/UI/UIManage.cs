using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIManage : MonoBehaviour
{
    int operationMode;  //0=スタンダードモード、1=エキスパートモード　操作モードを保持する変数
    bool invert; //上下反転しない、true=上下反転する。どっちが上にドラッグすると上昇か決める

    float volumeSE; //SEの音量
    float volumeNoise; //環境音の音量
    float volumeBGM; //BGMの音量。
    int resolution; //画面の解像度。0=低、1=中、2=高
    int effect; //エフェクトの数。0=低、1=中、2=高
    int weather; //天気のクオリティ。0=低、1=中、2=高。



    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void SettingSet()
    {
        //引数の値をを各変数にセットする。
    }

    public void TitleUIGameStartClick()
    {
        //タイトル画面のゲーム開始ボタンをクリックしたときの処理を行う。ゲーム開始ボタンのOn click()に割り当てる。
        SceneManage.ChangeScene() //ゲーム画面への遷移
    }

    public void TitleUIConfigClick()
    {
        //タイトル画面の設定ボタンをクリックしたときの処理を行う。
        SceneManage.ChangeScene() //設定画面への遷移
    }

    public void TitleUIExitClick()
    {
        //タイトル画面の終了ボタンをクリックしたときの処理を行う。
        //終了アニメーションの再生。ゲームの終了。
    }

    public void ConfitUIOpStandardClick()
    {
        //設定画面の操作モード-スタンダードボタンをクリックしたときの処理を行う。
        operationMode = 0;
    }

    public void ConfigUIOpExpertClick()
    {
        //設定画面の操作モード-エキスパートボタンをクリックしたときの処理を行う。
        operationMode = 1;
    }

    private void ConfigUIInvertClick()
    {
        //設定画面の上下反転ボタンをクリックしたときの処理を行う。チェックボックス。
        if (invert==false)
        {
            
        }
    }
}
