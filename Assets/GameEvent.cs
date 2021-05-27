using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameEvent : MonoBehaviour
{
    //���������璵�˕Ԃ�^�C�v�̓����蔻��
    private void OnCollisionEnter(Collision collision)
    {
        //�ė��C�x���g�Ăяo��
    }
    //����������ʂ蔲����^�C�v�̓����蔻��
    private void OnTriggerEnter(Collider other)
    {
        //�j�A�~�X�Ăяo��
        //���������O�C�x���g�Ăяo��
        if (other.tag == "ring")
        {
            AccelerateEvent();
        }
    }

    public void AccelerateEvent()
    {
        //���������O�ɓ��������Ƃ��̏���
        //�H�΂����X�e�[�^�X�ݒ�
        this.gameObject.GetComponent<CharacterMoveControl>().AccelSet();
        //�X�R�A���Z
        Camera.main.GetComponent<ScoreManage>().ScoreCalc(200);
        //�������鉹���Đ�
    }
}
