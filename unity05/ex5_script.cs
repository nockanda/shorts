using System.Collections;
using System.Collections.Generic;
using System.IO.Ports;
using System.Text;
using UnityEngine;

public class ex5_script : MonoBehaviour
{
    public ArticulationBody ex5_rod;

    SerialPort arduino = new SerialPort(); //인스턴스 생성!

    void Start()
    {
        //아두이노랑 통신환경 설정
        arduino.PortName = "COM3";
        arduino.BaudRate = 9600;
        arduino.Encoding = Encoding.UTF8;

        //일단 유니티와 아두이노를 연결한다
        arduino.Open();

        //유니티와 아두이노가 연결되면 이부분이 참이된다!
        if (arduino.IsOpen)
        {
            print("유니티와 아두이노가 연결됨!");
        }
    }

    void Update()
    {
        //유니티 엔진에서 아래 규칙에 따라서 수신한다!
        //1.아두이노랑 유니티가 연결되어있을 것!
        if (arduino.IsOpen)
        {
            //2.아두이노가 보낸 데이터가 유니티의 수신버퍼에 데이터가 존재할 것!
            if (arduino.BytesToRead > 0)
            {
                //상대방이 보낸 문자열에 종료문자가 \n이 있을때까지를 읽는다!
                string text = arduino.ReadLine();
                //ex5_rod의 위치값을 조정해야한다!
                //수신한값은 0~1023의 범위이고 ex5_rod의 위치값은 0~1.8의 범위이다!
                //여기서 0~1023의 범위를 0~1.8범위로 스케일링을 해야한다!
                int data = int.Parse(text);
                float value = 1.8f * (data / 1023.0f);
                ex5_rod.SetDriveTarget(ArticulationDriveAxis.X, value);
            }
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
