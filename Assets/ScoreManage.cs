using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScoreManage : MonoBehaviour
{
    //�����B�X�R�A�\���p
    [SerializeField] GameObject scoreDisplay;
    float score = 50;
    //�X�R�A�̊Ǘ����s��
    public int GetScore()
    { 
        //�X�R�A��Ԃ�
        return (int)score;
    }

    public void ScoreCalc(float add)
    {
        //�X�R�A�����Z����B�����A�����A�j�A�~�X�ŉ��Z�\��
        score += add;
        //Debug.Log("SCORE:" + score);
        //�����B�X�R�A�\���p
        scoreDisplay.GetComponent<TextMesh>().text = ((int)score).ToString();

    }

    public void UpdateHighScore()
    {
        //�n�C�X�R�A�ƍ��̃X�R�A���r���ăn�C�X�R�A�X�V
        DataManage dataManage = Camera.main.GetComponent<DataManage>();
        //LoadData���Ăяo���ăn�C�X�R�A�m�F
        (int highscore, int load_score, int operationMode, float volumeSE, float volumeNoise, float VolumeBGM, int resolution, int effect, int weather, float XSensitivity, float YSensitivity)  = dataManage.LoadData();
        //�n�C�X�R�A�ƍ��̃X�R�A���r
        if (score > highscore)
        {
            highscore = (int)score;
        }
        //SaveData���Ăяo���ăf�[�^�X�V
        dataManage.SaveData(highscore: highscore, score: (int)score);
    }

    public void ScoreReset()
    {
        //�X�R�A���[���Ƀ��Z�b�g����
        score = 0;
    }
}
