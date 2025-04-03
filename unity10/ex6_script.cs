using System.Collections;
using System.Collections.Generic;
using System.IO.Ports;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ex6_script : MonoBehaviour
{
    public Slider slider;
    public TMP_InputField input;
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
        
        slider.onValueChanged.AddListener((float num) => {
            if (arduino.IsOpen)
            {
                //num���� 0~180���̿� �Ҽ����� �ִ� �����̴�!
                int value = (int)num;
                input.text = value.ToString();
                arduino.Write(value + "\n");
            }
        });
    }

    void Update()
    {
        //�Ƶ��̳�� ������ �Ǿ����鼭, ���Ź��ۿ� ������ �����Ͱ� �ִ°�?
        if (arduino.IsOpen)
        {
            if (arduino.BytesToRead > 0)
            {
                string data = arduino.ReadLine();
                int degree = int.Parse(data);
                rod.SetDriveTarget(ArticulationDriveAxis.X, -degree);
            }
        }
    }
}
