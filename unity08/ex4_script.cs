using System.Collections;
using System.Collections.Generic;
using System.IO.Ports;
using UnityEngine;
using UnityEngine.UI;

public class ex4_script : MonoBehaviour
{
    //버튼3개를 잡아먹어서 각버튼을 누르면 아두이노에게 통신한다
    public Button ex4_btn1;
    public Button ex4_btn2;
    public Button ex4_btn3;
    public Button ex4_btn4;

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

        //버튼을 눌렀을때 버튼별로 전송할 값을 지정한다!
        ex4_btn1.onClick.AddListener(() => {
            //아두이노에게 0도값을 전송한다
            if (arduino.IsOpen)
            {
                arduino.Write("256\n");
            }
        });
        ex4_btn2.onClick.AddListener(() => {
            //아두이노에게 90도값을 전송한다
            if (arduino.IsOpen)
            {
                arduino.Write("-256\n");
            }
        });
        ex4_btn3.onClick.AddListener(() => {
            //아두이노에게 180도값을 전송한다
            if (arduino.IsOpen)
            {
                arduino.Write("512\n");
            }
        });
        ex4_btn4.onClick.AddListener(() => {
            //아두이노에게 180도값을 전송한다
            if (arduino.IsOpen)
            {
                arduino.Write("-512\n");
            }
        });
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
