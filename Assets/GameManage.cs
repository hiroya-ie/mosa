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
        if (Input.GetKeyDown("r"))
        {
            Camera.main.GetComponent<DataManage>().ResetData();
        }


    }
}
