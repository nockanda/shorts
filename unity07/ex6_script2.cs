using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ex6_script2 : MonoBehaviour
{
    //ex6_script3.cs�� �������ִ� ex6_arduino��� �������Ʈ�� ��ƸԾ���Ѵ�!
    public GameObject ex6_arduino;

    //ť��� �浹�� ���� �ƴϳ�?
    private void OnCollisionEnter(Collision collision)
    {
        //object(this)�� ���� �浹�� �߻��ߴ�!
        GetComponent<Renderer>().material.color = Color.red;
        //ex6_script3.cs�� �ִ� led_on�̶�� �Լ��� ȣ���ϰ�ʹ�!
        ex6_arduino.GetComponent<ex6_script3>().led_on();
    }
    private void OnCollisionExit(Collision collision)
    {
        //object(this)�� ���� �浹�� �Ǿ��ٰ� ������ �����Ǿ���!
        GetComponent<Renderer>().material.color = Color.white;
        //ex6_script3.cs�� �ִ� led_off�̶�� �Լ��� ȣ���ϰ�ʹ�!
        ex6_arduino.GetComponent<ex6_script3>().led_off();
    }

}
