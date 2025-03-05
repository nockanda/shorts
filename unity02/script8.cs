using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class script8 : MonoBehaviour
{
    ArticulationBody myjoystick;

    void Start()
    {
        myjoystick = this.GetComponent<ArticulationBody>();
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.UpArrow))
        {
            //y���� -45���� �̵�
            myjoystick.SetDriveTarget(ArticulationDriveAxis.Y, -45);
        }
        if (Input.GetKeyDown(KeyCode.DownArrow))
        {
            //y���� 45���� �̵�
            myjoystick.SetDriveTarget(ArticulationDriveAxis.Y, 45);
        }
        if (Input.GetKeyDown(KeyCode.RightArrow))
        {
            //z���� -45���� �̵�
            myjoystick.SetDriveTarget(ArticulationDriveAxis.Z, -45);
        }
        if (Input.GetKeyDown(KeyCode.LeftArrow))
        {
            //z���� 45���� �̵�
            myjoystick.SetDriveTarget(ArticulationDriveAxis.Z, 45);
        }
        if (Input.GetKeyDown(KeyCode.P))
        {
            //����ġ
            myjoystick.SetDriveTarget(ArticulationDriveAxis.Y, 0);
            myjoystick.SetDriveTarget(ArticulationDriveAxis.Z, 0);
        }
    }
}
