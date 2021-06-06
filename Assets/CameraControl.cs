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
    float traceSpeed = 3f;
    float TARGET_TRACE_SPEED = 0.3f;


    public void CameraTrace(Vector3 playerVelocity,Vector3 playerPos)
    {
        /*
         * プレイヤーの進行方向を向きながらプレイヤーを追いかける。
         */
        //角度を変更
        //transform.LookAt((playerVelocity*2)+playerPos);//プレイヤーを見る
        //https://gametukurikata.com/basic/smooth
        Vector3 relativePos = playerPos - this.transform.position;
        Vector3 angle = Quaternion.LookRotation(relativePos).eulerAngles;
        float rotateSpeed = 0.5f;

        float xRotate = Mathf.SmoothDampAngle(this.gameObject.transform.eulerAngles.x, angle.x, ref xVelocity, rotateSpeed);
        float yRotate = Mathf.SmoothDampAngle(this.gameObject.transform.eulerAngles.y, angle.y, ref yVelocity, rotateSpeed);
        float zRotate = Mathf.SmoothDampAngle(this.gameObject.transform.eulerAngles.z, angle.z, ref zVelocity, rotateSpeed);
        transform.eulerAngles = new Vector3(xRotate, yRotate, zRotate);
        //transform.rotation = Quaternion.Slerp(this.transform.rotation, angle, 0.1f);
        //プレイヤーを追いかける
        if(traceSpeed > TARGET_TRACE_SPEED)
        {
            traceSpeed -= Time.deltaTime;
        }
        else
        {
            traceSpeed = TARGET_TRACE_SPEED;
        }
        Vector3 cameraPos = playerPos - (playerVelocity/4);
        //transform.position = Vector3.Lerp(this.gameObject.transform.position, cameraPos, Time.deltaTime*10);
        transform.position = Vector3.SmoothDamp(transform.position, cameraPos, ref speed, traceSpeed);
    }

    public void CameraPosSet(Vector3 cameraPos,Vector3 cameraAngle)
    {
        /*
         * 引数で指定された座標にカメラを移動させる。
         */
        transform.position = cameraPos;
    }
}
