using System.Collections;
using System.Collections.Generic;
using System.IO.Ports;
using UnityEngine;
using UnityEngine.UI;

public class ex2_script : MonoBehaviour
{
    public Button ex2_btn1;
    public Button ex2_btn2;

    SerialPort arduino = new SerialPort();

    void Start()
    {
        //이벤트 리스너등록
        ex2_btn1.onClick.AddListener(btn1_click);
        ex2_btn2.onClick.AddListener(btn2_click);

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
    void btn1_click()
    {
        //아두이노에게 문자 '1'을 전송한다
        if(arduino.IsOpen)
        {
            arduino.Write("1");
        }
    }
    void btn2_click()
    {
        //아두이노에게 문자 '2'을 전송한다
        if (arduino.IsOpen)
        {
            arduino.Write("2");
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
