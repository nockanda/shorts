using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ex9_script1 : MonoBehaviour
{
    //9.간단한 실린더 모양을 만들고 ex9_button1을 누르면 실린더가 전진,
    //ex9_button2를 누르면 실린더가 후진하도록 하시오!(ex9_script1.cs)
    public Button button1; //전진
    public Button button2; //후진
    //실린더의 관절바디
    public ArticulationBody rod;

    void Start()
    {
        button1.onClick.AddListener(forward);
        button2.onClick.AddListener(backward);
        //초기값으로 rod에 1.8의 값을 입력해준다!(후진상태)
        rod.SetDriveTarget(ArticulationDriveAxis.X, 1.8f);
    }

    void forward()
    {
        //로드가 전진한다
        rod.SetDriveTarget(ArticulationDriveAxis.X, 0f);
    }
    void backward()
    {
        //로드가 후진한다
        rod.SetDriveTarget(ArticulationDriveAxis.X, 1.8f);
    }
}
