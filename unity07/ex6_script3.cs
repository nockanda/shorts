using System.Collections;
using System.Collections.Generic;
using System.IO.Ports;
using UnityEngine;

public class ex6_script3 : MonoBehaviour
{
    SerialPort arduino = new SerialPort();

    void Start()
    {
        //��ż����ϱ�!
        arduino.BaudRate = 9600;
        arduino.PortName = "COM3";
        //��Ʈ �����ϱ�!
        arduino.Open();

        if (arduino.IsOpen)
        {
            print("�Ƶ��̳�� �����!");
        }
    }

    public void led_on()
    {
        if(arduino.IsOpen)
        {
            arduino.Write("LED ON\n");
        }
    }
    public void led_off()
    {
        if (arduino.IsOpen)
        {
            arduino.Write("LED OFF\n");
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
