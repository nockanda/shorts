using System.Collections;
using System.Collections.Generic;
using System.IO.Ports;
using UnityEngine;
using UnityEngine.UI;

public class ex2_script : MonoBehaviour
{
    public Button ex2_btn1;
    public Button ex2_btn2;

    SerialPort arduino = new SerialPort();

    void Start()
    {
        //�̺�Ʈ �����ʵ��
        ex2_btn1.onClick.AddListener(btn1_click);
        ex2_btn2.onClick.AddListener(btn2_click);

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
    void btn1_click()
    {
        //�Ƶ��̳뿡�� ���� '1'�� �����Ѵ�
        if(arduino.IsOpen)
        {
            arduino.Write("1");
        }
    }
    void btn2_click()
    {
        //�Ƶ��̳뿡�� ���� '2'�� �����Ѵ�
        if (arduino.IsOpen)
        {
            arduino.Write("2");
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
