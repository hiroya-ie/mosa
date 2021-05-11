using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterMoveControl : MonoBehaviour
{

    //attitudeControl()用関数
    Vector3 basicAttitude;//基本姿勢
    Vector3 firstMousePosition;
    Vector3 mousePosition;
    float K = 0.01f; //空気抵抗の比例係数
    int Xsensitivity = 5000;//横方向の感度
    int Ysensitivity = 10000;//縦方向の感度
    float XtorqueVelocity;
    float YtorqueVelocity;
    public void AttitudeControl()
    {
        /*ドラッグを検出して基本姿勢に反映する。*/
        //////////////////////////////////////////////////スピードで応答性を変える
        //ドラッグの範囲に限界を設ける
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
        //キャラクターに反映（実験）
        Rigidbody characterPhysics = GetComponent<Rigidbody>();
        characterPhysics.angularVelocity = transform.forward * XtorqueVelocity + transform.right * YtorqueVelocity;
        //加速（実験）
        if (Input.GetKey("l"))
        {
            characterPhysics.AddForce(transform.forward * 1);
        }
        FlyControl();
    }

    public void FlyControl()
    {
        /*基本姿勢とスピードに応じて力学的な演算を行い、速度ベクトルを調整する*/
        Rigidbody characterPhysics = GetComponent<Rigidbody>();
        //進行方向と基本姿勢の角度差を求める。揚力、抗力が決まるため。基本姿勢の法線ベクトルと進行方向との角度差を使う。
        //主翼の揚力
        float attackAngle = Vector3.Angle(characterPhysics.velocity, transform.up)-90; //翼の仰角のこと
        float speed = Mathf.Sqrt(characterPhysics.velocity.x * characterPhysics.velocity.x + characterPhysics.velocity.y * characterPhysics.velocity.y + characterPhysics.velocity.z * characterPhysics.velocity.z);
        characterPhysics.AddForce(transform.up * attackAngle * speed/300);
        Debug.Log(attackAngle);
        //垂直尾翼

    }
}
