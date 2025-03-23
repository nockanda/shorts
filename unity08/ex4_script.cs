using System.Collections;
using System.Collections.Generic;
using System.IO.Ports;
using UnityEngine;
using UnityEngine.UI;

public class ex4_script : MonoBehaviour
{
    //��ư3���� ��ƸԾ ����ư�� ������ �Ƶ��̳뿡�� ����Ѵ�
    public Button ex4_btn1;
    public Button ex4_btn2;
    public Button ex4_btn3;
    public Button ex4_btn4;

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

        //��ư�� �������� ��ư���� ������ ���� �����Ѵ�!
        ex4_btn1.onClick.AddListener(() => {
            //�Ƶ��̳뿡�� 0������ �����Ѵ�
            if (arduino.IsOpen)
            {
                arduino.Write("256\n");
            }
        });
        ex4_btn2.onClick.AddListener(() => {
            //�Ƶ��̳뿡�� 90������ �����Ѵ�
            if (arduino.IsOpen)
            {
                arduino.Write("-256\n");
            }
        });
        ex4_btn3.onClick.AddListener(() => {
            //�Ƶ��̳뿡�� 180������ �����Ѵ�
            if (arduino.IsOpen)
            {
                arduino.Write("512\n");
            }
        });
        ex4_btn4.onClick.AddListener(() => {
            //�Ƶ��̳뿡�� 180������ �����Ѵ�
            if (arduino.IsOpen)
            {
                arduino.Write("-512\n");
            }
        });
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
