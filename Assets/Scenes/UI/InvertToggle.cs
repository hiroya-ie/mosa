using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/UI系のスクリプトを組むときは以下の追記を忘れずに
using UnityEngine.UI;

public class InvertToggle : MonoBehaviour
{
    public Toggle toggle;

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
