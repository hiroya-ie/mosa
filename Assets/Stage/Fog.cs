using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Fog : MonoBehaviour
{
    public Transform Target;
    //point = GameObject.Find("hogehoge").transform.position.x;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 targetPosIndex = Target.position;

        Transform myTransform = this.transform;

        Vector3 pos = myTransform.position;

        pos.y = targetPosIndex.y - 80;
        pos.z = targetPosIndex.z;

        myTransform.position = pos;

    }
}
