using System.Collections;
using System.Collections.Generic;
using System.IO.Ports;
using System.Text;
using UnityEngine;

public class ex3_script : MonoBehaviour
{
    public ArticulationBody ex3_rod;

    SerialPort arduino = new SerialPort(); //인스턴스 생성!

    void Start()
    {
        //실린더의 초기상태는 후진이다
        ex3_rod.SetDriveTarget(ArticulationDriveAxis.X, 1.8f);

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
        if (arduino.IsOpen)
        {
            if (arduino.BytesToRead > 0)
            {
                byte[] buff = new byte[1];
                arduino.Read(buff, 0, 1);

                if (buff[0] == '1')
                {
                    print("아두이노의 버튼1이 눌려짐!");
                    //실린더는 전진한다!
                    ex3_rod.SetDriveTarget(ArticulationDriveAxis.X, 0);
                }
                else if (buff[0] == '2')
                {
                    print("아두이노의 버튼2가 눌려짐!");
                    //실린더가 후진한다!
                    ex3_rod.SetDriveTarget(ArticulationDriveAxis.X, 1.8f);
                }

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
