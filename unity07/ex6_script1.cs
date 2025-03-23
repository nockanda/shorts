using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ex6_script1 : MonoBehaviour
{
    public int value = 5;

    void Start()
    {
        
    }

    void Update()
    {
        //매 프레임 반복 실행되는부분
        if (Input.GetKeyDown(KeyCode.RightArrow))
        {
            GetComponent<Rigidbody>().AddForce(1, value, 0, ForceMode.Impulse);
        }

        else if (Input.GetKeyDown(KeyCode.LeftArrow))
        {
            GetComponent<Rigidbody>().AddForce(-1, value, 0, ForceMode.Impulse);
        }
    }
}
