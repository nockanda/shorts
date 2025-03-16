using System.Collections;
using System.Collections.Generic;
using System.IO.Ports;
using UnityEngine;

public class ex2_script : MonoBehaviour
{
    public ArticulationBody door1;
    public ArticulationBody door2;

    SerialPort arduino = new SerialPort();

    void Start()
    {
        //아두이노랑 통신설정
        arduino.PortName = "COM3";
        arduino.BaudRate = 9600;

        arduino.Open(); //포트 개방!

        if (arduino.IsOpen)
        {
            print("아두이노랑 연결됨!");
        }
    }

    void Update()
    {
        if (arduino.IsOpen)
        {
            if (arduino.BytesToRead > 0)
            {
                //아두이노가 1개의 문자를 전송하니까 1개만 읽으면 OK!
                int data = arduino.ReadByte();
                if (data == '0')
                {
                    print("감지가 안됨!");
                    //door1과 door2가 1.9의값을 가지면 닫힘!
                    door1.SetDriveTarget(ArticulationDriveAxis.Y, 1.9f);
                    door2.SetDriveTarget(ArticulationDriveAxis.Y, 1.9f);
                }
                else if (data == '1')
                {
                    print("물건이 감지됨!!");
                    //door1과 door2가 0의값을 가지면 열림!
                    door1.SetDriveTarget(ArticulationDriveAxis.Y, 0f);
                    door2.SetDriveTarget(ArticulationDriveAxis.Y, 0f);
                }
            }
        }
    }
}
