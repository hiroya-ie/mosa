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
        //�ݒ�ǂݍ���
        //�^�C�g����ʋN��
    }

    // Update is called once per frame
    void Update()
    {
        //�V�[���ԍ��J�n
        //�Q�[���֘A�̊֐��Ăяo��
        script.AttitudeControl();
        
    }
}
