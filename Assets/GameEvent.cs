using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameEvent : MonoBehaviour
{
    //�Փ˃p�[�e�B�N��
    [SerializeField] GameObject crashParticle;
    //���������璵�˕Ԃ�^�C�v�̓����蔻��
    private void OnCollisionEnter(Collision collision)
    {
        //�ė��C�x���g�Ăяo��
        if (collision.gameObject.tag == "crash")
        {
            CrashEvent();
        }
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
        Camera.main.GetComponent<SoundManage>().AccelerateSound();
    }

    public void CrashEvent()
    {
        //�ė������Ƃ��̏������s���B
        //�Փ˃G�t�F�N�g�̔���
        GameObject crashParticleObj = Instantiate(crashParticle) as GameObject;
        crashParticleObj.transform.position = this.gameObject.transform.position;
        //�Փˉ��̔���
        //setDead���Ăяo���B
        this.gameObject.GetComponent<CharacterMoveControl>().setDead();
    }
}
