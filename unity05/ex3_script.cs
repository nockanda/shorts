using System.Collections;
using System.Collections.Generic;
using System.IO.Ports;
using System.Text;
using UnityEngine;

public class ex3_script : MonoBehaviour
{
    public ArticulationBody ex3_rod;

    SerialPort arduino = new SerialPort(); //�ν��Ͻ� ����!

    void Start()
    {
        //�Ǹ����� �ʱ���´� �����̴�
        ex3_rod.SetDriveTarget(ArticulationDriveAxis.X, 1.8f);

        //�Ƶ��̳�� ���ȯ�� ����
        arduino.PortName = "COM3";
        arduino.BaudRate = 9600;
        arduino.Encoding = Encoding.UTF8;

        //�ϴ� ����Ƽ�� �Ƶ��̳븦 �����Ѵ�
        arduino.Open();

        //����Ƽ�� �Ƶ��̳밡 ����Ǹ� �̺κ��� ���̵ȴ�!
        if (arduino.IsOpen)
        {
            print("����Ƽ�� �Ƶ��̳밡 �����!");
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
                    print("�Ƶ��̳��� ��ư1�� ������!");
                    //�Ǹ����� �����Ѵ�!
                    ex3_rod.SetDriveTarget(ArticulationDriveAxis.X, 0);
                }
                else if (buff[0] == '2')
                {
                    print("�Ƶ��̳��� ��ư2�� ������!");
                    //�Ǹ����� �����Ѵ�!
                    ex3_rod.SetDriveTarget(ArticulationDriveAxis.X, 1.8f);
                }

            }
        }
    }
    //����Ƽ�� ����ȴٸ�~
    void OnApplicationQuit()
    {
        //�Ƶ��̳�� ������ �����ع�����!
        arduino.Close();
        arduino.Dispose();
        print("�Ƶ��̳�� ����Ƽ�� ������ �����մϴ�!");
    }
}
