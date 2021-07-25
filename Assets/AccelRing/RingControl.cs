using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RingControl : MonoBehaviour
{
    GameObject player;
    [SerializeField] GameObject[] slimRing;
    bool Proximity = false;
    public AnimationCurve curve;
    float timeCount = 0;
    float scale;
    public bool hit = false;
    // Start is called before the first frame update
    void Start()
    {
        player = Camera.main.GetComponent<GameManage>().character;
    }

    // Update is called once per frame
    void Update()
    {
        float distance = Vector3.Distance(player.transform.position, this.gameObject.transform.position);
        if (distance < 200 && Proximity == false)
        {
            Proximity = true;
        }
        if (Proximity == true)
        {
            timeCount += Time.deltaTime;
        }

        transform.localEulerAngles = new Vector3(90 * curve.Evaluate(timeCount) + 45, -90, 90);
        if (hit == true)
        {
            transform.localScale = Vector3.Lerp(transform.localScale, new Vector3(1200, 1200, 1200), Time.deltaTime * 5);
        }
            for (int i = 0; i < slimRing.Length; i++)
            {
                scale = 1 - curve.Evaluate(timeCount) * ((i + 1) / 5f);
                slimRing[i].transform.localScale = new Vector3(scale, scale, scale);
                slimRing[i].transform.localPosition = new Vector3(0, curve.Evaluate(timeCount - i / 7f) * ((i + 1) / -20f), 0);
                slimRing[i].transform.localEulerAngles = new Vector3(0, curve.Evaluate(timeCount) * (i + 1) * -10, 0);
            }



    }
}
