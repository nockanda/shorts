using System.Collections;
using System.Collections.Generic;
using System.IO.Ports;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ex6_script : MonoBehaviour
{
    public Slider ex6_slider;
    public TMP_Text ex6_text;

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
        ex6_slider.onValueChanged.AddListener((float num) => {
            byte analog = (byte)num;
            ex6_text.text = analog.ToString();

            //0~255사이의 값을 문자열로 바꿔서 보내는데 끝에 \n을 붙혀서 보내야함!
            string msg = analog + "\n";
            arduino.Write(msg);
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
