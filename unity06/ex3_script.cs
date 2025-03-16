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
        //�Ƶ��̳�� ��ż���
        arduino.PortName = "COM3";
        arduino.BaudRate = 9600;

        arduino.Open(); //��Ʈ ����!

        if (arduino.IsOpen)
        {
            print("�Ƶ��̳�� �����!");
        }
    }

    void Update()
    {
        if (arduino.IsOpen)
        {
            if (arduino.BytesToRead > 0)
            {
                //�Ƶ��̳밡 1���� ���ڸ� �����ϴϱ� 1���� ������ OK!
                int data = arduino.ReadByte();
                if (data == '0')
                {
                    //print("�����ȵ�!");
                    //ex3_lamp�� ������ �Ͼ�������Ѵ�
                    ex3_lamp_material.color = Color.white;
                }
                else if (data == '1')
                {
                    //print("��ݰ���!");
                    //ex3_lamp�� ������ �����������Ѵ�
                    ex3_lamp_material.color = Color.red;
                    //����� �����Ǹ� ī��Ʈ�� 1���ø���!
                    ex3_count++;
                    //ť�꿡 �پ��ִ� ex3_label�� ������Ʈ���ش�!
                    ex3_label.text = "COUNT = " + ex3_count;
                }
            }
        }
    }
}
