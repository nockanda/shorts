using System.Collections;
using System.Collections.Generic;
using System.IO.Ports;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ex4_script : MonoBehaviour
{
    public Slider ex4_slider;
    public TMP_InputField ex4_input;
    public ArticulationBody ex4_arm;

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

        ex4_slider.onValueChanged.AddListener((float value) => {
            int degree = (int)value;
            ex4_input.text = degree.ToString();
            //�Ƶ��̳뿡�� ���Ṯ�� \n������ �����ϱ�~
            arduino.Write(degree + "\n");
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
                int degree = int.Parse(data);
                ex4_arm.SetDriveTarget(ArticulationDriveAxis.X, degree);
            }
        }
    }
}
