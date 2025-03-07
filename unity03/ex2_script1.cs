using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ex2_script1 : MonoBehaviour
{
    //���� �����Ϳ��� �Է��� ������Ʈ�� articuration body�� ������������� �Ʒ��� ���� ����ص� ������!
    public ArticulationBody front_left; //������ �չ���
    public ArticulationBody front_right; //������ �չ���
    public ArticulationBody rear_left; //������ �޹���
    public ArticulationBody rear_right; //������ �޹���

    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        //��ĭ�ٰ� ����Ű�е��� 8�� ������ �ڵ����� -100�� �ӵ��� �����Ѵ�!
        if(Input.GetKeyDown(KeyCode.Keypad8))
        {
            set_speed(-100, -100, -100, -100);
        }
        //����Ű�е��� 2�� ������ �ڵ����� 100�� �ӵ��� �����Ѵ�!
        if (Input.GetKeyDown(KeyCode.Keypad2))
        {
            set_speed(100, 100, 100, 100);
        }
        //Ű�е�5�� ������ �ӵ��� 0�̵Ǹ鼭 �극��ũ���ȴ�!
        if (Input.GetKeyDown(KeyCode.Keypad5))
        {
            set_speed(0, 0, 0, 0);
        }
        //Ű�е�6�� ������ ���������� �����ϰ� �������ʹ��� 2���� �����Ѵ�
        if (Input.GetKeyDown(KeyCode.Keypad6))
        {
            set_speed(-100, 100, -100, 100);
        }
        //Ű�е�5�� ������ ���������� �����ϰ� �������ʹ��� 2���� �����Ѵ�
        if (Input.GetKeyDown(KeyCode.Keypad4))
        {
            set_speed(100, -100, 100, -100);
        }
    }
    
    void set_speed(float speed1, float speed2, float speed3, float speed4)
    {
        front_left.SetDriveTargetVelocity(ArticulationDriveAxis.X, speed1);
        front_right.SetDriveTargetVelocity(ArticulationDriveAxis.X, speed2);
        rear_left.SetDriveTargetVelocity(ArticulationDriveAxis.X, speed3);
        rear_right.SetDriveTargetVelocity(ArticulationDriveAxis.X, speed4);
    }
}
