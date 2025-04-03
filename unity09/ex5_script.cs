using System.Collections;
using System.Collections.Generic;
using System.IO.Ports;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Windows;

public class ex5_script : MonoBehaviour
{
    public Button btn1;
    public Button btn2;
    public Button btn3;
    public Button btn4;
    public Button btn5;
    public TMP_InputField input1; //���Ƿ�
    public TMP_InputField input2; //������
    public ArticulationBody ex5_arm;

    Material material;

    SerialPort arduino = new SerialPort();

    void Start()
    {

        //�ø�����Ʈ ������ �ؾ��߾�����!
        arduino.BaudRate = 9600;
        arduino.PortName = "COM3";

        //�ø�����Ʈ �����ϱ�!
        arduino.Open();

        if (arduino.IsOpen)
        {
            print("�Ƶ��̳�� �����!");
        }

        btn1.onClick.AddListener(() => {
            if (arduino.IsOpen)
            {
                arduino.Write("0");
            }
        });
        btn2.onClick.AddListener(() => {
            if (arduino.IsOpen)
            {
                arduino.Write("1");
            }
        });
        btn3.onClick.AddListener(() => {
            if (arduino.IsOpen)
            {
                arduino.Write("2");
            }
        });
        btn4.onClick.AddListener(() => {
            if (arduino.IsOpen)
            {
                arduino.Write("3");
            }
        });
        btn5.onClick.AddListener(() => {
            if (arduino.IsOpen)
            {
                arduino.Write("4");
            }
        });
    }

    void Update()
    {
        //�Ƶ��̳밡 0.1�ʰ������� ������ ���ڿ��� �����Ѵ�!
        //���Ṯ�ڰ� LF('\n')���� ������ ����
        if (arduino.IsOpen)
        {
            if (arduino.BytesToRead > 0)
            {
                string data = arduino.ReadLine();
                int step = int.Parse(data);
                //4096�� 360���̴�!
                float degree = (360 * step) / 4096.0f;
                input1.text = data;
                input2.text = ((int)degree).ToString();
                ex5_arm.SetDriveTarget(ArticulationDriveAxis.X, degree);
            }
        }
    }
}
