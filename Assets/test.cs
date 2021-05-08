using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class test : MonoBehaviour
{
    public GameObject character;
    CharacterMoveControl script;
    // Start is called before the first frame update
    void Start()
    {
        script = character.GetComponent<CharacterMoveControl>();
    }

    // Update is called once per frame
    void Update()
    {
        script.AttitudeControl();

    }
}
