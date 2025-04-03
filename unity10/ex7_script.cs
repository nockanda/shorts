using System.Collections;
using System.Collections.Generic;
using System.IO.Ports;
using UnityEngine;

public class ex7_script : MonoBehaviour
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
                //x값,y값
                string data = arduino.ReadLine(); //0~1023의 문자열
                string[] values = data.Split(',');

                if(values.Length == 2)
                {
                    //숫자로 바꾸기~
                    int x = int.Parse(values[0]);
                    int y = int.Parse(values[1]);
                    //0~1023을 0~90으로 바꾼다음에 45로 빼면 -45 ~ +45가 된다!

                    float x1 = (90 * x / 1023.0f) - 45;
                    float y1 = (90 * y / 1023.0f) - 45;

                    rod.SetDriveTarget(ArticulationDriveAxis.Z, x1);
                    rod.SetDriveTarget(ArticulationDriveAxis.Y, -y1);
                }
            }
        }
    }
}
