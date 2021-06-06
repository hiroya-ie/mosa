using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SceneManage : MonoBehaviour
{
    // Start is called before the first frame update
    int currentSceneNum;
    bool isContinue;
    [SerializeField] GameObject TitlePanel;
    [SerializeField] GameObject GamePanel;
    [SerializeField] GameObject ConfigPanel;
    [SerializeField] GameObject MenuPanel;
    [SerializeField] GameEvent playerObject;
    
    void Start()
    {
        LoadTitle();
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
    }
    
    public void LoadConfig()
    {
        TitlePanel.SetActive(false);
        ConfigPanel.SetActive(true);
        GamePanel.SetActive(false);
        MenuPanel.SetActive(false);
    }
    
    public void LoadGame()
    {
        TitlePanel.SetActive(false);
        GamePanel.SetActive(true);
        ConfigPanel.SetActive(false);
        MenuPanel.SetActive(false);
        //ゲーム開始時の演出
        playerObject.GetComponent<CharacterMoveControl>().StartSet();
    }
    
    public void LoadMenu()
    {
        TitlePanel.SetActive(false);
        ConfigPanel.SetActive(false);
        GamePanel.SetActive(true);
        MenuPanel.SetActive(true);
    }

    
    
    
}
