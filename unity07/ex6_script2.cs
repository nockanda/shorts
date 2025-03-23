using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ex6_script2 : MonoBehaviour
{
    //ex6_script3.cs를 가지고있는 ex6_arduino라는 빈오브젝트를 잡아먹어야한다!
    public GameObject ex6_arduino;

    //큐브랑 충돌이 났냐 아니냐?
    private void OnCollisionEnter(Collision collision)
    {
        //object(this)에 뭔가 충돌이 발생했다!
        GetComponent<Renderer>().material.color = Color.red;
        //ex6_script3.cs에 있는 led_on이라는 함수를 호출하고싶다!
        ex6_arduino.GetComponent<ex6_script3>().led_on();
    }
    private void OnCollisionExit(Collision collision)
    {
        //object(this)에 뭔가 충돌이 되었다가 접촉이 해제되었다!
        GetComponent<Renderer>().material.color = Color.white;
        //ex6_script3.cs에 있는 led_off이라는 함수를 호출하고싶다!
        ex6_arduino.GetComponent<ex6_script3>().led_off();
    }

}
