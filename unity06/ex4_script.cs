using System.Collections;
using System.Collections.Generic;
using System.IO.Ports;
using UnityEngine;

public class ex4_script : MonoBehaviour
{
    public ArticulationBody ex4_rod;

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
                //종료문자가 \n일때까지 읽겠다!
                string data = arduino.ReadLine();
                //data안에 아두이노가 보낸 숫자가 실수형으로 들어있군!(아직문자열임)
                //실수형 숫자로 바꿔야겠군!
                float dist = float.Parse(data);
                //거리값의 범위는 0~20이고 관절바디의 작동범위는 0~1.9이다!
                //그러므로 0~20의 범위를 0~2.0의범위로 비율에 맞게 스케일링을 해야한다!
                ex4_rod.SetDriveTarget(ArticulationDriveAxis.X, dist / 10);
            }
        }
    }
}
