using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIManage : MonoBehaviour
{
    // Start is called before the first frame update
    public int highscore,score,operationMode,resolution,effect,weather;
    public float volumeSE,volumeNoise,volumeBGM;
    public bool invert;
    public GameObject MainCamera;
    public GameObject Character;
    public Toggle toggle;
    public Slider NoiseSlider,SESlider,BGMSlider;
    SceneManage SceneManagescript;
    DataManage DataManagescript;
    SoundManage SoundManagescript;
    
    void Start()
    {
        SceneManagescript = MainCamera.GetComponent<SceneManage>();
        DataManagescript = MainCamera.GetComponent<DataManage>();
        SoundManagescript = MainCamera.GetComponent<SoundManage>();
        
        (highscore,score,operationMode,volumeSE,volumeNoise,volumeBGM,resolution,effect,weather) = DataManagescript.LoadData();//全変数へデータロード
        
        SoundManagescript.NoiseAudioSource.volume = volumeNoise;
        SoundManagescript.SEAudioSource.volume = volumeSE;
        //SoundManagescript.BGMAudioSource.volume = volumeBGM;
        NoiseSlider.value = volumeNoise;
        SESlider.value = volumeSE;
        BGMSlider.value = volumeBGM;
    }
    
    public void TitleUIGameStartClick()
    {
        SceneManagescript.ChangeScene(2); //2でゲームスタート
        Time.timeScale = 1;
        Character.SetActive(true);
        SceneManagescript.isContinue = false;
        Character.GetComponent<CharacterMoveControl>().StartSet();

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
    
    public void ConfigUIOpStandardClick()
    {
        operationMode = 0;
    }
    
    public void ConfigUIOpExpertClick()
    {
        operationMode = 1;
    }
    
    public void ConfigUIInvertClick()
    {
        invert = toggle.isOn;
        //Debug.Log (toggle.isOn);
    }
    
    public void ConfigUIVolumeSESlide()
    {
        volumeSE = SESlider.normalizedValue;
        SoundManagescript.SEAudioSource.volume = volumeSE;
    }
    
    public void ConfigUIVolumeNoiseSlide()
    {
        volumeNoise = NoiseSlider.normalizedValue;
        //Debug.Log (volumeNoise);
        SoundManagescript.NoiseAudioSource.volume = volumeNoise;
    }
    
    public void ConfigUIVolumeBGMSlide()
    {
        volumeBGM = BGMSlider.normalizedValue;
        //SoundManagescript.BGMAudioSource.volume = volumeBGM;
    }
    
    public void ConfigUIReturnClick()
    {
        DataManagescript.SaveData(highscore,score,operationMode,volumeSE,volumeNoise,volumeBGM,resolution,effect,weather);
        if (SceneManagescript.isContinue){SceneManagescript.ChangeScene(3);}
        else {SceneManagescript.ChangeScene(0);}
    }
    
    public void GameUIPauseClick()
    {
        Time.timeScale = 0;
        SceneManagescript.ChangeScene(3); //3でpauseメニュー
    }
    
    public void MenuUIContinueClick()
    {
        Time.timeScale = 1;
        SceneManagescript.ChangeScene(2);
        SceneManagescript.isContinue = true;
    }
    
    public void MenuUIExitClick()
    {
        //Character.transform.position = new Vector3(0,0,0);
        /////Character.transform.rotation = Quaternion.identity;
        //MainCamera.transform.position = new Vector3(0,0,0);
        //MainCamera.transform.rotation = Quaternion.identity;
        //Character.SetActive(false);
        Character.GetComponent<CharacterMoveControl>().ResetCharacter();
        SceneManagescript.ChangeScene(0); //0でタイトル
        SceneManagescript.isContinue = false;
    }
    
    public void  MenuUIConfigClick()
    {
        SceneManagescript.ChangeScene(1);
    }
    
    
}
