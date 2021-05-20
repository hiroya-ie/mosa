using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI

public class Invert : MonoBehaviour
{
    public Toggle toggle;


    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }


    public void onfigUIInvertClick()
    {
        if (toggle.isOn = true)
        {
            UIManage.invert = true;
        }
        else
        {
            UIManage.invert = false;
        }
    }
}
