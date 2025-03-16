using System.Collections;
using System.Collections.Generic;
using System.IO.Ports;
using TMPro;
using UnityEngine;

public class ex5_script : MonoBehaviour
{
    public TMP_Text label1;
    public TMP_Text label2;
    public GameObject ex5_lamp;

    Material ex5_lamp_material;

    SerialPort arduino = new SerialPort();

    void Start()
    {
        ex5_lamp_material = ex5_lamp.GetComponent<Renderer>().material;
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
                //종료문자가 \n일때까지 읽겠다!
                string text = arduino.ReadLine();
                //text안에 CSV형식으로 온도와 습도가 있는상황이군!
                //text라는 문자열을 구분자를 콤마로해서 분리하겠다!
                string[] data = text.Split(','); 

                if(data.Length == 2)
                {
                    //온도와 습도가 오니까 짤려진 데이터는 갯수가 2개이다!
                    print($"온도={data[0]}'C, 습도={data[1]}%");
                    //print("온도="+ data[0] + "'C, 습도="+ data[1] + "%");
                    label1.text = $"TEMP: {data[0]}'C";
                    label2.text = $"HUMI: {data[1]}%";
                    //온도에 비교연산을 하려면 숫자여야한다
                    float temp = float.Parse(data[0]);
                    if(temp > 20)
                    {
                        //빨간색
                        ex5_lamp_material.color = Color.red;
                    }
                    else if(temp > 10)
                    {
                        //노란색
                        ex5_lamp_material.color = Color.yellow;
                    }
                    else
                    {
                        //녹색
                        ex5_lamp_material.color = Color.green;
                    }
                }
            }
        }
    }
}
