using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterMoveControl : MonoBehaviour
{
    Vector3 firstMousePosition;
    Vector3 mousePosition;
    public void AttitudeControl()
    {
        Vector3 dragVector = new Vector3(0,0,0);
        //ƒhƒ‰ƒbƒO‚ðŽæ“¾
        if (Input.GetMouseButtonDown(0))
        {
            firstMousePosition = Input.mousePosition;
        }
        if (Input.GetMouseButton(0))
        {
            mousePosition = Input.mousePosition;
            dragVector = mousePosition - firstMousePosition;
        }
        Debug.Log(this.gameObject.transform.eulerAngles);
    }
}
