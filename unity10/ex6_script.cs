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
            print("아두이노랑 연결됨!");
        }
        
        slider.onValueChanged.AddListener((float num) => {
            if (arduino.IsOpen)
            {
                //num값이 0~180사이에 소수점이 있는 숫자이다!
                int value = (int)num;
                input.text = value.ToString();
                arduino.Write(value + "\n");
            }
        });
    }

    void Update()
    {
        //아두이노랑 연결이 되었으면서, 수신버퍼에 가져올 데이터가 있는가?
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
