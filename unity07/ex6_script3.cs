using System.Collections;
using System.Collections.Generic;
using System.IO.Ports;
using UnityEngine;

public class ex6_script3 : MonoBehaviour
{
    SerialPort arduino = new SerialPort();

    void Start()
    {
        //통신설정하기!
        arduino.BaudRate = 9600;
        arduino.PortName = "COM3";
        //포트 개방하기!
        arduino.Open();

        if (arduino.IsOpen)
        {
            print("아두이노랑 연결됨!");
        }
    }

    public void led_on()
    {
        if(arduino.IsOpen)
        {
            arduino.Write("LED ON\n");
        }
    }
    public void led_off()
    {
        if (arduino.IsOpen)
        {
            arduino.Write("LED OFF\n");
        }
    }

    //유니티가 종료된다면~
    void OnApplicationQuit()
    {
        //아두이노랑 연결을 해제해버린다!
        arduino.Close();
        arduino.Dispose();
        print("아두이노와 유니티의 접속을 해제합니다!");
    }
}
