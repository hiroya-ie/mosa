using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraControl : MonoBehaviour
{
    /*
     * �J�����𐧌䂷��B
     */ 

    public void CameraTrace(Vector3 playerVelocity,Vector3 playerPos)
    {
        /*
         * �v���C���[�̐i�s�����������Ȃ���v���C���[��ǂ�������B
         */
        transform.LookAt(playerVelocity+playerPos);//�v���C���[������
        Vector3 cameraPos = playerPos - (playerVelocity/2);
        transform.position = Vector3.Lerp(this.gameObject.transform.position, cameraPos, Time.deltaTime*4);
    }

    public void CameraPosSet(Vector3 cameraPos,Vector3 cameraAngle)
    {
        /*
         * �����Ŏw�肳�ꂽ���W�ɃJ�������ړ�������B
         */ 

    }
}
