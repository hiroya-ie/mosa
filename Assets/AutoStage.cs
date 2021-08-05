using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using UnityEngine.SceneManagement;

public class AutoStage : MonoBehaviour
{

    int StageSize = 1000;
    int StageIndex;

    public Transform Target;//Unitychan
    public GameObject[] stagenum;//ステージのプレハブ
    public GameObject[] stagenum1;
    public int FirstStageIndex;//スタート時にどのインデックスからステージを生成するのか
    public int aheadStage; //事前に生成しておくステージ
    public List<GameObject> StageList = new List<GameObject>();//生成したステージのリスト
    int stage;
    [SerializeField] GameObject Fog;
    Color32 fogDesertColor = new Color32(204, 182, 155, 245);
    Color32 fogForestColor = new Color32(255, 255, 255, 255);

    // Start is called before the first frame update
    void Start()
    {
        stage = Random.Range(0, 2);
        if (stage == 0)
        {
            //砂漠ステージ

            Fog.GetComponent<Renderer>().material.SetColor("_FogColor", fogDesertColor);
        }
        else if (stage == 1)
        {
            //森ステージ
                        Fog.GetComponent<Renderer>().material.SetColor("_FogColor", fogForestColor);

        }
        StageIndex = FirstStageIndex - 1;
        StageManager(aheadStage);
    }

    // Update is called once per frame
    void Update()
    {
        int targetPosIndex = (int)(Target.position.z / StageSize);

        if (targetPosIndex + aheadStage > StageIndex)
        {
            StageManager(targetPosIndex + aheadStage);
        }
    }
    void StageManager(int maps)
    {
        if (maps <= StageIndex)
        {
            return;
        }
        if (stage == 1)
        {
            for (int i = StageIndex + 1; i <= maps; i++)//指定したステージまで作成する
            {
                GameObject stageObj = MakeStage(i);
                StageList.Add(stageObj);
            }
        }
        else
        {
            for (int i = StageIndex + 1; i <= maps; i++)//指定したステージまで作成する
            {
                GameObject stageObj = MakeStage1(i);
                StageList.Add(stageObj);
            }
        }

        while (StageList.Count > aheadStage + 1)//古いステージを削除する
        {
            DestroyStage();
        }

        StageIndex = maps;
    }

    GameObject MakeStage(int index)//ステージを生成する
    {
        int nextStage = Random.Range(0, stagenum.Length);

        GameObject stageObject = (GameObject)Instantiate(stagenum[nextStage], new Vector3(0, -300, index * StageSize), Quaternion.identity);
        return stageObject;
    }

    GameObject MakeStage1(int index)//ステージを生成する
    {
        int nextStage = Random.Range(0, stagenum.Length);

        GameObject stageObject = (GameObject)Instantiate(stagenum1[nextStage], new Vector3(0, -300, index * StageSize), Quaternion.identity);
        return stageObject;
    }

    void DestroyStage()
    {
        GameObject oldStage = StageList[0];
        StageList.RemoveAt(0);
        Destroy(oldStage);
    }

    void ReloadStage()
    {
        /*
        int nextStage = Random.Range(0, stagenum.Length);
        for(int i=0; i<4; i++){
            StageList.RemoveAt(i);
            Destroy(StageList[i]);
        }
        GameObject stageObject = (GameObject)Instantiate(stagenum[nextStage], new Vector3(0, -300,0), Quaternion.identity);
    */
        SceneManager.LoadScene(0);
    }
}