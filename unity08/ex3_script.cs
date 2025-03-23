using System.Collections;
using System.Collections.Generic;
using System.IO.Ports;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ex3_script : MonoBehaviour
{
    //ex3_slider�� ex3_text�� ��ƸԴ´�
    public Slider ex3_slider;
    public TMP_Text ex3_text;

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


    //����Ƽ�� ����ȴٸ�~
    void OnApplicationQuit()
    {
        //�Ƶ��̳�� ������ �����ع�����!
        arduino.Close();
        arduino.Dispose();
        print("�Ƶ��̳�� ����Ƽ�� ������ �����մϴ�!");
    }
}
