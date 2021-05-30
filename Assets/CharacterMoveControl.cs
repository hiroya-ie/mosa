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
    Vector3 sensitivity = new Vector3(60, 40, 0);
    int isNear;
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

        //前方への速度を算出
        Rigidbody characterPhysics = GetComponent<Rigidbody>();
        float forwardSpeed = Vector3.Dot(characterPhysics.velocity,transform.forward);
        //水平尾翼で生まれる力を計算
        float pitch = forwardSpeed * dragVector.y / sensitivity.y;
        float roll = forwardSpeed * dragVector.x / sensitivity.x;

        //姿勢に反映
        characterPhysics.maxAngularVelocity = 7;//回転の上限
        characterPhysics.AddTorque(transform.right * pitch*Time.deltaTime);
        characterPhysics.AddTorque(-transform.forward * roll * Time.deltaTime);



        FlyControl();
        //ニアミス判定実験
        if (Input.GetKey("o"))
        {
            isNear = 1;//左仮)
        }
        else if (Input.GetKey("p"))
        {
            isNear = 2;//右(仮)
        }

    }

    public void FlyControl()
    {
        /*基本姿勢とスピードに応じて力学的な演算を行い、速度ベクトルを調整する*/
        Rigidbody characterPhysics = GetComponent<Rigidbody>();
        //進行方向と基本姿勢の角度差を求める。揚力、抗力が決まるため。基本姿勢の法線ベクトルと進行方向との角度差を使う。
        //主翼の揚力
        float mainAttackAngle = Vector3.Angle(characterPhysics.velocity, transform.up) - 90; //翼の仰角のこと
        speed = Mathf.Sqrt(characterPhysics.velocity.x * characterPhysics.velocity.x + characterPhysics.velocity.y * characterPhysics.velocity.y + characterPhysics.velocity.z * characterPhysics.velocity.z);
        characterPhysics.AddForce(Time.deltaTime * transform.up * mainAttackAngle * speed*2);
        //垂直尾翼。横にスライドしないようにし、進行方向に頭を向ける
        float tailAttackAngle = Vector3.Angle(characterPhysics.velocity, transform.right) - 90;
        characterPhysics.AddForce(Time.deltaTime * transform.right * tailAttackAngle * speed);
        characterPhysics.AddTorque(Time.deltaTime * transform.up * -tailAttackAngle * speed);
        //速度によって姿勢を変える。（失速時は下を向く）
        if (1 - (speed / 100) > 0)
        {
            characterPhysics.AddTorque(Time.deltaTime * transform.right * mainAttackAngle * speed * (1 - (speed / 100)));
        }

        //加速させる。
        if (isAcceleration == true)
        {
            characterPhysics.AddForce(transform.forward * 20000 * Time.deltaTime);
            count += Time.deltaTime;
        }
        if (count > 0.3f)//加速終了（実験）
        {
            isAcceleration = false;
        }

        //速度ベクトルをカメラに伝える
        Camera.main.GetComponent<CameraControl>().CameraTrace(characterPhysics.velocity,this.gameObject.transform.position);
        //スコア加算命令
        Camera.main.GetComponent<ScoreManage>().ScoreCalc(Vector3.Magnitude(characterPhysics.velocity)/5000);
        MotionControl();
    }
    public void MotionControl()
    {
        //基本姿勢にニアミス時などのロール等モーションを加えた姿勢を演算し、キャラクターに反映する。動かすのは上半身のブロックのみで頭部と四肢の動きにはかかわらない。
        GameObject Body = GameObject.Find("Body");
        if (isNear == 1)
        {
            //左に回転
            Body.transform.Rotate(0, 0, 5);
            isNear = 0;
        }
        else if (isNear == 2)
        {
            //右に回転
            Body.transform.Rotate(0, 0, -5);
            isNear = 0;
        }
    }
}
