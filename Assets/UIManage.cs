using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIManage : MonoBehaviour
{
    // Start is called before the first frame update
    [SerializeField] GameObject StartMenuPanel;
    [SerializeField] GameObject GamePanel;
    void Start()
    {
        StartMenuPanel.SetActive(true);
        GamePanel.SetActive(false);
    }

    public void CloseStartMenu()
    {
        //Panel.SetActive(fales);
        StartMenuPanel.SetActive(false);
        GamePanel.SetActive(false);
    }
}
