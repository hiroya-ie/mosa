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
    public GameObject MainCamera;
    public GameObject Character;
    SceneManage SceneManagescript;
    
    void Start()
    {
        SceneManagescript = MainCamera.GetComponent<SceneManage>();
    }
    
    public void TitleUIGameStartClick()
    {
        SceneManagescript.ChangeScene(2); //2でゲームスタート
        Time.timeScale = 1;
        Character.SetActive(true);
    }
    
    public void TitleUIConfigClick()
    {
        SceneManagescript.ChangeScene(1); //1でconfig
    }
    
    public void TitleUIExitClick()
    {
        #if UNITY_EDITOR
		    UnityEditor.EditorApplication.isPlaying = false;
	    #elif UNITY_WEBPLAYER
		    Application.OpenURL("http://www.yahoo.co.jp/");
	    #else
		    Application.Quit();
	    #endif
    }
    
    public void ConfigUIOpStandardClick(){}
    
    public void ConfigUIOpExpertClick(){}
    
    public void ConfigUIInvertClick(){}
    
    public void ConfigUIVolumeSESlide(){}
    
    public void ConfigUIVolumeNoiseSlide(){}
    
    public void ConfigUIVolumeBGMSlide(){}
    
    public void ConfigUIReturnClick()
    {
        SceneManagescript.ChangeScene(0); //0でタイトル
    }
    
    public void GameUIPauseClick()
    {
        Time.timeScale = 0;
        SceneManagescript.ChangeScene(3); //3でpauseメニュー
    }
    
    public void MenuUIContinueClick()
    {
        Time.timeScale = 1;
        SceneManagescript.isContinue = true;
        SceneManagescript.ChangeScene(2);
    }
    
    public void MenuUIExitClick()
    {   
        Character.transform.position = new Vector3(0,0,0);
        Character.transform.rotation = Quaternion.identity;
        MainCamera.transform.position = new Vector3(0,0,0);
        MainCamera.transform.localRotation = default;
        Character.SetActive(false);
        SceneManagescript.ChangeScene(0); //0でタイトル
        SceneManagescript.isContinue = false;
    }
    
    
}
