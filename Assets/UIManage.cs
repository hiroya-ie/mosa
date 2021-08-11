using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class UIManage : MonoBehaviour
{
    // Start is called before the first frame update
    public int highscore,score,operationMode,resolution,effect,weather,invert;
    public float volumeSE,volumeNoise,volumeBGM,XSensitivity,YSensitivity;
    public bool invertBool;
    public GameObject MainCamera,Character,fog,trail,trail1;
    public Toggle toggle;
    public Button standardButton,expertButton;
    public Slider NoiseSlider,SESlider,BGMSlider,XSlider,YSlider;
    public Dropdown ImageQuality,Effect,Weather;
    SceneManage SceneManagescript;
    DataManage DataManagescript;
    SoundManage SoundManagescript;
    
    void Start()
    {
        SceneManagescript = MainCamera.GetComponent<SceneManage>();
        DataManagescript = MainCamera.GetComponent<DataManage>();
        SoundManagescript = MainCamera.GetComponent<SoundManage>();
        
        (highscore,score,operationMode,invert,volumeSE,volumeNoise,volumeBGM,resolution,effect,weather,XSensitivity,YSensitivity) = DataManagescript.LoadData();//全変数へデータロード
        
        invertToinvertBool();//int型からbool型へ
                
        SoundManagescript.NoiseAudioSource.volume = volumeNoise;
        SoundManagescript.SEAccelerateAudioSource.volume = volumeSE;
        SoundManagescript.SECrashAudioSource.volume = volumeSE;
        //SoundManagescript.BGMAudioSource.volume = volumeBGM;
        
        Character.GetComponent<CharacterMoveControl>().operationMode = operationMode;
        //Character.GetComponent<CharacterMoveControl>().invert = invert;
        Character.GetComponent<CharacterMoveControl>().XSensitivity = XSensitivity;
        Character.GetComponent<CharacterMoveControl>().YSensitivity = YSensitivity;
        
        NoiseSlider.value = volumeNoise;
        SESlider.value = volumeSE;
        BGMSlider.value = volumeBGM;
        XSlider.value = XSensitivity;
        YSlider.value = YSensitivity;
        
        //Debug.Log(effect);
        ConfigUIEffectRef();
        toggle.isOn = invertBool;
        
        if (operationMode==0)
        {
            standardButton.GetComponent<Image>().color=Color.black;
            expertButton.GetComponent<Image>().color=Color.white;
        }
        else
        {
            standardButton.GetComponent<Image>().color=Color.white;
            expertButton.GetComponent<Image>().color=Color.black;
        }
        
    }
    
    public void TitleUIGameStartClick()
    {
        SceneManagescript.ChangeScene(2); //2でゲームスタート
        Time.timeScale = 1;
        Character.SetActive(true);
        if (effect==2)
        {
            trail.SetActive(false);
            trail1.SetActive(false);
        }
        else
        {
            trail.SetActive(true);
            trail1.SetActive(true);
        }
        SceneManagescript.isContinue = true;
        
    }
    
    public void TitleUIConfigClick()
    {
        SceneManagescript.ChangeScene(1); //1でconfig
        //設定に変数の値を反映させる        
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
        standardButton.GetComponent<Image>().color=Color.black;
        expertButton.GetComponent<Image>().color=Color.white;
        operationMode = 0;
        Character.GetComponent<CharacterMoveControl>().operationMode = operationMode;
    }
    
    public void ConfigUIOpExpertClick()
    {
        standardButton.GetComponent<Image>().color=Color.white;
        expertButton.GetComponent<Image>().color=Color.black;
        operationMode = 1;
        Character.GetComponent<CharacterMoveControl>().operationMode = operationMode;
    }
    
    public void ConfigUIInvertClick()
    {
        invertBool = toggle.isOn;
        invertBoolToinvert();
        //Debug.Log (toggle.isOn);
        Character.GetComponent<CharacterMoveControl>().invert = invertBool;
    }
    
    public void ConfigUIVolumeSESlide()
    {
        volumeSE = SESlider.normalizedValue;
        SoundManagescript.SEAccelerateAudioSource.volume = volumeSE;
        SoundManagescript.SECrashAudioSource.volume = volumeSE;
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
    
    public void ConfigUISensitivityXSlide()
    {
        XSensitivity = XSlider.normalizedValue*100;
        //Debug.Log(XSensitivity);
        Character.GetComponent<CharacterMoveControl>().XSensitivity = XSensitivity;
    }
    
    public void ConfigUISensitivityYSlide()
    {
        YSensitivity = YSlider.normalizedValue*100;
        Character.GetComponent<CharacterMoveControl>().YSensitivity = YSensitivity;
    }
    
    public void ConfigUIReturnClick()
    {
        //Debug.Log(XSensitivity);
        DataManagescript.SaveData(highscore,score,operationMode,invert,volumeSE,volumeNoise,volumeBGM,resolution,effect,weather,XSensitivity,YSensitivity);
        if (SceneManagescript.isContinue){SceneManagescript.ChangeScene(3);}
        else 
        {
            SceneManagescript.ChangeScene(0);
            //Debug.Log (SceneManagescript.isContinue);
        }
    }
    
    public void ConfigUIImageQuality()
    {
        resolution = ImageQuality.value;
        switch(resolution)
        {
            case 0:
                
                break;
            case 1:
                
                break;
            case 2:
                
                break;
        }
    }
    
    public void ConfigUIEffect(){effect = Effect.value;}
    
    public void ConfigUIEffectRef()
    {
        Effect.value = effect;
        switch(effect)
        {
            case 0:
                fog.SetActive(true);
                break;
            case 1:
                fog.SetActive(false);
                break;
            case 2:
                fog.SetActive(false);
                break;
        }
    }
    
    public void ConfigUIWeather()
    {
        weather = Weather.value;
        switch(resolution)
        {
            case 0:
                
                break;
            case 1:
                
                break;
            case 2:
                
                break;
        }
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
        if (effect==2)
        {
            trail.SetActive(false);
            trail1.SetActive(false);
        }
        else
        {
            trail.SetActive(true);
            trail1.SetActive(true);
        }
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
        SceneManagescript.isContinue = true;
    }
    
    public void invertToinvertBool()
    {
        if(invert==0)
        {
            invertBool =  false;
        }
        else if(invert==1)
        {
            invertBool = true;
        }
    }
    
    public void invertBoolToinvert()
    {
        if(invertBool==false)
        {
            invert = 0;
        }
        else if(invertBool==true)
        {
            invert = 1;
        }
    }
    
    
}
