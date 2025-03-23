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
        //��ż����ϱ�!
        arduino.BaudRate = 9600;
        arduino.PortName = "COM3";
        //��Ʈ �����ϱ�!
        arduino.Open();

        if (arduino.IsOpen)
        {
            print("�Ƶ��̳�� �����!");
        }
        //�����̴��� ���콺�� �巡���ϸ� ���� ex3_text�� ��µȴ�!
        ex6_slider.onValueChanged.AddListener((float num) => {
            byte analog = (byte)num;
            ex6_text.text = analog.ToString();

            //0~255������ ���� ���ڿ��� �ٲ㼭 �����µ� ���� \n�� ������ ��������!
            string msg = analog + "\n";
            arduino.Write(msg);
        });

    }


    //����Ƽ�� ����ȴٸ�~
    void OnApplicationQuit()
    {
        //�Ƶ��̳�� ������ �����ع�����!
        arduino.Close();
        arduino.Dispose();
        print("�Ƶ��̳�� ����Ƽ�� ������ �����մϴ�!");
    }
}
