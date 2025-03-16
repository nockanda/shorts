using System.Collections;
using System.Collections.Generic;
using System.IO.Ports;
using TMPro;
using UnityEngine;

public class ex3_script : MonoBehaviour
{
    public GameObject ex3_lamp;
    public TMP_Text ex3_label;

    Material ex3_lamp_material;
    int ex3_count = 0;

    SerialPort arduino = new SerialPort();

    void Start()
    {
        ex3_lamp_material = ex3_lamp.GetComponent<Renderer>().material;
        //아두이노랑 통신설정
        arduino.PortName = "COM3";
        arduino.BaudRate = 9600;

        arduino.Open(); //포트 개방!

        if (arduino.IsOpen)
        {
            print("아두이노랑 연결됨!");
        }
    }

    void Update()
    {
        if (arduino.IsOpen)
        {
            if (arduino.BytesToRead > 0)
            {
                //아두이노가 1개의 문자를 전송하니까 1개만 읽으면 OK!
                int data = arduino.ReadByte();
                if (data == '0')
                {
                    //print("감지안됨!");
                    //ex3_lamp의 색상을 하얀색으로한다
                    ex3_lamp_material.color = Color.white;
                }
                else if (data == '1')
                {
                    //print("충격감지!");
                    //ex3_lamp의 색상을 빨간색으로한다
                    ex3_lamp_material.color = Color.red;
                    //충격이 감지되면 카운트를 1씩올린다!
                    ex3_count++;
                    //큐브에 붙어있는 ex3_label에 업데이트해준다!
                    ex3_label.text = "COUNT = " + ex3_count;
                }
            }
        }
    }
}
