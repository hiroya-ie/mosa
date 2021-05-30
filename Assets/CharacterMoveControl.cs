using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterMoveControl : MonoBehaviour
{
    bool isAcceleration;
    //attitudeControl()�p�֐�
    Vector3 basicAttitude;//��{�p��
    Vector3 firstMousePosition;
    Vector3 mousePosition;
    float K = 0.01f; //��C��R�̔��W��
    Vector3 sensitivity = new Vector3(60, 40, 0);
    int isNear;
    //FlyControl()�p�֐�
    float speed;

    //������
    float count;

    //isAccel��true�ɃZ�b�g�i�����j
    public void AccelSet()
    {
        isAcceleration = true;
        count = 0;
    }

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

        //�O���ւ̑��x���Z�o
        Rigidbody characterPhysics = GetComponent<Rigidbody>();
        float forwardSpeed = Vector3.Dot(characterPhysics.velocity,transform.forward);
        //���������Ő��܂��͂��v�Z
        float pitch = forwardSpeed * dragVector.y / sensitivity.y;
        float roll = forwardSpeed * dragVector.x / sensitivity.x;

        //�p���ɔ��f
        characterPhysics.maxAngularVelocity = 7;//��]�̏��
        characterPhysics.AddTorque(transform.right * pitch*Time.deltaTime);
        characterPhysics.AddTorque(-transform.forward * roll * Time.deltaTime);



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
        float mainAttackAngle = Vector3.Angle(characterPhysics.velocity, transform.up) - 90; //���̋p�̂���
        speed = Mathf.Sqrt(characterPhysics.velocity.x * characterPhysics.velocity.x + characterPhysics.velocity.y * characterPhysics.velocity.y + characterPhysics.velocity.z * characterPhysics.velocity.z);
        characterPhysics.AddForce(Time.deltaTime * transform.up * mainAttackAngle * speed*2);
        //���������B���ɃX���C�h���Ȃ��悤�ɂ��A�i�s�����ɓ���������
        float tailAttackAngle = Vector3.Angle(characterPhysics.velocity, transform.right) - 90;
        characterPhysics.AddForce(Time.deltaTime * transform.right * tailAttackAngle * speed);
        characterPhysics.AddTorque(Time.deltaTime * transform.up * -tailAttackAngle * speed);
        //���x�ɂ���Ďp����ς���B�i�������͉��������j
        if (1 - (speed / 100) > 0)
        {
            characterPhysics.AddTorque(Time.deltaTime * transform.right * mainAttackAngle * speed * (1 - (speed / 100)));
        }

        //����������B
        if (isAcceleration == true)
        {
            characterPhysics.AddForce(transform.forward * 20000 * Time.deltaTime);
            count += Time.deltaTime;
        }
        if (count > 0.3f)//�����I���i�����j
        {
            isAcceleration = false;
        }

        //���x�x�N�g�����J�����ɓ`����
        Camera.main.GetComponent<CameraControl>().CameraTrace(characterPhysics.velocity,this.gameObject.transform.position);
        //�X�R�A���Z����
        Camera.main.GetComponent<ScoreManage>().ScoreCalc(Vector3.Magnitude(characterPhysics.velocity)/5000);
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
