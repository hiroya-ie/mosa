using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterMoveControl : MonoBehaviour
{
    public int operationMode = 1;
    bool invert = true;
    bool isAcceleration;
    //attitudeControl()�p�֐�
    Vector3 basicAttitude;//��{�p��
    Vector3 firstMousePosition;
    Vector3 mousePosition;
    //float K = 0.01f; //��C��R�̔��W��
    Vector3 sensitivity = new Vector3(60, 40, 0);
    int isNear;
    //FlyControl()�p�֐�
    float speed;
    //MotionControl
    [SerializeField] GameObject body;
    //�Q�[���J�n�A�j���[�V����
    bool isStart = false;
    //���S
    bool isDead = false;
    float deadCount = 0;
    //�J��������
    Vector3 cameraPos;

    //������
    float accelCount;

    //isAccel��true�ɃZ�b�g�i�����j
    public void AccelSet()
    {
        isAcceleration = true;
        accelCount = 0;
    }

    //�Q�[���J�n�A�j���[�V�����p
    public void StartSet()
    {
        isStart = true;
    }

    public void GameOver()
    {
        //�Q�[���I�[�o�[���̏���
        isDead = false;
        ResetCharacter();
        Camera.main.GetComponent<ScoreManage>().UpdateHighScore();
        Camera.main.GetComponent<SceneManage>().ChangeScene(0);
    }
    public void ResetCharacter()
    {
        Camera.main.GetComponent<CameraControl>().CameraPosSet(new Vector3(0, -33.3f, -5f), new Vector3(8.579f, 0, 0));
        transform.position = new Vector3(0, 0.5523103f, 0.2638457f);
        transform.rotation = Quaternion.Euler(new Vector3(53.106f, 0, 0));
        this.gameObject.GetComponent<Rigidbody>().angularDrag = 5;
        this.gameObject.GetComponent<Rigidbody>().velocity = Vector3.zero;
        this.gameObject.GetComponent<Rigidbody>().angularVelocity = Vector3.zero;
        this.gameObject.SetActive(false);
        isDead = false;
    }


    public void AttitudeControl()
    {
        Rigidbody characterPhysics = GetComponent<Rigidbody>();
        if (isDead == true)
        {
            Camera.main.GetComponent<CameraControl>().CameraTrace(characterPhysics.velocity, this.gameObject.transform.position,isDead);
            this.gameObject.GetComponent<Rigidbody>().angularDrag = 0;
            deadCount += Time.deltaTime;
            if (deadCount > 6)
            {
                deadCount = 0;
                GameOver();
            }
            return;
        }



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

        if (isStart == true)//�Q�[���J�n�A�j���[�V����
        {
            if (transform.rotation.eulerAngles.x+10 > 180)
            {
                isStart = false;
            }
            else
            {
                dragVector = new Vector3(0, -10 * transform.rotation.eulerAngles.x, 0);
                characterPhysics.AddForce(transform.forward * 4000 * Time.deltaTime);
            }
        }

        //�X�s�[�h�ɂ���đ���̉�������ς���
        Vector3 adjustedDragVector = dragVector * speed / 100;


        characterPhysics.maxAngularVelocity = 7;//��]�̏��

        //�O���ւ̑��x���Z�o
        float forwardSpeed = Vector3.Dot(characterPhysics.velocity, transform.forward);
        //���샂�[�h�ɂ���đ����ς���

        if (operationMode == 0)
        {
            //�L�����̊p�x�Ɋ֌W�Ȃ��A�^��ɏ㏸
            //���������Ő��܂��͂��v�Z
            float pitch = forwardSpeed * adjustedDragVector.y / sensitivity.y;
            if (invert == false && isStart == false)
            {
                pitch *= -1;//�㉺���]���[�h���I�t�������甽�]������B
            }
            characterPhysics.AddTorque(transform.right * pitch * Time.deltaTime);
            //���̈ړ��́A�@�̂��X���遨�㏸�ƃ��[�𓯎��ɍs��
            float rollAngle = transform.localEulerAngles.z;
            if (rollAngle >= 181)
            {
                rollAngle -= 360;
            }
            //���ɌX����+�A�E�ɌX����-
            float angleDiff = rollAngle - (-60 * dragVector.x / 200);
            //Debug.Log(-45*dragVector.x / 200);
            characterPhysics.AddTorque(transform.forward * -angleDiff * Time.deltaTime*7);//�X������
            characterPhysics.AddTorque(transform.right * Mathf.Abs(Mathf.Sin(rollAngle * Mathf.Deg2Rad)) * Time.deltaTime* -Mathf.Abs(adjustedDragVector.x));//�Ȃ��邽�߂̃s�b�`����
            characterPhysics.AddTorque(transform.up * Mathf.Cos(rollAngle * Mathf.Deg2Rad) * Time.deltaTime * adjustedDragVector.x*5);
            

            //�^�[���Ȃǂŏ㉺���]�����Ƃ��ɁA���ǒ��͉�]�����Ȃ�
        }
        else if (operationMode == 1)
        {
            float rollAngle = transform.localEulerAngles.z;
            if (rollAngle >= 181)
            {
                rollAngle -= 360;
            }
            Debug.Log(rollAngle + "-"+(-45 * dragVector.x / 200) + "=" + (rollAngle - (-45 * dragVector.x / 200)));
            //��{�p����ϊ�
            //���������Ő��܂��͂��v�Z
            float pitch = forwardSpeed * adjustedDragVector.y / sensitivity.y;
            float roll = forwardSpeed * adjustedDragVector.x / sensitivity.x;
            if (invert == false && isStart==false)
            {
                pitch *= -1;//�㉺���]���[�h���I�t�������甽�]������B
            }
            //�p���ɔ��f
            characterPhysics.AddTorque(transform.right * pitch * Time.deltaTime);
            characterPhysics.AddTorque(-transform.forward * roll * Time.deltaTime);

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
        float mainAttackAngle = Vector3.Angle(characterPhysics.velocity, transform.up) - 90; //���̋p�̂���
        speed = Mathf.Sqrt(characterPhysics.velocity.x * characterPhysics.velocity.x + characterPhysics.velocity.y * characterPhysics.velocity.y + characterPhysics.velocity.z * characterPhysics.velocity.z);
        characterPhysics.AddForce(Time.deltaTime * transform.up * mainAttackAngle * speed*2);
        //���������B���ɃX���C�h���Ȃ��悤�ɂ��A�i�s�����ɓ���������
        float tailAttackAngle = Vector3.Angle(characterPhysics.velocity, transform.right) - 90;
        characterPhysics.AddForce(Time.deltaTime * transform.right * tailAttackAngle * speed/2);
        characterPhysics.AddTorque(Time.deltaTime * transform.up * -tailAttackAngle * speed/2);
        //���x�ɂ���Ďp����ς���B�i�������͉��������j
        if (1 - (speed / 100) > 0)
        {
            characterPhysics.AddTorque(Time.deltaTime * transform.right * mainAttackAngle * speed * (1 - (speed / 100)));
           
        }

        //����������B
        //�����`�[�g
        if (Input.GetKey("l"))
        {
            characterPhysics.AddForce(transform.forward * 6000 * Time.deltaTime);
        }

        if (isAcceleration == true)
        {
            characterPhysics.AddForce(transform.forward * 20000 * Time.deltaTime);
            accelCount += Time.deltaTime;
        }
        if (accelCount > 0.3f)//�����I���i�����j
        {
            isAcceleration = false;
        }

        //���x�x�N�g�����J�����ɓ`����
        if (isStart == false)
        {
            Camera.main.GetComponent<CameraControl>().CameraTrace(characterPhysics.velocity, this.gameObject.transform.position,isDead);
        }


        //�X�R�A���Z����
        Camera.main.GetComponent<ScoreManage>().ScoreCalc(Vector3.Magnitude(characterPhysics.velocity)*Time.deltaTime);
        MotionControl();
    }
    public void MotionControl()
    {
        //��{�p���Ƀj�A�~�X���Ȃǂ̃��[�������[�V�������������p�������Z���A�L�����N�^�[�ɔ��f����B�������̂͏㔼�g�̃u���b�N�݂̂œ����Ǝl���̓����ɂ͂������Ȃ��B
	    Animator animator = body.GetComponent<Animator>();
        if (isNear == 1)
        {
            //���ɉ�]
		    animator.SetTrigger("LeftRotation");
		    isNear = 0;
        }
        else if (isNear == 2)
        {
            //�E�ɉ�]
            animator.SetTrigger("RightRotation");
            isNear = 0;
        }
                
    }

    public void setDead()
    {
        isDead = true;
    }
}
