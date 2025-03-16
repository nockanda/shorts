using System.Collections;
using System.Collections.Generic;
using System.IO.Ports;
using System.Text;
using UnityEngine;

public class ex5_script : MonoBehaviour
{
    public ArticulationBody ex5_rod;

    SerialPort arduino = new SerialPort(); //�ν��Ͻ� ����!

    void Start()
    {
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
        //����Ƽ �������� �Ʒ� ��Ģ�� ���� �����Ѵ�!
        //1.�Ƶ��̳�� ����Ƽ�� ����Ǿ����� ��!
        if (arduino.IsOpen)
        {
            //2.�Ƶ��̳밡 ���� �����Ͱ� ����Ƽ�� ���Ź��ۿ� �����Ͱ� ������ ��!
            if (arduino.BytesToRead > 0)
            {
                //������ ���� ���ڿ��� ���Ṯ�ڰ� \n�� ������������ �д´�!
                string text = arduino.ReadLine();
                //ex5_rod�� ��ġ���� �����ؾ��Ѵ�!
                //�����Ѱ��� 0~1023�� �����̰� ex5_rod�� ��ġ���� 0~1.8�� �����̴�!
                //���⼭ 0~1023�� ������ 0~1.8������ �����ϸ��� �ؾ��Ѵ�!
                int data = int.Parse(text);
                float value = 1.8f * (data / 1023.0f);
                ex5_rod.SetDriveTarget(ArticulationDriveAxis.X, value);
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
