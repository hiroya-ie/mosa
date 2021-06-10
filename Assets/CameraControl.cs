using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraControl : MonoBehaviour
{
    /*
     * �J�����𐧌䂷��B
     */
    Vector3 speed = new Vector3(0, 0, 0);
    float xVelocity;
    float yVelocity;
    float zVelocity;
    float traceSpeed = 5f;
    float TARGET_TRACE_SPEED = 0.3f;
    Vector3 cameraPos;


    public void CameraTrace(Vector3 playerVelocity,Vector3 playerPos,bool isDead)
    {
        /*
         * �v���C���[�̐i�s�����������Ȃ���v���C���[��ǂ�������B
         */
        //�p�x��ύX
        Vector3 relativePos = playerPos - this.transform.position;
        Vector3 angle = Quaternion.LookRotation(relativePos).eulerAngles;
        float rotateSpeed = 0.5f;

        float xRotate = Mathf.SmoothDampAngle(this.gameObject.transform.eulerAngles.x, angle.x, ref xVelocity, rotateSpeed);
        float yRotate = Mathf.SmoothDampAngle(this.gameObject.transform.eulerAngles.y, angle.y, ref yVelocity, rotateSpeed);
        float zRotate = Mathf.SmoothDampAngle(this.gameObject.transform.eulerAngles.z, angle.z, ref zVelocity, rotateSpeed);
        transform.eulerAngles = new Vector3(xRotate, yRotate, zRotate);
        //transform.rotation = Quaternion.Slerp(this.transform.rotation, angle, 0.1f);
        //�v���C���[��ǂ�������
        if(traceSpeed > TARGET_TRACE_SPEED)
        {
            traceSpeed -= Time.deltaTime;
        }
        else
        {
            traceSpeed = TARGET_TRACE_SPEED;
        }


        if (isDead == false)
        {
            cameraPos = playerPos - (playerVelocity/6);
        }
        else
        {
            cameraPos.y += 0.05f;
        }

        //transform.position = Vector3.Lerp(this.gameObject.transform.position, cameraPos, Time.deltaTime*10);
        transform.position = Vector3.SmoothDamp(transform.position, cameraPos, ref speed, traceSpeed);

    }

    public void CameraPosSet(Vector3 cameraPos,Vector3 cameraAngle)
    {
        /*
         * �����Ŏw�肳�ꂽ���W�ɃJ�������ړ�������B
         */
        transform.position = cameraPos;
        transform.rotation = Quaternion.Euler(cameraAngle);
    }
}
