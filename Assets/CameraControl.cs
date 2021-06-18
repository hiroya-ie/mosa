using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraControl : MonoBehaviour
{
    /*
     * カメラを制御する。
     */
    Vector3 speed = new Vector3(0, 0, 0);
    float xVelocity;
    float yVelocity;
    float zVelocity;
    float traceSpeed = 5f;
    float TARGET_TRACE_SPEED = 0.3f;
    float rotateSpeed = 5f;
    float TARGET_ROTATE_SPEED = 0.5f;
    Vector3 cameraPos;


    public void CameraTrace(Vector3 playerVelocity,Vector3 playerPos,bool isDead)
    {
        /*
         * プレイヤーの進行方向を向きながらプレイヤーを追いかける。
         */
        //角度を変更
        if (rotateSpeed > TARGET_ROTATE_SPEED)
        {
            rotateSpeed -= Time.deltaTime;
        }
        else
        {
            rotateSpeed = TARGET_ROTATE_SPEED;
        }
        if (rotateSpeed < 4.2f)
        {
            Vector3 relativePos = playerPos - this.transform.position;
            Vector3 angle = Quaternion.LookRotation(relativePos).eulerAngles;

            float xRotate = Mathf.SmoothDampAngle(this.gameObject.transform.eulerAngles.x, angle.x, ref xVelocity, rotateSpeed);
            float yRotate = Mathf.SmoothDampAngle(this.gameObject.transform.eulerAngles.y, angle.y, ref yVelocity, rotateSpeed);
            float zRotate = Mathf.SmoothDampAngle(this.gameObject.transform.eulerAngles.z, angle.z, ref zVelocity, rotateSpeed);
            transform.eulerAngles = new Vector3(xRotate, yRotate, zRotate);//xyzすべて0になるときにエラー吐くっぽい？
        }

        //transform.rotation = Quaternion.Slerp(this.transform.rotation, angle, 0.1f);
        //プレイヤーを追いかける
        if (traceSpeed > TARGET_TRACE_SPEED)
        {
            traceSpeed -= Time.deltaTime;
        }
        else
        {
            traceSpeed = TARGET_TRACE_SPEED;
        }


        if (isDead == false)
        {
            cameraPos = playerPos - (playerVelocity / 6);
        }
        else
        {
            cameraPos.y += 0.15f;
        }

        //transform.position = Vector3.Lerp(this.gameObject.transform.position, cameraPos, Time.deltaTime*10);
        transform.position = Vector3.SmoothDamp(transform.position, cameraPos, ref speed, traceSpeed);

    }

    public void CameraPosSet(Vector3 cameraPos,Vector3 cameraAngle)
    {
        /*
         * 引数で指定された座標にカメラを移動させる。
         */
        transform.position = cameraPos;
        transform.rotation = Quaternion.Euler(cameraAngle);
        traceSpeed = 5f;
        rotateSpeed = 5f;
        speed = new Vector3(0, 0, 0);
        xVelocity = 0;
        yVelocity = 0;
        zVelocity = 0;
    }
}
