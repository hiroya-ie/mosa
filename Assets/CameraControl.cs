using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraControl : MonoBehaviour
{
    /*
     * カメラを制御する。
     */ 

    public void CameraTrace(Vector3 playerVelocity,Vector3 playerPos)
    {
        /*
         * プレイヤーの進行方向を向きながらプレイヤーを追いかける。
         */
        transform.LookAt(playerVelocity+playerPos);//プレイヤーを見る
        Vector3 cameraPos = playerPos - (playerVelocity/2);
        transform.position = Vector3.Lerp(this.gameObject.transform.position, cameraPos, Time.deltaTime*4);
    }

    public void CameraPosSet(Vector3 cameraPos,Vector3 cameraAngle)
    {
        /*
         * 引数で指定された座標にカメラを移動させる。
         */ 

    }
}
