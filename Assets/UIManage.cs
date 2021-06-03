using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIManage : MonoBehaviour
{
    // Start is called before the first frame update
    int operationMode,resolution,effect,weather;
    float volumeSE,volumeNoise,volumeBGM;
    bool invert;
    public GameObject mainCamera;
    public GameObject Character;
    SceneManage SMscript;
    
    void Start()
    {
        SMscript = mainCamera.GetComponent<SceneManage>();
    }
    
    public void TitleUIGameStartClick()
    {
        SMscript.ChangeScene(2); //2でゲームスタート
        Character.SetActive(true);
    }
    
    public void TitleUIConfigClick()
    {
        SMscript.ChangeScene(1); //1でconfig
    }
    
    public void TitleUIExitClick()
    {
        Application.Quit();
    }
    
    public void ConfigUIOpStandardClick(){}
    
    public void ConfigUIOpExpertClick(){}
    
    public void ConfigUIInvertClick(){}
    
    public void ConfigUIVolumeSESlide(){}
    
    public void ConfigUIVolumeNoise(){}
    
    public void ConfigUIVolumeBGMSlide(){}
    
    public void ConfigUIReturnClick()
    {
        SMscript.ChangeScene(0); //0でタイトル
    }
    
    public void GameUIPauseClick()
    {
        Time.timeScale = 0;
        SMscript.ChangeScene(3); //3でpauseメニュー
    }
    
    public void MenuUIContinueClick()
    {
        Time.timeScale = 1;
        SMscript.ChangeScene(2);
    }
    
    public void MenuUIExitClick()
    {
        Character.SetActive(false);
        SMscript.ChangeScene(0); //0でタイトル
    }
    
    
}
