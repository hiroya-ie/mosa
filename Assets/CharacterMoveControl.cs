using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterMoveControl : MonoBehaviour
{
    //設定可能変数
    public int operationMode = 1;
    public bool invert = true;
    public Vector3 sensitivity = new Vector3(60, 40, 0);
    [SerializeField] GameObject Canvas;
    public float XSensitivity;
    public float YSensitivity;

    bool isAcceleration;
    float accelDuration = 1f;
    [SerializeField] AnimationCurve accelRollCurve;
    //attitudeControl()用関数
    Vector3 basicAttitude;//基本姿勢
    Vector3 firstMousePosition;
    Vector3 mousePosition;
    int isNear;
    //FlyControl()用関数
    float speed;
    //MotionControl
    [SerializeField] GameObject body;
    //ゲーム開始アニメーション
    bool isStart = false;
    //死亡
    bool isDead = false;
    float deadCount = 0;
    //カメラ操作
    Vector3 cameraPos;
    //スコアを動かす
    [SerializeField] GameObject scoreDisplay;
    float currentScoreAngle;
    float cameraAttitude;
    

    //実験中
    float accelCount;

    //isAccelをtrueにセット（実験）
    public void AccelSet()
    {
        isAcceleration = true;
        accelCount = 0;
    }

    //ゲーム開始アニメーション用
    public void StartSet()
    {
        isStart = true;
    }

    public void GameOver()
    {
        //ゲームオーバー時の処理
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
        scoreDisplay.transform.position = new Vector3(4, -27.85526f, 12.53788f);
        isDead = false;
    }

    private float RollAngle()
    {
        float rollAngle = transform.localEulerAngles.z;
        if (rollAngle >= 181)
        {
            rollAngle -= 360;
        }
        return rollAngle;
    }

    public void AttitudeControl()
    {
        Rigidbody characterPhysics = GetComponent<Rigidbody>();
        if (isDead == true)
        {
            Camera.main.GetComponent<CameraControl>().CameraTrace(characterPhysics.velocity, this.gameObject.transform.position,isDead);
            this.gameObject.GetComponent<Rigidbody>().angularDrag = 0;
            if (deadCount == 0)
            {
                cameraAttitude = Camera.main.transform.position.y+100;
            }
            deadCount += Time.deltaTime;
            if (deadCount > 6)
            {
                
                deadCount = 0;
                GameOver();
            }
            else
            {
                scoreDisplay.gameObject.transform.position = new Vector3(Camera.main.transform.position.x, cameraAttitude, Camera.main.transform.position.z);
            }
            return;
        }

        scoreDisplay.transform.position = Vector3.Lerp(scoreDisplay.transform.position, this.gameObject.transform.position+transform.right*3, Time.deltaTime*5);
        scoreDisplay.transform.rotation = Quaternion.LookRotation(scoreDisplay.transform.position - (Camera.main.transform.position+Camera.main.transform.forward*5));
        scoreDisplay.transform.Rotate(0, 0, currentScoreAngle);
        Vector3 characterAngleVector = new Vector3(Mathf.Cos(this.gameObject.transform.eulerAngles.z * Mathf.Deg2Rad), Mathf.Sin(this.gameObject.transform.eulerAngles.z * Mathf.Deg2Rad), 0);
        Vector3 scoreAngleVector = new Vector3(Mathf.Cos(scoreDisplay.transform.eulerAngles.z * Mathf.Deg2Rad), Mathf.Sin(scoreDisplay.transform.eulerAngles.z * Mathf.Deg2Rad), 0);
        float characterScoreAngle = Vector3.SignedAngle(scoreAngleVector, characterAngleVector, new Vector3(0, 0, 1));
        currentScoreAngle = currentScoreAngle + characterScoreAngle * (Time.deltaTime * 5);

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

        if (isStart == true)//ゲーム開始アニメーション
        {
            if (transform.rotation.eulerAngles.x+10 > 180)
            {
                isStart = false;
                Debug.Log (XSensitivity);
                sensitivity = new Vector3(XSensitivity, YSensitivity, 0);
            }
            else
            {
                sensitivity = new Vector3(60, 40, 0);
                dragVector = new Vector3(0, -10 * transform.rotation.eulerAngles.x, 0);
                characterPhysics.AddForce(transform.forward * 4000 * Time.deltaTime);
            }
        }

        //スピードによって操作の応答性を変える
        Vector3 adjustedDragVector = dragVector * speed / 100;


        characterPhysics.maxAngularVelocity = 7;//回転の上限

        //前方への速度を算出
        float forwardSpeed = Vector3.Dot(characterPhysics.velocity, transform.forward);
        //操作モードによって操作を変える

        if (operationMode == 0)
        {
            Debug.Log("standard");
            //キャラの角度に関係なく、真上に上昇
            //水平尾翼で生まれる力を計算
            float pitch = forwardSpeed * adjustedDragVector.y / sensitivity.y;
            if (invert == true && isStart == false)
            {
                pitch *= -1;//上下反転モードがオフだったら反転させる。
            }
            characterPhysics.AddTorque(transform.right * pitch * Time.deltaTime);
            //横の移動は、機体を傾ける→上昇とヨーを同時に行う
            //左に傾くと+、右に傾くと-
            float angleDiff = RollAngle() - (-60 * dragVector.x / 200);
            //Debug.Log(-45*dragVector.x / 200);
            characterPhysics.AddTorque(transform.forward * -angleDiff * Time.deltaTime*7);//傾き制御
            characterPhysics.AddTorque(transform.right * Mathf.Abs(Mathf.Sin(RollAngle() * Mathf.Deg2Rad)) * Time.deltaTime* -Mathf.Abs(adjustedDragVector.x));//曲がるためのピッチ制御
            characterPhysics.AddTorque(transform.up * Mathf.Cos(RollAngle() * Mathf.Deg2Rad) * Time.deltaTime * adjustedDragVector.x*5);
            

            //ターンなどで上下反転したときに、操舵中は回転させない
        }
        else if (operationMode == 1)
        {
            //Debug.Log(rollAngle + "-"+(-45 * dragVector.x / 200) + "=" + (rollAngle - (-45 * dragVector.x / 200)));
            //基本姿勢を変換
            //水平尾翼で生まれる力を計算
            float pitch = forwardSpeed * adjustedDragVector.y / sensitivity.y;
            float roll = forwardSpeed * adjustedDragVector.x / sensitivity.x;
            if (invert == true && isStart==false)
            {
                pitch *= -1;//上下反転モードがオフだったら反転させる。
            }
            //姿勢に反映
            characterPhysics.AddTorque(transform.right * pitch * Time.deltaTime);
            characterPhysics.AddTorque(-transform.forward * roll * Time.deltaTime);

        }
        FlyControl();
        /*
        //ニアミス判定実験
        if (Input.GetKey("o"))
        {
            isNear = 1;//左仮)
        }
        else if (Input.GetKey("p"))
        {
            isNear = 2;//右(仮)
        }
        */

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
        characterPhysics.AddForce(Time.deltaTime * transform.right * tailAttackAngle * speed/2);
        characterPhysics.AddTorque(Time.deltaTime * transform.up * -tailAttackAngle * speed/2);
        //速度によって姿勢を変える。（失速時は下を向く）
        if (1 - (speed / 100) > 0)
        {
            characterPhysics.AddTorque(Time.deltaTime * transform.right * mainAttackAngle * speed * (1 - (speed / 100)));
           
        }

        //加速させる。
        //加速チート
        if (Input.GetKey("l"))
        {
            characterPhysics.AddForce(transform.forward * 6000 * Time.deltaTime);
        }

        if (isAcceleration == true)
        {
            characterPhysics.AddForce(transform.forward * 10000 * Time.deltaTime);
            float roll = accelRollCurve.Evaluate(accelCount / accelDuration) * -360;
            body.transform.localRotation = Quaternion.Euler(0, 0, roll);
            accelCount += Time.deltaTime;
        }
        if (accelCount > accelDuration)//加速終了（実験）
        {
            isAcceleration = false;
        }

        //速度ベクトルをカメラに伝える
        if (isStart == false)
        {
            Camera.main.GetComponent<CameraControl>().CameraTrace(characterPhysics.velocity, this.gameObject.transform.position+this.gameObject.transform.forward*Vector3.Magnitude(characterPhysics.velocity)/5,isDead);
        }


        //スコア加算命令
        Camera.main.GetComponent<ScoreManage>().ScoreCalc(Vector3.Magnitude(characterPhysics.velocity)*Time.deltaTime);
        //MotionControl();
    }
    public void MotionControl()
    {

        //基本姿勢にニアミス時などのロール等モーションを加えた姿勢を演算し、キャラクターに反映する。動かすのは上半身のブロックのみで頭部と四肢の動きにはかかわらない。
	    /*Animator animator = body.GetComponent<Animator>();
        if (isNear == 1)
        {
            //左に回転
		    animator.SetTrigger("LeftRotation");
		    isNear = 0;
        }
        else if (isNear == 2)
        {
            //右に回転
            animator.SetTrigger("RightRotation");
            isNear = 0;
        }*/
                
    }

    public void setDead()
    {
        isDead = true;
    }
}
