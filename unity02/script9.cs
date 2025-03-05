using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class script9 : MonoBehaviour
{
    //���� �Ʒ��� ���� ��ĭ�ٴ� object�� �����Ϳ��� �Է��� �޴� �������� �ȳ��ߴ�!
    //public GameObject joint1;
    //�׷��� �Է¹��� object�� �����ٵ� ������Ʈ�� ������ �ִٸ� �Ʒ��� ���� �����ִ�!
    public ArticulationBody joint1;
    public ArticulationBody joint2;
    public ArticulationBody joint3;

    
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.Keypad4))
        {
            //joint1�� 90��ȸ��
            joint1.SetDriveTarget(ArticulationDriveAxis.X, 90);
        }
        if (Input.GetKeyDown(KeyCode.Keypad5))
        {
            //joint1�� 0��ȸ��
            joint1.SetDriveTarget(ArticulationDriveAxis.X, 0);
        }
        if (Input.GetKeyDown(KeyCode.Keypad6))
        {
            //joint1�� -90��ȸ��
            joint1.SetDriveTarget(ArticulationDriveAxis.X, -90);
        }
        if (Input.GetKeyDown(KeyCode.Keypad8))
        {
            //joint3�� x=0�� ��ġ�� �̵�
            joint3.SetDriveTarget(ArticulationDriveAxis.X, 0);
        }
        if (Input.GetKeyDown(KeyCode.Keypad2))
        {
            //joint3�� x=-3�� ��ġ�� �̵�
            joint3.SetDriveTarget(ArticulationDriveAxis.X, -3);
        }
        if (Input.GetKeyDown(KeyCode.Keypad7))
        {
            //joint2�� y=0�� ��ġ�� �̵�
            joint2.SetDriveTarget(ArticulationDriveAxis.Y, 0);
        }
        if (Input.GetKeyDown(KeyCode.Keypad9))
        {
            //joint2�� y=-3�� ��ġ�� �̵�
            joint2.SetDriveTarget(ArticulationDriveAxis.Y, -3);
        }
    }
}
