using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ex2_script1 : MonoBehaviour
{
    //만약 에디터에서 입력할 오브젝트가 articuration body를 가지고있을경우 아래와 같이 사용해도 무방함!
    public ArticulationBody front_left; //운전석 앞바퀴
    public ArticulationBody front_right; //조수석 앞바퀴
    public ArticulationBody rear_left; //운전석 뒷바퀴
    public ArticulationBody rear_right; //조수석 뒷바퀴

    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        //녹칸다가 숫자키패드의 8을 누르면 자동차가 -100의 속도로 전진한다!
        if(Input.GetKeyDown(KeyCode.Keypad8))
        {
            set_speed(-100, -100, -100, -100);
        }
        //숫자키패드의 2를 누르면 자동차가 100의 속도로 후진한다!
        if (Input.GetKeyDown(KeyCode.Keypad2))
        {
            set_speed(100, 100, 100, 100);
        }
        //키패드5를 누르면 속도가 0이되면서 브레이크가된다!
        if (Input.GetKeyDown(KeyCode.Keypad5))
        {
            set_speed(0, 0, 0, 0);
        }
        //키패드6을 누르면 운전석쪽은 전진하고 조수석쪽바퀴 2개는 후진한다
        if (Input.GetKeyDown(KeyCode.Keypad6))
        {
            set_speed(-100, 100, -100, 100);
        }
        //키패드5를 누르면 운전석쪽은 후진하고 조수석쪽바퀴 2개는 전진한다
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
