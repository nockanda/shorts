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
            print("�Ƶ��̳�� �����!");
        }

    }

    void Update()
    {
        //�Ƶ��̳�� ������ �Ǿ����鼭, ���Ź��ۿ� ������ �����Ͱ� �ִ°�?
        if (arduino.IsOpen)
        {
            if (arduino.BytesToRead > 0)
            {
                //x��,y��
                string data = arduino.ReadLine(); //0~1023�� ���ڿ�
                string[] values = data.Split(',');

                if(values.Length == 2)
                {
                    //���ڷ� �ٲٱ�~
                    int x = int.Parse(values[0]);
                    int y = int.Parse(values[1]);
                    //0~1023�� 0~90���� �ٲ۴����� 45�� ���� -45 ~ +45�� �ȴ�!

                    float x1 = (90 * x / 1023.0f) - 45;
                    float y1 = (90 * y / 1023.0f) - 45;

                    rod.SetDriveTarget(ArticulationDriveAxis.Z, x1);
                    rod.SetDriveTarget(ArticulationDriveAxis.Y, -y1);
                }
            }
        }
    }
}
