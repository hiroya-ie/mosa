using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterMoveControl : MonoBehaviour
{

    //attitudeControl()�p�֐�
    Vector3 basicAttitude;//��{�p��
    Vector3 firstMousePosition;
    Vector3 mousePosition;
    float K = 0.01f; //��C��R�̔��W��
    int Xsensitivity = 5000;//�������̊��x
    int Ysensitivity = 10000;//�c�����̊��x
    int isNear;
    float XtorqueVelocity;
    float YtorqueVelocity;
    public GameObject body;
    float speed;
    public void AttitudeControl()
    {
        /*�h���b�O�����o���Ċ�{�p���ɔ��f����B*/
        Vector3 dragVector = new Vector3(0,0,0);
        //�h���b�O���擾
        if (Input.GetMouseButtonDown(0))
        {
            firstMousePosition = Input.mousePosition;
        }
        if (Input.GetMouseButton(0))
        {
            mousePosition = Input.mousePosition;
            dragVector = mousePosition - firstMousePosition;
        }

        //�L�[����i�����j
        if (Input.GetKey("w"))
        {
            dragVector.y = 200;
        }
        if (Input.GetKey("a"))
        {
            dragVector.x = -200;
        }
        if (Input.GetKey("s"))
        {
            dragVector.y = -200;
        }
        if (Input.GetKey("d"))
        {
            dragVector.x = 200;
        }
        
        //�h���b�O�̌��E��݂���
        if (dragVector.y > 200)
        {
            dragVector.y = 200;
        }else if(dragVector.y < -200)
        {
            dragVector.y = -200;
        }
        if(dragVector.x > 200)
        {
            dragVector.x = 200;
        }else if(dragVector.x < -200)
        {
            dragVector.x = -200;
        }
        //�X�s�[�h�ɂ���đ���̉�������ς���
        dragVector *= speed / 100;

        

        //��{�p����ϊ�
        //���E�����̉����x���v�Z�i��C��R����Łjfloat
        float XtorqueAccel = -dragVector.x / Xsensitivity - K * XtorqueVelocity;
        //���E�����̑��x���v�Z float
        XtorqueVelocity += XtorqueAccel;

        //�㉺�����̉����x���v�Z�i��C��R����Łj
        float YtorqueAccel = dragVector.y / Ysensitivity - K * YtorqueVelocity;
        //�㉺�����̑��x���v�Z float
        YtorqueVelocity += YtorqueAccel;

        //��{�p���ɔ��f
        basicAttitude += transform.forward * XtorqueVelocity + transform.right * YtorqueVelocity;
        //transform.rotation = Quaternion.Euler(basicAttitude);
        //�L�����N�^�[�ɔ��f�i�����j
        Rigidbody characterPhysics = GetComponent<Rigidbody>();
        characterPhysics.maxAngularVelocity = 50;
        characterPhysics.angularVelocity = transform.forward * XtorqueVelocity + transform.right * YtorqueVelocity;
        //�����i�����j
        if (Input.GetKey("l"))
        {
            characterPhysics.AddForce(transform.forward * 5);
        }
        FlyControl();
        //�j�A�~�X�������
        if (Input.GetKey("o"))
        {
            isNear = 1;//����)
        }
        else if (Input.GetKey("p"))
        {
            isNear = 2;//�E(��)
        }

    }

    public void FlyControl()
    {
        /*��{�p���ƃX�s�[�h�ɉ����ė͊w�I�ȉ��Z���s���A���x�x�N�g���𒲐�����*/
        Rigidbody characterPhysics = GetComponent<Rigidbody>();
        //�i�s�����Ɗ�{�p���̊p�x�������߂�B�g�́A�R�͂����܂邽�߁B��{�p���̖@���x�N�g���Ɛi�s�����Ƃ̊p�x�����g���B
        //�嗃�̗g��
        float mainAttackAngle = Vector3.Angle(characterPhysics.velocity, transform.up)-90; //���̋p�̂���
        speed = Mathf.Sqrt(characterPhysics.velocity.x * characterPhysics.velocity.x + characterPhysics.velocity.y * characterPhysics.velocity.y + characterPhysics.velocity.z * characterPhysics.velocity.z);
        characterPhysics.AddForce(transform.up * mainAttackAngle * speed/160);
        //���������B���ɃX���C�h���Ȃ��悤�ɂ��A�i�s�����ɓ���������
        float tailAttackAngle = Vector3.Angle(characterPhysics.velocity, transform.right) - 90;
        characterPhysics.AddForce(transform.right * tailAttackAngle * speed / 320);
        characterPhysics.AddTorque(transform.up * -tailAttackAngle * speed / 320);
        //���x�ɂ���Ďp����ς���B�i�������͉��������j
        if(-0.1f * speed + 10f >= 0){
            characterPhysics.AddTorque(new Vector3((-0.1f * speed + 10)* Vector3.Angle(characterPhysics.velocity, transform.forward) / 200, 0, 0));
        }
        MotionControl();

    }
    public void MotionControl()
    {
        //��{�p���Ƀj�A�~�X���Ȃǂ̃��[�������[�V�������������p�������Z���A�L�����N�^�[�ɔ��f����B�������̂͏㔼�g�̃u���b�N�݂̂œ����Ǝl���̓����ɂ͂������Ȃ��B
        GameObject Body = GameObject.Find("Body");
        if (isNear == 1)
        {
            //���ɉ�]
            Body.transform.Rotate(0, 0, 5);
            isNear = 0;
        }
        else if (isNear == 2)
        {
            //�E�ɉ�]
            Body.transform.Rotate(0, 0, -5);
            isNear = 0;
        }
    }
}
