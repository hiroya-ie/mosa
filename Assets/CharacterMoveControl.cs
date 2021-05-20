using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterMoveControl : MonoBehaviour
{
    bool isAcceleration;
    //attitudeControl()用関数
    Vector3 basicAttitude;//基本姿勢
    Vector3 firstMousePosition;
    Vector3 mousePosition;
    float K = 0.01f; //空気抵抗の比例係数
    int Xsensitivity = 5000;//横方向の感度
    int Ysensitivity = 10000;//縦方向の感度
    float XtorqueVelocity;
    float YtorqueVelocity;
    //FlyControl()用関数
    float speed;

    //実験中
    float count;

    //isAccelをtrueにセット（実験）
    public void AccelSet()
    {
        isAcceleration = true;
        count = 0;
    }

    public void AttitudeControl()
    {
        /*ドラッグを検出して基本姿勢に反映する。*/
        Vector3 dragVector = new Vector3(0,0,0);
        //ドラッグを取得
        if (Input.GetMouseButtonDown(0))
        {
            firstMousePosition = Input.mousePosition;
        }
        if (Input.GetMouseButton(0))
        {
            mousePosition = Input.mousePosition;
            dragVector = mousePosition - firstMousePosition;
        }

        //キー操作（実験）
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

        //ドラッグの限界を設ける
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
        //スピードによって操作の応答性を変える
        dragVector *= speed / 100;

        

        //基本姿勢を変換
        //左右方向の加速度を計算（空気抵抗ありで）float
        float XtorqueAccel = -dragVector.x / Xsensitivity - K * XtorqueVelocity;
        //左右方向の速度を計算 float
        XtorqueVelocity += XtorqueAccel;

        //上下方向の加速度を計算（空気抵抗ありで）
        float YtorqueAccel = dragVector.y / Ysensitivity - K * YtorqueVelocity;
        //上下方向の速度を計算 float
        YtorqueVelocity += YtorqueAccel;

        //基本姿勢に反映
        basicAttitude += transform.forward * XtorqueVelocity + transform.right * YtorqueVelocity;
        //transform.rotation = Quaternion.Euler(basicAttitude);
        //キャラクターに反映
        Rigidbody characterPhysics = GetComponent<Rigidbody>();
        characterPhysics.maxAngularVelocity = 50;
        characterPhysics.angularVelocity = transform.forward * XtorqueVelocity + transform.right * YtorqueVelocity;
        FlyControl();
    }

    public void FlyControl()
    {
        /*基本姿勢とスピードに応じて力学的な演算を行い、速度ベクトルを調整する*/
        Rigidbody characterPhysics = GetComponent<Rigidbody>();
        //進行方向と基本姿勢の角度差を求める。揚力、抗力が決まるため。基本姿勢の法線ベクトルと進行方向との角度差を使う。
        //主翼の揚力
        float mainAttackAngle = Vector3.Angle(characterPhysics.velocity, transform.up)-90; //翼の仰角のこと
        speed = Mathf.Sqrt(characterPhysics.velocity.x * characterPhysics.velocity.x + characterPhysics.velocity.y * characterPhysics.velocity.y + characterPhysics.velocity.z * characterPhysics.velocity.z);
        characterPhysics.AddForce(transform.up * mainAttackAngle * speed/160);
        //垂直尾翼。横にスライドしないようにし、進行方向に頭を向ける
        float tailAttackAngle = Vector3.Angle(characterPhysics.velocity, transform.right) - 90;
        characterPhysics.AddForce(transform.right * tailAttackAngle * speed / 320);
        characterPhysics.AddTorque(transform.up * -tailAttackAngle * speed / 320);
        //速度によって姿勢を変える。（失速時は下を向く）
        if(-0.1f * speed + 10f > 0){
            characterPhysics.AddTorque(new Vector3((-0.1f * speed + 10)* Vector3.Angle(characterPhysics.velocity, transform.forward) / 200, 0, 0));
        }
        //加速させる。
        if (isAcceleration == true)
        {
            characterPhysics.AddForce(transform.forward * 50);
            count += Time.deltaTime;
        }
        if (count > 0.3f)//加速終了（実験）
        {
            isAcceleration = false;
        }
        //速度ベクトルをカメラに伝える
        //スコア加算命令
    }
}
