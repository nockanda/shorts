using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class script9 : MonoBehaviour
{
    //원래 아래와 같이 녹칸다는 object를 에디터에서 입력을 받는 형식으로 안내했다!
    //public GameObject joint1;
    //그러나 입력받을 object가 관절바디 컴포넌트를 가지고 있다면 아래와 같이 쓸수있다!
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
            //joint1을 90도회전
            joint1.SetDriveTarget(ArticulationDriveAxis.X, 90);
        }
        if (Input.GetKeyDown(KeyCode.Keypad5))
        {
            //joint1을 0도회전
            joint1.SetDriveTarget(ArticulationDriveAxis.X, 0);
        }
        if (Input.GetKeyDown(KeyCode.Keypad6))
        {
            //joint1을 -90도회전
            joint1.SetDriveTarget(ArticulationDriveAxis.X, -90);
        }
        if (Input.GetKeyDown(KeyCode.Keypad8))
        {
            //joint3을 x=0의 위치로 이동
            joint3.SetDriveTarget(ArticulationDriveAxis.X, 0);
        }
        if (Input.GetKeyDown(KeyCode.Keypad2))
        {
            //joint3을 x=-3의 위치로 이동
            joint3.SetDriveTarget(ArticulationDriveAxis.X, -3);
        }
        if (Input.GetKeyDown(KeyCode.Keypad7))
        {
            //joint2을 y=0의 위치로 이동
            joint2.SetDriveTarget(ArticulationDriveAxis.Y, 0);
        }
        if (Input.GetKeyDown(KeyCode.Keypad9))
        {
            //joint2을 y=-3의 위치로 이동
            joint2.SetDriveTarget(ArticulationDriveAxis.Y, -3);
        }
    }
}
