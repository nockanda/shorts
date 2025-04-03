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

        //시리얼포트 설정을 해야했었구나!
        arduino.BaudRate = 9600;
        arduino.PortName = "COM3";

        //시리얼포트 개방하기!
        arduino.Open();

        if (arduino.IsOpen)
        {
            print("아두이노와 연결됨!");
        }

        ex4_slider.onValueChanged.AddListener((float value) => {
            int degree = (int)value;
            ex4_input.text = degree.ToString();
            //아두이노에게 종료문자 \n붙혀서 전송하기~
            arduino.Write(degree + "\n");
        });
    }

    void Update()
    {
        //아두이노가 0.1초간격으로 보내는 문자열을 수신한다!
        //종료문자가 LF('\n')으로 지정될 예정
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
