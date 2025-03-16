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
        //�Ƶ��̳�� ��ż���
        arduino.PortName = "COM3";
        arduino.BaudRate = 9600;

        arduino.Open(); //��Ʈ ����!

        if (arduino.IsOpen)
        {
            print("�Ƶ��̳�� �����!");
        }
    }

    void Update()
    {
        if (arduino.IsOpen)
        {
            if (arduino.BytesToRead > 0)
            {
                //�Ƶ��̳밡 1���� ���ڸ� �����ϴϱ� 1���� ������ OK!
                int data = arduino.ReadByte();
                if (data == '0')
                {
                    print("������ �ȵ�!");
                    //door1�� door2�� 1.9�ǰ��� ������ ����!
                    door1.SetDriveTarget(ArticulationDriveAxis.Y, 1.9f);
                    door2.SetDriveTarget(ArticulationDriveAxis.Y, 1.9f);
                }
                else if (data == '1')
                {
                    print("������ ������!!");
                    //door1�� door2�� 0�ǰ��� ������ ����!
                    door1.SetDriveTarget(ArticulationDriveAxis.Y, 0f);
                    door2.SetDriveTarget(ArticulationDriveAxis.Y, 0f);
                }
            }
        }
    }
}
