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
                //���Ṯ�ڰ� \n�϶����� �аڴ�!
                string data = arduino.ReadLine();
                //data�ȿ� �Ƶ��̳밡 ���� ���ڰ� �Ǽ������� ����ֱ�!(�������ڿ���)
                //�Ǽ��� ���ڷ� �ٲ�߰ڱ�!
                float dist = float.Parse(data);
                //�Ÿ����� ������ 0~20�̰� �����ٵ��� �۵������� 0~1.9�̴�!
                //�׷��Ƿ� 0~20�� ������ 0~2.0�ǹ����� ������ �°� �����ϸ��� �ؾ��Ѵ�!
                ex4_rod.SetDriveTarget(ArticulationDriveAxis.X, dist / 10);
            }
        }
    }
}
