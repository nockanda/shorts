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
                //�Ƶ��̳밡 ���� 0~1023�� �д´�
                string data = arduino.ReadLine(); //0~1023�� ���ڿ�
                int analog = int.Parse(data); //0~1023������ ����
                float position = 1.5f*analog/1023; //0~1023�� 0~1.5�� ������ �°� �ٲٱ�
                rod.SetDriveTarget(ArticulationDriveAxis.Z, position);
            }
        }
    }
}
