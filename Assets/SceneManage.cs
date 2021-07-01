using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SceneManage : MonoBehaviour
{
    // Start is called before the first frame update
    int currentSceneNum;
    public bool isContinue;
    public GameObject ScoreDisplay;
    public GameObject Character;
    [SerializeField] GameObject TitlePanel;
    [SerializeField] GameObject GamePanel;
    [SerializeField] GameObject ConfigPanel;
    [SerializeField] GameObject MenuPanel;
    ScoreManage ScoreManagescript;
    [SerializeField] GameEvent playerObject;
    [SerializeField] GameObject highscoredisplay;

    void Start()
    {
        LoadTitle();
        ScoreManagescript = GetComponent<ScoreManage>();
    }

    public int GetScene(){return currentSceneNum;}
    
    public void ChangeScene(int sceneNum)
    {
        switch(sceneNum)
        {
            case 0:
                LoadTitle();
                break;
            case 1:
                LoadConfig();
                break;
            case 2:
                LoadGame();
                break;
            case 3:
                LoadMenu();
                break;
        }
        currentSceneNum = sceneNum;
    }

    public void LoadTitle()
    {
        TitlePanel.SetActive(true);
        GamePanel.SetActive(false);
        ConfigPanel.SetActive(false);
        MenuPanel.SetActive(false);
        ScoreDisplay.SetActive(false);
        (int highscore, int load_score, int operationMode, float volumeSE, float volumeNoise, float VolumeBGM, int resolution, int effect, int weather) = Camera.main.GetComponent<DataManage>().LoadData();
        highscoredisplay.GetComponent<TextMesh>().text = ("highscore:" + (int)highscore).ToString();
        currentSceneNum = 0;
    }
    
    public void LoadConfig()
    {
        TitlePanel.SetActive(false);
        ConfigPanel.SetActive(true);
        GamePanel.SetActive(false);
        MenuPanel.SetActive(false);
        currentSceneNum = 1;
    }
    
    public void LoadGame()
    {
        TitlePanel.SetActive(false);
        GamePanel.SetActive(true);
        ConfigPanel.SetActive(false);
        MenuPanel.SetActive(false);
        ScoreDisplay.SetActive(true);
        if (isContinue == false)
        {
            ScoreManagescript.ScoreReset();
            playerObject.GetComponent<CharacterMoveControl>().StartSet();
        }
        //�Q�[���J�n���̉��o
        isContinue = false;

        currentSceneNum = 2;
    }
    
    public void LoadMenu()
    {
        TitlePanel.SetActive(false);
        ConfigPanel.SetActive(false);
        GamePanel.SetActive(true);
        MenuPanel.SetActive(true);
        currentSceneNum = 3;
    }

    
    
    
}
