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
                //���Ṯ�ڰ� \n�϶����� �аڴ�!
                string text = arduino.ReadLine();
                //text�ȿ� CSV�������� �µ��� ������ �ִ»�Ȳ�̱�!
                //text��� ���ڿ��� �����ڸ� �޸����ؼ� �и��ϰڴ�!
                string[] data = text.Split(','); 

                if(data.Length == 2)
                {
                    //�µ��� ������ ���ϱ� ©���� �����ʹ� ������ 2���̴�!
                    print($"�µ�={data[0]}'C, ����={data[1]}%");
                    //print("�µ�="+ data[0] + "'C, ����="+ data[1] + "%");
                    label1.text = $"TEMP: {data[0]}'C";
                    label2.text = $"HUMI: {data[1]}%";
                    //�µ��� �񱳿����� �Ϸ��� ���ڿ����Ѵ�
                    float temp = float.Parse(data[0]);
                    if(temp > 20)
                    {
                        //������
                        ex5_lamp_material.color = Color.red;
                    }
                    else if(temp > 10)
                    {
                        //�����
                        ex5_lamp_material.color = Color.yellow;
                    }
                    else
                    {
                        //���
                        ex5_lamp_material.color = Color.green;
                    }
                }
            }
        }
    }
}
