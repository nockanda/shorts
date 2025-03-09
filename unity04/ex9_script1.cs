using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ex9_script1 : MonoBehaviour
{
    //9.������ �Ǹ��� ����� ����� ex9_button1�� ������ �Ǹ����� ����,
    //ex9_button2�� ������ �Ǹ����� �����ϵ��� �Ͻÿ�!(ex9_script1.cs)
    public Button button1; //����
    public Button button2; //����
    //�Ǹ����� �����ٵ�
    public ArticulationBody rod;

    void Start()
    {
        button1.onClick.AddListener(forward);
        button2.onClick.AddListener(backward);
        //�ʱⰪ���� rod�� 1.8�� ���� �Է����ش�!(��������)
        rod.SetDriveTarget(ArticulationDriveAxis.X, 1.8f);
    }

    void forward()
    {
        //�ε尡 �����Ѵ�
        rod.SetDriveTarget(ArticulationDriveAxis.X, 0f);
    }
    void backward()
    {
        //�ε尡 �����Ѵ�
        rod.SetDriveTarget(ArticulationDriveAxis.X, 1.8f);
    }
}
