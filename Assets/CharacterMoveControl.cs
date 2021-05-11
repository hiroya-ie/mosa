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
    float XtorqueVelocity;
    float YtorqueVelocity;
    public void AttitudeControl()
    {
        /*�h���b�O�����o���Ċ�{�p���ɔ��f����B*/
        //////////////////////////////////////////////////�X�s�[�h�ŉ�������ς���
        //�h���b�O�͈̔͂Ɍ��E��݂���
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
        characterPhysics.angularVelocity = transform.forward * XtorqueVelocity + transform.right * YtorqueVelocity;
        //�����i�����j
        if (Input.GetKey("l"))
        {
            characterPhysics.AddForce(transform.forward * 1);
        }
        FlyControl();
    }

    public void FlyControl()
    {
        /*��{�p���ƃX�s�[�h�ɉ����ė͊w�I�ȉ��Z���s���A���x�x�N�g���𒲐�����*/
        Rigidbody characterPhysics = GetComponent<Rigidbody>();
        //�i�s�����Ɗ�{�p���̊p�x�������߂�B�g�́A�R�͂����܂邽�߁B��{�p���̖@���x�N�g���Ɛi�s�����Ƃ̊p�x�����g���B
        //�嗃�̗g��
        float attackAngle = Vector3.Angle(characterPhysics.velocity, transform.up)-90; //���̋p�̂���
        float speed = Mathf.Sqrt(characterPhysics.velocity.x * characterPhysics.velocity.x + characterPhysics.velocity.y * characterPhysics.velocity.y + characterPhysics.velocity.z * characterPhysics.velocity.z);
        characterPhysics.AddForce(transform.up * attackAngle * speed/300);
        Debug.Log(attackAngle);
        //��������

    }
}
