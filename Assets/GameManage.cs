using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameManage : MonoBehaviour
{
    public GameObject character;
    public Text highscoreText;
    CharacterMoveControl characterMoveControl;
    SceneManage sceneManage;

    //�����B�n�C�X�R�A�̃e�X�g�B
    [SerializeField] GameObject highscoredisplay;
    // Start is called before the first frame update
    void Start()
    {
        //Application.targetFrameRate = 10;
        characterMoveControl = character.GetComponent<CharacterMoveControl>();
        Camera.main.GetComponent<ScoreManage>().ScoreReset();
        sceneManage = Camera.main.GetComponent<SceneManage>();

        //�����B�n�C�X�R�A�̃e�X�g�B
        (int highscore, int load_score, int operationMode, float volumeSE, float volumeNoise, float VolumeBGM, int resolution, int effect, int weather, float XSensitivity, float YSensitivity) = Camera.main.GetComponent<DataManage>().LoadData();
        //highscoredisplay.GetComponent<TextMesh>().text = ("highscore:" + (int)highscore).ToString();
        highscoreText.text = "highscore:" + highscore;
        //�ݒ�ǂݍ���
        //�^�C�g����ʋN��
    }

    // Update is called once per frame
    void Update()
    {
        //�V�[���ԍ��J�n
        //�Q�[���֘A�̊֐��Ăяo��
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

        //�����B�n�C�X�R�A�̃e�X�g�B
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
