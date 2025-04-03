using System.Collections;
using System.Collections.Generic;
using System.IO.Ports;
using UnityEngine;

public class ex5_script : MonoBehaviour
{
    public ArticulationBody rod;

    SerialPort arduino = new SerialPort();

    void Start()
    {
        arduino.BaudRate = 9600;
        arduino.PortName = "COM3";

        arduino.Open();
        if (arduino.IsOpen)
        {
            print("아두이노랑 연결됨!");
        }

    }

    void Update()
    {
        //아두이노랑 연결이 되었으면서, 수신버퍼에 가져올 데이터가 있는가?
        if (arduino.IsOpen)
        {
            if (arduino.BytesToRead > 0)
            {
                //아두이노가 보낸 0~1023을 읽는다
                string data = arduino.ReadLine(); //0~1023의 문자열
                int analog = int.Parse(data); //0~1023사이의 숫자
                float position = 1.5f*analog/1023; //0~1023을 0~1.5로 비율에 맞게 바꾸기
                rod.SetDriveTarget(ArticulationDriveAxis.Z, position);
            }
        }
    }
}
