using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ex12_script1 : MonoBehaviour
{
    public Button button1; //arm1+
    public Button button2; //arm1-
    public Button button3; //arm2+
    public Button button4; //arm2-
    public Button button5; //arm3+
    public Button button6; //arm3-

    public ArticulationBody arm1;
    public ArticulationBody arm2;
    public ArticulationBody arm3;

    void Start()
    {
        button1.onClick.AddListener(arm1_plus);
        button2.onClick.AddListener(arm1_minus);
        button3.onClick.AddListener(arm2_plus);
        button4.onClick.AddListener(arm2_minus);
        button5.onClick.AddListener(arm3_plus);
        button6.onClick.AddListener(arm3_minus);
    }

    void arm1_plus()
    {
        float target = arm1.xDrive.target;
        target += 5;
        if (target > 90) target = 90;
        arm1.SetDriveTarget(ArticulationDriveAxis.X, target);
    }
    void arm1_minus()
    {
        float target = arm1.xDrive.target;
        target -= 5;
        if (target < -90) target = -90;
        arm1.SetDriveTarget(ArticulationDriveAxis.X, target);
    }
    void arm2_plus()
    {
        //-2~1.5 yÃà
        float target = arm2.yDrive.target;
        target += 0.1f;
        if (target > 1.5) target = 1.5f;
        arm2.SetDriveTarget(ArticulationDriveAxis.Y, target);
    }
    void arm2_minus()
    {
        float target = arm2.yDrive.target;
        target -= 0.1f;
        if (target < -2) target = -2f;
        arm2.SetDriveTarget(ArticulationDriveAxis.Y, target);
    }
    void arm3_plus()
    {
        //-2 ~2 xÃà
        float target = arm3.xDrive.target;
        target += 0.1f;
        if (target > 2) target = 2f;
        arm3.SetDriveTarget(ArticulationDriveAxis.X, target);
    }
    void arm3_minus()
    {
        float target = arm3.xDrive.target;
        target -= 0.1f;
        if (target < -2) target = -2f;
        arm3.SetDriveTarget(ArticulationDriveAxis.X, target);
    }
}
