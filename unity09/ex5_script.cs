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
    public TMP_InputField input1; //스탭량
    public TMP_InputField input2; //각도값
    public ArticulationBody ex5_arm;

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
        //아두이노가 0.1초간격으로 보내는 문자열을 수신한다!
        //종료문자가 LF('\n')으로 지정될 예정
        if (arduino.IsOpen)
        {
            if (arduino.BytesToRead > 0)
            {
                string data = arduino.ReadLine();
                int step = int.Parse(data);
                //4096이 360도이다!
                float degree = (360 * step) / 4096.0f;
                input1.text = data;
                input2.text = ((int)degree).ToString();
                ex5_arm.SetDriveTarget(ArticulationDriveAxis.X, degree);
            }
        }
    }
}
