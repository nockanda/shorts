using System.Collections;
using System.Collections.Generic;
using System.IO.Ports;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ex3_script : MonoBehaviour
{
    //ex3_slider와 ex3_text를 잡아먹는다
    public Slider ex3_slider;
    public TMP_Text ex3_text;

    SerialPort arduino = new SerialPort();

    void Start()
    {
        //통신설정하기!
        arduino.BaudRate = 9600;
        arduino.PortName = "COM3";
        //포트 개방하기!
        arduino.Open();

        if (arduino.IsOpen)
        {
            print("아두이노랑 연결됨!");
        }
        //슬라이더를 마우스로 드래그하면 값이 ex3_text에 출력된다!
        ex3_slider.onValueChanged.AddListener((float num) => {
            byte degree = (byte)num;
            ex3_text.text = degree.ToString();
            byte[] data = { degree };
            if(arduino.IsOpen)
            {
                arduino.Write(data, 0, 1);
            }
        });

    }


    //유니티가 종료된다면~
    void OnApplicationQuit()
    {
        //아두이노랑 연결을 해제해버린다!
        arduino.Close();
        arduino.Dispose();
        print("아두이노와 유니티의 접속을 해제합니다!");
    }
}
